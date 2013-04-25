//-----------------------------------------------------------------------
// <copyright file="CertificateId.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;

  /// <summary>
  /// Represents an ID value for the <see cref="Certificate" /> class.
  /// </summary>
  public partial struct CertificateId 
    : IConvertible,
      IEquatable<CertificateId>
  {
    private readonly int value;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateId structure.
    /// </summary>
    /// <param name="value">
    /// The value of the structure.
    /// </param>
    public CertificateId(int value)
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
    public static bool operator ==(CertificateId left, CertificateId right)
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
    public static bool operator !=(CertificateId left, CertificateId right)
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
    public static implicit operator CertificateId(int value)
    {
      return new CertificateId(value);
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
    public static implicit operator int(CertificateId value)
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

      if (!typeof(CertificateId).IsAssignableFrom(obj.GetType()))
      {
        return false;
      }

      return this.Equals((CertificateId)obj);
    }

    /// <inheritdoc />
    public bool Equals(CertificateId other)
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
  public partial struct CertificateId : IConvertible
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