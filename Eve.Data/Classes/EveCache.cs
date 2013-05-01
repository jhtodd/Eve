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

  /// <summary>
  /// A central cache for storing commonly used EVE values.
  /// </summary>
  public partial class EveCache : IDisposable
  {
    /// <summary>
    /// The prefix used to simulate a cache region for EVE-related items if using a shared cache
    /// (such as the default MemoryCache).
    /// </summary>
    protected internal const string EveCacheRegionPrefix = "E_";

    private static readonly CacheItemPolicy DefaultPolicy = new CacheItemPolicy() { Priority = CacheItemPriority.Default };
    private static readonly DomainMap RegionMapInstance = new DomainMap();

    private readonly ObjectCache innerCache;
    private readonly ReferenceCollection innerReferenceCollection;
    private readonly ReaderWriterLockSlim masterLock;
    private readonly ConcurrentDictionary<string, ReaderWriterLockSlim> regionLocks;
    private readonly EveCacheStatistics statistics;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveCache class that uses the default
    /// <see cref="MemoryCache" /> to store data.
    /// </summary>
    public EveCache() : this(General.Settings.CacheSize, General.Settings.CachePollingInterval)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EveCache class that uses a custom memory
    /// cache with the specified size to store data.
    /// </summary>
    /// <param name="cacheSize">
    /// The maximum size of the cache in megabytes, or 0 to use the default
    /// maximum size.
    /// </param>
    /// <param name="pollingInterval">
    /// How often to check to see if the cache has reached its maximum
    /// capacity, or <see cref="TimeSpan.Zero" /> to use the default
    /// polling interval.
    /// </param>
    public EveCache(int cacheSize, TimeSpan pollingInterval) : this(CreateCache(cacheSize, pollingInterval))
    {
      Contract.Requires(cacheSize >= 0, "The cache size must be a positive number, or 0 to use the default size.");
      Contract.Requires(pollingInterval >= TimeSpan.Zero, "The polling interval must be a positive value, or TimeSpan.Zero to use the default interval.");
      Contract.Requires(pollingInterval < TimeSpan.FromDays(1.0D), "The polling interval must be less than one day.");
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
      this.innerReferenceCollection = new ReferenceCollection();

      // The master lock needs recursive read locks so that region locks don't
      // unnecessarily block.
      this.masterLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

      this.regionLocks = new ConcurrentDictionary<string, ReaderWriterLockSlim>();
      this.statistics = new EveCacheStatistics();
    }

    /* Properties */

    /// <summary>
    /// Gets statistics about cache usage.
    /// </summary>
    /// <value>
    /// A <see cref="EveCacheStatistics" /> object containing statistics about how
    /// the cache is being used.
    /// </value>
    public EveCacheStatistics Statistics
    {
      get
      {
        Contract.Ensures(Contract.Result<EveCacheStatistics>() != null);
        return this.statistics;
      }
    }

    /// <summary>
    /// Gets the type map used to define cache regions for different types of
    /// cacheable objects.
    /// </summary>
    /// <value>
    /// The <see cref="DomainMap" /> used to define cache regions for different
    /// types of cacheable objects.
    /// </value>
    internal static DomainMap RegionMap
    {
      get
      {
        Contract.Ensures(Contract.Result<DomainMap>() != null);
        return RegionMapInstance;
      }
    }

    /// <summary>
    /// Gets the inner cache object used to store data.
    /// </summary>
    /// <value>
    /// The <see cref="ObjectCache" /> used to store data.
    /// </value>
    internal ObjectCache InnerCache
    {
      get
      {
        Contract.Ensures(Contract.Result<ObjectCache>() != null);
        return this.innerCache;
      }
    }

    /// <summary>
    /// Gets the inner collection used to track external references to cached objects.
    /// </summary>
    /// <value>
    /// The <see cref="ReferenceCollection" /> used to store information about
    /// references to cached objects.
    /// </value>
    internal ReferenceCollection InnerReferenceCollection
    {
      get
      {
        Contract.Ensures(Contract.Result<ReferenceCollection>() != null);
        return this.innerReferenceCollection;
      }
    }

    /// <summary>
    /// Gets the lock used to restrict concurrent access to the cache.
    /// </summary>
    /// <value>
    /// A <see cref="ReaderWriterLockSlim" /> used to restrict concurrent access
    /// to the cache.
    /// </value>
    internal ReaderWriterLockSlim MasterLock
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
    internal ConcurrentDictionary<string, ReaderWriterLockSlim> RegionLocks
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
      string key = EveCache.CreateCacheKey(region, value.CacheKey);

      this.EnterWriteLock(region);

      try
      {
        this.InnerSet(key, value, permanent);
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
        this.InnerClear();
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
    public bool Contains<T>(IConvertible id)
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.CreateCacheKey(region, id);

      this.EnterReadLock(region);

      try
      {
        return this.InnerContains(key);
      }
      finally
      {
        this.ExitReadLock(region);
      }
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
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
    public T GetOrAdd<T>(IConvertible id, Func<T> valueFactory) where T : IEveCacheable
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);
      Contract.Requires(valueFactory != null, Resources.Messages.EveCache_ValueFactoryCannotBeNull);
      Contract.Ensures(Contract.Result<T>() != null);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.CreateCacheKey(region, id);

      this.EnterReadLock(region);

      try
      {
        object result;

        if (this.InnerTryGetValue(key, out result))
        {
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
      string verifyKey = EveCache.CreateCacheKey(region, value.CacheKey);

      if (!object.Equals(key, verifyKey))
      {
        throw new InvalidOperationException("The key of the item being added to the cache must be the same as the key being requested.");
      }

      // Write to the cache
      this.EnterWriteLock(region);

      try
      {
        object result;

        if (this.InnerTryGetValue(key, out result))
        {
          return (T)result;
        }

        this.Statistics.Misses++;
        this.InnerSet(key, value, false);
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

      string region = RegionMap.GetRegion(value.GetType());
      string key = EveCache.CreateCacheKey(region, value.CacheKey);

      this.EnterReadLock(region);

      try
      {
        object result;

        if (this.InnerTryGetValue(key, out result))
        {
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
        object result;

        if (this.InnerTryGetValue(key, out result))
        {
          return (T)result;
        }

        this.Statistics.Misses++;
        this.InnerSet(key, value, false);
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
    public T Remove<T>(IConvertible id)
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.CreateCacheKey(region, id);

      this.EnterWriteLock(region);

      try
      {
        var result = this.InnerRemove(key);
        return (result == null) ? default(T) : (T)result;
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
    public bool TryGetValue<T>(IConvertible id, out T value)
    {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn<T>(out value) != null);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.CreateCacheKey(region, id);

      this.EnterReadLock(region);

      try
      {
        object result;

        if (this.InnerTryGetValue(key, out result))
        {
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
    /// Converts an array of bytes to a short string value.
    /// </summary>
    /// <param name="data">
    /// The byte array for which to return a string.
    /// </param>
    /// <returns>
    /// A short string generated from the byte array.
    /// </returns>
    internal static string ByteArrayToShortString(byte[] data)
    {
      Contract.Requires(data != null, "The data to encode cannot be null.");

      if (data.Length == 0)
      {
        return string.Empty;
      }

      // Skip past leading zeroes
      int offset = 0;
      for (offset = 0; offset < data.Length - 1; offset++)
      {
        if (data[offset] != 0)
        {
          break;
        }
      }

      int length = data.Length - offset;

      // Format as a hex string -- bitwise version by CodesInChaos
      // http://stackoverflow.com/a/14333437/627282
      char[] chars = new char[length * 2];
      int byteValue;

      for (int i = 0; i < length; i++)
      {
        byteValue = data[offset + i] >> 4;
        chars[i * 2] = (char)(55 + byteValue + (((byteValue - 10) >> 31) & -7));
        byteValue = data[offset + i] & 0xF;
        chars[(i * 2) + 1] = (char)(55 + byteValue + (((byteValue - 10) >> 31) & -7));
      }

      // Remove the leading zero, if any
      return chars[0] == '0' ? new string(chars, 1, chars.Length - 1) : new string(chars);
    }

    /// <summary>
    /// Creates a new cache with the specified settings.
    /// </summary>
    /// <param name="cacheSize">
    /// The maximum size of the cache in megabytes, or 0 to use the default
    /// maximum size.
    /// </param>
    /// <param name="pollingInterval">
    /// How often to check to see if the cache has reached its maximum
    /// capacity, or <see cref="TimeSpan.Zero" /> to use the default
    /// polling interval.
    /// </param>
    /// <returns>
    /// A new <see cref="MemoryCache" /> with the specified settings.
    /// </returns>
    internal static MemoryCache CreateCache(int cacheSize, TimeSpan pollingInterval)
    {
      Contract.Requires(cacheSize >= 0, "The cache size must be a positive number, or 0 to use the default size.");
      Contract.Requires(pollingInterval >= TimeSpan.Zero, "The polling interval cannot be less than zero.");
      Contract.Requires(pollingInterval < TimeSpan.FromDays(1.0D), "The polling interval must be less than one day.");
      Contract.Ensures(Contract.Result<MemoryCache>() != null);

      var config = new NameValueCollection();

      if (cacheSize > 0)
      {
        config.Add("cacheMemoryLimitMegabytes", cacheSize.ToString());
      }

      if (pollingInterval > TimeSpan.Zero)
      {
        config.Add("pollingInterval", pollingInterval.ToString("hh\\:mm\\:ss"));
      }

      string name = "EveCache";
      Contract.Assume(!string.Equals(name, "default", StringComparison.OrdinalIgnoreCase));
      return new MemoryCache(name, config);
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
    protected static string CreateCacheKey(string region, IConvertible id)
    {
      Contract.Requires(region != null, Resources.Messages.BaseValueCache_RegionCannotBeNull);
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);
      Contract.Ensures(Contract.Result<string>() != null);

      string idKey;
      byte[] bytes;

      switch (id.GetTypeCode())
      {
        case TypeCode.Boolean:
          idKey = id.ToBoolean(null) ? "1" : "0";
          break;

        case TypeCode.Byte:
          idKey = ByteArrayToShortString(new byte[] { id.ToByte(null) });
          break;

        case TypeCode.Double:
          idKey = ByteArrayToShortString(BitConverter.GetBytes(id.ToDouble(null)));
          break;

        case TypeCode.Int16:
          bytes = BitConverter.GetBytes(id.ToInt16(null));

          if (BitConverter.IsLittleEndian)
          {
            Array.Reverse(bytes);
          }

          idKey = ByteArrayToShortString(bytes);
          break;

        case TypeCode.Int32:
          bytes = BitConverter.GetBytes(id.ToInt32(null));

          if (BitConverter.IsLittleEndian)
          {
            Array.Reverse(bytes);
          }

          idKey = ByteArrayToShortString(bytes);
          break;

        case TypeCode.Int64:
          bytes = BitConverter.GetBytes(id.ToInt64(null));

          if (BitConverter.IsLittleEndian)
          {
            Array.Reverse(bytes);
          }

          idKey = ByteArrayToShortString(bytes);
          break;

        case TypeCode.SByte:
          unchecked
          {
            idKey = ByteArrayToShortString(new byte[] { (byte)id.ToSByte(null) });
          }

          break;

        case TypeCode.Single:
          idKey = ByteArrayToShortString(BitConverter.GetBytes(id.ToSingle(null)));
          break;

        case TypeCode.UInt16:
          bytes = BitConverter.GetBytes(id.ToUInt16(null));

          if (BitConverter.IsLittleEndian)
          {
            Array.Reverse(bytes);
          }

          idKey = ByteArrayToShortString(bytes);
          break;

        case TypeCode.UInt32:
          bytes = BitConverter.GetBytes(id.ToUInt32(null));

          if (BitConverter.IsLittleEndian)
          {
            Array.Reverse(bytes);
          }

          idKey = ByteArrayToShortString(bytes);
          break;

        case TypeCode.UInt64:
          bytes = BitConverter.GetBytes(id.ToUInt64(null));

          if (BitConverter.IsLittleEndian)
          {
            Array.Reverse(bytes);
          }

          idKey = ByteArrayToShortString(bytes);
          break;

        default:
          idKey = id.ToString();
          break;
      }

      return EveCacheRegionPrefix + region + "_" + idKey;
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
        this.statistics.Dispose();
      }
    }

    /// <summary>
    /// Begins a read lock for the specified cache region.
    /// </summary>
    /// <param name="region">
    /// The cache region to lock.
    /// </param>
    protected void EnterReadLock(string region)
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
    protected void EnterWriteLock(string region)
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
    protected void ExitReadLock(string region)
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
    protected void ExitWriteLock(string region)
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
    /// Performs the low-level actions necessary to clear the 
    /// contents of the cache.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This method performs no locking and assumes that thread safety
    /// concerns have been addressed by the calling method.
    /// </para>
    /// </remarks>
    protected void InnerClear()
    {
      // First, clear all inactive references
      this.InnerReferenceCollection.Clear();

      // Construct a list of all keys to remove from the cache
      var keysToRemove = this.InnerCache.Where(x => x.Key.StartsWith(EveCacheRegionPrefix)).Select(x => x.Key).ToArray();

      foreach (string key in keysToRemove)
      {
        this.InnerCache.Remove(key);
      }
    }

    /// <summary>
    /// Performs the low-level actions necessary to determine if the
    /// cache contains an item with the specified key.
    /// </summary>
    /// <param name="key">
    /// The key to locate in the cache.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if an item with the specified key is
    /// contained in the collection; otherwise <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method performs no locking and assumes that thread safety
    /// concerns have been addressed by the calling method.
    /// </para>
    /// </remarks>
    protected bool InnerContains(string key)
    {
      Contract.Requires(key != null);
      return this.InnerReferenceCollection.Contains(key) || this.InnerCache.Contains(key);
    }

    /// <summary>
    /// Performs the low-level actions necessary to remove an item from
    /// the cache.
    /// </summary>
    /// <param name="key">
    /// The key of the item to remove.
    /// </param>
    /// <returns>
    /// The item that was removed, or <see langword="null" /> if no
    /// item with the specified key was found.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method performs no locking and assumes that thread safety
    /// concerns have been addressed by the calling method.
    /// </para>
    /// </remarks>
    protected object InnerRemove(string key)
    {
      Contract.Requires(key != null);

      // First, check to see if there's an active reference to the
      // specified value.
      object result = this.InnerReferenceCollection.Remove(key);

      // If there is, remove it from the cache and return it.
      if (result != null)
      {
        this.InnerCache.Remove(key);
        return result;
      }

      // Otherwise, check the cache and remove it there.
      return this.InnerCache.Remove(key);
    }

    /// <summary>
    /// Performs the low-level actions necessary to set an item in the
    /// cache.
    /// </summary>
    /// <param name="key">
    /// The key of the item to set.
    /// </param>
    /// <param name="value">
    /// The value to set.
    /// </param>
    /// <param name="permanent">
    /// Specifies whether the item should be added permanently.
    /// </param>
    /// <remarks>
    /// <para>
    /// This method performs no locking and assumes that thread safety
    /// concerns have been addressed by the calling method.
    /// </para>
    /// </remarks>
    protected void InnerSet(string key, object value, bool permanent)
    {
      this.Statistics.Writes++;

      this.InnerReferenceCollection.Set(key, value, permanent);

      // If the item was already added as a permanent reference, there's
      // no need to add it to the cache as well.
      if (!permanent)
      {
        CacheItem cacheItem = new CacheItem(key, value);
        this.InnerCache.Add(cacheItem, DefaultPolicy);
      }
    }

    /// <summary>
    /// Performs the low-level actions necessary to retrieve an item
    /// from the cache.
    /// </summary>
    /// <param name="key">
    /// The key of the item to remove.
    /// </param>
    /// <param name="value">
    /// The object which will hold the retrieved value.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if an item with the specified key was found 
    /// and returned; otherwise <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method performs no locking and assumes that thread safety
    /// concerns have been addressed by the calling method.
    /// </para>
    /// </remarks>
    protected bool InnerTryGetValue(string key, out object value)
    {
      Contract.Requires(key != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);

      // If there's an active reference to the desired item, return it.
      if (this.InnerReferenceCollection.TryGetValue(key, out value))
      {
        this.Statistics.ReferenceHits++;
        return true;
      }

      // Otherwise, look in the cache.
      if (this.InnerCache.Contains(key))
      {
        this.Statistics.CacheHits++;
        value = this.InnerCache[key];
        return true;
      }

      return false;
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.innerCache != null);
      Contract.Invariant(this.innerReferenceCollection != null);
      Contract.Invariant(this.masterLock != null);
      Contract.Invariant(this.regionLocks != null);
      Contract.Invariant(this.statistics != null);
    }
  }
}