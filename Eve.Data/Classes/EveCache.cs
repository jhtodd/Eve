//-----------------------------------------------------------------------
// <copyright file="EveCache.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data {
  using System;
  using System.Collections;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Runtime.Caching;
  using System.Threading;

  using FreeNet;

  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// A central cache for storing commonly used EVE values.
  /// </summary>
  public class EveCache : IEnumerable<KeyValuePair<string, object>> {

    #region Classes
    //******************************************************************************
    /// <summary>
    /// Defines a mapping between cacheable types and the types that define the
    /// domain of the ID values.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Some cacheable types don't define a domain for their ID values directly,
    /// but rely on the domain defined by a parent type.  For example, ID values
    /// for <see cref="BlueprintType" /> aren't just unique among blueprints,
    /// but among all items.  So, we define a mapping between
    /// <see cref="BlueprintType" /> and <see cref="ItemType" /> to let the
    /// cache know that <see cref="ItemType" /> actually defines the ID domain
    /// for blueprints.
    /// </para>
    /// </remarks>
    public class CacheRegionMap {

      #region Instance Fields
      private readonly Dictionary<Type, string> _innerRegionMap;
      private readonly ReaderWriterLockSlim _lock;
      #endregion

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the CacheRegionMap class.
      /// </summary>
      public CacheRegionMap() {
        _lock = new ReaderWriterLockSlim();
        _innerRegionMap = new Dictionary<Type, string>();
      }
      //******************************************************************************
      /// <summary>
      /// Establishes object invariants of the class.
      /// </summary>
      [ContractInvariantMethod]
      private void ObjectInvariant() {
        Contract.Invariant(_innerRegionMap != null);
        Contract.Invariant(_lock != null);
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Registers a type and associates it with a cache region.
      /// </summary>
      /// 
      /// <param name="type">
      /// The type to register.
      /// </param>
      /// 
      /// <param name="region">
      /// The region associated with the type.
      /// </param>
      /// 
      /// <remarks>
      /// <para>
      /// Registering a type that already exists in the map will overwrite
      /// the previous value.
      /// </para>
      /// </remarks>
      public void RegisterType(Type type, string region) {
        Contract.Requires(type != null, "The type cannot be null.");
        Contract.Requires(region != null, "The cache region cannot be null.");

        Lock.EnterWriteLock();

        try {
          InnerRegionMap[type] = region;

        } finally {
          Lock.ExitWriteLock();
        }
      }
      //******************************************************************************
      /// <summary>
      /// Returns the cache region associated with the specified type.
      /// </summary>
      /// 
      /// <param name="type">
      /// The type whose cache region to return.
      /// </param>
      /// 
      /// <returns>
      /// The cache region for the specified type, or the cache region for the first
      /// type up the inheritance chain if no region has been explicitly defined for
      /// the specified type.
      /// </returns>
      /// 
      /// <exception cref="InvalidOperationException">
      /// Thrown if no region has been registered for <paramref name="type" /> or
      /// any of its base types.
      /// </exception>
      public string GetRegion(Type type) {
        Contract.Requires(type != null, "The type cannot be null.");

        Type baseType;
        string region = null;

        Lock.EnterReadLock();

        try {

          // First check to see if the current type is already assigned to a region
          if (InnerRegionMap.TryGetValue(type, out region)) {
            return region;
          }

          // Then check types higher up the inheritance chain
          baseType = type.BaseType;

          while (baseType != null) {

            // If we find a base type in the mapping, use it (and store the subtype
            // for faster subsequent lookup).
            if (InnerRegionMap.TryGetValue(baseType, out region)) {
              break;
            }

            baseType = baseType.BaseType;
          }

        } finally {
          Lock.ExitReadLock();
        }

        // If we found a region in one of the base types, register the current
        // type with that region for faster future access, and return.
        if (region != null) {
          RegisterType(type, region);
          return region;
        }

        throw new InvalidOperationException("No cache region has been defined for " + type.FullName + ".");
      }
      //******************************************************************************
      /// <summary>
      /// Returns a value indicating whether the specified type is registered for
      /// caching.
      /// </summary>
      /// 
      /// <param name="type">
      /// The type who registration to determine.
      /// </param>
      /// 
      /// <returns>
      /// <see langword="true" /> if <paramref name="type" /> or one of its base
      /// classes is registered for caching; otherwise <see langword="false" />.
      /// </returns>
      public bool IsRegistered(Type type) {
        Contract.Requires(type != null, "The type cannot be null.");

        Lock.EnterReadLock();

        try {

          // First check to see if the current type is already assigned to a region
          if (InnerRegionMap.ContainsKey(type)) {
            return true;
          }

          // Then check types higher up the inheritance chain
          type = type.BaseType;

          while (type != null) {

            // If we find a base type in the mapping, use it (and store the subtype
            // for faster subsequent lookup).
            if (InnerRegionMap.ContainsKey(type)) {
              return true;
            }

            type = type.BaseType;
          }

        } finally {
          Lock.ExitReadLock();
        }

        return false;
      }
      #endregion
      #region Protected Internal Properties
      //******************************************************************************
      /// <summary>
      /// Gets the mapping of types to cache regions.
      /// </summary>
      /// 
      /// <value>
      /// A <see cref="Dictionary{TKey, TValue}" /> containing the mapping from
      /// types to cache regions.
      /// </value>
      protected internal Dictionary<Type, string> InnerRegionMap {
        get {
          Contract.Ensures(Contract.Result<Dictionary<Type, int>>() != null);
          return _innerRegionMap;
        }
      }
      //******************************************************************************
      /// <summary>
      /// Gets the lock used to synchronize the map.
      /// </summary>
      /// 
      /// <value>
      /// The <see cref="ReaderWriterLockSlim" /> used to synchronize the map.
      /// </value>
      protected internal ReaderWriterLockSlim Lock {
        get {
          Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
          return _lock;
        }
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// Contains statistics about cache usage.
    /// </summary>
    public class CacheStatistics {

      #region Instance Fields
      private long _hits;
      private long _misses;
      private long _writes;
      #endregion

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the CacheStatistics class.
      /// </summary>
      public CacheStatistics() {
        _hits = 0L;
        _misses = 0L;
        _writes = 0L;
      }
      //******************************************************************************
      /// <summary>
      /// Establishes object invariants of the class.
      /// </summary>
      [ContractInvariantMethod]
      private void ObjectInvariant() {
        Contract.Invariant(_hits >= 0L);
        Contract.Invariant(_misses >= 0L);
        Contract.Invariant(_writes >= 0L);
      }
      #endregion
      #region Public Properties
      //******************************************************************************
      /// <summary>
      /// Gets the number of requests in which a result was successfully found and
      /// returned.
      /// </summary>
      /// 
      /// <value>
      /// The number of cache hits.
      /// </value>
      public long Hits {
        get {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return _hits;
        }
        internal set {
          Contract.Requires(value >= 0L, "The number of cache hits cannot be less than zero.");
          _hits = value;
        }
      }
      //******************************************************************************
      /// <summary>
      /// Gets the number of requests in which no result was found.
      /// </summary>
      /// 
      /// <value>
      /// The number of cache misses.
      /// </value>
      public long Misses {
        get {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return _misses;
        }
        internal set {
          Contract.Requires(value >= 0L, "The number of cache misses cannot be less than zero.");
          _misses = value;
        }
      }
      //******************************************************************************
      /// <summary>
      /// Gets the total number of requests made to the cache.
      /// </summary>
      /// 
      /// <value>
      /// The total number of requests (both hits and misses).
      /// </value>
      public long TotalRequests {
        get {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return Hits + Misses;
        }
      }
      //******************************************************************************
      /// <summary>
      /// Gets the total number of items written to the cache.
      /// </summary>
      /// 
      /// <value>
      /// The number of items written to the cache.
      /// </value>
      public long Writes {
        get {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return _writes;
        }
        internal set {
          Contract.Requires(value >= 0L, "The number of writes cannot be less than zero.");
          _writes = value;
        }
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Resets all statistics to zero.
      /// </summary>
      public void Reset() {
        Hits = 0L;
        Misses = 0L;
        Writes = 0L;
      }
      //******************************************************************************
      /// <inheritdoc />
      public override string ToString() {
        return "Writes: " + Writes.ToString() + 
               ", Hits: " + Hits.ToString() + 
               ", Misses: " + Misses.ToString() + 
               ", Total Requests: " + TotalRequests.ToString();
      }
      #endregion
    }
    #endregion

    #region Constants
    protected internal const string PREFIX = "E_";
    #endregion

    #region Static Fields
    private static readonly CacheRegionMap _regionMap;
    #endregion

    #region Static Constructor
    //******************************************************************************
    /// <summary>
    /// Initializes static members of the EveCache class.
    /// </summary>
    static EveCache() {
      _regionMap = InitializeRegionMap();
    }
    #endregion
    #region Static Properties
    //******************************************************************************
    /// <summary>
    /// Gets the type map used to define cache regions for different types of
    /// cacheable objects.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="CacheRegionMap" /> used to define cache regions for different
    /// types of cacheable objects.
    /// </value>
    public static CacheRegionMap RegionMap {
      get {
        Contract.Ensures(Contract.Result<CacheRegionMap>() != null);
        return _regionMap;
      }
    }
    #endregion
    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Creates a new cache with the specified size.
    /// </summary>
    /// 
    /// <param name="cacheSize">
    /// The size of the cache, in megabytes.
    /// </param>
    /// 
    /// <returns>
    /// A new <see cref="MemoryCache" /> with the specified size.
    /// </returns>
    protected static MemoryCache CreateCache(int cacheSize) {
      Contract.Ensures(Contract.Result<MemoryCache>() != null);

      var config = new NameValueCollection();
      config.Add("pollingInterval", "00:05:00");
      config.Add("physicalMemoryLimitPercentage", "0");
      config.Add("cacheMemoryLimitMegabytes", cacheSize.ToString());

      string name = "EveCache";
      Contract.Assume(!string.Equals(name, "default", StringComparison.OrdinalIgnoreCase));
      return new MemoryCache("EveCache", config);
    }
    //******************************************************************************
    /// <summary>
    /// Gets the cache key for an item with the specified type and ID value.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID for which to generate a cache key.
    /// </param>
    /// 
    /// <returns>
    /// The cache key for an item with the specified type and ID.
    /// </returns>
    protected static string GetCacheKey<T>(object id) {
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);
      Contract.Ensures(Contract.Result<string>() != null);

      return GetCacheKey(RegionMap.GetRegion(typeof(T)), id);
    }
    //******************************************************************************
    /// <summary>
    /// Gets the cache key for an item with the specified type, region, and ID
    /// value.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item.
    /// </typeparam>
    /// 
    /// <param name="region">
    /// The region for which to generate the key.
    /// </param>
    /// 
    /// <param name="id">
    /// The ID for which to generate a cache key.
    /// </param>
    /// 
    /// <returns>
    /// The cache key for an item with the specified type and ID.
    /// </returns>
    protected static string GetCacheKey(string region, object id) {
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);
      Contract.Requires(region != null, Resources.Messages.BaseValueCache_RegionCannotBeNull);
      Contract.Ensures(Contract.Result<string>() != null);

      Type idType = id.GetType();

      return PREFIX + region + (idType.IsEnum ? Enum.Format(idType, id, "d") : id.ToString());
    }
    #endregion

    //******************************************************************************
    /// <summary>
    /// Initializes and returns the type map used to define cache regions for
    /// different types of objects.
    /// </summary>
    /// 
    /// <returns>
    /// The <see cref="CacheRegionMap" /> used to define cache regions.
    /// </returns>
    protected static CacheRegionMap InitializeRegionMap() {
      CacheRegionMap typeMap = new CacheRegionMap();

      // All cacheable EVE types must be registered here
      typeMap.RegisterType(typeof(AttributeCategory), typeof(AttributeCategory).Name);
      typeMap.RegisterType(typeof(AttributeType), typeof(AttributeType).Name);
      typeMap.RegisterType(typeof(AttributeValue), typeof(AttributeValue).Name);
      typeMap.RegisterType(typeof(AgentType), typeof(AgentType).Name);
      typeMap.RegisterType(typeof(Category), typeof(Category).Name);
      typeMap.RegisterType(typeof(Group), typeof(Group).Name);
      typeMap.RegisterType(typeof(Icon), typeof(Icon).Name);
      typeMap.RegisterType(typeof(ItemType), typeof(ItemType).Name);
      typeMap.RegisterType(typeof(MarketGroup), typeof(MarketGroup).Name);
      typeMap.RegisterType(typeof(Race), typeof(Race).Name);
      typeMap.RegisterType(typeof(Unit), typeof(Unit).Name);

      return typeMap;
    }

    #region Instance Fields
    private ObjectCache _innerCache;
    private ReaderWriterLockSlim _masterLock;
    private ConcurrentDictionary<string, ReaderWriterLockSlim> _regionLocks;
    private CacheStatistics _statistics;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveCache class that uses the default
    /// <see cref="MemoryCache" /> to store data.
    /// </summary>
    public EveCache() : this(MemoryCache.Default) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveCache class that uses a custom memory
    /// cache with the specified size to store data.
    /// </summary>
    /// 
    /// <param name="cacheSize">
    /// The size of the cache, in megabytes.
    /// </param>
    public EveCache(int cacheSize) : this(CreateCache(cacheSize)) {
      Contract.Requires(cacheSize > 0, Resources.Messages.EveCache_CacheSizeCannotBeNegative);
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveCache class that uses the specified
    /// <see cref="ObjectCache" /> to store data.
    /// </summary>
    /// 
    /// <param name="cache">
    /// The <see cref="ObjectCache" /> used to store items.
    /// </param>
    public EveCache(ObjectCache cache) : base() {
      Contract.Requires(cache != null, Resources.Messages.EveCache_CacheCannotBeNull);

      _innerCache = cache;

      // The master lock needs recursive read locks so that region locks don't
      // unnecessarily block.
      _masterLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

      _regionLocks = new ConcurrentDictionary<string, ReaderWriterLockSlim>();
      _statistics = new CacheStatistics();
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_innerCache != null);
      Contract.Invariant(_masterLock != null);
      Contract.Invariant(_regionLocks != null);
      Contract.Invariant(_statistics != null);
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Add the item to the cache, replacing any existing item with the same ID.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item to add to the cache.
    /// </typeparam>
    /// 
    /// <param name="value">
    /// The item to add to the cache.
    /// </param>
    public void AddOrReplace<T>(T value) where T : IHasId {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);
      AddOrReplace(value, false);
    }
    //******************************************************************************
    /// <summary>
    /// Add the item to the cache, replacing any existing item with the same ID.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item to add to the cache.
    /// </typeparam>
    /// 
    /// <param name="value">
    /// The item to add to the cache.
    /// </param>
    /// 
    /// <param name="permanent">
    /// Specifies whether to add the value permanently or whether it can be
    /// automatically evicted.
    /// </param>
    /// 
    /// <remarks>
    /// <para>
    /// Items added with <paramref name="permanent" /> equal to
    /// <see langword="true" /> are immune to automatic eviction, but can
    /// still be removed or overwritten manually.
    /// </para>
    /// </remarks>
    public void AddOrReplace<T>(T value, bool permanent) where T : IHasId {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, value.Id);

      EnterWriteLock(region);

      try {
        Statistics.Writes++;
        InnerCache.Set(key, value, new CacheItemPolicy { Priority = (permanent ? CacheItemPriority.NotRemovable : CacheItemPriority.Default) });

      } finally {
        ExitWriteLock(region);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Removes all entries from the cache.
    /// </summary>
    public void Clear() {
      List<string> keysToRemove = new List<string>();

      EnterWriteLock(null);

      try {

        foreach (KeyValuePair<string, object> entry in InnerCache) {
          Contract.Assume(entry.Key != null);
          if (entry.Key.StartsWith(PREFIX)) {
            keysToRemove.Add(entry.Key);
          }
        }

        foreach (string key in keysToRemove) {
          InnerCache.Remove(key);
        }

      } finally {
        ExitWriteLock(null);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Returns a value indicating whether an item with the specified ID is
    /// contained in the cache.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of the item to locate.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID to locate in the cache.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if an item with the specified ID is contained
    /// in the cache; otherwise <see langword="false" />.
    /// </returns>
    public bool Contains<T>(object id) {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      EnterReadLock(region);

      try {
        return InnerCache.Contains(key);

      } finally {
        ExitReadLock(region);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the item to add or retrieve.
    /// </param>
    /// 
    /// <param name="valueFactory">
    /// The <see cref="Func{TOutput}" /> which will generate the value to be
    /// added if a matching item cannot be found in the cache.
    /// </param>
    /// 
    /// <returns>
    /// The item of the desired type and with the specified key, if a matching
    /// item is contained in the cache.  Otherwise, the result of executing
    /// <paramref name="valueFactory" />.
    /// </returns>
    public T GetOrAdd<T>(object id, Func<T> valueFactory) where T : IHasId {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);
      Contract.Requires(valueFactory != null, Resources.Messages.EveCache_ValueFactoryCannotBeNull);

      return GetOrAdd(id, valueFactory, false);
    }
    //******************************************************************************
    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the item to add or retrieve.
    /// </param>
    /// 
    /// <param name="valueFactory">
    /// The <see cref="Func{TOutput}" /> which will generate the value to be
    /// added if a matching item cannot be found in the cache.
    /// </param>
    /// 
    /// <param name="permanent">
    /// Specifies whether to add the value permanently or whether it can be
    /// automatically evicted.
    /// </param>
    /// 
    /// <returns>
    /// The item of the desired type and with the specified key, if a matching
    /// item is contained in the cache.  Otherwise, the result of executing
    /// <paramref name="valueFactory" />.
    /// </returns>
    /// 
    /// <remarks>
    /// <para>
    /// Items added with <paramref name="permanent" /> equal to
    /// <see langword="true" /> are immune to automatic eviction, but can
    /// still be removed or overwritten manually.
    /// </para>
    /// </remarks>
    public T GetOrAdd<T>(object id, Func<T> valueFactory, bool permanent) where T : IHasId {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);
      Contract.Requires(valueFactory != null, Resources.Messages.EveCache_ValueFactoryCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      EnterReadLock(region);

      try {

        // If the cache contains the desired key, return it
        if (InnerCache.Contains(key)) {
          Statistics.Hits++;
          var result = InnerCache[key];
          Contract.Assume(result != null);
          return (T) result;
        }

      } finally {
        ExitReadLock(region);
      }

      // Otherwise, write to the cache
      EnterWriteLock(region);

      try {

        // If an item with the desired key has been added while we were waiting
        // on the lock, return it.
        if (InnerCache.Contains(key)) {
          Statistics.Hits++;
          var result = InnerCache[key];
          Contract.Assume(result != null);
          return (T) result;
        }

        // Otherwise, add it to the cache
        T value = valueFactory();

        Statistics.Misses++;
        Statistics.Writes++;
        InnerCache.Add(key, value, new CacheItemPolicy { Priority = (permanent ? CacheItemPriority.NotRemovable : CacheItemPriority.Default) });
        return value;

      } finally {
        ExitWriteLock(region);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// 
    /// <param name="value">
    /// The value which will be added and returned if a matching item cannot
    /// be found in the cache.
    /// </param>
    /// 
    /// <returns>
    /// The item of the desired type and with the specified key, if a matching
    /// item is contained in the cache.  Otherwise, the result of executing
    /// <paramref name="valueFactory" />.
    /// </returns>
    public T GetOrAdd<T>(T value) where T : IHasId {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);
      return GetOrAdd(value, false);
    }
    //******************************************************************************
    /// <summary>
    /// Retrieves the item with the specified ID from the cache, or, if no
    /// matching item is present, adds the specified value to the cache and 
    /// returns it.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of item to add to or retrieve from the cache.
    /// </typeparam>
    /// 
    /// <param name="value">
    /// The value which will be added and returned if a matching item cannot
    /// be found in the cache.
    /// </param>
    /// 
    /// <param name="permanent">
    /// Specifies whether to add the value permanently or whether it can be
    /// automatically evicted.
    /// </param>
    /// 
    /// <returns>
    /// The item of the desired type and with the specified key, if a matching
    /// item is contained in the cache.  Otherwise, the result of executing
    /// <paramref name="valueFactory" />.
    /// </returns>
    /// 
    /// <remarks>
    /// <para>
    /// Items added with <paramref name="permanent" /> equal to
    /// <see langword="true" /> are immune to automatic eviction, but can
    /// still be removed or overwritten manually.
    /// </para>
    /// </remarks>
    public T GetOrAdd<T>(T value, bool permanent) where T : IHasId {
      Contract.Requires(value != null, Resources.Messages.EveCache_ValueCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, value.Id);

      EnterReadLock(region);

      try {

        // If the cache contains the desired key, return it
        if (InnerCache.Contains(key)) {
          Statistics.Hits++;
          var result = InnerCache[key];
          Contract.Assume(result != null);
          return (T) result;
        }

      } finally {
        ExitReadLock(region);
      }

      // Otherwise, write to the cache
      EnterWriteLock(region);

      try {

        // If an item with the desired key has been added while we were waiting
        // on the lock, return it.
        if (InnerCache.Contains(key)) {
          Statistics.Hits++;
          var result = InnerCache[key];
          Contract.Assume(result != null);
          return (T) result;
        }

        // Otherwise, add it to the cache
        Statistics.Misses++;
        Statistics.Writes++;
        InnerCache.Add(key, value, new CacheItemPolicy { Priority = (permanent ? CacheItemPriority.NotRemovable : CacheItemPriority.Default) });
        return value;

      } finally {
        ExitWriteLock(region);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Removes the item with the specified key.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of the item to remove.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the item to remove.
    /// </param>
    /// 
    /// <returns>
    /// The removed item, or the default value if no matching item was found.
    /// </returns>
    public T Remove<T>(object id) {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      EnterWriteLock(region);

      try {
        if (!InnerCache.Contains(key)) {
          return default(T);
        }

        var result = InnerCache.Remove(key);
        Contract.Assume(result != null);
        return (T) result;

      } finally {
        ExitWriteLock(region);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Attempts to retrieve an item with the specified ID, returning success or
    /// failure.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of the item to retrieve.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the item to retrieve.
    /// </param>
    /// 
    /// <param name="value">
    /// Output parameter.  With contain the retrieved value if a match is found,
    /// or the default value otherwise.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if a matching item was successfully retrieved;
    /// otherwise <see langword="false" />.
    /// </returns>
    public bool TryGetValue<T>(object id, out T value) {
      Contract.Requires(id != null, Resources.Messages.EveCache_IdCannotBeNull);

      string region = RegionMap.GetRegion(typeof(T));
      string key = EveCache.GetCacheKey(region, id);

      EnterReadLock(region);

      try {
        if (InnerCache.Contains(key)) {
          Statistics.Hits++;
          var result = InnerCache.Get(key);
          Contract.Assume(result != null);
          value = (T) result;
          return true;
        }

      } finally {
        ExitReadLock(region);
      }

      Statistics.Misses++;
      value = default(T);
      return false;
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets statistics about cache usage.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="CacheStatistics" /> object containing statistics about how
    /// the cache is being used.
    /// </value>
    public CacheStatistics Statistics {
      get {
        Contract.Ensures(Contract.Result<CacheStatistics>() != null);
        return _statistics;
      }
    }
    #endregion
    #region Protected Internal Properties
    //******************************************************************************
    /// <summary>
    /// Gets the inner cache object used to store data.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ObjectCache" /> used to store data.
    /// </value>
    protected internal ObjectCache InnerCache {
      get {
        Contract.Ensures(Contract.Result<ObjectCache>() != null);
        return _innerCache;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the lock used to restrict concurrent access to the cache.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReaderWriterLockSlim" /> used to restrict concurrent access
    /// to the cache.
    /// </value>
    protected internal ReaderWriterLockSlim MasterLock {
      get {
        Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
        return _masterLock;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the set of locks used to restrict concurrent access to individual
    /// cache regions.
    /// </summary>
    /// 
    /// <value>
    /// A collection of region locks used to restrict concurrent access to the
    /// cache.
    /// </value>
    protected internal ConcurrentDictionary<string, ReaderWriterLockSlim> RegionLocks {
      get {
        Contract.Ensures(Contract.Result<ConcurrentDictionary<string, ReaderWriterLockSlim>>() != null);
        return _regionLocks;
      }
    }
    #endregion
    #region Protected Internal Methods
    //******************************************************************************
    /// <summary>
    /// Begins a read lock for the specified cache region.
    /// </summary>
    /// 
    /// <param name="region">
    /// The cache region to lock.
    /// </param>
    protected internal void EnterReadLock(string region) {
      MasterLock.EnterReadLock();

      if (region != null) {
        ReaderWriterLockSlim regionLock = RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        regionLock.EnterReadLock();
      }
    }
    //******************************************************************************
    /// <summary>
    /// Begins a read lock for the specified cache region.
    /// </summary>
    /// 
    /// <param name="region">
    /// The cache region to lock, or <see langword="null" /> to lock the entire
    /// cache.
    /// </param>
    protected internal void EnterWriteLock(string region) {

      // If blocking the entire cache, lock the master lock
      if (region == null) {
        MasterLock.EnterWriteLock();

      // Otherwise lock the region lock
      } else {
        ReaderWriterLockSlim regionLock = RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        regionLock.EnterWriteLock();

        // Put a read lock on the master lock so that master writes can't take place
        // while the region is locked
        MasterLock.EnterReadLock();
      }
    }
    //******************************************************************************
    /// <summary>
    /// Ends a read lock for the specified cache region.
    /// </summary>
    /// 
    /// <param name="region">
    /// The cache region to lock.
    /// </param>
    protected internal void ExitReadLock(string region) {
      MasterLock.ExitReadLock();

      if (region != null) {
        ReaderWriterLockSlim regionLock = RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        regionLock.ExitReadLock();
      }
    }
    //******************************************************************************
    /// <summary>
    /// Ends a write lock for the specified cache region.
    /// </summary>
    /// 
    /// <param name="region">
    /// The cache region to lock, or <see langword="null" /> to lock the entire
    /// cache.
    /// </param>
    protected internal void ExitWriteLock(string region) {

      // If blocking the entire cache, lock the master lock
      if (region == null) {
        MasterLock.ExitWriteLock();

        // Otherwise lock the region lock
      } else {
        ReaderWriterLockSlim regionLock = RegionLocks.GetOrAdd(region, s => new ReaderWriterLockSlim());
        regionLock.ExitWriteLock();

        // We put a read lock on the master lock so that master writes can't take place
        // while the region is locked
        MasterLock.ExitReadLock();
      }
    }
    #endregion

    #region IEnumerable Members
    //******************************************************************************
    IEnumerator IEnumerable.GetEnumerator() {
      return ((IEnumerable<KeyValuePair<string, object>>) this).GetEnumerator();
    }
    #endregion
    #region IEnumerable<KeyValuePair<string, object>> Members
    //******************************************************************************
    IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() {
      return ((IEnumerable<KeyValuePair<string, object>>) InnerCache).Where(x => x.Key.StartsWith(PREFIX)).GetEnumerator();
    }
    #endregion
  }
}