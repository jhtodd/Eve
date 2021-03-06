﻿//-----------------------------------------------------------------------
// <copyright file="Faction.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  /// <summary>
  /// Contains information about an EVE faction.
  /// </summary>
  public sealed partial class Faction 
    : Item,
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
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Faction(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsFaction, "The entity must be a faction.");
    }

    /* Properties */

    /// <summary>
    /// Gets the faction's main corporation.
    /// </summary>
    /// <value>
    /// The faction's main corporation, or <see langword="null" />
    /// if no corporation information exists.
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
        return this.LazyInitializeAdapter(ref this.corporation, this.FactionInfo.CorporationId, () => this.FactionInfo.Corporation);
      }
    }

    /// <summary>
    /// Gets the ID of the faction's main corporation.
    /// </summary>
    /// <value>
    /// The ID of the corporation's main corporation, or
    /// <see langword="null" /> if no corporation information exists..
    /// </value>
    public NpcCorporationId? CorporationId
    {
      get 
      {
        Contract.Ensures(Contract.Result<NpcCorporationId?>() == null || this.FactionInfo != null);
        return this.FactionInfo == null ? (NpcCorporationId?)null : this.FactionInfo.CorporationId;
      }
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
        Contract.Ensures(this.IconId == null || Contract.Result<Icon>() != null);

        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.icon, this.FactionInfo.IconId, () => this.FactionInfo.Icon);
      }
    }

    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId
    {
      get 
      {
        Contract.Ensures(Contract.Result<IconId?>() == null || this.FactionInfo != null);
        return this.FactionInfo == null ? (IconId?)null : this.FactionInfo.IconId;
      }
    }

    /// <summary>
    /// Gets the faction's militia corporation, if any.
    /// </summary>
    /// <value>
    /// The corporation's militia corporation, or
    /// <see langword="null" /> if no militia corporation information exists.
    /// </value>
    public NpcCorporation MilitiaCorporation
    {
      get
      {
        Contract.Ensures(this.MilitiaCorporationId == null || Contract.Result<NpcCorporation>() != null);

        if (this.MilitiaCorporationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.militiaCorporation, this.FactionInfo.MilitiaCorporationId, () => this.FactionInfo.MilitiaCorporation);
      }
    }

    /// <summary>
    /// Gets the ID of the faction's militia corporation, if any.
    /// </summary>
    /// <value>
    /// The ID of the corporation's militia corporation, or
    /// <see langword="null" /> if no militia corporation information exists.
    /// </value>
    public NpcCorporationId? MilitiaCorporationId
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporationId?>() == null || this.FactionInfo != null);
        return this.FactionInfo == null ? (NpcCorporationId?)null : (NpcCorporationId?)this.FactionInfo.MilitiaCorporationId;
      }
    }

    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the faction, if any.
    /// </summary>
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current faction is associated with, or <see langword="null" /> if the
    /// faction is not associated with any races.
    /// </value>
    public RaceId? RaceIds
    {
      get 
      {
        // TODO: Replace with race array
        return this.FactionInfo == null ? (RaceId?)null : (RaceId?)this.FactionInfo.RaceIds;
      }
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

        var result = this.FactionInfo == null ? 0.0D : this.FactionInfo.SizeFactor;

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
    /// The solar system containing the faction's capital, or
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
        return this.LazyInitializeAdapter(ref this.solarSystem, this.FactionInfo.SolarSystemId, () => this.FactionInfo.SolarSystem);
      }
    }

    /// <summary>
    /// Gets the ID of the solar system containing the faction's capital.
    /// </summary>
    /// <value>
    /// The ID of the solar system containing the faction's capital, or
    /// <see langword="null" /> if no solar system information exists.
    /// </value>
    public SolarSystemId? SolarSystemId
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystemId?>() == null || this.FactionInfo != null);
        return this.FactionInfo == null ? (SolarSystemId?)null : this.FactionInfo.SolarSystemId;
      }
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

        var result = this.FactionInfo == null ? (short)0 : this.FactionInfo.StationCount;

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

        var result = this.FactionInfo == null ? (short)0 : this.FactionInfo.StationSystemCount;

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the <see cref="FactionEntity" /> containing the specific
    /// information for the current item type.
    /// </summary>
    /// <value>
    /// The <see cref="FactionEntity" /> containing the specific
    /// information for the current item type.
    /// </value>
    private FactionEntity FactionInfo
    {
      get { return this.Entity.FactionInfo; }
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
      get { return this.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.IconId; }
    }
  }
  #endregion

  #region IHasRaces Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasRaces" /> interface.
  /// </content>
  public partial class Faction : IHasRaces
  {
    RaceId? IHasRaces.RaceIds
    {
      get { return this.RaceIds; }
    }
  }
  #endregion
}