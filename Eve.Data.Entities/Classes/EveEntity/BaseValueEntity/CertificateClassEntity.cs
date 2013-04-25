//-----------------------------------------------------------------------
// <copyright file="CertificateClassEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="CertificateClass" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("crtClasses")]
  public class CertificateClassEntity : BaseValueEntity<CertificateClassId, CertificateClass>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateClassEntity class.
    /// </summary>
    public CertificateClassEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override CertificateClass ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new CertificateClass(repository, this);
    }
  }
}