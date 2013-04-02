//-----------------------------------------------------------------------
// <copyright file="NpcCorporationId.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Diagnostics.Contracts;

  using FreeNet;

  //******************************************************************************
  /// <summary>
  /// Represents an ID value for the <see cref="NpcCorporation" /> class.
  /// </summary>
  public struct NpcCorporationId : IEquatable<NpcCorporationId> {

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
    public static bool operator ==(NpcCorporationId left, NpcCorporationId right) {
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
    public static bool operator !=(NpcCorporationId left, NpcCorporationId right) {
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
    public static implicit operator NpcCorporationId(long value) {
      return new NpcCorporationId(value);
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
    public static implicit operator long(NpcCorporationId value) {
      return value.Value;
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
    public static implicit operator ItemId(NpcCorporationId value) {

      // NpcCorporationId "derives" from ItemId and can always be converted to it
      // (but not vice versa).
      return new ItemId(value.Value);
    }
    #endregion

    #region Instance Fields
    private long _value;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the NpcCorporationId structure.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value of the structure.
    /// </param>
    public NpcCorporationId(long value) {
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
    public long Value {
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

      if (!typeof(NpcCorporationId).IsAssignableFrom(obj.GetType())) {
        return false;
      }

      return Equals((NpcCorporationId) obj);
    }
    //******************************************************************************
    /// <inheritdoc />
    public bool Equals(NpcCorporationId other) {
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