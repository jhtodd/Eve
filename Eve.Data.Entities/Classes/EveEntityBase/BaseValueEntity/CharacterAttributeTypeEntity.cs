//-----------------------------------------------------------------------
// <copyright file="CharacterAttributeTypeEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Character;

  /// <summary>
  /// The data entity for the <see cref="CharacterAttributeType" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("chrAttributes")]
  public class CharacterAttributeTypeEntity : BaseValueEntity<CharacterAttributeId, CharacterAttributeType>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CharacterAttributeTypeEntity class.
    /// </summary>
    public CharacterAttributeTypeEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("IconId")]
    public virtual IconEntity Icon { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("iconID")]
    public int IconId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("notes")]
    public string Notes { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("shortDescription")]
    public string ShortDescription { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public override CharacterAttributeType ToAdapter(IEveRepository container)
    {
      Contract.Assume(container != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new CharacterAttributeType(container, this);
    }
  }
}