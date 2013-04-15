//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeGroupDetailEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Industry;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The data entity for the <see cref="AssemblyLineTypeGroupDetail" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("ramAssemblyLineTypeDetailPerGroup")]
  public class AssemblyLineTypeGroupDetailEntity : EveEntityBase<AssemblyLineTypeGroupDetail>
  {
    // Check InnerEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeGroupDetailEntity class.
    /// </summary>
    public AssemblyLineTypeGroupDetailEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [ForeignKey("AssemblyLineTypeId")]
    public virtual AssemblyLineTypeEntity AssemblyLineType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("assemblyLineTypeID", Order = 1)]
    [Key]
    public byte AssemblyLineTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [ForeignKey("GroupId")]
    public virtual GroupEntity Group { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("groupID", Order = 2)]
    [Key]
    public GroupId GroupId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("materialMultiplier")]
    public double MaterialMultiplier { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("timeMultiplier")]
    public double TimeMultiplier { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public override AssemblyLineTypeGroupDetail ToAdapter(IEveRepository container)
    {
      Contract.Assume(container != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new AssemblyLineTypeGroupDetail(container, this);
    }
  }
}