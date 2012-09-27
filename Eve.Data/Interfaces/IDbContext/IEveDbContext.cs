//-----------------------------------------------------------------------
// <copyright file="IEveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Entities;

  //******************************************************************************
  /// <summary>
  /// The base interface for objects that provide data access and change tracking
  /// for the application.
  /// </summary>
  [ContractClass(typeof(IEveDbContextContracts))]
  public interface IEveDbContext : IDbContext {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the <see cref="DbSet{T}" /> for icons.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for icons.
    /// </value>
    IDbSet<IconEntity> Icons { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IEveDbContext" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveDbContext))]
  internal abstract class IEveDbContextContracts : IEveDbContext {

    #region IEveDbContext Members
    //******************************************************************************
    IDbSet<IconEntity> IEveDbContext.Icons {
      get {
        Contract.Ensures(Contract.Result<IDbSet<IconEntity>>() != null);
        throw new NotImplementedException();
      }
    }
    #endregion

    #region IDbContext Members
    //******************************************************************************
    int IDbContext.SaveChanges() {
      throw new NotImplementedException();
    }
    //******************************************************************************
    IDbSet<TEntity> IDbContext.Set<TEntity>() {
      throw new NotImplementedException();
    }
    #endregion
    #region IDisposable Members
    //******************************************************************************
    void IDisposable.Dispose() {
      throw new NotImplementedException();
    }
    #endregion
  }
}