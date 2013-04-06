//-----------------------------------------------------------------------
// <copyright file="ICharacterAttribute.cs" company="Jeremy H. Todd">
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
  /// The base interface for classes which describe an attribute belonging to
  /// an EVE character.
  /// </summary>
  [ContractClass(typeof(ICharacterAttributeContracts))]
  public interface ICharacterAttribute
  {
    /* Properties */

    /// <summary>
    /// Gets the base value of the attribute.
    /// </summary>
    /// <value>
    /// The base value of the attribute.
    /// </value>
    double BaseValue { get; }

    /// <summary>
    /// Gets the ID of the attribute.
    /// </summary>
    /// <value>
    /// The ID of the attribute.
    /// </value>
    CharacterAttributeId Id { get; }

    /// <summary>
    /// Gets the type of the attribute.
    /// </summary>
    /// <value>
    /// The type of the attribute.
    /// </value>
    CharacterAttributeType Type { get; }
  }
}