//-----------------------------------------------------------------------
// <copyright file="EveDataSource.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;
  using System.Reflection;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// An EveDataSource that uses an automatically-generated
  /// <see cref="EveDbContext" /> object to query the database.
  /// </summary>
  public class EveDataSource : IEveDataSource
  {
    private static readonly EveDataSource DefaultInstance = new EveDataSource();

    private Func<EveDbContext> contextFactory;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveDataSource class, using the default
    /// <see cref="EveDbContext" /> to provide access to the database.
    /// </summary>
    public EveDataSource() : this(() => EveDbContext.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EveDataSource class, using the specified
    /// <see cref="EveDbContext" /> factory method to provide access to the database.
    /// </summary>
    /// <param name="contextFactory">
    /// A function for creating an <see cref="EveDbContext" /> which the data source 
    /// can use to provide access to the database.  A new instance can be returned
    /// each time the function is invoked, or the function can return a single
    /// instance repeatedly, although it should ensure that the same instance is
    /// never provided to multiple threads.
    /// </param>
    public EveDataSource(Func<EveDbContext> contextFactory)
    {
      Contract.Requires(contextFactory != null, "The context factory cannot be null.");
      this.contextFactory = contextFactory;
    }

    /* Properties */

    /// <summary>
    /// Gets the default EVE data source.
    /// </summary>
    /// <value>
    /// A default <see cref="EveDataSource" /> that uses the connection settings
    /// specified in the application configuration file.
    /// </value>
    public static EveDataSource Default
    {
      get
      {
        Contract.Ensures(Contract.Result<EveDataSource>() != null);
        return DefaultInstance;
      }
    }

    /// <summary>
    /// Gets or sets a factory method which returns a <see cref="EveDbContext" />
    /// that can be used to provide access to the database.
    /// </summary>
    /// <value>
    /// A method returning an <see cref="EveDbContext" /> that can be used to
    /// provide access to the database.
    /// </value>
    protected Func<EveDbContext> ContextFactory
    {
      get
      {
        Contract.Ensures(Contract.Result<Func<EveDbContext>>() != null);
        return this.contextFactory;
      }

      set
      {
        Contract.Requires(value != null, "The context factory cannot be null.");
        this.contextFactory = value;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public virtual void PrepopulateCache(EveCache cache)
    {
      var context = this.ContextFactory();

      // Permanently add all published Categories
      foreach (Category category in context.Categories.Where(x => x.Published == true).ToList().Select(x => x.ToAdapter()))
      {
        Contract.Assume(category != null);
        cache.AddOrReplace<Category>(category, true);
      }

      // Permanently add all Groups -- necessary for EveType.Create() to load successfully
      foreach (Group group in context.Groups.Where(x => x.Published == true).ToList().Select(x => x.ToAdapter()))
      {
        Contract.Assume(group != null);
        cache.AddOrReplace<Group>(group, true);
      }

      // Permanently add all units
      foreach (Unit unit in context.Units.ToList().Select(x => x.ToAdapter()))
      {
        Contract.Assume(unit != null);
        cache.AddOrReplace<Unit>(unit, true);
      }
    }

    #region Agent Methods
    /// <inheritdoc />
    public Agent GetAgentById(AgentId id)
    {
      Agent result;
      if (Eve.General.Cache.TryGetValue<Agent>(id, out result))
      {
        return result;
      }

      return this.GetAgents(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Agent> GetAgents(Expression<Func<AgentEntity, bool>> filter)
    {
      return this.GetAgents(new QuerySpecification<AgentEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Agent))]
    public IReadOnlyList<Agent> GetAgents(params IQueryModifier<AgentEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Agent>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region AgentType Methods
    /// <inheritdoc />
    public AgentType GetAgentTypeById(AgentTypeId id)
    {
      AgentType result;
      if (Eve.General.Cache.TryGetValue<AgentType>(id, out result))
      {
        return result;
      }

      return this.GetAgentTypes(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<AgentType> GetAgentTypes(Expression<Func<AgentTypeEntity, bool>> filter)
    {
      return this.GetAgentTypes(new QuerySpecification<AgentTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AgentType))]
    public IReadOnlyList<AgentType> GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<AgentType>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region AttributeCategory Methods
    /// <inheritdoc />
    public AttributeCategory GetAttributeCategoryById(AttributeCategoryId id)
    {
      AttributeCategory result;
      if (Eve.General.Cache.TryGetValue<AttributeCategory>(id, out result))
      {
        return result;
      }

      return this.GetAttributeCategories(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<AttributeCategory> GetAttributeCategories(Expression<Func<AttributeCategoryEntity, bool>> filter)
    {
      return this.GetAttributeCategories(new QuerySpecification<AttributeCategoryEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AttributeCategory))]
    public IReadOnlyList<AttributeCategory> GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<AttributeCategory>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region AttributeType Methods
    /// <inheritdoc />
    public AttributeType GetAttributeTypeById(AttributeId id)
    {
      AttributeType result;
      if (Eve.General.Cache.TryGetValue<AttributeType>(id, out result))
      {
        return result;
      }

      return this.GetAttributeTypes(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<AttributeType> GetAttributeTypes(Expression<Func<AttributeTypeEntity, bool>> filter)
    {
      return this.GetAttributeTypes(new QuerySpecification<AttributeTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AttributeType))]
    public IReadOnlyList<AttributeType> GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<AttributeType>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region AttributeValue Methods
    /// <inheritdoc />
    public AttributeValue GetAttributeValueById(TypeId itemTypeId, AttributeId id)
    {
      AttributeValue result;
      if (Eve.General.Cache.TryGetValue<AttributeValue>(id, out result))
      {
        return result;
      }

      return this.GetAttributeValues(x => x.ItemTypeId == itemTypeId && x.AttributeId == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<AttributeValue> GetAttributeValues(Expression<Func<AttributeValueEntity, bool>> filter)
    {
      return this.GetAttributeValues(new QuerySpecification<AttributeValueEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AttributeValue))]
    public IReadOnlyList<AttributeValue> GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers)
    {
      // AttributeValues are a special case -- don't cache, because they only have
      // relevance to a particular EveType, and that entire EveType will be 
      // cached anyway
      return GetList(modifiers).Select(x => x.ToAdapter()).ToArray();
    }
    #endregion

    #region Category Methods
    /// <inheritdoc />
    public Category GetCategoryById(CategoryId id)
    {
      Category result;
      if (Eve.General.Cache.TryGetValue<Category>(id, out result))
      {
        return result;
      }

      return this.GetCategories(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Category> GetCategories(Expression<Func<CategoryEntity, bool>> filter)
    {
      return this.GetCategories(new QuerySpecification<CategoryEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Category))]
    public IReadOnlyList<Category> GetCategories(params IQueryModifier<CategoryEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Category>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region CharacterAttributeType Methods
    /// <inheritdoc />
    public CharacterAttributeType GetCharacterAttributeTypeById(CharacterAttributeId id)
    {
      CharacterAttributeType result;
      if (Eve.General.Cache.TryGetValue<CharacterAttributeType>(id, out result))
      {
        return result;
      }

      return this.GetCharacterAttributeTypes(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(Expression<Func<CharacterAttributeTypeEntity, bool>> filter)
    {
      return this.GetCharacterAttributeTypes(new QuerySpecification<CharacterAttributeTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CharacterAttributeType))]
    public IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<CharacterAttributeType>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Constellation Methods
    /// <inheritdoc />
    public Constellation GetConstellationById(ConstellationId id)
    {
      Constellation result;
      if (Eve.General.Cache.TryGetValue<Constellation>(id, out result))
      {
        return result;
      }

      return this.GetConstellations(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Constellation> GetConstellations(Expression<Func<ConstellationEntity, bool>> filter)
    {
      return this.GetConstellations(new QuerySpecification<ConstellationEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Constellation))]
    public IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Constellation>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region ConstellationJump Methods
    /// <inheritdoc />
    public ConstellationJump GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId)
    {
      ConstellationJump result;
      if (Eve.General.Cache.TryGetValue<ConstellationJump>(ConstellationJump.CreateCompoundId(fromConstellationId, toConstellationId), out result))
      {
        return result;
      }

      return this.GetConstellationJumps(x => x.FromConstellationId == fromConstellationId.Value && x.ToConstellationId == toConstellationId.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<ConstellationJump> GetConstellationJumps(Expression<Func<ConstellationJumpEntity, bool>> filter)
    {
      return this.GetConstellationJumps(new QuerySpecification<ConstellationJumpEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(ConstellationJump))]
    public IReadOnlyList<ConstellationJump> GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select<ConstellationJumpEntity, ConstellationJump>(x =>
      {
        var cacheId = ConstellationJump.CreateCompoundId(x.FromConstellationId, x.ToConstellationId);
        return Eve.General.Cache.GetOrAdd<ConstellationJump>(cacheId, () => x.ToAdapter());
      }).ToArray();
    }
    #endregion

    #region CorporateActivity Methods
    /// <inheritdoc />
    public CorporateActivity GetCorporateActivityById(CorporateActivityId id)
    {
      CorporateActivity result;
      if (Eve.General.Cache.TryGetValue<CorporateActivity>(id, out result))
      {
        return result;
      }

      return this.GetCorporateActivities(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<CorporateActivity> GetCorporateActivities(Expression<Func<CorporateActivityEntity, bool>> filter)
    {
      return this.GetCorporateActivities(new QuerySpecification<CorporateActivityEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CorporateActivity))]
    public IReadOnlyList<CorporateActivity> GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<CorporateActivity>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Division Methods
    /// <inheritdoc />
    public Division GetDivisionById(DivisionId id)
    {
      Division result;
      if (Eve.General.Cache.TryGetValue<Division>(id, out result))
      {
        return result;
      }

      return this.GetDivisions(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Division> GetDivisions(Expression<Func<DivisionEntity, bool>> filter)
    {
      return this.GetDivisions(new QuerySpecification<DivisionEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Division))]
    public IReadOnlyList<Division> GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Division>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Effect Methods
    /// <inheritdoc />
    public Effect GetEffectById(TypeId itemTypeId, EffectId id)
    {
      Effect result;
      if (Eve.General.Cache.TryGetValue<Effect>(id, out result))
      {
        return result;
      }

      return this.GetEffects(x => x.ItemTypeId == itemTypeId && x.EffectId == (short)id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Effect> GetEffects(Expression<Func<EffectEntity, bool>> filter)
    {
      return this.GetEffects(new QuerySpecification<EffectEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Effect))]
    public IReadOnlyList<Effect> GetEffects(params IQueryModifier<EffectEntity>[] modifiers)
    {
      // Effects are a special case -- don't cache, because they only have
      // relevance to a particular EveType, and that entire EveType will be 
      // cached anyway
      return GetList(modifiers).Select(x => x.ToAdapter()).ToArray();
    }
    #endregion

    #region EffectType Methods
    /// <inheritdoc />
    public EffectType GetEffectTypeById(EffectId id)
    {
      EffectType result;
      if (Eve.General.Cache.TryGetValue<EffectType>(id, out result))
      {
        return result;
      }

      return this.GetEffectTypes(x => x.Id == (short)id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<EffectType> GetEffectTypes(Expression<Func<EffectTypeEntity, bool>> filter)
    {
      return this.GetEffectTypes(new QuerySpecification<EffectTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(EffectType))]
    public IReadOnlyList<EffectType> GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<EffectType>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region EveType Methods
    /// <inheritdoc />
    public EveType GetEveTypeById(TypeId id)
    {
      EveType result;

      if (Eve.General.Cache.TryGetValue<EveType>(id, out result))
      {
        return result;
      }

      return this.GetEveTypes(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<EveType> GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter)
    {
      return this.GetEveTypes(new QuerySpecification<EveTypeEntity>(filter));
    }

    /// <inheritdoc />
    public IReadOnlyList<EveType> GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<EveType>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }

    /// <inheritdoc />
    public TEveType GetEveTypeById<TEveType>(TypeId id) where TEveType : EveType
    {
      TEveType result;
      if (Eve.General.Cache.TryGetValue<TEveType>(id, out result))
      {
        return result;
      }

      return this.GetEveTypes<EveType>(x => x.Id == id.Value).Cast<TEveType>().Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<TEveType> GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter) where TEveType : EveType
    {
      return this.GetEveTypes<TEveType>(new QuerySpecification<EveTypeEntity>(filter));
    }

    /// <inheritdoc />
    public IReadOnlyList<TEveType> GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers) where TEveType : EveType
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<EveType>(x.Id, () => x.ToAdapter()))
                               .OfType<TEveType>()
                               .ToArray();
    }
    #endregion

    #region Faction Methods
    /// <inheritdoc />
    public Faction GetFactionById(FactionId id)
    {
      Faction result;
      if (Eve.General.Cache.TryGetValue<Faction>(id, out result))
      {
        return result;
      }

      return this.GetFactions(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Faction> GetFactions(Expression<Func<FactionEntity, bool>> filter)
    {
      return this.GetFactions(new QuerySpecification<FactionEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Faction))]
    public IReadOnlyList<Faction> GetFactions(params IQueryModifier<FactionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Faction>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Graphic Methods
    /// <inheritdoc />
    public Graphic GetGraphicById(GraphicId id)
    {
      Graphic result;
      if (Eve.General.Cache.TryGetValue<Graphic>(id, out result))
      {
        return result;
      }

      return this.GetGraphics(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Graphic> GetGraphics(Expression<Func<GraphicEntity, bool>> filter)
    {
      return this.GetGraphics(new QuerySpecification<GraphicEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Graphic))]
    public IReadOnlyList<Graphic> GetGraphics(params IQueryModifier<GraphicEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Graphic>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Group Methods
    /// <inheritdoc />
    public Group GetGroupById(GroupId id)
    {
      Group result;
      if (Eve.General.Cache.TryGetValue<Group>(id, out result))
      {
        return result;
      }

      return this.GetGroups(x => x.Id == id).Single();
    }

    /// <summary>
    /// Returns the results of the specified query for <see cref="Group" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    /// <inheritdoc />
    public IReadOnlyList<Group> GetGroups(Expression<Func<GroupEntity, bool>> filter)
    {
      return this.GetGroups(new QuerySpecification<GroupEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Group))]
    public IReadOnlyList<Group> GetGroups(params IQueryModifier<GroupEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Group>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Icon Methods
    /// <inheritdoc />
    public Icon GetIconById(IconId id)
    {
      Icon result;
      if (Eve.General.Cache.TryGetValue<Icon>(id, out result))
      {
        return result;
      }

      return this.GetIcons(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Icon> GetIcons(Expression<Func<IconEntity, bool>> filter)
    {
      return this.GetIcons(new QuerySpecification<IconEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Icon))]
    public IReadOnlyList<Icon> GetIcons(params IQueryModifier<IconEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Icon>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Item Methods
    /// <inheritdoc />
    public Item GetItemById(ItemId id)
    {
      Item result;
      if (Eve.General.Cache.TryGetValue<Item>(id, out result))
      {
        return result;
      }

      return this.GetItems(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Item> GetItems(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetItems(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    public IReadOnlyList<Item> GetItems(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Item>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }

    /// <inheritdoc />
    public TItem GetItemById<TItem>(ItemId id) where TItem : Item
    {
      TItem result = this.GetItemById(id).ConvertTo<TItem>();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter) where TItem : Item
    {
      return this.GetItems<TItem>(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers) where TItem : Item
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Item>(x.Id, () => x.ToAdapter()))
                               .OfType<TItem>()
                               .ToArray();
    }
    #endregion

    #region MarketGroup Methods
    /// <inheritdoc />
    public MarketGroup GetMarketGroupById(MarketGroupId id)
    {
      MarketGroup result;
      if (Eve.General.Cache.TryGetValue<MarketGroup>(id, out result))
      {
        return result;
      }

      return this.GetMarketGroups(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<MarketGroup> GetMarketGroups(Expression<Func<MarketGroupEntity, bool>> filter)
    {
      return this.GetMarketGroups(new QuerySpecification<MarketGroupEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(MarketGroup))]
    public IReadOnlyList<MarketGroup> GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<MarketGroup>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region MetaGroup Methods
    /// <inheritdoc />
    public MetaGroup GetMetaGroupById(MetaGroupId id)
    {
      MetaGroup result;
      if (Eve.General.Cache.TryGetValue<MetaGroup>(id, out result))
      {
        return result;
      }

      return this.GetMetaGroups(x => x.Id == id).Single();
    }

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaGroup" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    /// <inheritdoc />
    public IReadOnlyList<MetaGroup> GetMetaGroups(Expression<Func<MetaGroupEntity, bool>> filter)
    {
      return this.GetMetaGroups(new QuerySpecification<MetaGroupEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(MetaGroup))]
    public IReadOnlyList<MetaGroup> GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<MetaGroup>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region MetaType Methods
    /// <inheritdoc />
    public MetaType GetMetaTypeById(TypeId id)
    {
      MetaType result;
      if (Eve.General.Cache.TryGetValue<MetaType>(id, out result))
      {
        return result;
      }

      return this.GetMetaTypes(x => x.TypeId == id).Single();
    }

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaType" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    /// <inheritdoc />
    public IReadOnlyList<MetaType> GetMetaTypes(Expression<Func<MetaTypeEntity, bool>> filter)
    {
      return this.GetMetaTypes(new QuerySpecification<MetaTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(MetaType))]
    public IReadOnlyList<MetaType> GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<MetaType>(x.TypeId, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region NpcCorporation Methods
    /// <inheritdoc />
    public NpcCorporation GetNpcCorporationById(NpcCorporationId id)
    {
      NpcCorporation result;
      if (Eve.General.Cache.TryGetValue<NpcCorporation>(id, out result))
      {
        return result;
      }

      return this.GetNpcCorporations(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(Expression<Func<NpcCorporationEntity, bool>> filter)
    {
      return this.GetNpcCorporations(new QuerySpecification<NpcCorporationEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(NpcCorporation))]
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<NpcCorporation>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region NpcCorporationDivision Methods
    /// <inheritdoc />
    public NpcCorporationDivision GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId)
    {
      NpcCorporationDivision result;
      if (Eve.General.Cache.TryGetValue<NpcCorporationDivision>(NpcCorporationDivision.CreateCompoundId(corporationId, divisionId), out result))
      {
        return result;
      }

      return this.GetNpcCorporationDivisions(x => x.CorporationId == corporationId.Value && x.DivisionId == divisionId).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(Expression<Func<NpcCorporationDivisionEntity, bool>> filter)
    {
      return this.GetNpcCorporationDivisions(new QuerySpecification<NpcCorporationDivisionEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(NpcCorporationDivision))]
    public IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select<NpcCorporationDivisionEntity, NpcCorporationDivision>(x =>
      {
        var cacheId = NpcCorporationDivision.CreateCompoundId(x.CorporationId, x.DivisionId);
        return Eve.General.Cache.GetOrAdd<NpcCorporationDivision>(cacheId, () => x.ToAdapter());
      }).ToArray();
    }
    #endregion

    #region Race Methods
    /// <inheritdoc />
    public Race GetRaceById(RaceId id)
    {
      Race result;
      if (Eve.General.Cache.TryGetValue<Race>(id, out result))
      {
        return result;
      }

      return this.GetRaces(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Race> GetRaces(Expression<Func<RaceEntity, bool>> filter)
    {
      return this.GetRaces(new QuerySpecification<RaceEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Race))]
    public IReadOnlyList<Race> GetRaces(params IQueryModifier<RaceEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Race>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Region Methods
    /// <inheritdoc />
    public Region GetRegionById(RegionId id)
    {
      Region result;
      if (Eve.General.Cache.TryGetValue<Region>(id, out result))
      {
        return result;
      }

      return this.GetRegions(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Region> GetRegions(Expression<Func<RegionEntity, bool>> filter)
    {
      return this.GetRegions(new QuerySpecification<RegionEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Region))]
    public IReadOnlyList<Region> GetRegions(params IQueryModifier<RegionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Region>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region RegionJump Methods
    /// <inheritdoc />
    public RegionJump GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId)
    {
      RegionJump result;
      if (Eve.General.Cache.TryGetValue<RegionJump>(RegionJump.CreateCompoundId(fromRegionId, toRegionId), out result))
      {
        return result;
      }

      return this.GetRegionJumps(x => x.FromRegionId == fromRegionId && x.ToRegionId == toRegionId).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<RegionJump> GetRegionJumps(Expression<Func<RegionJumpEntity, bool>> filter)
    {
      return this.GetRegionJumps(new QuerySpecification<RegionJumpEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(RegionJump))]
    public IReadOnlyList<RegionJump> GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select<RegionJumpEntity, RegionJump>(x =>
      {
        var cacheId = RegionJump.CreateCompoundId(x.FromRegionId, x.ToRegionId);
        return Eve.General.Cache.GetOrAdd<RegionJump>(cacheId, () => x.ToAdapter());
      }).ToArray();
    }
    #endregion

    #region SolarSystem Methods
    /// <inheritdoc />
    public SolarSystem GetSolarSystemById(SolarSystemId id)
    {
      SolarSystem result;
      if (Eve.General.Cache.TryGetValue<SolarSystem>(id, out result))
      {
        return result;
      }

      return this.GetSolarSystems(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<SolarSystem> GetSolarSystems(Expression<Func<SolarSystemEntity, bool>> filter)
    {
      return this.GetSolarSystems(new QuerySpecification<SolarSystemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(SolarSystem))]
    public IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<SolarSystem>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region SolarSystemJump Methods
    /// <inheritdoc />
    public SolarSystemJump GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId)
    {
      SolarSystemJump result;
      if (Eve.General.Cache.TryGetValue<SolarSystemJump>(SolarSystemJump.CreateCompoundId(fromSolarSystemId, toSolarSystemId), out result))
      {
        return result;
      }

      return this.GetSolarSystemJumps(x => x.FromSolarSystemId == fromSolarSystemId.Value && x.ToSolarSystemId == toSolarSystemId.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(Expression<Func<SolarSystemJumpEntity, bool>> filter)
    {
      return this.GetSolarSystemJumps(new QuerySpecification<SolarSystemJumpEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(SolarSystemJump))]
    public IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select<SolarSystemJumpEntity, SolarSystemJump>(x =>
      {
        var cacheId = SolarSystemJump.CreateCompoundId(x.FromSolarSystemId, x.ToSolarSystemId);
        return Eve.General.Cache.GetOrAdd<SolarSystemJump>(cacheId, () => x.ToAdapter());
      }).ToArray();
    }
    #endregion

    #region StationOperation Methods
    /// <inheritdoc />
    public StationOperation GetStationOperationById(StationOperationId id)
    {
      StationOperation result;
      if (Eve.General.Cache.TryGetValue<StationOperation>(id, out result))
      {
        return result;
      }

      return this.GetStationOperations(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<StationOperation> GetStationOperations(Expression<Func<StationOperationEntity, bool>> filter)
    {
      return this.GetStationOperations(new QuerySpecification<StationOperationEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(StationOperation))]
    public IReadOnlyList<StationOperation> GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<StationOperation>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region StationService Methods
    /// <inheritdoc />
    public StationService GetStationServiceById(StationServiceId id)
    {
      StationService result;
      if (Eve.General.Cache.TryGetValue<StationService>(id, out result))
      {
        return result;
      }

      return this.GetStationServices(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<StationService> GetStationServices(Expression<Func<StationServiceEntity, bool>> filter)
    {
      return this.GetStationServices(new QuerySpecification<StationServiceEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(StationService))]
    public IReadOnlyList<StationService> GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<StationService>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region StationType Methods
    /// <inheritdoc />
    public StationType GetStationTypeById(TypeId id)
    {
      StationType result;
      if (Eve.General.Cache.TryGetValue<StationType>(id, out result))
      {
        return result;
      }

      return this.GetStationTypes(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<StationType> GetStationTypes(Expression<Func<StationTypeEntity, bool>> filter)
    {
      return this.GetStationTypes(new QuerySpecification<StationTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(StationType))]
    public IReadOnlyList<StationType> GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<StationType>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Unit Methods
    /// <inheritdoc />
    public Unit GetUnitById(UnitId id)
    {
      Unit result;
      if (Eve.General.Cache.TryGetValue<Unit>(id, out result))
      {
        return result;
      }

      return this.GetUnits(x => x.Id == id).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Unit> GetUnits(Expression<Func<UnitEntity, bool>> filter)
    {
      return this.GetUnits(new QuerySpecification<UnitEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Unit))]
    public IReadOnlyList<Unit> GetUnits(params IQueryModifier<UnitEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Unit>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    #region Universe Methods
    /// <inheritdoc />
    public Universe GetUniverseById(UniverseId id)
    {
      Universe result;
      if (Eve.General.Cache.TryGetValue<Universe>(id, out result))
      {
        return result;
      }

      return this.GetUniverses(x => x.Id == id.Value).Single();
    }

    /// <inheritdoc />
    public IReadOnlyList<Universe> GetUniverses(Expression<Func<UniverseEntity, bool>> filter)
    {
      return this.GetUniverses(new QuerySpecification<UniverseEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Universe))]
    public IReadOnlyList<Universe> GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => Eve.General.Cache.GetOrAdd<Universe>(x.Id, () => x.ToAdapter()))
                               .ToArray();
    }
    #endregion

    /// <summary>
    /// Returns an <see cref="IQueryable{T}" /> for the given entity type,
    /// filtered according to the specified modifiers.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to query.
    /// </typeparam>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IQueryable{T}" /> for the given entity type, with the
    /// specified modifiers applied.
    /// </returns>
    internal IQueryable<TEntity> GetQuery<TEntity>(params IQueryModifier<TEntity>[] modifiers)
      where TEntity : Entity
    {
      var context = this.ContextFactory();

      var query = context.Set<TEntity>().AsNoTracking();
      Contract.Assume(query != null);

      // Apply the modifiers
      foreach (IQueryModifier<TEntity> modifier in modifiers)
      {
        Contract.Assume(modifier != null);
        query = modifier.GetResults(query);
      }

      return query;
    }

    /// <summary>
    /// Returns the results of a query against the given entity type,
    /// filtered according to the specified modifiers.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to query.
    /// </typeparam>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of
    /// the query.
    /// </returns>
    internal IReadOnlyList<TEntity> GetList<TEntity>(params IQueryModifier<TEntity>[] modifiers)
      where TEntity : Entity
    {
      var context = this.ContextFactory();
      var query = context.Set<TEntity>().AsNoTracking();
      Contract.Assume(query != null);

      // Apply the modifiers
      foreach (IQueryModifier<TEntity> modifier in modifiers)
      {
        Contract.Assume(modifier != null);
        query = modifier.GetResults(query);
      }

      return query.ToList();
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.contextFactory != null);
    }
  }
}