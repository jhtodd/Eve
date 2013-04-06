//-----------------------------------------------------------------------
// <copyright file="IHasEffectsContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
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
  /// Contract class for the <see cref="IHasEffects" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasEffects))]
  internal abstract class IHasEffectsContracts : IHasEffects
  {
    IEffectCollection IHasEffects.Effects
    {
      get
      {
        Contract.Ensures(Contract.Result<IEffectCollection>() != null);
        throw new NotImplementedException();
      }
    }
  }
}