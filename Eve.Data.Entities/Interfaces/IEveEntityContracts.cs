//-----------------------------------------------------------------------
// <copyright file="IEveEntityContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IEveEntity" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveEntity))]
  internal abstract class IEveEntityContracts : IEveEntity
  {
    IConvertible IEveEntity.CacheKey
    {
      get 
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        throw new NotImplementedException(); 
      }
    }
  }
}