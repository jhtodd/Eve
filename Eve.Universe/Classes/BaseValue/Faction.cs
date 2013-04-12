//-----------------------------------------------------------------------
// <copyright file="Faction.cs" company="Jeremy H. Todd">
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

  using Eve.Data.Entities;

  using FreeNet;

  /// <summary>
  /// Contains information about an EVE faction.
  /// </summary>
  public sealed partial class Faction 
    : BaseValue<FactionId, FactionId, FactionEntity, Faction>,
      IHasIcon,
      IHasRaces
  {
    private NpcCorporation corporation;
    private Icon icon;
    private NpcCorporation militiaCorporation;
    private SolarSystem solarSystem;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Faction class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Faction(FactionEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }

    /* Properties */

    /// <summary>
    /// Gets the faction's main corporation.
    /// </summary>
    /// <value>
    /// The faction's main corporation.
    /// </value>
    public NpcCorporation Corporation
    {
      get
      {
        if (this.CorporationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.corporation ?? (this.corporation = Eve.General.Cache.GetOrAdd<NpcCorporation>(this.CorporationId, () => (NpcCorporation)this.Entity.Corporation.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the faction's main corporation.
    /// </summary>
    /// <value>
    /// The ID of the corporation's main corporation.
    /// </value>
    public NpcCorporationId CorporationId
    {
      get { return (NpcCorporationId)Entity.CorporationId; }
    }

    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon
    {
      get
      {
        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.icon ?? (this.icon = Eve.General.Cache.GetOrAdd<Icon>(this.IconId, () => (Icon)this.Entity.Icon.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId IconId
    {
      get { return Entity.IconId; }
    }

    /// <summary>
    /// Gets the faction's militia corporation, if any.
    /// </summary>
    /// <value>
    /// The corporation's militia corporation, or <see langword="null" />
    /// if the faction doesn't have a militia corporation.
    /// </value>
    public NpcCorporation MilitiaCorporation
    {
      get
      {
        if (this.MilitiaCorporationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.militiaCorporation ?? (this.militiaCorporation = Eve.General.Cache.GetOrAdd<NpcCorporation>(this.MilitiaCorporationId, () => (NpcCorporation)this.Entity.MilitiaCorporation.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the faction's militia corporation, if any.
    /// </summary>
    /// <value>
    /// The ID of the corporation's militia corporation, or <see langword="null" />
    /// if the faction doesn't have a militia corporation.
    /// </value>
    public NpcCorporationId? MilitiaCorporationId
    {
      get { return (NpcCorporationId?)Entity.MilitiaCorporationId; }
    }

    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the faction, if any.
    /// </summary>
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current faction is associated with, or <see langword="null" /> if the
    /// faction is not associated with any races.
    /// </value>
    public RaceId RaceId
    {
      get { return (RaceId)Entity.RaceIds; }
    }

    /// <summary>
    /// Gets a numeric value indicating the size of the faction.  The value of
    /// this property is not completely understood.
    /// </summary>
    /// <value>
    /// A numeric value indicating the size of the faction.
    /// </value>
    public double SizeFactor
    {
      get
      {
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

    /// <summary>
    /// Gets the solar system containing the faction's capital.
    /// </summary>
    /// <value>
    /// The solar system containing the faction's capital.
    /// </value>
    public SolarSystem SolarSystem
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystem>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.solarSystem ?? (this.solarSystem = Eve.General.Cache.GetOrAdd<SolarSystem>(this.SolarSystemId, () => (SolarSystem)this.Entity.SolarSystem.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the solar system containing the faction's capital.
    /// </summary>
    /// <value>
    /// The ID of the solar system containing the faction's capital.
    /// </value>
    public SolarSystemId SolarSystemId
    {
      get { return (SolarSystemId)Entity.SolarSystemId; }
    }

    /// <summary>
    /// Gets the number of stations operated by the faction.
    /// </summary>
    /// <value>
    /// The number of stations operated by the faction.
    /// </value>
    public short StationCount
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = Entity.StationCount;

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the number of solar systems containing stations operated by the
    /// faction.
    /// </summary>
    /// <value>
    /// The number of solar systems containing stations operated by the
    /// faction.
    /// </value>
    public short StationSystemCount
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = Entity.StationSystemCount;

        Contract.Assume(result >= 0);

        return result;
      }
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class Faction : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return IconId; }
    }
  }
  #endregion

  #region IHasRaces Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasRaces" /> interface.
  /// </content>
  public partial class Faction : IHasRaces
  {
    RaceId? IHasRaces.RaceId
    {
      get { return RaceId; }
    }
  }
  #endregion
}