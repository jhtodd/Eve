//-----------------------------------------------------------------------
// <copyright file="StargateEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="Stargate" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("mapJumps")]
  public class StargateEntity 
    : ItemExtensionEntity,
      IEveEntity<Stargate>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the StargateEntity class.
    /// </summary>
    public StargateEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("DestinationId")]
    public virtual StargateEntity Destination { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("celestialID")]
    public long DestinationId { get; internal set; }

    /// <summary>
    /// Gets the ID of the stargate.
    /// </summary>
    /// <value>
    /// The ID of the stargate.
    /// </value>
    [Key]
    public long Id { get; internal set; }

    /// <summary>
    /// Gets the <see cref="ItemEntity" /> associated with the current object.
    /// This can be considered the "other half" of the current object: 
    /// <c>ItemInfo</c> holds the basic information about the item, while the
    /// current object holds information specific to the item's current type
    /// (e.g. agent, region, faction, solar system, etc.).
    /// </summary>
    /// <value>
    /// The <see cref="ItemEntity" /> associated with the current object.
    /// </value>
    [ForeignKey("Id")]
    public virtual ItemEntity ItemInfo { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return this.Id; }
    }

    /* Methods */

    /// <inheritdoc />
    public Stargate ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null);
      Contract.Assume(this.ItemInfo != null);
      Contract.Assume(this.ItemInfo.IsStargate);
      return new Stargate(repository, this.ItemInfo);
    }
  }
}