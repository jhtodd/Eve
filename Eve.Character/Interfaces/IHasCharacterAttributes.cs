//-----------------------------------------------------------------------
// <copyright file="IHasCharacterAttributes.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// The base interface for classes which possess a collection of EVE
  /// character attributes.
  /// </summary>
  [ContractClass(typeof(IHasCharacterAttributesContracts))]
  public interface IHasCharacterAttributes {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the collection of character attributes that apply to the item.
    /// </summary>
    /// 
    /// <value>
    /// The collection of character attributes that apply to the item.
    /// </value>
    ICharacterAttributeCollection CharacterAttributes { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IHasCharacterAttributes" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasCharacterAttributes))]
  internal abstract class IHasCharacterAttributesContracts : IHasCharacterAttributes {

    #region IHasCharacterAttributes Members
    //******************************************************************************
    ICharacterAttributeCollection IHasCharacterAttributes.CharacterAttributes {
      get {
        Contract.Ensures(Contract.Result<ICharacterAttributeCollection>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion
  }
}