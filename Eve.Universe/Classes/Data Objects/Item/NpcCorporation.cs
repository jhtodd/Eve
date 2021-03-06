﻿//-----------------------------------------------------------------------
// <copyright file="NpcCorporation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// An EVE item describing an NPC-controlled corporation.
  /// </summary>
  public sealed partial class NpcCorporation
    : Item,
      IDisposable,
      IHasIcon
  {
    private ReadOnlyAgentCollection agents;
    private ReadOnlyNpcCorporationDivisionCollection divisions;
    private NpcCorporation enemy;
    private Faction faction;
    private NpcCorporation friend;
    private Icon icon;
    private ReadOnlyNpcCorporationInvestorCollection investors;
    private ReadOnlySkillTypeCollection researchFields;
    private SolarSystem solarSystem;
    private ReadOnlyEveTypeCollection tradeGoods;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="NpcCorporation" /> class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal NpcCorporation(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of agents in the division.
    /// </summary>
    /// <value>
    /// The collection of agents in the division.
    /// </value>
    public ReadOnlyAgentCollection Agents
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAgentCollection>() != null);

        return NpcCorporation.LazyInitialize(
          ref this.agents,
          () => ReadOnlyAgentCollection.Create(this.Repository, this.CorporationInfo == null ? null : this.CorporationInfo.Agents));
      }
    }

    /// <summary>
    /// Gets the number of border systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// <value>
    /// The number of border systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Border
    {
      get
      {
        return this.CorporationInfo == null ? (byte)0 : this.CorporationInfo.Border; 
      }
    }

    /// <summary>
    /// Gets the number of corridor systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// <value>
    /// The number of corridor systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Corridor
    {
      get
      { 
        return this.CorporationInfo == null ? (byte)0 : this.CorporationInfo.Corridor; 
      }
    }

    /// <summary>
    /// Gets the description of the corporation.
    /// </summary>
    /// <value>
    /// The description of the corporation.
    /// </value>
    public string Description
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return this.CorporationInfo == null ? string.Empty : (this.CorporationInfo.Description ?? string.Empty);
      }
    }

    /// <summary>
    /// Gets the collection of divisions belonging to the corporation.
    /// </summary>
    /// <value>
    /// The collection of divisions belonging to the corporation.
    /// </value>
    public ReadOnlyNpcCorporationDivisionCollection Divisions
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyNpcCorporationDivisionCollection>() != null);

        return NpcCorporation.LazyInitialize(
          ref this.divisions,
          () => ReadOnlyNpcCorporationDivisionCollection.Create(this.Repository, this.CorporationInfo == null ? null : this.CorporationInfo.Divisions));
      }
    }

    /// <summary>
    /// Gets the corporation's principal enemy, if applicable.
    /// </summary>
    /// <value>
    /// The corporation's principal enemy, or <see langword="null" />
    /// if no such enemy exists.
    /// </value>
    public NpcCorporation Enemy
    {
      get
      {
        Contract.Ensures(this.EnemyId == null || Contract.Result<NpcCorporation>() != null);

        if (this.EnemyId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.enemy, this.CorporationInfo.EnemyId, () => this.CorporationInfo.Enemy);
      }
    }

    /// <summary>
    /// Gets the ID of the corporation's principal enemy, if applicable.
    /// </summary>
    /// <value>
    /// The ID of the corporation's principal enemy, or <see langword="null" />
    /// if no such enemy exists.
    /// </value>
    public NpcCorporationId? EnemyId
    {
      get 
      {
        Contract.Ensures(Contract.Result<NpcCorporationId?>() == null || this.CorporationInfo != null);
        return this.CorporationInfo == null ? (NpcCorporationId?)null : (NpcCorporationId?)this.CorporationInfo.EnemyId;
      }
    }

    /// <summary>
    /// Gets a value indicating the extent of the corporation (i.e. the size of
    /// the area in which it operates).
    /// </summary>
    /// <value>
    /// A <see cref="NpcCorporationExtent" /> value indicating the extent of the
    /// corporation.
    /// </value>
    public NpcCorporationExtent Extent
    {
      get
      {
        switch (this.ExtentCode)
        {
          case "G":
            return NpcCorporationExtent.Global;

          case "N":
            return NpcCorporationExtent.National;

          case "R":
            return NpcCorporationExtent.Regional;

          case "C":
            return NpcCorporationExtent.Constellation;

          case "L":
            return NpcCorporationExtent.Local;

          default:
            return NpcCorporationExtent.Unknown;
        }
      }
    }

    /// <summary>
    /// Gets the character code indicating the corporation's extent.  Provided
    /// for compatibility; see the <see cref="Extent" /> property for a 
    /// friendlier alternative.
    /// </summary>
    /// <value>
    /// The single-character code indicating the corporation's extent.
    /// </value>
    public string ExtentCode
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        string result = this.CorporationInfo == null ? string.Empty : this.CorporationInfo.Extent;

        Contract.Assume(result != null);

        return result;
      }
    }

    /// <summary>
    /// Gets the faction which holds sovereignty over the region, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Faction" /> which holds sovereignty over the region, or
    /// <see langword="null" /> if no faction information exists.
    /// </value>
    public Faction Faction
    {
      get
      {
        // Cant' do because of workaround below
        ////Contract.Ensures(this.FactionId == null || Contract.Result<Faction>() != null);

        if (this.FactionId == null)
        {
          return null;
        }

        // TODO: As of 84566, one corporation (Arkombine) has a faction ID
        // that has no corresponding entry in the chrFactions table -- it's
        // just a generic item.  This is a workaround.
        if (this.CorporationInfo == null || this.CorporationInfo.Faction == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.faction, this.CorporationInfo.FactionId, () => this.CorporationInfo.Faction);
      }
    }

    /// <summary>
    /// Gets the ID of the faction which holds sovereignty over the solar system,
    /// if any.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Faction" /> which holds sovereignty over the
    /// solar system, or <see langword="null" /> if no faction information exists.
    /// </value>
    public FactionId? FactionId
    {
      get 
      {
        Contract.Ensures(Contract.Result<FactionId?>() == null || this.CorporationInfo != null);
        return this.CorporationInfo == null ? (FactionId?)null : (FactionId?)this.CorporationInfo.FactionId;
      }
    }

    /// <summary>
    /// Gets the corporation's principal friend, if applicable.
    /// </summary>
    /// <value>
    /// The corporation's principal friend, or <see langword="null" />
    /// if no such friend exists.
    /// </value>
    public NpcCorporation Friend
    {
      get
      {
        Contract.Ensures(this.FriendId == null || Contract.Result<NpcCorporation>() != null);

        if (this.FriendId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.friend, this.CorporationInfo.FriendId, () => this.CorporationInfo.Friend);
      }
    }

    /// <summary>
    /// Gets the ID of the corporation's principal friend, if applicable.
    /// </summary>
    /// <value>
    /// The ID of the corporation's principal friend, or <see langword="null" />
    /// if no such friend exists.
    /// </value>
    public NpcCorporationId? FriendId
    {
      get 
      {
        Contract.Ensures(Contract.Result<NpcCorporationId?>() == null || this.CorporationInfo != null);
        return this.CorporationInfo == null ? (NpcCorporationId?)null : (NpcCorporationId?)this.CorporationInfo.FriendId;
      }
    }

    /// <summary>
    /// Gets the number of fringe systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// <value>
    /// The number of fringe systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Fringe
    {
      get
      { 
        return this.CorporationInfo == null ? (byte)0 : this.CorporationInfo.Fringe; 
      }
    }

    /// <summary>
    /// Gets the number of hub systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// <value>
    /// The number of hub systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Hub
    {
      get { return this.CorporationInfo == null ? (byte)0 : this.CorporationInfo.Hub; }
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
        return this.LazyInitializeAdapter(ref this.icon, this.CorporationInfo.IconId, () => this.CorporationInfo.Icon);
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
        Contract.Ensures(Contract.Result<IconId?>() == null || this.CorporationInfo != null);
        return this.CorporationInfo == null ? (IconId?)null : (IconId?)this.CorporationInfo.IconId;
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new NpcCorporationId Id
    {
      get { return (NpcCorporationId)base.Id.Value; }
    }

    /// <summary>
    /// Gets the initial price of public shares.  This is not implemented in-game.
    /// </summary>
    /// <value>
    /// The initial price of public shares.
    /// </value>
    public int InitialPrice
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        int result = this.CorporationInfo == null ? 0 : this.CorporationInfo.InitialPrice;

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the collection of corporations which are invested in the current
    /// corporation.
    /// </summary>
    /// <value>
    /// The collection of corporations which are invested in the current
    /// corporation.
    /// </value>
    public ReadOnlyNpcCorporationInvestorCollection Investors
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyNpcCorporationInvestorCollection>() != null);

        return NpcCorporation.LazyInitialize(
          ref this.investors,
          () =>
          {
            if (this.CorporationInfo == null)
            {
              return ReadOnlyNpcCorporationInvestorCollection.Create(this.Repository, null);
            }

            List<NpcCorporationInvestor> items = new List<NpcCorporationInvestor>(4);

            if (this.CorporationInfo.InvestorId1 != null)
            {
              items.Add(new NpcCorporationInvestor(this.Repository, this.CorporationInfo.InvestorId1.Value, this.CorporationInfo.InvestorShares1));
            }

            if (this.CorporationInfo.InvestorId2 != null)
            {
              items.Add(new NpcCorporationInvestor(this.Repository, this.CorporationInfo.InvestorId2.Value, this.CorporationInfo.InvestorShares1));
            }

            if (this.CorporationInfo.InvestorId3 != null)
            {
              items.Add(new NpcCorporationInvestor(this.Repository, this.CorporationInfo.InvestorId3.Value, this.CorporationInfo.InvestorShares1));
            }

            if (this.CorporationInfo.InvestorId4 != null)
            {
              items.Add(new NpcCorporationInvestor(this.Repository, this.CorporationInfo.InvestorId4.Value, this.CorporationInfo.InvestorShares1));
            }

            return ReadOnlyNpcCorporationInvestorCollection.Create(this.Repository, items);
          });
      }
    }

    /// <summary>
    /// Gets the minimum security rating characters must possess to do business
    /// with the corporation.  This is not implemented in-game.
    /// </summary>
    /// <value>
    /// The minimum security rating characters must possess to do business
    /// with the corporation.
    /// </value>
    public double MinimumSecurity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result = this.CorporationInfo == null ? -10.0D : this.CorporationInfo.MinSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the number of public shares.  This is not implemented in-game.
    /// </summary>
    /// <value>
    /// The number of public shares.
    /// </value>
    public long PublicShares
    {
      get
      {
        Contract.Ensures(Contract.Result<long>() >= 0L);

        long result = this.CorporationInfo == null ? 0L : this.CorporationInfo.PublicShares;

        Contract.Assume(result >= 0L);

        return result;
      }
    }

    /// <summary>
    /// Gets the collection of research fields offered by agents of the corporation.
    /// </summary>
    /// <value>
    /// The collection of research fields.
    /// </value>
    public ReadOnlySkillTypeCollection ResearchFields
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySkillTypeCollection>() != null);

        return NpcCorporation.LazyInitialize(
          ref this.researchFields,
          () => ReadOnlySkillTypeCollection.Create(this.Repository, this.CorporationInfo == null ? null : this.CorporationInfo.ResearchFields));
      }
    }

    /// <summary>
    /// Gets a value indicating whether the corporation's stations are scattered
    /// across the game universe.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the corporation's stations are scattered
    /// across the game universe; otherwise <see langword="false" />.
    /// </value>
    public bool Scattered
    {
      get 
      {
        return this.CorporationInfo == null ? false : this.CorporationInfo.Scattered;
      }
    }

    /// <summary>
    /// Gets a value indicating the size of the corporation.
    /// </summary>
    /// <value>
    /// A <see cref="NpcCorporationSize" /> value indicating the size of the
    /// corporation.
    /// </value>
    public NpcCorporationSize Size
    {
      get
      {
        switch (this.SizeCode)
        {
          case "H":
            return NpcCorporationSize.Huge;

          case "L":
            return NpcCorporationSize.Large;

          case "M":
            return NpcCorporationSize.Medium;

          case "S":
            return NpcCorporationSize.Small;

          case "T":
            return NpcCorporationSize.Tiny;

          default:
            return NpcCorporationSize.Unknown;
        }
      }
    }

    /// <summary>
    /// Gets the character code indicating the corporation's size.  Provided
    /// for compatibility; see the <see cref="Size" /> property for a 
    /// friendlier alternative.
    /// </summary>
    /// <value>
    /// The single-character code indicating the corporation's size.
    /// </value>
    public string SizeCode
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        string result = this.CorporationInfo == null ? string.Empty : this.CorporationInfo.Size;

        Contract.Assume(result != null);

        return result;
      }
    }

    /// <summary>
    /// Gets a numeric value indicating the size of the corporation.  The value of
    /// this property is not completely understood.
    /// </summary>
    /// <value>
    /// A numeric value indicating the size of the corporation.
    /// </value>
    public double SizeFactor
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.CorporationInfo == null ? 0.0D : (this.CorporationInfo.SizeFactor.HasValue ? this.CorporationInfo.SizeFactor.Value : 0.0D);

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the solar system containing the corporation's headquarters.
    /// </summary>
    /// <value>
    /// The solar system containing the corporation's headquarters, or
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
        return this.LazyInitializeAdapter(ref this.solarSystem, this.CorporationInfo.SolarSystemId, () => this.CorporationInfo.SolarSystem);
      }
    }

    /// <summary>
    /// Gets the ID of the solar system containing the corporation's headquarters.
    /// </summary>
    /// <value>
    /// The ID of the solar system containing the corporation's headquarters, or
    /// <see langword="null" /> if no solar system information exists.
    /// </value>
    public SolarSystemId? SolarSystemId
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystemId?>() == null || this.CorporationInfo != null);
        return this.CorporationInfo == null ? (SolarSystemId?)null : (SolarSystemId?)this.CorporationInfo.SolarSystemId;
      }
    }

    /// <summary>
    /// Gets the number of stations operated by the corporation.
    /// </summary>
    /// <value>
    /// The number of stations operated by the corporation.
    /// </value>
    public short StationCount
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() >= 0);

        short result = this.CorporationInfo == null ? (short)0 : (this.CorporationInfo.StationCount.HasValue ? this.CorporationInfo.StationCount.Value : (short)0);

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the number of solar systems containing stations operated by the
    /// corporation.
    /// </summary>
    /// <value>
    /// The number of solar systems containing stations operated by the
    /// corporation.
    /// </value>
    public short StationSystemCount
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = this.CorporationInfo == null ? (short)0 : (this.CorporationInfo.StationSystemCount.HasValue ? this.CorporationInfo.StationSystemCount.Value : (short)0);

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the collection of items that the corporation buys or sells.
    /// </summary>
    /// <value>
    /// The collection of items that the corporation buys or sells.
    /// </value>
    public ReadOnlyEveTypeCollection TradeGoods
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyEveTypeCollection>() != null);

        return NpcCorporation.LazyInitialize(
          ref this.tradeGoods,
          () => ReadOnlyEveTypeCollection.Create(this.Repository, this.CorporationInfo == null ? null : this.CorporationInfo.TradeGoods));
      }
    }

    /// <summary>
    /// Gets the <see cref="NpcCorporationEntity" /> containing the specific
    /// information for the current item type.
    /// </summary>
    /// <value>
    /// The <see cref="NpcCorporationEntity" /> containing the specific
    /// information for the current item type.
    /// </value>
    private NpcCorporationEntity CorporationInfo
    {
      get { return this.Entity.CorporationInfo; }
    }

    /* Methods */

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    /// <param name="disposing">Specifies whether to dispose managed resources.</param>
    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.agents != null)
        {
          this.agents.Dispose();
        }

        if (this.divisions != null)
        {
          this.divisions.Dispose();
        }

        if (this.investors != null)
        {
          this.investors.Dispose();
        }

        if (this.researchFields != null)
        {
          this.researchFields.Dispose();
        }

        if (this.tradeGoods != null)
        {
          this.tradeGoods.Dispose();
        }
      }
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class NpcCorporation : IHasIcon
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
}