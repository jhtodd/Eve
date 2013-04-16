//-----------------------------------------------------------------------
// <copyright file="AssemblyLineStation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains a summary of the number of assembly lines of a particular
  /// type that are located at a particular station.
  /// </summary>
  public sealed partial class AssemblyLineStation 
    : EveEntityAdapter<AssemblyLineStationEntity>,
      IComparable<AssemblyLineStation>,
      IEquatable<AssemblyLineStation>,
      IEveCacheable,
      IKeyItem<long>
  {
    private AssemblyLineType assemblyLineType;
    private NpcCorporation owner;
    private Region region;
    private SolarSystem solarSystem;
    private Station station;
    private StationType stationType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineStation class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AssemblyLineStation(IEveRepository container, AssemblyLineStationEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the type of assembly line which is located at the current station.
    /// </summary>
    /// <value>
    /// An <see cref="AssemblyLineType" /> describing the type of assembly
    /// line.
    /// </value>    
    public AssemblyLineType AssemblyLineType
    {
      get
      {
        Contract.Ensures(Contract.Result<AssemblyLineType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.assemblyLineType ?? (this.assemblyLineType = this.Container.GetOrAdd<AssemblyLineType>(this.AssemblyLineTypeId, () => this.Entity.AssemblyLineType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of assembly line which is located at the current
    /// station.
    /// </summary>
    /// <value>
    /// The ID of the type of assembly line which is located at the current
    /// station.
    /// </value>   
    public AssemblyLineTypeId AssemblyLineTypeId
    {
      get { return this.Entity.AssemblyLineTypeId; }
    }

    /// <summary>
    /// Gets the corporation that owns the current station.
    /// </summary>
    /// <value>
    /// The <see cref="NpcCorporation" /> that owns the current station.
    /// </value>    
    public NpcCorporation Owner
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.owner ?? (this.owner = this.Container.GetOrAdd<NpcCorporation>(this.OwnerId, () => this.Entity.Owner.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the corporation that owns the current station.
    /// </summary>
    /// <value>
    /// The ID of the corporation that owns the current station.
    /// </value> 
    public NpcCorporationId OwnerId
    {
      get { return this.Entity.OwnerId; }
    }

    /// <summary>
    /// Gets the number of assembly lines of the specified type that
    /// exist at the current station.
    /// </summary>
    /// <value>
    /// The number of assembly lines of the specified type that
    /// exist at the current station.
    /// </value>
    public byte Quantity
    {
      get
      {
        Contract.Ensures(Contract.Result<byte>() > 0);

        var result = this.Entity.Quantity;
        Contract.Assume(result > 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the region in which the current station resides.
    /// </summary>
    /// <value>
    /// The <see cref="Region" /> in which the current station resides.
    /// </value>    
    public Region Region
    {
      get
      {
        Contract.Ensures(Contract.Result<Region>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.region ?? (this.region = this.Container.GetOrAdd<Region>(this.RegionId, () => this.Entity.Region.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the region in which the current station resides.
    /// </summary>
    /// <value>
    /// The ID of the region in which the current station resides.
    /// </value>   
    public RegionId RegionId
    {
      get { return this.Entity.RegionId; }
    }

    /// <summary>
    /// Gets the solar system in which the current station resides.
    /// </summary>
    /// <value>
    /// The <see cref="SolarSystem" /> in which the current station resides.
    /// </value>    
    public SolarSystem SolarSystem
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystem>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.solarSystem ?? (this.solarSystem = this.Container.GetOrAdd<SolarSystem>(this.SolarSystemId, () => this.Entity.SolarSystem.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the solar system in which the current station resides.
    /// </summary>
    /// <value>
    /// The ID of the solar system in which the current station resides.
    /// </value>   
    public SolarSystemId SolarSystemId
    {
      get { return this.Entity.SolarSystemId; }
    }

    /// <summary>
    /// Gets the current station.
    /// </summary>
    /// <value>
    /// The current <see cref="Station" />.
    /// </value>    
    public Station Station
    {
      get
      {
        Contract.Ensures(Contract.Result<Station>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.station ?? (this.station = this.Container.GetOrAdd<Station>(this.StationId, () => this.Entity.Station.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the current station.
    /// </summary>
    /// <value>
    /// The ID of the current station.
    /// </value>   
    public StationId StationId
    {
      get { return this.Entity.StationId; }
    }

    /// <summary>
    /// Gets the type of the current station.
    /// </summary>
    /// <value>
    /// The current <see cref="StationType" />.
    /// </value>    
    public StationType StationType
    {
      get
      {
        Contract.Ensures(Contract.Result<StationType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.stationType ?? (this.stationType = this.Container.GetOrAdd<StationType>(this.StationTypeId, () => this.Entity.StationType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the current station.
    /// </summary>
    /// <value>
    /// The ID of the type of the current station.
    /// </value>   
    public TypeId StationTypeId
    {
      get { return this.Entity.StationTypeId; }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="stationId">
    /// The ID of the station.
    /// </param>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(StationId stationId, AssemblyLineTypeId assemblyLineTypeId)
    {
      return (long)((((ulong)(long)stationId.Value) << 32) | ((ulong)(long)assemblyLineTypeId.Value));
    }

    /// <inheritdoc />
    public int CompareTo(AssemblyLineStation other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Station.Name.CompareTo(other.Station.Name);

      if (result == 0)
      {
        result = this.AssemblyLineType.Name.CompareTo(other.AssemblyLineType.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AssemblyLineStation);
    }

    /// <inheritdoc />
    public bool Equals(AssemblyLineStation other)
    {
      if (other == null)
      {
        return false;
      }

      return this.StationId == other.StationId && this.AssemblyLineTypeId == other.AssemblyLineTypeId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.StationId, this.AssemblyLineTypeId);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.AssemblyLineType.Name + " (" + this.Quantity.ToString() + ")";
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public sealed partial class AssemblyLineStation : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      AssemblyLineStation other = obj as AssemblyLineStation;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class AssemblyLineStation : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return CreateCompoundId(this.StationId, this.AssemblyLineTypeId); }
    }
  }
  #endregion

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class AssemblyLineStation : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return CreateCompoundId(this.StationId, this.AssemblyLineTypeId); }
    }
  }
  #endregion
}