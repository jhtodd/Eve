//-----------------------------------------------------------------------
// <copyright file="EveRepository.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Industry;
  using Eve.Universe;

  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// An EveRepository that uses an automatically-generated
  /// <see cref="DirectEveDbContext" /> object to query the database.
  /// </summary>
  public partial class EveRepository : IEveRepository
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
    /// <see cref="DirectEveDbContext" /> to provide access to the database, and the
    /// specified <see cref="EveCache"/> to store local data.
    /// </summary>
    /// <param name="context">
    /// The <see cref="IEveDbContext" /> used to provide access to the database,
    /// or <see langword="null" /> to use a newly-created <see cref="EveDbContext" />
    /// with default settings.  The context is considered "owned" by the repository
    /// and will be disposed when the repository is disposed.
    /// </param>
    /// <param name="cache">
    /// The <see cref="EveCache" /> used to store data locally, or 
    /// <see langword="null" /> to use a newly-created cache with default
    /// settings.  The cache is considered "owned" by the repository
    /// and will be disposed when the repository is disposed.
    /// </param>
    public EveRepository(IEveDbContext context, EveCache cache)
    {
      if (context == null)
      {
        context = new EveDbContext();
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
    /// Gets the <see cref="DirectEveDbContext" /> used to provide
    /// access to the database.
    /// </summary>
    /// <value>
    /// An <see cref="DirectEveDbContext" /> that can be used to provide
    /// access to the database.
    /// </value>
    /// <remarks>
    /// <para>
    /// The context is considered "owned" by the repository and will be disposed
    /// when the repository is disposed.
    /// </para>
    /// </remarks>
    protected IEveDbContext Context
    {
      get
      {
        Contract.Ensures(Contract.Result<IEveDbContext>() != null);
        return this.context;
      }
    }

    /// <inheritdoc />
    /// <remarks>
    /// <para>
    /// The cache is considered "owned" by the repository and will be disposed
    /// when the repository is disposed.
    /// </para>
    /// </remarks>
    protected EveCache Cache
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

    /// <inheritdoc />
    public T GetOrAdd<T>(IConvertible id, Func<T> valueFactory) where T : IEveCacheable
    {
      return this.Cache.GetOrAdd<T>(id, valueFactory);
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
      return LoadAndCacheResults<ActivityEntity, Activity>(this.Context.Activities, modifiers, x => x.Id);
    }
    
    /// <inheritdoc />
    public bool TryGetActivityById(ActivityId id, out Activity value)
    {
      if (this.Cache.TryGetValue<Activity>(id, out value))
      {
        return true;
      }

      var query = this.GetActivities(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
    public IReadOnlyList<Agent> GetAgents(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetAgents(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Agent))]
    public IReadOnlyList<Agent> GetAgents(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Agent>(this.Context.Agents, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetAgentById(AgentId id, out Agent value)
    {
      if (this.Cache.TryGetValue<Agent>(id, out value))
      {
        return true;
      }

      var query = this.GetAgents(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<AgentTypeEntity, AgentType>(this.Context.AgentTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetAgentTypeById(AgentTypeId id, out AgentType value)
    {
      if (this.Cache.TryGetValue<AgentType>(id, out value))
      {
        return true;
      }

      var query = this.GetAgentTypes(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<AncestryEntity, Ancestry>(this.Context.Ancestries, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetAncestryById(AncestryId id, out Ancestry value)
    {
      if (this.Cache.TryGetValue<Ancestry>(id, out value))
      {
        return true;
      }

      var query = this.GetAncestries(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region AssemblyLine Methods
    /// <inheritdoc />
    public AssemblyLine GetAssemblyLineById(AssemblyLineId id)
    {
      AssemblyLine result;
      if (this.Cache.TryGetValue<AssemblyLine>(id, out result))
      {
        return result;
      }

      var query = this.GetAssemblyLines(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLine> GetAssemblyLines(Expression<Func<AssemblyLineEntity, bool>> filter)
    {
      return this.GetAssemblyLines(new QuerySpecification<AssemblyLineEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLine))]
    public IReadOnlyList<AssemblyLine> GetAssemblyLines(params IQueryModifier<AssemblyLineEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineEntity, AssemblyLine>(this.Context.AssemblyLines, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineById(AssemblyLineId id, out AssemblyLine value)
    {
      if (this.Cache.TryGetValue<AssemblyLine>(id, out value))
      {
        return true;
      }

      var query = this.GetAssemblyLines(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region AssemblyLineStation Methods
    /// <inheritdoc />
    public AssemblyLineStation GetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId)
    {
      AssemblyLineStation result;
      if (this.Cache.TryGetValue<AssemblyLineStation>(AssemblyLineStation.CreateCacheKey(stationId, assemblyLineTypeId), out result))
      {
        return result;
      }

      var query = this.GetAssemblyLineStations(x => x.StationId == stationId.Value && x.AssemblyLineTypeId == assemblyLineTypeId.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineStation> GetAssemblyLineStations(Expression<Func<AssemblyLineStationEntity, bool>> filter)
    {
      return this.GetAssemblyLineStations(new QuerySpecification<AssemblyLineStationEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineStation))]
    public IReadOnlyList<AssemblyLineStation> GetAssemblyLineStations(params IQueryModifier<AssemblyLineStationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineStationEntity, AssemblyLineStation>(
        this.Context.AssemblyLineStations,
        modifiers,
        x => AssemblyLineStation.CreateCacheKey(x.StationId, x.AssemblyLineTypeId));
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId, out AssemblyLineStation value)
    {
      if (this.Cache.TryGetValue<AssemblyLineStation>(AssemblyLineStation.CreateCacheKey(stationId, assemblyLineTypeId), out value))
      {
        return true;
      }

      var query = this.GetAssemblyLineStations(x => x.StationId == stationId && x.AssemblyLineTypeId == assemblyLineTypeId);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region AssemblyLineType Methods
    /// <inheritdoc />
    public AssemblyLineType GetAssemblyLineTypeById(AssemblyLineTypeId id)
    {
      AssemblyLineType result;
      if (this.Cache.TryGetValue<AssemblyLineType>(id, out result))
      {
        return result;
      }

      var query = this.GetAssemblyLineTypes(x => x.Id == id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineType> GetAssemblyLineTypes(Expression<Func<AssemblyLineTypeEntity, bool>> filter)
    {
      return this.GetAssemblyLineTypes(new QuerySpecification<AssemblyLineTypeEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineType))]
    public IReadOnlyList<AssemblyLineType> GetAssemblyLineTypes(params IQueryModifier<AssemblyLineTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineTypeEntity, AssemblyLineType>(this.Context.AssemblyLineTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineTypeById(AssemblyLineTypeId id, out AssemblyLineType value)
    {
      if (this.Cache.TryGetValue<AssemblyLineType>(id, out value))
      {
        return true;
      }

      var query = this.GetAssemblyLineTypes(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region AssemblyLineTypeCategoryDetail Methods
    /// <inheritdoc />
    public AssemblyLineTypeCategoryDetail GetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId)
    {
      AssemblyLineTypeCategoryDetail result;
      if (this.Cache.TryGetValue<AssemblyLineTypeCategoryDetail>(AssemblyLineTypeCategoryDetail.CreateCacheKey(assemblyLineTypeId, categoryId), out result))
      {
        return result;
      }

      var query = this.GetAssemblyLineTypeCategoryDetails(x => x.AssemblyLineTypeId == assemblyLineTypeId && x.CategoryId == categoryId);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineTypeCategoryDetail> GetAssemblyLineTypeCategoryDetails(Expression<Func<AssemblyLineTypeCategoryDetailEntity, bool>> filter)
    {
      return this.GetAssemblyLineTypeCategoryDetails(new QuerySpecification<AssemblyLineTypeCategoryDetailEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineTypeCategoryDetail))]
    public IReadOnlyList<AssemblyLineTypeCategoryDetail> GetAssemblyLineTypeCategoryDetails(params IQueryModifier<AssemblyLineTypeCategoryDetailEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineTypeCategoryDetailEntity, AssemblyLineTypeCategoryDetail>(
        this.Context.AssemblyLineTypeCategoryDetails,
        modifiers,
        x => AssemblyLineTypeCategoryDetail.CreateCacheKey(x.AssemblyLineTypeId, x.CategoryId));
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId, out AssemblyLineTypeCategoryDetail value)
    {
      if (this.Cache.TryGetValue<AssemblyLineTypeCategoryDetail>(AssemblyLineTypeCategoryDetail.CreateCacheKey(assemblyLineTypeId, categoryId), out value))
      {
        return true;
      }

      var query = this.GetAssemblyLineTypeCategoryDetails(x => x.AssemblyLineTypeId == assemblyLineTypeId && x.CategoryId == categoryId);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region AssemblyLineTypeGroupDetail Methods
    /// <inheritdoc />
    public AssemblyLineTypeGroupDetail GetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId)
    {
      AssemblyLineTypeGroupDetail result;
      if (this.Cache.TryGetValue<AssemblyLineTypeGroupDetail>(AssemblyLineTypeGroupDetail.CreateCacheKey(assemblyLineTypeId, groupId), out result))
      {
        return result;
      }

      var query = this.GetAssemblyLineTypeGroupDetails(x => x.AssemblyLineTypeId == assemblyLineTypeId && x.GroupId == groupId);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineTypeGroupDetail> GetAssemblyLineTypeGroupDetails(Expression<Func<AssemblyLineTypeGroupDetailEntity, bool>> filter)
    {
      return this.GetAssemblyLineTypeGroupDetails(new QuerySpecification<AssemblyLineTypeGroupDetailEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineTypeGroupDetail))]
    public IReadOnlyList<AssemblyLineTypeGroupDetail> GetAssemblyLineTypeGroupDetails(params IQueryModifier<AssemblyLineTypeGroupDetailEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineTypeGroupDetailEntity, AssemblyLineTypeGroupDetail>(
        this.Context.AssemblyLineTypeGroupDetails,
        modifiers, 
        x => AssemblyLineTypeGroupDetail.CreateCacheKey(x.AssemblyLineTypeId, x.GroupId));
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId, out AssemblyLineTypeGroupDetail value)
    {
      if (this.Cache.TryGetValue<AssemblyLineTypeGroupDetail>(AssemblyLineTypeGroupDetail.CreateCacheKey(assemblyLineTypeId, groupId), out value))
      {
        return true;
      }

      var query = this.GetAssemblyLineTypeGroupDetails(x => x.AssemblyLineTypeId == assemblyLineTypeId && x.GroupId == groupId);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<AttributeCategoryEntity, AttributeCategory>(this.Context.AttributeCategories, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetAttributeCategoryById(AttributeCategoryId id, out AttributeCategory value)
    {
      if (this.Cache.TryGetValue<AttributeCategory>(id, out value))
      {
        return true;
      }

      var query = this.GetAttributeCategories(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<AttributeTypeEntity, AttributeType>(this.Context.AttributeTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetAttributeTypeById(AttributeId id, out AttributeType value)
    {
      if (this.Cache.TryGetValue<AttributeType>(id, out value))
      {
        return true;
      }

      var query = this.GetAttributeTypes(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return GetResults(this.Context.AttributeValues, modifiers).Select(x => x.ToAdapter(this)).ToArray();
    }

    /// <inheritdoc />
    public bool TryGetAttributeValueById(TypeId itemTypeId, AttributeId attributeId, out AttributeValue value)
    {
      if (this.Cache.TryGetValue<AttributeValue>(AttributeValue.CreateCacheKey(itemTypeId, attributeId), out value))
      {
        return true;
      }

      var query = this.GetAttributeValues(x => x.ItemTypeId == itemTypeId && x.AttributeId == attributeId);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<BloodlineEntity, Bloodline>(this.Context.Bloodlines, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetBloodlineById(BloodlineId id, out Bloodline value)
    {
      if (this.Cache.TryGetValue<Bloodline>(id, out value))
      {
        return true;
      }

      var query = this.GetBloodlines(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<CategoryEntity, Category>(this.Context.Categories, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetCategoryById(CategoryId id, out Category value)
    {
      if (this.Cache.TryGetValue<Category>(id, out value))
      {
        return true;
      }

      var query = this.GetCategories(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region Celestial Methods
    /// <inheritdoc />
    public Celestial GetCelestialById(CelestialId id)
    {
      Celestial result;
      if (this.Cache.TryGetValue<Celestial>(id, out result))
      {
        return result;
      }

      var query = this.GetCelestials(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Celestial> GetCelestials(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetCelestials(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Celestial))]
    public IReadOnlyList<Celestial> GetCelestials(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Celestial>(this.Context.Celestials, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetCelestialById(CelestialId id, out Celestial value)
    {
      if (this.Cache.TryGetValue<Celestial>(id, out value))
      {
        return true;
      }

      var query = this.GetCelestials(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<CharacterAttributeTypeEntity, CharacterAttributeType>(this.Context.CharacterAttributeTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetCharacterAttributeTypeById(CharacterAttributeId id, out CharacterAttributeType value)
    {
      if (this.Cache.TryGetValue<CharacterAttributeType>(id, out value))
      {
        return true;
      }

      var query = this.GetCharacterAttributeTypes(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
    public IReadOnlyList<Constellation> GetConstellations(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetConstellations(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Constellation))]
    public IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Constellation>(this.Context.Constellations, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetConstellationById(ConstellationId id, out Constellation value)
    {
      if (this.Cache.TryGetValue<Constellation>(id, out value))
      {
        return true;
      }

      var query = this.GetConstellations(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region ConstellationJump Methods
    /// <inheritdoc />
    public ConstellationJump GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId)
    {
      ConstellationJump result;
      if (this.Cache.TryGetValue<ConstellationJump>(ConstellationJump.CreateCacheKey(fromConstellationId, toConstellationId), out result))
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
      return LoadAndCacheResults<ConstellationJumpEntity, ConstellationJump>(
        this.Context.ConstellationJumps, 
        modifiers,
        x => ConstellationJump.CreateCacheKey(x.FromConstellationId, x.ToConstellationId));
    }

    /// <inheritdoc />
    public bool TryGetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId, out ConstellationJump value)
    {
      if (this.Cache.TryGetValue<ConstellationJump>(ConstellationJump.CreateCacheKey(fromConstellationId, toConstellationId), out value))
      {
        return true;
      }

      var query = this.GetConstellationJumps(x => x.FromConstellationId == fromConstellationId && x.ToConstellationId == toConstellationId);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<CorporateActivityEntity, CorporateActivity>(this.Context.CorporateActivities, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetCorporateActivityById(CorporateActivityId id, out CorporateActivity value)
    {
      if (this.Cache.TryGetValue<CorporateActivity>(id, out value))
      {
        return true;
      }

      var query = this.GetCorporateActivities(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<DivisionEntity, Division>(this.Context.Divisions, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetDivisionById(DivisionId id, out Division value)
    {
      if (this.Cache.TryGetValue<Division>(id, out value))
      {
        return true;
      }

      var query = this.GetDivisions(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return GetResults(this.Context.Effects, modifiers).Select(x => x.ToAdapter(this)).ToArray();
    }

    /// <inheritdoc />
    public bool TryGetEffectById(TypeId itemTypeId, EffectId effectId, out Effect value)
    {
      if (this.Cache.TryGetValue<Effect>(Effect.CreateCacheKey(itemTypeId, effectId), out value))
      {
        return true;
      }

      var query = this.GetEffects(x => x.ItemTypeId == itemTypeId.Value && x.EffectId == (short)effectId);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<EffectTypeEntity, EffectType>(this.Context.EffectTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetEffectTypeById(EffectId id, out EffectType value)
    {
      if (this.Cache.TryGetValue<EffectType>(id, out value))
      {
        return true;
      }

      var query = this.GetEffectTypes(x => x.Id == (short)id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<EveTypeEntity, EveType>(this.Context.EveTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetEveTypeById(TypeId id, out EveType value)
    {
      if (this.Cache.TryGetValue<EveType>(id, out value))
      {
        return true;
      }

      var query = this.GetEveTypes(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<EveTypeEntity, EveType, TEveType>(this.Context.EveTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetEveTypeById<TEveType>(TypeId id, out TEveType value) where TEveType : EveType
    {
      if (this.Cache.TryGetValue<TEveType>(id, out value))
      {
        return true;
      }

      var query = this.GetEveTypes<TEveType>(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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

      var query = this.GetFactions(x => x.Id == (long)id);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Faction> GetFactions(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetFactions(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Faction))]
    public IReadOnlyList<Faction> GetFactions(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Faction>(this.Context.Factions, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetFactionById(FactionId id, out Faction value)
    {
      if (this.Cache.TryGetValue<Faction>(id, out value))
      {
        return true;
      }

      var query = this.GetFactions(x => x.Id == (long)id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<GraphicEntity, Graphic>(this.Context.Graphics, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetGraphicById(GraphicId id, out Graphic value)
    {
      if (this.Cache.TryGetValue<Graphic>(id, out value))
      {
        return true;
      }

      var query = this.GetGraphics(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<GroupEntity, Group>(this.Context.Groups, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetGroupById(GroupId id, out Group value)
    {
      if (this.Cache.TryGetValue<Group>(id, out value))
      {
        return true;
      }

      var query = this.GetGroups(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<IconEntity, Icon>(this.Context.Icons, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetIconById(IconId id, out Icon value)
    {
      if (this.Cache.TryGetValue<Icon>(id, out value))
      {
        return true;
      }

      var query = this.GetIcons(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<ItemEntity, Item>(this.Context.Items, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetItemById(ItemId id, out Item value)
    {
      if (this.Cache.TryGetValue<Item>(id, out value))
      {
        return true;
      }

      var query = this.GetItems(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<ItemEntity, Item, TItem>(this.Context.Items, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetItemById<TItem>(ItemId id, out TItem value) where TItem : Item
    {
      if (this.Cache.TryGetValue<TItem>(id, out value))
      {
        return true;
      }

      var query = this.GetItems<TItem>(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region ItemPosition Methods
    /// <inheritdoc />
    public ItemPosition GetItemPositionById(ItemId id)
    {
      ItemPosition result;
      if (this.Cache.TryGetValue<ItemPosition>(id, out result))
      {
        return result;
      }

      var query = this.GetItemPositions(x => x.ItemId == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<ItemPosition> GetItemPositions(Expression<Func<ItemPositionEntity, bool>> filter)
    {
      return this.GetItemPositions(new QuerySpecification<ItemPositionEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(ItemPosition))]
    public IReadOnlyList<ItemPosition> GetItemPositions(params IQueryModifier<ItemPositionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemPositionEntity, ItemPosition>(this.Context.ItemPositions, modifiers, x => x.ItemId);
    }

    /// <inheritdoc />
    public bool TryGetItemPositionById(ItemId id, out ItemPosition value)
    {
      if (this.Cache.TryGetValue<ItemPosition>(id, out value))
      {
        return true;
      }

      var query = this.GetItemPositions(x => x.ItemId == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<MarketGroupEntity, MarketGroup>(this.Context.MarketGroups, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetMarketGroupById(MarketGroupId id, out MarketGroup value)
    {
      if (this.Cache.TryGetValue<MarketGroup>(id, out value))
      {
        return true;
      }

      var query = this.GetMarketGroups(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<MetaGroupEntity, MetaGroup>(this.Context.MetaGroups, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetMetaGroupById(MetaGroupId id, out MetaGroup value)
    {
      if (this.Cache.TryGetValue<MetaGroup>(id, out value))
      {
        return true;
      }

      var query = this.GetMetaGroups(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<MetaTypeEntity, MetaType>(this.Context.MetaTypes, modifiers, x => x.TypeId);
    }

    /// <inheritdoc />
    public bool TryGetMetaTypeById(TypeId id, out MetaType value)
    {
      if (this.Cache.TryGetValue<MetaType>(id, out value))
      {
        return true;
      }

      var query = this.GetMetaTypes(x => x.TypeId == id);
      value = query.SingleOrDefault();

      return value != null;
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
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetNpcCorporations(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(NpcCorporation))]
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, NpcCorporation>(this.Context.NpcCorporations, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetNpcCorporationById(NpcCorporationId id, out NpcCorporation value)
    {
      if (this.Cache.TryGetValue<NpcCorporation>(id, out value))
      {
        return true;
      }

      var query = this.GetNpcCorporations(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region NpcCorporationDivision Methods
    /// <inheritdoc />
    public NpcCorporationDivision GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId)
    {
      NpcCorporationDivision result;
      if (this.Cache.TryGetValue<NpcCorporationDivision>(NpcCorporationDivision.CreateCacheKey(corporationId, divisionId), out result))
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
      return LoadAndCacheResults<NpcCorporationDivisionEntity, NpcCorporationDivision>(
        this.Context.NpcCorporationDivisions,
        modifiers,
        x => NpcCorporationDivision.CreateCacheKey(x.CorporationId, x.DivisionId));
    }

    /// <inheritdoc />
    public bool TryGetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId, out NpcCorporationDivision value)
    {
      if (this.Cache.TryGetValue<NpcCorporationDivision>(NpcCorporationDivision.CreateCacheKey(corporationId, divisionId), out value))
      {
        return true;
      }

      var query = this.GetNpcCorporationDivisions(x => x.CorporationId == corporationId && x.DivisionId == divisionId);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<RaceEntity, Race>(this.Context.Races, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetRaceById(RaceId id, out Race value)
    {
      if (this.Cache.TryGetValue<Race>(id, out value))
      {
        return true;
      }

      var query = this.GetRaces(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
    public IReadOnlyList<Region> GetRegions(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetRegions(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Region))]
    public IReadOnlyList<Region> GetRegions(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Region>(this.Context.Regions, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetRegionById(RegionId id, out Region value)
    {
      if (this.Cache.TryGetValue<Region>(id, out value))
      {
        return true;
      }

      var query = this.GetRegions(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region RegionJump Methods
    /// <inheritdoc />
    public RegionJump GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId)
    {
      RegionJump result;
      if (this.Cache.TryGetValue<RegionJump>(RegionJump.CreateCacheKey(fromRegionId, toRegionId), out result))
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
      return LoadAndCacheResults<RegionJumpEntity, RegionJump>(
        this.Context.RegionJumps,
        modifiers,
        x => SolarSystemJump.CreateCacheKey(x.FromRegionId, x.ToRegionId));
    }

    /// <inheritdoc />
    public bool TryGetRegionJumpById(RegionId fromRegionId, RegionId toRegionId, out RegionJump value)
    {
      if (this.Cache.TryGetValue<RegionJump>(RegionJump.CreateCacheKey(fromRegionId, toRegionId), out value))
      {
        return true;
      }

      var query = this.GetRegionJumps(x => x.FromRegionId == fromRegionId && x.ToRegionId == toRegionId);
      value = query.SingleOrDefault();

      return value != null;
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
    public IReadOnlyList<SolarSystem> GetSolarSystems(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetSolarSystems(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(SolarSystem))]
    public IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, SolarSystem>(this.Context.SolarSystems, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetSolarSystemById(SolarSystemId id, out SolarSystem value)
    {
      if (this.Cache.TryGetValue<SolarSystem>(id, out value))
      {
        return true;
      }

      var query = this.GetSolarSystems(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region SolarSystemJump Methods
    /// <inheritdoc />
    public SolarSystemJump GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId)
    {
      SolarSystemJump result;
      if (this.Cache.TryGetValue<SolarSystemJump>(SolarSystemJump.CreateCacheKey(fromSolarSystemId, toSolarSystemId), out result))
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
      return LoadAndCacheResults<SolarSystemJumpEntity, SolarSystemJump>(
        this.Context.SolarSystemJumps,
        modifiers,
        x => SolarSystemJump.CreateCacheKey(x.FromSolarSystemId, x.ToSolarSystemId));
    }

    /// <inheritdoc />
    public bool TryGetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId, out SolarSystemJump value)
    {
      if (this.Cache.TryGetValue<SolarSystemJump>(SolarSystemJump.CreateCacheKey(fromSolarSystemId, toSolarSystemId), out value))
      {
        return true;
      }

      var query = this.GetSolarSystemJumps(x => x.FromSolarSystemId == fromSolarSystemId && x.ToSolarSystemId == toSolarSystemId);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    #region Stargate Methods
    /// <inheritdoc />
    public Stargate GetStargateById(StargateId id)
    {
      Stargate result;
      if (this.Cache.TryGetValue<Stargate>(id, out result))
      {
        return result;
      }

      var query = this.GetStargates(x => x.Id == id.Value);
      Contract.Assume(query.Count() == 1);

      result = query.Single();
      Contract.Assume(result != null);

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Stargate> GetStargates(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetStargates(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Stargate))]
    public IReadOnlyList<Stargate> GetStargates(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Stargate>(this.Context.Stargates, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetStargateById(StargateId id, out Stargate value)
    {
      if (this.Cache.TryGetValue<Stargate>(id, out value))
      {
        return true;
      }

      var query = this.GetStargates(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
    public IReadOnlyList<Station> GetStations(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetStations(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Station))]
    public IReadOnlyList<Station> GetStations(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Station>(this.Context.Stations, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetStationById(StationId id, out Station value)
    {
      if (this.Cache.TryGetValue<Station>(id, out value))
      {
        return true;
      }

      var query = this.GetStations(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<StationOperationEntity, StationOperation>(this.Context.StationOperations, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetStationOperationById(StationOperationId id, out StationOperation value)
    {
      if (this.Cache.TryGetValue<StationOperation>(id, out value))
      {
        return true;
      }

      var query = this.GetStationOperations(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<StationServiceEntity, StationService>(this.Context.StationServices, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetStationServiceById(StationServiceId id, out StationService value)
    {
      if (this.Cache.TryGetValue<StationService>(id, out value))
      {
        return true;
      }

      var query = this.GetStationServices(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<StationTypeEntity, StationType>(this.Context.StationTypes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetStationTypeById(TypeId id, out StationType value)
    {
      if (this.Cache.TryGetValue<StationType>(id, out value))
      {
        return true;
      }

      var query = this.GetStationTypes(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
      return LoadAndCacheResults<UnitEntity, Unit>(this.Context.Units, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetUnitById(UnitId id, out Unit value)
    {
      if (this.Cache.TryGetValue<Unit>(id, out value))
      {
        return true;
      }

      var query = this.GetUnits(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
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
    public IReadOnlyList<Universe> GetUniverses(Expression<Func<ItemEntity, bool>> filter)
    {
      return this.GetUniverses(new QuerySpecification<ItemEntity>(filter));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Universe))]
    public IReadOnlyList<Universe> GetUniverses(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, Universe>(this.Context.Universes, modifiers, x => x.Id);
    }

    /// <inheritdoc />
    public bool TryGetUniverseById(UniverseId id, out Universe value)
    {
      if (this.Cache.TryGetValue<Universe>(id, out value))
      {
        return true;
      }

      var query = this.GetUniverses(x => x.Id == id);
      value = query.SingleOrDefault();

      return value != null;
    }
    #endregion

    /// <summary>
    /// Performs a query, transforms the raw entity results into a list of
    /// entity adapters, and syncs the final results with the local cache.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to query.
    /// </typeparam>
    /// <typeparam name="TAdapter">
    /// The type of the entity adapter for <typeparamref name="TEntity" />
    /// </typeparam>
    /// <param name="query">
    /// The base <see cref="IQueryable{T}" /> that provides the query results.
    /// </param>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <param name="cacheKeyGenerator">
    /// A delegate used to generate a cache key for a raw data entity.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    protected internal IReadOnlyList<TAdapter> LoadAndCacheResults<TEntity, TAdapter>(
      IQueryable<TEntity> query,
      IQueryModifier<TEntity>[] modifiers,
      Func<TEntity, IConvertible> cacheKeyGenerator)
      where TEntity : IEveEntity<TAdapter>
      where TAdapter : IEveCacheable
    {
      Contract.Requires(query != null, "The base query cannot be null.");
      Contract.Requires(modifiers != null, "The array of query modifiers cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TAdapter>>() != null);

      lock (this.queryLock)
      {
        var results = GetResults(query, modifiers);
        return results.Select(x => this.Cache.GetOrAdd<TAdapter>(cacheKeyGenerator(x), () => x.ToAdapter(this))).ToArray();
      }
    }

    /// <summary>
    /// Performs a query, transforms the raw entity results into a list of
    /// entity adapters, and syncs the final results with the local cache.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to query.
    /// </typeparam>
    /// <typeparam name="TAdapter">
    /// The type of the entity adapter for <typeparamref name="TEntity" />
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The actual type to return.  This is usually derived from
    /// <typeparamref name="TAdapter" />.
    /// </typeparam>
    /// <param name="query">
    /// The base <see cref="IQueryable{T}" /> that provides the query results.
    /// </param>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <param name="cacheKeyGenerator">
    /// A delegate used to generate a cache key for a raw data entity.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    protected internal IReadOnlyList<TResult> LoadAndCacheResults<TEntity, TAdapter, TResult>(IQueryable<TEntity> query, IQueryModifier<TEntity>[] modifiers, Func<TEntity, IConvertible> cacheKeyGenerator)
      where TEntity : IEveEntity<TAdapter>
      where TAdapter : IEveCacheable
    {
      Contract.Requires(query != null, "The base query cannot be null.");
      Contract.Requires(modifiers != null, "The array of query modifiers cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TResult>>() != null);

      lock (this.queryLock)
      {
        var results = GetResults(query, modifiers);
        return results.Select(x => this.Cache.GetOrAdd<TAdapter>(cacheKeyGenerator(x), () => x.ToAdapter(this))).OfType<TResult>().ToArray();
      }
    }

    /// <summary>
    /// Returns the results of a query against the given entity type,
    /// filtered according to the specified modifiers.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity to query.
    /// </typeparam>
    /// <param name="query">
    /// The base <see cref="IQueryable{T}" /> that provides the
    /// results after the modifiers are applied.
    /// </param>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> containing the results of
    /// the query.
    /// </returns>
    protected internal IEnumerable<TEntity> GetResults<TEntity>(IQueryable<TEntity> query, params IQueryModifier<TEntity>[] modifiers)
      where TEntity : IEveEntity
    {
      Contract.Requires(query != null, "The base query cannot be null.");
      Contract.Requires(modifiers != null, "The array of query modifiers cannot be null.");
      Contract.Ensures(Contract.Result<IEnumerable<TEntity>>() != null);

      // Apply the modifiers
      foreach (IQueryModifier<TEntity> modifier in modifiers)
      {
        Contract.Assume(modifier != null);
        query = modifier.GetResults(query);
      }

      return query;
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
    /// </summary>throw new NotImplementedException\(\);\n
    private void PrepopulateCache()
    {
      // Permanently add all published Categories
      foreach (Category category in this.Context.Categories.Where(x => x.Published == true).ToList().Select(x => x.ToAdapter(this)))
      {
        Contract.Assume(category != null);
        this.Cache.AddOrReplace<Category>(category, true);
      }

      // Permanently add all Groups -- necessary for EveType.Create() to load successfully
      foreach (Group group in this.Context.Groups.ToList().Select(x => x.ToAdapter(this)))
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