//-----------------------------------------------------------------------
// <copyright file="IEveRepositoryItemContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IEveRepositoryItem" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveRepositoryItem))]
  internal abstract class IEveRepositoryItemContracts : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Repository
    {
      get 
      {
        Contract.Ensures(Contract.Result<IEveRepository>() != null);
        throw new NotImplementedException(); 
      }
    }
  }
}