//-----------------------------------------------------------------------
// <copyright file="ReadOnlyMetaTypeCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
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
  /// A read-only collection of meta types.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyMetaTypeCollection : ReadOnlyCollection<MetaType>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyMetaTypeCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyMetaTypeCollection(IEnumerable<MetaType> contents) : base()
    {
      if (contents != null)
      {
        foreach (MetaType metaType in contents)
        {
          Items.AddWithoutCallback(metaType);
        }
      }
    }
  }
}