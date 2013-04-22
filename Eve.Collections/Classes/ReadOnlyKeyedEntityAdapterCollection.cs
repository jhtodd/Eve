//-----------------------------------------------------------------------
// <copyright file="ReadOnlyKeyedEntityAdapterCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Collections
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of item types.
  /// </summary>
  /// <typeparam name="TKey">
  /// The type of the key used to index the collection.
  /// </typeparam>
  /// <typeparam name="TEntity">
  /// The type of the entity.
  /// </typeparam>
  /// <typeparam name="TAdapter">
  /// The type of the adapter.
  /// </typeparam>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyKeyedEntityAdapterCollection<TKey, TEntity, TAdapter> : ReadOnlyKeyedCollection<TKey, TAdapter>
    where TEntity : IEveEntity<TAdapter>
    where TAdapter : IEveEntityAdapter<TEntity>, IEveCacheable, IKeyItem<TKey>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyKeyedEntityAdapterCollection{TKey, TEntity, TAdapter}" /> class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyKeyedEntityAdapterCollection(IEnumerable<TAdapter> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      if (contents != null)
      {
        foreach (TAdapter item in contents.OrderBy(x => x))
        {
          Items.AddWithoutCallback(item);
        }
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyKeyedEntityAdapterCollection{TKey, TEntity, TAdapter}" /> class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> to which the contents will be added.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    public ReadOnlyKeyedEntityAdapterCollection(IEveRepository repository, IEnumerable<TEntity> entities)
      : this(CreateContents(repository, entities))
    {
      Contract.Requires(entities == null || repository != null, "The provided repository cannot be null.");
    }

    /* Methods */

    /// <summary>
    /// Given a sequence of EVE entities, returns a sequence of corresponding
    /// adapter objects in the provided repository.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the adapter objects.
    /// </param>
    /// <param name="entities">
    /// The sequence of entities.
    /// </param>
    /// <returns>
    /// A sequence of adapter objects created from the specified entities, and
    /// associated with the given repository.
    /// </returns>
    protected static IEnumerable<TAdapter> CreateContents(IEveRepository repository, IEnumerable<TEntity> entities)
    {
      Contract.Requires(entities == null || repository != null, "The provided repository cannot be null.");

      if (entities == null)
      {
        return null;
      }

      return entities.Select(x => repository.GetOrAddStoredValue(x.CacheKey, () => x.ToAdapter(repository)));
    }
  }
}