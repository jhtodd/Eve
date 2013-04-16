//-----------------------------------------------------------------------
// <copyright file="ReadOnlyBloodlineCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Collections.Generic;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of solar systems.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyBloodlineCollection : ReadOnlyCollection<Bloodline>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyBloodlineCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyBloodlineCollection(IEnumerable<Bloodline> contents) : base()
    {
      if (contents != null)
      {
        foreach (Bloodline bloodline in contents)
        {
          Items.AddWithoutCallback(bloodline);
        }
      }
    }
  }
}