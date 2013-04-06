//-----------------------------------------------------------------------
// <copyright file="IEveDataSourceContracts.cs" company="Jeremy H. Todd">
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
  /// Contract class for the <see cref="IEveDataSource" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveDataSource))]
  internal abstract class IEveDataSourceContracts : IEveDataSource
  {
    /* Methods */

    void IEveDataSource.PrepopulateCache(EveCache cache)
    {
      Contract.Requires(cache != null, Resources.Messages.IEveDataSource_CacheCannotBeNull);
      throw new NotImplementedException();
    }

    #region Agent Methods
    Agent IEveDataSource.GetAgentById(AgentId id)
    {
      Contract.Ensures(Contract.Result<Agent>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Agent> IEveDataSource.GetAgents(Expression<Func<AgentEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Agent> IEveDataSource.GetAgents(params IQueryModifier<AgentEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AgentType Methods
    AgentType IEveDataSource.GetAgentTypeById(AgentTypeId id)
    {
      Contract.Ensures(Contract.Result<AgentType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AgentType> IEveDataSource.GetAgentTypes(Expression<Func<AgentTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AgentType> IEveDataSource.GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeCategory Methods
    AttributeCategory IEveDataSource.GetAttributeCategoryById(AttributeCategoryId id)
    {
      Contract.Ensures(Contract.Result<AttributeCategory>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveDataSource.GetAttributeCategories(Expression<Func<AttributeCategoryEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveDataSource.GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeType Methods
    AttributeType IEveDataSource.GetAttributeTypeById(AttributeId id)
    {
      Contract.Ensures(Contract.Result<AttributeType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveDataSource.GetAttributeTypes(Expression<Func<AttributeTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveDataSource.GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeValue Methods
    AttributeValue IEveDataSource.GetAttributeValueById(TypeId itemTypeId, AttributeId id)
    {
      Contract.Ensures(Contract.Result<AttributeValue>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveDataSource.GetAttributeValues(Expression<Func<AttributeValueEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveDataSource.GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Category Methods
    Category IEveDataSource.GetCategoryById(CategoryId id)
    {
      Contract.Ensures(Contract.Result<Category>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveDataSource.GetCategories(Expression<Func<CategoryEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveDataSource.GetCategories(params IQueryModifier<CategoryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CharacterAttributeType Methods
    CharacterAttributeType IEveDataSource.GetCharacterAttributeTypeById(CharacterAttributeId id)
    {
      Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CharacterAttributeType> IEveDataSource.GetCharacterAttributeTypes(Expression<Func<CharacterAttributeTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CharacterAttributeType> IEveDataSource.GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Constellation Methods
    Constellation IEveDataSource.GetConstellationById(ConstellationId id)
    {
      Contract.Ensures(Contract.Result<Constellation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Constellation> IEveDataSource.GetConstellations(Expression<Func<ConstellationEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Constellation> IEveDataSource.GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region ConstellationJump Methods
    ConstellationJump IEveDataSource.GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId)
    {
      Contract.Ensures(Contract.Result<ConstellationJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ConstellationJump> IEveDataSource.GetConstellationJumps(Expression<Func<ConstellationJumpEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ConstellationJump> IEveDataSource.GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CorporateActivity Methods
    CorporateActivity IEveDataSource.GetCorporateActivityById(CorporateActivityId id)
    {
      Contract.Ensures(Contract.Result<CorporateActivity>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CorporateActivity> IEveDataSource.GetCorporateActivities(Expression<Func<CorporateActivityEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CorporateActivity> IEveDataSource.GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Division Methods
    Division IEveDataSource.GetDivisionById(DivisionId id)
    {
      Contract.Ensures(Contract.Result<Division>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Division> IEveDataSource.GetDivisions(Expression<Func<DivisionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Division> IEveDataSource.GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EffectType Methods
    EffectType IEveDataSource.GetEffectTypeById(EffectId id)
    {
      Contract.Ensures(Contract.Result<EffectType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveDataSource.GetEffectTypes(Expression<Func<EffectTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveDataSource.GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Effect Methods
    Effect IEveDataSource.GetEffectById(TypeId itemTypeId, EffectId id)
    {
      Contract.Ensures(Contract.Result<Effect>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveDataSource.GetEffects(Expression<Func<EffectEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveDataSource.GetEffects(params IQueryModifier<EffectEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EveType Methods
    EveType IEveDataSource.GetEveTypeById(TypeId id)
    {
      Contract.Ensures(Contract.Result<EveType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveDataSource.GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveDataSource.GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }

    TEveType IEveDataSource.GetEveTypeById<TEveType>(TypeId id)
    {
      Contract.Ensures(Contract.Result<TEveType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveDataSource.GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveDataSource.GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Faction Methods
    Faction IEveDataSource.GetFactionById(FactionId id)
    {
      Contract.Ensures(Contract.Result<Faction>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Faction> IEveDataSource.GetFactions(Expression<Func<FactionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Faction> IEveDataSource.GetFactions(params IQueryModifier<FactionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Group Methods
    Group IEveDataSource.GetGroupById(GroupId id)
    {
      Contract.Ensures(Contract.Result<Group>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveDataSource.GetGroups(Expression<Func<GroupEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveDataSource.GetGroups(params IQueryModifier<GroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Icon Methods
    Icon IEveDataSource.GetIconById(IconId id)
    {
      Contract.Ensures(Contract.Result<Icon>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveDataSource.GetIcons(Expression<Func<IconEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveDataSource.GetIcons(params IQueryModifier<IconEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Item Methods
    Item IEveDataSource.GetItemById(ItemId id)
    {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveDataSource.GetItems(Expression<Func<ItemEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveDataSource.GetItems(params IQueryModifier<ItemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }

    TItem IEveDataSource.GetItemById<TItem>(ItemId id)
    {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveDataSource.GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveDataSource.GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MarketGroup Methods
    MarketGroup IEveDataSource.GetMarketGroupById(MarketGroupId id)
    {
      Contract.Ensures(Contract.Result<MarketGroup>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveDataSource.GetMarketGroups(Expression<Func<MarketGroupEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveDataSource.GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaGroup Methods
    MetaGroup IEveDataSource.GetMetaGroupById(MetaGroupId id)
    {
      Contract.Ensures(Contract.Result<MetaGroup>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveDataSource.GetMetaGroups(Expression<Func<MetaGroupEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveDataSource.GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaType Methods
    MetaType IEveDataSource.GetMetaTypeById(TypeId id)
    {
      Contract.Ensures(Contract.Result<MetaType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveDataSource.GetMetaTypes(Expression<Func<MetaTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveDataSource.GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporation Methods
    NpcCorporation IEveDataSource.GetNpcCorporationById(NpcCorporationId id)
    {
      Contract.Ensures(Contract.Result<NpcCorporation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporation> IEveDataSource.GetNpcCorporations(Expression<Func<NpcCorporationEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporation> IEveDataSource.GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporationDivision Methods
    NpcCorporationDivision IEveDataSource.GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId)
    {
      Contract.Ensures(Contract.Result<NpcCorporationDivision>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporationDivision> IEveDataSource.GetNpcCorporationDivisions(Expression<Func<NpcCorporationDivisionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporationDivision> IEveDataSource.GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Race Methods
    Race IEveDataSource.GetRaceById(RaceId id)
    {
      Contract.Ensures(Contract.Result<Race>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Race> IEveDataSource.GetRaces(Expression<Func<RaceEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Race> IEveDataSource.GetRaces(params IQueryModifier<RaceEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Region Methods
    Region IEveDataSource.GetRegionById(RegionId id)
    {
      Contract.Ensures(Contract.Result<Region>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Region> IEveDataSource.GetRegions(Expression<Func<RegionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Region> IEveDataSource.GetRegions(params IQueryModifier<RegionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region RegionJump Methods
    RegionJump IEveDataSource.GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId)
    {
      Contract.Ensures(Contract.Result<RegionJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<RegionJump> IEveDataSource.GetRegionJumps(Expression<Func<RegionJumpEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<RegionJump> IEveDataSource.GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystem Methods
    SolarSystem IEveDataSource.GetSolarSystemById(SolarSystemId id)
    {
      Contract.Ensures(Contract.Result<SolarSystem>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystem> IEveDataSource.GetSolarSystems(Expression<Func<SolarSystemEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystem> IEveDataSource.GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystemJump Methods
    SolarSystemJump IEveDataSource.GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId)
    {
      Contract.Ensures(Contract.Result<SolarSystemJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystemJump> IEveDataSource.GetSolarSystemJumps(Expression<Func<SolarSystemJumpEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystemJump> IEveDataSource.GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationOperation Methods
    StationOperation IEveDataSource.GetStationOperationById(StationOperationId id)
    {
      Contract.Ensures(Contract.Result<StationOperation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationOperation> IEveDataSource.GetStationOperations(Expression<Func<StationOperationEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationOperation> IEveDataSource.GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationService Methods
    StationService IEveDataSource.GetStationServiceById(StationServiceId id)
    {
      Contract.Ensures(Contract.Result<StationService>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationService> IEveDataSource.GetStationServices(Expression<Func<StationServiceEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationService> IEveDataSource.GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationType Methods
    StationType IEveDataSource.GetStationTypeById(TypeId id)
    {
      Contract.Ensures(Contract.Result<StationType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationType> IEveDataSource.GetStationTypes(Expression<Func<StationTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationType> IEveDataSource.GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Unit Methods
    Unit IEveDataSource.GetUnitById(UnitId id)
    {
      Contract.Ensures(Contract.Result<Unit>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveDataSource.GetUnits(Expression<Func<UnitEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveDataSource.GetUnits(params IQueryModifier<UnitEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Universe Methods
    Universe IEveDataSource.GetUniverseById(UniverseId id)
    {
      Contract.Ensures(Contract.Result<Universe>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe> IEveDataSource.GetUniverses(Expression<Func<UniverseEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe> IEveDataSource.GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }
    #endregion
  }
}