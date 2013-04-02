//-----------------------------------------------------------------------
// <copyright file="Faction.cs" company="Jeremy H. Todd">
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

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about an EVE faction.
  /// </summary>
  public class Faction : BaseValue<FactionId, FactionId, FactionEntity, Faction>,
                         IHasIcon,
                         IHasRaces {

    #region Instance Fields
    private NpcCorporation _corporation;
    private Icon _icon;
    private NpcCorporation _militiaCorporation;
    private SolarSystem _solarSystem;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Faction class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Faction(FactionEntity entity) : base(entity) {
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
    /// Gets the faction's main corporation.
    /// </summary>
    /// 
    /// <value>
    /// The faction's main corporation.
    /// </value>
    public NpcCorporation Corporation {
      get {
        if (_corporation == null) {
          if (CorporationId != null) {

            // Load the cached version if available
            _corporation = Eve.General.Cache.GetOrAdd<NpcCorporation>(CorporationId, () => {
              NpcCorporationEntity corporationEntity = Entity.Corporation;
              Contract.Assume(corporationEntity != null);

              return new NpcCorporation(corporationEntity);
            });
          }
        }

        return _corporation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the faction's main corporation.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporation's main corporation.
    /// </value>
    public NpcCorporationId CorporationId {
      get {
        return (NpcCorporationId) Entity.CorporationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon {
      get {
        if (_icon == null) {

          // Load the cached version if available
          _icon = Eve.General.Cache.GetOrAdd<Icon>(IconId, () => {
            IconEntity iconEntity = Entity.Icon;
            Contract.Assume(iconEntity != null);

            return new Icon(iconEntity);
          });
        }

        return _icon;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId IconId {
      get {
        return Entity.IconId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the faction's militia corporation, if any.
    /// </summary>
    /// 
    /// <value>
    /// The corporation's militia corporation, or <see langword="null" />
    /// if the faction doesn't have a militia corporation.
    /// </value>
    public NpcCorporation MilitiaCorporation {
      get {
        if (_militiaCorporation == null) {
          if (MilitiaCorporationId != null) {

            // Load the cached version if available
            _militiaCorporation = Eve.General.Cache.GetOrAdd<NpcCorporation>(MilitiaCorporationId, () => {
              NpcCorporationEntity corporationEntity = Entity.Corporation;
              Contract.Assume(corporationEntity != null);

              return new NpcCorporation(corporationEntity);
            });
          }
        }

        return _militiaCorporation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the faction's militia corporation, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporation's militia corporation, or <see langword="null" />
    /// if the faction doesn't have a militia corporation.
    /// </value>
    public NpcCorporationId? MilitiaCorporationId {
      get {
        return (NpcCorporationId?) Entity.MilitiaCorporationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the faction, if any.
    /// </summary>
    /// 
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current faction is associated with, or <see cref="null" /> if the
    /// faction is not associated with any races.
    /// </value>
    RaceId RaceId {
      get {
        return (RaceId) Entity.RaceIds;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a numeric value indicating the size of the faction.  The value of
    /// this property is not completely understood.
    /// </summary>
    /// 
    /// <value>
    /// A numeric value indicating the size of the faction.
    /// </value>
    public double SizeFactor {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = Entity.SizeFactor;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the solar system containing the faction's capital.
    /// </summary>
    /// 
    /// <value>
    /// The solar system containing the faction's capital.
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
    /// Gets the ID of the solar system containing the faction's capital.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the solar system containing the faction's capital.
    /// </value>
    public SolarSystemId SolarSystemId {
      get {
        return (SolarSystemId) Entity.SolarSystemId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of stations operated by the faction.
    /// </summary>
    /// 
    /// <value>
    /// The number of stations operated by the faction.
    /// </value>
    public short StationCount {
      get {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = Entity.StationCount;

        Contract.Assume(result >= 0);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of solar systems containing stations operated by the
    /// faction.
    /// </summary>
    /// 
    /// <value>
    /// The number of solar systems containing stations operated by the
    /// faction.
    /// </value>
    public short StationSystemCount {
      get {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = Entity.StationSystemCount;

        Contract.Assume(result >= 0);

        return result;
      }
    }
    #endregion

    #region IHasIcon
    //******************************************************************************
    Icon IHasIcon.Icon {
      get {
        return Icon;
      }
    }
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get {
        return IconId;
      }
    }
    #endregion
    #region IHasRaces Members
    //******************************************************************************
    RaceId? IHasRaces.RaceId {
      get { return RaceId; }
    }
    #endregion
  }
}