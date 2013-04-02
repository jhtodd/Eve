//-----------------------------------------------------------------------
// <copyright file="EveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data {
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

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  using Eve;
  using Eve.Data.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// A <see cref="DbContext" /> that provides data access to the EVE database.
  /// This access is read-only.
  /// </summary>
  public class EveDbContext : DbContext {

    #region Static Fields
    private static readonly EveDbContext _default = new EveDbContext();
    #endregion

    #region Static Constructor
    //******************************************************************************
    /// <summary>
    /// Initializes static members of the EveDbContext class.
    /// </summary>
    static EveDbContext() {
      Database.SetInitializer<EveDbContext>(null);
    }
    #endregion
    #region Static Properties
    //******************************************************************************
    /// <summary>
    /// Gets the default data context.  This should be used in most circumstances.
    /// </summary>
    /// 
    /// <value>
    /// The default <see cref="EveDbContext" />.
    /// </value>
    public static EveDbContext Default {
      get {
        Contract.Ensures(Contract.Result<EveDbContext>() != null);
        return _default;
      }
    }
    #endregion

    #region Instance Fields
    private IDbSet<AgentEntity> _agents;
    private IDbSet<AgentTypeEntity> _agentTypes;
    private IDbSet<AttributeCategoryEntity> _attributeCategories;
    private IDbSet<AttributeTypeEntity> _attributeTypes;
    private IDbSet<AttributeValueEntity> _attributeValues;
    private IDbSet<CategoryEntity> _categories;
    private IDbSet<CharacterAttributeTypeEntity> _characterAttributeTypes;
    private IDbSet<ConstellationEntity> _constellations;
    private IDbSet<ConstellationJumpEntity> _constellationJumps;
    private IDbSet<CorporateActivityEntity> _corporateActivities;
    private IDbSet<DivisionEntity> _divisions;
    private IDbSet<EffectTypeEntity> _effectTypes;
    private IDbSet<EffectEntity> _effects;
    private IDbSet<EveTypeEntity> _eveTypes;
    private IDbSet<FlagEntity> _flags;
    private IDbSet<GroupEntity> _groups;
    private IDbSet<IconEntity> _icons;
    private IDbSet<ItemEntity> _items;
    private IDbSet<MarketGroupEntity> _marketGroups;
    private IDbSet<MetaGroupEntity> _metaGroups;
    private IDbSet<MetaTypeEntity> _metaTypes;
    private IDbSet<NpcCorporationEntity> _npcCorporations;
    private IDbSet<NpcCorporationDivisionEntity> _npcCorporationDivisions;
    private IDbSet<RaceEntity> _races;
    private IDbSet<RegionEntity> _regions;
    private IDbSet<RegionJumpEntity> _regionJumps;
    private IDbSet<SolarSystemEntity> _solarSystems;
    private IDbSet<SolarSystemJumpEntity> _solarSystemJumps;
    private IDbSet<StationOperationEntity> _stationOperations;
    private IDbSet<StationServiceEntity> _stationServices;
    private IDbSet<StationTypeEntity> _stationTypes;
    private IDbSet<UnitEntity> _units;
    private IDbSet<UniverseEntity> _universes;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDbContext class using conventions to
    /// create the name of the database to which a connection will be made. By
    /// convention the name is the full name (namespace + class name) of the
    /// derived context class.  For more information on how this is used to create
    /// a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    public EveDbContext() : base() {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDbContext class using the given string
    /// as the name or connection string for the database to which a connection
    /// will be made.  For more information on how this is used to create a
    /// connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    /// 
    /// <param name="nameOrConnectionString">
    /// Either the database name or a connection string.
    /// </param>
    public EveDbContext(string nameOrConnectionString) : base(nameOrConnectionString) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDbContext class using the existing
    /// connection to connect to a database. The connection will not be disposed
    /// when the context is disposed.
    /// </summary>
    /// 
    /// <param name="existingConnection">
    /// An existing connection to use for the new context.
    /// </param>
    /// 
    /// <param name="contextOwnsConnection">
    /// If set to true the connection is disposed when the context is disposed,
    /// otherwise the caller must dispose the connection.
    /// </param>
    public EveDbContext(DbConnection existingConnection, bool contextOwnsConnection)
      : base(existingConnection, contextOwnsConnection) {
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for agents.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for agents.
    /// </value>
    public IDbSet<AgentEntity> Agents {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AgentEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_agents != null);
        return _agents;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _agents = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for agent types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for agent types.
    /// </value>
    public IDbSet<AgentTypeEntity> AgentTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AgentTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_agentTypes != null);
        return _agentTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _agentTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute categories.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute categories.
    /// </value>
    public IDbSet<AttributeCategoryEntity> AttributeCategories {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AttributeCategoryEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_attributeCategories != null);
        return _attributeCategories;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _attributeCategories = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute types.
    /// </value>
    public IDbSet<AttributeTypeEntity> AttributeTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AttributeTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_attributeTypes != null);
        return _attributeTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _attributeTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute values.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute values.
    /// </value>
    public IDbSet<AttributeValueEntity> AttributeValues {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AttributeValueEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_attributeValues != null);
        return _attributeValues;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _attributeValues = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for categories.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for categories.
    /// </value>
    public IDbSet<CategoryEntity> Categories {
      get {
        Contract.Ensures(Contract.Result<IDbSet<CategoryEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_categories != null);
        return _categories;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _categories = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for character attribute types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for character attribute types.
    /// </value>
    public IDbSet<CharacterAttributeTypeEntity> CharacterAttributeTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<CharacterAttributeTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_characterAttributeTypes != null);
        return _characterAttributeTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _characterAttributeTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for constellations.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for constellations
    /// </value>
    public IDbSet<ConstellationEntity> Constellations {
      get {
        Contract.Ensures(Contract.Result<IDbSet<ConstellationEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_constellations != null);
        return _constellations;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _constellations = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for constellation jumps.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for constellation jumps.
    /// </value>
    public IDbSet<ConstellationJumpEntity> ConstellationJumps {
      get {
        Contract.Ensures(Contract.Result<IDbSet<ConstellationJumpEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_constellationJumps != null);
        return _constellationJumps;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _constellationJumps = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for corporate activities.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for corporate activities.
    /// </value>
    public IDbSet<CorporateActivityEntity> CorporateActivities {
      get {
        Contract.Ensures(Contract.Result<IDbSet<CorporateActivityEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_corporateActivities != null);
        return _corporateActivities;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _corporateActivities = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for divisions.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for divisions
    /// </value>
    public IDbSet<DivisionEntity> Divisions {
      get {
        Contract.Ensures(Contract.Result<IDbSet<DivisionEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_divisions != null);
        return _divisions;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _divisions = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for effect types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for effect types.
    /// </value>
    public IDbSet<EffectTypeEntity> EffectTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<EffectTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_effectTypes != null);
        return _effectTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _effectTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for effect values.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for effect values.
    /// </value>
    public IDbSet<EffectEntity> Effects {
      get {
        Contract.Ensures(Contract.Result<IDbSet<EffectEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_effects != null);
        return _effects;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _effects = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for item types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for item types.
    /// </value>
    public IDbSet<EveTypeEntity> EveTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<EveTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_eveTypes != null);
        return _eveTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _eveTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for flag types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for flag types.
    /// </value>
    public IDbSet<FlagEntity> Flags {
      get {
        Contract.Ensures(Contract.Result<IDbSet<FlagEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_flags != null);
        return _flags;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _flags = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for groups.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for groups.
    /// </value>
    public IDbSet<GroupEntity> Groups {
      get {
        Contract.Ensures(Contract.Result<IDbSet<GroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_groups != null);
        return _groups;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _groups = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for icons.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for icons.
    /// </value>
    public IDbSet<IconEntity> Icons {
      get {
        Contract.Ensures(Contract.Result<IDbSet<IconEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_icons != null);
        return _icons;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _icons = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for general items.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for general items
    /// </value>
    public IDbSet<ItemEntity> Items {
      get {
        Contract.Ensures(Contract.Result<IDbSet<ItemEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_items != null);
        return _items;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _items = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for market groups.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for market groups.
    /// </value>
    public IDbSet<MarketGroupEntity> MarketGroups {
      get {
        Contract.Ensures(Contract.Result<IDbSet<MarketGroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_marketGroups != null);
        return _marketGroups;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _marketGroups = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for meta groups.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for meta groups.
    /// </value>
    public IDbSet<MetaGroupEntity> MetaGroups {
      get {
        Contract.Ensures(Contract.Result<IDbSet<MetaGroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_metaGroups != null);
        return _metaGroups;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _metaGroups = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for meta types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for meta types.
    /// </value>
    public IDbSet<MetaTypeEntity> MetaTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<MetaTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_metaTypes != null);
        return _metaTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _metaTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for NPC corporations.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for NPC corporations
    /// </value>
    public IDbSet<NpcCorporationEntity> NpcCorporations {
      get {
        Contract.Ensures(Contract.Result<IDbSet<NpcCorporationEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_npcCorporations != null);
        return _npcCorporations;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _npcCorporations = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for divisions of NPC corporations.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for divisions of NPC corporations
    /// </value>
    public IDbSet<NpcCorporationDivisionEntity> NpcCorporationDivisions {
      get {
        Contract.Ensures(Contract.Result<IDbSet<NpcCorporationDivisionEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_npcCorporationDivisions != null);
        return _npcCorporationDivisions;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _npcCorporationDivisions = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="ObjectContext" /> underlying the current
    /// <see cref="DbContext" />.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ObjectContext" /> underlying the current
    /// <see cref="DbContext" />.
    /// </value>
    public ObjectContext ObjectContext {
      get {
        Contract.Ensures(Contract.Result<ObjectContext>() != null);

        var result = ((System.Data.Entity.Infrastructure.IObjectContextAdapter) this).ObjectContext;
        Contract.Assume(result != null);
        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for races.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for races.
    /// </value>
    public IDbSet<RaceEntity> Races {
      get {
        Contract.Ensures(Contract.Result<IDbSet<RaceEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_races != null);
        return _races;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _races = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for regions.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for regions.
    /// </value>
    public IDbSet<RegionEntity> Regions {
      get {
        Contract.Ensures(Contract.Result<IDbSet<RegionEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_regions != null);
        return _regions;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _regions = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for region jumps.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for region jumps.
    /// </value>
    public IDbSet<RegionJumpEntity> RegionJumps {
      get {
        Contract.Ensures(Contract.Result<IDbSet<RegionJumpEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_regionJumps != null);
        return _regionJumps;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _regionJumps = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for solar systems.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for solar Systems.
    /// </value>
    public IDbSet<SolarSystemEntity> SolarSystems {
      get {
        Contract.Ensures(Contract.Result<IDbSet<SolarSystemEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_solarSystems != null);
        return _solarSystems;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _solarSystems = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for solar system jumps.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for solar system jumps.
    /// </value>
    public IDbSet<SolarSystemJumpEntity> SolarSystemJumps {
      get {
        Contract.Ensures(Contract.Result<IDbSet<SolarSystemJumpEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_solarSystemJumps != null);
        return _solarSystemJumps;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _solarSystemJumps = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for station operations.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for station operations.
    /// </value>
    public IDbSet<StationOperationEntity> StationOperations {
      get {
        Contract.Ensures(Contract.Result<IDbSet<StationOperationEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_stationOperations != null);
        return _stationOperations;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _stationOperations = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for station services.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for station services.
    /// </value>
    public IDbSet<StationServiceEntity> StationServices {
      get {
        Contract.Ensures(Contract.Result<IDbSet<StationServiceEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_stationServices != null);
        return _stationServices;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _stationServices = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for station types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for station types.
    /// </value>
    public IDbSet<StationTypeEntity> StationTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<StationTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_stationTypes != null);
        return _stationTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _stationTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for units.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for units.
    /// </value>
    public IDbSet<UnitEntity> Units {
      get {
        Contract.Ensures(Contract.Result<IDbSet<UnitEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_units != null);
        return _units;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _units = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for universes.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for universes.
    /// </value>
    public IDbSet<UniverseEntity> Universes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<UniverseEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_universes != null);
        return _universes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _universes = value;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public override int SaveChanges() {
      return 0;
    }
    //******************************************************************************
    /// <summary>
    /// Returns an <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </summary>
    /// 
    /// <typeparam name="TEntity">
    /// The type of entity for which to return a <see cref="IDbSet{TEntity}" />.
    /// </typeparam>
    /// 
    /// <returns>
    /// An <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </returns>
    /// 
    /// <remarks>
    /// <para>
    /// This method simply returns the value of the base class's
    /// <see cref="DbContext.Set{TEntity}()" /> method as an
    /// <see cref="IDbSet{TEntity}" /> instead of a <see cref="DbSet{TEntity}" />
    /// object, for ease of testing and mocking.
    /// </para>
    /// </remarks>
    public new virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class {
      return base.Set<TEntity>();
    }
    #endregion
    #region Protected Methods
    //******************************************************************************
    /// <inheritdoc />
    [ContractVerification(false)]
    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

      #region AgentEntity Mappings
      var agent = modelBuilder.Entity<AgentEntity>();

      // Map the ResearchFields collection
      agent.HasMany<EveTypeEntity>(x => x.ResearchFields)
           .WithMany()
           .Map(x => x.ToTable("agtResearchAgents")
                      .MapLeftKey("agentID")
                      .MapRightKey("typeID"));
      #endregion

      #region AgentTypeEntity Mappings
      var agentType = modelBuilder.Entity<AgentTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      agentType.Map(x => x.MapInheritedProperties());
      agentType.HasKey(x => x.Id);
      agentType.Ignore(x => x.Description);
      agentType.Property(x => x.Id).HasColumnName("agentTypeID");
      agentType.Property(x => x.Name).HasColumnName("agentType");
      #endregion

      #region AttributeTypeEntity Mappings
      var attributeType = modelBuilder.Entity<AttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeType.Map(x => x.MapInheritedProperties());
      attributeType.HasKey(x => x.Id);
      attributeType.Property(x => x.Description).HasColumnName("description");
      attributeType.Property(x => x.Id).HasColumnName("attributeID");
      attributeType.Property(x => x.Name).HasColumnName("attributeName");
      #endregion

      #region AttributeValueEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region AttributeCategoryEntity Mappings
      var attributeCategory = modelBuilder.Entity<AttributeCategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeCategory.Map(x => x.MapInheritedProperties());
      attributeCategory.HasKey(x => x.Id);
      attributeCategory.Property(x => x.Description).HasColumnName("categoryDescription");
      attributeCategory.Property(x => x.Id).HasColumnName("categoryID");
      attributeCategory.Property(x => x.Name).HasColumnName("categoryName");
      #endregion

      #region CategoryEntity Mappings
      var category = modelBuilder.Entity<CategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      category.Map(x => x.MapInheritedProperties());
      category.HasKey(x => x.Id);
      category.Property(x => x.Description).HasColumnName("description");
      category.Property(x => x.Id).HasColumnName("categoryID");
      category.Property(x => x.Name).HasColumnName("categoryName");
      #endregion

      #region CharacterAttributeTypeEntity Mappings
      var characterAttributeType = modelBuilder.Entity<CharacterAttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      characterAttributeType.Map(x => x.MapInheritedProperties());
      characterAttributeType.HasKey(x => x.Id);
      characterAttributeType.Property(x => x.Description).HasColumnName("description");
      characterAttributeType.Property(x => x.Id).HasColumnName("attributeID");
      characterAttributeType.Property(x => x.Name).HasColumnName("attributeName");
      #endregion

      #region ConstellationEntity Mappings
      var constellation = modelBuilder.Entity<ConstellationEntity>();

      // Map the Jumps collection
      constellation.HasMany(x => x.Jumps).WithRequired(x => x.FromConstellation).HasForeignKey(x => x.FromConstellationId);
      #endregion

      #region ConstellationJumpEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region CorporateActivityEntity Mappings
      var corporateActivity = modelBuilder.Entity<CorporateActivityEntity>();

      // Map properties inherited from BaseValueEntity<>
      corporateActivity.Map(x => x.MapInheritedProperties());
      corporateActivity.HasKey(x => x.Id);
      corporateActivity.Property(x => x.Description).HasColumnName("description");
      corporateActivity.Property(x => x.Id).HasColumnName("activityID");
      corporateActivity.Property(x => x.Name).HasColumnName("activityName");
      #endregion

      #region DivisionEntity Mappings
      var division = modelBuilder.Entity<DivisionEntity>();

      // Map properties inherited from BaseValueEntity<>
      division.Map(x => x.MapInheritedProperties());
      division.HasKey(x => x.Id);
      division.Property(x => x.Description).HasColumnName("description");
      division.Property(x => x.Id).HasColumnName("divisionID");
      division.Property(x => x.Name).HasColumnName("divisionName");
      #endregion

      #region EffectEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region EffectTypeEntity Mappings
      var effectType = modelBuilder.Entity<EffectTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      effectType.Map(x => x.MapInheritedProperties());
      effectType.HasKey(x => x.Id);
      effectType.Property(x => x.Description).HasColumnName("description");
      effectType.Property(x => x.Id).HasColumnName("effectID");
      effectType.Property(x => x.Name).HasColumnName("effectName");
      #endregion

      #region EveTypeEntity Mappings
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
      #endregion

      #region FactionEntity Mappings
      var faction = modelBuilder.Entity<FactionEntity>();

      // Map properties inherited from BaseValueEntity<>
      faction.Map(x => x.MapInheritedProperties());
      faction.HasKey(x => x.Id);
      faction.Property(x => x.Description).HasColumnName("description");
      faction.Property(x => x.Id).HasColumnName("factionID");
      faction.Property(x => x.Name).HasColumnName("factionName");

      faction.HasRequired(x => x.SolarSystem).WithMany().HasForeignKey(x => x.SolarSystemId);
      #endregion

      #region FlagEntity Mappings
      var flag = modelBuilder.Entity<FlagEntity>();

      // Map properties inherited from BaseValueEntity<>
      flag.Map(x => x.MapInheritedProperties());
      flag.HasKey(x => x.Id);
      flag.Property(x => x.Description).HasColumnName("flagText");
      flag.Property(x => x.Id).HasColumnName("flagID");
      flag.Property(x => x.Name).HasColumnName("flagName");
      #endregion

      #region GroupEntity Mappings
      var group = modelBuilder.Entity<GroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      group.Map(x => x.MapInheritedProperties());
      group.HasKey(x => x.Id);
      group.Property(x => x.Description).HasColumnName("description");
      group.Property(x => x.Id).HasColumnName("groupID");
      group.Property(x => x.Name).HasColumnName("groupName");

      // Map the Types collection
      group.HasMany(x => x.Types).WithRequired(x => x.Group).HasForeignKey(x => x.GroupId);
      #endregion

      #region IconEntity Mappings
      var icon = modelBuilder.Entity<IconEntity>();

      // Map properties inherited from BaseValueEntity<>
      icon.Map(x => x.MapInheritedProperties());
      icon.HasKey(x => x.Id);
      icon.Property(x => x.Description).HasColumnName("description");
      icon.Property(x => x.Id).HasColumnName("iconID");
      icon.Property(x => x.Name).HasColumnName("iconFile");
      #endregion

      #region ItemEntity Mappings
      var item = modelBuilder.Entity<ItemEntity>();

      item.HasRequired(x => x.Location).WithMany();
      item.HasRequired(x => x.Owner).WithMany();
      #endregion

      #region MarketGroupEntity Mappings
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
      #endregion

      #region MetaGroupEntity Mappings
      var metaGroup = modelBuilder.Entity<MetaGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      metaGroup.Map(x => x.MapInheritedProperties());
      metaGroup.HasKey(x => x.Id);
      metaGroup.Property(x => x.Description).HasColumnName("description");
      metaGroup.Property(x => x.Id).HasColumnName("metaGroupID");
      metaGroup.Property(x => x.Name).HasColumnName("metaGroupName");
      #endregion

      #region MetaTypeEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region NpcCorporationEntity Mappings
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
      #endregion

      #region NpcCorporationDivisionEntity Mappings
      var corporationDivision = modelBuilder.Entity<NpcCorporationDivisionEntity>();

      // Map the Agents collection
      corporationDivision.HasMany(x => x.Agents)
                         .WithRequired()
                         .HasForeignKey(x => new { x.CorporationId, x.DivisionId });
      #endregion

      #region RaceEntity Mappings
      var race = modelBuilder.Entity<RaceEntity>();

      // Map properties inherited from BaseValueEntity<>
      race.Map(x => x.MapInheritedProperties());
      race.HasKey(x => x.Id);
      race.Property(x => x.Description).HasColumnName("description");
      race.Property(x => x.Id).HasColumnName("raceID");
      race.Property(x => x.Name).HasColumnName("raceName");
      #endregion

      #region RegionEntity Mappings
      var region = modelBuilder.Entity<RegionEntity>();

      // Map the Jumps collection
      region.HasMany(x => x.Jumps).WithRequired(x => x.FromRegion).HasForeignKey(x => x.FromRegionId);
      #endregion

      #region RegionJumpEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region SolarSystemEntity Mappings
      var solarSystem = modelBuilder.Entity<SolarSystemEntity>();

      solarSystem.HasRequired(x => x.Constellation).WithMany().HasForeignKey(x => x.ConstellationId);
      solarSystem.HasOptional(x => x.Faction).WithMany().HasForeignKey(x => x.FactionId);
      
      // Map the Jumps collection
      solarSystem.HasMany(x => x.Jumps).WithRequired(x => x.FromSolarSystem).HasForeignKey(x => x.FromSolarSystemId);
      #endregion

      #region SolarSystemJumpEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region StationOperationEntity Mappings
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
      #endregion

      #region StationServiceEntity Mappings
      var stationService = modelBuilder.Entity<StationServiceEntity>();

      // Map properties inherited from BaseValueEntity<>
      stationService.Map(x => x.MapInheritedProperties());
      stationService.HasKey(x => x.Id);
      stationService.Property(x => x.Description).HasColumnName("description");
      stationService.Property(x => x.Id).HasColumnName("serviceID");
      stationService.Property(x => x.Name).HasColumnName("serviceName");
      #endregion

      #region StationTypeEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region UnitEntity Mappings
      var unit = modelBuilder.Entity<UnitEntity>();

      // Map properties inherited from BaseValueEntity<>
      unit.Map(x => x.MapInheritedProperties());
      unit.HasKey(x => x.Id);
      unit.Property(x => x.Description).HasColumnName("description");
      unit.Property(x => x.Id).HasColumnName("unitID");
      unit.Property(x => x.Name).HasColumnName("unitName");
      #endregion

      #region UniverseEntity Mappings
      var universe = modelBuilder.Entity<UniverseEntity>();
      #endregion
    }
    #endregion
  }
}