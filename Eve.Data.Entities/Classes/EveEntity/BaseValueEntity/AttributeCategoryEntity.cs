//-----------------------------------------------------------------------
// <copyright file="AttributeCategoryEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The data entity for the <see cref="AttributeCategory" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("dgmAttributeCategories")]
  public class AttributeCategoryEntity : BaseValueEntity<AttributeCategoryId, AttributeCategory>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AttributeCategoryEntity class.
    /// </summary>
    public AttributeCategoryEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override AttributeCategory ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new AttributeCategory(repository, this);
    }
  }
}