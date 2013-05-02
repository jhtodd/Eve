﻿//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeCategoryDetail.cs" company="Jeremy H. Todd">
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

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about which <see cref="Category">Categories</see> an assembly
  /// line can process.
  /// </summary>
  public sealed partial class AssemblyLineTypeCategoryDetail
    : EveEntityAdapter<AssemblyLineTypeCategoryDetailEntity, AssemblyLineTypeCategoryDetail>,
      IKeyItem<CategoryId>,
      IKeyItem<long>
  {
    private AssemblyLineType assemblyLineType;
    private Category category;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeCategoryDetail class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AssemblyLineTypeCategoryDetail(IEveRepository repository, AssemblyLineTypeCategoryDetailEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the type of assembly line to which this category detail information
    /// applies.
    /// </summary>
    /// <value>
    /// An <see cref="AssemblyLineType" /> describing the type of assembly
    /// line.
    /// </value>    
    public AssemblyLineType AssemblyLineType
    {
      get
      {
        Contract.Ensures(Contract.Result<AssemblyLineType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.assemblyLineType, this.Entity.AssemblyLineTypeId, () => this.Entity.AssemblyLineType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of assembly line to which this category detail
    /// information applies.
    /// </summary>
    /// <value>
    /// The ID of the type of assembly line to which this category detail
    /// information applies.
    /// </value>   
    public AssemblyLineTypeId AssemblyLineTypeId
    {
      get { return this.Entity.AssemblyLineTypeId; }
    }

    /// <summary>
    /// Gets the <see cref="Category" /> to which this category detail information
    /// applies.
    /// </summary>
    /// <value>
    /// The <see cref="Category" /> to which this category detail information
    /// applies.
    /// </value>    
    public Category Category
    {
      get
      {
        Contract.Ensures(Contract.Result<Category>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.category, this.Entity.CategoryId, () => this.Entity.Category);
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Category" /> to which this category detail
    /// information applies.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Category" /> to which this category detail
    /// information applies.
    /// </value> 
    public CategoryId CategoryId
    {
      get { return this.Entity.CategoryId; }
    }

    /// <summary>
    /// Gets the material multiplier for jobs performed on types in the
    /// specified category.
    /// </summary>
    /// <value>
    /// The material multiplier for jobs performed on types in the
    /// specified category.
    /// </value>    
    public double MaterialMultiplier
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.Entity.MaterialMultiplier;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the time multiplier for jobs performed on types in the
    /// specified category.
    /// </summary>
    /// <value>
    /// The time multiplier for jobs performed on types in the
    /// specified category.
    /// </value>    
    public double TimeMultiplier
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.Entity.TimeMultiplier;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(AssemblyLineTypeCategoryDetail other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.AssemblyLineType.Name.CompareTo(other.AssemblyLineType.Name);

      if (result == 0)
      {
        result = this.Category.Name.CompareTo(other.Category.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.AssemblyLineType.Name + " (" + this.Category.Name + ")";
    }
  }

  #region IKeyItem<CategoryId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class AssemblyLineTypeCategoryDetail : IKeyItem<CategoryId>
  {
    CategoryId IKeyItem<CategoryId>.Key
    {
      get { return this.CategoryId; }
    }
  }
  #endregion

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class AssemblyLineTypeCategoryDetail : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return AssemblyLineTypeCategoryDetailEntity.CreateCacheKey(this.AssemblyLineTypeId, this.CategoryId); }
    }
  }
  #endregion
}