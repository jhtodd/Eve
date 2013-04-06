//-----------------------------------------------------------------------
// <copyright file="IAttributeContracts.cs" company="Jeremy H. Todd">
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
  /// Contract class for the <see cref="IAttribute" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IAttribute))]
  internal abstract class IAttributeContracts : IAttribute
  {
    double IAttribute.BaseValue
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        throw new NotImplementedException();
      }
    }

    AttributeId IAttribute.Id
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    AttributeType IAttribute.Type
    {
      get
      {
        Contract.Ensures(Contract.Result<AttributeType>() != null);
        throw new NotImplementedException();
      }
    }
  }
}