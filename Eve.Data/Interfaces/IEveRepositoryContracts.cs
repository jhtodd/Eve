//-----------------------------------------------------------------------
// <copyright file="IEveRepositoryContracts.cs" company="Jeremy H. Todd">
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
  /// Contract class for the <see cref="IEveRepository" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveRepository))]
  internal abstract partial class IEveRepositoryContracts : IEveRepository
  {
    /* Methods */

    T IEveRepository.GetOrAddStoredValue<T>(IConvertible id, Func<T> valueFactory)
    {
      Contract.Requires(id != null, "The ID cannot be null.");
      Contract.Requires(valueFactory != null, "The value creation method cannot be null.");
      Contract.Ensures(Contract.Result<T>() != null);
      throw new NotImplementedException();
    }

    #region Activity Methods
    Activity IEveRepository.GetActivityById(ActivityId id)
    {
      Contract.Ensures(Contract.Result<Activity>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Activity> IEveRepository.GetActivities(Func<IQueryable<ActivityEntity>, IQueryable<ActivityEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Activity>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Activity> IEveRepository.GetActivities(params IQueryModifier<ActivityEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Activity>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetActivityById(ActivityId id, out Activity value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Agent Methods
    Agent IEveRepository.GetAgentById(AgentId id)
    {
      Contract.Ensures(Contract.Result<Agent>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Agent> IEveRepository.GetAgents(Func<IQueryable<AgentEntity>, IQueryable<AgentEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Agent> IEveRepository.GetAgents(params IQueryModifier<AgentEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Agent>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAgentById(AgentId id, out Agent value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AgentType Methods
    AgentType IEveRepository.GetAgentTypeById(AgentTypeId id)
    {
      Contract.Ensures(Contract.Result<AgentType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AgentType> IEveRepository.GetAgentTypes(Func<IQueryable<AgentTypeEntity>, IQueryable<AgentTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AgentType> IEveRepository.GetAgentTypes(params IQueryModifier<AgentTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AgentType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAgentTypeById(AgentTypeId id, out AgentType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Ancestry Methods
    Ancestry IEveRepository.GetAncestryById(AncestryId id)
    {
      Contract.Ensures(Contract.Result<Ancestry>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Ancestry> IEveRepository.GetAncestries(Func<IQueryable<AncestryEntity>, IQueryable<AncestryEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Ancestry>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Ancestry> IEveRepository.GetAncestries(params IQueryModifier<AncestryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Ancestry>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAncestryById(AncestryId id, out Ancestry value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AssemblyLine Methods
    AssemblyLine IEveRepository.GetAssemblyLineById(AssemblyLineId id)
    {
      Contract.Ensures(Contract.Result<AssemblyLine>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLine> IEveRepository.GetAssemblyLines(Func<IQueryable<AssemblyLineEntity>, IQueryable<AssemblyLineEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLine>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLine> IEveRepository.GetAssemblyLines(params IQueryModifier<AssemblyLineEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLine>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineById(AssemblyLineId id, out AssemblyLine value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AssemblyLineStation Methods
    AssemblyLineStation IEveRepository.GetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId)
    {
      Contract.Ensures(Contract.Result<AssemblyLineStation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineStation> IEveRepository.GetAssemblyLineStations(Func<IQueryable<AssemblyLineStationEntity>, IQueryable<AssemblyLineStationEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineStation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineStation> IEveRepository.GetAssemblyLineStations(params IQueryModifier<AssemblyLineStationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineStation>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineStationById(StationId stationId, AssemblyLineTypeId assemblyLineTypeId, out AssemblyLineStation value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AssemblyLineType Methods
    AssemblyLineType IEveRepository.GetAssemblyLineTypeById(AssemblyLineTypeId id)
    {
      Contract.Ensures(Contract.Result<AssemblyLineType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineType> IEveRepository.GetAssemblyLineTypes(Func<IQueryable<AssemblyLineTypeEntity>, IQueryable<AssemblyLineTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineType> IEveRepository.GetAssemblyLineTypes(params IQueryModifier<AssemblyLineTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineTypeById(AssemblyLineTypeId id, out AssemblyLineType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AssemblyLineTypeCategoryDetail Methods
    AssemblyLineTypeCategoryDetail IEveRepository.GetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId)
    {
      Contract.Ensures(Contract.Result<AssemblyLineTypeCategoryDetail>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineTypeCategoryDetail> IEveRepository.GetAssemblyLineTypeCategoryDetails(Func<IQueryable<AssemblyLineTypeCategoryDetailEntity>, IQueryable<AssemblyLineTypeCategoryDetailEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineTypeCategoryDetail>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineTypeCategoryDetail> IEveRepository.GetAssemblyLineTypeCategoryDetails(params IQueryModifier<AssemblyLineTypeCategoryDetailEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineTypeCategoryDetail>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineTypeCategoryDetailById(AssemblyLineTypeId assemblyLineTypeId, CategoryId categoryId, out AssemblyLineTypeCategoryDetail value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AssemblyLineTypeGroupDetail Methods
    AssemblyLineTypeGroupDetail IEveRepository.GetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId)
    {
      Contract.Ensures(Contract.Result<AssemblyLineTypeGroupDetail>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineTypeGroupDetail> IEveRepository.GetAssemblyLineTypeGroupDetails(Func<IQueryable<AssemblyLineTypeGroupDetailEntity>, IQueryable<AssemblyLineTypeGroupDetailEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineTypeGroupDetail>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AssemblyLineTypeGroupDetail> IEveRepository.GetAssemblyLineTypeGroupDetails(params IQueryModifier<AssemblyLineTypeGroupDetailEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AssemblyLineTypeGroupDetail>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAssemblyLineTypeGroupDetailById(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId, out AssemblyLineTypeGroupDetail value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeCategory Methods
    AttributeCategory IEveRepository.GetAttributeCategoryById(AttributeCategoryId id)
    {
      Contract.Ensures(Contract.Result<AttributeCategory>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveRepository.GetAttributeCategories(Func<IQueryable<AttributeCategoryEntity>, IQueryable<AttributeCategoryEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeCategory> IEveRepository.GetAttributeCategories(params IQueryModifier<AttributeCategoryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeCategory>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAttributeCategoryById(AttributeCategoryId id, out AttributeCategory value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeType Methods
    AttributeType IEveRepository.GetAttributeTypeById(AttributeId id)
    {
      Contract.Ensures(Contract.Result<AttributeType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveRepository.GetAttributeTypes(Func<IQueryable<AttributeTypeEntity>, IQueryable<AttributeTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeType> IEveRepository.GetAttributeTypes(params IQueryModifier<AttributeTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAttributeTypeById(AttributeId id, out AttributeType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region AttributeValue Methods
    AttributeValue IEveRepository.GetAttributeValueById(EveTypeId itemTypeId, AttributeId attributeId)
    {
      Contract.Ensures(Contract.Result<AttributeValue>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveRepository.GetAttributeValues(Func<IQueryable<AttributeValueEntity>, IQueryable<AttributeValueEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<AttributeValue> IEveRepository.GetAttributeValues(params IQueryModifier<AttributeValueEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<AttributeValue>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetAttributeValueById(EveTypeId itemTypeId, AttributeId attributeId, out AttributeValue value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Bloodline Methods
    Bloodline IEveRepository.GetBloodlineById(BloodlineId id)
    {
      Contract.Ensures(Contract.Result<Bloodline>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Bloodline> IEveRepository.GetBloodlines(Func<IQueryable<BloodlineEntity>, IQueryable<BloodlineEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Bloodline>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Bloodline> IEveRepository.GetBloodlines(params IQueryModifier<BloodlineEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Bloodline>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetBloodlineById(BloodlineId id, out Bloodline value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Category Methods
    Category IEveRepository.GetCategoryById(CategoryId id)
    {
      Contract.Ensures(Contract.Result<Category>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveRepository.GetCategories(Func<IQueryable<CategoryEntity>, IQueryable<CategoryEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Category> IEveRepository.GetCategories(params IQueryModifier<CategoryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Category>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCategoryById(CategoryId id, out Category value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Celestial Methods
    Celestial IEveRepository.GetCelestialById(CelestialId id)
    {
      Contract.Ensures(Contract.Result<Celestial>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Celestial> IEveRepository.GetCelestials(Func<IQueryable<CelestialEntity>, IQueryable<CelestialEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Celestial>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Celestial> IEveRepository.GetCelestials(params IQueryModifier<CelestialEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Celestial>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCelestialById(CelestialId id, out Celestial value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Certificate Methods
    Certificate IEveRepository.GetCertificateById(CertificateId id)
    {
      Contract.Ensures(Contract.Result<Certificate>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Certificate> IEveRepository.GetCertificates(Func<IQueryable<CertificateEntity>, IQueryable<CertificateEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Certificate>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Certificate> IEveRepository.GetCertificates(params IQueryModifier<CertificateEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Certificate>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateById(CertificateId id, out Certificate value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CertificateCategory Methods
    CertificateCategory IEveRepository.GetCertificateCategoryById(CertificateCategoryId id)
    {
      Contract.Ensures(Contract.Result<CertificateCategory>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateCategory> IEveRepository.GetCertificateCategories(Func<IQueryable<CertificateCategoryEntity>, IQueryable<CertificateCategoryEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateCategory>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateCategory> IEveRepository.GetCertificateCategories(params IQueryModifier<CertificateCategoryEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateCategory>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateCategoryById(CertificateCategoryId id, out CertificateCategory value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CertificateClass Methods
    CertificateClass IEveRepository.GetCertificateClassById(CertificateClassId id)
    {
      Contract.Ensures(Contract.Result<CertificateClass>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateClass> IEveRepository.GetCertificateClasses(Func<IQueryable<CertificateClassEntity>, IQueryable<CertificateClassEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateClass>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateClass> IEveRepository.GetCertificateClasses(params IQueryModifier<CertificateClassEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateClass>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateClassById(CertificateClassId id, out CertificateClass value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CertificateRecommendation Methods
    CertificateRecommendation IEveRepository.GetCertificateRecommendationById(CertificateRecommendationId id)
    {
      Contract.Ensures(Contract.Result<CertificateRecommendation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateRecommendation> IEveRepository.GetCertificateRecommendations(Func<IQueryable<CertificateRecommendationEntity>, IQueryable<CertificateRecommendationEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateRecommendation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateRecommendation> IEveRepository.GetCertificateRecommendations(params IQueryModifier<CertificateRecommendationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateRecommendation>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateRecommendationById(CertificateRecommendationId id, out CertificateRecommendation value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CertificateRelationship Methods
    CertificateRelationship IEveRepository.GetCertificateRelationshipById(CertificateRelationshipId id)
    {
      Contract.Ensures(Contract.Result<CertificateRelationship>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateRelationship> IEveRepository.GetCertificateRelationships(Func<IQueryable<CertificateRelationshipEntity>, IQueryable<CertificateRelationshipEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateRelationship>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CertificateRelationship> IEveRepository.GetCertificateRelationships(params IQueryModifier<CertificateRelationshipEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CertificateRelationship>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCertificateRelationshipById(CertificateRelationshipId id, out CertificateRelationship value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CharacterAttributeType Methods
    CharacterAttributeType IEveRepository.GetCharacterAttributeTypeById(CharacterAttributeId id)
    {
      Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CharacterAttributeType> IEveRepository.GetCharacterAttributeTypes(Func<IQueryable<CharacterAttributeTypeEntity>, IQueryable<CharacterAttributeTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CharacterAttributeType> IEveRepository.GetCharacterAttributeTypes(params IQueryModifier<CharacterAttributeTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CharacterAttributeType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCharacterAttributeTypeById(CharacterAttributeId id, out CharacterAttributeType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Constellation Methods
    Constellation IEveRepository.GetConstellationById(ConstellationId id)
    {
      Contract.Ensures(Contract.Result<Constellation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Constellation> IEveRepository.GetConstellations(Func<IQueryable<ConstellationEntity>, IQueryable<ConstellationEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Constellation> IEveRepository.GetConstellations(params IQueryModifier<ConstellationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Constellation>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetConstellationById(ConstellationId id, out Constellation value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region ConstellationJump Methods
    ConstellationJump IEveRepository.GetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId)
    {
      Contract.Ensures(Contract.Result<ConstellationJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ConstellationJump> IEveRepository.GetConstellationJumps(Func<IQueryable<ConstellationJumpEntity>, IQueryable<ConstellationJumpEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ConstellationJump> IEveRepository.GetConstellationJumps(params IQueryModifier<ConstellationJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ConstellationJump>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetConstellationJumpById(ConstellationId fromConstellationId, ConstellationId toConstellationId, out ConstellationJump value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region ContrabandInfo Methods
    ContrabandInfo IEveRepository.GetContrabandInfoById(FactionId factionId, EveTypeId typeId)
    {
      Contract.Ensures(Contract.Result<ContrabandInfo>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ContrabandInfo> IEveRepository.GetContrabandInfo(Func<IQueryable<ContrabandInfoEntity>, IQueryable<ContrabandInfoEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<ContrabandInfo>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ContrabandInfo> IEveRepository.GetContrabandInfo(params IQueryModifier<ContrabandInfoEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ContrabandInfo>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetContrabandInfoById(FactionId factionId, EveTypeId typeId, out ContrabandInfo value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region ControlTowerResource Methods
    ControlTowerResource IEveRepository.GetControlTowerResourceById(EveTypeId controlTowerTypeId, EveTypeId resourceTypeId)
    {
      Contract.Ensures(Contract.Result<ControlTowerResource>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ControlTowerResource> IEveRepository.GetControlTowerResources(Func<IQueryable<ControlTowerResourceEntity>, IQueryable<ControlTowerResourceEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<ControlTowerResource>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ControlTowerResource> IEveRepository.GetControlTowerResources(params IQueryModifier<ControlTowerResourceEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ControlTowerResource>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetControlTowerResourceById(EveTypeId controlTowerTypeId, EveTypeId resourceTypeId, out ControlTowerResource value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region CorporateActivity Methods
    CorporateActivity IEveRepository.GetCorporateActivityById(CorporateActivityId id)
    {
      Contract.Ensures(Contract.Result<CorporateActivity>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CorporateActivity> IEveRepository.GetCorporateActivities(Func<IQueryable<CorporateActivityEntity>, IQueryable<CorporateActivityEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<CorporateActivity> IEveRepository.GetCorporateActivities(params IQueryModifier<CorporateActivityEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<CorporateActivity>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetCorporateActivityById(CorporateActivityId id, out CorporateActivity value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Division Methods
    Division IEveRepository.GetDivisionById(DivisionId id)
    {
      Contract.Ensures(Contract.Result<Division>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Division> IEveRepository.GetDivisions(Func<IQueryable<DivisionEntity>, IQueryable<DivisionEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Division> IEveRepository.GetDivisions(params IQueryModifier<DivisionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Division>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetDivisionById(DivisionId id, out Division value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EffectType Methods
    EffectType IEveRepository.GetEffectTypeById(EffectId id)
    {
      Contract.Ensures(Contract.Result<EffectType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveRepository.GetEffectTypes(Func<IQueryable<EffectTypeEntity>, IQueryable<EffectTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EffectType> IEveRepository.GetEffectTypes(params IQueryModifier<EffectTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EffectType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEffectTypeById(EffectId id, out EffectType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Effect Methods
    Effect IEveRepository.GetEffectById(EveTypeId itemTypeId, EffectId effectId)
    {
      Contract.Ensures(Contract.Result<Effect>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveRepository.GetEffects(Func<IQueryable<EffectEntity>, IQueryable<EffectEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Effect> IEveRepository.GetEffects(params IQueryModifier<EffectEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Effect>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEffectById(EveTypeId itemTypeId, EffectId effectId, out Effect value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region EveType Methods
    EveType IEveRepository.GetEveTypeById(EveTypeId id)
    {
      Contract.Ensures(Contract.Result<EveType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveRepository.GetEveTypes(Func<IQueryable<EveTypeEntity>, IQueryable<EveTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<EveType> IEveRepository.GetEveTypes(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<EveType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEveTypeById(EveTypeId id, out EveType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }

    TEveType IEveRepository.GetEveTypeById<TEveType>(EveTypeId id)
    {
      Contract.Ensures(Contract.Result<TEveType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveRepository.GetEveTypes<TEveType>(Func<IQueryable<EveTypeEntity>, IQueryable<EveTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TEveType> IEveRepository.GetEveTypes<TEveType>(params IQueryModifier<EveTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TEveType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetEveTypeById<TEveType>(EveTypeId id, out TEveType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Faction Methods
    Faction IEveRepository.GetFactionById(FactionId id)
    {
      Contract.Ensures(Contract.Result<Faction>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Faction> IEveRepository.GetFactions(Func<IQueryable<FactionEntity>, IQueryable<FactionEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Faction> IEveRepository.GetFactions(params IQueryModifier<FactionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Faction>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetFactionById(FactionId id, out Faction value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Graphic Methods
    Graphic IEveRepository.GetGraphicById(GraphicId id)
    {
      Contract.Ensures(Contract.Result<Graphic>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Graphic> IEveRepository.GetGraphics(Func<IQueryable<GraphicEntity>, IQueryable<GraphicEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Graphic>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Graphic> IEveRepository.GetGraphics(params IQueryModifier<GraphicEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Graphic>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetGraphicById(GraphicId id, out Graphic value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Group Methods
    Group IEveRepository.GetGroupById(GroupId id)
    {
      Contract.Ensures(Contract.Result<Group>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveRepository.GetGroups(Func<IQueryable<GroupEntity>, IQueryable<GroupEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Group> IEveRepository.GetGroups(params IQueryModifier<GroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Group>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetGroupById(GroupId id, out Group value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Icon Methods
    Icon IEveRepository.GetIconById(IconId id)
    {
      Contract.Ensures(Contract.Result<Icon>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveRepository.GetIcons(Func<IQueryable<IconEntity>, IQueryable<IconEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Icon> IEveRepository.GetIcons(params IQueryModifier<IconEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Icon>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetIconById(IconId id, out Icon value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Item Methods
    Item IEveRepository.GetItemById(ItemId id)
    {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveRepository.GetItems(Func<IQueryable<ItemEntity>, IQueryable<ItemEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Item> IEveRepository.GetItems(params IQueryModifier<ItemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Item>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetItemById(ItemId id, out Item value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }

    TItem IEveRepository.GetItemById<TItem>(ItemId id)
    {
      Contract.Ensures(Contract.Result<Item>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveRepository.GetItems<TItem>(Func<IQueryable<ItemEntity>, IQueryable<ItemEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TItem> IEveRepository.GetItems<TItem>(params IQueryModifier<ItemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TItem>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetItemById<TItem>(ItemId id, out TItem value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region ItemPosition Methods
    ItemPosition IEveRepository.GetItemPositionById(ItemId id)
    {
      Contract.Ensures(Contract.Result<ItemPosition>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ItemPosition> IEveRepository.GetItemPositions(Func<IQueryable<ItemPositionEntity>, IQueryable<ItemPositionEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<ItemPosition>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<ItemPosition> IEveRepository.GetItemPositions(params IQueryModifier<ItemPositionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<ItemPosition>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetItemPositionById(ItemId id, out ItemPosition value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MarketGroup Methods
    MarketGroup IEveRepository.GetMarketGroupById(MarketGroupId id)
    {
      Contract.Ensures(Contract.Result<MarketGroup>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveRepository.GetMarketGroups(Func<IQueryable<MarketGroupEntity>, IQueryable<MarketGroupEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MarketGroup> IEveRepository.GetMarketGroups(params IQueryModifier<MarketGroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MarketGroup>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetMarketGroupById(MarketGroupId id, out MarketGroup value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaGroup Methods
    MetaGroup IEveRepository.GetMetaGroupById(MetaGroupId id)
    {
      Contract.Ensures(Contract.Result<MetaGroup>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveRepository.GetMetaGroups(Func<IQueryable<MetaGroupEntity>, IQueryable<MetaGroupEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaGroup> IEveRepository.GetMetaGroups(params IQueryModifier<MetaGroupEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaGroup>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetMetaGroupById(MetaGroupId id, out MetaGroup value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region MetaType Methods
    MetaType IEveRepository.GetMetaTypeById(EveTypeId id)
    {
      Contract.Ensures(Contract.Result<MetaType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveRepository.GetMetaTypes(Func<IQueryable<MetaTypeEntity>, IQueryable<MetaTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<MetaType> IEveRepository.GetMetaTypes(params IQueryModifier<MetaTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<MetaType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetMetaTypeById(EveTypeId id, out MetaType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporation Methods
    NpcCorporation IEveRepository.GetNpcCorporationById(NpcCorporationId id)
    {
      Contract.Ensures(Contract.Result<NpcCorporation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporation> IEveRepository.GetNpcCorporations(Func<IQueryable<NpcCorporationEntity>, IQueryable<NpcCorporationEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporation> IEveRepository.GetNpcCorporations(params IQueryModifier<NpcCorporationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporation>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetNpcCorporationById(NpcCorporationId id, out NpcCorporation value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region NpcCorporationDivision Methods
    NpcCorporationDivision IEveRepository.GetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId)
    {
      Contract.Ensures(Contract.Result<NpcCorporationDivision>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporationDivision> IEveRepository.GetNpcCorporationDivisions(Func<IQueryable<NpcCorporationDivisionEntity>, IQueryable<NpcCorporationDivisionEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<NpcCorporationDivision> IEveRepository.GetNpcCorporationDivisions(params IQueryModifier<NpcCorporationDivisionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<NpcCorporationDivision>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetNpcCorporationDivisionById(NpcCorporationId corporationId, DivisionId divisionId, out NpcCorporationDivision value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Race Methods
    Race IEveRepository.GetRaceById(RaceId id)
    {
      Contract.Ensures(Contract.Result<Race>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Race> IEveRepository.GetRaces(Func<IQueryable<RaceEntity>, IQueryable<RaceEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Race> IEveRepository.GetRaces(params IQueryModifier<RaceEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Race>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetRaceById(RaceId id, out Race value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Region Methods
    Region IEveRepository.GetRegionById(RegionId id)
    {
      Contract.Ensures(Contract.Result<Region>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Region> IEveRepository.GetRegions(Func<IQueryable<RegionEntity>, IQueryable<RegionEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Region> IEveRepository.GetRegions(params IQueryModifier<RegionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Region>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetRegionById(RegionId id, out Region value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region RegionJump Methods
    RegionJump IEveRepository.GetRegionJumpById(RegionId fromRegionId, RegionId toRegionId)
    {
      Contract.Ensures(Contract.Result<RegionJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<RegionJump> IEveRepository.GetRegionJumps(Func<IQueryable<RegionJumpEntity>, IQueryable<RegionJumpEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<RegionJump> IEveRepository.GetRegionJumps(params IQueryModifier<RegionJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<RegionJump>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetRegionJumpById(RegionId fromRegionId, RegionId toRegionId, out RegionJump value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystem Methods
    SolarSystem IEveRepository.GetSolarSystemById(SolarSystemId id)
    {
      Contract.Ensures(Contract.Result<SolarSystem>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystem> IEveRepository.GetSolarSystems(Func<IQueryable<SolarSystemEntity>, IQueryable<SolarSystemEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystem> IEveRepository.GetSolarSystems(params IQueryModifier<SolarSystemEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystem>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetSolarSystemById(SolarSystemId id, out SolarSystem value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region SolarSystemJump Methods
    SolarSystemJump IEveRepository.GetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId)
    {
      Contract.Ensures(Contract.Result<SolarSystemJump>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystemJump> IEveRepository.GetSolarSystemJumps(Func<IQueryable<SolarSystemJumpEntity>, IQueryable<SolarSystemJumpEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<SolarSystemJump> IEveRepository.GetSolarSystemJumps(params IQueryModifier<SolarSystemJumpEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<SolarSystemJump>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetSolarSystemJumpById(SolarSystemId fromSolarSystemId, SolarSystemId toSolarSystemId, out SolarSystemJump value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Stargate Methods
    Stargate IEveRepository.GetStargateById(StargateId id)
    {
      Contract.Ensures(Contract.Result<Stargate>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Stargate> IEveRepository.GetStargates(Func<IQueryable<StargateEntity>, IQueryable<StargateEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Stargate>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Stargate> IEveRepository.GetStargates(params IQueryModifier<StargateEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Stargate>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStargateById(StargateId id, out Stargate value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Station Methods
    Station IEveRepository.GetStationById(StationId id)
    {
      Contract.Ensures(Contract.Result<Station>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Station> IEveRepository.GetStations(Func<IQueryable<StationEntity>, IQueryable<StationEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Station>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Station> IEveRepository.GetStations(params IQueryModifier<StationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Station>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationById(StationId id, out Station value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationOperation Methods
    StationOperation IEveRepository.GetStationOperationById(StationOperationId id)
    {
      Contract.Ensures(Contract.Result<StationOperation>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationOperation> IEveRepository.GetStationOperations(Func<IQueryable<StationOperationEntity>, IQueryable<StationOperationEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationOperation> IEveRepository.GetStationOperations(params IQueryModifier<StationOperationEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationOperation>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationOperationById(StationOperationId id, out StationOperation value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationService Methods
    StationService IEveRepository.GetStationServiceById(StationServiceId id)
    {
      Contract.Ensures(Contract.Result<StationService>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationService> IEveRepository.GetStationServices(Func<IQueryable<StationServiceEntity>, IQueryable<StationServiceEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationService> IEveRepository.GetStationServices(params IQueryModifier<StationServiceEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationService>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationServiceById(StationServiceId id, out StationService value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region StationType Methods
    StationType IEveRepository.GetStationTypeById(EveTypeId id)
    {
      Contract.Ensures(Contract.Result<StationType>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationType> IEveRepository.GetStationTypes(Func<IQueryable<StationTypeEntity>, IQueryable<StationTypeEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<StationType> IEveRepository.GetStationTypes(params IQueryModifier<StationTypeEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<StationType>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetStationTypeById(EveTypeId id, out StationType value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region TypeMaterial Methods
    TypeMaterial IEveRepository.GetTypeMaterialById(EveTypeId typeId, EveTypeId materialTypeId)
    {
      Contract.Ensures(Contract.Result<TypeMaterial>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeMaterial> IEveRepository.GetTypeMaterials(Func<IQueryable<TypeMaterialEntity>, IQueryable<TypeMaterialEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TypeMaterial>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeMaterial> IEveRepository.GetTypeMaterials(params IQueryModifier<TypeMaterialEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TypeMaterial>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetTypeMaterialById(EveTypeId typeId, EveTypeId materialTypeId, out TypeMaterial value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region TypeReaction Methods
    TypeReaction IEveRepository.GetTypeReactionById(EveTypeId reactionTypeId, bool input, EveTypeId typeId)
    {
      Contract.Ensures(Contract.Result<TypeReaction>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeReaction> IEveRepository.GetTypeReactions(Func<IQueryable<TypeReactionEntity>, IQueryable<TypeReactionEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<TypeReaction>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<TypeReaction> IEveRepository.GetTypeReactions(params IQueryModifier<TypeReactionEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<TypeReaction>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetTypeReactionById(EveTypeId reactionTypeId, bool input, EveTypeId typeId, out TypeReaction value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Unit Methods
    Unit IEveRepository.GetUnitById(UnitId id)
    {
      Contract.Ensures(Contract.Result<Unit>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveRepository.GetUnits(Func<IQueryable<UnitEntity>, IQueryable<UnitEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Unit> IEveRepository.GetUnits(params IQueryModifier<UnitEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Unit>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetUnitById(UnitId id, out Unit value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region Universe Methods
    Universe IEveRepository.GetUniverseById(UniverseId id)
    {
      Contract.Ensures(Contract.Result<Universe>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe> IEveRepository.GetUniverses(Func<IQueryable<UniverseEntity>, IQueryable<UniverseEntity>> queryOperations)
    {
      Contract.Requires(queryOperations != null, "The query operations delegate cannot be null.");
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }

    IReadOnlyList<Universe> IEveRepository.GetUniverses(params IQueryModifier<UniverseEntity>[] modifiers)
    {
      Contract.Requires(modifiers != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IReadOnlyList<Universe>>() != null);
      throw new NotImplementedException();
    }

    bool IEveRepository.TryGetUniverseById(UniverseId id, out Universe value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
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