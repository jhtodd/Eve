//-----------------------------------------------------------------------
// <copyright file="CertificateRelationshipId.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;

  /// <summary>
  /// Represents an ID value for the <see cref="CertificateRelationship" /> class.
  /// </summary>
  public partial struct CertificateRelationshipId 
    : IConvertible,
      IEquatable<CertificateRelationshipId>
  {
    private readonly int value;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateRelationshipId structure.
    /// </summary>
    /// <param name="value">
    /// The value of the structure.
    /// </param>
    public CertificateRelationshipId(int value)
    {
      this.value = value;
    }

    /* Properties */

    /// <summary>
    /// Gets the ID value.
    /// </summary>
    /// <value>
    /// The ID value.
    /// </value>
    public int Value
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
    public static bool operator ==(CertificateRelationshipId left, CertificateRelationshipId right)
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
    public static bool operator !=(CertificateRelationshipId left, CertificateRelationshipId right)
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
    public static implicit operator CertificateRelationshipId(int value)
    {
      return new CertificateRelationshipId(value);
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
    public static implicit operator int(CertificateRelationshipId value)
    {
      return value.Value;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (!typeof(CertificateRelationshipId).IsAssignableFrom(obj.GetType()))
      {
        return false;
      }

      return this.Equals((CertificateRelationshipId)obj);
    }

    /// <inheritdoc />
    public bool Equals(CertificateRelationshipId other)
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

  #region IConvertible Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IConvertible" /> interface.
  /// </content>
  public partial struct CertificateRelationshipId : IConvertible
  {
    TypeCode IConvertible.GetTypeCode()
    {
      return this.Value.GetTypeCode();
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToBoolean(provider);
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToByte(provider);
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToChar(provider);
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToDateTime(provider);
    }

    decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToDecimal(provider);
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToDouble(provider);
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToInt16(provider);
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToInt32(provider);
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToInt64(provider);
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToSByte(provider);
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToSingle(provider);
    }

    string IConvertible.ToString(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToString(provider);
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToType(conversionType, provider);
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToUInt16(provider);
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToUInt32(provider);
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      return ((IConvertible)this.Value).ToUInt64(provider);
    }
  }
  #endregion
}