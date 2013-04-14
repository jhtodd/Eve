//-----------------------------------------------------------------------
// <copyright file="IEveRepository.cs" company="Jeremy H. Todd">
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

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for a data source which provides access to the EVE
  /// database.
  /// </summary>
  [ContractClass(typeof(IEveRepositoryContracts))]
  public interface IEveRepository : IDisposable
  {
    /* Properties */

    /// <summary>
    /// Gets the <see cref="EveCache" /> used to store data locally.
    /// </summary>
    /// <value>
    /// An <see cref="EveCache" /> used to store data locally.
    /// </value>
    EveCache Cache { get; }

    /* Methods */

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
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
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
    IReadOnlyList<Agent> GetAgents(Expression<Func<AgentEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Agent" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Agent> GetAgents(params IQueryModifier<AgentEntity>[] modifiers);
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
    #endregion

    #region AttributeValue Methods
    /// <summary>
    /// Returns the <see cref="AttributeValue" /> object with the specified ID.
    /// </summary>
    /// <param name="itemTypeId">
    /// The type ID of the attribute to return.
    /// </param>
    /// <param name="id">
    /// The ID of the attribute to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    AttributeValue GetAttributeValueById(TypeId itemTypeId, AttributeId id);

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
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
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
    IReadOnlyList<Constellation> GetConstellations(Expression<Func<ConstellationEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Constellation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Constellation> GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers);
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
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
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
    #endregion

    #region Effect Methods
    /// <summary>
    /// Returns the <see cref="Effect" /> object with the specified ID.
    /// </summary>
    /// <param name="itemTypeId">
    /// The type ID of the effect to return.
    /// </param>
    /// <param name="id">
    /// The ID of the effect to return.
    /// </param>
    /// <returns>
    /// The item with the specified key.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if no unique item with the specified ID was found.
    /// </exception>
    Effect GetEffectById(TypeId itemTypeId, EffectId id);

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
    /// Returns the <see cref="EveType" /> object with the specified ID.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
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
    /// Thrown if the item with the specified ID was not of type
    /// <typeparamref name="TEveType" />.
    /// </exception>
    TEveType GetEveTypeById<TEveType>(TypeId id) where TEveType : EveType;

    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TEveType" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TEveType> GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter) where TEveType : EveType;

    /// <summary>
    /// Returns the results of the specified query for <see cref="EveType" /> objects.
    /// </summary>
    /// <typeparam name="TEveType">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TEveType" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TEveType> GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers) where TEveType : EveType;
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
    IReadOnlyList<Faction> GetFactions(Expression<Func<FactionEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Faction" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Faction> GetFactions(params IQueryModifier<FactionEntity>[] modifiers);
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
    /// Returns the <see cref="Item" /> object with the specified ID.
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
    TItem GetItemById<TItem>(ItemId id) where TItem : Item;

    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// <typeparam name="TItem">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="filter">
    /// The expression that will filter the results of the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TItem" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TItem> GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter) where TItem : Item;

    /// <summary>
    /// Returns the results of the specified query for <see cref="Item" /> objects.
    /// </summary>
    /// <typeparam name="TItem">
    /// The desired item type.  Only items of this type will be returned.
    /// </typeparam>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.  Only items of type <typeparamref name="TItem" />
    /// will be returned.
    /// </returns>
    IReadOnlyList<TItem> GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers) where TItem : Item;
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
    IReadOnlyList<NpcCorporation> GetNpcCorporations(Expression<Func<NpcCorporationEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="NpcCorporation" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<NpcCorporation> GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers);
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
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
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
    IReadOnlyList<Region> GetRegions(Expression<Func<RegionEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Region" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Region> GetRegions(params IQueryModifier<RegionEntity>[] modifiers);
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
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
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
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
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
    IReadOnlyList<SolarSystem> GetSolarSystems(Expression<Func<SolarSystemEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="SolarSystem" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<SolarSystem> GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers);
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
    /// <exception cref="InvalidCastException">
    /// Thrown if the item with the specified ID was not of the correct type.
    /// </exception>
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
    IReadOnlyList<Station> GetStations(Expression<Func<StationEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Station" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Station> GetStations(params IQueryModifier<StationEntity>[] modifiers);
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
    IReadOnlyList<Universe> GetUniverses(Expression<Func<UniverseEntity, bool>> filter);

    /// <summary>
    /// Returns the results of the specified query for <see cref="Universe" /> objects.
    /// </summary>
    /// <param name="modifiers">
    /// The modifiers that are applied to the query.
    /// </param>
    /// <returns>
    /// The results of the query.
    /// </returns>
    IReadOnlyList<Universe> GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers);
    #endregion
  }
}