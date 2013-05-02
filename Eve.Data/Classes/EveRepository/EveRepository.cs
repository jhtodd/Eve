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
  /// An EveRepository that uses a <see cref="EveDbContext" /> object
  /// to query the database.
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
    public T GetOrAddStoredValue<T>(IConvertible id, Func<T> valueFactory) where T : IEveCacheable
    {
      return this.Cache.GetOrAdd<T>(id, valueFactory);
    }

    #region Activity Methods
    /// <inheritdoc />
    public Activity GetActivityById(ActivityId id)
    {
      Activity result;

      if (!this.TryGetActivityById(id, out result))
      {
        throw new InvalidOperationException("No Activity with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Activity> GetActivities(Func<IQueryable<ActivityEntity>, IQueryable<ActivityEntity>> queryOperations)
    {
      return this.GetActivities(new QueryTransform<ActivityEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Activity))]
    public IReadOnlyList<Activity> GetActivities(params IQueryModifier<ActivityEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ActivityEntity, Activity>(this.Context.Activities, modifiers);
    }
    
    /// <inheritdoc />
    public bool TryGetActivityById(ActivityId id, out Activity value)
    {
      if (this.Cache.TryGetValue<Activity>(id, out value))
      {
        return true;
      }

      value = this.GetActivities(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Agent Methods
    /// <inheritdoc />
    public Agent GetAgentById(AgentId id)
    {
      Agent result;

      if (!this.TryGetAgentById(id, out result))
      {
        throw new InvalidOperationException("No Agent with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Agent> GetAgents(Func<IQueryable<AgentEntity>, IQueryable<AgentEntity>> queryOperations)
    {
      return this.GetAgents(new QueryTransform<AgentEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Agent))]
    public IReadOnlyList<Agent> GetAgents(params IQueryModifier<AgentEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AgentEntity, Agent>(this.Context.Agents, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAgentById(AgentId id, out Agent value)
    {
      if (this.Cache.TryGetValue<Agent>(id, out value))
      {
        return true;
      }

      value = this.GetAgents(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AgentType Methods
    /// <inheritdoc />
    public AgentType GetAgentTypeById(AgentTypeId id)
    {
      AgentType result;

      if (!this.TryGetAgentTypeById(id, out result))
      {
        throw new InvalidOperationException("No AgentType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AgentType> GetAgentTypes(Func<IQueryable<AgentTypeEntity>, IQueryable<AgentTypeEntity>> queryOperations)
    {
      return this.GetAgentTypes(new QueryTransform<AgentTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AgentType))]
    public IReadOnlyList<AgentType> GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AgentTypeEntity, AgentType>(this.Context.AgentTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAgentTypeById(AgentTypeId id, out AgentType value)
    {
      if (this.Cache.TryGetValue<AgentType>(id, out value))
      {
        return true;
      }

      value = this.GetAgentTypes(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Ancestry Methods
    /// <inheritdoc />
    public Ancestry GetAncestryById(AncestryId id)
    {
      Ancestry result;

      if (!this.TryGetAncestryById(id, out result))
      {
        throw new InvalidOperationException("No Ancestry with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Ancestry> GetAncestries(Func<IQueryable<AncestryEntity>, IQueryable<AncestryEntity>> queryOperations)
    {
      return this.GetAncestries(new QueryTransform<AncestryEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Ancestry))]
    public IReadOnlyList<Ancestry> GetAncestries(params IQueryModifier<AncestryEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AncestryEntity, Ancestry>(this.Context.Ancestries, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAncestryById(AncestryId id, out Ancestry value)
    {
      if (this.Cache.TryGetValue<Ancestry>(id, out value))
      {
        return true;
      }

      value = this.GetAncestries(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AssemblyLine Methods
    /// <inheritdoc />
    public AssemblyLine GetAssemblyLineById(AssemblyLineId id)
    {
      AssemblyLine result;

      if (!this.TryGetAssemblyLineById(id, out result))
      {
        throw new InvalidOperationException("No AssemblyLine with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLine> GetAssemblyLines(Func<IQueryable<AssemblyLineEntity>, IQueryable<AssemblyLineEntity>> queryOperations)
    {
      return this.GetAssemblyLines(new QueryTransform<AssemblyLineEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLine))]
    public IReadOnlyList<AssemblyLine> GetAssemblyLines(params IQueryModifier<AssemblyLineEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineEntity, AssemblyLine>(this.Context.AssemblyLines, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineById(AssemblyLineId id, out AssemblyLine value)
    {
      if (this.Cache.TryGetValue<AssemblyLine>(id, out value))
      {
        return true;
      }

      value = this.GetAssemblyLines(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AssemblyLineStation Methods
    /// <inheritdoc />
    public AssemblyLineStation GetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId)
    {
      AssemblyLineStation result;

      if (!this.TryGetAssemblyLineStationById(stationId, assemblyLineTypeId, out result))
      {
        throw new InvalidOperationException("No AssemblyLineStation with ID (" + stationId.ToString() + ", " + assemblyLineTypeId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineStation> GetAssemblyLineStations(Func<IQueryable<AssemblyLineStationEntity>, IQueryable<AssemblyLineStationEntity>> queryOperations)
    {
      return this.GetAssemblyLineStations(new QueryTransform<AssemblyLineStationEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineStation))]
    public IReadOnlyList<AssemblyLineStation> GetAssemblyLineStations(params IQueryModifier<AssemblyLineStationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineStationEntity, AssemblyLineStation>(this.Context.AssemblyLineStations, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId, out AssemblyLineStation value)
    {
      if (this.Cache.TryGetValue<AssemblyLineStation>(AssemblyLineStationEntity.CreateCacheKey(stationId, assemblyLineTypeId), out value))
      {
        return true;
      }

      value = this.GetAssemblyLineStations(q => q.Where(x => x.StationId == stationId.Value && x.AssemblyLineTypeId == assemblyLineTypeId.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AssemblyLineType Methods
    /// <inheritdoc />
    public AssemblyLineType GetAssemblyLineTypeById(AssemblyLineTypeId id)
    {
      AssemblyLineType result;

      if (!this.TryGetAssemblyLineTypeById(id, out result))
      {
        throw new InvalidOperationException("No AssemblyLineType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineType> GetAssemblyLineTypes(Func<IQueryable<AssemblyLineTypeEntity>, IQueryable<AssemblyLineTypeEntity>> queryOperations)
    {
      return this.GetAssemblyLineTypes(new QueryTransform<AssemblyLineTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineType))]
    public IReadOnlyList<AssemblyLineType> GetAssemblyLineTypes(params IQueryModifier<AssemblyLineTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineTypeEntity, AssemblyLineType>(this.Context.AssemblyLineTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineTypeById(AssemblyLineTypeId id, out AssemblyLineType value)
    {
      if (this.Cache.TryGetValue<AssemblyLineType>(id, out value))
      {
        return true;
      }

      value = this.GetAssemblyLineTypes(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AssemblyLineTypeCategoryDetail Methods
    /// <inheritdoc />
    public AssemblyLineTypeCategoryDetail GetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId)
    {
      AssemblyLineTypeCategoryDetail result;

      if (!this.TryGetAssemblyLineTypeCategoryDetailById(assemblyLineTypeId, categoryId, out result))
      {
        throw new InvalidOperationException("No AssemblyLineTypeCategoryDetail with ID (" + assemblyLineTypeId.ToString() + ", " + categoryId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineTypeCategoryDetail> GetAssemblyLineTypeCategoryDetails(Func<IQueryable<AssemblyLineTypeCategoryDetailEntity>, IQueryable<AssemblyLineTypeCategoryDetailEntity>> queryOperations)
    {
      return this.GetAssemblyLineTypeCategoryDetails(new QueryTransform<AssemblyLineTypeCategoryDetailEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineTypeCategoryDetail))]
    public IReadOnlyList<AssemblyLineTypeCategoryDetail> GetAssemblyLineTypeCategoryDetails(params IQueryModifier<AssemblyLineTypeCategoryDetailEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineTypeCategoryDetailEntity, AssemblyLineTypeCategoryDetail>(this.Context.AssemblyLineTypeCategoryDetails, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId, out AssemblyLineTypeCategoryDetail value)
    {
      if (this.Cache.TryGetValue<AssemblyLineTypeCategoryDetail>(AssemblyLineTypeCategoryDetailEntity.CreateCacheKey(assemblyLineTypeId, categoryId), out value))
      {
        return true;
      }

      value = this.GetAssemblyLineTypeCategoryDetails(q => q.Where(x => x.AssemblyLineTypeId == assemblyLineTypeId.Value && x.CategoryId == categoryId)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AssemblyLineTypeGroupDetail Methods
    /// <inheritdoc />
    public AssemblyLineTypeGroupDetail GetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId)
    {
      AssemblyLineTypeGroupDetail result;

      if (!this.TryGetAssemblyLineTypeGroupDetailById(assemblyLineTypeId, groupId, out result))
      {
        throw new InvalidOperationException("No AssemblyLineTypeGroupDetail with ID (" + assemblyLineTypeId.ToString() + ", " + groupId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AssemblyLineTypeGroupDetail> GetAssemblyLineTypeGroupDetails(Func<IQueryable<AssemblyLineTypeGroupDetailEntity>, IQueryable<AssemblyLineTypeGroupDetailEntity>> queryOperations)
    {
      return this.GetAssemblyLineTypeGroupDetails(new QueryTransform<AssemblyLineTypeGroupDetailEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AssemblyLineTypeGroupDetail))]
    public IReadOnlyList<AssemblyLineTypeGroupDetail> GetAssemblyLineTypeGroupDetails(params IQueryModifier<AssemblyLineTypeGroupDetailEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AssemblyLineTypeGroupDetailEntity, AssemblyLineTypeGroupDetail>(this.Context.AssemblyLineTypeGroupDetails, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId, out AssemblyLineTypeGroupDetail value)
    {
      if (this.Cache.TryGetValue<AssemblyLineTypeGroupDetail>(AssemblyLineTypeGroupDetailEntity.CreateCacheKey(assemblyLineTypeId, groupId), out value))
      {
        return true;
      }

      value = this.GetAssemblyLineTypeGroupDetails(q => q.Where(x => x.AssemblyLineTypeId == assemblyLineTypeId.Value && x.GroupId == groupId)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AttributeCategory Methods
    /// <inheritdoc />
    public AttributeCategory GetAttributeCategoryById(AttributeCategoryId id)
    {
      AttributeCategory result;

      if (!this.TryGetAttributeCategoryById(id, out result))
      {
        throw new InvalidOperationException("No AttributeCategory with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AttributeCategory> GetAttributeCategories(Func<IQueryable<AttributeCategoryEntity>, IQueryable<AttributeCategoryEntity>> queryOperations)
    {
      return this.GetAttributeCategories(new QueryTransform<AttributeCategoryEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AttributeCategory))]
    public IReadOnlyList<AttributeCategory> GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AttributeCategoryEntity, AttributeCategory>(this.Context.AttributeCategories, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAttributeCategoryById(AttributeCategoryId id, out AttributeCategory value)
    {
      if (this.Cache.TryGetValue<AttributeCategory>(id, out value))
      {
        return true;
      }

      value = this.GetAttributeCategories(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AttributeType Methods
    /// <inheritdoc />
    public AttributeType GetAttributeTypeById(AttributeId id)
    {
      AttributeType result;

      if (!this.TryGetAttributeTypeById(id, out result))
      {
        throw new InvalidOperationException("No AttributeType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AttributeType> GetAttributeTypes(Func<IQueryable<AttributeTypeEntity>, IQueryable<AttributeTypeEntity>> queryOperations)
    {
      return this.GetAttributeTypes(new QueryTransform<AttributeTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(AttributeType))]
    public IReadOnlyList<AttributeType> GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<AttributeTypeEntity, AttributeType>(this.Context.AttributeTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetAttributeTypeById(AttributeId id, out AttributeType value)
    {
      if (this.Cache.TryGetValue<AttributeType>(id, out value))
      {
        return true;
      }

      value = this.GetAttributeTypes(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region AttributeValue Methods
    /// <inheritdoc />
    public AttributeValue GetAttributeValueById(TypeId itemTypeId, AttributeId attributeId)
    {
      AttributeValue result;

      if (!this.TryGetAttributeValueById(itemTypeId, attributeId, out result))
      {
        throw new InvalidOperationException("No AttributeValue with ID (" + itemTypeId.ToString() + ", " + attributeId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<AttributeValue> GetAttributeValues(Func<IQueryable<AttributeValueEntity>, IQueryable<AttributeValueEntity>> queryOperations)
    {
      return this.GetAttributeValues(new QueryTransform<AttributeValueEntity>(queryOperations));
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
      if (this.Cache.TryGetValue<AttributeValue>(AttributeValueEntity.CreateCacheKey(itemTypeId, attributeId), out value))
      {
        return true;
      }

      value = this.GetAttributeValues(q => q.Where(x => x.ItemTypeId == itemTypeId.Value && x.AttributeId == attributeId)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Bloodline Methods
    /// <inheritdoc />
    public Bloodline GetBloodlineById(BloodlineId id)
    {
      Bloodline result;

      if (!this.TryGetBloodlineById(id, out result))
      {
        throw new InvalidOperationException("No Bloodline with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Bloodline> GetBloodlines(Func<IQueryable<BloodlineEntity>, IQueryable<BloodlineEntity>> queryOperations)
    {
      return this.GetBloodlines(new QueryTransform<BloodlineEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Bloodline))]
    public IReadOnlyList<Bloodline> GetBloodlines(params IQueryModifier<BloodlineEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<BloodlineEntity, Bloodline>(this.Context.Bloodlines, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetBloodlineById(BloodlineId id, out Bloodline value)
    {
      if (this.Cache.TryGetValue<Bloodline>(id, out value))
      {
        return true;
      }

      value = this.GetBloodlines(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Category Methods
    /// <inheritdoc />
    public Category GetCategoryById(CategoryId id)
    {
      Category result;

      if (!this.TryGetCategoryById(id, out result))
      {
        throw new InvalidOperationException("No Category with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Category> GetCategories(Func<IQueryable<CategoryEntity>, IQueryable<CategoryEntity>> queryOperations)
    {
      return this.GetCategories(new QueryTransform<CategoryEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Category))]
    public IReadOnlyList<Category> GetCategories(params IQueryModifier<CategoryEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CategoryEntity, Category>(this.Context.Categories, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCategoryById(CategoryId id, out Category value)
    {
      if (this.Cache.TryGetValue<Category>(id, out value))
      {
        return true;
      }

      value = this.GetCategories(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Celestial Methods
    /// <inheritdoc />
    public Celestial GetCelestialById(CelestialId id)
    {
      Celestial result;

      if (!this.TryGetCelestialById(id, out result))
      {
        throw new InvalidOperationException("No Celestial with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Celestial> GetCelestials(Func<IQueryable<CelestialEntity>, IQueryable<CelestialEntity>> queryOperations)
    {
      return this.GetCelestials(new QueryTransform<CelestialEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Celestial))]
    public IReadOnlyList<Celestial> GetCelestials(params IQueryModifier<CelestialEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CelestialEntity, Celestial>(this.Context.Celestials, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCelestialById(CelestialId id, out Celestial value)
    {
      if (this.Cache.TryGetValue<Celestial>(id, out value))
      {
        return true;
      }

      value = this.GetCelestials(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Certificate Methods
    /// <inheritdoc />
    public Certificate GetCertificateById(CertificateId id)
    {
      Certificate result;

      if (!this.TryGetCertificateById(id, out result))
      {
        throw new InvalidOperationException("No Certificate with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Certificate> GetCertificates(Func<IQueryable<CertificateEntity>, IQueryable<CertificateEntity>> queryOperations)
    {
      return this.GetCertificates(new QueryTransform<CertificateEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Certificate))]
    public IReadOnlyList<Certificate> GetCertificates(params IQueryModifier<CertificateEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CertificateEntity, Certificate>(this.Context.Certificates, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCertificateById(CertificateId id, out Certificate value)
    {
      if (this.Cache.TryGetValue<Certificate>(id, out value))
      {
        return true;
      }

      value = this.GetCertificates(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region CertificateCategory Methods
    /// <inheritdoc />
    public CertificateCategory GetCertificateCategoryById(CertificateCategoryId id)
    {
      CertificateCategory result;

      if (!this.TryGetCertificateCategoryById(id, out result))
      {
        throw new InvalidOperationException("No CertificateCategory with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<CertificateCategory> GetCertificateCategories(Func<IQueryable<CertificateCategoryEntity>, IQueryable<CertificateCategoryEntity>> queryOperations)
    {
      return this.GetCertificateCategories(new QueryTransform<CertificateCategoryEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CertificateCategory))]
    public IReadOnlyList<CertificateCategory> GetCertificateCategories(params IQueryModifier<CertificateCategoryEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CertificateCategoryEntity, CertificateCategory>(this.Context.CertificateCategories, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCertificateCategoryById(CertificateCategoryId id, out CertificateCategory value)
    {
      if (this.Cache.TryGetValue<CertificateCategory>(id, out value))
      {
        return true;
      }

      value = this.GetCertificateCategories(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region CertificateClass Methods
    /// <inheritdoc />
    public CertificateClass GetCertificateClassById(CertificateClassId id)
    {
      CertificateClass result;

      if (!this.TryGetCertificateClassById(id, out result))
      {
        throw new InvalidOperationException("No CertificateClass with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<CertificateClass> GetCertificateClasses(Func<IQueryable<CertificateClassEntity>, IQueryable<CertificateClassEntity>> queryOperations)
    {
      return this.GetCertificateClasses(new QueryTransform<CertificateClassEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CertificateClass))]
    public IReadOnlyList<CertificateClass> GetCertificateClasses(params IQueryModifier<CertificateClassEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CertificateClassEntity, CertificateClass>(this.Context.CertificateClasses, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCertificateClassById(CertificateClassId id, out CertificateClass value)
    {
      if (this.Cache.TryGetValue<CertificateClass>(id, out value))
      {
        return true;
      }

      value = this.GetCertificateClasses(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region CertificateRecommendation Methods
    /// <inheritdoc />
    public CertificateRecommendation GetCertificateRecommendationById(CertificateRecommendationId id)
    {
      CertificateRecommendation result;

      if (!this.TryGetCertificateRecommendationById(id, out result))
      {
        throw new InvalidOperationException("No CertificateRecommendation with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<CertificateRecommendation> GetCertificateRecommendations(Func<IQueryable<CertificateRecommendationEntity>, IQueryable<CertificateRecommendationEntity>> queryOperations)
    {
      return this.GetCertificateRecommendations(new QueryTransform<CertificateRecommendationEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CertificateRecommendation))]
    public IReadOnlyList<CertificateRecommendation> GetCertificateRecommendations(params IQueryModifier<CertificateRecommendationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CertificateRecommendationEntity, CertificateRecommendation>(this.Context.CertificateRecommendations, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCertificateRecommendationById(CertificateRecommendationId id, out CertificateRecommendation value)
    {
      if (this.Cache.TryGetValue<CertificateRecommendation>(id, out value))
      {
        return true;
      }

      value = this.GetCertificateRecommendations(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region CertificateRelationship Methods
    /// <inheritdoc />
    public CertificateRelationship GetCertificateRelationshipById(CertificateRelationshipId id)
    {
      CertificateRelationship result;

      if (!this.TryGetCertificateRelationshipById(id, out result))
      {
        throw new InvalidOperationException("No CertificateRelationship with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<CertificateRelationship> GetCertificateRelationships(Func<IQueryable<CertificateRelationshipEntity>, IQueryable<CertificateRelationshipEntity>> queryOperations)
    {
      return this.GetCertificateRelationships(new QueryTransform<CertificateRelationshipEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CertificateRelationship))]
    public IReadOnlyList<CertificateRelationship> GetCertificateRelationships(params IQueryModifier<CertificateRelationshipEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CertificateRelationshipEntity, CertificateRelationship>(this.Context.CertificateRelationships, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCertificateRelationshipById(CertificateRelationshipId id, out CertificateRelationship value)
    {
      if (this.Cache.TryGetValue<CertificateRelationship>(id, out value))
      {
        return true;
      }

      value = this.GetCertificateRelationships(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region CharacterAttributeType Methods
    /// <inheritdoc />
    public CharacterAttributeType GetCharacterAttributeTypeById(CharacterAttributeId id)
    {
      CharacterAttributeType result;

      if (!this.TryGetCharacterAttributeTypeById(id, out result))
      {
        throw new InvalidOperationException("No CharacterAttributeType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(Func<IQueryable<CharacterAttributeTypeEntity>, IQueryable<CharacterAttributeTypeEntity>> queryOperations)
    {
      return this.GetCharacterAttributeTypes(new QueryTransform<CharacterAttributeTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CharacterAttributeType))]
    public IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CharacterAttributeTypeEntity, CharacterAttributeType>(this.Context.CharacterAttributeTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCharacterAttributeTypeById(CharacterAttributeId id, out CharacterAttributeType value)
    {
      if (this.Cache.TryGetValue<CharacterAttributeType>(id, out value))
      {
        return true;
      }

      value = this.GetCharacterAttributeTypes(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Constellation Methods
    /// <inheritdoc />
    public Constellation GetConstellationById(ConstellationId id)
    {
      Constellation result;

      if (!this.TryGetConstellationById(id, out result))
      {
        throw new InvalidOperationException("No Constellation with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Constellation> GetConstellations(Func<IQueryable<ConstellationEntity>, IQueryable<ConstellationEntity>> queryOperations)
    {
      return this.GetConstellations(new QueryTransform<ConstellationEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Constellation))]
    public IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ConstellationEntity, Constellation>(this.Context.Constellations, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetConstellationById(ConstellationId id, out Constellation value)
    {
      if (this.Cache.TryGetValue<Constellation>(id, out value))
      {
        return true;
      }

      value = this.GetConstellations(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region ConstellationJump Methods
    /// <inheritdoc />
    public ConstellationJump GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId)
    {
      ConstellationJump result;

      if (!this.TryGetConstellationJumpById(fromConstellationId, toConstellationId, out result))
      {
        throw new InvalidOperationException("No ConstellationJump with ID (" + fromConstellationId.ToString() + ", " + toConstellationId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<ConstellationJump> GetConstellationJumps(Func<IQueryable<ConstellationJumpEntity>, IQueryable<ConstellationJumpEntity>> queryOperations)
    {
      return this.GetConstellationJumps(new QueryTransform<ConstellationJumpEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(ConstellationJump))]
    public IReadOnlyList<ConstellationJump> GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ConstellationJumpEntity, ConstellationJump>(this.Context.ConstellationJumps, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId, out ConstellationJump value)
    {
      if (this.Cache.TryGetValue<ConstellationJump>(ConstellationJumpEntity.CreateCacheKey(fromConstellationId, toConstellationId), out value))
      {
        return true;
      }

      value = this.GetConstellationJumps(q => q.Where(x => x.FromConstellationId == fromConstellationId.Value && x.ToConstellationId == toConstellationId.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region ContrabandInfo Methods
    /// <inheritdoc />
    public ContrabandInfo GetContrabandInfoById(FactionId factionId, TypeId typeId)
    {
      ContrabandInfo result;

      if (!this.TryGetContrabandInfoById(factionId, typeId, out result))
      {
        throw new InvalidOperationException("No ContrabandInfo with ID (" + factionId.ToString() + ", " + typeId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<ContrabandInfo> GetContrabandInfo(Func<IQueryable<ContrabandInfoEntity>, IQueryable<ContrabandInfoEntity>> queryOperations)
    {
      return this.GetContrabandInfo(new QueryTransform<ContrabandInfoEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(ContrabandInfo))]
    public IReadOnlyList<ContrabandInfo> GetContrabandInfo(params IQueryModifier<ContrabandInfoEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ContrabandInfoEntity, ContrabandInfo>(this.Context.ContrabandInfo, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetContrabandInfoById(FactionId factionId, TypeId typeId, out ContrabandInfo value)
    {
      if (this.Cache.TryGetValue<ContrabandInfo>(ContrabandInfoEntity.CreateCacheKey((long)factionId, typeId), out value))
      {
        return true;
      }

      value = this.GetContrabandInfo(q => q.Where(x => x.FactionId == (long)factionId && x.TypeId == typeId.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region ControlTowerResource Methods
    /// <inheritdoc />
    public ControlTowerResource GetControlTowerResourceById(TypeId controlTowerTypeId, TypeId resourceTypeId)
    {
      ControlTowerResource result;

      if (!this.TryGetControlTowerResourceById(controlTowerTypeId, resourceTypeId, out result))
      {
        throw new InvalidOperationException("No ControlTowerResource with ID (" + controlTowerTypeId.ToString() + ", " + resourceTypeId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<ControlTowerResource> GetControlTowerResources(Func<IQueryable<ControlTowerResourceEntity>, IQueryable<ControlTowerResourceEntity>> queryOperations)
    {
      return this.GetControlTowerResources(new QueryTransform<ControlTowerResourceEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(ControlTowerResource))]
    public IReadOnlyList<ControlTowerResource> GetControlTowerResources(params IQueryModifier<ControlTowerResourceEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ControlTowerResourceEntity, ControlTowerResource>(this.Context.ControlTowerResources, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetControlTowerResourceById(TypeId controlTowerTypeId, TypeId resourceTypeId, out ControlTowerResource value)
    {
      if (this.Cache.TryGetValue<ControlTowerResource>(ControlTowerResourceEntity.CreateCacheKey(controlTowerTypeId, resourceTypeId), out value))
      {
        return true;
      }

      value = this.GetControlTowerResources(q => q.Where(x => x.ControlTowerTypeId == controlTowerTypeId.Value && x.ResourceTypeId == resourceTypeId.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region CorporateActivity Methods
    /// <inheritdoc />
    public CorporateActivity GetCorporateActivityById(CorporateActivityId id)
    {
      CorporateActivity result;

      if (!this.TryGetCorporateActivityById(id, out result))
      {
        throw new InvalidOperationException("No CorporateActivity with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<CorporateActivity> GetCorporateActivities(Func<IQueryable<CorporateActivityEntity>, IQueryable<CorporateActivityEntity>> queryOperations)
    {
      return this.GetCorporateActivities(new QueryTransform<CorporateActivityEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(CorporateActivity))]
    public IReadOnlyList<CorporateActivity> GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<CorporateActivityEntity, CorporateActivity>(this.Context.CorporateActivities, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetCorporateActivityById(CorporateActivityId id, out CorporateActivity value)
    {
      if (this.Cache.TryGetValue<CorporateActivity>(id, out value))
      {
        return true;
      }

      value = this.GetCorporateActivities(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Division Methods
    /// <inheritdoc />
    public Division GetDivisionById(DivisionId id)
    {
      Division result;

      if (!this.TryGetDivisionById(id, out result))
      {
        throw new InvalidOperationException("No Division with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Division> GetDivisions(Func<IQueryable<DivisionEntity>, IQueryable<DivisionEntity>> queryOperations)
    {
      return this.GetDivisions(new QueryTransform<DivisionEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Division))]
    public IReadOnlyList<Division> GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<DivisionEntity, Division>(this.Context.Divisions, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetDivisionById(DivisionId id, out Division value)
    {
      if (this.Cache.TryGetValue<Division>(id, out value))
      {
        return true;
      }

      value = this.GetDivisions(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Effect Methods
    /// <inheritdoc />
    public Effect GetEffectById(TypeId itemTypeId, EffectId effectId)
    {
      Effect result;

      if (!this.TryGetEffectById(itemTypeId, effectId, out result))
      {
        throw new InvalidOperationException("No Effect with ID (" + itemTypeId.ToString() + ", " + effectId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Effect> GetEffects(Func<IQueryable<EffectEntity>, IQueryable<EffectEntity>> queryOperations)
    {
      return this.GetEffects(new QueryTransform<EffectEntity>(queryOperations));
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
      if (this.Cache.TryGetValue<Effect>(EffectEntity.CreateCacheKey(itemTypeId, effectId), out value))
      {
        return true;
      }

      value = this.GetEffects(q => q.Where(x => x.ItemTypeId == itemTypeId.Value && x.EffectId == effectId)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region EffectType Methods
    /// <inheritdoc />
    public EffectType GetEffectTypeById(EffectId id)
    {
      EffectType result;

      if (!this.TryGetEffectTypeById(id, out result))
      {
        throw new InvalidOperationException("No EffectType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<EffectType> GetEffectTypes(Func<IQueryable<EffectTypeEntity>, IQueryable<EffectTypeEntity>> queryOperations)
    {
      return this.GetEffectTypes(new QueryTransform<EffectTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(EffectType))]
    public IReadOnlyList<EffectType> GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<EffectTypeEntity, EffectType>(this.Context.EffectTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetEffectTypeById(EffectId id, out EffectType value)
    {
      if (this.Cache.TryGetValue<EffectType>(id, out value))
      {
        return true;
      }

      value = this.GetEffectTypes(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region EveType Methods
    /// <inheritdoc />
    public EveType GetEveTypeById(TypeId id)
    {
      EveType result;

      if (!this.TryGetEveTypeById(id, out result))
      {
        throw new InvalidOperationException("No EveType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<EveType> GetEveTypes(Func<IQueryable<EveTypeEntity>, IQueryable<EveTypeEntity>> queryOperations)
    {
      return this.GetEveTypes(new QueryTransform<EveTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(EveType))]
    public IReadOnlyList<EveType> GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<EveTypeEntity, EveType>(this.Context.EveTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetEveTypeById(TypeId id, out EveType value)
    {
      if (this.Cache.TryGetValue<EveType>(id, out value))
      {
        return true;
      }

      value = this.GetEveTypes(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }

    /// <inheritdoc />
    public TEveType GetEveTypeById<TEveType>(TypeId id) where TEveType : EveType
    {
      TEveType result;

      if (!this.TryGetEveTypeById<TEveType>(id, out result))
      {
        throw new InvalidOperationException("No " + typeof(TEveType).Name + " with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<TEveType> GetEveTypes<TEveType>(Func<IQueryable<EveTypeEntity>, IQueryable<EveTypeEntity>> queryOperations) where TEveType : EveType
    {
      return this.GetEveTypes<TEveType>(new QueryTransform<EveTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    public IReadOnlyList<TEveType> GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers) where TEveType : EveType
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<EveTypeEntity, EveType, TEveType>(this.Context.EveTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetEveTypeById<TEveType>(TypeId id, out TEveType value) where TEveType : EveType
    {
      if (this.Cache.TryGetValue<TEveType>(id, out value))
      {
        return true;
      }

      value = this.GetEveTypes<TEveType>(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Faction Methods
    /// <inheritdoc />
    public Faction GetFactionById(FactionId id)
    {
      Faction result;

      if (!this.TryGetFactionById(id, out result))
      {
        throw new InvalidOperationException("No Faction with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Faction> GetFactions(Func<IQueryable<FactionEntity>, IQueryable<FactionEntity>> queryOperations)
    {
      return this.GetFactions(new QueryTransform<FactionEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Faction))]
    public IReadOnlyList<Faction> GetFactions(params IQueryModifier<FactionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<FactionEntity, Faction>(this.Context.Factions, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetFactionById(FactionId id, out Faction value)
    {
      if (this.Cache.TryGetValue<Faction>(id, out value))
      {
        return true;
      }

      value = this.GetFactions(q => q.Where(x => x.Id == (long)id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Graphic Methods
    /// <inheritdoc />
    public Graphic GetGraphicById(GraphicId id)
    {
      Graphic result;

      if (!this.TryGetGraphicById(id, out result))
      {
        throw new InvalidOperationException("No Graphic with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Graphic> GetGraphics(Func<IQueryable<GraphicEntity>, IQueryable<GraphicEntity>> queryOperations)
    {
      return this.GetGraphics(new QueryTransform<GraphicEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Graphic))]
    public IReadOnlyList<Graphic> GetGraphics(params IQueryModifier<GraphicEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<GraphicEntity, Graphic>(this.Context.Graphics, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetGraphicById(GraphicId id, out Graphic value)
    {
      if (this.Cache.TryGetValue<Graphic>(id, out value))
      {
        return true;
      }

      value = this.GetGraphics(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Group Methods
    /// <inheritdoc />
    public Group GetGroupById(GroupId id)
    {
      Group result;

      if (!this.TryGetGroupById(id, out result))
      {
        throw new InvalidOperationException("No Group with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Group> GetGroups(Func<IQueryable<GroupEntity>, IQueryable<GroupEntity>> queryOperations)
    {
      return this.GetGroups(new QueryTransform<GroupEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Group))]
    public IReadOnlyList<Group> GetGroups(params IQueryModifier<GroupEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<GroupEntity, Group>(this.Context.Groups, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetGroupById(GroupId id, out Group value)
    {
      if (this.Cache.TryGetValue<Group>(id, out value))
      {
        return true;
      }

      value = this.GetGroups(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Icon Methods
    /// <inheritdoc />
    public Icon GetIconById(IconId id)
    {
      Icon result;

      if (!this.TryGetIconById(id, out result))
      {
        throw new InvalidOperationException("No Icon with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Icon> GetIcons(Func<IQueryable<IconEntity>, IQueryable<IconEntity>> queryOperations)
    {
      return this.GetIcons(new QueryTransform<IconEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Icon))]
    public IReadOnlyList<Icon> GetIcons(params IQueryModifier<IconEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<IconEntity, Icon>(this.Context.Icons, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetIconById(IconId id, out Icon value)
    {
      if (this.Cache.TryGetValue<Icon>(id, out value))
      {
        return true;
      }

      value = this.GetIcons(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Item Methods
    /// <inheritdoc />
    public Item GetItemById(ItemId id)
    {
      Item result;

      if (!this.TryGetItemById(id, out result))
      {
        throw new InvalidOperationException("No Item with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Item> GetItems(Func<IQueryable<ItemEntity>, IQueryable<ItemEntity>> queryOperations)
    {
      return this.GetItems(new QueryTransform<ItemEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Item))]
    public IReadOnlyList<Item> GetItems(params IQueryModifier<ItemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item>(this.Context.Items, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetItemById(ItemId id, out Item value)
    {
      if (this.Cache.TryGetValue<Item>(id, out value))
      {
        return true;
      }

      value = this.GetItems(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }

    /// <inheritdoc />
    public TItem GetItemById<TItem>(ItemId id) where TItem : Item
    {
      TItem result;

      if (!this.TryGetItemById<TItem>(id, out result))
      {
        throw new InvalidOperationException("No " + typeof(TItem).Name + " with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItems<TItem>(Func<IQueryable<ItemEntity>, IQueryable<ItemEntity>> queryOperations) where TItem : Item
    {
      return this.GetItems<TItem>(new QueryTransform<ItemEntity>(queryOperations));
    }

    /// <inheritdoc />
    public IReadOnlyList<TItem> GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers) where TItem : Item
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemEntity, Item, TItem>(this.Context.Items, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetItemById<TItem>(ItemId id, out TItem value) where TItem : Item
    {
      if (this.Cache.TryGetValue<TItem>(id, out value))
      {
        return true;
      }

      value = this.GetItems<TItem>(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();

      return value != null;
    }
    #endregion

    #region ItemPosition Methods
    /// <inheritdoc />
    public ItemPosition GetItemPositionById(ItemId id)
    {
      ItemPosition result;

      if (!this.TryGetItemPositionById(id, out result))
      {
        throw new InvalidOperationException("No ItemPosition with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<ItemPosition> GetItemPositions(Func<IQueryable<ItemPositionEntity>, IQueryable<ItemPositionEntity>> queryOperations)
    {
      return this.GetItemPositions(new QueryTransform<ItemPositionEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(ItemPosition))]
    public IReadOnlyList<ItemPosition> GetItemPositions(params IQueryModifier<ItemPositionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<ItemPositionEntity, ItemPosition>(this.Context.ItemPositions, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetItemPositionById(ItemId id, out ItemPosition value)
    {
      if (this.Cache.TryGetValue<ItemPosition>(id, out value))
      {
        return true;
      }

      value = this.GetItemPositions(q => q.Where(x => x.ItemId == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region MarketGroup Methods
    /// <inheritdoc />
    public MarketGroup GetMarketGroupById(MarketGroupId id)
    {
      MarketGroup result;

      if (!this.TryGetMarketGroupById(id, out result))
      {
        throw new InvalidOperationException("No MarketGroup with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<MarketGroup> GetMarketGroups(Func<IQueryable<MarketGroupEntity>, IQueryable<MarketGroupEntity>> queryOperations)
    {
      return this.GetMarketGroups(new QueryTransform<MarketGroupEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(MarketGroup))]
    public IReadOnlyList<MarketGroup> GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<MarketGroupEntity, MarketGroup>(this.Context.MarketGroups, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetMarketGroupById(MarketGroupId id, out MarketGroup value)
    {
      if (this.Cache.TryGetValue<MarketGroup>(id, out value))
      {
        return true;
      }

      value = this.GetMarketGroups(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region MetaGroup Methods
    /// <inheritdoc />
    public MetaGroup GetMetaGroupById(MetaGroupId id)
    {
      MetaGroup result;

      if (!this.TryGetMetaGroupById(id, out result))
      {
        throw new InvalidOperationException("No MetaGroup with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<MetaGroup> GetMetaGroups(Func<IQueryable<MetaGroupEntity>, IQueryable<MetaGroupEntity>> queryOperations)
    {
      return this.GetMetaGroups(new QueryTransform<MetaGroupEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(MetaGroup))]
    public IReadOnlyList<MetaGroup> GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<MetaGroupEntity, MetaGroup>(this.Context.MetaGroups, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetMetaGroupById(MetaGroupId id, out MetaGroup value)
    {
      if (this.Cache.TryGetValue<MetaGroup>(id, out value))
      {
        return true;
      }

      value = this.GetMetaGroups(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region MetaType Methods
    /// <inheritdoc />
    public MetaType GetMetaTypeById(TypeId id)
    {
      MetaType result;

      if (!this.TryGetMetaTypeById(id, out result))
      {
        throw new InvalidOperationException("No MetaType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<MetaType> GetMetaTypes(Func<IQueryable<MetaTypeEntity>, IQueryable<MetaTypeEntity>> queryOperations)
    {
      return this.GetMetaTypes(new QueryTransform<MetaTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(MetaType))]
    public IReadOnlyList<MetaType> GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<MetaTypeEntity, MetaType>(this.Context.MetaTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetMetaTypeById(TypeId id, out MetaType value)
    {
      if (this.Cache.TryGetValue<MetaType>(id, out value))
      {
        return true;
      }

      value = this.GetMetaTypes(q => q.Where(x => x.TypeId == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region NpcCorporation Methods
    /// <inheritdoc />
    public NpcCorporation GetNpcCorporationById(NpcCorporationId id)
    {
      NpcCorporation result;

      if (!this.TryGetNpcCorporationById(id, out result))
      {
        throw new InvalidOperationException("No NpcCorporation with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(Func<IQueryable<NpcCorporationEntity>, IQueryable<NpcCorporationEntity>> queryOperations)
    {
      return this.GetNpcCorporations(new QueryTransform<NpcCorporationEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(NpcCorporation))]
    public IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<NpcCorporationEntity, NpcCorporation>(this.Context.NpcCorporations, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetNpcCorporationById(NpcCorporationId id, out NpcCorporation value)
    {
      if (this.Cache.TryGetValue<NpcCorporation>(id, out value))
      {
        return true;
      }

      value = this.GetNpcCorporations(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region NpcCorporationDivision Methods
    /// <inheritdoc />
    public NpcCorporationDivision GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId)
    {
      NpcCorporationDivision result;

      if (!this.TryGetNpcCorporationDivisionById(corporationId, divisionId, out result))
      {
        throw new InvalidOperationException("No NpcCorporationDivision with ID (" + corporationId.ToString() + ", " + divisionId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(Func<IQueryable<NpcCorporationDivisionEntity>, IQueryable<NpcCorporationDivisionEntity>> queryOperations)
    {
      return this.GetNpcCorporationDivisions(new QueryTransform<NpcCorporationDivisionEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(NpcCorporationDivision))]
    public IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<NpcCorporationDivisionEntity, NpcCorporationDivision>(this.Context.NpcCorporationDivisions, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId, out NpcCorporationDivision value)
    {
      if (this.Cache.TryGetValue<NpcCorporationDivision>(NpcCorporationDivisionEntity.CreateCacheKey(corporationId, divisionId), out value))
      {
        return true;
      }

      value = this.GetNpcCorporationDivisions(q => q.Where(x => x.CorporationId == corporationId.Value && x.DivisionId == divisionId)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Race Methods
    /// <inheritdoc />
    public Race GetRaceById(RaceId id)
    {
      Race result;

      if (!this.TryGetRaceById(id, out result))
      {
        throw new InvalidOperationException("No Race with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Race> GetRaces(Func<IQueryable<RaceEntity>, IQueryable<RaceEntity>> queryOperations)
    {
      return this.GetRaces(new QueryTransform<RaceEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Race))]
    public IReadOnlyList<Race> GetRaces(params IQueryModifier<RaceEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<RaceEntity, Race>(this.Context.Races, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetRaceById(RaceId id, out Race value)
    {
      if (this.Cache.TryGetValue<Race>(id, out value))
      {
        return true;
      }

      value = this.GetRaces(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Region Methods
    /// <inheritdoc />
    public Region GetRegionById(RegionId id)
    {
      Region result;

      if (!this.TryGetRegionById(id, out result))
      {
        throw new InvalidOperationException("No Region with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Region> GetRegions(Func<IQueryable<RegionEntity>, IQueryable<RegionEntity>> queryOperations)
    {
      return this.GetRegions(new QueryTransform<RegionEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Region))]
    public IReadOnlyList<Region> GetRegions(params IQueryModifier<RegionEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<RegionEntity, Region>(this.Context.Regions, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetRegionById(RegionId id, out Region value)
    {
      if (this.Cache.TryGetValue<Region>(id, out value))
      {
        return true;
      }

      value = this.GetRegions(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region RegionJump Methods
    /// <inheritdoc />
    public RegionJump GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId)
    {
      RegionJump result;

      if (!this.TryGetRegionJumpById(fromRegionId, toRegionId, out result))
      {
        throw new InvalidOperationException("No RegionJump with ID (" + fromRegionId.ToString() + ", " + toRegionId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<RegionJump> GetRegionJumps(Func<IQueryable<RegionJumpEntity>, IQueryable<RegionJumpEntity>> queryOperations)
    {
      return this.GetRegionJumps(new QueryTransform<RegionJumpEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(RegionJump))]
    public IReadOnlyList<RegionJump> GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<RegionJumpEntity, RegionJump>(this.Context.RegionJumps, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetRegionJumpById(RegionId fromRegionId, RegionId toRegionId, out RegionJump value)
    {
      if (this.Cache.TryGetValue<RegionJump>(RegionJumpEntity.CreateCacheKey(fromRegionId, toRegionId), out value))
      {
        return true;
      }

      value = this.GetRegionJumps(q => q.Where(x => x.FromRegionId == fromRegionId.Value && x.ToRegionId == toRegionId.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region SolarSystem Methods
    /// <inheritdoc />
    public SolarSystem GetSolarSystemById(SolarSystemId id)
    {
      SolarSystem result;

      if (!this.TryGetSolarSystemById(id, out result))
      {
        throw new InvalidOperationException("No SolarSystem with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<SolarSystem> GetSolarSystems(Func<IQueryable<SolarSystemEntity>, IQueryable<SolarSystemEntity>> queryOperations)
    {
      return this.GetSolarSystems(new QueryTransform<SolarSystemEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(SolarSystem))]
    public IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<SolarSystemEntity, SolarSystem>(this.Context.SolarSystems, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetSolarSystemById(SolarSystemId id, out SolarSystem value)
    {
      if (this.Cache.TryGetValue<SolarSystem>(id, out value))
      {
        return true;
      }

      value = this.GetSolarSystems(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region SolarSystemJump Methods
    /// <inheritdoc />
    public SolarSystemJump GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId)
    {
      SolarSystemJump result;

      if (!this.TryGetSolarSystemJumpById(fromSolarSystemId, toSolarSystemId, out result))
      {
        throw new InvalidOperationException("No SolarSystemJump with ID (" + fromSolarSystemId.ToString() + ", " + toSolarSystemId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(Func<IQueryable<SolarSystemJumpEntity>, IQueryable<SolarSystemJumpEntity>> queryOperations)
    {
      return this.GetSolarSystemJumps(new QueryTransform<SolarSystemJumpEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(SolarSystemJump))]
    public IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<SolarSystemJumpEntity, SolarSystemJump>(this.Context.SolarSystemJumps, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId, out SolarSystemJump value)
    {
      if (this.Cache.TryGetValue<SolarSystemJump>(SolarSystemJumpEntity.CreateCacheKey(fromSolarSystemId, toSolarSystemId), out value))
      {
        return true;
      }

      value = this.GetSolarSystemJumps(q => q.Where(x => x.FromSolarSystemId == fromSolarSystemId.Value && x.ToSolarSystemId == toSolarSystemId.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Stargate Methods
    /// <inheritdoc />
    public Stargate GetStargateById(StargateId id)
    {
      Stargate result;

      if (!this.TryGetStargateById(id, out result))
      {
        throw new InvalidOperationException("No Stargate with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Stargate> GetStargates(Func<IQueryable<StargateEntity>, IQueryable<StargateEntity>> queryOperations)
    {
      return this.GetStargates(new QueryTransform<StargateEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Stargate))]
    public IReadOnlyList<Stargate> GetStargates(params IQueryModifier<StargateEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<StargateEntity, Stargate>(this.Context.Stargates, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetStargateById(StargateId id, out Stargate value)
    {
      if (this.Cache.TryGetValue<Stargate>(id, out value))
      {
        return true;
      }

      value = this.GetStargates(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Station Methods
    /// <inheritdoc />
    public Station GetStationById(StationId id)
    {
      Station result;

      if (!this.TryGetStationById(id, out result))
      {
        throw new InvalidOperationException("No Station with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Station> GetStations(Func<IQueryable<StationEntity>, IQueryable<StationEntity>> queryOperations)
    {
      return this.GetStations(new QueryTransform<StationEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Station))]
    public IReadOnlyList<Station> GetStations(params IQueryModifier<StationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<StationEntity, Station>(this.Context.Stations, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetStationById(StationId id, out Station value)
    {
      if (this.Cache.TryGetValue<Station>(id, out value))
      {
        return true;
      }

      value = this.GetStations(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region StationOperation Methods
    /// <inheritdoc />
    public StationOperation GetStationOperationById(StationOperationId id)
    {
      StationOperation result;

      if (!this.TryGetStationOperationById(id, out result))
      {
        throw new InvalidOperationException("No StationOperation with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<StationOperation> GetStationOperations(Func<IQueryable<StationOperationEntity>, IQueryable<StationOperationEntity>> queryOperations)
    {
      return this.GetStationOperations(new QueryTransform<StationOperationEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(StationOperation))]
    public IReadOnlyList<StationOperation> GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<StationOperationEntity, StationOperation>(this.Context.StationOperations, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetStationOperationById(StationOperationId id, out StationOperation value)
    {
      if (this.Cache.TryGetValue<StationOperation>(id, out value))
      {
        return true;
      }

      value = this.GetStationOperations(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region StationService Methods
    /// <inheritdoc />
    public StationService GetStationServiceById(StationServiceId id)
    {
      StationService result;

      if (!this.TryGetStationServiceById(id, out result))
      {
        throw new InvalidOperationException("No StationService with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<StationService> GetStationServices(Func<IQueryable<StationServiceEntity>, IQueryable<StationServiceEntity>> queryOperations)
    {
      return this.GetStationServices(new QueryTransform<StationServiceEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(StationService))]
    public IReadOnlyList<StationService> GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<StationServiceEntity, StationService>(this.Context.StationServices, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetStationServiceById(StationServiceId id, out StationService value)
    {
      if (this.Cache.TryGetValue<StationService>(id, out value))
      {
        return true;
      }

      value = this.GetStationServices(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region StationType Methods
    /// <inheritdoc />
    public StationType GetStationTypeById(TypeId id)
    {
      StationType result;

      if (!this.TryGetStationTypeById(id, out result))
      {
        throw new InvalidOperationException("No StationType with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<StationType> GetStationTypes(Func<IQueryable<StationTypeEntity>, IQueryable<StationTypeEntity>> queryOperations)
    {
      return this.GetStationTypes(new QueryTransform<StationTypeEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(StationType))]
    public IReadOnlyList<StationType> GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<StationTypeEntity, StationType>(this.Context.StationTypes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetStationTypeById(TypeId id, out StationType value)
    {
      if (this.Cache.TryGetValue<StationType>(id, out value))
      {
        return true;
      }

      value = this.GetStationTypes(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region TypeMaterial Methods
    /// <inheritdoc />
    public TypeMaterial GetTypeMaterialById(TypeId typeId, TypeId materialTypeId)
    {
      TypeMaterial result;

      if (!this.TryGetTypeMaterialById(typeId, materialTypeId, out result))
      {
        throw new InvalidOperationException("No TypeMaterial with ID (" + typeId.ToString() + ", " + materialTypeId.ToString() + ") could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<TypeMaterial> GetTypeMaterials(Func<IQueryable<TypeMaterialEntity>, IQueryable<TypeMaterialEntity>> queryOperations)
    {
      return this.GetTypeMaterials(new QueryTransform<TypeMaterialEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(TypeMaterial))]
    public IReadOnlyList<TypeMaterial> GetTypeMaterials(params IQueryModifier<TypeMaterialEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<TypeMaterialEntity, TypeMaterial>(this.Context.TypeMaterials, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetTypeMaterialById(TypeId typeId, TypeId materialTypeId, out TypeMaterial value)
    {
      if (this.Cache.TryGetValue<TypeMaterial>(TypeMaterialEntity.CreateCacheKey(typeId, materialTypeId), out value))
      {
        return true;
      }

      value = this.GetTypeMaterials(q => q.Where(x => x.TypeId == typeId.Value && x.MaterialTypeId == materialTypeId.Value)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Unit Methods
    /// <inheritdoc />
    public Unit GetUnitById(UnitId id)
    {
      Unit result;

      if (!this.TryGetUnitById(id, out result))
      {
        throw new InvalidOperationException("No Unit with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Unit> GetUnits(Func<IQueryable<UnitEntity>, IQueryable<UnitEntity>> queryOperations)
    {
      return this.GetUnits(new QueryTransform<UnitEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Unit))]
    public IReadOnlyList<Unit> GetUnits(params IQueryModifier<UnitEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<UnitEntity, Unit>(this.Context.Units, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetUnitById(UnitId id, out Unit value)
    {
      if (this.Cache.TryGetValue<Unit>(id, out value))
      {
        return true;
      }

      value = this.GetUnits(q => q.Where(x => x.Id == id)).SingleOrDefault();
      return value != null;
    }
    #endregion

    #region Universe Methods
    /// <inheritdoc />
    public Universe GetUniverseById(UniverseId id)
    {
      Universe result;

      if (!this.TryGetUniverseById(id, out result))
      {
        throw new InvalidOperationException("No Universe with ID " + id.ToString() + " could be found.");
      }

      return result;
    }

    /// <inheritdoc />
    public IReadOnlyList<Universe> GetUniverses(Func<IQueryable<UniverseEntity>, IQueryable<UniverseEntity>> queryOperations)
    {
      return this.GetUniverses(new QueryTransform<UniverseEntity>(queryOperations));
    }

    /// <inheritdoc />
    [EveQueryMethod(typeof(Universe))]
    public IReadOnlyList<Universe> GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers)
    {
      // Construct the result set, filtering items through the global cache along the way
      return LoadAndCacheResults<UniverseEntity, Universe>(this.Context.Universes, modifiers);
    }

    /// <inheritdoc />
    public bool TryGetUniverseById(UniverseId id, out Universe value)
    {
      if (this.Cache.TryGetValue<Universe>(id, out value))
      {
        return true;
      }

      value = this.GetUniverses(q => q.Where(x => x.Id == id.Value)).SingleOrDefault();
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
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    protected internal IReadOnlyList<TAdapter> LoadAndCacheResults<TEntity, TAdapter>(
      IQueryable<TEntity> query,
      IQueryModifier<TEntity>[] modifiers)
      where TEntity : IEveEntity<TAdapter>
      where TAdapter : IEveCacheable
    {
      Contract.Requires(query != null, "The base query cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TAdapter>>() != null);

      lock (this.queryLock)
      {
        var results = GetResults(query, modifiers);
        return results.Select(x => this.Cache.GetOrAdd<TAdapter>(x.CacheKey, () => x.ToAdapter(this))).ToArray();
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
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    protected internal IReadOnlyList<TResult> LoadAndCacheResults<TEntity, TAdapter, TResult>(
      IQueryable<TEntity> query,
      IQueryModifier<TEntity>[] modifiers)
      where TEntity : IEveEntity<TAdapter>
      where TAdapter : IEveCacheable
    {
      Contract.Requires(query != null, "The base query cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TResult>>() != null);

      lock (this.queryLock)
      {
        var results = GetResults(query, modifiers);
        return results.Select(x => this.Cache.GetOrAdd<TAdapter>(x.CacheKey, () => x.ToAdapter(this))).OfType<TResult>().ToArray();
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
      Contract.Ensures(Contract.Result<IEnumerable<TEntity>>() != null);

      // Apply the modifiers
      if (modifiers != null)
      {
        foreach (IQueryModifier<TEntity> modifier in modifiers)
        {
          Contract.Assume(modifier != null);
          query = modifier.GetResults(query);
        }
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