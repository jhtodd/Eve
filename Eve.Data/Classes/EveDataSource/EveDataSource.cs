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
  using FreeNet.Utilities;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Meta;
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

    #region Instance Fields
    private EveDbContext _context;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDataSource class, using the default
    /// <see cref="EveDbContext" /> to provide access to the database.
    /// </summary>
    public EveDataSource() : this(EveDbContext.Default) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDataSource class, using the specified
    /// <see cref="EveDbContext" /> to provide access to the database.
    /// </summary>
    /// 
    /// <param name="context">
    /// The <see cref="EveDbContext" /> used to provide access to the database.
    /// </param>
    public EveDataSource(EveDbContext context) {
      Contract.Requires(context != null, Resources.Messages.IEveDataSource_ContextCannotBeNull);
      _context = context;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_context != null);
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual void PrepopulateCache(EveCache cache) {

      lock (Context) {

        // Permanently add all published Categories
        foreach (Category category in Context.Categories.Where(x => x.Published == true).ToList().Select(x => new Category(x))) {
          Contract.Assume(category != null);
          cache.AddOrReplace<Category>(category, true);
        }

        // Permanently add all Groups -- necessary for EveType.Create() to load successfully
        foreach (Group group in Context.Groups.Where(x => x.Published == true).ToList().Select(x => new Group(x))) {
          Contract.Assume(group != null);
          cache.AddOrReplace<Group>(group, true);
        }

        // Permanently add all units
        foreach (Unit unit in Context.Units.ToList().Select(x => new Unit(x))) {
          Contract.Assume(unit != null);
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
        return _context;
      }
    }
    #endregion

    #region Agent Methods
    //******************************************************************************
    /// <inheritdoc />
    public Agent GetAgentById(AgentId id) {
      Agent result;
      if (Eve.General.Cache.TryGetValue<Agent>(id, out result)) {
        return result;
      }

      return GetAgents(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Agent> GetAgents(Expression<Func<AgentEntity, bool>> filter) {
      return GetAgents(new QuerySpecification<AgentEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(Agent))]
    public IReadOnlyList<Agent> GetAgents(params IQueryModifier<AgentEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<AgentEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<AgentEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Agent>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Agent>(x.Id, () => new Agent(x));
        }));
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
      [EveQueryMethod(typeof(AgentType))]
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
          return new ReadOnlyList<AgentType>(query.ToList().Select(x => {
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
    [EveQueryMethod(typeof(AttributeCategory))]
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
        return new ReadOnlyList<AttributeCategory>(query.ToList().Select(x => {
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
    [EveQueryMethod(typeof(AttributeType))]
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
        return new ReadOnlyList<AttributeType>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<AttributeType>(x.Id, () => new AttributeType(x));
        }));
      }
    }
    #endregion

    #region AttributeValue Methods
    //******************************************************************************
    /// <inheritdoc />
    public AttributeValue GetAttributeValueById(TypeId itemTypeId, AttributeId id) {
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
    [EveQueryMethod(typeof(AttributeValue))]
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
        // relevance to a particular EveType, and that entire EveType will be 
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
    [EveQueryMethod(typeof(Category))]
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
        return new ReadOnlyList<Category>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Category>(x.Id, () => new Category(x));
        }));
      }
    }
    #endregion

    #region CharacterAttributeType Methods
    //******************************************************************************
    /// <inheritdoc />
    public CharacterAttributeType GetCharacterAttributeTypeById(CharacterAttributeId id) {
      CharacterAttributeType result;
      if (Eve.General.Cache.TryGetValue<CharacterAttributeType>(id, out result)) {
        return result;
      }

      return GetCharacterAttributeTypes(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(Expression<Func<CharacterAttributeTypeEntity, bool>> filter) {
      return GetCharacterAttributeTypes(new QuerySpecification<CharacterAttributeTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(CharacterAttributeType))]
    public IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<CharacterAttributeTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<CharacterAttributeTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<CharacterAttributeType>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<CharacterAttributeType>(x.Id, () => new CharacterAttributeType(x));
        }));
      }
    }
    #endregion

    #region Constellation Methods
    //******************************************************************************
    /// <inheritdoc />
    public Constellation GetConstellationById(ConstellationId id) {
      Constellation result;
      if (Eve.General.Cache.TryGetValue<Constellation>(id, out result)) {
        return result;
      }

      return GetConstellations(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Constellation> GetConstellations(Expression<Func<ConstellationEntity, bool>> filter) {
      return GetConstellations(new QuerySpecification<ConstellationEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(Constellation))]
    public IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<ConstellationEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<ConstellationEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<Constellation>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Constellation>(x.Id, () => new Constellation(x));
        }));
      }
    }
    #endregion

    #region ConstellationJump Methods
    //******************************************************************************
    /// <inheritdoc />
    public ConstellationJump GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId) {
      ConstellationJump result;
      if (Eve.General.Cache.TryGetValue<ConstellationJump>(ConstellationJump.CreateCompoundId(fromConstellationId, toConstellationId), out result)) {
        return result;
      }

      return GetConstellationJumps(x => x.FromConstellationId == fromConstellationId.Value && x.ToConstellationId == toConstellationId.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<ConstellationJump> GetConstellationJumps(Expression<Func<ConstellationJumpEntity, bool>> filter) {
      return GetConstellationJumps(new QuerySpecification<ConstellationJumpEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(ConstellationJump))]
    public IReadOnlyList<ConstellationJump> GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<ConstellationJumpEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<ConstellationJumpEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<ConstellationJump>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<ConstellationJump>(ConstellationJump.CreateCompoundId(x.FromConstellationId, x.ToConstellationId), () => new ConstellationJump(x));
        }));
      }
    }
    #endregion

    #region CorporateActivity Methods
    //******************************************************************************
    /// <inheritdoc />
    public CorporateActivity GetCorporateActivityById(CorporateActivityId id) {
      CorporateActivity result;
      if (Eve.General.Cache.TryGetValue<CorporateActivity>(id, out result)) {
        return result;
      }

      return GetCorporateActivities(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<CorporateActivity> GetCorporateActivities(Expression<Func<CorporateActivityEntity, bool>> filter) {
      return GetCorporateActivities(new QuerySpecification<CorporateActivityEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(CorporateActivity))]
    public IReadOnlyList<CorporateActivity> GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<CorporateActivityEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<CorporateActivityEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<CorporateActivity>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<CorporateActivity>(x.Id, () => new CorporateActivity(x));
        }));
      }
    }
    #endregion

    #region Division Methods
    //******************************************************************************
    /// <inheritdoc />
    public Division GetDivisionById(DivisionId id) {
      Division result;
      if (Eve.General.Cache.TryGetValue<Division>(id, out result)) {
        return result;
      }

      return GetDivisions(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Division> GetDivisions(Expression<Func<DivisionEntity, bool>> filter) {
      return GetDivisions(new QuerySpecification<DivisionEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(Division))]
    public IReadOnlyList<Division> GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<DivisionEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<DivisionEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Division>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Division>(x.Id, () => new Division(x));
        }));
      }
    }
    #endregion

    #region Effect Methods
    //******************************************************************************
    /// <inheritdoc />
    public Effect GetEffectById(TypeId itemTypeId, EffectId id) {
      Effect result;
      if (Eve.General.Cache.TryGetValue<Effect>(id, out result)) {
        return result;
      }

      return GetEffects(x => x.ItemTypeId == itemTypeId && x.EffectId == (short) id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Effect> GetEffects(Expression<Func<EffectEntity, bool>> filter) {
      return GetEffects(new QuerySpecification<EffectEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(Effect))]
    public IReadOnlyList<Effect> GetEffects(params IQueryModifier<EffectEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<EffectEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<EffectEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Effects are a special case -- don't cache, because they only have
        // relevance to a particular EveType, and that entire EveType will be 
        // cached anyway
        return new ReadOnlyList<Effect>(query.AsEnumerable().Select(x => new Effect(x)));
      }
    }
    #endregion

    #region EffectType Methods
    //******************************************************************************
    /// <inheritdoc />
    public EffectType GetEffectTypeById(EffectId id) {
      EffectType result;
      if (Eve.General.Cache.TryGetValue<EffectType>(id, out result)) {
        return result;
      }

      return GetEffectTypes(x => x.Id == (short) id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<EffectType> GetEffectTypes(Expression<Func<EffectTypeEntity, bool>> filter) {
      return GetEffectTypes(new QuerySpecification<EffectTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(EffectType))]
    public IReadOnlyList<EffectType> GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<EffectTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<EffectTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<EffectType>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<EffectType>(x.Id, () => new EffectType(x));
        }));
      }
    }
    #endregion

    #region EveType Methods
    //******************************************************************************
    /// <inheritdoc />
    public EveType GetEveTypeById(TypeId id) {
      EveType result;

      if (Eve.General.Cache.TryGetValue<EveType>(id, out result)) {
        return result;
      }

      return GetEveTypes(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<EveType> GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter) {
      return GetEveTypes(new QuerySpecification<EveTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<EveType> GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<EveTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<EveTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<EveType>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<EveType>(x.Id, () => EveType.Create(x));
        }));
      }
    }
    //******************************************************************************
    /// <inheritdoc />
    public TEveType GetEveTypeById<TEveType>(TypeId id) where TEveType : EveType {
      TEveType result;
      if (Eve.General.Cache.TryGetValue<TEveType>(id, out result)) {
        return result;
      }

      return GetEveTypes<EveType>(x => x.Id == id.Value).Cast<TEveType>().Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<TEveType> GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter) where TEveType : EveType {
      return GetEveTypes<TEveType>(new QuerySpecification<EveTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<TEveType> GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers) where TEveType : EveType {
      lock (Context) {

        var query = Context.Set<EveTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<EveTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<TEveType>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<EveType>(x.Id, () => EveType.Create(x));
        }).OfType<TEveType>());
      }
    }
    #endregion

    #region Faction Methods
    //******************************************************************************
    /// <inheritdoc />
    public Faction GetFactionById(FactionId id) {
      Faction result;
      if (Eve.General.Cache.TryGetValue<Faction>(id, out result)) {
        return result;
      }

      return GetFactions(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Faction> GetFactions(Expression<Func<FactionEntity, bool>> filter) {
      return GetFactions(new QuerySpecification<FactionEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(Faction))]
    public IReadOnlyList<Faction> GetFactions(params IQueryModifier<FactionEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<FactionEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<FactionEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<Faction>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Faction>(x.Id, () => new Faction(x));
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
    [EveQueryMethod(typeof(Group))]
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
        return new ReadOnlyList<Group>(query.ToList().Select(x => {
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
    [EveQueryMethod(typeof(Icon))]
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
        return new ReadOnlyList<Icon>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Icon>(x.Id, () => new Icon(x));
        }));
      }
    }
    #endregion

    #region Item Methods
    //******************************************************************************
    /// <inheritdoc />
    public Item GetItemById(ItemId id) {

      // If no other type could be loaded, fall back on a generic item
      lock (Context) {
        var query = Context.Set<ItemEntity>().AsNoTracking();
        Contract.Assume(query != null);

        ItemEntity entity = query.Where(x => x.Id == id.Value).Single();

        return Eve.General.Cache.GetOrAdd<Item>(entity.Id, () => Item.Create(entity));
      }
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Item> GetItems(Expression<Func<ItemEntity, bool>> filter) {
      return GetItems(new QuerySpecification<ItemEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Item> GetItems(params IQueryModifier<ItemEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<ItemEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<ItemEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<Item>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Item>(x.Id, () => Item.Create(x));
        }));
      }
    }
    //******************************************************************************
    /// <inheritdoc />
    public TItem GetItemById<TItem>(ItemId id) where TItem : Item {
      TItem result = GetItemById(id).ConvertTo<TItem>();

      Contract.Assume(result != null);

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter) where TItem : Item {
      return GetItems<TItem>(new QuerySpecification<ItemEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers) where TItem : Item {
      lock (Context) {

        var query = Context.Set<ItemEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<ItemEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<TItem>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Item>(x.Id, () => Item.Create(x));
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
    [EveQueryMethod(typeof(MarketGroup))]
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
        return new ReadOnlyList<MarketGroup>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<MarketGroup>(x.Id, () => new MarketGroup(x));
        }));
      }
    }
    #endregion

    #region MetaGroup Methods
    //******************************************************************************
    /// <inheritdoc />
    public MetaGroup GetMetaGroupById(MetaGroupId id) {
      MetaGroup result;
      if (Eve.General.Cache.TryGetValue<MetaGroup>(id, out result)) {
        return result;
      }

      return GetMetaGroups(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaGroup" /> objects.
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
    public IReadOnlyList<MetaGroup> GetMetaGroups(Expression<Func<MetaGroupEntity, bool>> filter) {
      return GetMetaGroups(new QuerySpecification<MetaGroupEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(MetaGroup))]
    public IReadOnlyList<MetaGroup> GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<MetaGroupEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<MetaGroupEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<MetaGroup>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<MetaGroup>(x.Id, () => new MetaGroup(x));
        }));
      }
    }
    #endregion

    #region MetaType Methods
    //******************************************************************************
    /// <inheritdoc />
    public MetaType GetMetaTypeById(TypeId id) {
      MetaType result;
      if (Eve.General.Cache.TryGetValue<MetaType>(id, out result)) {
        return result;
      }

      return GetMetaTypes(x => x.TypeId == id).Single();
    }
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaType" /> objects.
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
    public IReadOnlyList<MetaType> GetMetaTypes(Expression<Func<MetaTypeEntity, bool>> filter) {
      return GetMetaTypes(new QuerySpecification<MetaTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(MetaType))]
    public IReadOnlyList<MetaType> GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<MetaTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<MetaTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<MetaType>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<MetaType>(x.TypeId, () => new MetaType(x));
        }));
      }
    }
    #endregion

    #region NpcCorporation Methods
    //******************************************************************************
    /// <inheritdoc />
    public NpcCorporation GetNpcCorporationById(NpcCorporationId id) {
      NpcCorporation result;
      if (Eve.General.Cache.TryGetValue<NpcCorporation>(id, out result)) {
        return result;
      }

      return GetNpcCorporations(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(Expression<Func<NpcCorporationEntity, bool>> filter) {
      return GetNpcCorporations(new QuerySpecification<NpcCorporationEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(NpcCorporation))]
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<NpcCorporationEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<NpcCorporationEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<NpcCorporation>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<NpcCorporation>(x.Id, () => new NpcCorporation(x));
        }));
      }
    }
    #endregion

    #region NpcCorporationDivision Methods
    //******************************************************************************
    /// <inheritdoc />
    public NpcCorporationDivision GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId) {
      NpcCorporationDivision result;
      if (Eve.General.Cache.TryGetValue<NpcCorporationDivision>(NpcCorporationDivision.CreateCompoundId(corporationId, divisionId), out result)) {
        return result;
      }

      return GetNpcCorporationDivisions(x => x.CorporationId == corporationId.Value && x.DivisionId == divisionId).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(Expression<Func<NpcCorporationDivisionEntity, bool>> filter) {
      return GetNpcCorporationDivisions(new QuerySpecification<NpcCorporationDivisionEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(NpcCorporationDivision))]
    public IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<NpcCorporationDivisionEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<NpcCorporationDivisionEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<NpcCorporationDivision>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<NpcCorporationDivision>(NpcCorporationDivision.CreateCompoundId(x.CorporationId, x.DivisionId), () => new NpcCorporationDivision(x));
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
    [EveQueryMethod(typeof(Race))]
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
        return new ReadOnlyList<Race>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Race>(x.Id, () => new Race(x));
        }));
      }
    }
    #endregion

    #region Region Methods
    //******************************************************************************
    /// <inheritdoc />
    public Region GetRegionById(RegionId id) {
      Region result;
      if (Eve.General.Cache.TryGetValue<Region>(id, out result)) {
        return result;
      }

      return GetRegions(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Region> GetRegions(Expression<Func<RegionEntity, bool>> filter) {
      return GetRegions(new QuerySpecification<RegionEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(Region))]
    public IReadOnlyList<Region> GetRegions(params IQueryModifier<RegionEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<RegionEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<RegionEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<Region>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Region>(x.Id, () => new Region(x));
        }));
      }
    }
    #endregion

    #region RegionJump Methods
    //******************************************************************************
    /// <inheritdoc />
    public RegionJump GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId) {
      RegionJump result;
      if (Eve.General.Cache.TryGetValue<RegionJump>(RegionJump.CreateCompoundId(fromRegionId, toRegionId), out result)) {
        return result;
      }

      return GetRegionJumps(x => x.FromRegionId == fromRegionId && x.ToRegionId == toRegionId).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<RegionJump> GetRegionJumps(Expression<Func<RegionJumpEntity, bool>> filter) {
      return GetRegionJumps(new QuerySpecification<RegionJumpEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(RegionJump))]
    public IReadOnlyList<RegionJump> GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<RegionJumpEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<RegionJumpEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<RegionJump>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<RegionJump>(RegionJump.CreateCompoundId(x.FromRegionId, x.ToRegionId), () => new RegionJump(x));
        }));
      }
    }
    #endregion

    #region SolarSystem Methods
    //******************************************************************************
    /// <inheritdoc />
    public SolarSystem GetSolarSystemById(SolarSystemId id) {
      SolarSystem result;
      if (Eve.General.Cache.TryGetValue<SolarSystem>(id, out result)) {
        return result;
      }

      return GetSolarSystems(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<SolarSystem> GetSolarSystems(Expression<Func<SolarSystemEntity, bool>> filter) {
      return GetSolarSystems(new QuerySpecification<SolarSystemEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(SolarSystem))]
    public IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<SolarSystemEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<SolarSystemEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<SolarSystem>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<SolarSystem>(x.Id, () => new SolarSystem(x));
        }));
      }
    }
    #endregion

    #region SolarSystemJump Methods
    //******************************************************************************
    /// <inheritdoc />
    public SolarSystemJump GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId) {
      SolarSystemJump result;
      if (Eve.General.Cache.TryGetValue<SolarSystemJump>(SolarSystemJump.CreateCompoundId(fromSolarSystemId, toSolarSystemId), out result)) {
        return result;
      }

      return GetSolarSystemJumps(x => x.FromSolarSystemId == fromSolarSystemId.Value && x.ToSolarSystemId == toSolarSystemId.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(Expression<Func<SolarSystemJumpEntity, bool>> filter) {
      return GetSolarSystemJumps(new QuerySpecification<SolarSystemJumpEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(SolarSystemJump))]
    public IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<SolarSystemJumpEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<SolarSystemJumpEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<SolarSystemJump>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<SolarSystemJump>(SolarSystemJump.CreateCompoundId(x.FromSolarSystemId, x.ToSolarSystemId), () => new SolarSystemJump(x));
        }));
      }
    }
    #endregion

    #region StationOperation Methods
    //******************************************************************************
    /// <inheritdoc />
    public StationOperation GetStationOperationById(StationOperationId id) {
      StationOperation result;
      if (Eve.General.Cache.TryGetValue<StationOperation>(id, out result)) {
        return result;
      }

      return GetStationOperations(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<StationOperation> GetStationOperations(Expression<Func<StationOperationEntity, bool>> filter) {
      return GetStationOperations(new QuerySpecification<StationOperationEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(StationOperation))]
    public IReadOnlyList<StationOperation> GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<StationOperationEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<StationOperationEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<StationOperation>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<StationOperation>(x.Id, () => new StationOperation(x));
        }));
      }
    }
    #endregion

    #region StationService Methods
    //******************************************************************************
    /// <inheritdoc />
    public StationService GetStationServiceById(StationServiceId id) {
      StationService result;
      if (Eve.General.Cache.TryGetValue<StationService>(id, out result)) {
        return result;
      }

      return GetStationServices(x => x.Id == id).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<StationService> GetStationServices(Expression<Func<StationServiceEntity, bool>> filter) {
      return GetStationServices(new QuerySpecification<StationServiceEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(StationService))]
    public IReadOnlyList<StationService> GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<StationServiceEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<StationServiceEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<StationService>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<StationService>(x.Id, () => new StationService(x));
        }));
      }
    }
    #endregion

    #region StationType Methods
    //******************************************************************************
    /// <inheritdoc />
    public StationType GetStationTypeById(TypeId id) {
      StationType result;
      if (Eve.General.Cache.TryGetValue<StationType>(id, out result)) {
        return result;
      }

      return GetStationTypes(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<StationType> GetStationTypes(Expression<Func<StationTypeEntity, bool>> filter) {
      return GetStationTypes(new QuerySpecification<StationTypeEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(StationType))]
    public IReadOnlyList<StationType> GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<StationTypeEntity>().AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<StationTypeEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way
        return new ReadOnlyList<StationType>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<StationType>(x.Id, () => (StationType) EveType.Create(x));
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
    [EveQueryMethod(typeof(Unit))]
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
        return new ReadOnlyList<Unit>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Unit>(x.Id, () => new Unit(x));
        }));
      }
    }
    #endregion

    #region Universe Methods
    //******************************************************************************
    /// <inheritdoc />
    public Universe GetUniverseById(UniverseId id) {
      Universe result;
      if (Eve.General.Cache.TryGetValue<Universe>(id, out result)) {
        return result;
      }

      return GetUniverses(x => x.Id == id.Value).Single();
    }
    //******************************************************************************
    /// <inheritdoc />
    public IReadOnlyList<Universe> GetUniverses(Expression<Func<UniverseEntity, bool>> filter) {
      return GetUniverses(new QuerySpecification<UniverseEntity>(filter));
    }
    //******************************************************************************
    /// <inheritdoc />
    [EveQueryMethod(typeof(Universe))]
    public IReadOnlyList<Universe> GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers) {
      lock (Context) {

        var query = Context.Set<UniverseEntity>()
                           .AsNoTracking();
        Contract.Assume(query != null);

        // Apply the modifiers
        foreach (IQueryModifier<UniverseEntity> modifier in modifiers) {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }

        // Construct the result set, filtering items through the global cache along the way

        // Return only those values that are of the desired type
        return new ReadOnlyList<Universe>(query.ToList().Select(x => {
          return Eve.General.Cache.GetOrAdd<Universe>(x.Id, () => new Universe(x));
        }));
      }
    }
    #endregion
  }
}