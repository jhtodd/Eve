//-----------------------------------------------------------------------
// <copyright file="ReadOnlyEveTypeCollection{TEveType}.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Collections;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of objects derived from <see cref="EveType" />.
  /// </summary>
  /// <typeparam name="TEveType">
  /// The type derived from <see cref="EveType" /> contained in the collection.
  /// </typeparam>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyEveTypeCollection<TEveType>
    : ReadOnlyEntityAdapterCollection<EveTypeEntity, EveType, TEveType>
    where TEveType : EveType
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyEveTypeCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyEveTypeCollection(IEveRepository repository, IEnumerable<TEveType> contents)
      : base(repository, contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlyEveTypeCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    public ReadOnlyEveTypeCollection(IEveRepository repository, IEnumerable<EveTypeEntity> entities)
      : base(repository, entities)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }
  }
}