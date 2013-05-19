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
  using System.Linq;
  using System.Linq.Expressions;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Industry;
  using Eve.Universe;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for classes which provide high-level access to
  /// information from the EVE database.
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
    T GetOrAddStoredValue<T>(IConvertible id, Func<T> valueFactory) where T : IEveCacheable;

    #region Activity Methods
    /// <summary>
    /// Returns the <see cref="Activity" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Activity" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Activity> GetActivities(Func<IQueryable<ActivityEntity>, IQueryable<ActivityEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Activity" />
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Agent" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Agent> GetAgents(Func<IQueryable<AgentEntity>, IQueryable<AgentEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Agent" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Agent> GetAgents(params IQueryModifier<AgentEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Agent" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="AgentType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AgentType> GetAgentTypes(Func<IQueryable<AgentTypeEntity>, IQueryable<AgentTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AgentType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Ancestry" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Ancestry> GetAncestries(Func<IQueryable<AncestryEntity>, IQueryable<AncestryEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Ancestry" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="AssemblyLine" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AssemblyLine> GetAssemblyLines(Func<IQueryable<AssemblyLineEntity>, IQueryable<AssemblyLineEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLine" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="AssemblyLineStation" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AssemblyLineStation> GetAssemblyLineStations(Func<IQueryable<AssemblyLineStationEntity>, IQueryable<AssemblyLineStationEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineStation" />
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="AssemblyLineType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AssemblyLineType> GetAssemblyLineTypes(Func<IQueryable<AssemblyLineTypeEntity>, IQueryable<AssemblyLineTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// Returns all <see cref="AssemblyLineTypeCategoryDetail" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AssemblyLineTypeCategoryDetail> GetAssemblyLineTypeCategoryDetails(Func<IQueryable<AssemblyLineTypeCategoryDetailEntity>, IQueryable<AssemblyLineTypeCategoryDetailEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineTypeCategoryDetail" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// Returns all <see cref="AssemblyLineTypeGroupDetail" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AssemblyLineTypeGroupDetail> GetAssemblyLineTypeGroupDetails(Func<IQueryable<AssemblyLineTypeGroupDetailEntity>, IQueryable<AssemblyLineTypeGroupDetailEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AssemblyLineTypeGroupDetail" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="AttributeCategory" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AttributeCategory> GetAttributeCategories(Func<IQueryable<AttributeCategoryEntity>, IQueryable<AttributeCategoryEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeCategory" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="AttributeType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AttributeType> GetAttributeTypes(Func<IQueryable<AttributeTypeEntity>, IQueryable<AttributeTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    AttributeValue GetAttributeValueById(EveTypeId itemTypeId, AttributeId attributeId);

    /// <summary>
    /// Returns all <see cref="AttributeValue" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<AttributeValue> GetAttributeValues(Func<IQueryable<AttributeValueEntity>, IQueryable<AttributeValueEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeValue" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetAttributeValueById(EveTypeId itemTypeId, AttributeId attributeId, out AttributeValue value);
    #endregion

    #region Bloodline Methods
    /// <summary>
    /// Returns the <see cref="Bloodline" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Bloodline" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Bloodline> GetBloodlines(Func<IQueryable<BloodlineEntity>, IQueryable<BloodlineEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Bloodline" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetBloodlineById(BloodlineId id, out Bloodline value);
    #endregion

    #region BlueprintType Methods
    /// <summary>
    /// Returns the <see cref="BlueprintType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    BlueprintType GetBlueprintTypeById(EveTypeId id);

    /// <summary>
    /// Returns all <see cref="BlueprintType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<BlueprintType> GetBlueprintTypes(Func<IQueryable<BlueprintTypeEntity>, IQueryable<BlueprintTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="BlueprintType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<BlueprintType> GetBlueprintTypes(params IQueryModifier<BlueprintTypeEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="BlueprintType" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetBlueprintTypeById(EveTypeId id, out BlueprintType value);
    #endregion

    #region Category Methods
    /// <summary>
    /// Returns the <see cref="Category" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Category" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Category> GetCategories(Func<IQueryable<CategoryEntity>, IQueryable<CategoryEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Category" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Celestial" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Celestial> GetCelestials(Func<IQueryable<CelestialEntity>, IQueryable<CelestialEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Celestial" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Celestial> GetCelestials(params IQueryModifier<CelestialEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Celestial" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetCelestialById(CelestialId id, out Celestial value);
    #endregion

    #region Certificate Methods
    /// <summary>
    /// Returns the <see cref="Certificate" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    Certificate GetCertificateById(CertificateId id);

    /// <summary>
    /// Returns all <see cref="Certificate" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Certificate> GetCertificates(Func<IQueryable<CertificateEntity>, IQueryable<CertificateEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Certificate" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Certificate> GetCertificates(params IQueryModifier<CertificateEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Certificate" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetCertificateById(CertificateId id, out Certificate value);
    #endregion

    #region CertificateCategory Methods
    /// <summary>
    /// Returns the <see cref="CertificateCategory" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    CertificateCategory GetCertificateCategoryById(CertificateCategoryId id);

    /// <summary>
    /// Returns all <see cref="CertificateCategory" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<CertificateCategory> GetCertificateCategories(Func<IQueryable<CertificateCategoryEntity>, IQueryable<CertificateCategoryEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="CertificateCategory" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<CertificateCategory> GetCertificateCategories(params IQueryModifier<CertificateCategoryEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="CertificateCategory" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetCertificateCategoryById(CertificateCategoryId id, out CertificateCategory value);
    #endregion

    #region CertificateClass Methods
    /// <summary>
    /// Returns the <see cref="CertificateClass" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    CertificateClass GetCertificateClassById(CertificateClassId id);

    /// <summary>
    /// Returns all <see cref="CertificateClass" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<CertificateClass> GetCertificateClasses(Func<IQueryable<CertificateClassEntity>, IQueryable<CertificateClassEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="CertificateClass" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<CertificateClass> GetCertificateClasses(params IQueryModifier<CertificateClassEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="CertificateClass" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetCertificateClassById(CertificateClassId id, out CertificateClass value);
    #endregion

    #region CertificateRecommendation Methods
    /// <summary>
    /// Returns the <see cref="CertificateRecommendation" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    CertificateRecommendation GetCertificateRecommendationById(CertificateRecommendationId id);

    /// <summary>
    /// Returns all <see cref="CertificateRecommendation" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<CertificateRecommendation> GetCertificateRecommendations(Func<IQueryable<CertificateRecommendationEntity>, IQueryable<CertificateRecommendationEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="CertificateRecommendation" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<CertificateRecommendation> GetCertificateRecommendations(params IQueryModifier<CertificateRecommendationEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="CertificateRecommendation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetCertificateRecommendationById(CertificateRecommendationId id, out CertificateRecommendation value);
    #endregion

    #region CertificateRelationship Methods
    /// <summary>
    /// Returns the <see cref="CertificateRelationship" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    CertificateRelationship GetCertificateRelationshipById(CertificateRelationshipId id);

    /// <summary>
    /// Returns all <see cref="CertificateRelationship" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<CertificateRelationship> GetCertificateRelationships(Func<IQueryable<CertificateRelationshipEntity>, IQueryable<CertificateRelationshipEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="CertificateRelationship" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<CertificateRelationship> GetCertificateRelationships(params IQueryModifier<CertificateRelationshipEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="CertificateRelationship" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetCertificateRelationshipById(CertificateRelationshipId id, out CertificateRelationship value);
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
    /// The item with the specified ID value(s).
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
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(Func<IQueryable<CharacterAttributeTypeEntity>, IQueryable<CharacterAttributeTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for 
    /// <see cref="CharacterAttributeType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Constellation" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Constellation> GetConstellations(Func<IQueryable<ConstellationEntity>, IQueryable<ConstellationEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Constellation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Constellation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="ConstellationJump" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<ConstellationJump> GetConstellationJumps(Func<IQueryable<ConstellationJumpEntity>, IQueryable<ConstellationJumpEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ConstellationJump" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId, out ConstellationJump value);
    #endregion

    #region ContrabandInfo Methods
    /// <summary>
    /// Returns the <see cref="ContrabandInfo" /> object with the specified IDs.
    /// </summary>
    /// <param name="factionId">
    /// The ID of faction in which the item is contraband.
    /// </param>
    /// <param name="typeId">
    /// The ID of the type of contraband item.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    ContrabandInfo GetContrabandInfoById(FactionId factionId, EveTypeId typeId);

    /// <summary>
    /// Returns all <see cref="ContrabandInfo" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<ContrabandInfo> GetContrabandInfo(Func<IQueryable<ContrabandInfoEntity>, IQueryable<ContrabandInfoEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ContrabandInfo" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<ContrabandInfo> GetContrabandInfo(params IQueryModifier<ContrabandInfoEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="ContrabandInfo" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="factionId">
    /// The ID of faction in which the item is contraband.
    /// </param>
    /// <param name="typeId">
    /// The ID of the type of contraband item.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetContrabandInfoById(FactionId factionId, EveTypeId typeId, out ContrabandInfo value);
    #endregion

    #region ControlTowerResource Methods
    /// <summary>
    /// Returns the <see cref="ControlTowerResource" /> object with the specified IDs.
    /// </summary>
    /// <param name="controlTowerTypeId">
    /// The ID of the control tower type.
    /// </param>
    /// <param name="resourceTypeId">
    /// The ID of the resource type.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    ControlTowerResource GetControlTowerResourceById(EveTypeId controlTowerTypeId, EveTypeId resourceTypeId);

    /// <summary>
    /// Returns all <see cref="ControlTowerResource" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<ControlTowerResource> GetControlTowerResources(Func<IQueryable<ControlTowerResourceEntity>, IQueryable<ControlTowerResourceEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ControlTowerResource" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<ControlTowerResource> GetControlTowerResources(params IQueryModifier<ControlTowerResourceEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="ControlTowerResource" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="controlTowerTypeId">
    /// The ID of the control tower type.
    /// </param>
    /// <param name="resourceTypeId">
    /// The ID of the resource type.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetControlTowerResourceById(EveTypeId controlTowerTypeId, EveTypeId resourceTypeId, out ControlTowerResource value);
    #endregion

    #region CorporateActivity Methods
    /// <summary>
    /// Returns the <see cref="CorporateActivity" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="CorporateActivity" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<CorporateActivity> GetCorporateActivities(Func<IQueryable<CorporateActivityEntity>, IQueryable<CorporateActivityEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="CorporateActivity" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Division" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Division> GetDivisions(Func<IQueryable<DivisionEntity>, IQueryable<DivisionEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Division" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    Effect GetEffectById(EveTypeId itemTypeId, EffectId effectId);

    /// <summary>
    /// Returns all <see cref="Effect" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Effect> GetEffects(Func<IQueryable<EffectEntity>, IQueryable<EffectEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Effect" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetEffectById(EveTypeId itemTypeId, EffectId effectId, out Effect value);
    #endregion

    #region EffectType Methods
    /// <summary>
    /// Returns the <see cref="EffectType" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="EffectType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<EffectType> GetEffectTypes(Func<IQueryable<EffectTypeEntity>, IQueryable<EffectTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="EffectType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    EveType GetEveTypeById(EveTypeId id);

    /// <summary>
    /// Returns all <see cref="EveType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<EveType> GetEveTypes(Func<IQueryable<EveTypeEntity>, IQueryable<EveTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetEveTypeById(EveTypeId id, out EveType value);

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
    TEveType GetEveTypeById<TEveType>(EveTypeId id) where TEveType : EveType;

    /// <summary>
    /// Returns all <see cref="EveType" /> objects matching the specified criteria.
    /// of the desired type.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.  Only items of type <typeparamref name="TEveType" />
    /// will be returned.  Any matching items of other types will be omitted.
    /// </returns>
    IReadOnlyList<TEveType> GetEveTypes<TEveType>(Func<IQueryable<EveTypeEntity>, IQueryable<EveTypeEntity>> queryOperations) where TEveType : EveType;

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
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.  Only items of type <typeparamref name="TEveType" />
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item of the desired type is
    /// found; otherwise <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetEveTypeById<TEveType>(EveTypeId id, out TEveType value) where TEveType : EveType;
    #endregion

    #region Faction Methods
    /// <summary>
    /// Returns the <see cref="Faction" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Faction" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Faction> GetFactions(Func<IQueryable<FactionEntity>, IQueryable<FactionEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Faction" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Faction> GetFactions(params IQueryModifier<FactionEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Faction" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Graphic" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Graphic> GetGraphics(Func<IQueryable<GraphicEntity>, IQueryable<GraphicEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Graphic" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Group" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Group> GetGroups(Func<IQueryable<GroupEntity>, IQueryable<GroupEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Group" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Icon" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Icon> GetIcons(Func<IQueryable<IconEntity>, IQueryable<IconEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Icon" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Item" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Item> GetItems(Func<IQueryable<ItemEntity>, IQueryable<ItemEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Item" /> objects matching the specified criteria.
    /// of the desired type.
    /// </summary>
    /// <typeparam name="TItem">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.  Only items of type <typeparamref name="TItem" />
    /// will be returned.  Any matching items of other types will be omitted.
    /// </returns>
    IReadOnlyList<TItem> GetItems<TItem>(Func<IQueryable<ItemEntity>, IQueryable<ItemEntity>> queryOperations) where TItem : Item;

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
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.  Only items of type <typeparamref name="TItem" />
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item of the desired type
    /// is found; otherwise <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="ItemPosition" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<ItemPosition> GetItemPositions(Func<IQueryable<ItemPositionEntity>, IQueryable<ItemPositionEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="ItemPosition" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="MarketGroup" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<MarketGroup> GetMarketGroups(Func<IQueryable<MarketGroupEntity>, IQueryable<MarketGroupEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MarketGroup" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="MetaGroup" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<MetaGroup> GetMetaGroups(Func<IQueryable<MetaGroupEntity>, IQueryable<MetaGroupEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaGroup" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    MetaType GetMetaTypeById(EveTypeId typeId);

    /// <summary>
    /// Returns all <see cref="MetaType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<MetaType> GetMetaTypes(Func<IQueryable<MetaTypeEntity>, IQueryable<MetaTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaType" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetMetaTypeById(EveTypeId id, out MetaType value);
    #endregion

    #region NpcCorporation Methods
    /// <summary>
    /// Returns the <see cref="NpcCorporation" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="NpcCorporation" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<NpcCorporation> GetNpcCorporations(Func<IQueryable<NpcCorporationEntity>, IQueryable<NpcCorporationEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="NpcCorporation" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="NpcCorporationDivision" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(Func<IQueryable<NpcCorporationDivisionEntity>, IQueryable<NpcCorporationDivisionEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Race" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Race> GetRaces(Func<IQueryable<RaceEntity>, IQueryable<RaceEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Race" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Region" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Region> GetRegions(Func<IQueryable<RegionEntity>, IQueryable<RegionEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Region" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Region> GetRegions(params IQueryModifier<RegionEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Region" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="RegionJump" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<RegionJump> GetRegionJumps(Func<IQueryable<RegionJumpEntity>, IQueryable<RegionJumpEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="RegionJump" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="SolarSystem" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<SolarSystem> GetSolarSystems(Func<IQueryable<SolarSystemEntity>, IQueryable<SolarSystemEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystem" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="SolarSystem" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="SolarSystemJump" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(Func<IQueryable<SolarSystemJumpEntity>, IQueryable<SolarSystemJumpEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystemJump" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Stargate" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Stargate> GetStargates(Func<IQueryable<StargateEntity>, IQueryable<StargateEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Stargate" />
    /// objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Stargate> GetStargates(params IQueryModifier<StargateEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Stargate" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetStargateById(StargateId id, out Stargate value);
    #endregion

    #region Container Methods
    /// <summary>
    /// Returns the <see cref="Station" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Station" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Station> GetStations(Func<IQueryable<StationEntity>, IQueryable<StationEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Station" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Station> GetStations(params IQueryModifier<StationEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Station" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="StationOperation" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<StationOperation> GetStationOperations(Func<IQueryable<StationOperationEntity>, IQueryable<StationOperationEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationOperation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="StationService" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<StationService> GetStationServices(Func<IQueryable<StationServiceEntity>, IQueryable<StationServiceEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationService" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    StationType GetStationTypeById(EveTypeId id);

    /// <summary>
    /// Returns all <see cref="StationType" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<StationType> GetStationTypes(Func<IQueryable<StationTypeEntity>, IQueryable<StationTypeEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="StationType" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetStationTypeById(EveTypeId id, out StationType value);
    #endregion

    #region TypeMaterial Methods
    /// <summary>
    /// Returns the <see cref="TypeMaterial" /> object with the specified ID.
    /// </summary>
    /// <param name="typeId">
    /// The ID of the type being manufactured or reprocessed.
    /// </param>
    /// <param name="materialTypeId">
    /// The ID of the required material type.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    TypeMaterial GetTypeMaterialById(EveTypeId typeId, EveTypeId materialTypeId);

    /// <summary>
    /// Returns all <see cref="TypeMaterial" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<TypeMaterial> GetTypeMaterials(Func<IQueryable<TypeMaterialEntity>, IQueryable<TypeMaterialEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="TypeMaterial" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<TypeMaterial> GetTypeMaterials(params IQueryModifier<TypeMaterialEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="TypeMaterial" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="typeId">
    /// The ID of the type being manufactured or reprocessed.
    /// </param>
    /// <param name="materialTypeId">
    /// The ID of the required material type.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetTypeMaterialById(EveTypeId typeId, EveTypeId materialTypeId, out TypeMaterial value);
    #endregion

    #region TypeReaction Methods
    /// <summary>
    /// Returns the <see cref="TypeReaction" /> object with the specified ID.
    /// </summary>
    /// <param name="reactionTypeId">
    /// The ID of the reaction type.
    /// </param>
    /// <param name="input">
    /// Specifies whether the type specified by <paramref name="typeId" />
    /// is input or output for the reaction.
    /// </param>
    /// <param name="typeId">
    /// The ID of the type required by or produced by the reaction.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    TypeReaction GetTypeReactionById(EveTypeId reactionTypeId, bool input, EveTypeId typeId);

    /// <summary>
    /// Returns all <see cref="TypeReaction" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<TypeReaction> GetTypeReactions(Func<IQueryable<TypeReactionEntity>, IQueryable<TypeReactionEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="TypeReaction" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<TypeReaction> GetTypeReactions(params IQueryModifier<TypeReactionEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="TypeReaction" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="reactionTypeId">
    /// The ID of the reaction type.
    /// </param>
    /// <param name="input">
    /// Specifies whether the type specified by <paramref name="typeId" />
    /// is input or output for the reaction.
    /// </param>
    /// <param name="typeId">
    /// The ID of the type required by or produced by the reaction.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetTypeReactionById(EveTypeId reactionTypeId, bool input, EveTypeId typeId, out TypeReaction value);
    #endregion

    #region Unit Methods
    /// <summary>
    /// Returns the <see cref="Unit" /> object with the specified ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <returns>
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Unit" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Unit> GetUnits(Func<IQueryable<UnitEntity>, IQueryable<UnitEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Unit" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
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
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
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
    /// The item with the specified ID value(s).
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
    /// Returns all <see cref="Universe" /> objects matching the specified criteria.
    /// </summary>
    /// <param name="queryOperations">
    /// A delegate specifying what operation to perform on the data source to return the desired items.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the items that meet
    /// the specified criteria.
    /// </returns>
    IReadOnlyList<Universe> GetUniverses(Func<IQueryable<UniverseEntity>, IQueryable<UniverseEntity>> queryOperations);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Universe" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// An <see cref="IReadOnlyList{T}" /> containing the results of the query.
    /// </returns>
    IReadOnlyList<Universe> GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers);

    /// <summary>
    /// Attempts to retrieve the <see cref="Universe" /> object with the
    /// specified ID, returning success or failure.
    /// </summary>
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// <param name="value">
    /// The parameter which will hold the item with the specified ID value(s),
    /// if a matching item is found.  Output parameter.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a matching item is found; otherwise
    /// <see langword="false" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if more than one item with the specified ID was found.
    /// </exception>
    bool TryGetUniverseById(UniverseId id, out Universe value);
    #endregion
  }
}