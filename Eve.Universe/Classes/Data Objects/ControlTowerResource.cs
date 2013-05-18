//-----------------------------------------------------------------------
// <copyright file="ControlTowerResource.cs" company="Jeremy H. Todd">
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
  /// Contains information about a resource required by a control
  /// tower in order to operate.
  /// </summary>
  public sealed partial class ControlTowerResource
    : EveEntityAdapter<ControlTowerResourceEntity, ControlTowerResource>,
      IHasIcon,
      IKeyItem<long>
  {
    private EveType controlTowerType; // TODO: Change to ControlTowerType
    private EveType resourceType;
    private Faction faction;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ControlTowerResource class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal ControlTowerResource(IEveRepository repository, ControlTowerResourceEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the type of the control tower that requires the resource.
    /// </summary>
    /// <value>
    /// The type of the control tower that requires the resource.
    /// </value>
    public EveType ControlTowerType // TODO: Change to ControlTowerType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        return this.LazyInitializeAdapter(ref this.controlTowerType, this.Entity.ControlTowerTypeId, () => this.Entity.ControlTowerType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of the control tower that requires the resource.
    /// </summary>
    /// <value>
    /// The ID of the type of the control tower that requires the resource.
    /// </value>
    public EveTypeId ControlTowerTypeId
    {
      get { return (EveTypeId)this.Entity.ControlTowerTypeId; }
    }

    /// <summary>
    /// Gets the <see cref="Faction" /> that determines whether the resource
    /// is required.  This is for things like starbase charters in empire space.
    /// </summary>
    /// <value>
    /// The <see cref="Faction" /> that determines whether the resource
    /// is required.
    /// </value>
    public Faction Faction
    {
      get
      {
        if (this.FactionId == null)
        {
          return null;
        }

        return this.LazyInitializeAdapter(ref this.faction, this.Entity.FactionId, () => this.Entity.Faction);
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Faction" /> in which the item is considered contraband.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Faction" /> in which the item is considered contraband.
    /// </value>
    public FactionId? FactionId
    {
      get { return (FactionId?)this.Entity.FactionId; }
    }

    /// <summary>
    /// Gets the minimum security level at which the requirement
    /// applies, or <see cref="Double.NegativeInfinity" /> if it
    /// applies at all security levels.  This is for things like
    /// starbase charters for starbases in empire space.
    /// </summary>
    /// <value>
    /// The minimum security level at which the requirement applies,
    /// or <see cref="Double.NegativeInfinity" /> if it applies
    /// at all security levels.
    /// </value>
    public double MinimumSecurityLevel
    {
      get
      {
        Contract.Ensures(!double.IsPositiveInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.MinimumSecurityLevel.HasValue ? this.Entity.MinimumSecurityLevel.Value : double.NegativeInfinity;

        Contract.Assume(!double.IsPositiveInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the purpose for which the resource is required.
    /// </summary>
    /// <value>
    /// A <see cref="ControlTowerResourcePurpose" /> indicating the
    /// purpose for which the resource is required.
    /// </value>
    public ControlTowerResourcePurpose Purpose
    {
      get { return this.Entity.Purpose; }
    }

    /// <summary>
    /// Gets the amount of the resource required to operate the tower
    /// for one hour.
    /// </summary>
    /// <value>
    /// The required amount of the resource.
    /// </value>
    public int Quantity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);

        var result = this.Entity.Quantity;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the type of the required resource.
    /// </summary>
    /// <value>
    /// The type of the required resource.
    /// </value>
    public EveType ResourceType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        return this.LazyInitializeAdapter(ref this.resourceType, this.Entity.ResourceTypeId, () => this.Entity.ResourceType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of the required resource.
    /// </summary>
    /// <value>
    /// The ID of the type of the required resource.
    /// </value>
    public EveTypeId ResourceTypeId
    {
      get { return (EveTypeId)this.Entity.ResourceTypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(ControlTowerResource other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.ResourceType.Name.CompareTo(other.ResourceType.Name);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.ResourceType.Name;
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class ControlTowerResource : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.ResourceType.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.ResourceType.IconId; }
    }
  }
  #endregion

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class ControlTowerResource : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return ControlTowerResourceEntity.CreateCacheKey(this.ResourceTypeId, this.ControlTowerTypeId); }
    }
  }
  #endregion
}