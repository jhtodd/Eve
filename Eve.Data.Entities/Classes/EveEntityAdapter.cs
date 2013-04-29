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
  /// <typeparam name="TDerived">
  /// The type of the concrete derived class.  For more information, see
  /// the <see cref="EveDataObject{TDerived}" /> class.
  /// </typeparam>
  public abstract partial class EveEntityAdapter<TEntity, TDerived> 
    : EveDataObject<TDerived>,
      IEveEntityAdapter<TEntity>
    where TEntity : IEveEntity
    where TDerived : class
  {
    private readonly TEntity entity;
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
    protected EveEntityAdapter(IEveRepository repository, TEntity entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");

      this.entity = entity;
      this.repository = repository;
    }

    /* Properties */

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        return this.Entity.CacheKey;
      }
    }

    /// <summary>
    /// Gets the entity encapsulated by the adapter.
    /// </summary>
    /// <value>
    /// The entity encapsulated by the adapter.
    /// </value>
    protected TEntity Entity
    {
      get
      {
        Contract.Ensures(Contract.Result<TEntity>() != null);
        return this.entity;
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
    /// <typeparam name="TOutput">
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
    protected TOutput LazyInitializeAdapter<TProvidedEntity, TAdapter, TOutput>(ref TOutput adapter, IConvertible cacheKey, Func<TProvidedEntity> entityProvider)
      where TProvidedEntity : IEveEntity<TAdapter>
      where TAdapter : class, IEveCacheable
      where TOutput : class, TAdapter
    {
      Contract.Requires(cacheKey != null, "The cache key cannot be null.");
      Contract.Requires(entityProvider != null, "The entity provider delegate cannot be null.");
      Contract.Ensures(Contract.Result<TAdapter>() != null);
      Contract.Ensures(Contract.ValueAtReturn(out adapter) != null);

      LazyInitializer.EnsureInitialized(
        ref adapter,
        () => this.Repository.GetOrAddStoredValue<TOutput>(cacheKey, () => (TOutput)entityProvider().ToAdapter(this.Repository)));

      Contract.Assume(adapter != null);
      return adapter;
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.entity != null);
      Contract.Invariant(this.repository != null);
    }
  }

  #region IEntityAdapter<TEntity> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEntityAdapter{TEntity}" /> interface.
  /// </content>
  public partial class EveEntityAdapter<TEntity, TDerived> : IEntityAdapter<TEntity>
  {
    TEntity IEntityAdapter<TEntity>.Entity
    {
      get { return this.Entity; }
    }
  }
  #endregion

  #region IEveRepositoryItem Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveRepositoryItem" /> interface.
  /// </content>
  public abstract partial class EveEntityAdapter<TEntity, TDerived> : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Repository
    {
      get { return this.Repository; }
    }
  }
  #endregion
}