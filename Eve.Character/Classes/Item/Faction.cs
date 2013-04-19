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
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Faction(IEveRepository container, ItemEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsFaction, "The entity must be a faction.");
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
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.corporation,
          () => this.Container.GetOrAdd<NpcCorporation>(this.CorporationId, () => (NpcCorporation)this.FactionInfo.Corporation.ToAdapter(this.Container)));

        Contract.Assume(this.corporation != null);
        return this.corporation;
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
      get { return (NpcCorporationId)this.FactionInfo.CorporationId; }
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
        Contract.Ensures(Contract.Result<Icon>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.icon, 
          () => this.Container.GetOrAdd<Icon>(this.IconId, () => this.FactionInfo.Icon.ToAdapter(this.Container)));

        Contract.Assume(this.icon != null);
        return this.icon;
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
      get { return this.FactionInfo.IconId; }
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
        Contract.Ensures(this.MilitiaCorporationId == null || Contract.Result<NpcCorporation>() != null);

        if (this.MilitiaCorporationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.militiaCorporation,
          () => this.Container.GetOrAdd<NpcCorporation>(this.MilitiaCorporationId, () => (NpcCorporation)this.FactionInfo.MilitiaCorporation.ToAdapter(this.Container)));

        Contract.Assume(this.militiaCorporation != null);
        return this.militiaCorporation;
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
      get { return (NpcCorporationId?)this.FactionInfo.MilitiaCorporationId; }
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
      get { return (RaceId)this.FactionInfo.RaceIds; }
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

        var result = this.FactionInfo.SizeFactor;

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
        LazyInitializer.EnsureInitialized(
          ref this.solarSystem,
          () => this.Container.GetOrAdd<SolarSystem>(this.SolarSystemId, () => (SolarSystem)this.FactionInfo.SolarSystem.ToAdapter(this.Container)));

        Contract.Assume(this.solarSystem != null);
        return this.solarSystem;
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
      get { return (SolarSystemId)this.FactionInfo.SolarSystemId; }
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

        var result = this.FactionInfo.StationCount;

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

        var result = this.FactionInfo.StationSystemCount;

        Contract.Assume(result >= 0);

        return result;
      }
    }

    private FactionEntity FactionInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<FactionEntity>() != null);

        var result = this.Entity.FactionInfo;

        Contract.Assume(result != null);
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