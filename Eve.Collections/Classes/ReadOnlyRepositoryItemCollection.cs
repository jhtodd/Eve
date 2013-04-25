//-----------------------------------------------------------------------
// <copyright file="ReadOnlyRepositoryItemCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of <see cref="IEveRepositoryItem" /> objects,
  /// retrieved by numeric index.
  /// </summary>
  /// <typeparam name="T">
  /// The type of object contained in the collection.
  /// </typeparam>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyRepositoryItemCollection<T>
    : ReadOnlyCollection<T>,
      IEveRepositoryItem
    where T : IEveRepositoryItem
  {
    private IEveRepository repository;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyRepositoryItemCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyRepositoryItemCollection(IEveRepository repository, IEnumerable<T> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      Contract.Requires(repository != null, "The repository associated with the collection cannot be null.");

      this.repository = repository;

      if (contents != null)
      {
        foreach (T item in contents.Where(x => x != null).OrderBy(x => x))
        {
          Items.AddWithoutCallback(item);
        }
      }
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
    protected override bool Validate(T value)
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
  public partial class ReadOnlyRepositoryItemCollection<T> : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Repository
    {
      get { return this.Repository; }
    }
  }
  #endregion
}