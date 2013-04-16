//-----------------------------------------------------------------------
// <copyright file="IHasSkillsContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IHasSkills" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IHasSkills))]
  internal abstract class IHasSkillsContracts : IHasSkills
  {
    ISkillCollection IHasSkills.Skills
    {
      get
      {
        Contract.Ensures(Contract.Result<ISkillCollection>() != null);
        throw new NotImplementedException();
      }
    }
  }
}