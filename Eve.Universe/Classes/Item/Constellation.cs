//-----------------------------------------------------------------------
// <copyright file="Constellation.cs" company="Jeremy H. Todd">
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
  /// An EVE item describing an in-game constellation.
  /// </summary>
  public class Constellation : Item {

    #region Instance Fields
    private Faction _faction;
    private ReadOnlyConstellationJumpCollection _jumps;
    private Region _region;
    private ReadOnlySolarSystemCollection _solarSystems;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Constellation class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Constellation(ConstellationEntity entity) : base(entity) {
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
    /// Gets the faction which holds sovereignty over the constellation, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Faction" /> which holds sovereignty over the 
    /// constellation, or <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public Faction Faction {
      get {
        if (_faction == null) {

          Contract.Assume(Entity.Region != null);

          // If the constellation's faction is set, use it
          if (Entity.FactionId != null) {

            // Load the cached version if available
            _faction = Eve.General.Cache.GetOrAdd<Faction>(Entity.FactionId, () => {
              FactionEntity factionEntity = Entity.Faction;
              Contract.Assume(factionEntity != null);

              return new Faction(factionEntity);
            });
          
          // If the region's faction is set, use that
          } else if (Entity.Region.FactionId != null) {
            
            // Load the cached version if available
            _faction = Eve.General.Cache.GetOrAdd<Faction>(Entity.Region.FactionId, () => {
              FactionEntity factionEntity = Entity.Region.Faction;
              Contract.Assume(factionEntity != null);

              return new Faction(factionEntity);
            });
          }
        }

        return _faction;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the faction which holds sovereignty over the region, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Faction" /> which holds sovereignty over the
    /// region, or <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public FactionId? FactionId {
      get {
        Contract.Assume(Entity.Region != null);
        return Entity.FactionId ?? Entity.Region.FactionId;
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
    public new ConstellationId Id {
      get {
        return (ConstellationId) base.Id.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of jumps connecting the constellation to other constellations.
    /// </summary>
    /// 
    /// <value>
    /// The collection of jumps connecting the constellation to other constellations.
    /// </value>
    public ReadOnlyConstellationJumpCollection Jumps {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyConstellationJumpCollection>() != null);

        if (_jumps == null) {
          _jumps = new ReadOnlyConstellationJumpCollection(Eve.General.DataSource.GetConstellationJumps(x => x.FromConstellationId == this.Id.Value).OrderBy(x => x));
        }

        return _jumps;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the radius of the item.
    /// </summary>
    /// 
    /// <value>
    /// The radius of the item.
    /// </value>
    public double Radius {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = Entity.Radius;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the region in which the constellation resides.
    /// </summary>
    /// 
    /// <value>
    /// The region in which the constellation resides.
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
    /// Gets the ID of the region in which the constellation resides.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the region in which the constellation resides.
    /// </value>
    public RegionId RegionId {
      get {
        return (RegionId) Entity.RegionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of solar systems in the constellation.
    /// </summary>
    /// 
    /// <value>
    /// The collection of solar systems in the constellation.
    /// </value>
    public ReadOnlySolarSystemCollection SolarSystems {
      get {
        Contract.Ensures(Contract.Result<ReadOnlySolarSystemCollection>() != null);

        if (_solarSystems == null) {
          _solarSystems = new ReadOnlySolarSystemCollection(Eve.General.DataSource.GetSolarSystems(x => x.ConstellationId == this.Id.Value).OrderBy(x => x));
        }

        return _solarSystems;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the X component of the item's location.
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
    /// Gets the Y component of the item's location.
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
    /// Gets the Z component of the item's location.
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
    //******************************************************************************
    /// <summary>
    /// Gets the maximum extent of the item in the X direction.
    /// </summary>
    /// 
    /// <value>
    /// The maximum extent of the item in the X direction.
    /// </value>
    public double XMax {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.XMax;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the maximum extent of the item in the Y direction.
    /// </summary>
    /// 
    /// <value>
    /// The maximum extent of the item in the Y direction.
    /// </value>
    public double YMax {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.YMax;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the maximum extent of the item in the Z direction.
    /// </summary>
    /// 
    /// <value>
    /// The maximum extent of the item in the Z direction.
    /// </value>
    public double ZMax {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.ZMax;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the minimum extent of the item in the X direction.
    /// </summary>
    /// 
    /// <value>
    /// The minimum extent of the item in the X direction.
    /// </value>
    public double XMin {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.XMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the minimum extent of the item in the Y direction.
    /// </summary>
    /// 
    /// <value>
    /// The minimum extent of the item in the Y direction.
    /// </value>
    public double YMin {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.YMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the minimum extent of the item in the Z direction.
    /// </summary>
    /// 
    /// <value>
    /// The minimum extent of the item in the Z direction.
    /// </value>
    public double ZMin {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = Entity.ZMin;

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
    protected new ConstellationEntity Entity {
      get {
        Contract.Ensures(Contract.Result<ConstellationEntity>() != null);

        return (ConstellationEntity) base.Entity;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of constellations.
  /// </summary>
  public class ReadOnlyConstellationCollection : ReadOnlyCollection<Constellation> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyConstellationCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyConstellationCollection(IEnumerable<Constellation> contents) : base() {
      if (contents != null) {
        foreach (Constellation constellation in contents) {
          Items.AddWithoutCallback(constellation);
        }
      }
    }
    #endregion
  }
}