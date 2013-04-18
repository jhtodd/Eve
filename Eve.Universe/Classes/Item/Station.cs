//-----------------------------------------------------------------------
// <copyright file="Station.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Industry;

  /// <summary>
  /// An EVE item describing an in-game station.
  /// </summary>
  public sealed class Station : Item
  {
    private ReadOnlyAgentCollection agents;
    private ReadOnlyAssemblyLineCollection assemblyLines;
    private ReadOnlyAssemblyLineStationCollection assemblyLineTypes;
    private Constellation constellation;
    private NpcCorporation corporation;
    private StationOperation operation;
    private Region region;
    private SolarSystem solarSystem;
    private StationType stationType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Station class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Station(IEveRepository container, ItemEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsStation, "The entity must be a station.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of agents in the station.
    /// </summary>
    /// <value>
    /// The collection of agents in the station.
    /// </value>
    public ReadOnlyAgentCollection Agents
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAgentCollection>() != null);

        return this.agents ?? (this.agents = new ReadOnlyAgentCollection(this.Container.GetAgents(x => x.AgentInfo.LocationId == this.Id.Value).OrderBy(x => x)));
      }
    }

    /// <summary>
    /// Gets the collection of assembly lines located at the station.
    /// </summary>
    /// <value>
    /// The collection of assembly lines located at the station.
    /// </value>
    public ReadOnlyAssemblyLineCollection AssemblyLines
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAssemblyLineCollection>() != null);

        return this.assemblyLines ?? (this.assemblyLines = new ReadOnlyAssemblyLineCollection(this.Container.GetAssemblyLines(x => x.ContainerId == this.Id.Value).OrderBy(x => x)));
      }
    }

    /// <summary>
    /// Gets a collection describing the types and number of assembly
    /// lines located at the station.
    /// </summary>
    /// <value>
    /// A collection describing the types and number of assembly
    /// lines located at the station.
    /// </value>
    public ReadOnlyAssemblyLineStationCollection AssemblyLineTypes
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAssemblyLineStationCollection>() != null);

        return this.assemblyLineTypes ?? (this.assemblyLineTypes = new ReadOnlyAssemblyLineStationCollection(this.Container.GetAssemblyLineStations(x => x.StationId == this.Id.Value).OrderBy(x => x)));
      }
    }
    
    /// <summary>
    /// Gets the constellation in which the station resides.
    /// </summary>
    /// <value>
    /// The constellation in which the station resides.
    /// </value>
    public Constellation Constellation
    {
      get
      {
        Contract.Ensures(Contract.Result<Constellation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.constellation ?? (this.constellation = this.Container.GetOrAdd<Constellation>(this.ConstellationId, () => (Constellation)this.StationInfo.Constellation.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the constellation in which the station resides.
    /// </summary>
    /// <value>
    /// The ID of the constellation in which the station resides.
    /// </value>
    public ConstellationId ConstellationId
    {
      get { return (ConstellationId)this.StationInfo.ConstellationId; }
    }

    /// <summary>
    /// Gets the corporation that controls the station.
    /// </summary>
    /// <value>
    /// The corporation that controls the station.
    /// </value>
    public NpcCorporation Corporation
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.corporation ?? (this.corporation = this.Container.GetOrAdd<NpcCorporation>(this.CorporationId, () => (NpcCorporation)this.StationInfo.Corporation.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the corporation that controls the station.
    /// </summary>
    /// <value>
    /// The ID of the corporation that controls the station.
    /// </value>
    public NpcCorporationId CorporationId
    {
      get { return (NpcCorporationId)this.StationInfo.CorporationId; }
    }

    /// <summary>
    /// Gets the cost to dock per unit of ship volume.  This is always zero for
    /// NPC stations.
    /// </summary>
    /// <value>
    /// The cost to dock per unit of ship volume.
    /// </value>
    public double DockingCostPerVolume
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.StationInfo.DockingCostPerVolume;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new StationId Id
    {
      get { return (StationId)base.Id.Value; }
    }

    /// <summary>
    /// Gets the maximum volume of a ship allowed to dock at the station.
    /// </summary>
    /// <value>
    /// The maximum volume of a ship allowed to dock at the station.
    /// </value>
    public double MaxShipVolumeDockable
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.StationInfo.MaxShipVolumeDockable;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the base (?) cost to rent an office at the station.
    /// </summary>
    /// <value>
    /// The base cost to rent an office at the station.  This is adjusted in-game
    /// by factors such as occupancy and possibly standings.
    /// </value>
    public int OfficeRentalCost
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        var result = this.StationInfo.OfficeRentalCost;

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the mode in which the station is operating.
    /// </summary>
    /// <value>
    /// The mode in which the station is operating.
    /// </value>
    public StationOperation Operation
    {
      get
      {
        Contract.Ensures(Contract.Result<StationOperation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.operation ?? (this.operation = this.Container.GetOrAdd<StationOperation>(this.OperationId, () => this.StationInfo.Operation.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the mode in which the station is operating.
    /// </summary>
    /// <value>
    /// The ID of the mode in which the station is operating.
    /// </value>
    public StationOperationId OperationId
    {
      get { return this.StationInfo.OperationId; }
    }

    /// <summary>
    /// Gets the region in which the station resides.
    /// </summary>
    /// <value>
    /// The region in which the station resides.
    /// </value>
    public Region Region
    {
      get
      {
        Contract.Ensures(Contract.Result<Region>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.region ?? (this.region = this.Container.GetOrAdd<Region>(this.RegionId, () => (Region)this.StationInfo.Region.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the region in which the station resides.
    /// </summary>
    /// <value>
    /// The ID of the region in which the station resides.
    /// </value>
    public RegionId RegionId
    {
      get { return (RegionId)this.StationInfo.RegionId; }
    }

    /// <summary>
    /// Gets the efficiency of reprocessing jobs performed at the station.
    /// </summary>
    /// <value>
    /// The efficiency of reprocessing jobs performed at the station.
    /// </value>
    public double ReprocessingEfficiency
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);
        Contract.Ensures(Contract.Result<double>() <= 1.0D);

        var result = this.StationInfo.ReprocessingEfficiency;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);
        Contract.Assume(result <= 1.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets a flag value related to reprocessing.  The meaning of this property
    /// is unknown and appears to always be 4 for NPC stations.
    /// </summary>
    /// <value>
    /// A flag value related to reprocessing.
    /// </value>
    public byte ReprocessingHangarFlag
    {
      get { return this.StationInfo.ReprocessingHangarFlag; }
    }

    /// <summary>
    /// Gets the station's take from reprocessing jobs.
    /// </summary>
    /// <value>
    /// The station's take from reprocessing jobs.
    /// </value>
    public double ReprocessingStationsTake
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);
        Contract.Ensures(Contract.Result<double>() <= 1.0D);

        var result = this.StationInfo.ReprocessingStationsTake;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);
        Contract.Assume(result <= 1.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets a value related to the security level of the station.  The meaning
    /// of this property is unknown and is -1 for two stations and 0 for all the
    /// rest.
    /// </summary>
    /// <value>
    /// A value related to the security level of the station.
    /// </value>
    public short Security
    {
      get { return this.StationInfo.Security; }
    }

    /// <summary>
    /// Gets the solar system in which the station resides.
    /// </summary>
    /// <value>
    /// The solar system in which the station resides.
    /// </value>
    public SolarSystem SolarSystem
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystem>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.solarSystem ?? (this.solarSystem = this.Container.GetOrAdd<SolarSystem>(this.SolarSystemId, () => (SolarSystem)this.StationInfo.SolarSystem.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the solar system in which the station resides.
    /// </summary>
    /// <value>
    /// The ID of the solar system in which the station resides.
    /// </value>
    public SolarSystemId SolarSystemId
    {
      get { return (SolarSystemId)this.StationInfo.SolarSystemId; }
    }

    /// <summary>
    /// Gets the type of the station.
    /// </summary>
    /// <value>
    /// The type of the station.
    /// </value>
    public StationType StationType
    {
      get
      {
        Contract.Ensures(Contract.Result<StationType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.stationType ?? (this.stationType = this.Container.GetOrAdd<StationType>(this.StationTypeId, () => (StationType)this.StationInfo.StationType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the station.
    /// </summary>
    /// <value>
    /// The ID of the type of the station.
    /// </value>
    public TypeId StationTypeId
    {
      get { return this.StationInfo.StationTypeId; }
    }

    /// <summary>
    /// Gets the X component of the station's location, relative to the center
    /// of its solar system.
    /// </summary>
    /// <value>
    /// The X component of the item's location.
    /// </value>
    public double X
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.StationInfo.X;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Y component of the station's location, relative to the center
    /// of its solar system.
    /// </summary>
    /// <value>
    /// The Y component of the item's location.
    /// </value>
    public double Y
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.StationInfo.Y;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Z component of the station's location, relative to the center
    /// of its solar system.
    /// </summary>
    /// <value>
    /// The Z component of the item's location.
    /// </value>
    public double Z
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.StationInfo.Z;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    private StationEntity StationInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<StationEntity>() != null);

        var result = this.Entity.StationInfo;

        Contract.Assume(result != null);
        return result;
      }
    }
  }
}