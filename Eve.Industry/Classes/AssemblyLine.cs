//-----------------------------------------------------------------------
// <copyright file="AssemblyLine.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about an assembly line located at a station.
  /// </summary>
  public sealed partial class AssemblyLine 
    : EveEntityAdapter<AssemblyLineEntity>,
      IComparable<AssemblyLine>,
      IEquatable<AssemblyLine>,
      IEveCacheable,
      IKeyItem<AssemblyLineId>
  {
    private Activity activity;
    private AssemblyLineType assemblyLineType;
    private NpcCorporation owner;
    private Station station;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLine class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AssemblyLine(IEveRepository container, AssemblyLineEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the <see cref="Activity" /> performed by the assembly line.
    /// </summary>
    /// <value>
    /// The <see cref="Activity" /> performed by the assembly line.
    /// </value>
    public Activity Activity
    {
      get
      {
        Contract.Ensures(Contract.Result<Activity>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.activity,
          () => this.Container.GetOrAddStoredValue<Activity>(this.ActivityId, () => this.Entity.Activity.ToAdapter(this.Container)));

        Contract.Assume(this.activity != null);
        return this.activity;
      }
    }

    /// <summary>
    /// Gets the ID of the activity performed by the assembly line.
    /// </summary>
    /// <value>
    /// The ID of the activity performed by the assembly line.
    /// </value>
    public ActivityId ActivityId
    {
      get { return this.Entity.ActivityId; }
    }

    /// <summary>
    /// Gets the cost to install a job in the assembly line.
    /// </summary>
    /// <value>
    /// The cost to install a job in the assembly line.
    /// </value>
    public double CostInstall
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CostInstall;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the cost to install a job in the assembly line.
    /// </summary>
    /// <value>
    /// The cost to install a job in the assembly line.
    /// </value>
    public double CostPerHour
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CostPerHour;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the discount offered by the assembly line for each point of
    /// positive standing the character has with the owning corporation,
    /// in percent.
    /// </summary>
    /// <value>
    /// The discount offered for each point of positive standing.
    /// </value>
    public double DiscountPerGoodStandingPoint
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.DiscountPerGoodStandingPoint;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the ID of the assembly line.
    /// </summary>
    /// <value>
    /// The ID of the assembly line.
    /// </value>
    public AssemblyLineId Id
    {
      get { return this.Entity.Id; }
    }

    /// <summary>
    /// Gets the maximum security level a character can have and be
    /// allowed to use the assembly line.
    /// </summary>
    /// <value>
    /// The maximum allowed security level.
    /// </value>
    public double MaximumCharSecurity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.MaximumCharSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the maximum security level a character's corporation can 
    /// have and be allowed to use the assembly line.
    /// </summary>
    /// <value>
    /// The maximum allowed corporation security level.
    /// </value>
    public double MaximumCorpSecurity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.MaximumCorpSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the minimum security level a character can have and be
    /// allowed to use the assembly line.
    /// </summary>
    /// <value>
    /// The minimum allowed security level.
    /// </value>
    public double MinimumCharSecurity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.MinimumCharSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the minimum security level a character's corporation can 
    /// have and be allowed to use the assembly line.
    /// </summary>
    /// <value>
    /// The minimum allowed corporation security level.
    /// </value>
    public double MinimumCorpSecurity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.MinimumCorpSecurity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the minimum standing a character must have with the
    /// owning corporation in order to use the assembly line.
    /// </summary>
    /// <value>
    /// The minimum standing required to use the assembly line.
    /// </value>
    public double MinimumStanding
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.MinimumStanding;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the next time the assembly line is available.  This is
    /// an in-game value and is always <see langword="null" />.
    /// </summary>
    /// <value>
    /// The value of this property is always <see langword="null" />.
    /// </value>
    public DateTime? NextFreeTime
    {
      get { return this.Entity.NextFreeTime; }
    }

    /// <summary>
    /// Gets the corporation that owns the assembly line.
    /// </summary>
    /// <value>
    /// The <see cref="NpcCorporation" /> that owns the assembly line.
    /// </value>    
    public NpcCorporation Owner
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.owner, 
          () => this.Container.GetOrAddStoredValue<NpcCorporation>(this.OwnerId, () => (NpcCorporation)this.Entity.Owner.ToAdapter(this.Container)));

        Contract.Assume(this.owner != null);
        return this.owner;
      }
    }

    /// <summary>
    /// Gets the ID of the corporation that owns the current station.
    /// </summary>
    /// <value>
    /// The ID of the corporation that owns the current station.
    /// </value> 
    public NpcCorporationId OwnerId
    {
      get { return this.Entity.OwnerId; }
    }

    /// <summary>
    /// Gets a restrict mask value (?).
    /// </summary>
    /// <value>
    /// The meaning of this property is unknown.
    /// </value>
    public byte RestrictionMask
    {
      get { return this.Entity.RestrictionMask; }
    }

    /// <summary>
    /// Gets the <see cref="Station" /> where the assembly line is located.
    /// </summary>
    /// <value>
    /// The <see cref="Station" /> where the assembly line is located.
    /// </value>    
    public Station Station
    {
      get
      {
        Contract.Ensures(Contract.Result<Station>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.station,
          () => this.Container.GetOrAddStoredValue<Station>(this.StationId, () => (Station)this.Entity.Container.ToAdapter(this.Container)));

        Contract.Assume(this.station != null);
        return this.station;
      }
    }

    /// <summary>
    /// Gets the ID of the station where the assembly line is located.
    /// </summary>
    /// <value>
    /// The ID of the station where the assembly line is located.
    /// </value>   
    public StationId StationId
    {
      get { return this.Entity.ContainerId; }
    }

    /// <summary>
    /// Gets the surcharge imposed by the assembly line for each point of
    /// negative standing the character has with the owning corporation,
    /// in percent.
    /// </summary>
    /// <value>
    /// The surcharge imposed for each point of negative standing.
    /// </value>
    public double SurchargePerBadStandingPoint
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.SurchargePerBadStandingPoint;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the type of the assembly line.
    /// </summary>
    /// <value>
    /// An <see cref="AssemblyLineType" /> describing the type of the
    /// assembly line.
    /// </value>    
    public AssemblyLineType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<AssemblyLineType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.assemblyLineType,
          () => this.Container.GetOrAddStoredValue<AssemblyLineType>(this.TypeId, () => this.Entity.AssemblyLineType.ToAdapter(this.Container)));

        Contract.Assume(this.assemblyLineType != null);
        return this.assemblyLineType;
      }
    }

    /// <summary>
    /// Gets the ID of the assembly line's type.
    /// station.
    /// </summary>
    /// <value>
    /// The ID of the assembly line's type.
    /// </value>   
    public AssemblyLineTypeId TypeId
    {
      get { return this.Entity.AssemblyLineTypeId; }
    }

    /// <summary>
    /// Gets an ID value for UI grouping (?).
    /// </summary>
    /// <value>
    /// The meaning of this property is unknown.
    /// </value>
    public byte UiGroupingId
    {
      get { return this.Entity.UiGroupingId; }
    }

    /* Methods */

    /// <inheritdoc />
    public int CompareTo(AssemblyLine other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.Type.Name.CompareTo(other.Type.Name);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AssemblyLine);
    }

    /// <inheritdoc />
    public bool Equals(AssemblyLine other)
    {
      if (other == null)
      {
        return false;
      }

      return this.Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Type.Name;
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public sealed partial class AssemblyLine : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      AssemblyLine other = obj as AssemblyLine;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IKeyItem<AssemblyLineId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class AssemblyLine : IKeyItem<AssemblyLineId>
  {
    AssemblyLineId IKeyItem<AssemblyLineId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}