//-----------------------------------------------------------------------
// <copyright file="IEveCacheableContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IEveCacheable" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveCacheable))]
  internal abstract class IEveCacheableContracts : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        throw new NotImplementedException();
      }
    }
  }
}