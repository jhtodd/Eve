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
  /// Contains information about the category of an attribute.
  /// </summary>
  public sealed class CertificateCategory : BaseValue<CertificateCategoryId, CertificateCategoryId, CertificateCategoryEntity, CertificateCategory>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateCategory class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CertificateCategory(IEveRepository container, CertificateCategoryEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}