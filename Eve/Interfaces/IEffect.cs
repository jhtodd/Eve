﻿//-----------------------------------------------------------------------
// <copyright file="IEffect.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for classes that describe an effect associated with an
  /// EVE item.
  /// </summary>
  [ContractClass(typeof(IEffectContracts))]
  public interface IEffect
  {
    /* Properties */

    /// <summary>
    /// Gets the ID of the effect.
    /// </summary>
    /// <value>
    /// The ID of the effect.
    /// </value>
    EffectId EffectId { get; }

    /// <summary>
    /// Gets the type of the effect.
    /// </summary>
    /// <value>
    /// The type of the effect.
    /// </value>
    EffectType EffectType { get; }

    /// <summary>
    /// Gets a value indicating whether it is the default effect for the item.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the effect is the default effect for the item;
    /// otherwise <see langword="false" />.
    /// </value>
    bool IsDefault { get; }
  }
}