//-----------------------------------------------------------------------
// <copyright file="SolarSystem.cs" company="Jeremy H. Todd">
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
  /// An EVE item describing an in-game solar system.
  /// </summary>
  public class SolarSystem : Item {

    #region Instance Fields
    private Constellation _constellation;
    private EveType _sunType;
    private Faction _faction;
    private ReadOnlySolarSystemJumpCollection _jumps;
    private Region _region;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the SolarSystem class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal SolarSystem(SolarSystemEntity entity) : base(entity) {
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
    /// Gets a value indicating whether the solar system is adjacent to a solar
    /// system controlled by a different faction.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the solar system is adjacent to a solar system
    /// controlled by a different faction.; otherwise <see langword="false" />.
    /// </value>
    public bool Border {
      get {
        return Entity.Border;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the constellation in which the solar system resides.
    /// </summary>
    /// 
    /// <value>
    /// The constellation in which the solar system resides.
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
    /// Gets the ID of the constellation in which the solar system resides.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the constellation in which the solar system resides.
    /// </value>
    public ConstellationId ConstellationId {
      get {
        return (ConstellationId) Entity.ConstellationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the solar system is adjacent to a solar
    /// system in another constellation.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the solar system is adjacent to a solar
    /// system in another constellation; otherwise <see langword="false" />.
    /// </value>
    public bool ConstellationBorder {
      get {
        return Entity.ConstellationBorder;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the solar system is a corridor system
    /// (i.e. has two jumps).
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the solar system is a corridor system;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Corridor {
      get {
        return Entity.Corridor;
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

          Contract.Assume(Entity.Constellation != null);
          Contract.Assume(Entity.Region != null);

          // If the solar system's faction is set, use it
          if (Entity.FactionId != null) {

            // Load the cached version if available
            _faction = Eve.General.Cache.GetOrAdd<Faction>(Entity.FactionId, () => {
              FactionEntity factionEntity = Entity.Faction;
              Contract.Assume(factionEntity != null);

              return new Faction(factionEntity);
            });

          // If the constellation's faction is set, use it
          } else if (Entity.Constellation.FactionId != null) {

            // Load the cached version if available
            _faction = Eve.General.Cache.GetOrAdd<Faction>(Entity.Constellation.FactionId, () => {
              FactionEntity factionEntity = Entity.Constellation.Faction;
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
    /// Gets the ID of the faction which holds sovereignty over the solar system,
    /// if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Faction" /> which holds sovereignty over the
    /// solar system, or <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public FactionId? FactionId {
      get {
        Contract.Assume(Entity.Constellation != null);
        Contract.Assume(Entity.Region != null);

        return Entity.FactionId ?? Entity.Constellation.FactionId ?? Entity.Region.FactionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the solar system is a fringe system
    /// (i.e. has only one jump).
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the solar system is a fringe system;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Fringe {
      get {
        return Entity.Fringe;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the solar system is a hub system
    /// (i.e. has at least three jumps).
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the solar system is a hub system;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Hub {
      get {
        return Entity.Hub;
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
    public new SolarSystemId Id {
      get {
        return (SolarSystemId) base.Id.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the solar system contains assets belonging
    /// to more than one faction.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the solar system contains assets belonging
    /// to more than one faction; otherwise <see langword="false" />.
    /// </value>
    public bool International {
      get {
        return Entity.International;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of jumps connecting the solar system to other solar systems.
    /// </summary>
    /// 
    /// <value>
    /// The collection of jumps connecting the solar system to other solar systems.
    /// </value>
    public ReadOnlySolarSystemJumpCollection Jumps {
      get {
        Contract.Ensures(Contract.Result<ReadOnlySolarSystemJumpCollection>() != null);

        if (_jumps == null) {
          _jumps = new ReadOnlySolarSystemJumpCollection(Eve.General.DataSource.GetSolarSystemJumps(x => x.FromSolarSystemId == this.Id.Value).OrderBy(x => x));
        }

        return _jumps;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the luminosity of the solar system's star.
    /// </summary>
    /// 
    /// <value>
    /// The luminosity of the solar system's star.
    /// </value>
    public double Luminosity {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = Entity.Luminosity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
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
    /// Gets the region in which the solar system resides.
    /// </summary>
    /// 
    /// <value>
    /// The region in which the solar system resides.
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
    /// Gets the ID of the region in which the solar system resides.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the region in which the solar system resides.
    /// </value>
    public RegionId RegionId {
      get {
        return (RegionId) Entity.RegionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the solar system is adjacent to a solar
    /// system in another region.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the solar system is adjacent to a solar
    /// system in another region; otherwise <see langword="false" />.
    /// </value>
    public bool Regional {
      get {
        return Entity.Regional;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the security status of the solar system.
    /// </summary>
    /// 
    /// <value>
    /// The security status of the solar system.
    /// </value>
    public double Security {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result = Entity.Security;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the security class of the solar system.
    /// </summary>
    /// 
    /// <value>
    /// The security class of the solar system.
    /// </value>
    public string SecurityClass {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.SecurityClass ?? string.Empty;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the solar system's star.
    /// </summary>
    /// 
    /// <value>
    /// The type of the solar system's star.
    /// </value>
    public EveType SunType { // TODO: Update with specified type
      get {
        if (_sunType == null) {

          // Load the cached version if available
          _sunType = Eve.General.Cache.GetOrAdd<EveType>(SunTypeId, () => {
            EveTypeEntity typeEntity = Entity.SunType;
            Contract.Assume(typeEntity != null);

            return EveType.Create(typeEntity);
          });
        }

        return _sunType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the solar system's star.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the solar system's star.
    /// </value>
    public TypeId SunTypeId {
      get {
        return Entity.SunTypeId;
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
    protected new SolarSystemEntity Entity {
      get {
        Contract.Ensures(Contract.Result<SolarSystemEntity>() != null);

        return (SolarSystemEntity) base.Entity;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of solar systems.
  /// </summary>
  public class ReadOnlySolarSystemCollection : ReadOnlyCollection<SolarSystem> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlySolarSystemCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlySolarSystemCollection(IEnumerable<SolarSystem> contents) : base() {
      if (contents != null) {
        foreach (SolarSystem solarSystem in contents) {
          Items.AddWithoutCallback(solarSystem);
        }
      }
    }
    #endregion
  }
}