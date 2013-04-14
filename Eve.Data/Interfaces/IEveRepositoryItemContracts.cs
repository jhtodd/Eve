//-----------------------------------------------------------------------
// <copyright file="IEveRepositoryItemContracts.cs" company="Jeremy H. Todd">
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
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contract class for the <see cref="IEveRepositoryItem" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveRepositoryItem))]
  internal abstract class IEveRepositoryItemContracts : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Container
    {
      get 
      {
        Contract.Ensures(Contract.Result<IEveRepository>() != null);
        throw new NotImplementedException(); 
      }
    }
  }
}