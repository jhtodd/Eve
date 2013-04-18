//-----------------------------------------------------------------------
// <copyright file="ReadOnlySolarSystemJumpCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Collections.Generic;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of solar system jumps.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlySolarSystemJumpCollection : ReadOnlyCollection<SolarSystemJump>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlySolarSystemJumpCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlySolarSystemJumpCollection(IEnumerable<SolarSystemJump> contents) : base()
    {
      if (contents != null)
      {
        foreach (SolarSystemJump solarSystemJump in contents)
        {
          Items.AddWithoutCallback(solarSystemJump);
        }
      }
    }
  }
}