//-----------------------------------------------------------------------
// <copyright file="ReadOnlyConstellationJumpCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Collections.Generic;
  using System.Linq;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of constellation jumps.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyConstellationJumpCollection : ReadOnlyCollection<ConstellationJump>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyConstellationJumpCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyConstellationJumpCollection(IEnumerable<ConstellationJump> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      if (contents != null)
      {
        foreach (ConstellationJump constellationJump in contents)
        {
          Items.AddWithoutCallback(constellationJump);
        }
      }
    }
  }
}