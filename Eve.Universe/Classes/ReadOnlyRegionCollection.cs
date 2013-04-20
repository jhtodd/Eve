﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyRegionCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Collections.Generic;
  using System.Linq;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of regions.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyRegionCollection : ReadOnlyCollection<Region>
  {
    /* Constructors */
    
    /// <summary>
    /// Initializes a new instance of the ReadOnlyRegionCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyRegionCollection(IEnumerable<Region> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      if (contents != null)
      {
        foreach (Region region in contents)
        {
          Items.AddWithoutCallback(region);
        }
      }
    }
  }
}