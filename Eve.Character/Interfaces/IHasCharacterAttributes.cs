﻿//-----------------------------------------------------------------------
// <copyright file="IHasCharacterAttributes.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for classes which possess a collection of EVE
  /// character attributes.
  /// </summary>
  [ContractClass(typeof(IHasCharacterAttributesContracts))]
  public interface IHasCharacterAttributes
  {
    /* Properties */

    /// <summary>
    /// Gets the collection of character attributes that apply to the item.
    /// </summary>
    /// <value>
    /// The collection of character attributes that apply to the item.
    /// </value>
    ICharacterAttributeCollection CharacterAttributes { get; }
  }
}