//-----------------------------------------------------------------------
// <copyright file="MetaGroupEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.CodeAnalysis;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The data entity for the <see cref="MetaGroup" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("invMetaGroups")]
  public class MetaGroupEntity : BaseValueEntity<MetaGroupId>
  {
    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the MetaGroupEntity class.
    /// </summary>
    public MetaGroupEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("IconId")]
    public virtual IconEntity Icon { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("iconID")]
    public int? IconId { get; set; }

    /* Methods */

    /// <inheritdoc />
    public override IEveEntityAdapter<EveEntityBase> ToAdapter()
    {
      return new MetaGroup(this);
    }
  }
}