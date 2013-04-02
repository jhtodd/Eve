//-----------------------------------------------------------------------
// <copyright file="ICharacterAttributeCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character {
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

  //******************************************************************************
  /// <summary>
  /// The base interface for collections of EVE character attributes.
  /// </summary>
  [ContractClass(typeof(ICharacterAttributeCollectionContracts))]
  public interface ICharacterAttributeCollection : IReadOnlyList<ICharacterAttribute> {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the character attribute with the specified ID.
    /// </summary>
    /// 
    /// <param name="characterAttributeId">
    /// The ID of the character attribute to retrieve.
    /// </param>
    /// 
    /// <returns>
    /// The character attribute with the specified ID.
    /// </returns>
    /// 
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no character attribute with the specified ID was found in the
    /// collection.
    /// </exception>
    ICharacterAttribute this[CharacterAttributeId characterAttributeId] { get; }
    #endregion
    #region Interface Methods
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether a character attribute with the specified
    /// ID is contained in the collection.
    /// </summary>
    /// 
    /// <param name="characterAttributeId">
    /// The ID of the character attribute to locate.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if a character attribute with the specified ID is
    /// contained in the collection; otherwise <see langword="false" />.
    /// </returns>
    bool ContainsKey(CharacterAttributeId characterAttributeId);
    //******************************************************************************
    /// <summary>
    /// Gets the value of the specified character attribute, or 0 if the specified
    /// character attribute is not contained in the collection.
    /// </summary>
    /// 
    /// <param name="characterAttributeId">
    /// The ID of the character attribute whose value to retrieve.
    /// </param>
    /// 
    /// <returns>
    /// The value of the specified character attribute.
    /// </returns>
    double GetCharacterAttributeValue(CharacterAttributeId characterAttributeId);
    //******************************************************************************
    /// <summary>
    /// Attempts to retrieve the character attribute with the specified ID,
    /// returning success or failure.
    /// </summary>
    /// 
    /// <param name="characterAttributeId">
    /// The ID of the character attribute to retrieve.
    /// </param>
    /// 
    /// <param name="value">
    /// Output parameter.  Will contain the specified character attribute if found.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if the specified character attribute was found;
    /// otherwise <see langword="false" />.
    /// </returns>
    bool TryGetValue(CharacterAttributeId characterAttributeId, out ICharacterAttribute value);
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="ICharacterAttributeCollection" /> interface.
  /// </summary>
  [ContractClassFor(typeof(ICharacterAttributeCollection))]
  internal abstract class ICharacterAttributeCollectionContracts : ICharacterAttributeCollection {

    #region ICharacterAttributeCollection Members
    //******************************************************************************
    ICharacterAttribute ICharacterAttributeCollection.this[CharacterAttributeId characterAttributeId] {
      get {
        Contract.Ensures(Contract.Result<ICharacterAttribute>() != null);
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    bool ICharacterAttributeCollection.ContainsKey(CharacterAttributeId characterAttributeId) {
      throw new NotImplementedException();
    }
    //******************************************************************************
    double ICharacterAttributeCollection.GetCharacterAttributeValue(CharacterAttributeId id) {
      Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
      Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
      throw new NotImplementedException();
    }
    //******************************************************************************
    bool ICharacterAttributeCollection.TryGetValue(CharacterAttributeId characterAttributeId, out ICharacterAttribute value) {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      throw new NotImplementedException();
    }
    #endregion

    #region IEnumerable Members
    //******************************************************************************
    IEnumerator IEnumerable.GetEnumerator() {
      throw new NotImplementedException();
    }
    #endregion
    #region IEnumerable<ICharacterAttribute> Members
    //******************************************************************************
    IEnumerator<ICharacterAttribute> IEnumerable<ICharacterAttribute>.GetEnumerator() {
      throw new NotImplementedException();
    }
    #endregion
    #region IReadOnlyCollection<ICharacterAttribute> Members
    //******************************************************************************
    int IReadOnlyCollection<ICharacterAttribute>.Count {
      get { throw new NotImplementedException(); }
    }
    #endregion
    #region IReadOnlyList<ICharacterAttribute> Members
    //******************************************************************************
    ICharacterAttribute IReadOnlyList<ICharacterAttribute>.this[int index] {
      get { throw new NotImplementedException(); }
    }
    #endregion
  }
}