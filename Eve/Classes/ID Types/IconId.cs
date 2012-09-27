//-----------------------------------------------------------------------
// <copyright file="IconId.cs" company="Jeremy H. Todd">
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
  public struct IconId : IEquatable<IconId> {

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
    public static bool operator ==(IconId left, IconId right) {
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
    public static bool operator !=(IconId left, IconId right) {
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
    public static implicit operator IconId(int value) {
      return new IconId(value);
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
    public static implicit operator int(IconId value) {
      return value.Value;
    }
    #endregion

    #region Instance Fields
    int _value;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the IconId structure.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value of the structure.
    /// </param>
    public IconId(int value) {
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

      if (!typeof(IconId).IsAssignableFrom(obj.GetType())) {
        return false;
      }

      return Equals((IconId) obj);
    }
    //******************************************************************************
    /// <inheritdoc />
    public bool Equals(IconId other) {
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