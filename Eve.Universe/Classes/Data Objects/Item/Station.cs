﻿//-----------------------------------------------------------------------
// <copyright file="Station.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

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
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Station(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
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

        return Station.LazyInitialize(
          ref this.agents,
          () => ReadOnlyAgentCollection.Create(this.Repository, this.StationInfo == null ? null : this.StationInfo.Agents));
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

        return Station.LazyInitialize(
          ref this.assemblyLines,
          () => ReadOnlyAssemblyLineCollection.Create(this.Repository, this.StationInfo == null ? null : this.StationInfo.AssemblyLines));
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

        return Station.LazyInitialize(
          ref this.assemblyLineTypes,
          () => ReadOnlyAssemblyLineStationCollection.Create(this.Repository, this.StationInfo == null ? null : this.StationInfo.AssemblyLineTypes));
      }
    }
    
    /// <summary>
    /// Gets the constellation in which the station resides.
    /// </summary>
    /// <value>
    /// The constellation in which the station resides, or
    /// <see langword="null" /> if no constellation information
    /// exists.
    /// </value>
    public Constellation Constellation
    {
      get
      {
        Contract.Ensures(this.ConstellationId == null || Contract.Result<Constellation>() != null);

        if (this.ConstellationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.constellation, this.StationInfo.ConstellationId, () => this.StationInfo.Constellation);
      }
    }

    /// <summary>
    /// Gets the ID of the constellation in which the station resides.
    /// </summary>
    /// <value>
    /// The ID of the constellation in which the station resides, or
    /// <see langword="null" /> if no constellation information
    /// exists.
    /// </value>
    public ConstellationId? ConstellationId
    {
      get 
      {
        Contract.Ensures(Contract.Result<ConstellationId?>() == null || this.StationInfo != null);
        return this.StationInfo == null ? (ConstellationId?)null : (ConstellationId?)this.StationInfo.ConstellationId;
      }
    }

    /// <summary>
    /// Gets the corporation that controls the station.
    /// </summary>
    /// <value>
    /// The corporation that controls the station, or
    /// <see langword="null" /> if no corporation information
    /// exists.
    /// </value>
    public NpcCorporation Corporation
    {
      get
      {
        Contract.Ensures(this.CorporationId == null || Contract.Result<NpcCorporation>() != null);

        if (this.CorporationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.corporation, this.StationInfo.CorporationId, () => this.StationInfo.Corporation);
      }
    }

    /// <summary>
    /// Gets the ID of the corporation that controls the station.
    /// </summary>
    /// <value>
    /// The ID of the corporation that controls the station, or
    /// <see langword="null" /> if no corporation information
    /// exists..
    /// </value>
    public NpcCorporationId? CorporationId
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporationId?>() == null || this.StationInfo != null);
        return this.StationInfo == null ? (NpcCorporationId?)null : (NpcCorporationId?)this.StationInfo.CorporationId;
      }
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

        var result = this.StationInfo == null ? 0.0D : this.StationInfo.DockingCostPerVolume;

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

        var result = this.StationInfo == null ? 0.0D : this.StationInfo.MaxShipVolumeDockable;

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

        var result = this.StationInfo == null ? 0 : this.StationInfo.OfficeRentalCost;

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the mode in which the station is operating.
    /// </summary>
    /// <value>
    /// The mode in which the station is operating, or
    /// <see langword="null" /> if no operation information exists.
    /// </value>
    public StationOperation Operation
    {
      get
      {
        Contract.Ensures(this.OperationId == null || Contract.Result<StationOperation>() != null);

        if (this.OperationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.operation, this.StationInfo.OperationId, () => this.StationInfo.Operation);
      }
    }

    /// <summary>
    /// Gets the ID of the mode in which the station is operating.
    /// </summary>
    /// <value>
    /// The ID of the mode in which the station is operating, or
    /// <see langword="null" /> if no operation information exists.
    /// </value>
    public StationOperationId? OperationId
    {
      get
      {
        Contract.Ensures(Contract.Result<StationOperationId?>() == null || this.StationInfo != null);
        return this.StationInfo == null ? (StationOperationId?)null : (StationOperationId?)this.StationInfo.OperationId;
      }
    }

    /// <summary>
    /// Gets the region in which the station resides.
    /// </summary>
    /// <value>
    /// The region in which the station resides, or
    /// <see langword="null" /> if no region information exists.
    /// </value>
    public Region Region
    {
      get
      {
        Contract.Ensures(this.RegionId == null || Contract.Result<Region>() != null);

        if (this.RegionId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.region, this.StationInfo.RegionId, () => this.StationInfo.Region);
      }
    }

    /// <summary>
    /// Gets the ID of the region in which the station resides.
    /// </summary>
    /// <value>
    /// The ID of the region in which the station resides, or
    /// <see langword="null" /> if no region information exists.
    /// </value>
    public RegionId? RegionId
    {
      get 
      {
        Contract.Ensures(Contract.Result<RegionId?>() == null || this.StationInfo != null);
        return this.StationInfo == null ? (RegionId?)null : (RegionId?)this.StationInfo.RegionId;
      }
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

        var result = this.StationInfo == null ? 0.0D : this.StationInfo.ReprocessingEfficiency;

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
      get { return this.StationInfo == null ? (byte)0 : this.StationInfo.ReprocessingHangarFlag; }
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

        var result = this.StationInfo == null ? 0.0D : this.StationInfo.ReprocessingStationsTake;

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
      get { return this.StationInfo == null ? (short)0 : this.StationInfo.Security; }
    }

    /// <summary>
    /// Gets the solar system in which the station resides.
    /// </summary>
    /// <value>
    /// The solar system in which the station resides, or
    /// <see langword="null" /> if no solar system information exists.
    /// </value>
    public SolarSystem SolarSystem
    {
      get
      {
        Contract.Ensures(this.SolarSystemId == null || Contract.Result<SolarSystem>() != null);

        if (this.SolarSystemId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.solarSystem, this.StationInfo.SolarSystemId, () => this.StationInfo.SolarSystem);
      }
    }

    /// <summary>
    /// Gets the ID of the solar system in which the station resides.
    /// </summary>
    /// <value>
    /// The ID of the solar system in which the station resides, or
    /// <see langword="null" /> if no solar system information exists.
    /// </value>
    public SolarSystemId? SolarSystemId
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystemId?>() == null || this.StationInfo != null);
        return this.StationInfo == null ? (SolarSystemId?)null : (SolarSystemId?)this.StationInfo.SolarSystemId;
      }
    }

    /// <summary>
    /// Gets the type of the station.
    /// </summary>
    /// <value>
    /// The type of the station, or <see langword="null" />
    /// if no station information exists.
    /// </value>
    public StationType StationType
    {
      get
      {
        Contract.Ensures(this.StationTypeId == null || Contract.Result<StationType>() != null);

        if (this.StationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.stationType, this.StationInfo.StationTypeId, () => this.StationInfo.StationType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of the station.
    /// </summary>
    /// <value>
    /// The ID of the type of the station, or <see langword="null" />
    /// if no station information exists.
    /// </value>
    public EveTypeId? StationTypeId
    {
      get
      {
        Contract.Ensures(Contract.Result<EveTypeId?>() == null || this.StationInfo != null);
        return this.StationInfo == null ? (EveTypeId?)null : (EveTypeId?)this.StationInfo.StationTypeId;
      }
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

        double result = this.StationInfo == null ? 0.0D : this.StationInfo.X;

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

        double result = this.StationInfo == null ? 0.0D : this.StationInfo.Y;

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

        double result = this.StationInfo == null ? 0.0D : this.StationInfo.Z;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the <see cref="StationEntity" /> containing the specific
    /// information for the current item type.
    /// </summary>
    /// <value>
    /// The <see cref="StationEntity" /> containing the specific
    /// information for the current item type.
    /// </value>
    private StationEntity StationInfo
    {
      get { return this.Entity.StationInfo; }
    }
  }
}