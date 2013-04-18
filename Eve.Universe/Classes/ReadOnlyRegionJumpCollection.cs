﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyRegionJumpCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Collections.Generic;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of region jumps.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyRegionJumpCollection : ReadOnlyCollection<RegionJump>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyRegionJumpCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyRegionJumpCollection(IEnumerable<RegionJump> contents) : base()
    {
      if (contents != null)
      {
        foreach (RegionJump regionJump in contents)
        {
          Items.AddWithoutCallback(regionJump);
        }
      }
    }
  }
}