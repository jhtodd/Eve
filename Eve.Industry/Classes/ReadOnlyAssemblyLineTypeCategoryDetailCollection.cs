﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAssemblyLineTypeCategoryDetailCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System.Collections.Generic;

  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of effects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlyAssemblyLineTypeCategoryDetailCollection : ReadOnlyKeyedCollection<CategoryId, AssemblyLineTypeCategoryDetail>
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