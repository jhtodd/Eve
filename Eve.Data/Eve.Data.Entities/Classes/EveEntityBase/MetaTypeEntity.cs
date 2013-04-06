//-----------------------------------------------------------------------
// <copyright file="MetaTypeEntity.cs" company="Jeremy H. Todd">
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
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The data entity for the <see cref="MetaType" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("invMetaTypes")]
  public class MetaTypeEntity : EveEntityBase
  {
    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the MetaTypeEntity class.
    /// </summary>
    public MetaTypeEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("TypeId")]
    public virtual EveTypeEntity Type { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("typeID")]
    [Key]
    public int TypeId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("MetaGroupId")]
    public virtual MetaGroupEntity MetaGroup { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("metaGroupID")]
    public MetaGroupId MetaGroupId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ParentTypeId")]
    public virtual EveTypeEntity ParentType { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("parentTypeID")]
    public int ParentTypeId { get; set; }

    /* Methods */

    /// <inheritdoc />
    public override IEveEntityAdapter<EveEntityBase> ToAdapter()
    {
      return new MetaType(this);
    }
  }
}