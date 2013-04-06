//-----------------------------------------------------------------------
// <copyright file="ConstellationId.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Diagnostics.Contracts;

  using FreeNet;

  /// <summary>
  /// Represents an ID value for the <see cref="Constellation" /> class.
  /// </summary>
  public struct ConstellationId : IEquatable<ConstellationId>
  {
    private readonly long value;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ConstellationId structure.
    /// </summary>
    /// <param name="value">
    /// The value of the structure.
    /// </param>
    public ConstellationId(long value)
    {
      this.value = value;
    }

    /* Constructors */
    
    /// <summary>
    /// Gets the ID value.
    /// </summary>
    /// <value>
    /// The ID value.
    /// </value>
    public long Value
    {
      get { return this.value; }
    }

    /* Methods */

    /// <summary>
    /// The equality operator.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the two values are equal; otherwise
    /// <see langword="false" />.
    /// </returns>
    public static bool operator ==(ConstellationId left, ConstellationId right)
    {
      return left.Value.Equals(right.Value);
    }

    /// <summary>
    /// The inequality operator.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the two values are unequal; otherwise
    /// <see langword="false" />.
    /// </returns>
    public static bool operator !=(ConstellationId left, ConstellationId right)
    {
      return !left.Value.Equals(right.Value);
    }

    /// <summary>
    /// Implicit conversion from another type.
    /// </summary>
    /// <param name="value">
    /// The value to convert.
    /// </param>
    /// <returns>
    /// The converted value.
    /// </returns>
    public static implicit operator ConstellationId(long value)
    {
      return new ConstellationId(value);
    }

    /// <summary>
    /// Implicit conversion to another type.
    /// </summary>
    /// <param name="value">
    /// The value to convert.
    /// </param>
    /// <returns>
    /// The converted value.
    /// </returns>
    public static implicit operator long(ConstellationId value)
    {
      return value.Value;
    }

    /// <summary>
    /// Implicit conversion from another type.
    /// </summary>
    /// <param name="value">
    /// The value to convert.
    /// </param>
    /// <returns>
    /// The converted value.
    /// </returns>
    public static implicit operator ItemId(ConstellationId value)
    {
      // ConstellationId "derives" from ItemId and can always be converted to it
      // (but not vice versa).
      return new ItemId(value.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (!typeof(ConstellationId).IsAssignableFrom(obj.GetType()))
      {
        return false;
      }

      return this.Equals((ConstellationId)obj);
    }

    /// <inheritdoc />
    public bool Equals(ConstellationId other)
    {
      return this.Value.Equals(other.Value);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this.Value.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Value.ToString();
    }
  }
}