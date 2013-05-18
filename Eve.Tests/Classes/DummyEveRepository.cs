//-----------------------------------------------------------------------
// <copyright file="DummyEveRepository.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Caching;

  using Eve.Data;

  using NUnit.Framework;

  /// <summary>
  /// An implementation of <see cref="IEveRepository" /> that performs
  /// no actions.
  /// </summary>
  public class DummyEveRepository : IEveRepository
  {
    T IEveRepository.GetOrAddStoredValue<T>(IConvertible id, Func<T> valueFactory)
    {
      throw new NotImplementedException();
    }

    Industry.Activity IEveRepository.GetActivityById(Industry.ActivityId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.Activity> IEveRepository.GetActivities(Func<IQueryable<Data.Entities.ActivityEntity>, IQueryable<Data.Entities.ActivityEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.Activity> IEveRepository.GetActivities(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ActivityEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetActivityById(Industry.ActivityId id, out Industry.Activity value)
    {
      throw new NotImplementedException();
    }

    Universe.Agent IEveRepository.GetAgentById(Universe.AgentId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Agent> IEveRepository.GetAgents(Func<IQueryable<Data.Entities.AgentEntity>, IQueryable<Data.Entities.AgentEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Agent> IEveRepository.GetAgents(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AgentEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAgentById(Universe.AgentId id, out Universe.Agent value)
    {
      throw new NotImplementedException();
    }

    Universe.AgentType IEveRepository.GetAgentTypeById(Universe.AgentTypeId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.AgentType> IEveRepository.GetAgentTypes(Func<IQueryable<Data.Entities.AgentTypeEntity>, IQueryable<Data.Entities.AgentTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.AgentType> IEveRepository.GetAgentTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AgentTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAgentTypeById(Universe.AgentTypeId id, out Universe.AgentType value)
    {
      throw new NotImplementedException();
    }

    Character.Ancestry IEveRepository.GetAncestryById(Character.AncestryId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Ancestry> IEveRepository.GetAncestries(Func<IQueryable<Data.Entities.AncestryEntity>, IQueryable<Data.Entities.AncestryEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Ancestry> IEveRepository.GetAncestries(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AncestryEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAncestryById(Character.AncestryId id, out Character.Ancestry value)
    {
      throw new NotImplementedException();
    }

    Industry.AssemblyLine IEveRepository.GetAssemblyLineById(Industry.AssemblyLineId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLine> IEveRepository.GetAssemblyLines(Func<IQueryable<Data.Entities.AssemblyLineEntity>, IQueryable<Data.Entities.AssemblyLineEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLine> IEveRepository.GetAssemblyLines(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AssemblyLineEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineById(Industry.AssemblyLineId id, out Industry.AssemblyLine value)
    {
      throw new NotImplementedException();
    }

    Industry.AssemblyLineStation IEveRepository.GetAssemblyLineStationById(Universe.StationId stationId, Industry.AssemblyLineTypeId assemblyLineTypeId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineStation> IEveRepository.GetAssemblyLineStations(Func<IQueryable<Data.Entities.AssemblyLineStationEntity>, IQueryable<Data.Entities.AssemblyLineStationEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineStation> IEveRepository.GetAssemblyLineStations(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AssemblyLineStationEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineStationById(Universe.StationId stationId, Industry.AssemblyLineTypeId assemblyLineTypeId, out Industry.AssemblyLineStation value)
    {
      throw new NotImplementedException();
    }

    Industry.AssemblyLineType IEveRepository.GetAssemblyLineTypeById(Industry.AssemblyLineTypeId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineType> IEveRepository.GetAssemblyLineTypes(Func<IQueryable<Data.Entities.AssemblyLineTypeEntity>, IQueryable<Data.Entities.AssemblyLineTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineType> IEveRepository.GetAssemblyLineTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AssemblyLineTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineTypeById(Industry.AssemblyLineTypeId id, out Industry.AssemblyLineType value)
    {
      throw new NotImplementedException();
    }

    Industry.AssemblyLineTypeCategoryDetail IEveRepository.GetAssemblyLineTypeCategoryDetailById(Industry.AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineTypeCategoryDetail> IEveRepository.GetAssemblyLineTypeCategoryDetails(Func<IQueryable<Data.Entities.AssemblyLineTypeCategoryDetailEntity>, IQueryable<Data.Entities.AssemblyLineTypeCategoryDetailEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineTypeCategoryDetail> IEveRepository.GetAssemblyLineTypeCategoryDetails(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AssemblyLineTypeCategoryDetailEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineTypeCategoryDetailById(Industry.AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId, out Industry.AssemblyLineTypeCategoryDetail value)
    {
      throw new NotImplementedException();
    }

    Industry.AssemblyLineTypeGroupDetail IEveRepository.GetAssemblyLineTypeGroupDetailById(Industry.AssemblyLineTypeId assemblyLineTypeId, GroupId groupId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineTypeGroupDetail> IEveRepository.GetAssemblyLineTypeGroupDetails(Func<IQueryable<Data.Entities.AssemblyLineTypeGroupDetailEntity>, IQueryable<Data.Entities.AssemblyLineTypeGroupDetailEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Industry.AssemblyLineTypeGroupDetail> IEveRepository.GetAssemblyLineTypeGroupDetails(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AssemblyLineTypeGroupDetailEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineTypeGroupDetailById(Industry.AssemblyLineTypeId assemblyLineTypeId, GroupId groupId, out Industry.AssemblyLineTypeGroupDetail value)
    {
      throw new NotImplementedException();
    }

    AttributeCategory IEveRepository.GetAttributeCategoryById(AttributeCategoryId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveRepository.GetAttributeCategories(Func<IQueryable<Data.Entities.AttributeCategoryEntity>, IQueryable<Data.Entities.AttributeCategoryEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveRepository.GetAttributeCategories(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AttributeCategoryEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAttributeCategoryById(AttributeCategoryId id, out AttributeCategory value)
    {
      throw new NotImplementedException();
    }

    AttributeType IEveRepository.GetAttributeTypeById(AttributeId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveRepository.GetAttributeTypes(Func<IQueryable<Data.Entities.AttributeTypeEntity>, IQueryable<Data.Entities.AttributeTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveRepository.GetAttributeTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AttributeTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAttributeTypeById(AttributeId id, out AttributeType value)
    {
      throw new NotImplementedException();
    }

    AttributeValue IEveRepository.GetAttributeValueById(EveTypeId itemTypeId, AttributeId attributeId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveRepository.GetAttributeValues(Func<IQueryable<Data.Entities.AttributeValueEntity>, IQueryable<Data.Entities.AttributeValueEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveRepository.GetAttributeValues(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.AttributeValueEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAttributeValueById(EveTypeId itemTypeId, AttributeId attributeId, out AttributeValue value)
    {
      throw new NotImplementedException();
    }

    Character.Bloodline IEveRepository.GetBloodlineById(Character.BloodlineId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Bloodline> IEveRepository.GetBloodlines(Func<IQueryable<Data.Entities.BloodlineEntity>, IQueryable<Data.Entities.BloodlineEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Bloodline> IEveRepository.GetBloodlines(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.BloodlineEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetBloodlineById(Character.BloodlineId id, out Character.Bloodline value)
    {
      throw new NotImplementedException();
    }

    Category IEveRepository.GetCategoryById(CategoryId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveRepository.GetCategories(Func<IQueryable<Data.Entities.CategoryEntity>, IQueryable<Data.Entities.CategoryEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveRepository.GetCategories(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CategoryEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCategoryById(CategoryId id, out Category value)
    {
      throw new NotImplementedException();
    }

    Universe.Celestial IEveRepository.GetCelestialById(Universe.CelestialId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Celestial> IEveRepository.GetCelestials(Func<IQueryable<Data.Entities.CelestialEntity>, IQueryable<Data.Entities.CelestialEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Celestial> IEveRepository.GetCelestials(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CelestialEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCelestialById(Universe.CelestialId id, out Universe.Celestial value)
    {
      throw new NotImplementedException();
    }

    Character.Certificate IEveRepository.GetCertificateById(Character.CertificateId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Certificate> IEveRepository.GetCertificates(Func<IQueryable<Data.Entities.CertificateEntity>, IQueryable<Data.Entities.CertificateEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Certificate> IEveRepository.GetCertificates(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CertificateEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateById(Character.CertificateId id, out Character.Certificate value)
    {
      throw new NotImplementedException();
    }

    Character.CertificateCategory IEveRepository.GetCertificateCategoryById(Character.CertificateCategoryId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateCategory> IEveRepository.GetCertificateCategories(Func<IQueryable<Data.Entities.CertificateCategoryEntity>, IQueryable<Data.Entities.CertificateCategoryEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateCategory> IEveRepository.GetCertificateCategories(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CertificateCategoryEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateCategoryById(Character.CertificateCategoryId id, out Character.CertificateCategory value)
    {
      throw new NotImplementedException();
    }

    Character.CertificateClass IEveRepository.GetCertificateClassById(Character.CertificateClassId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateClass> IEveRepository.GetCertificateClasses(Func<IQueryable<Data.Entities.CertificateClassEntity>, IQueryable<Data.Entities.CertificateClassEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateClass> IEveRepository.GetCertificateClasses(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CertificateClassEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateClassById(Character.CertificateClassId id, out Character.CertificateClass value)
    {
      throw new NotImplementedException();
    }

    Character.CertificateRecommendation IEveRepository.GetCertificateRecommendationById(Character.CertificateRecommendationId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateRecommendation> IEveRepository.GetCertificateRecommendations(Func<IQueryable<Data.Entities.CertificateRecommendationEntity>, IQueryable<Data.Entities.CertificateRecommendationEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateRecommendation> IEveRepository.GetCertificateRecommendations(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CertificateRecommendationEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateRecommendationById(Character.CertificateRecommendationId id, out Character.CertificateRecommendation value)
    {
      throw new NotImplementedException();
    }

    Character.CertificateRelationship IEveRepository.GetCertificateRelationshipById(Character.CertificateRelationshipId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateRelationship> IEveRepository.GetCertificateRelationships(Func<IQueryable<Data.Entities.CertificateRelationshipEntity>, IQueryable<Data.Entities.CertificateRelationshipEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CertificateRelationship> IEveRepository.GetCertificateRelationships(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CertificateRelationshipEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateRelationshipById(Character.CertificateRelationshipId id, out Character.CertificateRelationship value)
    {
      throw new NotImplementedException();
    }

    Character.CharacterAttributeType IEveRepository.GetCharacterAttributeTypeById(Character.CharacterAttributeId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CharacterAttributeType> IEveRepository.GetCharacterAttributeTypes(Func<IQueryable<Data.Entities.CharacterAttributeTypeEntity>, IQueryable<Data.Entities.CharacterAttributeTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.CharacterAttributeType> IEveRepository.GetCharacterAttributeTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CharacterAttributeTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCharacterAttributeTypeById(Character.CharacterAttributeId id, out Character.CharacterAttributeType value)
    {
      throw new NotImplementedException();
    }

    Universe.Constellation IEveRepository.GetConstellationById(Universe.ConstellationId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Constellation> IEveRepository.GetConstellations(Func<IQueryable<Data.Entities.ConstellationEntity>, IQueryable<Data.Entities.ConstellationEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Constellation> IEveRepository.GetConstellations(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ConstellationEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetConstellationById(Universe.ConstellationId id, out Universe.Constellation value)
    {
      throw new NotImplementedException();
    }

    Universe.ConstellationJump IEveRepository.GetConstellationJumpById(Universe.ConstellationId fromConstellationId, Universe.ConstellationId toConstellationId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ConstellationJump> IEveRepository.GetConstellationJumps(Func<IQueryable<Data.Entities.ConstellationJumpEntity>, IQueryable<Data.Entities.ConstellationJumpEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ConstellationJump> IEveRepository.GetConstellationJumps(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ConstellationJumpEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetConstellationJumpById(Universe.ConstellationId fromConstellationId, Universe.ConstellationId toConstellationId, out Universe.ConstellationJump value)
    {
      throw new NotImplementedException();
    }

    Universe.ContrabandInfo IEveRepository.GetContrabandInfoById(Character.FactionId factionId, EveTypeId typeId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ContrabandInfo> IEveRepository.GetContrabandInfo(Func<IQueryable<Data.Entities.ContrabandInfoEntity>, IQueryable<Data.Entities.ContrabandInfoEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ContrabandInfo> IEveRepository.GetContrabandInfo(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ContrabandInfoEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetContrabandInfoById(Character.FactionId factionId, EveTypeId typeId, out Universe.ContrabandInfo value)
    {
      throw new NotImplementedException();
    }

    Universe.ControlTowerResource IEveRepository.GetControlTowerResourceById(EveTypeId controlTowerTypeId, EveTypeId resourceTypeId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ControlTowerResource> IEveRepository.GetControlTowerResources(Func<IQueryable<Data.Entities.ControlTowerResourceEntity>, IQueryable<Data.Entities.ControlTowerResourceEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ControlTowerResource> IEveRepository.GetControlTowerResources(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ControlTowerResourceEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetControlTowerResourceById(EveTypeId controlTowerTypeId, EveTypeId resourceTypeId, out Universe.ControlTowerResource value)
    {
      throw new NotImplementedException();
    }

    Universe.CorporateActivity IEveRepository.GetCorporateActivityById(Universe.CorporateActivityId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.CorporateActivity> IEveRepository.GetCorporateActivities(Func<IQueryable<Data.Entities.CorporateActivityEntity>, IQueryable<Data.Entities.CorporateActivityEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.CorporateActivity> IEveRepository.GetCorporateActivities(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.CorporateActivityEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCorporateActivityById(Universe.CorporateActivityId id, out Universe.CorporateActivity value)
    {
      throw new NotImplementedException();
    }

    Universe.Division IEveRepository.GetDivisionById(Universe.DivisionId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Division> IEveRepository.GetDivisions(Func<IQueryable<Data.Entities.DivisionEntity>, IQueryable<Data.Entities.DivisionEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Division> IEveRepository.GetDivisions(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.DivisionEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetDivisionById(Universe.DivisionId id, out Universe.Division value)
    {
      throw new NotImplementedException();
    }

    Effect IEveRepository.GetEffectById(EveTypeId itemTypeId, EffectId effectId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveRepository.GetEffects(Func<IQueryable<Data.Entities.EffectEntity>, IQueryable<Data.Entities.EffectEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveRepository.GetEffects(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.EffectEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEffectById(EveTypeId itemTypeId, EffectId effectId, out Effect value)
    {
      throw new NotImplementedException();
    }

    EffectType IEveRepository.GetEffectTypeById(EffectId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveRepository.GetEffectTypes(Func<IQueryable<Data.Entities.EffectTypeEntity>, IQueryable<Data.Entities.EffectTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveRepository.GetEffectTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.EffectTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEffectTypeById(EffectId id, out EffectType value)
    {
      throw new NotImplementedException();
    }

    EveType IEveRepository.GetEveTypeById(EveTypeId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveRepository.GetEveTypes(Func<IQueryable<Data.Entities.EveTypeEntity>, IQueryable<Data.Entities.EveTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveRepository.GetEveTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.EveTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEveTypeById(EveTypeId id, out EveType value)
    {
      throw new NotImplementedException();
    }

    TEveType IEveRepository.GetEveTypeById<TEveType>(EveTypeId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveRepository.GetEveTypes<TEveType>(Func<IQueryable<Data.Entities.EveTypeEntity>, IQueryable<Data.Entities.EveTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveRepository.GetEveTypes<TEveType>(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.EveTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEveTypeById<TEveType>(EveTypeId id, out TEveType value)
    {
      throw new NotImplementedException();
    }

    Character.Faction IEveRepository.GetFactionById(Character.FactionId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Faction> IEveRepository.GetFactions(Func<IQueryable<Data.Entities.FactionEntity>, IQueryable<Data.Entities.FactionEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Faction> IEveRepository.GetFactions(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.FactionEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetFactionById(Character.FactionId id, out Character.Faction value)
    {
      throw new NotImplementedException();
    }

    Graphic IEveRepository.GetGraphicById(GraphicId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Graphic> IEveRepository.GetGraphics(Func<IQueryable<Data.Entities.GraphicEntity>, IQueryable<Data.Entities.GraphicEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Graphic> IEveRepository.GetGraphics(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.GraphicEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetGraphicById(GraphicId id, out Graphic value)
    {
      throw new NotImplementedException();
    }

    Group IEveRepository.GetGroupById(GroupId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveRepository.GetGroups(Func<IQueryable<Data.Entities.GroupEntity>, IQueryable<Data.Entities.GroupEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveRepository.GetGroups(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.GroupEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetGroupById(GroupId id, out Group value)
    {
      throw new NotImplementedException();
    }

    Icon IEveRepository.GetIconById(IconId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveRepository.GetIcons(Func<IQueryable<Data.Entities.IconEntity>, IQueryable<Data.Entities.IconEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveRepository.GetIcons(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.IconEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetIconById(IconId id, out Icon value)
    {
      throw new NotImplementedException();
    }

    Item IEveRepository.GetItemById(ItemId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveRepository.GetItems(Func<IQueryable<Data.Entities.ItemEntity>, IQueryable<Data.Entities.ItemEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveRepository.GetItems(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ItemEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetItemById(ItemId id, out Item value)
    {
      throw new NotImplementedException();
    }

    TItem IEveRepository.GetItemById<TItem>(ItemId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveRepository.GetItems<TItem>(Func<IQueryable<Data.Entities.ItemEntity>, IQueryable<Data.Entities.ItemEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveRepository.GetItems<TItem>(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ItemEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetItemById<TItem>(ItemId id, out TItem value)
    {
      throw new NotImplementedException();
    }

    Universe.ItemPosition IEveRepository.GetItemPositionById(ItemId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ItemPosition> IEveRepository.GetItemPositions(Func<IQueryable<Data.Entities.ItemPositionEntity>, IQueryable<Data.Entities.ItemPositionEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.ItemPosition> IEveRepository.GetItemPositions(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.ItemPositionEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetItemPositionById(ItemId id, out Universe.ItemPosition value)
    {
      throw new NotImplementedException();
    }

    MarketGroup IEveRepository.GetMarketGroupById(MarketGroupId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveRepository.GetMarketGroups(Func<IQueryable<Data.Entities.MarketGroupEntity>, IQueryable<Data.Entities.MarketGroupEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveRepository.GetMarketGroups(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.MarketGroupEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetMarketGroupById(MarketGroupId id, out MarketGroup value)
    {
      throw new NotImplementedException();
    }

    MetaGroup IEveRepository.GetMetaGroupById(MetaGroupId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveRepository.GetMetaGroups(Func<IQueryable<Data.Entities.MetaGroupEntity>, IQueryable<Data.Entities.MetaGroupEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveRepository.GetMetaGroups(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.MetaGroupEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetMetaGroupById(MetaGroupId id, out MetaGroup value)
    {
      throw new NotImplementedException();
    }

    MetaType IEveRepository.GetMetaTypeById(EveTypeId typeId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveRepository.GetMetaTypes(Func<IQueryable<Data.Entities.MetaTypeEntity>, IQueryable<Data.Entities.MetaTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveRepository.GetMetaTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.MetaTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetMetaTypeById(EveTypeId id, out MetaType value)
    {
      throw new NotImplementedException();
    }

    Universe.NpcCorporation IEveRepository.GetNpcCorporationById(Universe.NpcCorporationId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.NpcCorporation> IEveRepository.GetNpcCorporations(Func<IQueryable<Data.Entities.NpcCorporationEntity>, IQueryable<Data.Entities.NpcCorporationEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.NpcCorporation> IEveRepository.GetNpcCorporations(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.NpcCorporationEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetNpcCorporationById(Universe.NpcCorporationId id, out Universe.NpcCorporation value)
    {
      throw new NotImplementedException();
    }

    Universe.NpcCorporationDivision IEveRepository.GetNpcCorporationDivisionById(Universe.NpcCorporationId corporationId, Universe.DivisionId divisionId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.NpcCorporationDivision> IEveRepository.GetNpcCorporationDivisions(Func<IQueryable<Data.Entities.NpcCorporationDivisionEntity>, IQueryable<Data.Entities.NpcCorporationDivisionEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.NpcCorporationDivision> IEveRepository.GetNpcCorporationDivisions(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.NpcCorporationDivisionEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetNpcCorporationDivisionById(Universe.NpcCorporationId corporationId, Universe.DivisionId divisionId, out Universe.NpcCorporationDivision value)
    {
      throw new NotImplementedException();
    }

    Character.Race IEveRepository.GetRaceById(Character.RaceId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Race> IEveRepository.GetRaces(Func<IQueryable<Data.Entities.RaceEntity>, IQueryable<Data.Entities.RaceEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Character.Race> IEveRepository.GetRaces(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.RaceEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetRaceById(Character.RaceId id, out Character.Race value)
    {
      throw new NotImplementedException();
    }

    Universe.Region IEveRepository.GetRegionById(Universe.RegionId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Region> IEveRepository.GetRegions(Func<IQueryable<Data.Entities.RegionEntity>, IQueryable<Data.Entities.RegionEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Region> IEveRepository.GetRegions(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.RegionEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetRegionById(Universe.RegionId id, out Universe.Region value)
    {
      throw new NotImplementedException();
    }

    Universe.RegionJump IEveRepository.GetRegionJumpById(Universe.RegionId fromRegionId, Universe.RegionId toRegionId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.RegionJump> IEveRepository.GetRegionJumps(Func<IQueryable<Data.Entities.RegionJumpEntity>, IQueryable<Data.Entities.RegionJumpEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.RegionJump> IEveRepository.GetRegionJumps(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.RegionJumpEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetRegionJumpById(Universe.RegionId fromRegionId, Universe.RegionId toRegionId, out Universe.RegionJump value)
    {
      throw new NotImplementedException();
    }

    Universe.SolarSystem IEveRepository.GetSolarSystemById(Universe.SolarSystemId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.SolarSystem> IEveRepository.GetSolarSystems(Func<IQueryable<Data.Entities.SolarSystemEntity>, IQueryable<Data.Entities.SolarSystemEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.SolarSystem> IEveRepository.GetSolarSystems(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.SolarSystemEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetSolarSystemById(Universe.SolarSystemId id, out Universe.SolarSystem value)
    {
      throw new NotImplementedException();
    }

    Universe.SolarSystemJump IEveRepository.GetSolarSystemJumpById(Universe.SolarSystemId fromSolarSystemId, Universe.SolarSystemId toSolarSystemId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.SolarSystemJump> IEveRepository.GetSolarSystemJumps(Func<IQueryable<Data.Entities.SolarSystemJumpEntity>, IQueryable<Data.Entities.SolarSystemJumpEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.SolarSystemJump> IEveRepository.GetSolarSystemJumps(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.SolarSystemJumpEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetSolarSystemJumpById(Universe.SolarSystemId fromSolarSystemId, Universe.SolarSystemId toSolarSystemId, out Universe.SolarSystemJump value)
    {
      throw new NotImplementedException();
    }

    Universe.Stargate IEveRepository.GetStargateById(Universe.StargateId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Stargate> IEveRepository.GetStargates(Func<IQueryable<Data.Entities.StargateEntity>, IQueryable<Data.Entities.StargateEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Stargate> IEveRepository.GetStargates(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.StargateEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStargateById(Universe.StargateId id, out Universe.Stargate value)
    {
      throw new NotImplementedException();
    }

    Universe.Station IEveRepository.GetStationById(Universe.StationId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Station> IEveRepository.GetStations(Func<IQueryable<Data.Entities.StationEntity>, IQueryable<Data.Entities.StationEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Station> IEveRepository.GetStations(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.StationEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationById(Universe.StationId id, out Universe.Station value)
    {
      throw new NotImplementedException();
    }

    Universe.StationOperation IEveRepository.GetStationOperationById(Universe.StationOperationId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.StationOperation> IEveRepository.GetStationOperations(Func<IQueryable<Data.Entities.StationOperationEntity>, IQueryable<Data.Entities.StationOperationEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.StationOperation> IEveRepository.GetStationOperations(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.StationOperationEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationOperationById(Universe.StationOperationId id, out Universe.StationOperation value)
    {
      throw new NotImplementedException();
    }

    Universe.StationService IEveRepository.GetStationServiceById(Universe.StationServiceId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.StationService> IEveRepository.GetStationServices(Func<IQueryable<Data.Entities.StationServiceEntity>, IQueryable<Data.Entities.StationServiceEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.StationService> IEveRepository.GetStationServices(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.StationServiceEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationServiceById(Universe.StationServiceId id, out Universe.StationService value)
    {
      throw new NotImplementedException();
    }

    Universe.StationType IEveRepository.GetStationTypeById(EveTypeId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.StationType> IEveRepository.GetStationTypes(Func<IQueryable<Data.Entities.StationTypeEntity>, IQueryable<Data.Entities.StationTypeEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.StationType> IEveRepository.GetStationTypes(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.StationTypeEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationTypeById(EveTypeId id, out Universe.StationType value)
    {
      throw new NotImplementedException();
    }

    TypeMaterial IEveRepository.GetTypeMaterialById(EveTypeId typeId, EveTypeId materialTypeId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeMaterial> IEveRepository.GetTypeMaterials(Func<IQueryable<Data.Entities.TypeMaterialEntity>, IQueryable<Data.Entities.TypeMaterialEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeMaterial> IEveRepository.GetTypeMaterials(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.TypeMaterialEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetTypeMaterialById(EveTypeId typeId, EveTypeId materialTypeId, out TypeMaterial value)
    {
      throw new NotImplementedException();
    }

    TypeReaction IEveRepository.GetTypeReactionById(EveTypeId reactionTypeId, bool input, EveTypeId typeId)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeReaction> IEveRepository.GetTypeReactions(Func<IQueryable<Data.Entities.TypeReactionEntity>, IQueryable<Data.Entities.TypeReactionEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeReaction> IEveRepository.GetTypeReactions(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.TypeReactionEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetTypeReactionById(EveTypeId reactionTypeId, bool input, EveTypeId typeId, out TypeReaction value)
    {
      throw new NotImplementedException();
    }

    Unit IEveRepository.GetUnitById(UnitId id)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveRepository.GetUnits(Func<IQueryable<Data.Entities.UnitEntity>, IQueryable<Data.Entities.UnitEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveRepository.GetUnits(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.UnitEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetUnitById(UnitId id, out Unit value)
    {
      throw new NotImplementedException();
    }

    Universe.Universe IEveRepository.GetUniverseById(Universe.UniverseId id)
    {
      throw new NotImplementedException();
    }


    IReadOnlyList<Universe.Universe> IEveRepository.GetUniverses(Func<IQueryable<Data.Entities.UniverseEntity>, IQueryable<Data.Entities.UniverseEntity>> queryOperations)
    {
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe.Universe> IEveRepository.GetUniverses(params FreeNet.Data.Entity.IQueryModifier<Data.Entities.UniverseEntity>[] modifiers)
    {
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetUniverseById(Universe.UniverseId id, out Universe.Universe value)
    {
      throw new NotImplementedException();
    }

    void IDisposable.Dispose()
    {
      throw new NotImplementedException();
    }
  }
}