//-----------------------------------------------------------------------
// <copyright file="IAttributeCollectionContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Contract class for the <see cref="IAttributeCollection" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IAttributeCollection))]
  internal abstract partial class IAttributeCollectionContracts : IAttributeCollection
  {
    IAttribute IAttributeCollection.this[AttributeId attributeId]
    {
      get
      {
        Contract.Ensures(Contract.Result<IAttribute>() != null);
        throw new NotImplementedException();
      }
    }

    bool IAttributeCollection.ContainsKey(AttributeId attributeId)
    {
      throw new NotImplementedException();
    }

    double IAttributeCollection.GetAttributeValue(AttributeId id)
    {
      Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
      Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
      throw new NotImplementedException();
    }

    double IAttributeCollection.GetAttributeValue(AttributeId id, double defaultValue)
    {
      throw new NotImplementedException();
    }

    T IAttributeCollection.GetAttributeValue<T>(AttributeId id)
    {
      throw new NotImplementedException();
    }

    T IAttributeCollection.GetAttributeValue<T>(AttributeId id, T defaultValue)
    {
      throw new NotImplementedException();
    }

    bool IAttributeCollection.TryGetValue(AttributeId attributeId, out IAttribute value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
  }

  #region IEnumerable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable" /> interface.
  /// </content>
  internal abstract partial class IAttributeCollectionContracts : IEnumerable
  {
    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IEnumerable<IAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable{T}" /> interface.
  /// </content>
  internal abstract partial class IAttributeCollectionContracts : IEnumerable<IAttribute>
  {
    IEnumerator<IAttribute> IEnumerable<IAttribute>.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  #region IReadOnlyCollection<IAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyCollection{T}" /> interface.
  /// </content>
  internal abstract partial class IAttributeCollectionContracts : IReadOnlyCollection<IAttribute>
  {
    int IReadOnlyCollection<IAttribute>.Count
    {
      get { throw new NotImplementedException(); }
    }
  }
  #endregion

  #region IReadOnlyList<IAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyList{T}" /> interface.
  /// </content>
  internal abstract partial class IAttributeCollectionContracts : IReadOnlyList<IAttribute>
  {
    IAttribute IReadOnlyList<IAttribute>.this[int index]
    {
      get
      {
        throw new NotImplementedException();
      }
    }
  }
  #endregion
}