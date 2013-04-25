//-----------------------------------------------------------------------
// <copyright file="ReadOnlyItemCollection{TItem}.cs" company="Jeremy H. Todd">
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
  /// A read-only collection of objects derived from <see cref="Item" />.
  /// </summary>
  /// <typeparam name="TItem">
  /// The type derived from <see cref="Item" /> contained in the collection.
  /// </typeparam>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyItemCollection<TItem>
    : ReadOnlyEntityAdapterCollection<ItemEntity, Item, TItem>
    where TItem : Item
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyItemCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyItemCollection(IEveRepository repository, IEnumerable<TItem> contents)
      : base(repository, contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlyItemCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    public ReadOnlyItemCollection(IEveRepository repository, IEnumerable<ItemEntity> entities)
      : base(repository, entities)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }
  }
}