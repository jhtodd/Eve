//-----------------------------------------------------------------------
// <copyright file="ReadOnlyContrabandInfoCollection.cs" company="Jeremy H. Todd">
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
  /// A read-only collection of <see cref="ContrabandInfo" /> objects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlyContrabandInfoCollection
    : ReadOnlyRepositoryItemCollection<ContrabandInfo>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyContrabandInfoCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="capacity">
    /// The capacity of the collection.
    /// </param>
    internal ReadOnlyContrabandInfoCollection(IEveRepository repository, int capacity) : base(repository, capacity)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
      Contract.Requires(capacity >= 0, "The capacity cannot be less than zero.");
    }

    /* Methods */

    /// <summary>
    /// Creates a new instance of the ReadOnlyContrabandInfoCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    /// <returns>
    /// A newly created collection containing the specified items.
    /// </returns>
    public static ReadOnlyContrabandInfoCollection Create(IEveRepository repository, IEnumerable<ContrabandInfo> contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");

      var result = new ReadOnlyContrabandInfoCollection(repository, contents == null ? 0 : contents.Count());

      if (contents != null)
      {
        foreach (var item in contents)
        {
          result.Items.AddWithoutCallback(item);
        }
      }

      return result;
    }

    /// <summary>
    /// Creates a new instance of the ReadOnlyContrabandInfoCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    /// <returns>
    /// A newly created collection containing the specified items.
    /// </returns>
    public static ReadOnlyContrabandInfoCollection Create(IEveRepository repository, IEnumerable<ContrabandInfoEntity> entities)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");

      return Create(repository, entities == null ? null : entities.Select(x => x.ToAdapter(repository)).OrderBy(x => x));
    }
  }
}