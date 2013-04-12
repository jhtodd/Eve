//-----------------------------------------------------------------------
// <copyright file="EveCache.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Runtime.Caching;
  using System.Threading;

  using Eve.Character;
  using Eve.Universe;

  using FreeNet;

  /// <summary>
  /// A central cache for storing commonly used EVE values.
  /// </summary>
  public partial class EveCache
    : IDisposable,
      IEnumerable<KeyValuePair<string, object>>
  {
    /// <summary>
    /// The prefix used to simulate a cache region for EVE-related items if using a shared cache
    /// (such as the default MemoryCache).
    /// </summary>
    protected internal const string EveCacheRegionPrefix = "E_";

    private static readonly CacheRegionMap RegionMapInstance = new CacheRegionMap();

    private ObjectCache innerCache;
    private ReaderWriterLockSlim masterLock;
    private ConcurrentDictionary<string, ReaderWriterLockSlim> regionLocks;
    private CacheStatistics statistics;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveCache class that uses the default
    /// <see cref="MemoryCache" /> to store data.
    /// </summary>
    public EveCache() : this(MemoryCache.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EveCache class that uses a custom memory
    /// cache with the specified size to store data.
    /// </summary>
    /// <param name="cacheSize">
    /// The size of the cache, in megabytes.
    /// </param>
    public EveCache(int cacheSize) : this(CreateCache(cacheSize))
    {
      Contract.Requires(cacheSize > 0, Resources.Messages.EveCache_CacheSizeCannotBeNegative);
    }

    /// <summary>
    /// Initializes a new instance of the EveCache class that uses the specified
    /// <see cref="ObjectCache" /> to store data.
    /// </summary>
    /// <param name="cache">
    /// The <see cref="ObjectCache" /> used to store items.
    /// </param>
    public EveCache(ObjectCache cache) : base()
    {
      Contract.Requires(cache != null, Resources.Messages.EveCache_CacheCannotBeNull);

      this.innerCache = cache;

      // The master lock needs recursive read locks so that region locks don't
      // unnecessarily block.
      this.masterLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

      this.regionLocks = new ConcurrentDictionary<string, ReaderWriterLockSlim>();
      this.statistics = new CacheStatistics();
    }

    /* Properties */

    /// <summary>
    /// Gets statistics about cache usage.
    /// </summary>
    /// <value>
    /// A <see cref="CacheStatistics" /> object containing statistics about how
    /// the cache is being used.
    /// </value>
    public CacheStatistics Statistics
    {
      get
      {
        Contract.Ensures(Contract.Result<CacheStatistics>() != null);
        return this.statistics;
      }
    }

    /// <summary>
    /// Gets the type map used to define cache regions for different types of
    /// cacheable objects.
    /// </summary>
    /// <value>
    /// The <see cref="CacheRegionMap" /> used to define cache regions for different
    /// types of cacheable objects.
    /// </value>
    internal static CacheRegionMap RegionMap
    {
      get
      {
        Contract.Ensures(Contract.Result<CacheRegionMap>() != null);
        return RegionMapInstance;
      }
    }

    /// <summary>
    /// Gets the inner cache object used to store data.
    /// </summary>
    /// <value>
    /// The <see cref="ObjectCache" /> used to store data.
    /// </value>
    protected internal ObjectCache InnerCache
    {
      get
      {
        Contract.Ensures(Contract.Result<ObjectCache>() != null);
        return this.innerCache;
      }
    }

    /// <summary>
    /// Gets the lock used to restrict concurrent access to the cache.
    /// </summary>
    /// <value>
    /// A <see cref="ReaderWriterLockSlim" /> used to restrict concurrent access
    /// to the cache.
    /// </value>
    protected internal ReaderWriterLockSlim MasterLock
    {
      get
      {
        Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
        return this.masterLock;
      }
    }

    /// <summary>
    /// Gets the set of locks used to restrict concurrent access to individual
    /// cache regions.
    /// </summary>
    /// <value>
    /// A collection of region locks used to restrict concurrent access to the
    /// cache.
    /// </value>
    protected internal ConcurrentDictionary<string, ReaderWriterLockSlim> RegionLocks
    {
      get
      {
        Contract.Ensures(Contract.Result<ConcurrentDictionary<string, ReaderWriterLockSlim>>() != null);
        return this.regionLocks;
      }
    }

    /* Methods */

    /// <summary>
    /// Add the item to the cache, replacing any existing item with the same ID.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item to add to the cache.
    /// </typeparam>
    /// <param name="value">
    /// The item to add to the cache.
    /// </param>
    public void AddOrReplace<T>(T value) where T : IEveCacheable
    {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);
      this.AddOrReplace(value, false);
    }

    /// <summary>
    /// Add the item to the cache, replacing any existing item with the same ID.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item to add to the cache.
    /// </typeparam>
    /// <param name="value">
    /// The item to add to the cache.
    /// </param>
    /// <param name="permanent">
    /// Specifies whether to add the value permanently or whether it can be
    /// automatically evicted.
    /// </param>
    /// <remarks>
    /// <para>
    /// Items added with <paramref name="permanent" /> equal to
    /// <see langword="true" /> are immune to automatic eviction, but can
    /// still be removed or overwritten manually.
    /// </para>
    /// </remarks>
    public void AddOrReplace<T>(T value, bool permanent) where T : IEveCacheable
    {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, value.CacheKey);

      this.EnterWriteLock(region);

      try
      {
        this.Statistics.Writes++;
        this.InnerCache.Set(key, value, new CacheItemPolicy { Priority = permanent ? CacheItemPriority.NotRemovable : CacheItemPriority.Default });
      }
      finally
      {
        this.ExitWriteLock(region);
      }
    }

    /// <summary>
    /// Removes all entries from the cache.
    /// </summary>
    public void Clear()
    {
      List<string> keysToRemove = new List<string>();

      this.EnterWriteLock(null);

      try
      {
        foreach (KeyValuePair<string, object> entry in this.InnerCache)
        {
          Contract.Assume(entry.Key != null);
          if (entry.Key.StartsWith(EveCacheRegionPrefix))
          {
            keysToRemove.Add(entry.Key);
          }
        }

        foreach (string key in keysToRemove)
        {
          this.InnerCache.Remove(key);
        }
      }
      finally
      {
        this.ExitWriteLock(null);
      }
    }

    /// <summary>
    /// Returns a value indicating whether an item with the specified ID is
    /// contained in the cache.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the item to locate.
    /// </typeparam>
    /// <param name="id">
    /// The ID to locate in the cache.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if an item with the specified ID is contained
    /// in the cache; otherwise <see langword="false" />.
    /// </returns>
    public bool Contains<T>(object id)
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      this.EnterReadLock(region);

      try
      {
        return this.InnerCache.Contains(key);
      }
      finally
      {
        this.ExitReadLock(region);

        // TODO: These should be unnecessary.  Bug in static checker?  Check with future version.
        Contract.Assume(this.innerCache != null);
        Contract.Assume(this.masterLock != null);
        Contract.Assume(this.regionLocks != null);
        Contract.Assume(this.statistics != null);
      }
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return ((IEnumerable<KeyValuePair<string, object>>)this.InnerCache).Where(x => x.Key.StartsWith(EveCacheRegionPrefix)).GetEnumerator();
    }

    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to add or retrieve.
    /// </param>
    /// <param name="valueFactory">
    /// The <see cref="Func{TOutput}" /> which will generate the value to be
    /// added if a matching item cannot be found in the cache.
    /// </param>
    /// <returns>
    /// The item of the desired type and with the specified key, if a matching
    /// item is contained in the cache.  Otherwise, the result of executing
    /// <paramref name="valueFactory" />.
    /// </returns>
    public T GetOrAdd<T>(object id, Func<T> valueFactory) where T : IEveCacheable
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);
      Contract.Requires(valueFactory != null, Resources.Messages.EveCache_ValueFactoryCannotBeNull);
      Contract.Ensures(Contract.Result<T>() != null);

      return this.GetOrAdd(id, valueFactory, false);
    }

    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to add or retrieve.
    /// </param>
    /// <param name="valueFactory">
    /// The <see cref="Func{TOutput}" /> which will generate the value to be
    /// added if a matching item cannot be found in the cache.
    /// </param>
    /// <param name="permanent">
    /// Specifies whether to add the value permanently or whether it can be
    /// automatically evicted.
    /// </param>
    /// <returns>
    /// The item of the desired type and with the specified key, if a matching
    /// item is contained in the cache.  Otherwise, the result of executing
    /// <paramref name="valueFactory" />.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Items added with <paramref name="permanent" /> equal to
    /// <see langword="true" /> are immune to automatic eviction, but can
    /// still be removed or overwritten manually.
    /// </para>
    /// </remarks>
    public T GetOrAdd<T>(object id, Func<T> valueFactory, bool permanent) where T : IEveCacheable
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);
      Contract.Requires(valueFactory != null, Resources.Messages.EveCache_ValueFactoryCannotBeNull);
      Contract.Ensures(Contract.Result<T>() != null);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      this.EnterReadLock(region);

      try
      {
        // If the cache contains the desired key, return it
        if (this.InnerCache.Contains(key))
        {
          this.Statistics.Hits++;
          var result = this.InnerCache[key];
          Contract.Assume(result != null);
          return (T)result;
        }
      }
      finally
      {
        this.ExitReadLock(region);
      }

      // Otherwise, get our value to be added.  Do this outside of a lock in 
      // case valueFactory() itself wants to read from or add something to the
      // cache.
      T value = valueFactory();
      Contract.Assume(value != null);

      // Check to make sure the value being added actually has the same key as
      // the value we were passed -- otherwise the cache could be put into an
      // inconsistent state.
      string verifyKey = EveCache.GetCacheKey(region, value.CacheKey);

      if (!object.Equals(key, verifyKey))
      {
        throw new InvalidOperationException("The key of the item being added to the cache must be the same as the key being requested.");
      }

      // Write to the cache
      this.EnterWriteLock(region);

      try
      {
        // If an item with the desired key has been added while we were waiting
        // on the lock, return it.
        if (this.InnerCache.Contains(key))
        {
          this.Statistics.Hits++;
          var result = this.InnerCache[key];
          Contract.Assume(result != null);
          return (T)result;
        }

        this.Statistics.Misses++;
        this.Statistics.Writes++;
        this.InnerCache.Add(key, value, new CacheItemPolicy { Priority = permanent ? CacheItemPriority.NotRemovable : CacheItemPriority.Default });
        return value;
      }
      finally
      {
        this.ExitWriteLock(region);
      }
    }

    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// <param name="value">
    /// The value which will be added and returned if a matching item cannot
    /// be found in the cache.
    /// </param>
    /// <returns>
    /// The cached item with the same key as <paramref name="value" />, if
    /// such an item exists.  Otherwise, <paramref name="value" /> will be
    /// added to the cache and then returned.
    /// </returns>
    public T GetOrAdd<T>(T value) where T : IEveCacheable
    {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);
      Contract.Ensures(Contract.Result<T>() != null);

      return this.GetOrAdd(value, false);
    }

    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// <param name="value">
    /// The value which will be added and returned if a matching item cannot
    /// be found in the cache.
    /// </param>
    /// <param name="permanent">
    /// Specifies whether to add the value permanently or whether it can be
    /// automatically evicted.
    /// </param>
    /// <returns>
    /// The cached item with the same key as <paramref name="value" />, if
    /// such an item exists.  Otherwise, <paramref name="value" /> will be
    /// added to the cache and then returned.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Items added with <paramref name="permanent" /> equal to
    /// <see langword="true" /> are immune to automatic eviction, but can
    /// still be removed or overwritten manually.
    /// </para>
    /// </remarks>
    public T GetOrAdd<T>(T value, bool permanent) where T : IEveCacheable
    {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);
      Contract.Ensures(Contract.Result<T>() != null);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, value.CacheKey);

      this.EnterReadLock(region);

      try
      {
        // If the cache contains the desired key, return it
        if (this.InnerCache.Contains(key))
        {
          this.Statistics.Hits++;
          var result = this.InnerCache[key];
          Contract.Assume(result != null);
          return (T)result;
        }
      }
      finally
      {
        this.ExitReadLock(region);
      }

      // Otherwise, write to the cache
      this.EnterWriteLock(region);

      try
      {
        // If an item with the desired key has been added while we were waiting
        // on the lock, return it.
        if (this.InnerCache.Contains(key))
        {
          this.Statistics.Hits++;
          var result = this.InnerCache[key];
          Contract.Assume(result != null);
          return (T)result;
        }

        // Otherwise, add it to the cache
        this.Statistics.Misses++;
        this.Statistics.Writes++;
        this.InnerCache.Add(key, value, new CacheItemPolicy { Priority = permanent ? CacheItemPriority.NotRemovable : CacheItemPriority.Default });
        return value;
      }
      finally
      {
        this.ExitWriteLock(region);
      }
    }

    /// <summary>
    /// Removes the item with the specified key.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the item to remove.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to remove.
    /// </param>
    /// <returns>
    /// The removed item, or the default value if no matching item was found.
    /// </returns>
    public T Remove<T>(object id)
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      this.EnterWriteLock(region);

      try
      {
        if (!this.InnerCache.Contains(key))
        {
          return default(T);
        }

        var result = this.InnerCache.Remove(key);
        Contract.Assume(result != null);
        return (T)result;
      }
      finally
      {
        this.ExitWriteLock(region);
      }
    }

    /// <summary>
    /// Attempts to retrieve an item with the specified ID, returning success or
    /// failure.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the item to retrieve.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to retrieve.
    /// </param>
    /// <param name="value">
    /// Output parameter.  With contain the retrieved value if a match is found,
    /// or the default value otherwise.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item was successfully retrieved;
    /// otherwise <see langword="false" />.
    /// </returns>
    public bool TryGetValue<T>(object id, out T value)
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      this.EnterReadLock(region);

      try
      {
        if (this.InnerCache.Contains(key))
        {
          this.Statistics.Hits++;
          var result = this.InnerCache.Get(key);
          Contract.Assume(result != null);
          value = (T)result;
          return true;
        }
      }
      finally
      {
        this.ExitReadLock(region);
      }

      this.Statistics.Misses++;
      value = default(T);
      return false;
    }

    /// <summary>
    /// Begins a read lock for the specified cache region.
    /// </summary>
    /// <param name="region">
    /// The cache region to lock.
    /// </param>
    protected internal void EnterReadLock(string region)
    {
      this.MasterLock.EnterReadLock();

      if (region != null)
      {
        ReaderWriterLockSlim regionLock = this.RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        Contract.Assume(regionLock != null);
        regionLock.EnterReadLock();
      }
    }

    /// <summary>
    /// Begins a read lock for the specified cache region.
    /// </summary>
    /// <param name="region">
    /// The cache region to lock, or <see langword="null" /> to lock the entire
    /// cache.
    /// </param>
    protected internal void EnterWriteLock(string region)
    {
      // If blocking the entire cache, lock the master lock
      if (region == null)
      {
        this.MasterLock.EnterWriteLock();

      // Otherwise lock the region lock
      }
      else
      {
        ReaderWriterLockSlim regionLock = this.RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        Contract.Assume(regionLock != null);
        regionLock.EnterWriteLock();

        // Put a read lock on the master lock so that master writes can't take place
        // while the region is locked
        this.MasterLock.EnterReadLock();
      }
    }

    /// <summary>
    /// Ends a read lock for the specified cache region.
    /// </summary>
    /// <param name="region">
    /// The cache region to lock.
    /// </param>
    protected internal void ExitReadLock(string region)
    {
      this.MasterLock.ExitReadLock();

      if (region != null)
      {
        ReaderWriterLockSlim regionLock = this.RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        Contract.Assume(regionLock != null);
        regionLock.ExitReadLock();
      }
    }

    /// <summary>
    /// Ends a write lock for the specified cache region.
    /// </summary>
    /// <param name="region">
    /// The cache region to lock, or <see langword="null" /> to lock the entire
    /// cache.
    /// </param>
    protected internal void ExitWriteLock(string region)
    {
      // If blocking the entire cache, unlock the master lock
      if (region == null)
      {
        this.MasterLock.ExitWriteLock();

      // Otherwise unlock the region lock
      }
      else
      {
        ReaderWriterLockSlim regionLock = this.RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        Contract.Assume(regionLock != null);
        regionLock.ExitWriteLock();

        // We put a read lock on the master lock so that master writes can't take place
        // while the region is locked
        this.MasterLock.ExitReadLock();
      }
    }

    /// <summary>
    /// Creates a new cache with the specified size.
    /// </summary>
    /// <param name="cacheSize">
    /// The size of the cache, in megabytes.
    /// </param>
    /// <returns>
    /// A new <see cref="MemoryCache" /> with the specified size.
    /// </returns>
    protected static MemoryCache CreateCache(int cacheSize)
    {
      Contract.Ensures(Contract.Result<MemoryCache>() != null);

      var config = new NameValueCollection();
      config.Add("pollingInterval", "00:05:00");
      config.Add("physicalMemoryLimitPercentage", "0");
      config.Add("cacheMemoryLimitMegabytes", cacheSize.ToString());

      string name = "EveCache";
      Contract.Assume(!string.Equals(name, "default", StringComparison.OrdinalIgnoreCase));
      return new MemoryCache("EveCache", config);
    }

    /// <summary>
    /// Gets the cache key for an item with the specified type and ID value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item.
    /// </typeparam>
    /// <param name="id">
    /// The ID for which to generate a cache key.
    /// </param>
    /// <returns>
    /// The cache key for an item with the specified type and ID.
    /// </returns>
    protected static string GetCacheKey<T>(object id)
    {
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);
      Contract.Ensures(Contract.Result<string>() != null);

      return GetCacheKey(RegionMap.GetRegion(typeof(T)), id);
    }

    /// <summary>
    /// Gets the cache key for an item with the specified type, region, and ID
    /// value.
    /// </summary>
    /// <param name="region">
    /// The region for which to generate the key.
    /// </param>
    /// <param name="id">
    /// The ID for which to generate a cache key.
    /// </param>
    /// <returns>
    /// The cache key for an item with the specified type and ID.
    /// </returns>
    protected static string GetCacheKey(string region, object id)
    {
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);
      Contract.Requires(region != null, Resources.Messages.BaseValueCache_RegionCannotBeNull);
      Contract.Ensures(Contract.Result<string>() != null);

      Type idType = id.GetType();

      return EveCacheRegionPrefix + region + (idType.IsEnum ? Enum.Format(idType, id, "d") : id.ToString());
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    /// <param name="disposing">Specifies whether to dispose managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.masterLock.Dispose();
      }
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.innerCache != null);
      Contract.Invariant(this.masterLock != null);
      Contract.Invariant(this.regionLocks != null);
      Contract.Invariant(this.statistics != null);
    }
  }

  #region IEnumerable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable" /> interface.
  /// </content>
  public partial class EveCache : IEnumerable
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Standard practice for read-only collections.")]
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }
  }
  #endregion
}