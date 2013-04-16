//-----------------------------------------------------------------------
// <copyright file="IHasEffectsContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Diagnostics.Contracts;

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