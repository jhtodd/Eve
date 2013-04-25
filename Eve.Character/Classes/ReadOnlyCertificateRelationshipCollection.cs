//-----------------------------------------------------------------------
// <copyright file="ReadOnlyCertificateRelationshipCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Collections;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of <see cref="CertificateRelationship" /> objects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlyCertificateRelationshipCollection
    : ReadOnlyEntityAdapterCollection<CertificateRelationshipEntity, CertificateRelationship>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyCertificateRelationshipCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyCertificateRelationshipCollection(IEveRepository repository, IEnumerable<CertificateRelationship> contents)
      : base(repository, contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlyCertificateRelationshipCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    public ReadOnlyCertificateRelationshipCollection(IEveRepository repository, IEnumerable<CertificateRelationshipEntity> entities)
      : base(repository, entities)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }
  }
}