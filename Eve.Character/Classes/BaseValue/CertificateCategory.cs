//-----------------------------------------------------------------------
// <copyright file="CertificateCategory.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about the category of a certificate.
  /// </summary>
  public sealed class CertificateCategory : BaseValue<CertificateCategoryId, CertificateCategoryId, CertificateCategoryEntity, CertificateCategory>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateCategory class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CertificateCategory(IEveRepository repository, CertificateCategoryEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}