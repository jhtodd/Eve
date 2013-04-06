//-----------------------------------------------------------------------
// <copyright file="UniverseEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="Universe" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("mapUniverse")]
  public class UniverseEntity : ItemEntity
  {
    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the UniverseEntity class.
    /// </summary>
    public UniverseEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("radius")]
    public double Radius { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("universeName")]
    public string UniverseName { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("x")]
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("xMax")]
    public double XMax { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("xMin")]
    public double XMin { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("y")]
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("yMax")]
    public double YMax { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("yMin")]
    public double YMin { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("z")]
    public double Z { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("zMax")]
    public double ZMax { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("zMin")]
    public double ZMin { get; set; }
  }
}