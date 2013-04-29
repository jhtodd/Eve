//-----------------------------------------------------------------------
// <copyright file="CertificateClass.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about a class of certificate.
  /// </summary>
  public sealed class CertificateClass : BaseValue<CertificateClassId, CertificateClassId, CertificateClassEntity, CertificateClass>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateClass class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CertificateClass(IEveRepository repository, CertificateClassEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}