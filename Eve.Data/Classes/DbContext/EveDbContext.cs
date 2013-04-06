//-----------------------------------------------------------------------
// <copyright file="EveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Common;
  using System.Data.Entity;
  using System.Data.Objects;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  /// <summary>
  /// A <see cref="DbContext" /> that provides data access to the EVE database.
  /// This access is read-only.
  /// </summary>
  public class EveDbContext : DbContext
  {
    private static readonly EveDbContext DefaultInstance = new EveDbContext();

    private IDbSet<AgentEntity> agents;
    private IDbSet<AgentTypeEntity> agentTypes;
    private IDbSet<AttributeCategoryEntity> attributeCategories;
    private IDbSet<AttributeTypeEntity> attributeTypes;
    private IDbSet<AttributeValueEntity> attributeValues;
    private IDbSet<CategoryEntity> categories;
    private IDbSet<CharacterAttributeTypeEntity> characterAttributeTypes;
    private IDbSet<ConstellationEntity> constellations;
    private IDbSet<ConstellationJumpEntity> constellationJumps;
    private IDbSet<CorporateActivityEntity> corporateActivities;
    private IDbSet<DivisionEntity> divisions;
    private IDbSet<EffectEntity> effects;
    private IDbSet<EffectTypeEntity> effectTypes;
    private IDbSet<EveTypeEntity> eveTypes;
    private IDbSet<FlagEntity> flags;
    private IDbSet<GroupEntity> groups;
    private IDbSet<IconEntity> icons;
    private IDbSet<ItemEntity> items;
    private IDbSet<MarketGroupEntity> marketGroups;
    private IDbSet<MetaGroupEntity> metaGroups;
    private IDbSet<MetaTypeEntity> metaTypes;
    private IDbSet<NpcCorporationEntity> npcCorporations;
    private IDbSet<NpcCorporationDivisionEntity> npcCorporationDivisions;
    private IDbSet<RaceEntity> races;
    private IDbSet<RegionEntity> regions;
    private IDbSet<RegionJumpEntity> regionJumps;
    private IDbSet<SolarSystemEntity> solarSystems;
    private IDbSet<SolarSystemJumpEntity> solarSystemJumps;
    private IDbSet<StationOperationEntity> stationOperations;
    private IDbSet<StationServiceEntity> stationServices;
    private IDbSet<StationTypeEntity> stationTypes;
    private IDbSet<UnitEntity> units;
    private IDbSet<UniverseEntity> universes;

    /* Constructors */

    /// <summary>
    /// Initializes static members of the <see cref="EveDbContext" /> class.
    /// </summary>
    static EveDbContext()
    {
      Database.SetInitializer<EveDbContext>(null);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDbContext" /> class using
    /// conventions to create the name of the database to which a connection will
    /// be made. By convention the name is the full name (namespace + class name)
    /// of the derived context class.  For more information on how this is used
    /// to create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    public EveDbContext() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDbContext" /> class using
    /// the given string as the name or connection string for the database to which
    /// a connection will be made.  For more information on how this is used to
    /// create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    /// <param name="nameOrConnectionString">
    /// Either the database name or a connection string.
    /// </param>
    public EveDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDbContext" /> class using
    /// the existing connection to connect to a database. The connection will not be
    /// disposed when the context is disposed.
    /// </summary>
    /// <param name="existingConnection">
    /// An existing connection to use for the new context.
    /// </param>
    /// <param name="contextOwnsConnection">
    /// If set to true the connection is disposed when the context is disposed,
    /// otherwise the caller must dispose the connection.
    /// </param>
    public EveDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the default data context.  This should be used in most circumstances.
    /// </summary>
    /// <value>
    /// The default <see cref="EveDbContext" />.
    /// </value>
    public static EveDbContext Default
    {
      get
      {
        Contract.Ensures(Contract.Result<EveDbContext>() != null);
        return DefaultInstance;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for agents.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for agents.
    /// </value>
    public IDbSet<AgentEntity> Agents
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<AgentEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.agents != null);
        return this.agents;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.agents = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for agent types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for agent types.
    /// </value>
    public IDbSet<AgentTypeEntity> AgentTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<AgentTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.agentTypes != null);
        return this.agentTypes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.agentTypes = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute categories.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute categories.
    /// </value>
    public IDbSet<AttributeCategoryEntity> AttributeCategories
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<AttributeCategoryEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.attributeCategories != null);
        return this.attributeCategories;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.attributeCategories = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute types.
    /// </value>
    public IDbSet<AttributeTypeEntity> AttributeTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<AttributeTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.attributeTypes != null);
        return this.attributeTypes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.attributeTypes = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute values.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute values.
    /// </value>
    public IDbSet<AttributeValueEntity> AttributeValues
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<AttributeValueEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.attributeValues != null);
        return this.attributeValues;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.attributeValues = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for categories.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for categories.
    /// </value>
    public IDbSet<CategoryEntity> Categories
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<CategoryEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.categories != null);
        return this.categories;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.categories = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for character attribute types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for character attribute types.
    /// </value>
    public IDbSet<CharacterAttributeTypeEntity> CharacterAttributeTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<CharacterAttributeTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.characterAttributeTypes != null);
        return this.characterAttributeTypes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.characterAttributeTypes = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for constellations.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for constellations.
    /// </value>
    public IDbSet<ConstellationEntity> Constellations
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<ConstellationEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.constellations != null);
        return this.constellations;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.constellations = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for constellation jumps.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for constellation jumps.
    /// </value>
    public IDbSet<ConstellationJumpEntity> ConstellationJumps
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<ConstellationJumpEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.constellationJumps != null);
        return this.constellationJumps;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.constellationJumps = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for corporate activities.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for corporate activities.
    /// </value>
    public IDbSet<CorporateActivityEntity> CorporateActivities
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<CorporateActivityEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.corporateActivities != null);
        return this.corporateActivities;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.corporateActivities = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for divisions.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for divisions.
    /// </value>
    public IDbSet<DivisionEntity> Divisions
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<DivisionEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.divisions != null);
        return this.divisions;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.divisions = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for effect types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for effect types.
    /// </value>
    public IDbSet<EffectTypeEntity> EffectTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<EffectTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.effectTypes != null);
        return this.effectTypes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.effectTypes = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for effect values.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for effect values.
    /// </value>
    public IDbSet<EffectEntity> Effects
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<EffectEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.effects != null);
        return this.effects;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.effects = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for item types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for item types.
    /// </value>
    public IDbSet<EveTypeEntity> EveTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<EveTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.eveTypes != null);
        return this.eveTypes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.eveTypes = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for flag types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for flag types.
    /// </value>
    public IDbSet<FlagEntity> Flags
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<FlagEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.flags != null);
        return this.flags;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.flags = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for groups.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for groups.
    /// </value>
    public IDbSet<GroupEntity> Groups
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<GroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.groups != null);
        return this.groups;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.groups = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for icons.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for icons.
    /// </value>
    public IDbSet<IconEntity> Icons
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<IconEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.icons != null);
        return this.icons;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.icons = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for general items.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for general items.
    /// </value>
    public IDbSet<ItemEntity> Items
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<ItemEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.items != null);
        return this.items;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.items = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for market groups.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for market groups.
    /// </value>
    public IDbSet<MarketGroupEntity> MarketGroups
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<MarketGroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.marketGroups != null);
        return this.marketGroups;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.marketGroups = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for meta groups.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for meta groups.
    /// </value>
    public IDbSet<MetaGroupEntity> MetaGroups
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<MetaGroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.metaGroups != null);
        return this.metaGroups;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.metaGroups = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for meta types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for meta types.
    /// </value>
    public IDbSet<MetaTypeEntity> MetaTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<MetaTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.metaTypes != null);
        return this.metaTypes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.metaTypes = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for NPC corporations.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for NPC corporations.
    /// </value>
    public IDbSet<NpcCorporationEntity> NpcCorporations
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<NpcCorporationEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.npcCorporations != null);
        return this.npcCorporations;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.npcCorporations = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for divisions of NPC corporations.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for divisions of NPC corporations.
    /// </value>
    public IDbSet<NpcCorporationDivisionEntity> NpcCorporationDivisions
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<NpcCorporationDivisionEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.npcCorporationDivisions != null);
        return this.npcCorporationDivisions;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.npcCorporationDivisions = value;
      }
    }

    /// <summary>
    /// Gets the <see cref="ObjectContext" /> underlying the current
    /// <see cref="DbContext" />.
    /// </summary>
    /// <value>
    /// The <see cref="ObjectContext" /> underlying the current
    /// <see cref="DbContext" />.
    /// </value>
    public ObjectContext ObjectContext
    {
      get
      {
        Contract.Ensures(Contract.Result<ObjectContext>() != null);

        var result = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this).ObjectContext;
        Contract.Assume(result != null);
        return result;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for races.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for races.
    /// </value>
    public IDbSet<RaceEntity> Races
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<RaceEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.races != null);
        return this.races;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.races = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for regions.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for regions.
    /// </value>
    public IDbSet<RegionEntity> Regions
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<RegionEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.regions != null);
        return this.regions;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.regions = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for region jumps.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for region jumps.
    /// </value>
    public IDbSet<RegionJumpEntity> RegionJumps
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<RegionJumpEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.regionJumps != null);
        return this.regionJumps;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.regionJumps = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for solar systems.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for solar Systems.
    /// </value>
    public IDbSet<SolarSystemEntity> SolarSystems
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<SolarSystemEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.solarSystems != null);
        return this.solarSystems;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.solarSystems = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for solar system jumps.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for solar system jumps.
    /// </value>
    public IDbSet<SolarSystemJumpEntity> SolarSystemJumps
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<SolarSystemJumpEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.solarSystemJumps != null);
        return this.solarSystemJumps;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.solarSystemJumps = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for station operations.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for station operations.
    /// </value>
    public IDbSet<StationOperationEntity> StationOperations
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<StationOperationEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.stationOperations != null);
        return this.stationOperations;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.stationOperations = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for station services.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for station services.
    /// </value>
    public IDbSet<StationServiceEntity> StationServices
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<StationServiceEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.stationServices != null);
        return this.stationServices;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.stationServices = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for station types.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for station types.
    /// </value>
    public IDbSet<StationTypeEntity> StationTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<StationTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.stationTypes != null);
        return this.stationTypes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.stationTypes = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for units.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for units.
    /// </value>
    public IDbSet<UnitEntity> Units
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<UnitEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.units != null);
        return this.units;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.units = value;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for universes.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for universes.
    /// </value>
    public IDbSet<UniverseEntity> Universes
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<UniverseEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(this.universes != null);
        return this.universes;
      }

      set
      {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        this.universes = value;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public override int SaveChanges()
    {
      return 0;
    }

    /// <summary>
    /// Returns an <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity for which to return a <see cref="IDbSet{TEntity}" />.
    /// </typeparam>
    /// <returns>
    /// An <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method simply returns the value of the base class's
    /// <see cref="DbContext.Set{TEntity}()" /> method as an
    /// <see cref="IDbSet{TEntity}" /> instead of a <see cref="DbSet{TEntity}" />
    /// object, for ease of testing and mocking.
    /// </para>
    /// </remarks>
    public new virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class
    {
      return base.Set<TEntity>();
    }

    /// <inheritdoc />
    [ContractVerification(false)]
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      /* AgentEntity Mappings *******************************************************/
      var agent = modelBuilder.Entity<AgentEntity>();

      // Map the ResearchFields collection
      agent.HasMany<EveTypeEntity>(x => x.ResearchFields)
           .WithMany()
           .Map(x => x.ToTable("agtResearchAgents")
                      .MapLeftKey("agentID")
                      .MapRightKey("typeID"));

      /* AgentTypeEntity Mappings ***************************************************/
      var agentType = modelBuilder.Entity<AgentTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      agentType.Map(x => x.MapInheritedProperties());
      agentType.HasKey(x => x.Id);
      agentType.Ignore(x => x.Description);
      agentType.Property(x => x.Id).HasColumnName("agentTypeID");
      agentType.Property(x => x.Name).HasColumnName("agentType");

      /* AttributeTypeEntity Mappings ***********************************************/
      var attributeType = modelBuilder.Entity<AttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeType.Map(x => x.MapInheritedProperties());
      attributeType.HasKey(x => x.Id);
      attributeType.Property(x => x.Description).HasColumnName("description");
      attributeType.Property(x => x.Id).HasColumnName("attributeID");
      attributeType.Property(x => x.Name).HasColumnName("attributeName");

      /* AttributeValueEntity Mappings **********************************************/

      // All mappings defined by Data Annotations

      /* AttributeCategoryEntity Mappings *******************************************/
      var attributeCategory = modelBuilder.Entity<AttributeCategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeCategory.Map(x => x.MapInheritedProperties());
      attributeCategory.HasKey(x => x.Id);
      attributeCategory.Property(x => x.Description).HasColumnName("categoryDescription");
      attributeCategory.Property(x => x.Id).HasColumnName("categoryID");
      attributeCategory.Property(x => x.Name).HasColumnName("categoryName");

      /* AttributeCategoryEntity Mappings *******************************************/
      var category = modelBuilder.Entity<CategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      category.Map(x => x.MapInheritedProperties());
      category.HasKey(x => x.Id);
      category.Property(x => x.Description).HasColumnName("description");
      category.Property(x => x.Id).HasColumnName("categoryID");
      category.Property(x => x.Name).HasColumnName("categoryName");

      /* CharacterAttributeTypeEntity Mappings **************************************/
      var characterAttributeType = modelBuilder.Entity<CharacterAttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      characterAttributeType.Map(x => x.MapInheritedProperties());
      characterAttributeType.HasKey(x => x.Id);
      characterAttributeType.Property(x => x.Description).HasColumnName("description");
      characterAttributeType.Property(x => x.Id).HasColumnName("attributeID");
      characterAttributeType.Property(x => x.Name).HasColumnName("attributeName");

      /* ConstellationEntity Mappings ***********************************************/
      var constellation = modelBuilder.Entity<ConstellationEntity>();

      // Map the Jumps collection
      constellation.HasMany(x => x.Jumps).WithRequired(x => x.FromConstellation).HasForeignKey(x => x.FromConstellationId);

      /* ConstellationJumpEntity Mappings *******************************************/

      // All mappings defined by Data Annotations

      /* CorporateActivityEntity Mappings *******************************************/
      var corporateActivity = modelBuilder.Entity<CorporateActivityEntity>();

      // Map properties inherited from BaseValueEntity<>
      corporateActivity.Map(x => x.MapInheritedProperties());
      corporateActivity.HasKey(x => x.Id);
      corporateActivity.Property(x => x.Description).HasColumnName("description");
      corporateActivity.Property(x => x.Id).HasColumnName("activityID");
      corporateActivity.Property(x => x.Name).HasColumnName("activityName");

      /* DivisionEntity Mappings ****************************************************/
      var division = modelBuilder.Entity<DivisionEntity>();

      // Map properties inherited from BaseValueEntity<>
      division.Map(x => x.MapInheritedProperties());
      division.HasKey(x => x.Id);
      division.Property(x => x.Description).HasColumnName("description");
      division.Property(x => x.Id).HasColumnName("divisionID");
      division.Property(x => x.Name).HasColumnName("divisionName");

      /* EffectEntity Mappings ******************************************************/

      // All mappings defined by Data Annotations

      /* EffectTypeEntity Mappings **************************************************/
      var effectType = modelBuilder.Entity<EffectTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      effectType.Map(x => x.MapInheritedProperties());
      effectType.HasKey(x => x.Id);
      effectType.Property(x => x.Description).HasColumnName("description");
      effectType.Property(x => x.Id).HasColumnName("effectID");
      effectType.Property(x => x.Name).HasColumnName("effectName");

      /* EveTypeEntity Mappings *****************************************************/
      var type = modelBuilder.Entity<EveTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      type.Map(x => x.MapInheritedProperties());
      type.HasKey(x => x.Id);
      type.Property(x => x.Description).HasColumnName("description");
      type.Property(x => x.Id).HasColumnName("typeID");
      type.Property(x => x.Name).HasColumnName("typeName");

      // Map the Attributes collection
      type.HasMany(x => x.Attributes).WithRequired().HasForeignKey(x => x.ItemTypeId);

      // Map the Effects collection
      type.HasMany(x => x.Effects).WithRequired().HasForeignKey(x => x.ItemTypeId);

      // Map the MetaType property
      type.HasOptional(x => x.MetaType).WithRequired(x => x.Type);

      // Map the VariantMetaTypes collection
      type.HasMany(x => x.ChildMetaTypes).WithRequired().HasForeignKey(x => x.ParentTypeId);

      /* FactionEntity Mappings *****************************************************/
      var faction = modelBuilder.Entity<FactionEntity>();

      // Map properties inherited from BaseValueEntity<>
      faction.Map(x => x.MapInheritedProperties());
      faction.HasKey(x => x.Id);
      faction.Property(x => x.Description).HasColumnName("description");
      faction.Property(x => x.Id).HasColumnName("factionID");
      faction.Property(x => x.Name).HasColumnName("factionName");

      faction.HasRequired(x => x.SolarSystem).WithMany().HasForeignKey(x => x.SolarSystemId);

      /* FlagEntity Mappings ********************************************************/
      var flag = modelBuilder.Entity<FlagEntity>();

      // Map properties inherited from BaseValueEntity<>
      flag.Map(x => x.MapInheritedProperties());
      flag.HasKey(x => x.Id);
      flag.Property(x => x.Description).HasColumnName("flagText");
      flag.Property(x => x.Id).HasColumnName("flagID");
      flag.Property(x => x.Name).HasColumnName("flagName");

      /* GroupEntity Mappings *******************************************************/
      var group = modelBuilder.Entity<GroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      group.Map(x => x.MapInheritedProperties());
      group.HasKey(x => x.Id);
      group.Property(x => x.Description).HasColumnName("description");
      group.Property(x => x.Id).HasColumnName("groupID");
      group.Property(x => x.Name).HasColumnName("groupName");

      // Map the Types collection
      group.HasMany(x => x.Types).WithRequired(x => x.Group).HasForeignKey(x => x.GroupId);

      /* IconEntity Mappings ********************************************************/
      var icon = modelBuilder.Entity<IconEntity>();

      // Map properties inherited from BaseValueEntity<>
      icon.Map(x => x.MapInheritedProperties());
      icon.HasKey(x => x.Id);
      icon.Property(x => x.Description).HasColumnName("description");
      icon.Property(x => x.Id).HasColumnName("iconID");
      icon.Property(x => x.Name).HasColumnName("iconFile");

      /* ItemEntity Mappings ********************************************************/
      var item = modelBuilder.Entity<ItemEntity>();

      item.HasRequired(x => x.Location).WithMany();
      item.HasRequired(x => x.Owner).WithMany();

      /* MarketGroupEntity Mappings *************************************************/
      var marketGroup = modelBuilder.Entity<MarketGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      marketGroup.Map(x => x.MapInheritedProperties());
      marketGroup.HasKey(x => x.Id);
      marketGroup.Property(x => x.Description).HasColumnName("description");
      marketGroup.Property(x => x.Id).HasColumnName("marketGroupID");
      marketGroup.Property(x => x.Name).HasColumnName("marketGroupName");

      // Map the ChildGroups collection
      marketGroup.HasMany(x => x.ChildGroups).WithOptional(x => x.ParentGroup).HasForeignKey(x => x.ParentGroupId);

      // Map the Types collection
      marketGroup.HasMany(x => x.Types).WithRequired(x => x.MarketGroup).HasForeignKey(x => x.MarketGroupId);

      /* MetaGroupEntity Mappings ***************************************************/
      var metaGroup = modelBuilder.Entity<MetaGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      metaGroup.Map(x => x.MapInheritedProperties());
      metaGroup.HasKey(x => x.Id);
      metaGroup.Property(x => x.Description).HasColumnName("description");
      metaGroup.Property(x => x.Id).HasColumnName("metaGroupID");
      metaGroup.Property(x => x.Name).HasColumnName("metaGroupName");

      /* MetaTypeEntity Mappings ****************************************************/

      // All mappings defined by Data Annotations

      /* NpcCorporationEntity Mappings **********************************************/
      var corporation = modelBuilder.Entity<NpcCorporationEntity>();

      // Map the Agents collection
      corporation.HasMany(x => x.Agents)
                 .WithRequired(x => x.Corporation)
                 .HasForeignKey(x => x.CorporationId);

      // Map the TradeGoods collection
      corporation.HasMany<EveTypeEntity>(x => x.TradeGoods)
                 .WithMany().Map(x => x.ToTable("crpNPCCorporationTrades")
                            .MapLeftKey("corporationID")
                            .MapRightKey("typeID"));

      // Map the ResearchFields collection
      corporation.HasMany<EveTypeEntity>(x => x.ResearchFields)
                 .WithMany().Map(x => x.ToTable("crpNPCCorporationResearchFields")
                 .MapLeftKey("corporationID")
                 .MapRightKey("skillID"));

      /* NpcCorporationDivisionEntity Mappings **************************************/
      var corporationDivision = modelBuilder.Entity<NpcCorporationDivisionEntity>();

      // Map the Agents collection
      corporationDivision.HasMany(x => x.Agents)
                         .WithRequired()
                         .HasForeignKey(x => new { x.CorporationId, x.DivisionId });

      /* RaceEntity Mappings ********************************************************/
      var race = modelBuilder.Entity<RaceEntity>();

      // Map properties inherited from BaseValueEntity<>
      race.Map(x => x.MapInheritedProperties());
      race.HasKey(x => x.Id);
      race.Property(x => x.Description).HasColumnName("description");
      race.Property(x => x.Id).HasColumnName("raceID");
      race.Property(x => x.Name).HasColumnName("raceName");

      /* RegionEntity Mappings ******************************************************/
      var region = modelBuilder.Entity<RegionEntity>();

      // Map the Jumps collection
      region.HasMany(x => x.Jumps).WithRequired(x => x.FromRegion).HasForeignKey(x => x.FromRegionId);

      /* RegionJumpEntity Mappings **************************************************/

      // All mappings defined by Data Annotations

      /* SolarSystemEntity Mappings *************************************************/
      var solarSystem = modelBuilder.Entity<SolarSystemEntity>();

      solarSystem.HasRequired(x => x.Constellation).WithMany().HasForeignKey(x => x.ConstellationId);
      solarSystem.HasOptional(x => x.Faction).WithMany().HasForeignKey(x => x.FactionId);

      // Map the Jumps collection
      solarSystem.HasMany(x => x.Jumps).WithRequired(x => x.FromSolarSystem).HasForeignKey(x => x.FromSolarSystemId);

      /* SolarSystemJumpEntity Mappings *********************************************/

      // All mappings defined by Data Annotations

      /* StationOperationEntity Mappings ********************************************/
      var stationOperation = modelBuilder.Entity<StationOperationEntity>();

      // Map properties inherited from BaseValueEntity<>
      stationOperation.Map(x => x.MapInheritedProperties());
      stationOperation.HasKey(x => x.Id);
      stationOperation.Property(x => x.Description).HasColumnName("description");
      stationOperation.Property(x => x.Id).HasColumnName("operationID");
      stationOperation.Property(x => x.Name).HasColumnName("operationName");

      // Map the Services collection
      stationOperation.HasMany(x => x.Services).WithMany().Map(x => x.ToTable("staOperationServices")
                                                                     .MapLeftKey("operationID")
                                                                     .MapRightKey("serviceID"));

      /* StationServiceEntity Mappings **********************************************/
      var stationService = modelBuilder.Entity<StationServiceEntity>();

      // Map properties inherited from BaseValueEntity<>
      stationService.Map(x => x.MapInheritedProperties());
      stationService.HasKey(x => x.Id);
      stationService.Property(x => x.Description).HasColumnName("description");
      stationService.Property(x => x.Id).HasColumnName("serviceID");
      stationService.Property(x => x.Name).HasColumnName("serviceName");

      /* StationTypeEntity Mappings *************************************************/

      // All mappings defined by Data Annotations

      /* UnitEntity Mappings ********************************************************/
      var unit = modelBuilder.Entity<UnitEntity>();

      // Map properties inherited from BaseValueEntity<>
      unit.Map(x => x.MapInheritedProperties());
      unit.HasKey(x => x.Id);
      unit.Property(x => x.Description).HasColumnName("description");
      unit.Property(x => x.Id).HasColumnName("unitID");
      unit.Property(x => x.Name).HasColumnName("unitName");

      /* UniverseEntity Mappings ****************************************************/
      var universe = modelBuilder.Entity<UniverseEntity>();
    }
  }
}