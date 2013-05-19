//-----------------------------------------------------------------------
// <copyright file="AssemblyLineStation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains a summary of the number of assembly lines of a particular
  /// type that are located at a particular station.
  /// </summary>
  public sealed partial class AssemblyLineStation : EveEntityAdapter<AssemblyLineStationEntity, AssemblyLineStation>
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
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AssemblyLineStation(IEveRepository repository, AssemblyLineStationEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
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
        return this.LazyInitializeAdapter(ref this.assemblyLineType, this.Entity.AssemblyLineTypeId, () => this.Entity.AssemblyLineType);
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
        return this.LazyInitializeAdapter(ref this.owner, this.Entity.OwnerId, () => this.Entity.Owner);
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
        return this.LazyInitializeAdapter(ref this.region, this.Entity.RegionId, () => this.Entity.Region);
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
        return this.LazyInitializeAdapter(ref this.solarSystem, this.Entity.SolarSystemId, () => this.Entity.SolarSystem);
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
        return this.LazyInitializeAdapter(ref this.station, this.Entity.StationId, () => this.Entity.Station);
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
        return this.LazyInitializeAdapter(ref this.stationType, this.Entity.StationTypeId, () => this.Entity.StationType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of the current station.
    /// </summary>
    /// <value>
    /// The ID of the type of the current station.
    /// </value>   
    public EveTypeId StationTypeId
    {
      get { return this.Entity.StationTypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(AssemblyLineStation other)
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
    public override string ToString()
    {
      return this.AssemblyLineType.Name + " (" + this.Quantity.ToString() + ")";
    }
  }
}