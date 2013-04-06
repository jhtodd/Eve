//-----------------------------------------------------------------------
// <copyright file="ICharacterAttributeCollection.cs" company="Jeremy H. Todd">
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
  using FreeNet.Collections;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for collections of EVE character attributes.
  /// </summary>
  [ContractClass(typeof(ICharacterAttributeCollectionContracts))]
  public interface ICharacterAttributeCollection : IReadOnlyList<ICharacterAttribute>
  {
    /* Indexers */

    /// <summary>
    /// Gets the character attribute with the specified ID.
    /// </summary>
    /// <param name="characterAttributeId">
    /// The ID of the character attribute to retrieve.
    /// </param>
    /// <returns>
    /// The character attribute with the specified ID.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no character attribute with the specified ID was found in the
    /// collection.
    /// </exception>
    ICharacterAttribute this[CharacterAttributeId characterAttributeId] { get; }

    /* Methods */

    /// <summary>
    /// Gets a value indicating whether a character attribute with the specified
    /// ID is contained in the collection.
    /// </summary>
    /// <param name="characterAttributeId">
    /// The ID of the character attribute to locate.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a character attribute with the specified ID is
    /// contained in the collection; otherwise <see langword="false" />.
    /// </returns>
    bool ContainsKey(CharacterAttributeId characterAttributeId);

    /// <summary>
    /// Gets the value of the specified character attribute, or 0 if the specified
    /// character attribute is not contained in the collection.
    /// </summary>
    /// <param name="characterAttributeId">
    /// The ID of the character attribute whose value to retrieve.
    /// </param>
    /// <returns>
    /// The value of the specified character attribute.
    /// </returns>
    double GetCharacterAttributeValue(CharacterAttributeId characterAttributeId);

    /// <summary>
    /// Attempts to retrieve the character attribute with the specified ID,
    /// returning success or failure.
    /// </summary>
    /// <param name="characterAttributeId">
    /// The ID of the character attribute to retrieve.
    /// </param>
    /// <param name="value">
    /// Output parameter.  Will contain the specified character attribute if found.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the specified character attribute was found;
    /// otherwise <see langword="false" />.
    /// </returns>
    bool TryGetValue(CharacterAttributeId characterAttributeId, out ICharacterAttribute value);
  }
}