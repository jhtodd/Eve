//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAncestryCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of solar systems.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyAncestryCollection : ReadOnlyCollection<Ancestry>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAncestryCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAncestryCollection(IEnumerable<Ancestry> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      if (contents != null)
      {
        foreach (Ancestry bloodline in contents)
        {
          Items.AddWithoutCallback(bloodline);
        }
      }
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAncestryCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> to which the contents will be added.
    /// </param>
    /// <param name="contents">
    /// A sequence of <see cref="AncestryEntity" /> objects from which to create
    /// the <see cref="Ancestry" /> objects which will be contained in the
    /// collection.
    /// </param>
    public ReadOnlyAncestryCollection(IEveRepository repository, IEnumerable<AncestryEntity> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      Contract.Requires(contents == null || repository != null, "The provided repository cannot be null.");

      if (contents != null)
      {
        var realContents = contents.Select(x => repository.GetOrAddStoredValue(x.Id, () => x.ToAdapter(repository)));

        foreach (Ancestry item in realContents)
        {
          Items.AddWithoutCallback(item);
        }
      }
    }
  }
}