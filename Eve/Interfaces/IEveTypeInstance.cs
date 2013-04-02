//-----------------------------------------------------------------------
// <copyright file="IEveTypeInstance.cs" company="Jeremy H. Todd">
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
  /// The base interface for classes that describe instances of an EVE type
  /// (e.g. skills, blueprints, modules, etc.).
  /// </summary>
  [ContractClass(typeof(IEveTypeInstanceContracts))]
  public interface IEveTypeInstance {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the item.
    /// </value>
    TypeId Id { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the item.
    /// </summary>
    /// 
    /// <value>
    /// The type of the item.
    /// </value>
    EveType Type { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IEveTypeInstance" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveTypeInstance))]
  internal abstract class IEveTypeInstanceContracts : IEveTypeInstance {

    #region IEveTypeInstance Members
    //******************************************************************************
    TypeId IEveTypeInstance.Id {
      get { 
        throw new NotImplementedException(); 
      }
    }
    //******************************************************************************
    EveType IEveTypeInstance.Type {
      get {
        Contract.Ensures(Contract.Result<EveType>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion
  }
}