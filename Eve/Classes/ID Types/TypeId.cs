﻿//-----------------------------------------------------------------------
// <copyright file="TypeId.cs" company="Jeremy H. Todd">
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
  /// Represents an ID value for the <see cref="EveType" /> class.
  /// </summary>
  public struct TypeId : IEquatable<TypeId> {

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
    public static bool operator ==(TypeId left, TypeId right) {
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
    public static bool operator !=(TypeId left, TypeId right) {
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
    public static implicit operator TypeId(int value) {
      return new TypeId(value);
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
    public static implicit operator int(TypeId value) {
      return value.Value;
    }
    #endregion

    #region Instance Fields
    private int _value;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the TypeId structure.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value of the structure.
    /// </param>
    public TypeId(int value) {
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

      if (!typeof(TypeId).IsAssignableFrom(obj.GetType())) {
        return false;
      }

      return Equals((TypeId) obj);
    }
    //******************************************************************************
    /// <inheritdoc />
    public bool Equals(TypeId other) {
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