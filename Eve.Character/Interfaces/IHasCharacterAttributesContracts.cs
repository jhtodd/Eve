//-----------------------------------------------------------------------
// <copyright file="IHasCharacterAttributesContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IHasCharacterAttributes" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasCharacterAttributes))]
  internal abstract class IHasCharacterAttributesContracts : IHasCharacterAttributes
  {
    ICharacterAttributeCollection IHasCharacterAttributes.CharacterAttributes
    {
      get
      {
        Contract.Ensures(Contract.Result<ICharacterAttributeCollection>() != null);
        throw new NotImplementedException();
      }
    }
  }
}