//-----------------------------------------------------------------------
// <copyright file="AttributeValue.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// Contains information about the value of an EVE attribute.
  /// </summary>>
  [Table("dgmTypeAttributes")]
  public class AttributeValue : ImmutableEntity,
                                IComparable,
                                IComparable<AttributeValue>,
                                IEquatable<AttributeValue>,
                                IKeyItem<AttributeId>,
                                IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private AttributeId _attributeId;
    private int _itemTypeId;
    private double _value;

    private AttributeType _innerAttributeType;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeValue class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public AttributeValue() {
      _value = double.NaN;

      Contract.Assume(!double.IsInfinity(_value)); // Shouldn't be needed, but is
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeValue class.
    /// </summary>
    /// 
    /// <param name="attributeId">
    /// The ID of the attribute associated with the value.
    /// </param>
    /// 
    /// <param name="itemTypeId">
    /// The ID of the item type the value describes.
    /// </param>
    /// 
    /// <param name="value">
    /// The numeric value of the attribute, or <see cref="double.NaN" /> to use
    /// the default value defined by the attribute type.
    /// </param>
    public AttributeValue(AttributeId attributeId, int itemTypeId, double value) {
      Contract.Requires(!double.IsInfinity(value), Resources.Messages.AttributeValue_ValueMustBeNumeric);

      _attributeId = attributeId;
      _itemTypeId = itemTypeId;
      _value = value;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(!double.IsInfinity(_value));
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute the current value applies to.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the attribute the current value applies to.
    /// </value>
    [Column("attributeID")]
    [Key()]
    public AttributeId AttributeId {
      get {
        return _attributeId;
      }
      private set {
        _attributeId = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute type to which the value applies.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> to which the value applies.
    /// </value>
    public AttributeType AttributeType {
      get {
        Contract.Ensures(Contract.Result<AttributeType>() != null);

        AttributeType result = InnerAttributeType;
        Contract.Assume(result != null);
        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item type the value describes.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the item type the value describes.
    /// </value>
    [Column("typeID")]
    [Key()]
    public int ItemTypeId {
      get {
        return _itemTypeId;
      }
      private set {
        _itemTypeId = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the value of the attribute before any modifiers are applied.
    /// </summary>
    /// 
    /// <value>
    /// The value of the attribute before any modifiers are applied.
    /// </value>
    public double Value {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        // If _value is NaN, then it hasn't been set -- return the default
        // value for the attribute.  This is a trick so we don't have to 
        // load the attribute type unless absolutely necessary.
        if (double.IsNaN(_value)) {
          _value = AttributeType.DefaultValue;
        }

        return _value;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(AttributeValue other) {
      if (other == null) {
        return 1;
      }

      int result = AttributeType.Name.CompareTo(other.AttributeType.Name);

      if (result == 0) {
        result = Value.CompareTo(other.Value);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as AttributeValue);
    }
    //******************************************************************************
    /// <summary>
    /// Returns a value indicating whether the current instance is equal to the
    /// specified object.
    /// </summary>
    /// 
    /// <param name="obj">
    /// The object to compare to the current object.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> is equal to the current
    /// instance; otherwise <see langword="false" />.
    /// </returns>
    public virtual bool Equals(AttributeValue other) {
      if (other == null) {
        return false;
      }

      return AttributeId.Equals(other.AttributeId) && Value.Equals(other.Value);
    }
    //******************************************************************************
    /// <summary>
    /// Formats the current value according to the attribute type.
    /// </summary>
    /// 
    /// <param name="format">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the attribute value.
    /// </returns>
    public string FormatValue() {
      return FormatValue(string.Empty);
    }
    //******************************************************************************
    /// <summary>
    /// Formats the current value according to the attribute type.
    /// </summary>
    /// 
    /// <param name="format">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the attribute value.
    /// </returns>
    public string FormatValue(string format) {

      // If the attribute has a unit, use it for the formatting
      if (AttributeType.UnitId != null && AttributeType.Unit != null) {
        return AttributeType.Unit.FormatValue(Value, format);
      }

      // Otherwise, just format the number
      return Value.ToString(format);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return FreeNet.Methods.GetCompoundHashCode(AttributeId, Value);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return ToString(string.Empty);
    }
    //******************************************************************************
    /// <inheritdoc />
    /// 
    /// <param name="format">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the attribute value.
    /// </returns>
    public string ToString(string format) {

      // If the attribute has a unit, use it for the formatting
      if (AttributeType.UnitId != null && AttributeType.Unit != null) {
        return AttributeType.Name + " = " + AttributeType.Unit.FormatValue(Value, format);
      }

      // Otherwise, just format the number
      return AttributeType.Name + " = " + Value.ToString(format);
    }
    #endregion
    #region Protected Internal Properties
    //******************************************************************************
    /// <summary>
    /// Hidden mapped property to set the value as a floating-point number.
    /// </summary>
    /// 
    /// <value>
    /// The value expressed as an floating-point number.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// The source table contains two columns: valueInt and valueFloat.  For
    /// every record, one column has a value and the other is null.  To 
    /// simplify mapping, we have two corresponding properties here.  Both
    /// assign a value to <see cref="Value" /> if they aren't passed a null
    /// reference.
    /// </para>
    /// </remarks>
    [Column("valueFloat")]
    protected internal double? ValueFloat {
      get {
        return Value;
      }
      set {
        Contract.Requires(value == null || !double.IsInfinity(value.Value), Resources.Messages.AttributeValue_ValueMustBeNumeric);
        Contract.Requires(value == null || !double.IsNaN(value.Value), Resources.Messages.AttributeValue_ValueMustBeNumeric);

        if (value == null) {
          return;
        }

        _value = value.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Hidden mapped property to set the value as an integer.
    /// </summary>
    /// 
    /// <value>
    /// The value expressed as an integer.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// The source table contains two columns: valueInt and valueFloat.  For
    /// every record, one column has a value and the other is null.  To 
    /// simplify mapping, we have two corresponding properties here.  Both
    /// assign a value to <see cref="Value" /> if they aren't passed a null
    /// reference.
    /// </para>
    /// </remarks>
    [Column("valueInt")]
    protected internal int? ValueInt {
      get {
        return Convert.ToInt32(Value);
      }
      set {
        if (value == null) {
          return;
        }

        double newValue = value.Value;
        Contract.Assume(!double.IsInfinity(newValue));
        Contract.Assume(!double.IsNaN(newValue));

        _value = newValue;
      }
    }
    #endregion

    #region Hidden Navigation Properties
    //******************************************************************************
    /// <summary>
    /// Hidden navigation property backing for the <see cref="AttributeType" />
    /// property.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Necessary for required navigation properties so that the publicly accessible
    /// wrapper can enforce non-null ensures with Code Contracts.
    /// </para>
    /// </remarks>
    [ForeignKey("AttributeId")]
    protected internal virtual AttributeType InnerAttributeType {
      get {
        return _innerAttributeType;
      }
      set {
        Contract.Requires(value != null, Resources.Messages.AttributeValue_AttributeTypeCannotBeNull);
        _innerAttributeType = value;
      }
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      AttributeValue other = obj as AttributeValue;
      return CompareTo(other);
    }
    #endregion
    #region IHasIcon Members
    //******************************************************************************
    Icon IHasIcon.Icon {
      get {
        return AttributeType.Icon;
      }
    }
    //******************************************************************************
    int? IHasIcon.IconId {
      get {
        return AttributeType.IconId;
      }
    }
    #endregion
    #region IKeyItem<AttributeId> Members
    //******************************************************************************
    AttributeId IKeyItem<AttributeId>.Key {
      get {
        return AttributeId;
      }
    }
    #endregion
  }
}