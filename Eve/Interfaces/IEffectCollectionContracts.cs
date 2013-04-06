//-----------------------------------------------------------------------
// <copyright file="IEffectCollectionContracts.cs" company="Jeremy H. Todd">
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
  using FreeNet.Collections;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contract class for the <see cref="IEffectCollection" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEffectCollection))]
  internal abstract partial class IEffectCollectionContracts : IEffectCollection
  {
    IEffect IEffectCollection.this[EffectId effectId]
    {
      get
      {
        Contract.Ensures(Contract.Result<IEffect>() != null);
        throw new NotImplementedException();
      }
    }

    bool IEffectCollection.ContainsKey(EffectId effectId)
    {
      throw new NotImplementedException();
    }

    bool IEffectCollection.TryGetValue(EffectId effectId, out IEffect value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
  }

  #region IEnumerable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable" /> interface.
  /// </content>
  internal abstract partial class IEffectCollectionContracts : IEnumerable
  {
    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IEnumerable<IEffect> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable{T}" /> interface.
  /// </content>
  internal abstract partial class IEffectCollectionContracts : IEnumerable<IEffect>
  {
    IEnumerator<IEffect> IEnumerable<IEffect>.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IReadOnlyCollection<IEffect> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyCollection{T}" /> interface.
  /// </content>
  internal abstract partial class IEffectCollectionContracts : IReadOnlyCollection<IEffect>
  {
    int IReadOnlyCollection<IEffect>.Count
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion

  #region IReadOnlyList<IEffect> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyList{T}" /> interface.
  /// </content>
  internal abstract partial class IEffectCollectionContracts : IReadOnlyList<IEffect>
  {
    IEffect IReadOnlyList<IEffect>.this[int index]
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion
}