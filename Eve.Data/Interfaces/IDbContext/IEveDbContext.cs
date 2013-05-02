//-----------------------------------------------------------------------
// <copyright file="IEveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for objects that provide data access and change tracking
  /// for the application.
  /// </summary>
  [ContractClass(typeof(IEveDbContextContracts))]
  public interface IEveDbContext : IDisposable
  {
    /* Properties */

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query industrial activities.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for industrial activities.
    /// </value>
    IQueryable<ActivityEntity> Activities { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query agents.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for agents.
    /// </value>
    IQueryable<AgentEntity> Agents { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query agent types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for agent types.
    /// </value>
    IQueryable<AgentTypeEntity> AgentTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query ancestries.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for ancestries.
    /// </value>
    IQueryable<AncestryEntity> Ancestries { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query
    /// information about the assembly lines.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for assembly line information.
    /// </value>
    IQueryable<AssemblyLineEntity> AssemblyLines { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query
    /// information about the types of assembly lines at stations.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for assembly line station information.
    /// </value>
    IQueryable<AssemblyLineStationEntity> AssemblyLineStations { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query assembly line type.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for assembly line types.
    /// </value>
    IQueryable<AssemblyLineTypeEntity> AssemblyLineTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query assembly line category detail information.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for assembly line type category details.
    /// </value>
    IQueryable<AssemblyLineTypeCategoryDetailEntity> AssemblyLineTypeCategoryDetails { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query assembly line group detail information.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for assembly line type group details.
    /// </value>
    IQueryable<AssemblyLineTypeGroupDetailEntity> AssemblyLineTypeGroupDetails { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query attribute categories.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for attribute categories.
    /// </value>
    IQueryable<AttributeCategoryEntity> AttributeCategories { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query attribute types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for attribute types.
    /// </value>
    IQueryable<AttributeTypeEntity> AttributeTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query attribute values.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for attribute values.
    /// </value>
    IQueryable<AttributeValueEntity> AttributeValues { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query bloodlines.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for bloodlines.
    /// </value>
    IQueryable<BloodlineEntity> Bloodlines { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query categories.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for categories.
    /// </value>
    IQueryable<CategoryEntity> Categories { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query celestial objects.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for celestials.
    /// </value>
    IQueryable<CelestialEntity> Celestials { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query certificates.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for certificates.
    /// </value>
    IQueryable<CertificateEntity> Certificates { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query certificate categories.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for certificate categories.
    /// </value>
    IQueryable<CertificateCategoryEntity> CertificateCategories { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query certificate classes.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for certificate classes.
    /// </value>
    IQueryable<CertificateClassEntity> CertificateClasses { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query certificate recommendations.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for certificate recommendations.
    /// </value>
    IQueryable<CertificateRecommendationEntity> CertificateRecommendations { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query certificate relationships.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for certificate relationships.
    /// </value>
    IQueryable<CertificateRelationshipEntity> CertificateRelationships { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query character attribute types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for character attribute types.
    /// </value>
    IQueryable<CharacterAttributeTypeEntity> CharacterAttributeTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query constellations.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for constellations.
    /// </value>
    IQueryable<ConstellationEntity> Constellations { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query constellation jumps.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for constellation jumps.
    /// </value>
    IQueryable<ConstellationJumpEntity> ConstellationJumps { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query contraband info.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for contraband info.
    /// </value>
    IQueryable<ContrabandInfoEntity> ContrabandInfo { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query control tower resources.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for control tower resources.
    /// </value>
    IQueryable<ControlTowerResourceEntity> ControlTowerResources { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query corporate activities.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for corporate activities.
    /// </value>
    IQueryable<CorporateActivityEntity> CorporateActivities { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query divisions.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for divisions.
    /// </value>
    IQueryable<DivisionEntity> Divisions { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query effect types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for effect types.
    /// </value>
    IQueryable<EffectTypeEntity> EffectTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query effect values.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for effect values.
    /// </value>
    IQueryable<EffectEntity> Effects { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query item types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for item types.
    /// </value>
    IQueryable<EveTypeEntity> EveTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query factions.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for factions.
    /// </value>
    IQueryable<FactionEntity> Factions { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query flag types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for flag types.
    /// </value>
    IQueryable<FlagEntity> Flags { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query graphics.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for graphics.
    /// </value>
    IQueryable<GraphicEntity> Graphics { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query groups.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for groups.
    /// </value>
    IQueryable<GroupEntity> Groups { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query icons.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for icons.
    /// </value>
    IQueryable<IconEntity> Icons { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query general items.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for general items.
    /// </value>
    IQueryable<ItemEntity> Items { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query the positions of items.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for the positions of items.
    /// </value>
    IQueryable<ItemPositionEntity> ItemPositions { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query market groups.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for market groups.
    /// </value>
    IQueryable<MarketGroupEntity> MarketGroups { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query meta groups.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for meta groups.
    /// </value>
    IQueryable<MetaGroupEntity> MetaGroups { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query meta types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for meta types.
    /// </value>
    IQueryable<MetaTypeEntity> MetaTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query NPC corporations.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for NPC corporations.
    /// </value>
    IQueryable<NpcCorporationEntity> NpcCorporations { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query divisions of NPC corporations.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for divisions of NPC corporations.
    /// </value>
    IQueryable<NpcCorporationDivisionEntity> NpcCorporationDivisions { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query races.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for races.
    /// </value>
    IQueryable<RaceEntity> Races { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query regions.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for regions.
    /// </value>
    IQueryable<RegionEntity> Regions { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query region jumps.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for region jumps.
    /// </value>
    IQueryable<RegionJumpEntity> RegionJumps { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query solar systems.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for solar Systems.
    /// </value>
    IQueryable<SolarSystemEntity> SolarSystems { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query solar system jumps.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for solar system jumps.
    /// </value>
    IQueryable<SolarSystemJumpEntity> SolarSystemJumps { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query stargates.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for stargates.
    /// </value>
    IQueryable<StargateEntity> Stargates { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query stations.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for stations.
    /// </value>
    IQueryable<StationEntity> Stations { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query station operations.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for station operations.
    /// </value>
    IQueryable<StationOperationEntity> StationOperations { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query station services.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for station services.
    /// </value>
    IQueryable<StationServiceEntity> StationServices { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query station types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for station types.
    /// </value>
    IQueryable<StationTypeEntity> StationTypes { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query materials required by types.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for materials required by types.
    /// </value>
    IQueryable<TypeMaterialEntity> TypeMaterials { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query units.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for units.
    /// </value>
    IQueryable<UnitEntity> Units { get; }

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query universes.
    /// </summary>
    /// <value>
    /// The <see cref="IQueryable{T}" /> for universes.
    /// </value>
    IQueryable<UniverseEntity> Universes { get; }

    /* Methods */

    /// <summary>
    /// Gets an <see cref="IQueryable{T}" /> that can be used to query the specified
    /// entity type.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The entity type returned by the resulting query.
    /// </typeparam>
    /// <returns>
    /// An <see cref="IQueryable{T}" /> for the specified entity type.
    /// </returns>
    IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEveEntity;
  }
}