﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyNpcCorporationDivisionCollection.cs" company="Jeremy H. Todd">
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
  /// A read-only collection of corporation divisions.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyNpcCorporationDivisionCollection : ReadOnlyKeyedCollection<DivisionId, NpcCorporationDivision>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyNpcCorporationDivisionCollection" /> class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyNpcCorporationDivisionCollection(IEnumerable<NpcCorporationDivision> contents) : base()
    {
      if (contents != null)
      {
        foreach (NpcCorporationDivision division in contents)
        {
          Items.AddWithoutCallback(division);
        }
      }
    }
  }
}