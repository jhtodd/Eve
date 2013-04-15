//-----------------------------------------------------------------------
// <copyright file="IEveRepositoryContracts.cs" company="Jeremy H. Todd">
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
  using Eve.Industry;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contract class for the <see cref="IEveRepository" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveRepository))]
  internal abstract partial class IEveRepositoryContracts : IEveRepository
  {
    /* Properties */

    EveCache IEveRepository.Cache
    {
      get
      {
        Contract.Ensures(Contract.Result<EveCache>() != null);
        throw new NotImplementedException();
      }
    }

    /* Methods */

    #region Activity Methods
    Activity IEveRepository.GetActivityById(ActivityId id)
    {
      Contract.Ensures(Contract.Result<Activity>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Activity> IEveRepository.GetActivities(Expression<Func<ActivityEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Activity>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Activity> IEveRepository.GetActivities(params IQueryModifier<ActivityEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Activity>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Agent Methods
    Agent IEveRepository.GetAgentById(AgentId id)
    {
      Contract.Ensures(Contract.Result<Agent>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Agent> IEveRepository.GetAgents(Expression<Func<AgentEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Agent> IEveRepository.GetAgents(params IQueryModifier<AgentEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AgentType Methods
    AgentType IEveRepository.GetAgentTypeById(AgentTypeId id)
    {
      Contract.Ensures(Contract.Result<AgentType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AgentType> IEveRepository.GetAgentTypes(Expression<Func<AgentTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AgentType> IEveRepository.GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Ancestry Methods
    Ancestry IEveRepository.GetAncestryById(AncestryId id)
    {
      Contract.Ensures(Contract.Result<Ancestry>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Ancestry> IEveRepository.GetAncestries(Expression<Func<AncestryEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Ancestry>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Ancestry> IEveRepository.GetAncestries(params IQueryModifier<AncestryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Ancestry>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeCategory Methods
    AttributeCategory IEveRepository.GetAttributeCategoryById(AttributeCategoryId id)
    {
      Contract.Ensures(Contract.Result<AttributeCategory>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveRepository.GetAttributeCategories(Expression<Func<AttributeCategoryEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveRepository.GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeType Methods
    AttributeType IEveRepository.GetAttributeTypeById(AttributeId id)
    {
      Contract.Ensures(Contract.Result<AttributeType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveRepository.GetAttributeTypes(Expression<Func<AttributeTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveRepository.GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeValue Methods
    AttributeValue IEveRepository.GetAttributeValueById(TypeId itemTypeId, AttributeId id)
    {
      Contract.Ensures(Contract.Result<AttributeValue>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveRepository.GetAttributeValues(Expression<Func<AttributeValueEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveRepository.GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Bloodline Methods
    Bloodline IEveRepository.GetBloodlineById(BloodlineId id)
    {
      Contract.Ensures(Contract.Result<Bloodline>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Bloodline> IEveRepository.GetBloodlines(Expression<Func<BloodlineEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Bloodline>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Bloodline> IEveRepository.GetBloodlines(params IQueryModifier<BloodlineEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Bloodline>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Category Methods
    Category IEveRepository.GetCategoryById(CategoryId id)
    {
      Contract.Ensures(Contract.Result<Category>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveRepository.GetCategories(Expression<Func<CategoryEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveRepository.GetCategories(params IQueryModifier<CategoryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CharacterAttributeType Methods
    CharacterAttributeType IEveRepository.GetCharacterAttributeTypeById(CharacterAttributeId id)
    {
      Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CharacterAttributeType> IEveRepository.GetCharacterAttributeTypes(Expression<Func<CharacterAttributeTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CharacterAttributeType> IEveRepository.GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Constellation Methods
    Constellation IEveRepository.GetConstellationById(ConstellationId id)
    {
      Contract.Ensures(Contract.Result<Constellation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Constellation> IEveRepository.GetConstellations(Expression<Func<ConstellationEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Constellation> IEveRepository.GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region ConstellationJump Methods
    ConstellationJump IEveRepository.GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId)
    {
      Contract.Ensures(Contract.Result<ConstellationJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ConstellationJump> IEveRepository.GetConstellationJumps(Expression<Func<ConstellationJumpEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ConstellationJump> IEveRepository.GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CorporateActivity Methods
    CorporateActivity IEveRepository.GetCorporateActivityById(CorporateActivityId id)
    {
      Contract.Ensures(Contract.Result<CorporateActivity>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CorporateActivity> IEveRepository.GetCorporateActivities(Expression<Func<CorporateActivityEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CorporateActivity> IEveRepository.GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Division Methods
    Division IEveRepository.GetDivisionById(DivisionId id)
    {
      Contract.Ensures(Contract.Result<Division>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Division> IEveRepository.GetDivisions(Expression<Func<DivisionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Division> IEveRepository.GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EffectType Methods
    EffectType IEveRepository.GetEffectTypeById(EffectId id)
    {
      Contract.Ensures(Contract.Result<EffectType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveRepository.GetEffectTypes(Expression<Func<EffectTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveRepository.GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Effect Methods
    Effect IEveRepository.GetEffectById(TypeId itemTypeId, EffectId id)
    {
      Contract.Ensures(Contract.Result<Effect>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveRepository.GetEffects(Expression<Func<EffectEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveRepository.GetEffects(params IQueryModifier<EffectEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EveType Methods
    EveType IEveRepository.GetEveTypeById(TypeId id)
    {
      Contract.Ensures(Contract.Result<EveType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveRepository.GetEveTypes(Expression<Func<EveTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveRepository.GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }

    TEveType IEveRepository.GetEveTypeById<TEveType>(TypeId id)
    {
      Contract.Ensures(Contract.Result<TEveType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveRepository.GetEveTypes<TEveType>(Expression<Func<EveTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveRepository.GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Faction Methods
    Faction IEveRepository.GetFactionById(FactionId id)
    {
      Contract.Ensures(Contract.Result<Faction>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Faction> IEveRepository.GetFactions(Expression<Func<FactionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Faction> IEveRepository.GetFactions(params IQueryModifier<FactionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Graphic Methods
    Graphic IEveRepository.GetGraphicById(GraphicId id)
    {
      Contract.Ensures(Contract.Result<Graphic>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Graphic> IEveRepository.GetGraphics(Expression<Func<GraphicEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Graphic>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Graphic> IEveRepository.GetGraphics(params IQueryModifier<GraphicEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Graphic>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Group Methods
    Group IEveRepository.GetGroupById(GroupId id)
    {
      Contract.Ensures(Contract.Result<Group>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveRepository.GetGroups(Expression<Func<GroupEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveRepository.GetGroups(params IQueryModifier<GroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Icon Methods
    Icon IEveRepository.GetIconById(IconId id)
    {
      Contract.Ensures(Contract.Result<Icon>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveRepository.GetIcons(Expression<Func<IconEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveRepository.GetIcons(params IQueryModifier<IconEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Item Methods
    Item IEveRepository.GetItemById(ItemId id)
    {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveRepository.GetItems(Expression<Func<ItemEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveRepository.GetItems(params IQueryModifier<ItemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }

    TItem IEveRepository.GetItemById<TItem>(ItemId id)
    {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveRepository.GetItems<TItem>(Expression<Func<ItemEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveRepository.GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MarketGroup Methods
    MarketGroup IEveRepository.GetMarketGroupById(MarketGroupId id)
    {
      Contract.Ensures(Contract.Result<MarketGroup>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveRepository.GetMarketGroups(Expression<Func<MarketGroupEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveRepository.GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaGroup Methods
    MetaGroup IEveRepository.GetMetaGroupById(MetaGroupId id)
    {
      Contract.Ensures(Contract.Result<MetaGroup>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveRepository.GetMetaGroups(Expression<Func<MetaGroupEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveRepository.GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaType Methods
    MetaType IEveRepository.GetMetaTypeById(TypeId id)
    {
      Contract.Ensures(Contract.Result<MetaType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveRepository.GetMetaTypes(Expression<Func<MetaTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveRepository.GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporation Methods
    NpcCorporation IEveRepository.GetNpcCorporationById(NpcCorporationId id)
    {
      Contract.Ensures(Contract.Result<NpcCorporation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporation> IEveRepository.GetNpcCorporations(Expression<Func<NpcCorporationEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporation> IEveRepository.GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporationDivision Methods
    NpcCorporationDivision IEveRepository.GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId)
    {
      Contract.Ensures(Contract.Result<NpcCorporationDivision>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporationDivision> IEveRepository.GetNpcCorporationDivisions(Expression<Func<NpcCorporationDivisionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporationDivision> IEveRepository.GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Race Methods
    Race IEveRepository.GetRaceById(RaceId id)
    {
      Contract.Ensures(Contract.Result<Race>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Race> IEveRepository.GetRaces(Expression<Func<RaceEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Race> IEveRepository.GetRaces(params IQueryModifier<RaceEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Region Methods
    Region IEveRepository.GetRegionById(RegionId id)
    {
      Contract.Ensures(Contract.Result<Region>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Region> IEveRepository.GetRegions(Expression<Func<RegionEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Region> IEveRepository.GetRegions(params IQueryModifier<RegionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region RegionJump Methods
    RegionJump IEveRepository.GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId)
    {
      Contract.Ensures(Contract.Result<RegionJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<RegionJump> IEveRepository.GetRegionJumps(Expression<Func<RegionJumpEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<RegionJump> IEveRepository.GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystem Methods
    SolarSystem IEveRepository.GetSolarSystemById(SolarSystemId id)
    {
      Contract.Ensures(Contract.Result<SolarSystem>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystem> IEveRepository.GetSolarSystems(Expression<Func<SolarSystemEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystem> IEveRepository.GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystemJump Methods
    SolarSystemJump IEveRepository.GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId)
    {
      Contract.Ensures(Contract.Result<SolarSystemJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystemJump> IEveRepository.GetSolarSystemJumps(Expression<Func<SolarSystemJumpEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystemJump> IEveRepository.GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Station Methods
    Station IEveRepository.GetStationById(StationId id)
    {
      Contract.Ensures(Contract.Result<Station>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Station> IEveRepository.GetStations(Expression<Func<StationEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Station>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Station> IEveRepository.GetStations(params IQueryModifier<StationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Station>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationOperation Methods
    StationOperation IEveRepository.GetStationOperationById(StationOperationId id)
    {
      Contract.Ensures(Contract.Result<StationOperation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationOperation> IEveRepository.GetStationOperations(Expression<Func<StationOperationEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationOperation> IEveRepository.GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationService Methods
    StationService IEveRepository.GetStationServiceById(StationServiceId id)
    {
      Contract.Ensures(Contract.Result<StationService>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationService> IEveRepository.GetStationServices(Expression<Func<StationServiceEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationService> IEveRepository.GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationType Methods
    StationType IEveRepository.GetStationTypeById(TypeId id)
    {
      Contract.Ensures(Contract.Result<StationType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationType> IEveRepository.GetStationTypes(Expression<Func<StationTypeEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationType> IEveRepository.GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Unit Methods
    Unit IEveRepository.GetUnitById(UnitId id)
    {
      Contract.Ensures(Contract.Result<Unit>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveRepository.GetUnits(Expression<Func<UnitEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveRepository.GetUnits(params IQueryModifier<UnitEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Universe Methods
    Universe IEveRepository.GetUniverseById(UniverseId id)
    {
      Contract.Ensures(Contract.Result<Universe>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe> IEveRepository.GetUniverses(Expression<Func<UniverseEntity, bool>> filter)
    {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe> IEveRepository.GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }
    #endregion
  }

  #region IDisposable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IDisposable" /> interface.
  /// </content>
  internal abstract partial class IEveRepositoryContracts : IDisposable
  {
    void IDisposable.Dispose()
    {
      throw new NotImplementedException();
    }
  }
  #endregion
}