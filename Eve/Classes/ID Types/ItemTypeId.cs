//-----------------------------------------------------------------------
// <copyright file="ItemTypeId.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Diagnostics.Contracts;

  using FreeNet;

  //******************************************************************************
  /// <summary>
  /// Represents an ID value for the <see cref="Icon" /> class.
  /// </summary>
  public struct ItemTypeId : IEquatable<ItemTypeId> {

    #region Operators
    //******************************************************************************
    /// <summary>
    /// The equality operator.
    /// </summary>
    /// 
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// 
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if the two values are equal; otherwise
    /// <see langword="false" />.
    /// </returns>
    public static bool operator ==(ItemTypeId left, ItemTypeId right) {
      return left.Value.Equals(right.Value);
    }
    //******************************************************************************
    /// <summary>
    /// The inequality operator.
    /// </summary>
    /// 
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// 
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if the two values are unequal; otherwise
    /// <see langword="false" />.
    /// </returns>
    public static bool operator !=(ItemTypeId left, ItemTypeId right) {
      return !left.Value.Equals(right.Value);
    }
    //******************************************************************************
    /// <summary>
    /// Implicit conversion from another type.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value to convert.
    /// </param>
    /// 
    /// <returns>
    /// The converted value.
    /// </returns>
    public static implicit operator ItemTypeId(int value) {
      return new ItemTypeId(value);
    }
    //******************************************************************************
    /// <summary>
    /// Implicit conversion to another type.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value to convert.
    /// </param>
    /// 
    /// <returns>
    /// The converted value.
    /// </returns>
    public static implicit operator int(ItemTypeId value) {
      return value.Value;
    }
    #endregion

    #region Instance Fields
    int _value;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ItemTypeId structure.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value of the structure.
    /// </param>
    public ItemTypeId(int value) {
      _value = value;
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID value.
    /// </summary>
    /// 
    /// <value>
    /// The ID value.
    /// </value>
    public int Value {
      get {
        return _value;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      if (obj == null) {
        return false;
      }

      if (!typeof(ItemTypeId).IsAssignableFrom(obj.GetType())) {
        return false;
      }

      return Equals((ItemTypeId) obj);
    }
    //******************************************************************************
    /// <inheritdoc />
    public bool Equals(ItemTypeId other) {
      return Value.Equals(other.Value);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return Value.GetHashCode();
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return Value.ToString();
    }
    #endregion
  }
}