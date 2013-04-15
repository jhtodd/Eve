//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAssemblyLineTypeCategoryDetailCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
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
  /// A read-only collection of effects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyAssemblyLineTypeCategoryDetailCollection : ReadOnlyKeyedCollection<CategoryId, AssemblyLineTypeCategoryDetail>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAssemblyLineTypeCategoryDetailCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAssemblyLineTypeCategoryDetailCollection(IEnumerable<AssemblyLineTypeCategoryDetail> contents)
      : base(new KeyGenerator<CategoryId, AssemblyLineTypeCategoryDetail>(x => x.CategoryId), null)
    {
      if (contents != null)
      {
        foreach (AssemblyLineTypeCategoryDetail details in contents)
        {
          Items.AddWithoutCallback(details);
        }
      }
    }
  }
}