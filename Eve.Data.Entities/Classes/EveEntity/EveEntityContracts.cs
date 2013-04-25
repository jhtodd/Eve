//-----------------------------------------------------------------------
// <copyright file="EveEntityContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="EveEntity" /> abstract class.
  /// </summary>
  [ContractClassFor(typeof(EveEntity))]
  internal abstract class EveEntityContracts : EveEntity
  {
    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get 
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        throw new NotImplementedException(); 
      }
    }
  }
}