//-----------------------------------------------------------------------
// <copyright file="ReadOnlySolarSystemCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Collections;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of <see cref="SolarSystem" /> objects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlySolarSystemCollection
    : ReadOnlyEntityAdapterCollection<SolarSystemEntity, SolarSystem>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlySolarSystemCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlySolarSystemCollection(IEveRepository repository, IEnumerable<SolarSystem> contents)
      : base(repository, contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlySolarSystemCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    public ReadOnlySolarSystemCollection(IEveRepository repository, IEnumerable<SolarSystemEntity> entities)
      : base(repository, entities)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }
  }
}