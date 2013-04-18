//-----------------------------------------------------------------------
// <copyright file="AssemblyLineStationEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Industry;

  /// <summary>
  /// The data entity for the <see cref="AssemblyLineStation" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("ramAssemblyLineStations")]
  public class AssemblyLineStationEntity : EveEntity<AssemblyLineStation>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.
    
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineStationEntity class.
    /// </summary>
    public AssemblyLineStationEntity() : base()
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
    [Column("assemblyLineTypeID", Order = 2)]
    [Key]
    public byte AssemblyLineTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("OwnerId")]
    public virtual ItemEntity Owner { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("ownerID")]
    public long OwnerId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("quantity")]
    public byte Quantity { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("RegionId")]
    public virtual ItemEntity Region { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("regionID")]
    public long RegionId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("SolarSystemId")]
    public virtual ItemEntity SolarSystem { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("solarSystemID")]
    public long SolarSystemId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("StationId")]
    public virtual ItemEntity Station { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("stationID", Order = 1)]
    [Key]
    public long StationId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("StationTypeId")]
    public virtual StationTypeEntity StationType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("stationTypeID")]
    public int StationTypeId { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public override AssemblyLineStation ToAdapter(IEveRepository container)
    {
      Contract.Assume(container != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new AssemblyLineStation(container, this);
    }
  }
}