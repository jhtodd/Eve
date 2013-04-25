//-----------------------------------------------------------------------
// <copyright file="ItemNameEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for names associated with EVE items.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("invNames")]
  public class ItemNameEntity : EveEntity
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemNameEntity" /> class.
    /// </summary>
    public ItemNameEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("itemID")]
    [Key]
    public long ItemId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("itemName")]
    public string Value { get; internal set; }

    /// <inheritdoc />
    [NotMapped]
    protected internal override IConvertible CacheKey
    {
      get { return this.ItemId; }
    }
  }
}