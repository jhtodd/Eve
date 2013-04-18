//-----------------------------------------------------------------------
// <copyright file="ReadOnlyConstellationCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Collections.Generic;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of constellations.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyConstellationCollection : ReadOnlyCollection<Constellation>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyConstellationCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyConstellationCollection(IEnumerable<Constellation> contents) : base()
    {
      if (contents != null)
      {
        foreach (Constellation constellation in contents)
        {
          Items.AddWithoutCallback(constellation);
        }
      }
    }
  }
}