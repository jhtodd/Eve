//-----------------------------------------------------------------------
// <copyright file="GenericItem.cs" company="Jeremy H. Todd">
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

  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// An EVE item which doesn't fall into any particular category.
  /// </summary>
  public sealed class GenericItem : Item
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the GenericItem class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal GenericItem(ItemEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
  }
}