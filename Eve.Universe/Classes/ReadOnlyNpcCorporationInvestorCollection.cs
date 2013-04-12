﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyNpcCorporationInvestorCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;

  /// <summary>
  /// A read-only collection of corporate investors.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public class ReadOnlyNpcCorporationInvestorCollection : ReadOnlyCollection<NpcCorporationInvestor>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyNpcCorporationInvestorCollection" /> class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyNpcCorporationInvestorCollection(IEnumerable<NpcCorporationInvestor> contents) : base()
    {
      if (contents != null)
      {
        foreach (NpcCorporationInvestor investor in contents)
        {
          Items.AddWithoutCallback(investor);
        }
      }
    }
  }
}