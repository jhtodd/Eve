//-----------------------------------------------------------------------
// <copyright file="IHasAttributes.cs" company="Jeremy H. Todd">
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
  /// The base interface for classes which possess a collection of EVE attributes.
  /// </summary>
  [ContractClass(typeof(IHasAttributesContracts))]
  public interface IHasAttributes {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the collection of attributes that apply to the item.
    /// </summary>
    /// 
    /// <value>
    /// The collection of attributes that apply to the item.
    /// </value>
    IAttributeCollection Attributes { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IHasAttributes" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasAttributes))]
  internal abstract class IHasAttributesContracts : IHasAttributes {

    #region IHasAttributes Members
    //******************************************************************************
    IAttributeCollection IHasAttributes.Attributes {
      get {
        Contract.Ensures(Contract.Result<IAttributeCollection>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion
  }
}