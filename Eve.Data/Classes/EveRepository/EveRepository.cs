//-----------------------------------------------------------------------
// <copyright file="EveRepository.cs" company="Jeremy H. Todd">
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
  using Eve.Industry;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// An EveRepository that uses an automatically-generated
  /// <see cref="EveDbContext" /> object to query the database.
  /// </summary>
  public class EveRepository : IEveRepository
  {
    private EveCache cache;
    private IEveDbContext context;
    private object queryLock;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveRepository class, using the default
    /// <see cref="EveDbContext" /> to provide access to the database.
    /// </summary>
    public EveRepository() : this(null, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EveRepository class, using the specified
    /// <see cref="EveDbContext" /> to provide access to the database, and the
    /// specified <see cref="EveCache"/> to store local data.
    /// </summary>
    /// <param name="context">
    /// The <see cref="EveDbContext" /> used to provide access to the database,
    /// or <see langword="null" /> to use the default context.
    /// </param>
    /// <param name="cache">
    /// The <see cref="EveCache" /> used to store data locally, or 
    /// <see langword="null" /> to use a newly-created cache with default
    /// settings.
    /// </param>
    public EveRepository(IEveDbContext context, EveCache cache)
    {
      if (context == null)
      {
        context = EveDbContext.Default;
      }

      if (cache == null)
      {
        cache = new EveCache();
      }

      this.cache = cache;
      this.context = context;
      this.queryLock = new object();

      this.PrepopulateCache();
    }

    /* Properties */

    /// <summary>
    /// Gets the <see cref="EveDbContext" /> used to provide
    /// access to the database.
    /// </summary>
    /// <value>
    /// An <see cref="EveDbContext" /> that can be used to provide
    /// access to the database.
    /// </value>
    public IEveDbContext Context
    {
      get
      {
        Contract.Ensures(Contract.Result<IEveDbContext>() != null);
        return this.context;
      }
    }

    /// <inheritdoc />
    public EveCache Cache
    {
      get
      {
        Contract.Ensures(Contract.Result<EveCache>() != null);
        return this.cache;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public void Dispose()
    {
      this.Dispose(true);
    }

    #region Activity Methods
    /// <inheritdoc />
    public Activity GetActivityById(ActivityId id)
    {
      Activity result;
      if (this.Cache.TryGetValue<Activity>(id, out result))
      {
        return result;
      }

      var query = this.GetActivities(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Activity> GetActivities(Expression<Func<ActivityEntity, bool>> filter)
    {
      return this.GetActivities(new QuerySpecification<ActivityEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Activity))]
    public IReadOnlyList<Activity> GetActivities(params IQueryModifier<ActivityEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Activity>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Agent Methods
    /// <inheritdoc />
    public Agent GetAgentById(AgentId id)
    {
      Agent result;
      if (this.Cache.TryGetValue<Agent>(id, out result))
      {
        return result;
      }

      var query = this.GetAgents(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);
      
      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Agent>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region AgentType Methods
    /// <inheritdoc />
    public AgentType GetAgentTypeById(AgentTypeId id)
    {
      AgentType result;
      if (this.Cache.TryGetValue<AgentType>(id, out result))
      {
        return result;
      }

      var query = this.GetAgentTypes(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<AgentType>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Ancestry Methods
    /// <inheritdoc />
    public Ancestry GetAncestryById(AncestryId id)
    {
      Ancestry result;
      if (this.Cache.TryGetValue<Ancestry>(id, out result))
      {
        return result;
      }

      var query = this.GetAncestries(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Ancestry> GetAncestries(Expression<Func<AncestryEntity, bool>> filter)
    {
      return this.GetAncestries(new QuerySpecification<AncestryEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Ancestry))]
    public IReadOnlyList<Ancestry> GetAncestries(params IQueryModifier<AncestryEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Ancestry>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region AttributeCategory Methods
    /// <inheritdoc />
    public AttributeCategory GetAttributeCategoryById(AttributeCategoryId id)
    {
      AttributeCategory result;
      if (this.Cache.TryGetValue<AttributeCategory>(id, out result))
      {
        return result;
      }

      var query = this.GetAttributeCategories(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<AttributeCategory>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region AttributeType Methods
    /// <inheritdoc />
    public AttributeType GetAttributeTypeById(AttributeId id)
    {
      AttributeType result;
      if (this.Cache.TryGetValue<AttributeType>(id, out result))
      {
        return result;
      }

      var query = this.GetAttributeTypes(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<AttributeType>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region AttributeValue Methods
    /// <inheritdoc />
    public AttributeValue GetAttributeValueById(TypeId itemTypeId, AttributeId id)
    {
      AttributeValue result;
      if (this.Cache.TryGetValue<AttributeValue>(id, out result))
      {
        return result;
      }

      var query = this.GetAttributeValues(x => x.ItemTypeId == itemTypeId && x.AttributeId == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => x.ToAdapter(this)).ToArray();
    }
    #endregion

    #region Bloodline Methods
    /// <inheritdoc />
    public Bloodline GetBloodlineById(BloodlineId id)
    {
      Bloodline result;
      if (this.Cache.TryGetValue<Bloodline>(id, out result))
      {
        return result;
      }

      var query = this.GetBloodlines(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Bloodline> GetBloodlines(Expression<Func<BloodlineEntity, bool>> filter)
    {
      return this.GetBloodlines(new QuerySpecification<BloodlineEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Bloodline))]
    public IReadOnlyList<Bloodline> GetBloodlines(params IQueryModifier<BloodlineEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Bloodline>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Category Methods
    /// <inheritdoc />
    public Category GetCategoryById(CategoryId id)
    {
      Category result;
      if (this.Cache.TryGetValue<Category>(id, out result))
      {
        return result;
      }

      var query = this.GetCategories(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Category>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region CharacterAttributeType Methods
    /// <inheritdoc />
    public CharacterAttributeType GetCharacterAttributeTypeById(CharacterAttributeId id)
    {
      CharacterAttributeType result;
      if (this.Cache.TryGetValue<CharacterAttributeType>(id, out result))
      {
        return result;
      }

      var query = this.GetCharacterAttributeTypes(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<CharacterAttributeType>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Constellation Methods
    /// <inheritdoc />
    public Constellation GetConstellationById(ConstellationId id)
    {
      Constellation result;
      if (this.Cache.TryGetValue<Constellation>(id, out result))
      {
        return result;
      }

      var query = this.GetConstellations(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Constellation>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region ConstellationJump Methods
    /// <inheritdoc />
    public ConstellationJump GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId)
    {
      ConstellationJump result;
      if (this.Cache.TryGetValue<ConstellationJump>(ConstellationJump.CreateCompoundId(fromConstellationId, toConstellationId), out result))
      {
        return result;
      }

      var query = this.GetConstellationJumps(x => x.FromConstellationId == fromConstellationId.Value && x.ToConstellationId == toConstellationId.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
        return this.Cache.GetOrAdd<ConstellationJump>(cacheId, () => x.ToAdapter(this));
      }).ToArray();
    }
    #endregion

    #region CorporateActivity Methods
    /// <inheritdoc />
    public CorporateActivity GetCorporateActivityById(CorporateActivityId id)
    {
      CorporateActivity result;
      if (this.Cache.TryGetValue<CorporateActivity>(id, out result))
      {
        return result;
      }

      var query = this.GetCorporateActivities(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<CorporateActivity>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Division Methods
    /// <inheritdoc />
    public Division GetDivisionById(DivisionId id)
    {
      Division result;
      if (this.Cache.TryGetValue<Division>(id, out result))
      {
        return result;
      }

      var query = this.GetDivisions(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Division>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Effect Methods
    /// <inheritdoc />
    public Effect GetEffectById(TypeId itemTypeId, EffectId id)
    {
      Effect result;
      if (this.Cache.TryGetValue<Effect>(id, out result))
      {
        return result;
      }

      var query = this.GetEffects(x => x.ItemTypeId == itemTypeId && x.EffectId == (short)id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => x.ToAdapter(this)).ToArray();
    }
    #endregion

    #region EffectType Methods
    /// <inheritdoc />
    public EffectType GetEffectTypeById(EffectId id)
    {
      EffectType result;
      if (this.Cache.TryGetValue<EffectType>(id, out result))
      {
        return result;
      }

      var query = this.GetEffectTypes(x => x.Id == (short)id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<EffectType>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region EveType Methods
    /// <inheritdoc />
    public EveType GetEveTypeById(TypeId id)
    {
      EveType result;

      if (this.Cache.TryGetValue<EveType>(id, out result))
      {
        return result;
      }

      var query = this.GetEveTypes(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<EveType> GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter)
    {
      return this.GetEveTypes(new QuerySpecification<EveTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(EveType))]
    public IReadOnlyList<EveType> GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<EveType>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }

    /// <inheritdoc />
    public TEveType GetEveTypeById<TEveType>(TypeId id) where TEveType : EveType
    {
      TEveType result;
      if (this.Cache.TryGetValue<TEveType>(id, out result))
      {
        return result;
      }

      var query = this.GetEveTypes<EveType>(x => x.Id == id.Value).Cast<TEveType>();
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<EveType>(x.Id, () => x.ToAdapter(this)))
                               .OfType<TEveType>()
                               .ToArray();
    }
    #endregion

    #region Faction Methods
    /// <inheritdoc />
    public Faction GetFactionById(FactionId id)
    {
      Faction result;
      if (this.Cache.TryGetValue<Faction>(id, out result))
      {
        return result;
      }

      var query = this.GetFactions(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Faction>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Graphic Methods
    /// <inheritdoc />
    public Graphic GetGraphicById(GraphicId id)
    {
      Graphic result;
      if (this.Cache.TryGetValue<Graphic>(id, out result))
      {
        return result;
      }

      var query = this.GetGraphics(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Graphic>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Group Methods
    /// <inheritdoc />
    public Group GetGroupById(GroupId id)
    {
      Group result;
      if (this.Cache.TryGetValue<Group>(id, out result))
      {
        return result;
      }

      var query = this.GetGroups(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Group>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Icon Methods
    /// <inheritdoc />
    public Icon GetIconById(IconId id)
    {
      Icon result;
      if (this.Cache.TryGetValue<Icon>(id, out result))
      {
        return result;
      }

      var query = this.GetIcons(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Icon>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Item Methods
    /// <inheritdoc />
    public Item GetItemById(ItemId id)
    {
      Item result;
      if (this.Cache.TryGetValue<Item>(id, out result))
      {
        return result;
      }

      var query = this.GetItems(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Item> GetItems(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetItems(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Item))]
    public IReadOnlyList<Item> GetItems(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Item>(x.Id, () => x.ToAdapter(this)))
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Item>(x.Id, () => x.ToAdapter(this)))
                               .OfType<TItem>()
                               .ToArray();
    }
    #endregion

    #region MarketGroup Methods
    /// <inheritdoc />
    public MarketGroup GetMarketGroupById(MarketGroupId id)
    {
      MarketGroup result;
      if (this.Cache.TryGetValue<MarketGroup>(id, out result))
      {
        return result;
      }

      var query = this.GetMarketGroups(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<MarketGroup>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region MetaGroup Methods
    /// <inheritdoc />
    public MetaGroup GetMetaGroupById(MetaGroupId id)
    {
      MetaGroup result;
      if (this.Cache.TryGetValue<MetaGroup>(id, out result))
      {
        return result;
      }

      var query = this.GetMetaGroups(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<MetaGroup>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region MetaType Methods
    /// <inheritdoc />
    public MetaType GetMetaTypeById(TypeId id)
    {
      MetaType result;
      if (this.Cache.TryGetValue<MetaType>(id, out result))
      {
        return result;
      }

      var query = this.GetMetaTypes(x => x.TypeId == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<MetaType>(x.TypeId, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region NpcCorporation Methods
    /// <inheritdoc />
    public NpcCorporation GetNpcCorporationById(NpcCorporationId id)
    {
      NpcCorporation result;
      if (this.Cache.TryGetValue<NpcCorporation>(id, out result))
      {
        return result;
      }

      var query = this.GetNpcCorporations(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<NpcCorporation>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region NpcCorporationDivision Methods
    /// <inheritdoc />
    public NpcCorporationDivision GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId)
    {
      NpcCorporationDivision result;
      if (this.Cache.TryGetValue<NpcCorporationDivision>(NpcCorporationDivision.CreateCompoundId(corporationId, divisionId), out result))
      {
        return result;
      }

      var query = this.GetNpcCorporationDivisions(x => x.CorporationId == corporationId.Value && x.DivisionId == divisionId);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
        return this.Cache.GetOrAdd<NpcCorporationDivision>(cacheId, () => x.ToAdapter(this));
      }).ToArray();
    }
    #endregion

    #region Race Methods
    /// <inheritdoc />
    public Race GetRaceById(RaceId id)
    {
      Race result;
      if (this.Cache.TryGetValue<Race>(id, out result))
      {
        return result;
      }

      var query = this.GetRaces(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Race>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Region Methods
    /// <inheritdoc />
    public Region GetRegionById(RegionId id)
    {
      Region result;
      if (this.Cache.TryGetValue<Region>(id, out result))
      {
        return result;
      }

      var query = this.GetRegions(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Region>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region RegionJump Methods
    /// <inheritdoc />
    public RegionJump GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId)
    {
      RegionJump result;
      if (this.Cache.TryGetValue<RegionJump>(RegionJump.CreateCompoundId(fromRegionId, toRegionId), out result))
      {
        return result;
      }

      var query = this.GetRegionJumps(x => x.FromRegionId == fromRegionId && x.ToRegionId == toRegionId);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
        return this.Cache.GetOrAdd<RegionJump>(cacheId, () => x.ToAdapter(this));
      }).ToArray();
    }
    #endregion

    #region SolarSystem Methods
    /// <inheritdoc />
    public SolarSystem GetSolarSystemById(SolarSystemId id)
    {
      SolarSystem result;
      if (this.Cache.TryGetValue<SolarSystem>(id, out result))
      {
        return result;
      }

      var query = this.GetSolarSystems(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<SolarSystem>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region SolarSystemJump Methods
    /// <inheritdoc />
    public SolarSystemJump GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId)
    {
      SolarSystemJump result;
      if (this.Cache.TryGetValue<SolarSystemJump>(SolarSystemJump.CreateCompoundId(fromSolarSystemId, toSolarSystemId), out result))
      {
        return result;
      }

      var query = this.GetSolarSystemJumps(x => x.FromSolarSystemId == fromSolarSystemId.Value && x.ToSolarSystemId == toSolarSystemId.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
        return this.Cache.GetOrAdd<SolarSystemJump>(cacheId, () => x.ToAdapter(this));
      }).ToArray();
    }
    #endregion

    #region Station Methods
    /// <inheritdoc />
    public Station GetStationById(StationId id)
    {
      Station result;
      if (this.Cache.TryGetValue<Station>(id, out result))
      {
        return result;
      }

      var query = this.GetStations(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Station> GetStations(Expression<Func<StationEntity, bool>> filter)
    {
      return this.GetStations(new QuerySpecification<StationEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Station))]
    public IReadOnlyList<Station> GetStations(params IQueryModifier<StationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Station>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region StationOperation Methods
    /// <inheritdoc />
    public StationOperation GetStationOperationById(StationOperationId id)
    {
      StationOperation result;
      if (this.Cache.TryGetValue<StationOperation>(id, out result))
      {
        return result;
      }

      var query = this.GetStationOperations(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<StationOperation>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region StationService Methods
    /// <inheritdoc />
    public StationService GetStationServiceById(StationServiceId id)
    {
      StationService result;
      if (this.Cache.TryGetValue<StationService>(id, out result))
      {
        return result;
      }

      var query = this.GetStationServices(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<StationService>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region StationType Methods
    /// <inheritdoc />
    public StationType GetStationTypeById(TypeId id)
    {
      StationType result;
      if (this.Cache.TryGetValue<StationType>(id, out result))
      {
        return result;
      }

      var query = this.GetStationTypes(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<StationType>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Unit Methods
    /// <inheritdoc />
    public Unit GetUnitById(UnitId id)
    {
      Unit result;
      if (this.Cache.TryGetValue<Unit>(id, out result))
      {
        return result;
      }

      var query = this.GetUnits(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Unit>(x.Id, () => x.ToAdapter(this)))
                               .ToArray();
    }
    #endregion

    #region Universe Methods
    /// <inheritdoc />
    public Universe GetUniverseById(UniverseId id)
    {
      Universe result;
      if (this.Cache.TryGetValue<Universe>(id, out result))
      {
        return result;
      }

      var query = this.GetUniverses(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
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
      return GetList(modifiers).Select(x => this.Cache.GetOrAdd<Universe>(x.Id, () => x.ToAdapter(this)))
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
    internal IQueryable<TEntity> GetQuery<TEntity>(params IQueryModifier<TEntity>[] modifiers) where TEntity : Entity
    {
      Contract.Requires(modifiers != null, "The array of query modifiers cannot be null.");
      Contract.Ensures(Contract.Result<IQueryable<TEntity>>() != null);

      lock (this.queryLock)
      {
        var query = this.Context.Query<TEntity>();

        // Apply the modifiers
        foreach (IQueryModifier<TEntity> modifier in modifiers)
        {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        return query;
      }
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
    internal IReadOnlyList<TEntity> GetList<TEntity>(params IQueryModifier<TEntity>[] modifiers) where TEntity : Entity
    {
      Contract.Requires(modifiers != null, "The array of query modifiers cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TEntity>>() != null);

      lock (this.queryLock)
      {
        var query = this.Context.Query<TEntity>();

        // Apply the modifiers
        foreach (IQueryModifier<TEntity> modifier in modifiers)
        {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        return query.ToList();
      }
    }

    /// <summary>
    /// Disposes the current object.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether to dispose managed resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.cache.Dispose();
        this.context.Dispose();
      }
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.cache != null);
      Contract.Invariant(this.context != null);
    }

    /// <summary>
    /// Loads and caches the set of data that is necessary for future queries
    /// to be performed.
    /// </summary>
    private void PrepopulateCache()
    {
      // Permanently add all published Categories
      foreach (Category category in this.Context.Categories.Where(x => x.Published == true).ToList().Select(x => x.ToAdapter(this)))
      {
        Contract.Assume(category != null);
        this.Cache.AddOrReplace<Category>(category, true);
      }

      // Permanently add all Groups -- necessary for EveType.Create() to load successfully
      foreach (Group group in this.Context.Groups.Where(x => x.Published == true).ToList().Select(x => x.ToAdapter(this)))
      {
        Contract.Assume(group != null);
        this.Cache.AddOrReplace<Group>(group, true);
      }

      // Permanently add all units
      foreach (Unit unit in this.Context.Units.ToList().Select(x => x.ToAdapter(this)))
      {
        Contract.Assume(unit != null);
        this.Cache.AddOrReplace<Unit>(unit, true);
      }
    }
  }
}