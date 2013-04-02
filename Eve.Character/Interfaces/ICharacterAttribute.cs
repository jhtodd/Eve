//-----------------------------------------------------------------------
// <copyright file="ICharacterAttribute.cs" company="Jeremy H. Todd">
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
  /// The base interface for classes which describe an attribute belonging to
  /// an EVE character.
  /// </summary>
  [ContractClass(typeof(ICharacterAttributeContracts))]
  public interface ICharacterAttribute {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the base value of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The base value of the attribute.
    /// </value>
    double BaseValue { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the attribute.
    /// </value>
    CharacterAttributeId Id { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The type of the attribute.
    /// </value>
    CharacterAttributeType Type { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="ICharacterAttribute" /> interface.
  /// </summary>
  [ContractClassFor(typeof(ICharacterAttribute))]
  internal abstract class ICharacterAttributeContracts : ICharacterAttribute {

    #region ICharacterAttribute Members
    //******************************************************************************
    double ICharacterAttribute.BaseValue {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    CharacterAttributeId ICharacterAttribute.Id {
      get { 
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    CharacterAttributeType ICharacterAttribute.Type {
      get {
        Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion
  }
}