//-----------------------------------------------------------------------
// <copyright file="ConstellationEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Linq;

  using Eve.Universe;

  using FreeNet;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// The data entity for the <see cref="Constellation" /> class.
  /// </summary>
  [Table("mapConstellations")]
  public class ConstellationEntity : ItemEntity {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ConstellationEntity class.
    /// </summary>
    public ConstellationEntity() : base() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("constellationName")]
    public string ConstellationName { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FactionId")]
    public virtual FactionEntity Faction { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("factionID")]
    public FactionId? FactionId { get; set; }
    //******************************************************************************
    // <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ICollection<ConstellationJumpEntity> Jumps { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("radius")]
    public double Radius { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("RegionId")]
    public virtual RegionEntity Region { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("regionID")]
    public long RegionId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("Id")]
    public virtual ICollection<SolarSystemEntity> SolarSystems { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("x")]
    public double X { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("xMax")]
    public double XMax { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("xMin")]
    public double XMin { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("y")]
    public double Y { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("yMax")]
    public double YMax { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("yMin")]
    public double YMin { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("z")]
    public double Z { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("zMax")]
    public double ZMax { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("zMin")]
    public double ZMin { get; set; }
    #endregion
  }
}