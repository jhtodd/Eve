﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyTypeCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of item types.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyTypeCollection : ReadOnlyCollection<EveType>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyTypeCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyTypeCollection(IEnumerable<EveType> contents) : base()
    {
      if (contents != null)
      {
        foreach (EveType item in contents)
        {
          Items.AddWithoutCallback(item);
        }
      }
    }
  }
}