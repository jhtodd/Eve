//-----------------------------------------------------------------------
// <copyright file="Region.cs" company="Jeremy H. Todd">
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
  /// An EVE item describing an in-game region.
  /// </summary>
  public class Region : Item {

    #region Instance Fields
    private ReadOnlyConstellationCollection _constellations;
    private Faction _faction;
    private ReadOnlyRegionJumpCollection _jumps;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Region class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Region(RegionEntity entity) : base(entity) {
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
    /// Gets the collection of constellations in the region.
    /// </summary>
    /// 
    /// <value>
    /// The collection of constellations in the region.
    /// </value>
    public ReadOnlyConstellationCollection Constellations {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyConstellationCollection>() != null);

        if (_constellations == null) {
          _constellations = new ReadOnlyConstellationCollection(Eve.General.DataSource.GetConstellations(x => x.RegionId == this.Id.Value).OrderBy(x => x));
        }

        return _constellations;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the faction which holds sovereignty over the region, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Faction" /> which holds sovereignty over the region, or
    /// <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public Faction Faction {
      get {
        if (_faction == null) {
          if (FactionId != null) {

            // Load the cached version if available
            _faction = Eve.General.Cache.GetOrAdd<Faction>(FactionId, () => {
              FactionEntity factionEntity = Entity.Faction;
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
        return Entity.FactionId;
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
    public new RegionId Id {
      get {
        return (RegionId) base.Id.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of jumps connecting the region to other regions.
    /// </summary>
    /// 
    /// <value>
    /// The collection of jumps connecting the region to other regions.
    /// </value>
    public ReadOnlyRegionJumpCollection Jumps {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyRegionJumpCollection>() != null);

        if (_jumps == null) {
          _jumps = new ReadOnlyRegionJumpCollection(Eve.General.DataSource.GetRegionJumps(x => x.FromRegionId == this.Id.Value).OrderBy(x => x));
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
    protected new RegionEntity Entity {
      get {
        Contract.Ensures(Contract.Result<RegionEntity>() != null);

        return (RegionEntity) base.Entity;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of regions.
  /// </summary>
  public class ReadOnlyRegionCollection : ReadOnlyCollection<Region> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyRegionCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyRegionCollection(IEnumerable<Region> contents) : base() {
      if (contents != null) {
        foreach (Region region in contents) {
          Items.AddWithoutCallback(region);
        }
      }
    }
    #endregion
  }
}