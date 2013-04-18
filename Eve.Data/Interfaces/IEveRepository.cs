//-----------------------------------------------------------------------
// <copyright file="IEveRepository.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq.Expressions;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Industry;
  using Eve.Universe;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for a data source which provides access to the EVE
  /// database.
  /// </summary>
  [ContractClass(typeof(IEveRepositoryContracts))]
  public interface IEveRepository : IDisposable
  {
    /* Methods */
   
    /// <summary>
    /// Retrieves any previously stored object with the specified type and
    /// ID value, or creates and stores a value if no stored version already
    /// exists.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to retrieve from or store in the repository.
    /// </typeparam>
    /// <param name="id">
    /// The ID to attempt to retrieve from the repository.
    /// </param>
    /// <param name="valueFactory">
    /// A delegate which can be used to create a value to store in the
    /// repository, in the event that no previously stored version exists.
    /// If a previously stored version does exist, this delegate is never
    /// invoked.  The value returned by the delegate must implement
    /// <see cref="IEveCacheable" />, and the value of its
    /// <see cref="IEveCacheable.CacheKey" /> property must be equal to
    /// <paramref name="id" />.
    /// </param>
    /// <returns>
    /// The previously stored object with the specified type and ID, if one
    /// exists.  Otherwise, the value returned by the 
    /// <paramref name="valueFactory" /> delegate, after it has been 
    /// stored in the repository.
    /// </returns>
    T GetOrAdd<T>(IConvertible id, Func<T> valueFactory) where T : IEveCacheable;

    #region Activity Methods
    /// <summary>
    /// Returns the <see cref="Activity" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Activity GetActivityById(ActivityId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Activity" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Activity> GetActivities(Expression<Func<ActivityEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Activity" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Activity> GetActivities(params IQueryModifier<ActivityEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Activity" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetActivityById(ActivityId id, out Activity value);
    #endregion

    #region Agent Methods
    /// <summary>
    /// Returns the <see cref="Agent" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Agent GetAgentById(AgentId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Agent" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Agent> GetAgents(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Agent" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Agent> GetAgents(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Agent" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAgentById(AgentId id, out Agent value);
    #endregion

    #region AgentType Methods
    /// <summary>
    /// Returns the <see cref="AgentType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AgentType GetAgentTypeById(AgentTypeId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AgentType" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AgentType> GetAgentTypes(Expression<Func<AgentTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AgentType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AgentType> GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AgentType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAgentTypeById(AgentTypeId id, out AgentType value);
    #endregion

    #region Ancestry Methods
    /// <summary>
    /// Returns the <see cref="Ancestry" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Ancestry GetAncestryById(AncestryId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Ancestry" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Ancestry> GetAncestries(Expression<Func<AncestryEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Ancestry" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Ancestry> GetAncestries(params IQueryModifier<AncestryEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Ancestry" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAncestryById(AncestryId id, out Ancestry value);
    #endregion

    #region AssemblyLine Methods
    /// <summary>
    /// Returns the <see cref="AssemblyLine" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AssemblyLine GetAssemblyLineById(AssemblyLineId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLine" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLine> GetAssemblyLines(Expression<Func<AssemblyLineEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLine" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLine> GetAssemblyLines(params IQueryModifier<AssemblyLineEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AssemblyLine" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAssemblyLineById(AssemblyLineId id, out AssemblyLine value);
    #endregion

    #region AssemblyLineStation Methods
    /// <summary>
    /// Returns the <see cref="AssemblyLineStation" /> object with the specified ID.
    /// </summary>
    /// <param name="stationId">
    /// The ID of the station.
    /// </param>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AssemblyLineStation GetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineStation" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineStation> GetAssemblyLineStations(Expression<Func<AssemblyLineStationEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineStation" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineStation> GetAssemblyLineStations(params IQueryModifier<AssemblyLineStationEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AssemblyLineStation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="stationId">
    /// The ID of the station.
    /// </param>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId, out AssemblyLineStation value);
    #endregion

    #region AssemblyLineType Methods
    /// <summary>
    /// Returns the <see cref="AssemblyLineType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AssemblyLineType GetAssemblyLineTypeById(AssemblyLineTypeId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineType" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineType> GetAssemblyLineTypes(Expression<Func<AssemblyLineTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineType> GetAssemblyLineTypes(params IQueryModifier<AssemblyLineTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AssemblyLineType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAssemblyLineTypeById(AssemblyLineTypeId id, out AssemblyLineType value);
    #endregion

    #region AssemblyLineTypeCategoryDetail Methods
    /// <summary>
    /// Returns the <see cref="AssemblyLineTypeCategoryDetail" /> object with the specified ID.
    /// </summary>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="categoryId">
    /// The ID of the category.
    /// </param>
    /// <returns>
    /// The item with the specified key values.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AssemblyLineTypeCategoryDetail GetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineTypeCategoryDetail" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineTypeCategoryDetail> GetAssemblyLineTypeCategoryDetails(Expression<Func<AssemblyLineTypeCategoryDetailEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineTypeCategoryDetail" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineTypeCategoryDetail> GetAssemblyLineTypeCategoryDetails(params IQueryModifier<AssemblyLineTypeCategoryDetailEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AssemblyLineTypeCategoryDetail" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="categoryId">
    /// The ID of the category.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId, out AssemblyLineTypeCategoryDetail value);
    #endregion

    #region AssemblyLineTypeGroupDetail Methods
    /// <summary>
    /// Returns the <see cref="AssemblyLineTypeGroupDetail" /> object with the specified ID.
    /// </summary>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="groupId">
    /// The ID of the group.
    /// </param>
    /// <returns>
    /// The item with the specified key values.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AssemblyLineTypeGroupDetail GetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineTypeGroupDetail" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineTypeGroupDetail> GetAssemblyLineTypeGroupDetails(Expression<Func<AssemblyLineTypeGroupDetailEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineTypeGroupDetail" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AssemblyLineTypeGroupDetail> GetAssemblyLineTypeGroupDetails(params IQueryModifier<AssemblyLineTypeGroupDetailEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AssemblyLineTypeGroupDetail" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="groupId">
    /// The ID of the group.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId, out AssemblyLineTypeGroupDetail value);
    #endregion

    #region AttributeCategory Methods
    /// <summary>
    /// Returns the <see cref="AttributeCategory" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AttributeCategory GetAttributeCategoryById(AttributeCategoryId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeCategory" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeCategory> GetAttributeCategories(Expression<Func<AttributeCategoryEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeCategory" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeCategory> GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AttributeCategory" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAttributeCategoryById(AttributeCategoryId id, out AttributeCategory value);
    #endregion

    #region AttributeType Methods
    /// <summary>
    /// Returns the <see cref="AttributeType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AttributeType GetAttributeTypeById(AttributeId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeType" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeType> GetAttributeTypes(Expression<Func<AttributeTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeType> GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AttributeType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAttributeTypeById(AttributeId id, out AttributeType value);
    #endregion

    #region AttributeValue Methods
    /// <summary>
    /// Returns the <see cref="AttributeValue" /> object with the specified ID.
    /// </summary>
    /// <param name="itemTypeId">
    /// The type ID of the attribute to return.
    /// </param>
    /// <param name="attributeId">
    /// The ID of the attribute to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    AttributeValue GetAttributeValueById(TypeId itemTypeId, AttributeId attributeId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeValue" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeValue> GetAttributeValues(Expression<Func<AttributeValueEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeValue" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeValue> GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="AttributeValue" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="itemTypeId">
    /// The type ID of the attribute to return.
    /// </param>
    /// <param name="attributeId">
    /// The ID of the attribute to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetAttributeValueById(TypeId itemTypeId, AttributeId attributeId, out AttributeValue value);
    #endregion

    #region Bloodline Methods
    /// <summary>
    /// Returns the <see cref="Bloodline" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Bloodline GetBloodlineById(BloodlineId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Bloodline" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Bloodline> GetBloodlines(Expression<Func<BloodlineEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Bloodline" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Bloodline> GetBloodlines(params IQueryModifier<BloodlineEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Bloodline" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetBloodlineById(BloodlineId id, out Bloodline value);
    #endregion

    #region Category Methods
    /// <summary>
    /// Returns the <see cref="Category" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Category GetCategoryById(CategoryId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Category" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Category> GetCategories(Expression<Func<CategoryEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Category" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Category> GetCategories(params IQueryModifier<CategoryEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Category" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetCategoryById(CategoryId id, out Category value);
    #endregion

    #region Celestial Methods
    /// <summary>
    /// Returns the <see cref="Celestial" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Celestial GetCelestialById(CelestialId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Celestial" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Celestial> GetCelestials(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Celestial" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Celestial> GetCelestials(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Celestial" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetCelestialById(CelestialId id, out Celestial value);
    #endregion

    #region CharacterAttributeType Methods
    /// <summary>
    /// Returns the <see cref="CharacterAttributeType" /> object with the
    /// specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    CharacterAttributeType GetCharacterAttributeTypeById(CharacterAttributeId id);

    /// <summary>
    /// Returns the results of the specified query for
    /// <see cref="CharacterAttributeType" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(Expression<Func<CharacterAttributeTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for 
    /// <see cref="CharacterAttributeType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="CharacterAttributeType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetCharacterAttributeTypeById(CharacterAttributeId id, out CharacterAttributeType value);
    #endregion

    #region Constellation Methods
    /// <summary>
    /// Returns the <see cref="Constellation" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Constellation GetConstellationById(ConstellationId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Constellation" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Constellation> GetConstellations(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Constellation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Constellation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetConstellationById(ConstellationId id, out Constellation value);
    #endregion

    #region ConstellationJump Methods
    /// <summary>
    /// Returns the <see cref="ConstellationJump" /> object with the specified IDs.
    /// </summary>
    /// <param name="fromConstellationId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// <param name="toConstellationId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    ConstellationJump GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ConstellationJump" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<ConstellationJump> GetConstellationJumps(Expression<Func<ConstellationJumpEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ConstellationJump" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<ConstellationJump> GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="ConstellationJump" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="fromConstellationId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// <param name="toConstellationId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId, out ConstellationJump value);
    #endregion

    #region CorporateActivity Methods
    /// <summary>
    /// Returns the <see cref="CorporateActivity" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    CorporateActivity GetCorporateActivityById(CorporateActivityId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="CorporateActivity" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CorporateActivity> GetCorporateActivities(Expression<Func<CorporateActivityEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="CorporateActivity" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CorporateActivity> GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="CorporateActivity" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetCorporateActivityById(CorporateActivityId id, out CorporateActivity value);
    #endregion

    #region Division Methods
    /// <summary>
    /// Returns the <see cref="Division" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Division GetDivisionById(DivisionId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Division" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Division> GetDivisions(Expression<Func<DivisionEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Division" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Division> GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Division" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetDivisionById(DivisionId id, out Division value);
    #endregion

    #region Effect Methods
    /// <summary>
    /// Returns the <see cref="Effect" /> object with the specified ID.
    /// </summary>
    /// <param name="itemTypeId">
    /// The type ID of the effect to return.
    /// </param>
    /// <param name="effectId">
    /// The ID of the effect to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Effect GetEffectById(TypeId itemTypeId, EffectId effectId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Effect" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Effect> GetEffects(Expression<Func<EffectEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Effect" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Effect> GetEffects(params IQueryModifier<EffectEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Constellation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="itemTypeId">
    /// The type ID of the effect to return.
    /// </param>
    /// <param name="effectId">
    /// The ID of the effect to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetEffectById(TypeId itemTypeId, EffectId effectId, out Effect value);
    #endregion

    #region EffectType Methods
    /// <summary>
    /// Returns the <see cref="EffectType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    EffectType GetEffectTypeById(EffectId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="EffectType" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EffectType> GetEffectTypes(Expression<Func<EffectTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="EffectType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EffectType> GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="EffectType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetEffectTypeById(EffectId id, out EffectType value);
    #endregion

    #region EveType Methods
    /// <summary>
    /// Returns the <see cref="EveType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    EveType GetEveTypeById(TypeId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EveType> GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EveType> GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="EveType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetEveTypeById(TypeId id, out EveType value);

    /// <summary>
    /// Returns the <see cref="EveType" /> object of the desired type and
    /// with the specified ID.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of type
    /// <typeparamref name="TEveType" />.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    TEveType GetEveTypeById<TEveType>(TypeId id) where TEveType : EveType;

    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects
    /// of the desired type.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TEveType" />
    /// will be returned.  Any matching items of other types will be omitted.
    /// </returns>
    IReadOnlyList<TEveType> GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter) where TEveType : EveType;

    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects
    /// of the desired type.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TEveType" />
    /// will be returned.  Any matching items of other types will be omitted.
    /// </returns>
    IReadOnlyList<TEveType> GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers) where TEveType : EveType;

    /// <summary>
    /// Attempts to retrieve the <see cref="EveType" /> object of the desired
    /// type and with the specified ID, returning success or failure.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item of the desired type is
    /// found; otherwise <see langword="false" />.
    /// </returns>
    bool TryGetEveTypeById<TEveType>(TypeId id, out TEveType value) where TEveType : EveType;
    #endregion

    #region Faction Methods
    /// <summary>
    /// Returns the <see cref="Faction" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Faction GetFactionById(FactionId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Faction" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Faction> GetFactions(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Faction" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Faction> GetFactions(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Faction" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetFactionById(FactionId id, out Faction value);
    #endregion

    #region Graphic Methods
    /// <summary>
    /// Returns the <see cref="Graphic" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Graphic GetGraphicById(GraphicId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Graphic" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Graphic> GetGraphics(Expression<Func<GraphicEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Graphic" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Graphic> GetGraphics(params IQueryModifier<GraphicEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Graphic" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetGraphicById(GraphicId id, out Graphic value);
    #endregion

    #region Group Methods
    /// <summary>
    /// Returns the <see cref="Group" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Group GetGroupById(GroupId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Group" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Group> GetGroups(Expression<Func<GroupEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Group" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Group> GetGroups(params IQueryModifier<GroupEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Group" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetGroupById(GroupId id, out Group value);
    #endregion

    #region Icon Methods
    /// <summary>
    /// Returns the <see cref="Icon" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Icon GetIconById(IconId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Icon" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Icon> GetIcons(Expression<Func<IconEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Icon" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Icon> GetIcons(params IQueryModifier<IconEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Icon" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetIconById(IconId id, out Icon value);
    #endregion

    #region Item Methods
    /// <summary>
    /// Returns the <see cref="Item" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Item GetItemById(ItemId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Item> GetItems(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Item> GetItems(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Item" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetItemById(ItemId id, out Item value);

    /// <summary>
    /// Returns the <see cref="Item" /> object of the desired type and
    /// with the specified ID.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of the item to retrieve.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the desired type.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    TItem GetItemById<TItem>(ItemId id) where TItem : Item;

    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects
    /// of the desired type.
    /// </summary>
    /// <typeparam name="TItem">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TItem" />
    /// will be returned.  Any matching items of other types will be omitted.
    /// </returns>
    IReadOnlyList<TItem> GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter) where TItem : Item;

    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects
    /// of the desired type.
    /// </summary>
    /// <typeparam name="TItem">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TItem" />
    /// will be returned.  Any matching items of other types will be omitted.
    /// </returns>
    IReadOnlyList<TItem> GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers) where TItem : Item;

    /// <summary>
    /// Attempts to retrieve the <see cref="Item" /> object of the desired
    /// type and with the specified ID, returning success or failure.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of the item to return.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item of the desired type
    /// is found; otherwise <see langword="false" />.
    /// </returns>
    bool TryGetItemById<TItem>(ItemId id, out TItem value) where TItem : Item;
    #endregion

    #region ItemPosition Methods
    /// <summary>
    /// Returns the <see cref="ItemPosition" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    ItemPosition GetItemPositionById(ItemId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ItemPosition" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<ItemPosition> GetItemPositions(Expression<Func<ItemPositionEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ItemPosition" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<ItemPosition> GetItemPositions(params IQueryModifier<ItemPositionEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="ItemPosition" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetItemPositionById(ItemId id, out ItemPosition value);
    #endregion

    #region MarketGroup Methods
    /// <summary>
    /// Returns the <see cref="MarketGroup" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    MarketGroup GetMarketGroupById(MarketGroupId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MarketGroup" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MarketGroup> GetMarketGroups(Expression<Func<MarketGroupEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MarketGroup" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MarketGroup> GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="MarketGroup" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetMarketGroupById(MarketGroupId id, out MarketGroup value);
    #endregion

    #region MetaGroup Methods
    /// <summary>
    /// Returns the <see cref="MetaGroup" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    MetaGroup GetMetaGroupById(MetaGroupId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaGroup" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MetaGroup> GetMetaGroups(Expression<Func<MetaGroupEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaGroup" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MetaGroup> GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="MetaGroup" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetMetaGroupById(MetaGroupId id, out MetaGroup value);
    #endregion

    #region MetaType Methods
    /// <summary>
    /// Returns the <see cref="MetaType" /> object with the specified ID.
    /// </summary>
    /// <param name="typeId">
    /// The ID of the type whose meta type to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    MetaType GetMetaTypeById(TypeId typeId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaType" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MetaType> GetMetaTypes(Expression<Func<MetaTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MetaType> GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="MetaType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetMetaTypeById(TypeId id, out MetaType value);
    #endregion

    #region NpcCorporation Methods
    /// <summary>
    /// Returns the <see cref="NpcCorporation" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    NpcCorporation GetNpcCorporationById(NpcCorporationId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporation> GetNpcCorporations(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="NpcCorporation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetNpcCorporationById(NpcCorporationId id, out NpcCorporation value);
    #endregion

    #region NpcCorporationDivision Methods
    /// <summary>
    /// Returns the <see cref="NpcCorporationDivision" /> object with the specified ID.
    /// </summary>
    /// <param name="corporationId">
    /// The ID of the corporation.
    /// </param>
    /// <param name="divisionId">
    /// The ID of the division.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    NpcCorporationDivision GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporationDivision" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(Expression<Func<NpcCorporationDivisionEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="NpcCorporationDivision" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="corporationId">
    /// The ID of the corporation.
    /// </param>
    /// <param name="divisionId">
    /// The ID of the division.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId, out NpcCorporationDivision value);
    #endregion

    #region Race Methods
    /// <summary>
    /// Returns the <see cref="Race" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Race GetRaceById(RaceId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Race" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Race> GetRaces(Expression<Func<RaceEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Race" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Race> GetRaces(params IQueryModifier<RaceEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Race" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetRaceById(RaceId id, out Race value);
    #endregion

    #region Region Methods
    /// <summary>
    /// Returns the <see cref="Region" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Region GetRegionById(RegionId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Region" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Region> GetRegions(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Region" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Region> GetRegions(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Region" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetRegionById(RegionId id, out Region value);
    #endregion

    #region RegionJump Methods
    /// <summary>
    /// Returns the <see cref="RegionJump" /> object with the specified IDs.
    /// </summary>
    /// <param name="fromRegionId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// <param name="toRegionId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    RegionJump GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="RegionJump" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<RegionJump> GetRegionJumps(Expression<Func<RegionJumpEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="RegionJump" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<RegionJump> GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="RegionJump" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="fromRegionId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// <param name="toRegionId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetRegionJumpById(RegionId fromRegionId, RegionId toRegionId, out RegionJump value);
    #endregion

    #region SolarSystem Methods
    /// <summary>
    /// Returns the <see cref="SolarSystem" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    SolarSystem GetSolarSystemById(SolarSystemId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystem" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystem> GetSolarSystems(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystem" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="SolarSystem" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetSolarSystemById(SolarSystemId id, out SolarSystem value);
    #endregion

    #region SolarSystemJump Methods
    /// <summary>
    /// Returns the <see cref="SolarSystemJump" /> object with the specified IDs.
    /// </summary>
    /// <param name="fromSolarSystemId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// <param name="toSolarSystemId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    SolarSystemJump GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId);

    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystemJump" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(Expression<Func<SolarSystemJumpEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystemJump" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="SolarSystemJump" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="fromSolarSystemId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// <param name="toSolarSystemId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId, out SolarSystemJump value);
    #endregion

    #region Stargate Methods
    /// <summary>
    /// Returns the <see cref="Stargate" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Stargate GetStargateById(StargateId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Stargate" />
    /// objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Stargate> GetStargates(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Stargate" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Stargate> GetStargates(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Stargate" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetStargateById(StargateId id, out Stargate value);
    #endregion

    #region Station Methods
    /// <summary>
    /// Returns the <see cref="Station" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Station GetStationById(StationId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Station" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Station> GetStations(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Station" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Station> GetStations(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Station" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetStationById(StationId id, out Station value);
    #endregion

    #region StationOperation Methods
    /// <summary>
    /// Returns the <see cref="StationOperation" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    StationOperation GetStationOperationById(StationOperationId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationOperation" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationOperation> GetStationOperations(Expression<Func<StationOperationEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationOperation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationOperation> GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="StationOperation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetStationOperationById(StationOperationId id, out StationOperation value);
    #endregion

    #region StationService Methods
    /// <summary>
    /// Returns the <see cref="StationService" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    StationService GetStationServiceById(StationServiceId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationService" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationService> GetStationServices(Expression<Func<StationServiceEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationService" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationService> GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="StationService" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetStationServiceById(StationServiceId id, out StationService value);
    #endregion

    #region StationType Methods
    /// <summary>
    /// Returns the <see cref="StationType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    StationType GetStationTypeById(TypeId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationType" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationType> GetStationTypes(Expression<Func<StationTypeEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationType> GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="StationType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetStationTypeById(TypeId id, out StationType value);
    #endregion

    #region Unit Methods
    /// <summary>
    /// Returns the <see cref="Unit" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Unit GetUnitById(UnitId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Unit" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Unit> GetUnits(Expression<Func<UnitEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Unit" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Unit> GetUnits(params IQueryModifier<UnitEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Unit" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetUnitById(UnitId id, out Unit value);
    #endregion

    #region Universe Methods
    /// <summary>
    /// Returns the <see cref="Universe" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// <remarks>
    /// <para>
    /// When retrieving a single item, this method should be used whenever
    /// possible, because retrieving by the ID value allows a cached version
    /// of the item to be returned without requiring a database query,
    /// dramatically increasing performance.
    /// </para>
    /// </remarks>
    Universe GetUniverseById(UniverseId id);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Universe" /> objects.
    /// </summary>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Universe> GetUniverses(Expression<Func<ItemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Universe" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Universe> GetUniverses(params IQueryModifier<ItemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Universe" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID,
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetUniverseById(UniverseId id, out Universe value);
    #endregion
  }
}