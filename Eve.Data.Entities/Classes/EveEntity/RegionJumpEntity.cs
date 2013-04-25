﻿//-----------------------------------------------------------------------
// <copyright file="RegionJumpEntity.cs" company="Jeremy H. Todd">
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
  /// The base class for data entities for jumps between regions.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("mapRegionJumps")]
  public class RegionJumpEntity : EveEntity<RegionJump>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the RegionJumpEntity class.
    /// </summary>
    public RegionJumpEntity() : base()
    {
    }

    /* Properties */

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
    [Column("fromRegionID", Order = 1)]
    [Key]
    public long FromRegionId { get; internal set; }

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
    [Column("toRegionID", Order = 2)]
    [Key]
    public long ToRegionId { get; internal set; }

    /// <inheritdoc />
    [NotMapped]
    protected internal override IConvertible CacheKey
    {
      get { return CreateCacheKey(this.FromRegionId, this.ToRegionId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="fromRegionId">
    /// The ID of the origin region.
    /// </param>
    /// <param name="toRegionId">
    /// The ID of the destination region.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCacheKey(long fromRegionId, long toRegionId)
    {
      return (long)((((ulong)(long)fromRegionId.GetHashCode()) << 32) | ((ulong)(long)toRegionId.GetHashCode()));
    }

    /// <inheritdoc />
    public override RegionJump ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new RegionJump(repository, this);
    }
  }
}