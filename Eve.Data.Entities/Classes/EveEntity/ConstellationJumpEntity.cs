//-----------------------------------------------------------------------
// <copyright file="ConstellationJumpEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Universe;

  /// <summary>
  /// The base class for data entities for jumps between constellations.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("mapConstellationJumps")]
  public class ConstellationJumpEntity : EveEntity<ConstellationJump>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.
    
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ConstellationJumpEntity class.
    /// </summary>
    public ConstellationJumpEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FromConstellationId")]
    public virtual ConstellationEntity FromConstellation { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("fromConstellationID", Order = 1)]
    [Key]
    public long FromConstellationId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FromRegionId")]
    public virtual RegionEntity FromRegion { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("fromRegionID")]
    public long FromRegionId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ToConstellationId")]
    public virtual ConstellationEntity ToConstellation { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("toConstellationID", Order = 2)]
    [Key]
    public long ToConstellationId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ToRegionId")]
    public virtual RegionEntity ToRegion { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("toRegionID")]
    public long ToRegionId { get; internal set; }

    /// <inheritdoc />
    [NotMapped]
    protected internal override IConvertible CacheKey
    {
      get { return CreateCacheKey(this.FromConstellationId, this.ToConstellationId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="fromId">
    /// The ID of the origin constellation.
    /// </param>
    /// <param name="toId">
    /// The ID of the destination constellation.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCacheKey(ConstellationId fromId, ConstellationId toId)
    {
      return (long)((((ulong)(long)fromId.GetHashCode()) << 32) | ((ulong)(long)toId.GetHashCode()));
    }

    /// <inheritdoc />
    public override ConstellationJump ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new ConstellationJump(repository, this);
    }
  }
}