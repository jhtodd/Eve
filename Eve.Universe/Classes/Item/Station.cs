//-----------------------------------------------------------------------
// <copyright file="Station.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// An EVE item describing an in-game station.
  /// </summary>
  public sealed class Station : Item
  {
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
    internal Station(IEveRepository container, StationEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */
    
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
        return this.constellation ?? (this.constellation = this.Container.Load<Constellation>(this.ConstellationId, () => this.Entity.Constellation.ToAdapter(this.Container)));
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
      get { return (ConstellationId)this.Entity.ConstellationId; }
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
        return this.corporation ?? (this.corporation = this.Container.Load<NpcCorporation>(this.CorporationId, () => this.Entity.Corporation.ToAdapter(this.Container)));
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
      get { return (NpcCorporationId)this.Entity.CorporationId; }
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

        var result = this.Entity.DockingCostPerVolume;

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

        var result = this.Entity.MaxShipVolumeDockable;

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

        var result = this.Entity.OfficeRentalCost;

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
        return this.operation ?? (this.operation = this.Container.Load<StationOperation>(this.OperationId, () => this.Entity.Operation.ToAdapter(this.Container)));
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
      get { return this.Entity.OperationId; }
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
        return this.region ?? (this.region = this.Container.Load<Region>(this.RegionId, () => this.Entity.Region.ToAdapter(this.Container)));
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
      get { return (RegionId)this.Entity.RegionId; }
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

        var result = this.Entity.ReprocessingEfficiency;

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
      get { return this.Entity.ReprocessingHangarFlag; }
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

        var result = this.Entity.ReprocessingStationsTake;

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
      get { return this.Entity.Security; }
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
        return this.solarSystem ?? (this.solarSystem = this.Container.Load<SolarSystem>(this.SolarSystemId, () => this.Entity.SolarSystem.ToAdapter(this.Container)));
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
      get { return (SolarSystemId)this.Entity.SolarSystemId; }
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
        return this.stationType ?? (this.stationType = this.Container.Load<StationType>(this.StationTypeId, () => (StationType)this.Entity.StationType.ToAdapter(this.Container)));
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
      get { return this.Entity.StationTypeId; }
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

        double result = this.Entity.X;

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

        double result = this.Entity.Y;

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

        double result = this.Entity.Z;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    private new StationEntity Entity
    {
      get
      {
        Contract.Ensures(Contract.Result<StationEntity>() != null);

        return (StationEntity)base.Entity;
      }
    }
  }
}