//-----------------------------------------------------------------------
// <copyright file="IHasAttributesContracts.cs" company="Jeremy H. Todd">
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