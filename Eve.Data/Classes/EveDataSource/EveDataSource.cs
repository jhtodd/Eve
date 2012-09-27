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
  using FreeNet.Collections;
  using FreeNet.Data.Entity;

  using Eve.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// An EveDataSource that uses an automatically-generated
  /// <see cref="EveDbContext" /> object to query the database.
  /// </summary>
  public class EveDataSource : IEveDataSource {

    #region Static Fields
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
    public virtual void PrepopulateCache(EveCache cache) {

      lock (Context) {

        // Permanently add all published Categories
        foreach (Category category in Context.Categories.Where(x => x.Published == true).AsEnumerable().Select(x => new Category(x))) {
          cache.AddOrReplace<Category>(category, true);
        }

        // Permanently add all published Groups
        foreach (Group group in Context.Groups.Where(x => x.Published == true).AsEnumerable().Select(x => new Group(x))) {
          cache.AddOrReplace<Group>(group, true);
        }

        // Permanently add all units
        foreach (Unit unit in Context.Units.AsEnumerable().Select(x => new Unit(x))) {
          cache.AddOrReplace<Unit>(unit, true);
        }
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

    #region AgentType Methods
    //******************************************************************************
      /// <inheritdoc />
      public AgentType GetAgentTypeById(AgentTypeId id) {
        AgentType result;
        if (Eve.General.Cache.TryGetValue<AgentType>(id, out result)) {
          return result;
        }

        return GetAgentTypes(x => x.Id == id).Single();
      }
      //******************************************************************************
      /// <inheritdoc />
      public IReadOnlyList<AgentType> GetAgentTypes(Expression<Func<AgentTypeEntity, bool>> filter) {
        return GetAgentTypes(new QuerySpecification<AgentTypeEntity>(filter));
      }
      //******************************************************************************
      /// <inheritdoc />
      public IReadOnlyList<AgentType> GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers) {
        lock (Context) {

          var query = Context.Set<AgentTypeEntity>().AsNoTracking();
          Contract.Assume(query != null);

          // Apply the modifiers
          foreach (IQueryModifier<AgentTypeEntity> modifier in modifiers) {
            Contract.Assume(modifier != null);
            query = modifier.GetResults(query);
          }

          // Construct the result set, filtering items through the global cache along the way
          return new ReadOnlyList<AgentType>(query.AsEnumerable().Select(x => {
            return Eve.General.Cache.GetOrAdd<AgentType>(x.Id, () => new AgentType(x));
          }));
        }
      }
      #endregion

    #region AttributeCategory Methods
    //******************************************************************************
    /// <inheritdoc />
    public AttributeCategory GetAttributeCategoryById(AttributeCategoryId id) {
      AttributeCategory result;
      if (Eve.General.Cache.TryGetValue<AttributeCategory>(id, out result)) {
        return result;
      }

      return GetAttributeCategories(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<AttributeCategory> GetAttributeCategories(Expression<Func<AttributeCategoryEntity, bool>> filter) {
      return GetAttributeCategories(new QuerySpecification<AttributeCategoryEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<AttributeCategory> GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<AttributeCategoryEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<AttributeCategoryEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<AttributeCategory>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<AttributeCategory>(x.Id, () => new AttributeCategory(x));
        }));
      }
    }
    #endregion

    #region AttributeType Methods
    //******************************************************************************
    /// <inheritdoc />
    public AttributeType GetAttributeTypeById(AttributeId id) {
      AttributeType result;
      if (Eve.General.Cache.TryGetValue<AttributeType>(id, out result)) {
        return result;
      }

      return GetAttributeTypes(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<AttributeType> GetAttributeTypes(Expression<Func<AttributeTypeEntity, bool>> filter) {
      return GetAttributeTypes(new QuerySpecification<AttributeTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<AttributeType> GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<AttributeTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<AttributeTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<AttributeType>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<AttributeType>(x.Id, () => new AttributeType(x));
        }));
      }
    }
    #endregion

    #region AttributeValue Methods
    //******************************************************************************
    /// <inheritdoc />
    public AttributeValue GetAttributeValueById(ItemTypeId itemTypeId, AttributeId id) {
      AttributeValue result;
      if (Eve.General.Cache.TryGetValue<AttributeValue>(id, out result)) {
        return result;
      }

      return GetAttributeValues(x => x.ItemTypeId == itemTypeId && x.AttributeId == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<AttributeValue> GetAttributeValues(Expression<Func<AttributeValueEntity, bool>> filter) {
      return GetAttributeValues(new QuerySpecification<AttributeValueEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<AttributeValue> GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<AttributeValueEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<AttributeValueEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // AttributeValues are a special case -- don't cache, because they only have
        // relevance to a particular ItemType, and that entire ItemType will be 
        // cached anyway
        return new ReadOnlyList<AttributeValue>(query.AsEnumerable().Select(x => new AttributeValue(x)));
      }
    }
    #endregion

    #region Category Methods
    //******************************************************************************
    /// <inheritdoc />
    public Category GetCategoryById(CategoryId id) {
      Category result;
      if (Eve.General.Cache.TryGetValue<Category>(id, out result)) {
        return result;
      }

      return GetCategories(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Category> GetCategories(Expression<Func<CategoryEntity, bool>> filter) {
      return GetCategories(new QuerySpecification<CategoryEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Category> GetCategories(params IQueryModifier<CategoryEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<CategoryEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<CategoryEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Category>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<Category>(x.Id, () => new Category(x));
        }));
      }
    }
    #endregion

    #region Group Methods
    //******************************************************************************
    /// <inheritdoc />
    public Group GetGroupById(GroupId id) {
      Group result;
      if (Eve.General.Cache.TryGetValue<Group>(id, out result)) {
        return result;
      }

      return GetGroups(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Group" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    /// <inheritdoc />
    public IReadOnlyList<Group> GetGroups(Expression<Func<GroupEntity, bool>> filter) {
      return GetGroups(new QuerySpecification<GroupEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Group> GetGroups(params IQueryModifier<GroupEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<GroupEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<GroupEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Group>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<Group>(x.Id, () => new Group(x));
        }));
      }
    }
    #endregion

    #region Icon Methods
    //******************************************************************************
    /// <inheritdoc />
    public Icon GetIconById(IconId id) {
      Icon result;
      if (Eve.General.Cache.TryGetValue<Icon>(id, out result)) {
        return result;
      }

      return GetIcons(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Icon> GetIcons(Expression<Func<IconEntity, bool>> filter) {
      return GetIcons(new QuerySpecification<IconEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Icon> GetIcons(params IQueryModifier<IconEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<IconEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<IconEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Icon>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<Icon>(x.Id, () => new Icon(x));
        }));
      }
    }
    #endregion

    #region ItemType Methods
    //******************************************************************************
    /// <inheritdoc />
    public TItem GetItemTypeById<TItem>(ItemTypeId id) where TItem : ItemType {
      TItem result;
      if (Eve.General.Cache.TryGetValue<TItem>(id, out result)) {
        return result;
      }

      return GetItemTypes<ItemType>(x => x.Id == id.Value).Cast<TItem>().Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItemTypes<TItem>(Expression<Func<ItemTypeEntity, bool>> filter) where TItem : ItemType {
      return GetItemTypes<TItem>(new QuerySpecification<ItemTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItemTypes<TItem>(params IQueryModifier<ItemTypeEntity>[] modifiers) where TItem : ItemType {
      lock (Context) {

        var query = Context.Set<ItemTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<ItemTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<TItem>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<ItemType>(x.Id, () => ItemType.Create(x));
        }).OfType<TItem>());
      }
    }
    #endregion

    #region MarketGroup Methods
    //******************************************************************************
    /// <inheritdoc />
    public MarketGroup GetMarketGroupById(MarketGroupId id) {
      MarketGroup result;
      if (Eve.General.Cache.TryGetValue<MarketGroup>(id, out result)) {
        return result;
      }

      return GetMarketGroups(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<MarketGroup> GetMarketGroups(Expression<Func<MarketGroupEntity, bool>> filter) {
      return GetMarketGroups(new QuerySpecification<MarketGroupEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<MarketGroup> GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<MarketGroupEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<MarketGroupEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<MarketGroup>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<MarketGroup>(x.Id, () => new MarketGroup(x));
        }));
      }
    }
    #endregion

    #region Race Methods
    //******************************************************************************
    /// <inheritdoc />
    public Race GetRaceById(RaceId id) {
      Race result;
      if (Eve.General.Cache.TryGetValue<Race>(id, out result)) {
        return result;
      }

      return GetRaces(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Race> GetRaces(Expression<Func<RaceEntity, bool>> filter) {
      return GetRaces(new QuerySpecification<RaceEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Race> GetRaces(params IQueryModifier<RaceEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<RaceEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<RaceEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Race>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<Race>(x.Id, () => new Race(x));
        }));
      }
    }
    #endregion

    #region Unit Methods
    //******************************************************************************
    /// <inheritdoc />
    public Unit GetUnitById(UnitId id) {
      Unit result;
      if (Eve.General.Cache.TryGetValue<Unit>(id, out result)) {
        return result;
      }

      return GetUnits(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Unit> GetUnits(Expression<Func<UnitEntity, bool>> filter) {
      return GetUnits(new QuerySpecification<UnitEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Unit> GetUnits(params IQueryModifier<UnitEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<UnitEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<UnitEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Unit>(query.AsEnumerable().Select(x => {
          return Eve.General.Cache.GetOrAdd<Unit>(x.Id, () => new Unit(x));
        }));
      }
    }
    #endregion
  }
}