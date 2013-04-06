//-----------------------------------------------------------------------
// <copyright file="StationEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="Station" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("staStations")]
  public class StationEntity : ItemEntity
  {
    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the StationEntity class.
    /// </summary>
    public StationEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ConstellationId")]
    public virtual ConstellationEntity Constellation { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("constellationID")]
    public long ConstellationId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("CorporationId")]
    public NpcCorporationEntity Corporation { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("corporationID")]
    public long CorporationId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("dockingCostPerVolume")]
    public double DockingCostPerVolume { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("maxShipVolumeDockable")]
    public double MaxShipVolumeDockable { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("officeRentalCost")]
    public int OfficeRentalCost { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("OperationId")]
    public StationOperationEntity Operation { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("operationID")]
    public StationOperationId OperationId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("RegionId")]
    public virtual RegionEntity Region { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("regionID")]
    public long RegionId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("reprocessingEfficiency")]
    public double ReprocessingEfficiency { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("reprocessingHangarFlag")]
    public byte ReprocessingHangarFlag { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("reprocessingStationsTake")]
    public double ReprocessingStationsTake { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("security")]
    public short Security { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("SolarSystemId")]
    public SolarSystemEntity SolarSystem { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("solarSystemID")]
    public long SolarSystemId { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("stationName")]
    public string StationName { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("StationTypeId")]
    public StationTypeEntity StationType { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("stationTypeID")]
    public int StationTypeId { get; set; }

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
    [Column("y")]
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("z")]
    public double Z { get; set; }
  }
}