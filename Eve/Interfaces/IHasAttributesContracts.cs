//-----------------------------------------------------------------------
// <copyright file="IHasAttributesContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IHasAttributes" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasAttributes))]
  internal abstract class IHasAttributesContracts : IHasAttributes
  {
    IAttributeCollection IHasAttributes.Attributes
    {
      get
      {
        Contract.Ensures(Contract.Result<IAttributeCollection>() != null);
        throw new NotImplementedException();
      }
    }
  }
}