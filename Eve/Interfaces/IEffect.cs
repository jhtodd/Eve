//-----------------------------------------------------------------------
// <copyright file="IEffect.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
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
  /// The base interface for classes that describe an effect associated with an
  /// EVE item.
  /// </summary>
  [ContractClass(typeof(IEffectContracts))]
  public interface IEffect {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the effect.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the effect.
    /// </value>
    EffectId Id { get; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether it is the default effect for the item.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the effect is the default effect for the item;
    /// otherwise <see langword="false" />.
    /// </value>
    bool IsDefault { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the effect.
    /// </summary>
    /// 
    /// <value>
    /// The type of the effect.
    /// </value>
    EffectType Type { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IEffect" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEffect))]
  internal abstract class IEffectContracts : IEffect {

    #region IEffect Members
    //******************************************************************************
    EffectId IEffect.Id {
      get {
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    bool IEffect.IsDefault {
      get { throw new NotImplementedException(); }
    }
    //******************************************************************************
    EffectType IEffect.Type {
      get {
        Contract.Ensures(Contract.Result<EffectType>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion
  }
}