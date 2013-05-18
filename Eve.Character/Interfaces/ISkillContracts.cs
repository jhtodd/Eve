//-----------------------------------------------------------------------
// <copyright file="ISkillContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="ISkill" /> interface.
  /// </summary>
  [ContractClassFor(typeof(ISkill))]
  internal abstract partial class ISkillContracts : ISkill
  {
    byte ISkill.Level
    {
      get
      {
        Contract.Ensures(Contract.Result<byte>() >= 0);
        Contract.Ensures(Contract.Result<byte>() <= SkillType.MaxSkillLevel);

        throw new NotImplementedException();
      }
    }

    SkillId ISkill.Id
    {
      get { throw new NotImplementedException(); }
    }

    int ISkill.SkillPoints
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        throw new NotImplementedException();
      }
    }

    SkillType ISkill.Type
    {
      get
      {
        Contract.Ensures(Contract.Result<SkillType>() != null);

        throw new NotImplementedException();
      }
    }
  }

  #region IEveTypeInstance Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveTypeInstance" /> interface.
  /// </content>
  internal abstract partial class ISkillContracts : IEveTypeInstance
  {
    EveTypeId IEveTypeInstance.Id
    {
      get { throw new NotImplementedException(); }
    }

    EveType IEveTypeInstance.Type
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion
}