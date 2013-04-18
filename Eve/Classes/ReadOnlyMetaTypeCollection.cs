//-----------------------------------------------------------------------
// <copyright file="ReadOnlyMetaTypeCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Collections.Generic;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of meta types.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyMetaTypeCollection : ReadOnlyCollection<MetaType>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyMetaTypeCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyMetaTypeCollection(IEnumerable<MetaType> contents) : base()
    {
      if (contents != null)
      {
        foreach (MetaType metaType in contents)
        {
          Items.AddWithoutCallback(metaType);
        }
      }
    }
  }
}