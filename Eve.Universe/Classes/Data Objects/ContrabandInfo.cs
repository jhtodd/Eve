//-----------------------------------------------------------------------
// <copyright file="ContrabandInfo.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about a jump link between constellations.
  /// </summary>
  public sealed partial class ContrabandInfo
    : EveEntityAdapter<ContrabandInfoEntity, ContrabandInfo>,
      IKeyItem<long>
  {
    private Faction faction;
    private EveType type;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ContrabandInfo class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal ContrabandInfo(IEveRepository repository, ContrabandInfoEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the minimum solar system security level above which faction
    /// police will attack if they detect contraband.
    /// </summary>
    /// <value>
    /// The minimum solar system security level above which faction
    /// police will attack if they detect contraband.
    /// </value>
    public double AttackMinimumSecurity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.AttackMinimumSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the minimum solar system security level above which faction
    /// police will confiscate contraband.
    /// </summary>
    /// <value>
    /// The minimum solar system security level above which faction
    /// police will confiscate contraband.
    /// </value>
    public double ConfiscateMinimumSecurity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.ConfiscateMinimumSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the <see cref="Faction" /> in which the item is considered contraband.
    /// </summary>
    /// <value>
    /// The <see cref="Faction" /> in which the item is considered contraband.
    /// </value>
    public Faction Faction
    {
      get
      {
        Contract.Ensures(Contract.Result<Faction>() != null);

        return this.LazyInitializeAdapter(ref this.faction, this.Entity.FactionId, () => this.Entity.Faction);
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Faction" /> in which the item is considered contraband.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Faction" /> in which the item is considered contraband.
    /// </value>
    public FactionId FactionId
    {
      get { return (FactionId)this.Entity.FactionId; }
    }

    /// <summary>
    /// Gets the fine assessed by faction police when they detect the contraband item.
    /// </summary>
    /// <value>
    /// The fine assessed by faction police when they detect the contraband item.
    /// </value>
    public double FineByValue
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.FineByValue;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the standing loss incurred when faction police detect the contraband item.
    /// </summary>
    /// <value>
    /// The standing loss incurred when faction police detect the contraband item.
    /// </value>
    public double StandingLoss
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.StandingLoss;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the type of the contraband item.
    /// </summary>
    /// <value>
    /// The type of the contraband item.
    /// </value>
    public EveType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        return this.LazyInitializeAdapter(ref this.type, this.Entity.TypeId, () => this.Entity.Type);
      }
    }

    /// <summary>
    /// Gets the ID of the type of the contraband item.
    /// </summary>
    /// <value>
    /// The ID of the type of the contraband item.
    /// </value>
    public EveTypeId TypeId
    {
      get { return this.Entity.TypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(ContrabandInfo other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Type.Name.CompareTo(other.Type.Name);

      if (result == 0)
      {
        result = this.Faction.Name.CompareTo(other.Faction.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Type.Name + " (" + this.Faction.Name + ")";
    }
  }

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class ContrabandInfo : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return ContrabandInfoEntity.CreateCacheKey((long)this.FactionId, this.TypeId); }
    }
  }
  #endregion
}