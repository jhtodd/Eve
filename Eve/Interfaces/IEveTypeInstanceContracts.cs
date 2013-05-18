//-----------------------------------------------------------------------
// <copyright file="IEveTypeInstanceContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IEveTypeInstance" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveTypeInstance))]
  internal abstract class IEveTypeInstanceContracts : IEveTypeInstance
  {
    EveTypeId IEveTypeInstance.Id
    {
      get { throw new NotImplementedException(); }
    }

    EveType IEveTypeInstance.Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);
        throw new NotImplementedException();
      }
    }
  }
}