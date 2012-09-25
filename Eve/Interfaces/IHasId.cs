//-----------------------------------------------------------------------
// <copyright file="IHasId.cs" company="Jeremy H. Todd">
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
  /// The base interface for a data source which provides access to the EVE
  /// database.
  /// </summary>
  [ContractClass(typeof(IHasIdContracts))]
  public interface IHasId {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    object Id { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// The base interface for a data source which provides access to the EVE
  /// database.
  /// </summary>
  [ContractClass(typeof(IHasIdContracts<>))]
  public interface IHasId<out TId> : IHasId {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    new TId Id { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IHasId" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasId))]
  internal abstract class IHasIdContracts : IHasId {

    #region IHasId<TId> Members
    //******************************************************************************
    object IHasId.Id {
      get {
        Contract.Ensures(Contract.Result<object>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IHasId{TId}" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasId<>))]
  internal abstract class IHasIdContracts<TId> : IHasId<TId> {

    #region IHasId<TId> Members
    //******************************************************************************
    TId IHasId<TId>.Id {
      get {
        Contract.Ensures(Contract.Result<TId>() != null);
        throw new NotImplementedException(); 
      }
    }
    #endregion

    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get { throw new NotImplementedException(); }
    }
    #endregion
  }
}