//-----------------------------------------------------------------------
// <copyright file="IAttribute.cs" company="Jeremy H. Todd">
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
  /// The base interface for classes that describe the value of an EVE attribute.
  /// </summary>
  [ContractClass(typeof(IAttributeContracts))]
  public interface IAttribute {

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
    AttributeId Id { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The type of the attribute.
    /// </value>
    AttributeType Type { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IAttribute" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IAttribute))]
  internal abstract class IAttributeContracts : IAttribute {

    #region IAttribute Members
    //******************************************************************************
    double IAttribute.BaseValue {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    AttributeId IAttribute.Id {
      get {
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    AttributeType IAttribute.Type {
      get {
        Contract.Ensures(Contract.Result<AttributeType>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion
  }
}