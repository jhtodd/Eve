﻿//-----------------------------------------------------------------------
// <copyright file="ICharacterAttributeContracts.cs" company="Jeremy H. Todd">
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
  /// Contract class for the <see cref="ICharacterAttribute" /> interface.
  /// </summary>
  [ContractClassFor(typeof(ICharacterAttribute))]
  internal abstract class ICharacterAttributeContracts : ICharacterAttribute
  {
    double ICharacterAttribute.BaseValue
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);
        throw new NotImplementedException();
      }
    }

    CharacterAttributeId ICharacterAttribute.Id
    {
      get { throw new NotImplementedException(); }
    }

    CharacterAttributeType ICharacterAttribute.Type
    {
      get
      {
        Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);
        throw new NotImplementedException();
      }
    }
  }
}