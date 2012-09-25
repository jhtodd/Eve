//-----------------------------------------------------------------------
// <copyright file="EveDataSource.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Reflection;

  using FreeNet;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// An EveDataSource that uses an automatically-generated
  /// <see cref="EveDbContext" /> object to query the database.
  /// </summary>
  public class EveDataSource : IEveDataSource {

    #region Static Fields
    private static readonly BaseValueCache _cache = new BaseValueCache();
    private static readonly EveDataSource _default = new EveDataSource();
    #endregion
    #region Static Properties
    //******************************************************************************
    /// <summary>
    /// Gets the default EVE data source.
    /// </summary>
    /// 
    /// <value>
    /// A default <see cref="EveDataSource" /> that uses the connection settings
    /// specified in the application configuration file.
    /// </value>
    public static EveDataSource Default {
      get {
        Contract.Ensures(Contract.Result<EveDataSource>() != null);
        return _default;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the cache for <see cref="ICacheable" /> types loaded from the
    /// database.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="BaseValueCache" /> used to store references to loaded
    /// instances of base value objects.
    /// </value>
    protected static BaseValueCache Cache {
      get {
        Contract.Ensures(Contract.Result<BaseValueCache>() != null);
        return _cache;
      }
    }
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDataSource class, using the default
    /// <see cref="EveDbContext" /> to provide access to the database.
    /// </summary>
    public EveDataSource()  {
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public IList<TEntity> Get<TEntity>(IQueryModifier<TEntity> modifier) where TEntity : class, IHasId {
      lock (Context) {

        var query = Context.Set<TEntity>().AsNoTracking();
        Contract.Assume(query != null);

        query = modifier.GetResults(query);

        // If retrieving cacheable types, filter the results through the cache before returning
        if (typeof(IHasId).IsAssignableFrom(typeof(TEntity)) && BaseValueCache.RegionMap.IsRegistered(typeof(TEntity))) {
          return query.AsEnumerable().Select(x => Cache.GetOrAdd(x)).ToArray();
        }

        return query.ToArray();
      }
    }
    //******************************************************************************
    /// <inheritdoc />
    public IList<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IHasId {
      return Get<TEntity>(new QuerySpecification<TEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IList<TEntity> GetAll<TEntity>() where TEntity : class, IHasId {
      lock (Context) {
        var query = Context.Set<TEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // If retrieving cacheable types, filter the results through the cache before returning
        if (typeof(IHasId).IsAssignableFrom(typeof(TEntity)) && BaseValueCache.RegionMap.IsRegistered(typeof(TEntity))) {
          return query.AsEnumerable().Select(x => Cache.GetOrAdd(x)).ToArray();
        }

        return query.ToArray();
      }
    }
    //******************************************************************************
    /// <inheritdoc />
    public TEntity GetById<TEntity>(object id) where TEntity : class, IHasId {
      TEntity result;

      if (Cache.TryGetValue<TEntity>(id, out result)) {
        return result;
      }

      try {
        lock (Context) {
          var set = Context.Set<TEntity>();
          Contract.Assume(set != null);
          result = set.Where(x => x.Id == id).Single();

          Contract.Assume(result != null);
          Cache.GetOrAdd<TEntity>(result);
          return result;
        }
      
      } catch (InvalidOperationException ex) {
        throw new InvalidOperationException("No unique item with the specified ID could be found.", ex);
      }
    }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the <see cref="EveDbContext" /> used to provide access to the database.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="EveDbContext" /> used to provide access to the database.
    /// </value>
    protected EveDbContext Context {
      get {
        Contract.Ensures(Contract.Result<EveDbContext>() != null);
        return EveDbContext.Default;
      }
    }
    #endregion
  }
}