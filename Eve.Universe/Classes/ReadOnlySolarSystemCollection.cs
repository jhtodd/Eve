//-----------------------------------------------------------------------
// <copyright file="ReadOnlySolarSystemCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of solar systems.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlySolarSystemCollection : ReadOnlyCollection<SolarSystem>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlySolarSystemCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlySolarSystemCollection(IEnumerable<SolarSystem> contents) : base()
    {
      if (contents != null)
      {
        foreach (SolarSystem solarSystem in contents)
        {
          Items.AddWithoutCallback(solarSystem);
        }
      }
    }
  }
}