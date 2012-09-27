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
  using Eve.Entities;
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
    AttributeValue GetAttributeValueById(ItemTypeId ItemTypeId, AttributeId id);
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

    #region ItemType Methods
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="ItemType" /> object with the specified ID.
    /// </summary>
    /// 
    /// <typeparam name="TItem">
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
    /// <typeparamref name="TItem" />.
    /// </exception>
    TItem GetItemTypeById<TItem>(ItemTypeId id) where TItem : ItemType;
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="ItemType" /> objects.
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
    IReadOnlyList<TItem> GetItemTypes<TItem>(Expression<Func<ItemTypeEntity, bool>> filter) where TItem : ItemType;
    //******************************************************************************
    /// <summary>
    /// Returns the results of the specified query for <see cref="ItemType" /> objects.
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
    IReadOnlyList<TItem> GetItemTypes<TItem>(params IQueryModifier<ItemTypeEntity>[] modifiers) where TItem : ItemType;
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
    AttributeValue IEveDataSource.GetAttributeValueById(ItemTypeId itemTypeId, AttributeId id) {
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

    #region ItemType Methods
    //******************************************************************************
    TItem IEveDataSource.GetItemTypeById<TItem>(ItemTypeId id) {
      Contract.Ensures(Contract.Result<TItem>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<TItem> IEveDataSource.GetItemTypes<TItem>(Expression<Func<ItemTypeEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IReadOnlyList<TItem> IEveDataSource.GetItemTypes<TItem>(params IQueryModifier<ItemTypeEntity>[] modifiers) {
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

  }
}