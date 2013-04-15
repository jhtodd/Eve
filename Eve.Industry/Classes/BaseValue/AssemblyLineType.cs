//-----------------------------------------------------------------------
// <copyright file="AssemblyLineType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contains information about the type of an assembly line that can
  /// perform industrial operations.
  /// </summary>
  public sealed partial class AssemblyLineType
    : BaseValue<AssemblyLineTypeId, byte, AssemblyLineTypeEntity, AssemblyLineType>
  {
    private Activity activity;
    private ReadOnlyAssemblyLineTypeCategoryDetailCollection categoryDetails;
    private ReadOnlyAssemblyLineTypeGroupDetailCollection groupDetails;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineType class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AssemblyLineType(IEveRepository container, AssemblyLineTypeEntity entity)
      : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the <see cref="Activity" /> performed by the assembly line.
    /// </summary>
    /// <value>
    /// An <see cref="Activity" /> describing the operation performed
    /// by the assembly line.
    /// </value>
    public Activity Activity
    {
      get
      {
        Contract.Ensures(Contract.Result<Activity>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.activity ?? (this.activity = this.Container.Load<Activity>(this.ActivityId, () => this.Entity.Activity.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Activity" /> performed by the
    /// assembly line.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Activity" /> describing the operation
    /// performed by the assembly line.
    /// </value>    
    public ActivityId ActivityId
    {
      get
      {
        return this.Entity.ActivityId;
      }
    }

    /// <summary>
    /// Gets the material multiplier for jobs performed by the assembly line.
    /// </summary>
    /// <value>
    /// The material multiplier for jobs performed by the assembly line.
    /// </value>    
    public double BaseMaterialMultiplier
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.Entity.BaseMaterialMultiplier;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the time multiplier for jobs performed by the assembly line.
    /// </summary>
    /// <value>
    /// The time multiplier for jobs performed by the assembly line.
    /// </value>      
    public double BaseTimeMultiplier
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.Entity.BaseTimeMultiplier;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the collection of category details that describe which item
    /// categories can be processed by assembly lines of this type.
    /// </summary>
    /// <value>
    /// The collection of category details that describe which item
    /// categories can be processed by assembly lines of this type.
    /// </value>
    public ReadOnlyAssemblyLineTypeCategoryDetailCollection CategoryDetails
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAssemblyLineTypeCategoryDetailCollection>() != null);

        return this.categoryDetails ?? (this.categoryDetails = new ReadOnlyAssemblyLineTypeCategoryDetailCollection(this.Container.GetAssemblyLineTypeCategoryDetails(x => x.AssemblyLineTypeId == this.Id.Value).OrderBy(x => x)));
      }
    }

    /// <summary>
    /// Gets the collection of group details that describe which item
    /// categories can be processed by assembly lines of this type.
    /// </summary>
    /// <value>
    /// The collection of group details that describe which item
    /// categories can be processed by assembly lines of this type.
    /// </value>
    public ReadOnlyAssemblyLineTypeGroupDetailCollection GroupDetails
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAssemblyLineTypeGroupDetailCollection>() != null);

        return this.groupDetails ?? (this.groupDetails = new ReadOnlyAssemblyLineTypeGroupDetailCollection(this.Container.GetAssemblyLineTypeGroupDetails(x => x.AssemblyLineTypeId == this.Id.Value).OrderBy(x => x)));
      }
    }

    /// <summary>
    /// Gets the minimum cost per hour for jobs performed at the assembly line.
    /// </summary>
    /// <value>
    /// The minimum cost per hour for jobs performed at the assembly line.
    /// </value>    
    public double MinCostPerHour
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.Entity.MinCostPerHour.HasValue ? this.Entity.MinCostPerHour.Value : 0.0D;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets a value related to the volume supported by the assembly line (?).
    /// </summary>
    /// <value>
    /// The meaning of this property is not understood.
    /// </value>    
    public double? Volume
    {
      get
      {
        return this.Entity.Volume;
      }
    }
  }
}