﻿//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeCategoryDetailEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Industry;

  /// <summary>
  /// The data entity for the <see cref="AssemblyLineTypeCategoryDetail" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public class AssemblyLineTypeCategoryDetailEntity : EveEntity<AssemblyLineTypeCategoryDetail>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeCategoryDetailEntity class.
    /// </summary>
    public AssemblyLineTypeCategoryDetailEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    public virtual AssemblyLineTypeEntity AssemblyLineType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    public byte AssemblyLineTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    public virtual CategoryEntity Category { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    public CategoryId CategoryId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    public double MaterialMultiplier { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    public double TimeMultiplier { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return CreateCacheKey(this.AssemblyLineTypeId, this.CategoryId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="categoryId">
    /// The ID of the category.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static IConvertible CreateCacheKey(byte assemblyLineTypeId, CategoryId categoryId)
    {
      return EveCache.CreateCompoundCacheKey(assemblyLineTypeId, categoryId);
    }

    /// <inheritdoc />
    public override AssemblyLineTypeCategoryDetail ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new AssemblyLineTypeCategoryDetail(repository, this);
    }
  }
}