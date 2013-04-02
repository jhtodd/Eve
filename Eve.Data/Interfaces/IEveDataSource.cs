//-----------------------------------------------------------------------
// <copyright file="IEveDataSource.cs" company="Jeremy H. Todd">
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

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// The base interface for a data source which provides access to the EVE
  /// database.
  /// </summary>
  [ContractClass(typeof(IEveDataSourceContracts))]
  public interface IEveDataSource {

    #region Interface Methods
    //******************************************************************************
    /// <summary>
    /// Populates the cache with certain commonly-used items.
    /// </summary>
    /// 
    /// <param name="cache">
    /// The cache to populate.
    /// </param>
    void PrepopulateCache(EveCache cache);
    #endregion

    #region Agent Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Agent" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    Agent GetAgentById(AgentId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Agent" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Agent> GetAgents(Expression<Func<AgentEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Agent" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Agent> GetAgents(params IQueryModifier<AgentEntity>[] modifiers);
    #endregion

    #region AgentType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="AgentType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    AgentType GetAgentTypeById(AgentTypeId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AgentType" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AgentType> GetAgentTypes(Expression<Func<AgentTypeEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AgentType" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AgentType> GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers);
    #endregion

    #region AttributeCategory Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="AttributeCategory" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    AttributeCategory GetAttributeCategoryById(AttributeCategoryId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeCategory" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeCategory> GetAttributeCategories(Expression<Func<AttributeCategoryEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeCategory" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeCategory> GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers);
    #endregion

    #region AttributeType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="AttributeType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    AttributeType GetAttributeTypeById(AttributeId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeType" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeType> GetAttributeTypes(Expression<Func<AttributeTypeEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeType" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeType> GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers);
    #endregion

    #region AttributeValue Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="AttributeValue" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    AttributeValue GetAttributeValueById(TypeId itemTypeId, AttributeId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeValue" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeValue> GetAttributeValues(Expression<Func<AttributeValueEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="AttributeValue" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<AttributeValue> GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers);
    #endregion

    #region Category Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Category" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Category GetCategoryById(CategoryId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Category" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Category> GetCategories(Expression<Func<CategoryEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Category" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Category> GetCategories(params IQueryModifier<CategoryEntity>[] modifiers);
    #endregion

    #region CharacterAttributeType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="CharacterAttributeType" /> object with the
    /// specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    CharacterAttributeType GetCharacterAttributeTypeById(CharacterAttributeId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for
    /// <see cref="CharacterAttributeType" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(Expression<Func<CharacterAttributeTypeEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for 
    /// <see cref="CharacterAttributeType" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CharacterAttributeType> GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers);
    #endregion

    #region Constellation Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Constellation" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    Constellation GetConstellationById(ConstellationId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Constellation" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Constellation> GetConstellations(Expression<Func<ConstellationEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Constellation" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers);
    #endregion

    #region ConstellationJump Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="ConstellationJump" /> object with the specified IDs.
    /// </summary>
    /// 
    /// <param name="fromConstellationId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// 
    /// <param name="toConstellationId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    ConstellationJump GetConstellationJumpById(ConstellationId fromConstellationID, ConstellationId toConstellationId);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="ConstellationJump" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<ConstellationJump> GetConstellationJumps(Expression<Func<ConstellationJumpEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="ConstellationJump" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<ConstellationJump> GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers);
    #endregion

    #region CorporateActivity Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="CorporateActivity" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    CorporateActivity GetCorporateActivityById(CorporateActivityId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="CorporateActivity" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CorporateActivity> GetCorporateActivities(Expression<Func<CorporateActivityEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="CorporateActivity" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<CorporateActivity> GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers);
    #endregion

    #region Division Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Division" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Division GetDivisionById(DivisionId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Division" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Division> GetDivisions(Expression<Func<DivisionEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Division" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Division> GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers);
    #endregion

    #region Effect Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Effect" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Effect GetEffectById(TypeId itemTypeId, EffectId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Effect" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Effect> GetEffects(Expression<Func<EffectEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Effect" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Effect> GetEffects(params IQueryModifier<EffectEntity>[] modifiers);
    #endregion

    #region EffectType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="EffectType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    EffectType GetEffectTypeById(EffectId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="EffectType" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EffectType> GetEffectTypes(Expression<Func<EffectTypeEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="EffectType" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EffectType> GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers);
    #endregion

    #region EveType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="EveType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// </exception>
    EveType GetEveTypeById(TypeId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EveType> GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<EveType> GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers);
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="EveType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of type
    /// <typeparamref name="TEveType" />.
    /// </exception>
    TEveType GetEveTypeById<TEveType>(TypeId id) where TEveType : EveType;
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// 
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.  Only items of type <typeparam name="TEveType" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TEveType> GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter) where TEveType : EveType;
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// 
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.  Only items of type <typeparam name="TEveType" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TEveType> GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers) where TEveType : EveType;
    #endregion

    #region Faction Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Faction" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Faction GetFactionById(FactionId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Faction" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Faction> GetFactions(Expression<Func<FactionEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Faction" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Faction> GetFactions(params IQueryModifier<FactionEntity>[] modifiers);
    #endregion

    #region Group Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Group" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Group GetGroupById(GroupId id);
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
    IReadOnlyList<Group> GetGroups(Expression<Func<GroupEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Group" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Group> GetGroups(params IQueryModifier<GroupEntity>[] modifiers);
    #endregion

    #region Icon Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Icon" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Icon GetIconById(IconId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Icon" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Icon> GetIcons(Expression<Func<IconEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Icon" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Icon> GetIcons(params IQueryModifier<IconEntity>[] modifiers);
    #endregion

    #region Item Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Item" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Item GetItemById(ItemId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Item> GetItems(Expression<Func<ItemEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Item> GetItems(params IQueryModifier<ItemEntity>[] modifiers);
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Item" /> object with the specified ID.
    /// </summary>
    /// 
    /// <typeparam name="TItem">
    /// The type of the item to retrieve.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastOperation">
    /// Thrown if the item with the specified ID was not of the desired type.
    /// </exception>
    TItem GetItemById<TItem>(ItemId id) where TItem : Item;
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// 
    /// <typeparam name="TItem">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.  Only items of type <typeparam name="TItem" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TItem> GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter) where TItem : Item;
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// 
    /// <typeparam name="TItem">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.  Only items of type <typeparam name="TItem" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TItem> GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers) where TItem : Item;
    #endregion

    #region MarketGroup Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="MarketGroup" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    MarketGroup GetMarketGroupById(MarketGroupId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="MarketGroup" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MarketGroup> GetMarketGroups(Expression<Func<MarketGroupEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="MarketGroup" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MarketGroup> GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers);
    #endregion

    #region MetaGroup Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="MetaGroup" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    MetaGroup GetMetaGroupById(MetaGroupId id);
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
    IReadOnlyList<MetaGroup> GetMetaGroups(Expression<Func<MetaGroupEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaGroup" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MetaGroup> GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers);
    #endregion

    #region MetaType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="MetaType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    MetaType GetMetaTypeById(TypeId typeId);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaType" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MetaType> GetMetaTypes(Expression<Func<MetaTypeEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="MetaType" />
    /// objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<MetaType> GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers);
    #endregion

    #region NpcCorporation Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="NpcCorporation" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    NpcCorporation GetNpcCorporationById(NpcCorporationId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporation> GetNpcCorporations(Expression<Func<NpcCorporationEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers);
    #endregion

    #region NpcCorporationDivision Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="NpcCorporationDivision" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="corporationId">
    /// The ID of the corporation.
    /// </param>
    /// 
    /// <param name="divisionId">
    /// The ID of the division.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    NpcCorporationDivision GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporationDivision" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(Expression<Func<NpcCorporationDivisionEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporationDivision> GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers);
    #endregion

    #region Race Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Race" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Race GetRaceById(RaceId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Race" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Race> GetRaces(Expression<Func<RaceEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Race" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Race> GetRaces(params IQueryModifier<RaceEntity>[] modifiers);
    #endregion

    #region Region Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Region" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    Region GetRegionById(RegionId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Region" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Region> GetRegions(Expression<Func<RegionEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Region" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Region> GetRegions(params IQueryModifier<RegionEntity>[] modifiers);
    #endregion

    #region RegionJump Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="RegionJump" /> object with the specified IDs.
    /// </summary>
    /// 
    /// <param name="fromRegionId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// 
    /// <param name="toRegionId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    RegionJump GetRegionJumpById(RegionId fromRegionID, RegionId toRegionId);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="RegionJump" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<RegionJump> GetRegionJumps(Expression<Func<RegionJumpEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="RegionJump" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<RegionJump> GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers);
    #endregion

    #region SolarSystem Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="SolarSystem" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    SolarSystem GetSolarSystemById(SolarSystemId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystem" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystem> GetSolarSystems(Expression<Func<SolarSystemEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystem" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers);
    #endregion

    #region SolarSystemJump Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="SolarSystemJump" /> object with the specified IDs.
    /// </summary>
    /// 
    /// <param name="fromSolarSystemId">
    /// The ID of the origin region of the jump to return.
    /// </param>
    /// 
    /// <param name="toSolarSystemId">
    /// The ID of the destination region of the jump to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
    SolarSystemJump GetSolarSystemJumpById(SolarSystemId fromSolarSystemID, SolarSystemId toSolarSystemId);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystemJump" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(Expression<Func<SolarSystemJumpEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystemJump" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystemJump> GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers);
    #endregion

    #region StationOperation Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="StationOperation" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    StationOperation GetStationOperationById(StationOperationId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="StationOperation" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationOperation> GetStationOperations(Expression<Func<StationOperationEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="StationOperation" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationOperation> GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers);
    #endregion

    #region StationService Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="StationService" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    StationService GetStationServiceById(StationServiceId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="StationService" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationService> GetStationServices(Expression<Func<StationServiceEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="StationService" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationService> GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers);
    #endregion

    #region StationType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="StationType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidTypeException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    StationType GetStationTypeById(TypeId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="StationType" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationType> GetStationTypes(Expression<Func<StationTypeEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="StationType" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<StationType> GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers);
    #endregion

    #region Unit Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Unit" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Unit GetUnitById(UnitId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Unit" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Unit> GetUnits(Expression<Func<UnitEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Unit" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Unit> GetUnits(params IQueryModifier<UnitEntity>[] modifiers);
    #endregion

    #region Universe Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="Universe" /> object with the specified ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item to return.
    /// </param>
    /// 
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    /// 
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of type
    /// <typeparamref name="TItem" />.
    /// </exception>
    Universe GetUniverseById(UniverseId id);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Universe" /> objects.
    /// </summary>
    /// 
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Universe> GetUniverses(Expression<Func<UniverseEntity, bool>> filter);
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="Universe" /> objects.
    /// </summary>
    /// 
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// 
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Universe> GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers);
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IEveDataSource" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveDataSource))]
  internal abstract class IEveDataSourceContracts : IEveDataSource {

    #region Interface Methods
    //******************************************************************************
    void IEveDataSource.PrepopulateCache(EveCache cache) {
      Contract.Requires(cache != null, Resources.Messages.IEveDataSource_CacheCannotBeNull);
      throw new NotImplementedException();
    }
    #endregion

    #region Agent Methods
    //******************************************************************************
    Agent IEveDataSource.GetAgentById(AgentId id) {
      Contract.Ensures(Contract.Result<Agent>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Agent> IEveDataSource.GetAgents(Expression<Func<AgentEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Agent> IEveDataSource.GetAgents(params IQueryModifier<AgentEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AgentType Methods
    //******************************************************************************
      AgentType IEveDataSource.GetAgentTypeById(AgentTypeId id) {
        Contract.Ensures(Contract.Result<AgentType>() != null);
        throw new NotImplementedException();
      }
      //******************************************************************************
      IReadOnlyList<AgentType> IEveDataSource.GetAgentTypes(Expression<Func<AgentTypeEntity, bool>> filter) {
        Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
        Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
        throw new NotImplementedException();
      }
      //******************************************************************************
      IReadOnlyList<AgentType> IEveDataSource.GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers) {
        Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
        Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
        throw new NotImplementedException();
      }
      #endregion

    #region AttributeCategory Methods
    //******************************************************************************
    AttributeCategory IEveDataSource.GetAttributeCategoryById(AttributeCategoryId id) {
      Contract.Ensures(Contract.Result<AttributeCategory>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<AttributeCategory> IEveDataSource.GetAttributeCategories(Expression<Func<AttributeCategoryEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<AttributeCategory> IEveDataSource.GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeType Methods
    //******************************************************************************
    AttributeType IEveDataSource.GetAttributeTypeById(AttributeId id) {
      Contract.Ensures(Contract.Result<AttributeType>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<AttributeType> IEveDataSource.GetAttributeTypes(Expression<Func<AttributeTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<AttributeType> IEveDataSource.GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeValue Methods
    //******************************************************************************
    AttributeValue IEveDataSource.GetAttributeValueById(TypeId itemTypeId, AttributeId id) {
      Contract.Ensures(Contract.Result<AttributeValue>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<AttributeValue> IEveDataSource.GetAttributeValues(Expression<Func<AttributeValueEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<AttributeValue> IEveDataSource.GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Category Methods
    //******************************************************************************
    Category IEveDataSource.GetCategoryById(CategoryId id) {
      Contract.Ensures(Contract.Result<Category>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Category> IEveDataSource.GetCategories(Expression<Func<CategoryEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Category> IEveDataSource.GetCategories(params IQueryModifier<CategoryEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CharacterAttributeType Methods
    //******************************************************************************
    CharacterAttributeType IEveDataSource.GetCharacterAttributeTypeById(CharacterAttributeId id) {
      Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<CharacterAttributeType> IEveDataSource.GetCharacterAttributeTypes(Expression<Func<CharacterAttributeTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<CharacterAttributeType> IEveDataSource.GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Constellation Methods
    //******************************************************************************
    Constellation IEveDataSource.GetConstellationById(ConstellationId id) {
      Contract.Ensures(Contract.Result<Constellation>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Constellation> IEveDataSource.GetConstellations(Expression<Func<ConstellationEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Constellation> IEveDataSource.GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region ConstellationJump Methods
    //******************************************************************************
    ConstellationJump IEveDataSource.GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId) {
      Contract.Ensures(Contract.Result<ConstellationJump>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<ConstellationJump> IEveDataSource.GetConstellationJumps(Expression<Func<ConstellationJumpEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<ConstellationJump> IEveDataSource.GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CorporateActivity Methods
    //******************************************************************************
    CorporateActivity IEveDataSource.GetCorporateActivityById(CorporateActivityId id) {
      Contract.Ensures(Contract.Result<CorporateActivity>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<CorporateActivity> IEveDataSource.GetCorporateActivities(Expression<Func<CorporateActivityEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<CorporateActivity> IEveDataSource.GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Division Methods
    //******************************************************************************
    Division IEveDataSource.GetDivisionById(DivisionId id) {
      Contract.Ensures(Contract.Result<Division>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Division> IEveDataSource.GetDivisions(Expression<Func<DivisionEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Division> IEveDataSource.GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EffectType Methods
    //******************************************************************************
    EffectType IEveDataSource.GetEffectTypeById(EffectId id) {
      Contract.Ensures(Contract.Result<EffectType>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<EffectType> IEveDataSource.GetEffectTypes(Expression<Func<EffectTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<EffectType> IEveDataSource.GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Effect Methods
    //******************************************************************************
    Effect IEveDataSource.GetEffectById(TypeId itemTypeId, EffectId id) {
      Contract.Ensures(Contract.Result<Effect>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Effect> IEveDataSource.GetEffects(Expression<Func<EffectEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Effect> IEveDataSource.GetEffects(params IQueryModifier<EffectEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EveType Methods
    //******************************************************************************
    EveType IEveDataSource.GetEveTypeById(TypeId id) {
      Contract.Ensures(Contract.Result<EveType>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<EveType> IEveDataSource.GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<EveType> IEveDataSource.GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    TEveType IEveDataSource.GetEveTypeById<TEveType>(TypeId id) {
      Contract.Ensures(Contract.Result<TEveType>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<TEveType> IEveDataSource.GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<TEveType> IEveDataSource.GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Faction Methods
    //******************************************************************************
    Faction IEveDataSource.GetFactionById(FactionId id) {
      Contract.Ensures(Contract.Result<Faction>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Faction> IEveDataSource.GetFactions(Expression<Func<FactionEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Faction> IEveDataSource.GetFactions(params IQueryModifier<FactionEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Group Methods
    //******************************************************************************
    Group IEveDataSource.GetGroupById(GroupId id) {
      Contract.Ensures(Contract.Result<Group>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Group> IEveDataSource.GetGroups(Expression<Func<GroupEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Group> IEveDataSource.GetGroups(params IQueryModifier<GroupEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Icon Methods
    //******************************************************************************
    Icon IEveDataSource.GetIconById(IconId id) {
      Contract.Ensures(Contract.Result<Icon>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Icon> IEveDataSource.GetIcons(Expression<Func<IconEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Icon> IEveDataSource.GetIcons(params IQueryModifier<IconEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Item Methods
    //******************************************************************************
    Item IEveDataSource.GetItemById(ItemId id) {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Item> IEveDataSource.GetItems(Expression<Func<ItemEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Item> IEveDataSource.GetItems(params IQueryModifier<ItemEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    TItem IEveDataSource.GetItemById<TItem>(ItemId id) {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<TItem> IEveDataSource.GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<TItem> IEveDataSource.GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MarketGroup Methods
    //******************************************************************************
    MarketGroup IEveDataSource.GetMarketGroupById(MarketGroupId id) {
      Contract.Ensures(Contract.Result<MarketGroup>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<MarketGroup> IEveDataSource.GetMarketGroups(Expression<Func<MarketGroupEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<MarketGroup> IEveDataSource.GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaGroup Methods
    //******************************************************************************
    MetaGroup IEveDataSource.GetMetaGroupById(MetaGroupId id) {
      Contract.Ensures(Contract.Result<MetaGroup>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<MetaGroup> IEveDataSource.GetMetaGroups(Expression<Func<MetaGroupEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<MetaGroup> IEveDataSource.GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaType Methods
    //******************************************************************************
    MetaType IEveDataSource.GetMetaTypeById(TypeId id) {
      Contract.Ensures(Contract.Result<MetaType>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<MetaType> IEveDataSource.GetMetaTypes(Expression<Func<MetaTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<MetaType> IEveDataSource.GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporation Methods
    //******************************************************************************
    NpcCorporation IEveDataSource.GetNpcCorporationById(NpcCorporationId id) {
      Contract.Ensures(Contract.Result<NpcCorporation>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<NpcCorporation> IEveDataSource.GetNpcCorporations(Expression<Func<NpcCorporationEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<NpcCorporation> IEveDataSource.GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporationDivision Methods
    //******************************************************************************
    NpcCorporationDivision IEveDataSource.GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId) {
      Contract.Ensures(Contract.Result<NpcCorporationDivision>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<NpcCorporationDivision> IEveDataSource.GetNpcCorporationDivisions(Expression<Func<NpcCorporationDivisionEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<NpcCorporationDivision> IEveDataSource.GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Race Methods
    //******************************************************************************
    Race IEveDataSource.GetRaceById(RaceId id) {
      Contract.Ensures(Contract.Result<Race>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Race> IEveDataSource.GetRaces(Expression<Func<RaceEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Race> IEveDataSource.GetRaces(params IQueryModifier<RaceEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Region Methods
    //******************************************************************************
    Region IEveDataSource.GetRegionById(RegionId id) {
      Contract.Ensures(Contract.Result<Region>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Region> IEveDataSource.GetRegions(Expression<Func<RegionEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Region> IEveDataSource.GetRegions(params IQueryModifier<RegionEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region RegionJump Methods
    //******************************************************************************
    RegionJump IEveDataSource.GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId) {
      Contract.Ensures(Contract.Result<RegionJump>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<RegionJump> IEveDataSource.GetRegionJumps(Expression<Func<RegionJumpEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<RegionJump> IEveDataSource.GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystem Methods
    //******************************************************************************
    SolarSystem IEveDataSource.GetSolarSystemById(SolarSystemId id) {
      Contract.Ensures(Contract.Result<SolarSystem>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<SolarSystem> IEveDataSource.GetSolarSystems(Expression<Func<SolarSystemEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<SolarSystem> IEveDataSource.GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystemJump Methods
    //******************************************************************************
    SolarSystemJump IEveDataSource.GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId) {
      Contract.Ensures(Contract.Result<SolarSystemJump>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<SolarSystemJump> IEveDataSource.GetSolarSystemJumps(Expression<Func<SolarSystemJumpEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<SolarSystemJump> IEveDataSource.GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationOperation Methods
    //******************************************************************************
    StationOperation IEveDataSource.GetStationOperationById(StationOperationId id) {
      Contract.Ensures(Contract.Result<StationOperation>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<StationOperation> IEveDataSource.GetStationOperations(Expression<Func<StationOperationEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<StationOperation> IEveDataSource.GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationService Methods
    //******************************************************************************
    StationService IEveDataSource.GetStationServiceById(StationServiceId id) {
      Contract.Ensures(Contract.Result<StationService>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<StationService> IEveDataSource.GetStationServices(Expression<Func<StationServiceEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<StationService> IEveDataSource.GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationType Methods
    //******************************************************************************
    StationType IEveDataSource.GetStationTypeById(TypeId id) {
      Contract.Ensures(Contract.Result<StationType>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<StationType> IEveDataSource.GetStationTypes(Expression<Func<StationTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<StationType> IEveDataSource.GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Unit Methods
    //******************************************************************************
    Unit IEveDataSource.GetUnitById(UnitId id) {
      Contract.Ensures(Contract.Result<Unit>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Unit> IEveDataSource.GetUnits(Expression<Func<UnitEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Unit> IEveDataSource.GetUnits(params IQueryModifier<UnitEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Universe Methods
    //******************************************************************************
    Universe IEveDataSource.GetUniverseById(UniverseId id) {
      Contract.Ensures(Contract.Result<Universe>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Universe> IEveDataSource.GetUniverses(Expression<Func<UniverseEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<Universe> IEveDataSource.GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers) {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }
    #endregion
  }
}