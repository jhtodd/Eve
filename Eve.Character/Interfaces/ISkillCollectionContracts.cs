//-----------------------------------------------------------------------
// <copyright file="ISkillCollectionContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="ISkillCollection" /> interface.
  /// </summary>
  [ContractClassFor(typeof(ISkillCollection))]
  internal abstract partial class ISkillCollectionContracts : ISkillCollection
  {
    ISkill ISkillCollection.this[SkillId skillId]
    {
      get
      {
        Contract.Ensures(Contract.Result<ISkill>() != null);
        throw new NotImplementedException();
      }
    }

    bool ISkillCollection.ContainsKey(SkillId skillId)
    {
      throw new NotImplementedException();
    }

    byte ISkillCollection.GetSkillLevel(SkillId skillId)
    {
      Contract.Ensures(Contract.Result<byte>() <= SkillType.MaxSkillLevel);
      throw new NotImplementedException();
    }

    bool ISkillCollection.TryGetValue(SkillId skillId, out ISkill value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
  }

  #region IEnumerable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable" /> interface.
  /// </content>
  internal abstract partial class ISkillCollectionContracts : IEnumerable
  {
    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IEnumerable<ISkill> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable{T}" /> interface.
  /// </content>
  internal abstract partial class ISkillCollectionContracts : IEnumerable<ISkill>
  {
    IEnumerator<ISkill> IEnumerable<ISkill>.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IReadOnlyCollection<ISkill> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyCollection{T}" /> interface.
  /// </content>
  internal abstract partial class ISkillCollectionContracts : IReadOnlyCollection<ISkill>
  {
    int IReadOnlyCollection<ISkill>.Count
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion

  #region IReadOnlyList<ISkill> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyList{T}" /> interface.
  /// </content>
  internal abstract partial class ISkillCollectionContracts : IReadOnlyList<ISkill>
  {
    ISkill IReadOnlyList<ISkill>.this[int index]
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion
}