//-----------------------------------------------------------------------
// <copyright file="ICharacterAttributeCollectionContracts.cs" company="Jeremy H. Todd">
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
  using FreeNet.Collections;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contract class for the <see cref="ICharacterAttributeCollection" /> interface.
  /// </summary>
  [ContractClassFor(typeof(ICharacterAttributeCollection))]
  internal abstract partial class ICharacterAttributeCollectionContracts : ICharacterAttributeCollection
  {
    ICharacterAttribute ICharacterAttributeCollection.this[CharacterAttributeId characterAttributeId]
    {
      get
      {
        Contract.Ensures(Contract.Result<ICharacterAttribute>() != null);
        throw new NotImplementedException();
      }
    }

    bool ICharacterAttributeCollection.ContainsKey(CharacterAttributeId characterAttributeId)
    {
      throw new NotImplementedException();
    }

    double ICharacterAttributeCollection.GetCharacterAttributeValue(CharacterAttributeId id)
    {
      Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
      Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
      throw new NotImplementedException();
    }

    bool ICharacterAttributeCollection.TryGetValue(CharacterAttributeId characterAttributeId, out ICharacterAttribute value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
  }

  #region IEnumerable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable" /> interface.
  /// </content>
  internal abstract partial class ICharacterAttributeCollectionContracts : IEnumerable
  {
    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IEnumerable<ICharacterAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable{T}" /> interface.
  /// </content>
  internal abstract partial class ICharacterAttributeCollectionContracts : IEnumerable<ICharacterAttribute>
  {
    IEnumerator<ICharacterAttribute> IEnumerable<ICharacterAttribute>.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IReadOnlyCollection<ICharacterAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyCollection{T}" /> interface.
  /// </content>
  internal abstract partial class ICharacterAttributeCollectionContracts : IReadOnlyCollection<ICharacterAttribute>
  {
    int IReadOnlyCollection<ICharacterAttribute>.Count
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion

  #region IReadOnlyList<ICharacterAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyList{T}" /> interface.
  /// </content>
  internal abstract partial class ICharacterAttributeCollectionContracts : IReadOnlyList<ICharacterAttribute>
  {
    ICharacterAttribute IReadOnlyList<ICharacterAttribute>.this[int index]
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion
}