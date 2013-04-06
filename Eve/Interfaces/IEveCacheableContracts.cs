//-----------------------------------------------------------------------
// <copyright file="IEveCacheableContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
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
  /// Contract class for the <see cref="IEveCacheable" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveCacheable))]
  internal abstract class IEveCacheableContracts : IEveCacheable
  {
    object IEveCacheable.CacheKey
    {
      get
      {
        Contract.Ensures(Contract.Result<object>() != null);
        throw new NotImplementedException();
      }
    }
  }
}