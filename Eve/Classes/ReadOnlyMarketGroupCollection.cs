//-----------------------------------------------------------------------
// <copyright file="ReadOnlyMarketGroupCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;

  /// <summary>
  /// A read-only collection of market groups.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyMarketGroupCollection : ReadOnlyKeyedCollection<MarketGroupId, MarketGroup>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyMarketGroupCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyMarketGroupCollection(IEnumerable<MarketGroup> contents) : base()
    {
      if (contents != null)
      {
        foreach (MarketGroup group in contents)
        {
          Items.AddWithoutCallback(group);
        }
      }
    }
  }
}