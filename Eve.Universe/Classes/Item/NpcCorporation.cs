//-----------------------------------------------------------------------
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
    private ReadOnlyTypeCollection tradeGoods;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="NpcCorporation" /> class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal NpcCorporation(IEveRepository container, NpcCorporationEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
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

        return this.agents ?? (this.agents = new ReadOnlyAgentCollection(this.Container.GetAgents(x => x.CorporationId == this.Id.Value).OrderBy(x => x)));
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
      get { return this.Entity.Border; }
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
      get { return this.Entity.Corridor; }
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

        return this.Entity.Description ?? string.Empty;
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

        return this.divisions ?? (this.divisions = new ReadOnlyNpcCorporationDivisionCollection(this.Container.GetNpcCorporationDivisions(x => x.CorporationId == this.Id.Value).OrderBy(x => x)));
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
        if (this.EnemyId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.enemy ?? (this.enemy = this.Container.GetOrAdd<NpcCorporation>(this.EnemyId, () => this.Entity.Enemy.ToAdapter(this.Container)));
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
      get { return (NpcCorporationId?)this.Entity.EnemyId; }
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
            throw new InvalidOperationException("No NpcCorporationExtent member corresponding to value \"" + this.ExtentCode + "\" exists.");
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
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        Contract.Ensures(Contract.Result<string>().Length == 1);

        string result = this.Entity.Extent;

        Contract.Assume(!string.IsNullOrWhiteSpace(result));
        Contract.Assume(result.Length == 1);

        return result;
      }
    }

    /// <summary>
    /// Gets the faction which holds sovereignty over the region, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Faction" /> which holds sovereignty over the region, or
    /// <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public Faction Faction
    {
      get
      {
        // This property can sometimes be null even with a FactionId value,
        // because a small number of records have a FactionId pointing to an
        // "Unknown" item that is not a faction.  Return null in that case.
        if (this.Entity.Faction == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.faction ?? (this.faction = this.Container.GetOrAdd<Faction>(this.FactionId, () => this.Entity.Faction.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the faction which holds sovereignty over the solar system,
    /// if any.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Faction" /> which holds sovereignty over the
    /// solar system, or <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public FactionId FactionId
    {
      get { return this.Entity.FactionId; }
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
        if (this.FriendId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.friend ?? (this.friend = this.Container.GetOrAdd<NpcCorporation>(this.FriendId, () => this.Entity.Friend.ToAdapter(this.Container)));
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
      get { return (NpcCorporationId?)this.Entity.FriendId; }
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
      get { return this.Entity.Fringe; }
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
      get { return this.Entity.Hub; }
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
        return this.icon ?? (this.icon = this.Container.GetOrAdd<Icon>(this.IconId, () => this.Entity.Icon.ToAdapter(this.Container)));
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
      get { return this.Entity.IconId; }
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

        int result = this.Entity.InitialPrice;

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

        if (this.investors == null)
        {
          List<NpcCorporationInvestor> items = new List<NpcCorporationInvestor>();

          if (this.Entity.InvestorId1 != null)
          {
            items.Add(new NpcCorporationInvestor(this.Container, this.Entity.InvestorId1.Value, this.Entity.InvestorShares1));
          }

          if (this.Entity.InvestorId2 != null)
          {
            items.Add(new NpcCorporationInvestor(this.Container, this.Entity.InvestorId2.Value, this.Entity.InvestorShares1));
          }

          if (this.Entity.InvestorId3 != null)
          {
            items.Add(new NpcCorporationInvestor(this.Container, this.Entity.InvestorId3.Value, this.Entity.InvestorShares1));
          }

          if (this.Entity.InvestorId4 != null)
          {
            items.Add(new NpcCorporationInvestor(this.Container, this.Entity.InvestorId4.Value, this.Entity.InvestorShares1));
          }

          this.investors = new ReadOnlyNpcCorporationInvestorCollection(items.ToArray());
        }

        return this.investors;
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

        double result = this.Entity.MinSecurity;

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

        long result = this.Entity.PublicShares;

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

        if (this.researchFields == null)
        {
          // Filter through the cache
          Contract.Assume(this.Entity.ResearchFields != null);

          this.researchFields = new ReadOnlySkillTypeCollection(
            this.Entity.ResearchFields.Select(x => this.Container.GetOrAdd<SkillType>(x.Id, () => (SkillType)x.ToAdapter(this.Container)))
                                      .OrderBy(x => x));
        }

        return this.researchFields;
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
      get { return this.Entity.Scattered; }
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
            throw new InvalidOperationException("No NpcCorporationSize member corresponding to value \"" + this.SizeCode + "\" exists.");
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
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        Contract.Ensures(Contract.Result<string>().Length == 1);

        string result = this.Entity.Size;

        Contract.Assume(!string.IsNullOrWhiteSpace(result));
        Contract.Assume(result.Length == 1);

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

        double result = this.Entity.SizeFactor.HasValue ? this.Entity.SizeFactor.Value : 0.0D;

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
    /// The solar system containing the corporation's headquarters.
    /// </value>
    public SolarSystem SolarSystem
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystem>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.solarSystem ?? (this.solarSystem = this.Container.GetOrAdd<SolarSystem>(this.SolarSystemId, () => this.Entity.SolarSystem.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the solar system containing the corporation's headquarters.
    /// </summary>
    /// <value>
    /// The ID of the solar system containing the corporation's headquarters.
    /// </value>
    public SolarSystemId SolarSystemId
    {
      get { return (SolarSystemId)this.Entity.SolarSystemId; }
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

        short result = this.Entity.StationCount.HasValue ? this.Entity.StationCount.Value : (short)0;

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

        var result = this.Entity.StationSystemCount.HasValue ? this.Entity.StationSystemCount.Value : (short)0;

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
    public ReadOnlyTypeCollection TradeGoods
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyTypeCollection>() != null);

        if (this.tradeGoods == null)
        {
          // Filter through the cache
          Contract.Assume(this.Entity.TradeGoods != null);

          this.tradeGoods = new ReadOnlyTypeCollection(
            this.Entity.TradeGoods.Select(x => this.Container.GetOrAdd<EveType>(x.Id, () => x.ToAdapter(this.Container)))
                                  .OrderBy(x => x));
        }

        return this.tradeGoods;
      }
    }

    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    private new NpcCorporationEntity Entity
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporationEntity>() != null);

        return (NpcCorporationEntity)base.Entity;
      }
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