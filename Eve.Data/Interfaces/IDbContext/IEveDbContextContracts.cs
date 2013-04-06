//-----------------------------------------------------------------------
// <copyright file="IEveDbContextContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contract class for the <see cref="IEveDbContext" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveDbContext))]
  internal abstract partial class IEveDbContextContracts : IEveDbContext
  {
    IDbSet<IconEntity> IEveDbContext.Icons
    {
      get
      {
        Contract.Ensures(Contract.Result<IDbSet<IconEntity>>() != null);
        throw new NotImplementedException();
      }
    }
  }

  #region IDbContext Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IDbContext" /> interface.
  /// </content>
  internal abstract partial class IEveDbContextContracts : IDbContext
  {
    int IDbContext.SaveChanges()
    {
      throw new NotImplementedException();
    }

    IDbSet<TEntity> IDbContext.Set<TEntity>()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IDisposable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IDisposable" /> interface.
  /// </content>
  internal abstract partial class IEveDbContextContracts : IDisposable
  {
    void IDisposable.Dispose()
    {
      throw new NotImplementedException();
    }
  }
  #endregion
}