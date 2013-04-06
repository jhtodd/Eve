//-----------------------------------------------------------------------
// <copyright file="IEffectContracts.cs" company="Jeremy H. Todd">
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
  /// Contract class for the <see cref="IEffect" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEffect))]
  internal abstract class IEffectContracts : IEffect
  {
    EffectId IEffect.Id
    {
      get { throw new NotImplementedException(); }
    }

    bool IEffect.IsDefault
    {
      get { throw new NotImplementedException(); }
    }

    EffectType IEffect.Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EffectType>() != null);
        throw new NotImplementedException();
      }
    }
  }
}