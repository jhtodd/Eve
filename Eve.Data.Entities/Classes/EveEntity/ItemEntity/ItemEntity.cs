//-----------------------------------------------------------------------
// <copyright file="ItemEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base class for data entities for EVE items.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public partial class ItemEntity : EveEntity<Item>
  {
    /// <summary>
    /// Defines the range of ID values belonging to Agent items.
    /// </summary>
    internal const long AgentMaximumId = 3999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Agent items.
    /// </summary>
    internal const long AgentMinimumId = 3000000L;

    /// <summary>
    /// Defines the range of ID values belonging to Celestial items.
    /// </summary>
    internal const long CelestialMaximumId = 49999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Celestial items.
    /// </summary>
    internal const long CelestialMinimumId = 40000000L;

    /// <summary>
    /// Defines the range of ID values belonging to Constellation items.
    /// </summary>
    internal const long ConstellationMaximumId = 29999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Constellation items.
    /// </summary>
    internal const long ConstellationMinimumId = 20000000L;

    /// <summary>
    /// Defines the range of ID values belonging to Corporation items.
    /// </summary>
    internal const long CorporationMaximumId = 1999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Corporation items.
    /// </summary>
    internal const long CorporationMinimumId = 1000000L;

    /// <summary>
    /// Defines the range of ID values belonging to Faction items.
    /// </summary>
    internal const long FactionMaximumId = 599999L;

    /// <summary>
    /// Defines the range of ID values belonging to Faction items.
    /// </summary>
    internal const long FactionMinimumId = 500000L;

    /// <summary>
    /// Defines the range of ID values belonging to Region items.
    /// </summary>
    internal const long RegionMaximumId = 19999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Region items.
    /// </summary>
    internal const long RegionMinimumId = 10000000L;

    /// <summary>
    /// Defines the range of ID values belonging to SolarSystem items.
    /// </summary>
    internal const long SolarSystemMaximumId = 39999999L;

    /// <summary>
    /// Defines the range of ID values belonging to SolarSystem items.
    /// </summary>
    internal const long SolarSystemMinimumId = 30000000L;

    /// <summary>
    /// Defines the range of ID values belonging to Stargate items.
    /// </summary>
    internal const long StargateMaximumId = 59999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Stargate items.
    /// </summary>
    internal const long StargateMinimumId = 50000000L;

    /// <summary>
    /// Defines the range of ID values belonging to Station items.
    /// </summary>
    internal const long StationMaximumId = 69999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Station items.
    /// </summary>
    internal const long StationMinimumId = 60000000L;

    /// <summary>
    /// Defines the range of ID values belonging to Universe items.
    /// </summary>
    internal const long UniverseMaximumId = 9999999L;

    /// <summary>
    /// Defines the range of ID values belonging to Universe items.
    /// </summary>
    internal const long UniverseMinimumId = 9000000L;
    
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ItemEntity class.
    /// </summary>
    public ItemEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets additional information if the current item is an agent.
    /// </summary>
    /// <value>
    /// An <see cref="AgentEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// an agent.
    /// </value>
    public virtual AgentEntity AgentInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a celestial object.
    /// </summary>
    /// <value>
    /// A <see cref="CelestialEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a celestial object.
    /// </value>
    public virtual CelestialEntity CelestialInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a constellation.
    /// </summary>
    /// <value>
    /// A <see cref="ConstellationEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a constellation.
    /// </value>
    public virtual ConstellationEntity ConstellationInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a corporation.
    /// </summary>
    /// <value>
    /// A <see cref="NpcCorporationEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a corporation.
    /// </value>
    public virtual NpcCorporationEntity CorporationInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a faction.
    /// </summary>
    /// <value>
    /// A <see cref="FactionEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a faction.
    /// </value>
    public virtual FactionEntity FactionInfo { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual FlagEntity Flag { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public FlagId FlagId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public long Id { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether the item is an agent.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is an agent; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="AgentInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsAgent
    {
      get 
      {
        return this.Id >= AgentMinimumId && this.Id <= AgentMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a celestial object.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a celestial object; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="CelestialInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsCelestial
    {
      get
      {
        return this.Id >= CelestialMinimumId && this.Id <= CelestialMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a constellation.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a constellation; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="ConstellationInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsConstellation
    {
      get
      {
        return this.Id >= ConstellationMinimumId && this.Id <= ConstellationMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a corporation.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a corporation; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="CorporationInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsCorporation
    {
      get
      {
        return this.Id >= CorporationMinimumId && this.Id <= CorporationMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a faction.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a faction; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="FactionInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsFaction
    {
      get
      {
        return this.Id >= FactionMinimumId && this.Id <= FactionMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a region.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a region; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="RegionInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsRegion
    {
      get
      {
        return this.Id >= RegionMinimumId && this.Id <= RegionMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a solar system.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a solar system; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="SolarSystemInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsSolarSystem
    {
      get
      {
        return this.Id >= SolarSystemMinimumId && this.Id <= SolarSystemMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a stargate.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a stargate; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="StargateInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsStargate
    {
      get
      {
        return this.Id >= StargateMinimumId && this.Id <= StargateMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a station.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a station; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="StationInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsStation
    {
      get
      {
        return this.Id >= StationMinimumId && this.Id <= StationMaximumId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the item is a universe.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is a universe; otherwise
    /// <see langword="false" />.
    /// </value>
    /// <remarks>
    /// <para>
    /// If the value of this property is <see langword="true" />,
    /// the <see cref="UniverseInfo" /> property will contain additional
    /// information about the item.
    /// </para>
    /// </remarks>
    public bool IsUniverse
    {
      get
      {
        return this.Id == 9 || (this.Id >= UniverseMinimumId && this.Id <= UniverseMaximumId);
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual EveTypeEntity ItemType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ItemTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ItemEntity Location { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public long LocationId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ItemNameEntity Name { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ItemEntity Owner { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public long OwnerId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ItemPositionEntity Position { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int Quantity { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a region.
    /// </summary>
    /// <value>
    /// A <see cref="RegionEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a region.
    /// </value>
    public virtual RegionEntity RegionInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a solar system.
    /// </summary>
    /// <value>
    /// A <see cref="SolarSystemEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a solar system.
    /// </value>
    public virtual SolarSystemEntity SolarSystemInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a stargate.
    /// </summary>
    /// <value>
    /// A <see cref="StargateEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a stargate.
    /// </value>
    public virtual StargateEntity StargateInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a station.
    /// </summary>
    /// <value>
    /// A <see cref="StationEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a station.
    /// </value>
    public virtual StationEntity StationInfo { get; internal set; }

    /// <summary>
    /// Gets additional information if the current item is a universe.
    /// </summary>
    /// <value>
    /// A <see cref="UniverseEntity" /> containing extra information about the
    /// item, or <see langword="null" /> if the current item does not describe
    /// a universe.
    /// </value>
    public virtual UniverseEntity UniverseInfo { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return this.Id; }
    }

    /* Methods */

    /// <inheritdoc />
    public override Item ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return Item.Create(repository, this);
    }
  }
}