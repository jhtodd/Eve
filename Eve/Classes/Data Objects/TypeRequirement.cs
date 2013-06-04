//-----------------------------------------------------------------------
// <copyright file="TypeRequirement.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Industry;

  using FreeNet.Collections;

  /// <summary>
  /// Contains information about the materials required to manufacture
  /// a type of EVE item.  This is also used to determine which materials
  /// are recovered when an item is reprocessed.
  /// </summary>
  public sealed partial class TypeRequirement
    : EveEntityAdapter<TypeRequirementEntity, TypeRequirement>,
      IHasIcon
  {
    private Activity activity;
    private EveType requiredType;
    private EveType type;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the TypeRequirement class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal TypeRequirement(IEveRepository repository, TypeRequirementEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the activity to which the material requirement applies.
    /// </summary>
    /// <value>
    /// The activity to which the material requirement applies.
    /// </value>
    public Activity Activity
    {
      get
      {
        Contract.Ensures(Contract.Result<Activity>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.activity, this.Entity.ActivityId, () => this.Entity.Activity);
      }
    }

    /// <summary>
    /// Gets the ID of the activity to which the material requirement applies.
    /// </summary>
    /// <value>
    /// The ID of the activity to which the material requirement applies.
    /// </value>
    public ActivityId ActivityId
    {
      get { return this.Entity.ActivityId; }
    }

    /// <summary>
    /// Gets the damage sustained by each unit of the required material
    /// when the operation is performed.
    /// </summary>
    /// <value>
    /// The damage sustained by each unit of the required material
    /// when the operation is performed.
    /// </value>
    public double DamagePerJob
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);
        Contract.Ensures(Contract.Result<double>() <= 1.0D);

        var result = this.Entity.DamagePerJob;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);
        Contract.Assume(result <= 1.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the required quantity of the material.
    /// </summary>
    /// <value>
    /// The required quantity of the material.
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
    /// Gets a value indicating whether the requirement represents a
    /// material that is present in the 
    /// </summary>
    public bool Recycle
    {
      get { return this.Entity.Recycle; }
    }

    /// <summary>
    /// Gets the type of the required material.
    /// </summary>
    /// <value>
    /// The type of the required material.
    /// </value>
    public EveType RequiredType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.requiredType, this.Entity.RequiredTypeId, () => this.Entity.RequiredType);
      }
    }

    /// <summary>
    /// Gets the ID of the required material.
    /// </summary>
    /// <value>
    /// The ID of the required material.
    /// </value>
    public EveTypeId RequiredTypeId
    {
      get { return this.Entity.RequiredTypeId; }
    }

    /// <summary>
    /// Gets the type to which the material requirement applies.
    /// </summary>
    /// <value>
    /// The type to which the material requirement applies.
    /// </value>
    public EveType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.type, this.Entity.TypeId, () => this.Entity.Type);
      }
    }

    /// <summary>
    /// Gets the ID of the type to which the material requirement applies.
    /// </summary>
    /// <value>
    /// The ID of the type to which the material requirement applies.
    /// </value>
    public EveTypeId TypeId
    {
      get { return this.Entity.TypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(TypeRequirement other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Type.CompareTo(other.Type);

      if (result == 0)
      {
        result = this.RequiredType.CompareTo(other.RequiredType);
      }

      if (result == 0)
      {
        result = this.Quantity.CompareTo(other.Quantity);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.RequiredType.Name + " (" + this.Quantity.ToString("#,##0") + ")";
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class TypeRequirement : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.RequiredType.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.RequiredType.IconId; }
    }
  }
  #endregion
}