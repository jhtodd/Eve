//-----------------------------------------------------------------------
// <copyright file="IEffectCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
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
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for collections of EVE effects.
  /// </summary>
  [ContractClass(typeof(IEffectCollectionContracts))]
  public interface IEffectCollection : IReadOnlyList<IEffect>
  {
    /* Indexers */

    /// <summary>
    /// Gets the effect with the specified ID.
    /// </summary>
    /// <param name="effectId">
    /// The ID of the effect to retrieve.
    /// </param>
    /// <returns>
    /// The effect with the specified ID.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no effect with the specified ID was found in the collection.
    /// </exception>
    IEffect this[EffectId effectId] { get; }

    /* Methods */

    /// <summary>
    /// Gets a value indicating whether an effect with the specified ID is
    /// contained in the collection.
    /// </summary>
    /// <param name="effectId">
    /// The ID of the effect to locate.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if an effect with the specified ID is contained
    /// in the collection; otherwise <see langword="false" />.
    /// </returns>
    bool ContainsKey(EffectId effectId);

    /// <summary>
    /// Attempts to retrieve the effect with the specified ID, returning success
    /// or failure.
    /// </summary>
    /// <param name="effectId">
    /// The ID of the effect to retrieve.
    /// </param>
    /// <param name="value">
    /// Output parameter.  Will contain the specified effect if found.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the specified effect was found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetValue(EffectId effectId, out IEffect value);
  }
}