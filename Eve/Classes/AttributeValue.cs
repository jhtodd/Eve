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
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Extensions;

  using Eve.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about the value of an EVE attribute.
  /// </summary>
  public class AttributeValue : EntityAdapter<AttributeValueEntity>,
                                IComparable,
                                IComparable<AttributeValue>,
                                IEquatable<AttributeValue>,
                                IHasIcon,
                                IHasId<long>,
                                IKeyItem<AttributeId> {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// 
    /// <param name="itemTypeId">
    /// The item type ID.
    /// </param>
    /// 
    /// <param name="attributeId">
    /// The attribute ID.
    /// </param>
    /// 
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(ItemTypeId itemTypeId, AttributeId attributeId) {
      return (long) ((((ulong) (long) itemTypeId) << 32) | ((ulong) (long) attributeId));
    }
    #endregion

    #region Instance Fields
    private AttributeType _attributeType;
    private ItemType _itemType;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeValue class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public AttributeValue(AttributeValueEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
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
    public AttributeId AttributeId {
      get {
        return Entity.AttributeId;
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

        if (_attributeType == null) {
          AttributeTypeEntity attributeTypeEntity = Entity.AttributeType;
          Contract.Assume(attributeTypeEntity != null);
          _attributeType = new AttributeType(attributeTypeEntity);
        }

        return _attributeType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the item type the value describes.
    /// </summary>
    /// 
    /// <value>
    /// The item type the value describes.
    /// </value>
    public ItemType ItemType {
      get {
        Contract.Ensures(Contract.Result<ItemType>() != null);

        if (_itemType == null) {
          ItemTypeEntity itemTypeEntity = Entity.ItemType;
          Contract.Assume(itemTypeEntity != null);
          _itemType = ItemType.Create(itemTypeEntity);
        }

        return _itemType;
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
    public ItemTypeId ItemTypeId {
      get {
        return Entity.ItemTypeId;
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

        double result;
        double? valueFloat = Entity.ValueFloat;
        double? valueInt = Entity.ValueInt;

        if (valueFloat.HasValue) {
          result = valueFloat.Value;

        } else if (valueInt.HasValue) {
          result = valueInt.Value;

        } else {
          result = 0.0D; // Should never happen if database is consistent
        }

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        
        return result;
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

      int result = AttributeType.CompareTo(other.AttributeType);

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

      // Format the value according to the attribute
      return AttributeType.DisplayName + ": " + AttributeType.FormatValue(Value, format);
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
    IconId? IHasIcon.IconId {
      get {
        return AttributeType.IconId;
      }
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get {
        return ((IHasId<long>) this).Id;
      }
    }
    #endregion
    #region IHasId<long> Members
    //******************************************************************************
    long IHasId<long>.Id {
      get {
        return CreateCompoundId(ItemTypeId, AttributeId);
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

  //******************************************************************************
  /// <summary>
  /// A read-only collection of attributes.
  /// </summary>
  public class ReadOnlyAttributeValueCollection : ReadOnlyKeyedCollection<AttributeId, AttributeValue> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyAttributeValueCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAttributeValueCollection(IEnumerable<AttributeValue> contents) : base() {
      if (contents != null) {
        foreach (AttributeValue attribute in contents) {
          Items.AddWithoutCallback(attribute);
        }
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Gets the value of the specified attribute, or the default value specified
    /// by the desired attribute type if a specific value for that attribute is
    /// not present.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// 
    /// <returns>
    /// The value of the specified attribute, converted to
    /// <typeparamref name="T" />.
    /// </returns>
    public T GetValue<T>(AttributeId id) {
      AttributeValue attribute;

      if (TryGetValue(id, out attribute)) {
        return attribute.Value.ConvertTo<T>();
      }

      AttributeType type = Eve.General.DataSource.GetAttributeTypeById(id);
      return type.DefaultValue.ConvertTo<T>();
    }
    //******************************************************************************
    /// <summary>
    /// Gets the value of the specified attribute, or the specified default value
    /// if that attribute is not present.
    /// </summary>
    /// 
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// 
    /// <param name="id">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// 
    /// <param name="defaultValue">
    /// The value to return if the specified attribute is not contained in the
    /// collection.
    /// </param>
    /// 
    /// <returns>
    /// The value of the specified attribute, converted to
    /// <typeparamref name="T" />.
    /// </returns>
    public T GetValue<T>(AttributeId id, T defaultValue) {
      AttributeValue attribute;

      if (TryGetValue(id, out attribute)) {
        return attribute.Value.ConvertTo<T>();
      }

      return defaultValue;
    }
    #endregion
  }
}