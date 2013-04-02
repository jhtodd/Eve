//-----------------------------------------------------------------------
// <copyright file="NpcCorporation.cs" company="Jeremy H. Todd">
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

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// An EVE item describing an NPC-controlled corporation.
  /// </summary>
  public class NpcCorporation : Item,
                                IHasIcon {

    #region Instance Fields
    private ReadOnlyAgentCollection _agents;
    private ReadOnlyNpcCorporationDivisionCollection _divisions;
    private NpcCorporation _enemy;
    private Faction _faction;
    private NpcCorporation _friend;
    private Icon _icon;
    private ReadOnlyNpcCorporationInvestorCollection _investors;
    private ReadOnlySkillTypeCollection _researchFields;
    private SolarSystem _solarSystem;
    private ReadOnlyTypeCollection _tradeGoods;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the NpcCorporation class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal NpcCorporation(NpcCorporationEntity entity) : base(entity) {
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
    /// Gets the collection of agents in the division.
    /// </summary>
    /// 
    /// <value>
    /// The collection of agents in the division.
    /// </value>
    public ReadOnlyAgentCollection Agents {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyAgentCollection>() != null);

        if (_agents == null) {
          _agents = new ReadOnlyAgentCollection(Eve.General.DataSource.GetAgents(x => x.CorporationId == this.Id.Value).OrderBy(x => x));
        }

        return _agents;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of border systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// 
    /// <value>
    /// The number of border systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Border {
      get {
        return Entity.Border;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of corridor systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// 
    /// <value>
    /// The number of corridor systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Corridor {
      get {
        return Entity.Corridor;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the description of the corporation.
    /// </summary>
    /// 
    /// <value>
    /// The description of the corporation.
    /// </value>
    public string Description {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.Description ?? string.Empty;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of divisions belonging to the corporation.
    /// </summary>
    /// 
    /// <value>
    /// The collection of divisions belonging to the corporation.
    /// </value>
    public ReadOnlyNpcCorporationDivisionCollection Divisions {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyNpcCorporationDivisionCollection>() != null);

        if (_divisions == null) {
          _divisions = new ReadOnlyNpcCorporationDivisionCollection(Eve.General.DataSource.GetNpcCorporationDivisions(x => x.CorporationId == this.Id.Value).OrderBy(x => x));
        }

        return _divisions;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the corporation's principal enemy, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The corporation's principal enemy, or <see langword="null" />
    /// if no such enemy exists.
    /// </value>
    public NpcCorporation Enemy {
      get {
        if (_enemy == null) {
          if (EnemyId != null) {

            // Load the cached version if available
            _enemy = Eve.General.Cache.GetOrAdd<NpcCorporation>(EnemyId, () => {
              NpcCorporationEntity corporationEntity = Entity.Enemy;
              Contract.Assume(corporationEntity != null);

              return new NpcCorporation(corporationEntity);
            });
          }
        }

        return _enemy;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the corporation's principal enemy, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporation's principal enemy, or <see langword="null" />
    /// if no such enemy exists.
    /// </value>
    public NpcCorporationId? EnemyId {
      get {
        return (NpcCorporationId?) Entity.EnemyId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating the extent of the corporation (i.e. the size of
    /// the area in which it operates).
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="NpcCorporationExtent" /> value indicating the extent of the
    /// corporation.
    /// </value>
    public NpcCorporationExtent Extent {
      get {
        switch (ExtentCode) {
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
            throw new InvalidOperationException("No NpcCorporationExtent member corresponding to value \"" + ExtentCode + "\" exists.");
        }
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the character code indicating the corporation's extent.  Provided
    /// for compatibility; see the <see cref="Extent" /> property for a 
    /// friendlier alternative.
    /// </summary>
    /// 
    /// <value>
    /// The single-character code indicating the corporation's extent.
    /// </value>
    public string ExtentCode {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        Contract.Ensures(Contract.Result<string>().Length == 1);

        string result = Entity.Extent;

        Contract.Assume(!string.IsNullOrWhiteSpace(result));
        Contract.Assume(result.Length == 1);

        return result;
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


          // Load the cached version if available
          _faction = Eve.General.Cache.GetOrAdd<Faction>(FactionId, () => {
            FactionEntity factionEntity = Entity.Faction;
            Contract.Assume(factionEntity != null);

            return new Faction(factionEntity);
          });
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
    public FactionId FactionId {
      get {
        return Entity.FactionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the corporation's principal friend, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The corporation's principal friend, or <see langword="null" />
    /// if no such friend exists.
    /// </value>
    public NpcCorporation Friend {
      get {
        if (_friend == null) {
          if (FriendId != null) {

            // Load the cached version if available
            _friend = Eve.General.Cache.GetOrAdd<NpcCorporation>(FriendId, () => {
              NpcCorporationEntity corporationEntity = Entity.Friend;
              Contract.Assume(corporationEntity != null);

              return new NpcCorporation(corporationEntity);
            });
          }
        }

        return _friend;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the corporation's principal friend, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporation's principal friend, or <see langword="null" />
    /// if no such friend exists.
    /// </value>
    public NpcCorporationId? FriendId {
      get {
        return (NpcCorporationId?) Entity.FriendId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of fringe systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// 
    /// <value>
    /// The number of fringe systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Fringe {
      get {
        return Entity.Fringe;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of hub systems that contain one of the
    /// corporation's stations.
    /// </summary>
    /// 
    /// <value>
    /// The number of hub systems that contain one of the
    /// corporation's stations.
    /// </value>
    public byte Hub {
      get {
        return Entity.Hub;
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
        Contract.Ensures(Contract.Result<Icon>() != null);

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
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new NpcCorporationId Id {
      get {
        return (NpcCorporationId) base.Id.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// The initial price of public shares.  This is not implemented in-game.
    /// </summary>
    /// 
    /// <value>
    /// The initial price of public shares.
    /// </value>
    public int InitialPrice {
      get {
        Contract.Ensures(Contract.Result<int>() >= 0);

        int result = Entity.InitialPrice;

        Contract.Assume(result >= 0);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of corporations which are invested in the current
    /// corporation.
    /// </summary>
    /// 
    /// <value>
    /// The collection of corporations which are invested in the current
    /// corporation.
    /// </value>
    public ReadOnlyNpcCorporationInvestorCollection Investors {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyNpcCorporationInvestorCollection>() != null);

        if (_investors == null) {
          List<NpcCorporationInvestor> items = new List<NpcCorporationInvestor>();

          if (Entity.InvestorId1 != null) {
            items.Add(new NpcCorporationInvestor(Entity.InvestorId1.Value, Entity.InvestorShares1));
          }

          if (Entity.InvestorId2 != null) {
            items.Add(new NpcCorporationInvestor(Entity.InvestorId2.Value, Entity.InvestorShares1));
          }

          if (Entity.InvestorId3 != null) {
            items.Add(new NpcCorporationInvestor(Entity.InvestorId3.Value, Entity.InvestorShares1));
          }

          if (Entity.InvestorId4 != null) {
            items.Add(new NpcCorporationInvestor(Entity.InvestorId4.Value, Entity.InvestorShares1));
          }

          _investors = new ReadOnlyNpcCorporationInvestorCollection(items.ToArray());
        }

        return _investors;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the minimum security rating characters must possess to do business
    /// with the corporation.  This is not implemented in-game.
    /// </summary>
    /// 
    /// <value>
    /// The minimum security rating characters must possess to do business
    /// with the corporation.
    /// </value>
    public double MinimumSecurity {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result = Entity.MinSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// The number of public shares.  This is not implemented in-game.
    /// </summary>
    /// 
    /// <value>
    /// The number of public shares.
    /// </value>
    public long PublicShares {
      get {
        Contract.Ensures(Contract.Result<long>() >= 0L);

        long result = Entity.PublicShares;

        Contract.Assume(result >= 0L);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of research fields offered by agents of the corporation.
    /// </summary>
    /// 
    /// <value>
    /// The collection of research fields.
    /// </value>
    public ReadOnlySkillTypeCollection ResearchFields {
      get {
        Contract.Ensures(Contract.Result<ReadOnlySkillTypeCollection>() != null);

        if (_researchFields == null) {

          // Filter through the cache
          Contract.Assume(Entity.ResearchFields != null);

          _researchFields = new ReadOnlySkillTypeCollection(
            Entity.ResearchFields.Select(x => Eve.General.Cache.GetOrAdd<SkillType>(x.Id, () => (SkillType) EveType.Create(x)))
                                        .OrderBy(x => x)
          );
        }

        return _researchFields;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the corporation's stations are scattered
    /// across the game universe.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the corporation's stations are scattered
    /// across the game universe; otherwise <see langword="false" />.
    /// </value>
    public bool Scattered {
      get {
        return Entity.Scattered;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating the size of the corporation.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="NpcCorporationSize" /> value indicating the size of the
    /// corporation.
    /// </value>
    public NpcCorporationSize Size {
      get {
        switch (SizeCode) {
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
            throw new InvalidOperationException("No NpcCorporationSize member corresponding to value \"" + SizeCode + "\" exists.");
        }
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the character code indicating the corporation's size.  Provided
    /// for compatibility; see the <see cref="Size" /> property for a 
    /// friendlier alternative.
    /// </summary>
    /// 
    /// <value>
    /// The single-character code indicating the corporation's size.
    /// </value>
    public string SizeCode {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        Contract.Ensures(Contract.Result<string>().Length == 1);

        string result = Entity.Size;

        Contract.Assume(!string.IsNullOrWhiteSpace(result));
        Contract.Assume(result.Length == 1);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a numeric value indicating the size of the corporation.  The value of
    /// this property is not completely understood.
    /// </summary>
    /// 
    /// <value>
    /// A numeric value indicating the size of the corporation.
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
    /// Gets the solar system containing the corporation's headquarters.
    /// </summary>
    /// 
    /// <value>
    /// The solar system containing the corporation's headquarters.
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
    /// Gets the ID of the solar system containing the corporation's headquarters.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the solar system containing the corporation's headquarters.
    /// </value>
    public SolarSystemId SolarSystemId {
      get {
        return (SolarSystemId) Entity.SolarSystemId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of stations operated by the corporation.
    /// </summary>
    /// 
    /// <value>
    /// The number of stations operated by the corporation.
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
    /// corporation.
    /// </summary>
    /// 
    /// <value>
    /// The number of solar systems containing stations operated by the
    /// corporation.
    /// </value>
    public short StationSystemCount {
      get {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = Entity.StationSystemCount;

        Contract.Assume(result >= 0);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of items that the corporation buys or sells.
    /// </summary>
    /// 
    /// <value>
    /// The collection of items that the corporation buys or sells.
    /// </value>
    public ReadOnlyTypeCollection TradeGoods {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyTypeCollection>() != null);

        if (_tradeGoods == null) {

          // Filter through the cache
          Contract.Assume(Entity.TradeGoods != null);

          _tradeGoods = new ReadOnlyTypeCollection(
            Entity.TradeGoods.Select(x => Eve.General.Cache.GetOrAdd<EveType>(x.Id, () => EveType.Create(x)))
                                    .OrderBy(x => x)
          );
        }

        return _tradeGoods;
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
    protected new NpcCorporationEntity Entity {
      get {
        Contract.Ensures(Contract.Result<NpcCorporationEntity>() != null);

        return (NpcCorporationEntity) base.Entity;
      }
    }
    #endregion

    #region IHasIcon Members
    //******************************************************************************
    Icon IHasIcon.Icon {
      get { return Icon; }
    }
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get { return IconId; }
    }
    #endregion
  }
}