//-----------------------------------------------------------------------
// <copyright file="IEveDbContextContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  /// <summary>
  /// Contract class for the <see cref="IEveDbContext" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveDbContext))]
  internal abstract partial class IEveDbContextContracts : IEveDbContext
  {
    IQueryable<ActivityEntity> IEveDbContext.Activities
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<ActivityEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AgentEntity> IEveDbContext.Agents
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AgentEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AgentTypeEntity> IEveDbContext.AgentTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AgentTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AncestryEntity> IEveDbContext.Ancestries
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AncestryEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AssemblyLineEntity> IEveDbContext.AssemblyLines
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AssemblyLineEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AssemblyLineStationEntity> IEveDbContext.AssemblyLineStations
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AssemblyLineStationEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AssemblyLineTypeEntity> IEveDbContext.AssemblyLineTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AssemblyLineTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AssemblyLineTypeCategoryDetailEntity> IEveDbContext.AssemblyLineTypeCategoryDetails
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AssemblyLineTypeCategoryDetailEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AssemblyLineTypeGroupDetailEntity> IEveDbContext.AssemblyLineTypeGroupDetails
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AssemblyLineTypeGroupDetailEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AttributeCategoryEntity> IEveDbContext.AttributeCategories
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AttributeCategoryEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AttributeTypeEntity> IEveDbContext.AttributeTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AttributeTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<AttributeValueEntity> IEveDbContext.AttributeValues
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<AttributeValueEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<BloodlineEntity> IEveDbContext.Bloodlines
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<BloodlineEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CategoryEntity> IEveDbContext.Categories
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CategoryEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CelestialEntity> IEveDbContext.Celestials
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CelestialEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CertificateEntity> IEveDbContext.Certificates
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CertificateEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CertificateCategoryEntity> IEveDbContext.CertificateCategories
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CertificateCategoryEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CertificateClassEntity> IEveDbContext.CertificateClasses
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CertificateClassEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CertificateRecommendationEntity> IEveDbContext.CertificateRecommendations
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CertificateRecommendationEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CertificateRelationshipEntity> IEveDbContext.CertificateRelationships
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CertificateRelationshipEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CharacterAttributeTypeEntity> IEveDbContext.CharacterAttributeTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CharacterAttributeTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<ConstellationEntity> IEveDbContext.Constellations
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<ConstellationEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<ConstellationJumpEntity> IEveDbContext.ConstellationJumps
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<ConstellationJumpEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<ContrabandInfoEntity> IEveDbContext.ContrabandInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<ContrabandInfoEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<ControlTowerResourceEntity> IEveDbContext.ControlTowerResources
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<ControlTowerResourceEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<CorporateActivityEntity> IEveDbContext.CorporateActivities
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<CorporateActivityEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<DivisionEntity> IEveDbContext.Divisions
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<DivisionEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<EffectTypeEntity> IEveDbContext.EffectTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<EffectTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<EffectEntity> IEveDbContext.Effects
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<EffectEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<EveTypeEntity> IEveDbContext.EveTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<EveTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<FactionEntity> IEveDbContext.Factions
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<FactionEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<FlagEntity> IEveDbContext.Flags
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<FlagEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<GraphicEntity> IEveDbContext.Graphics
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<GraphicEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<GroupEntity> IEveDbContext.Groups
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<GroupEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<IconEntity> IEveDbContext.Icons
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<IconEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<ItemEntity> IEveDbContext.Items
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<ItemEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<ItemPositionEntity> IEveDbContext.ItemPositions
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<ItemPositionEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<MarketGroupEntity> IEveDbContext.MarketGroups
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<MarketGroupEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<MetaGroupEntity> IEveDbContext.MetaGroups
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<MetaGroupEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<MetaTypeEntity> IEveDbContext.MetaTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<MetaTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<NpcCorporationEntity> IEveDbContext.NpcCorporations
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<NpcCorporationEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<NpcCorporationDivisionEntity> IEveDbContext.NpcCorporationDivisions
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<NpcCorporationDivisionEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<RaceEntity> IEveDbContext.Races
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<RaceEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<RegionEntity> IEveDbContext.Regions
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<RegionEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<RegionJumpEntity> IEveDbContext.RegionJumps
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<RegionJumpEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<SolarSystemEntity> IEveDbContext.SolarSystems
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<SolarSystemEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<SolarSystemJumpEntity> IEveDbContext.SolarSystemJumps
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<SolarSystemJumpEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<StargateEntity> IEveDbContext.Stargates
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<StargateEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<StationEntity> IEveDbContext.Stations
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<StationEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<StationOperationEntity> IEveDbContext.StationOperations
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<StationOperationEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<StationServiceEntity> IEveDbContext.StationServices
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<StationServiceEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<StationTypeEntity> IEveDbContext.StationTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<StationTypeEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<TypeMaterialEntity> IEveDbContext.TypeMaterials
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<TypeMaterialEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<TypeReactionEntity> IEveDbContext.TypeReactions
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<TypeReactionEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<UnitEntity> IEveDbContext.Units
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<UnitEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<UniverseEntity> IEveDbContext.Universes
    {
      get
      {
        Contract.Ensures(Contract.Result<IQueryable<UniverseEntity>>() != null);
        throw new NotImplementedException();
      }
    }

    IQueryable<TEntity> IEveDbContext.Query<TEntity>()
    {
      Contract.Ensures(Contract.Result<IQueryable<TEntity>>() != null);
      throw new NotImplementedException();
    }
  }

  #region IDisposable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IDisposable" /> interface.
  /// </content>
  internal abstract partial class IEveDbContextContracts : IDisposable
  {
    void IDisposable.Dispose()
    {
      throw new NotImplementedException();
    }
  }
  #endregion
}