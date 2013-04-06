//-----------------------------------------------------------------------
// <copyright file="IHasEffects.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for classes which possess a collection of EVE effects.
  /// </summary>
  [ContractClass(typeof(IHasEffectsContracts))]
  public interface IHasEffects
  {
    /* Properties */

    /// <summary>
    /// Gets the collection of effects associated with the item.
    /// </summary>
    /// <value>
    /// The collection of effects associated with the item.
    /// </value>
    IEffectCollection Effects { get; }
  }
}