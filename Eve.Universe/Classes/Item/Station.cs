//-----------------------------------------------------------------------
// <copyright file="Station.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  using Eve.Data.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// An EVE item describing an in-game station.
  /// </summary>
  public class Station : Item {

    #region Instance Fields
    private Constellation _constellation;
    private NpcCorporation _corporation;
    private StationOperation _operation;
    private Region _region;
    private SolarSystem _solarSystem;
    private StationType _stationType;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Station class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Station(StationEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
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
    /// Gets the constellation in which the station resides.
    /// </summary>
    /// 
    /// <value>
    /// The constellation in which the station resides.
    /// </value>
    public Constellation Constellation {
      get {
        Contract.Ensures(Contract.Result<Constellation>() != null);

        if (_constellation == null) {

          // Load the cached version if available
          _constellation = Eve.General.Cache.GetOrAdd<Constellation>(ConstellationId, () => {
            ConstellationEntity constellationEntity = Entity.Constellation;
            Contract.Assume(constellationEntity != null);

            return new Constellation(constellationEntity);
          });
        }

        return _constellation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the constellation in which the station resides.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the constellation in which the station resides.
    /// </value>
    public ConstellationId ConstellationId {
      get {
        return (ConstellationId) Entity.ConstellationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the corporation that controls the station.
    /// </summary>
    /// 
    /// <value>
    /// The corporation that controls the station.
    /// </value>
    public NpcCorporation Corporation {
      get {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        if (_corporation == null) {

          // Load the cached version if available
          _corporation = Eve.General.Cache.GetOrAdd<NpcCorporation>(CorporationId, () => {
            NpcCorporationEntity corporationEntity = Entity.Corporation;
            Contract.Assume(corporationEntity != null);

            return new NpcCorporation(corporationEntity);
          });
        }

        return _corporation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the corporation that controls the station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporation that controls the station.
    /// </value>
    public NpcCorporationId CorporationId {
      get {
        return (NpcCorporationId) Entity.CorporationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the cost to dock per unit of ship volume.  This is always zero for
    /// NPC stations.
    /// </summary>
    /// 
    /// <value>
    /// The cost to dock per unit of ship volume.
    /// </value>
    public double DockingCostPerVolume {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = Entity.DockingCostPerVolume;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new StationId Id {
      get {
        return (StationId) base.Id.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the maximum volume of a ship allowed to dock at the station.
    /// </summary>
    /// 
    /// <value>
    /// The maximum volume of a ship allowed to dock at the station.
    /// </value>
    public double MaxShipVolumeDockable {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = Entity.MaxShipVolumeDockable;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the base (?) cost to rent an office at the station.
    /// </summary>
    /// 
    /// <value>
    /// The base cost to rent an office at the station.  This is adjusted in-game
    /// by factors such as occupancy and possibly standings.
    /// </value>
    public int OfficeRentalCost {
      get {
        Contract.Ensures(Contract.Result<int>() >= 0);

        var result = Entity.OfficeRentalCost;

        Contract.Assume(result >= 0);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the mode in which the station is operating.
    /// </summary>
    /// 
    /// <value>
    /// The mode in which the station is operating.
    /// </value>
    public StationOperation Operation {
      get {
        Contract.Ensures(Contract.Result<StationOperation>() != null);

        if (_operation == null) {

          // Load the cached version if available
          _operation = Eve.General.Cache.GetOrAdd<StationOperation>(OperationId, () => {
            StationOperationEntity operationEntity = Entity.Operation;
            Contract.Assume(operationEntity != null);

            return new StationOperation(operationEntity);
          });
        }

        return _operation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the mode in which the station is operating.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the mode in which the station is operating.
    /// </value>
    public StationOperationId OperationId {
      get {
        return Entity.OperationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the region in which the station resides.
    /// </summary>
    /// 
    /// <value>
    /// The region in which the station resides.
    /// </value>
    public Region Region {
      get {
        Contract.Ensures(Contract.Result<Region>() != null);

        if (_region == null) {

          // Load the cached version if available
          _region = Eve.General.Cache.GetOrAdd<Region>(RegionId, () => {
            RegionEntity regionEntity = Entity.Region;
            Contract.Assume(regionEntity != null);

            return new Region(regionEntity);
          });
        }

        return _region;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the region in which the station resides.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the region in which the station resides.
    /// </value>
    public RegionId RegionId {
      get {
        return (RegionId) Entity.RegionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the efficiency of reprocessing jobs performed at the station.
    /// </summary>
    /// 
    /// <value>
    /// The efficiency of reprocessing jobs performed at the station.
    /// </value>
    public double ReprocessingEfficiency {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);
        Contract.Ensures(Contract.Result<double>() <= 1.0D);

        var result = Entity.ReprocessingEfficiency;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);
        Contract.Assume(result <= 1.0D);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a flag value related to reprocessing.  The meaning of this property
    /// is unknown and appears to always be 4 for NPC stations.
    /// </summary>
    /// 
    /// <value>
    /// A flag value related to reprocessing.
    /// </value>
    public byte ReprocessingHangarFlag {
      get {
        return Entity.ReprocessingHangarFlag;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the station's take from reprocessing jobs.
    /// </summary>
    /// 
    /// <value>
    /// The station's take from reprocessing jobs.
    /// </value>
    public double ReprocessingStationsTake {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);
        Contract.Ensures(Contract.Result<double>() <= 1.0D);

        var result = Entity.ReprocessingStationsTake;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);
        Contract.Assume(result <= 1.0D);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value related to the security level of the station.  The meaning
    /// of this property is unknown and is -1 for two stations and 0 for all the
    /// rest.
    /// </summary>
    /// 
    /// <value>
    /// A value related to the security level of the station.
    /// </value>
    public short Security {
      get {
        return Entity.Security;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the solar system in which the station resides.
    /// </summary>
    /// 
    /// <value>
    /// The solar system in which the station resides.
    /// </value>
    public SolarSystem SolarSystem {
      get {
        Contract.Ensures(Contract.Result<SolarSystem>() != null);

        if (_solarSystem == null) {

          // Load the cached version if available
          _solarSystem = Eve.General.Cache.GetOrAdd<SolarSystem>(SolarSystemId, () => {
            SolarSystemEntity solarSystemEntity = Entity.SolarSystem;
            Contract.Assume(solarSystemEntity != null);

            return new SolarSystem(solarSystemEntity);
          });
        }

        return _solarSystem;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the solar system in which the station resides.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the solar system in which the station resides.
    /// </value>
    public SolarSystemId SolarSystemId {
      get {
        return (SolarSystemId) Entity.SolarSystemId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the station.
    /// </summary>
    /// 
    /// <value>
    /// The type of the station.
    /// </value>
    public StationType StationType {
      get {
        Contract.Ensures(Contract.Result<StationType>() != null);

        if (_stationType == null) {

          // Load the cached version if available
          _stationType = Eve.General.Cache.GetOrAdd<StationType>(StationTypeId, () => {
            StationTypeEntity stationTypeEntity = Entity.StationType;
            Contract.Assume(stationTypeEntity != null);

            return new StationType(stationTypeEntity);
          });
        }

        return _stationType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the station.
    /// </value>
    public TypeId StationTypeId {
      get {
        return Entity.StationTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the X component of the station's location, relative to the center
    /// of its solar system.
    /// </summary>
    /// 
    /// <value>
    /// The X component of the item's location.
    /// </value>
    public double X {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.X;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the Y component of the station's location, relative to the center
    /// of its solar system.
    /// </summary>
    /// 
    /// <value>
    /// The Y component of the item's location.
    /// </value>
    public double Y {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.Y;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the Z component of the station's location, relative to the center
    /// of its solar system.
    /// </summary>
    /// 
    /// <value>
    /// The Z component of the item's location.
    /// </value>
    public double Z {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.Z;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
    /// 
    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    protected new StationEntity Entity {
      get {
        Contract.Ensures(Contract.Result<StationEntity>() != null);

        return (StationEntity) base.Entity;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of stations.
  /// </summary>
  public class ReadOnlyStationCollection : ReadOnlyCollection<Station> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyStationCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyStationCollection(IEnumerable<Station> contents) : base() {
      if (contents != null) {
        foreach (Station station in contents) {
          Items.AddWithoutCallback(station);
        }
      }
    }
    #endregion
  }
}