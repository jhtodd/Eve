﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlySkillTypeCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
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
  /// A read-only collection of skill types.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlySkillTypeCollection : ReadOnlyKeyedCollection<SkillId, SkillType>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlySkillTypeCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlySkillTypeCollection(IEnumerable<SkillType> contents) : base()
    {
      if (contents != null)
      {
        foreach (SkillType item in contents)
        {
          Items.AddWithoutCallback(item);
        }
      }
    }
  }
}