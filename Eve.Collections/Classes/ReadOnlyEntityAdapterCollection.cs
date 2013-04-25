//-----------------------------------------------------------------------
// <copyright file="ReadOnlyEntityAdapterCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of <see cref="IEveEntityAdapter{TEntity}" /> objects,
  /// retrieved by numeric index.
  /// </summary>
  /// <typeparam name="TEntity">
  /// The type of entity encapsulated by <typeparamref name="TAdapter" />.
  /// </typeparam>
  /// <typeparam name="TAdapter">
  /// The type of <see cref="IEveEntityAdapter{TEntity}" /> contained in the collection.
  /// </typeparam>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyEntityAdapterCollection<TEntity, TAdapter> : ReadOnlyRepositoryItemCollection<TAdapter>
    where TEntity : IEveEntity<TAdapter>
    where TAdapter : IEveCacheable, IEveRepositoryItem
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyEntityAdapterCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyEntityAdapterCollection(IEveRepository repository, IEnumerable<TAdapter> contents)
      : base(repository, contents)
    {
      Contract.Requires(repository != null, "The repository associated with the collection cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlyEntityAdapterCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    public ReadOnlyEntityAdapterCollection(IEveRepository repository, IEnumerable<TEntity> entities)
      : this(repository, CreateContents(repository, entities))
    {
      Contract.Requires(repository != null, "The repository associated with the collection cannot be null.");
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