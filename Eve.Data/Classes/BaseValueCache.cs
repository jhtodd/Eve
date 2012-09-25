//-----------------------------------------------------------------------
// <copyright file="BaseValueCache.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data {
  using System;
  using System.Collections;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Runtime.Caching;
  using System.Threading;

  using FreeNet;

  using Eve;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// A cache for storing instances of immutable data.
  /// </summary>
  /// 
  /// <remarks>
  /// <para>
  /// The <c>BaseValueCache</c> class is an identity map which stores immutable
  /// items in memory.  The purpose of the cache is twofold: first, because the
  /// items are immutable, there is no reason to have two instances of an
  /// identical value.  The cache provides a way to ensure that only one instance
  /// is in circulation at any given time.  Second, in some cases, being able to
  /// retrieve a cached value can save an unnecessary database call, loading and
  /// returning the cached value instead of performing a round trip to the
  /// database.
  /// </para>
  /// 
  /// <para>
  /// Items added to the cache can be stored in two ways: either as direct
  /// references to the actual objects, in which case they remain in the cache
  /// permanently (for use with very common values that should always stay
  /// resident), or as weak references, which can be periodically flushed with
  /// the <see cref="Clean" /> method if the objects they're referencing has
  /// expired.
  /// </para>
  /// 
  /// <para>
  /// Classes that wish to be stored in the cache must implement the
  /// <see cref="ICacheable" /> interface, and a mapping for them must be
  /// established in the <see cref="InnerTypeMap" />.
  /// </para>
  /// </remarks>
  public class BaseValueCache {

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

        throw new InvalidOperationException("No cache region has been defined for "  + type.FullName + ".");
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
    #endregion

    #region Static Fields
    private static readonly CacheRegionMap _regionMap;
    #endregion

    #region Static Constructor
    //******************************************************************************
    /// <summary>
    /// Initializes static members of the BaseValueCache class.
    /// </summary>
    static BaseValueCache() {
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
    /// </returns>W
    public static string GetCacheKey<T>(object id) {
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);
      Contract.Ensures(Contract.Result<string>() != null);

      Type idType = id.GetType();

      return RegionMap.GetRegion(typeof(T)) + (idType.IsEnum ? Enum.Format(idType, id, "d") : id.ToString());
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
    private Dictionary<string, object> _innerCache;
    private ReaderWriterLockSlim _lock;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the BaseValueCache class, populating the
    /// cache with an initial set of permanent contents.
    /// </summary>
    public BaseValueCache() {
      _innerCache = new Dictionary<string, object>();
      _lock = new ReaderWriterLockSlim();
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_innerCache != null);
      Contract.Invariant(_lock != null);
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Removes unused and expired entries from the cache.
    /// </summary>
    public void Clean() {
      Lock.EnterWriteLock();

      try {
        List<string> keysToRemove = new List<string>();

        // Loop over the contents of the cache.  Some entries may contain an actual
        // value and some may contain a weak reference.  The actual values are the
        // ones that have been flagged as being there permanently, so we leave
        // them alone.  The weak references are the ones that have been added
        // dynamically in response to subsequent database calls -- some of those
        // may have expired and can be removed.
        foreach (KeyValuePair<string, object> entry in InnerCache) {
          WeakReference<IHasId> weakValue = entry.Value as WeakReference<IHasId>;

          // If the entry contains a weak reference and that weak reference is no
          // longer alive, remove it.  Weak references that are still alive should
          // not be removed -- their contents are still referenced by some other
          // object and will stay in memory even if the reference is removed from
          // the cache, and so the resources that could be reclaimed by removing 
          // the reference are insignificant.
          if (weakValue != null) {
            IHasId result;

            if (!weakValue.TryGetTarget(out result)) {
              keysToRemove.Add(entry.Key);
            }
          }
        }

        foreach (string key in keysToRemove) {
          InnerCache.Remove(key);
        }

      } finally {
        Lock.ExitWriteLock();
      }
    }
    //******************************************************************************
    /// <summary>
    /// Returns a value indicating whether the cache contains an item of the
    /// desired type with the specified ID.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of the item to locate in the cache.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID to locate in the cache.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if the cache contains an item of the specified
    /// type and with the specified ID; otherwise <see langword="false" />.
    /// </returns>
    public bool Contains<T>(object id) where T : class, IHasId {
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);

      string key = GetCacheKey<T>(id);

      Lock.EnterReadLock();

      try {
        return InnerCache.ContainsKey(key);
      } finally {
        Lock.ExitReadLock();
      }
    }
    //******************************************************************************
    /// <summary>
    /// Retrieves an item from the cache, or adds a new item if no matching item
    /// was contained.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value to add and return if a matching item could not be retrieved
    /// from the cache.
    /// </param>
    /// 
    /// <returns>
    /// The value retrieved from the cache, if present, or <paramref name="value" />
    /// if not.
    /// </returns>
    /// 
    /// <remarks>
    /// <para>
    /// This method checks to see if an item with the same ID as
    /// <paramref name="value" /> is contained in the cache.  If so, that item is
    /// returned.  Otherwise, <paramref name="value" /> is added to the cache
    /// and then returned.
    /// </para>
    /// </remarks>
    public T GetOrAdd<T>(T value) where T : class, IHasId {
      Contract.Requires(value != null, Resources.Messages.BaseValueCache_ValueCannotBeNull);
      Contract.Ensures(Contract.Result<T>() != null);

      IHasId result;
      string key = GetCacheKey<T>(value.Id);

      Lock.EnterReadLock();

      try {

        // A cached version was found; return it.
        if (TryGetValueInternal(key, out result)) {
          return (T) result;
        }

      } finally {
        Lock.ExitReadLock();
      }

      // If no cached version was found, obtain a write lock and add the value
      // we were passed.
      Lock.EnterWriteLock();

      try {

        // If a cached version was added while we were waiting for the write lock;
        // return it.
        if (TryGetValueInternal(key, out result)) {
          return (T) result;
        }

        AddInternal(key, value, false);
        return value;

      } finally {
        Lock.ExitWriteLock();
      }
    }
    //******************************************************************************
    /// <summary>
    /// Attempts to retrieve a value from the cache, returning success or
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
    /// Output parameter.  Will contain the retrieved value, or the default if no
    /// value was retrieved.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if a value was successfully retrieved; otherwise
    /// <see langword="false" />.
    /// </returns>
    public bool TryGetValue<T>(object id, out T value) where T : class, IHasId {
      Contract.Requires(id != null, Resources.Messages.BaseValueCache_IdCannotBeNull);

      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      
      string key = GetCacheKey<T>(id);

      Lock.EnterReadLock();

      try {
        IHasId result;
        bool success = TryGetValueInternal(key, out result);

        if (success) {
          //Contract.Assume(result != null);
        }

        value = (T) result;
        return success;

      } finally {
        Lock.ExitReadLock();
      }
    }
    #endregion
    #region Protected Internal Properties
    //******************************************************************************
    /// <summary>
    /// Gets the inner dictionary used to cache values.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ConcurrentDictionary{string, TValue}" /> used to cache values.
    /// </value>
    protected internal Dictionary<string, object> InnerCache {
      get {
        Contract.Ensures(Contract.Result<Dictionary<string, object>>() != null);
        return _innerCache;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the lock used to synchronize the cache.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ReaderWriterLockSlim" /> used to synchronize the cache.
    /// </value>
    protected internal ReaderWriterLockSlim Lock {
      get {
        Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
        return _lock;
      }
    }
    #endregion
    #region Protected Methods
    //******************************************************************************
    /// <summary>
    /// Adds an item to the cache.
    /// </summary>
    /// 
    /// <param name="value">
    /// The item to add to the cache.
    /// </param>
    /// 
    /// <param name="permanent">
    /// <see langword="true" /> to add the item as a pemanent value, or
    /// <see langword="false"/> to add the item as a weak reference.
    /// </param>
    /// 
    /// <remarks>
    /// <para>
    /// This method is for internal use and does not handle concurrency.  Any
    /// calling methods must make sure a write lock is obtained before invoking
    /// this method.
    /// </para>
    /// </remarks>
    protected void AddInternal(string key, IHasId value, bool permanent) {
      Contract.Requires(value != null, Resources.Messages.BaseValueCache_ValueCannotBeNull);

      InnerCache[key] = permanent ? value : (object) new WeakReference<IHasId>(value);
    }
    //******************************************************************************
    /// <summary>
    /// Attempts to retrieve a value from the cache, returning success or
    /// failure.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to retrieve.
    /// </param>
    /// 
    /// <param name="value">
    /// Output parameter.  Will contain the retrieved value, or the default if no
    /// value was retrieved.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if a value was successfully retrieved; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// 
    /// <remarks>
    /// <para>
    /// This method is for internal use and does not handle concurrency.  Any
    /// calling methods must make sure a read lock is obtained before invoking
    /// this method.
    /// </para>
    /// </remarks>
    protected bool TryGetValueInternal(string key, out IHasId value) {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn<IHasId>(out value) != null);

      object storedValue;

      if (InnerCache.TryGetValue(key, out storedValue)) {
        value = storedValue as IHasId;

        // If the value in the cache is an actual value, return it
        if (value != null) {
          return true;
        
        // Otherwise it's a weak reference -- we need to see if it's still alive
        // first
        } else {
          Contract.Assume(storedValue != null);
          WeakReference<IHasId> weakValue = (WeakReference<IHasId>) storedValue;
          bool success = weakValue.TryGetTarget(out value);

          Contract.Assume(!success || value != null);
          return success;
        }
      }

      value = null;
      return false;
    }
    #endregion
  }
}