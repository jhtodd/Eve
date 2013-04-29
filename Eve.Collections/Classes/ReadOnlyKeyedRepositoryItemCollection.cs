//-----------------------------------------------------------------------
// <copyright file="ReadOnlyKeyedRepositoryItemCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Collections
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of <see cref="IEveRepositoryItem" /> objects,
  /// retrieved by numeric index or a key value.
  /// </summary>
  /// <typeparam name="TKey">
  /// The type of key used to index the collection.
  /// </typeparam>
  /// <typeparam name="TValue">
  /// The type of object contained in the collection.
  /// </typeparam>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyKeyedRepositoryItemCollection<TKey, TValue>
    : ReadOnlyKeyedCollection<TKey, TValue>,
      IEveRepositoryItem
    where TValue : IEveRepositoryItem, IKeyItem<TKey>
  {
    private IEveRepository repository;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyKeyedRepositoryItemCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="capacity">
    /// The capacity of the collection.
    /// </param>
    public ReadOnlyKeyedRepositoryItemCollection(IEveRepository repository, int capacity) : base(capacity)
    {
      Contract.Requires(repository != null, "The repository associated with the collection cannot be null.");
      Contract.Requires(capacity >= 0, "The capacity cannot be less than zero.");

      this.repository = repository;
    }

    /* Properties */

    /// <summary>
    /// Gets the repository associated with the items in the collection.
    /// </summary>
    /// <value>
    /// The <see cref="IEveRepository" /> associated with the items in the
    /// collection.
    /// </value>
    protected IEveRepository Repository
    {
      get
      {
        Contract.Ensures(Contract.Result<IEveRepository>() != null);
        return this.repository;
      }
    }

    /* Methods */

    /// <inheritdoc />
    protected override bool Validate(TValue value)
    {
      if (value.Repository != this.Repository)
      {
        throw new InvalidOperationException("Every item added to the collection must be associated with the same repository.");
      }

      return base.Validate(value);
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.repository != null);
    }        
  }

  #region IEveRepositoryItem Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveRepositoryItem" /> interface.
  /// </content>
  public partial class ReadOnlyKeyedRepositoryItemCollection<TKey, TValue> : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Repository
    {
      get { return this.Repository; }
    }
  }
  #endregion
}