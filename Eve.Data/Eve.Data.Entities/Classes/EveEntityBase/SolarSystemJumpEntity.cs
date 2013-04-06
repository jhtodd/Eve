//-----------------------------------------------------------------------
// <copyright file="SolarSystemJumpEntity.cs" company="Jeremy H. Todd">
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

  using Eve.Universe;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for data entities for jumps between solar systems.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("mapSolarSystemJumps")]
  public class SolarSystemJumpEntity : EveEntityBase
  {
    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SolarSystemJumpEntity class.
    /// </summary>
    public SolarSystemJumpEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FromConstellationId")]
    public virtual ConstellationEntity FromConstellation { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("fromConstellationID")]
    public long FromConstellationId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FromRegionId")]
    public virtual RegionEntity FromRegion { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("fromRegionID")]
    public long FromRegionId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FromSolarSystemId")]
    public virtual SolarSystemEntity FromSolarSystem { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("fromSolarSystemID", Order = 1)]
    [Key]
    public long FromSolarSystemId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ToConstellationId")]
    public virtual ConstellationEntity ToConstellation { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("toConstellationID")]
    public long ToConstellationId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ToRegionId")]
    public virtual RegionEntity ToRegion { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("toRegionID")]
    public long ToRegionId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ToSolarSystemId")]
    public virtual SolarSystemEntity ToSolarSystem { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("toSolarSystemID", Order = 2)]
    [Key]
    public long ToSolarSystemId { get; set; }

    /* Methods */

    /// <inheritdoc />
    public override IEveEntityAdapter<EveEntityBase> ToAdapter()
    {
      return new SolarSystemJump(this);
    }
  }
}