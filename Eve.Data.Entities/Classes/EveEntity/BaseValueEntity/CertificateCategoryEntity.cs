//-----------------------------------------------------------------------
// <copyright file="CertificateCategoryEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="CertificateCategory" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("crtCategories")]
  public class CertificateCategoryEntity : BaseValueEntity<CertificateCategoryId, CertificateCategory>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateCategoryEntity class.
    /// </summary>
    public CertificateCategoryEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override CertificateCategory ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new CertificateCategory(repository, this);
    }
  }
}