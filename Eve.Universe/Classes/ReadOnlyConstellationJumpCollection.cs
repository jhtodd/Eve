//-----------------------------------------------------------------------
// <copyright file="ReadOnlyConstellationJumpCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// A read-only collection of constellation jumps.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyConstellationJumpCollection : ReadOnlyCollection<ConstellationJump>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyConstellationJumpCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyConstellationJumpCollection(IEnumerable<ConstellationJump> contents) : base()
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