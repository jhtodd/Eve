//-----------------------------------------------------------------------
// <copyright file="EveEntityAdapter.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for objects which serve as an adapter for EVE entities.
  /// </summary>
  /// <typeparam name="TEntity">
  /// The type of entity wrapped by the adapter.
  /// </typeparam>
  public abstract partial class EveEntityAdapter<TEntity> 
    : EntityAdapter<TEntity>,
      IEveCacheable,
      IEveEntityAdapter<TEntity>
    where TEntity : IEveEntity
  {
    private readonly IEveRepository repository;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveEntityAdapter class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected EveEntityAdapter(IEveRepository repository, TEntity entity) : base(entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");

      this.repository = repository;
    }

    /* Properties */

    /// <summary>
    /// Gets the ID value used to store the object in the cache.
    /// </summary>
    /// <value>
    /// A value which uniquely identifies the entity.
    /// </value>
    protected internal virtual IConvertible CacheKey
    {
      get
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        return this.Entity.CacheKey;
      }
    }

    /// <summary>
    /// Gets the <see cref="IEveRepository" /> the item is associated
    /// with.
    /// </summary>
    /// <value>
    /// The <see cref="IEveRepository" /> the item is associated with.
    /// </value>
    protected IEveRepository Repository
    {
      get
      {
        Contract.Ensures(Contract.Result<IEveRepository>() != null);
        return this.repository;
      }
    }

    /* Methods */

    /// <summary>
    /// Lazily initializes the specified value, using the provided factory
    /// delegate to create the value if necessary.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value to initialize.
    /// </typeparam>
    /// <param name="value">
    /// The value to initialize.
    /// </param>
    /// <param name="valueFactory">
    /// A delegate that can be used to generate the value to be assigned
    /// to <paramref name="value" /> if it has not already been initialized.
    /// </param>
    /// <returns>
    /// The result of the initialization.
    /// </returns>
    protected static T LazyInitialize<T>(ref T value, Func<T> valueFactory) where T : class
    {
      Contract.Requires(valueFactory != null, "The value factory delegate cannot be null.");
      Contract.Ensures(Contract.Result<T>() != null);
      Contract.Ensures(Contract.ValueAtReturn(out value) != null);

      var result = LazyInitializer.EnsureInitialized(ref value, valueFactory);

      Contract.Assume(result != null);
      Contract.Assume(value != null);

      return result;
    }

    /// <summary>
    /// If a cached adapter for the specified entity type exists, returns that
    /// cached adapter.  If not, creates a new adapter for the entity and
    /// stores it in the cache.
    /// </summary>
    /// <typeparam name="TProvidedEntity">
    /// The type of entity for which to return an adapter.
    /// </typeparam>
    /// <typeparam name="TAdapter">
    /// The type of the adapter to retrieve or create.
    /// </typeparam>
    /// <param name="adapter">
    /// The variable which will contain the retrieve or created adapter.
    /// Passed by reference.
    /// </param>
    /// <param name="cacheKey">
    /// The key for which to attempt to find a adapter for the entity
    /// returned by <paramref name="entityProvider" />.
    /// </param>
    /// <param name="entityProvider">
    /// A method which will return the entity for which to retrieve or create
    /// an adapter.  This is only invoked if no cached adapter with
    /// the specified <paramref name="cacheKey" /> is found.
    /// </param>
    /// <returns>
    /// An entity adapter of type <typeparamref name="TAdapter" /> which
    /// wraps the specified entity.  This is the same value assigned to
    /// <paramref name="adapter" />.
    /// </returns>
    protected TAdapter LazyInitializeAdapter<TProvidedEntity, TAdapter>(ref TAdapter adapter, IConvertible cacheKey, Func<TProvidedEntity> entityProvider)
      where TProvidedEntity : IEveEntity<TAdapter>
      where TAdapter : class, IEveCacheable
    {
      Contract.Requires(cacheKey != null, "The cache key cannot be null.");
      Contract.Requires(entityProvider != null, "The entity provider delegate cannot be null.");
      Contract.Ensures(Contract.Result<TAdapter>() != null);
      Contract.Ensures(Contract.ValueAtReturn(out adapter) != null);

      return LazyInitialize(
        ref adapter, 
        () => this.Repository.GetOrAddStoredValue<TAdapter>(cacheKey, () => entityProvider().ToAdapter(this.Repository)));
    }

    /// <summary>
    /// If a cached adapter for the specified entity type exists, returns that
    /// cached adapter.  If not, creates a new adapter for the entity and
    /// stores it in the cache.
    /// </summary>
    /// <typeparam name="TProvidedEntity">
    /// The type of entity for which to return an adapter.
    /// </typeparam>
    /// <typeparam name="TAdapter">
    /// The type of the adapter to retrieve or create.
    /// </typeparam>
    /// <typeparam name="TDerived">
    /// The type to cast the adapter to.
    /// </typeparam>
    /// <param name="adapter">
    /// The variable which will contain the retrieve or created adapter.
    /// Passed by reference.
    /// </param>
    /// <param name="cacheKey">
    /// The key for which to attempt to find a adapter for the entity
    /// returned by <paramref name="entityProvider" />.
    /// </param>
    /// <param name="entityProvider">
    /// A method which will return the entity for which to retrieve or create
    /// an adapter.  This is only invoked if no cached adapter with
    /// the specified <paramref name="cacheKey" /> is found.
    /// </param>
    /// <returns>
    /// An entity adapter of type <typeparamref name="TAdapter" /> which
    /// wraps the specified entity.
    /// </returns>
    protected TDerived LazyInitializeAdapter<TProvidedEntity, TAdapter, TDerived>(ref TDerived adapter, IConvertible cacheKey, Func<TProvidedEntity> entityProvider)
      where TProvidedEntity : IEveEntity<TAdapter>
      where TAdapter : class, IEveCacheable
      where TDerived : class, TAdapter
    {
      Contract.Requires(cacheKey != null, "The cache key cannot be null.");
      Contract.Requires(entityProvider != null, "The entity provider delegate cannot be null.");
      Contract.Ensures(Contract.Result<TAdapter>() != null);

      LazyInitializer.EnsureInitialized(
        ref adapter,
        () => this.Repository.GetOrAddStoredValue<TDerived>(cacheKey, () => (TDerived)entityProvider().ToAdapter(this.Repository)));

      Contract.Assume(adapter != null);
      return adapter;
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.repository != null);
    }
  }

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public abstract partial class EveEntityAdapter<TEntity> : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion

  #region IEveRepositoryItem Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveRepositoryItem" /> interface.
  /// </content>
  public abstract partial class EveEntityAdapter<TEntity> : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Repository
    {
      get { return this.Repository; }
    }
  }
  #endregion
}