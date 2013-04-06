//-----------------------------------------------------------------------
// <copyright file="ReadOnlyStationServiceCollection.cs" company="Jeremy H. Todd">
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

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of station services.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyStationServiceCollection : ReadOnlyKeyedCollection<StationServiceId, StationService>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyStationServiceCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyStationServiceCollection(IEnumerable<StationService> contents) : base()
    {
      if (contents != null)
      {
        foreach (StationService service in contents)
        {
          Items.AddWithoutCallback(service);
        }
      }
    }
  }
}