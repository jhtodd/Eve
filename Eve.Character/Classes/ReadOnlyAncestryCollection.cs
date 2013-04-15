//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAncestryCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of solar systems.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyAncestryCollection : ReadOnlyCollection<Ancestry>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAncestryCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAncestryCollection(IEnumerable<Ancestry> contents) : base()
    {
      if (contents != null)
      {
        foreach (Ancestry bloodline in contents)
        {
          Items.AddWithoutCallback(bloodline);
        }
      }
    }
  }
}