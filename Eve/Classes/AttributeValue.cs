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
  using FreeNet.Utilities;

  using Eve.Data.Entities;
  using Eve.Meta;

  //******************************************************************************
  /// <summary>
  /// Contains information about the value of an EVE attribute.
  /// </summary>
  public class AttributeValue : EntityAdapter<AttributeValueEntity>,
                                IAttribute,
                                IComparable,
                                IComparable<IAttribute>,
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
    public static long CreateCompoundId(TypeId itemTypeId, AttributeId attributeId) {
      return (long) ((((ulong) (long) itemTypeId) << 32) | ((ulong) (long) attributeId));
    }
    #endregion

    #region Instance Fields
    private AttributeType _attributeType;
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
    public AttributeId Id {
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
    public AttributeType Type {
      get {
        Contract.Ensures(Contract.Result<AttributeType>() != null);

        if (_attributeType == null) {

          // Load the cached version if available
          _attributeType = Eve.General.Cache.GetOrAdd<AttributeType>(Id, () => {
            AttributeTypeEntity attributeTypeEntity = Entity.AttributeType;
            Contract.Assume(attributeTypeEntity != null);

            return new AttributeType(attributeTypeEntity);
          });
        }

        return _attributeType;
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
    public double BaseValue {
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
    public virtual int CompareTo(IAttribute other) {
      if (other == null) {
        return 1;
      }

      int result = Type.CompareTo(other.Type);

      if (result == 0) {
        result = BaseValue.CompareTo(other.BaseValue);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as AttributeValue);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(AttributeValue other) {
      if (other == null) {
        return false;
      }

      return Id.Equals(other.Id) && BaseValue.Equals(other.BaseValue);
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
      if (Type.UnitId != null && Type.Unit != null) {
        return Type.Unit.FormatValue(BaseValue, format);
      }

      // Otherwise, just format the number
      return BaseValue.ToString(format);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return CompoundHashCode.Create(Id, BaseValue);
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
    public virtual string ToString(string format) {
      Contract.Ensures(Contract.Result<string>() != null);

      // Format the value according to the attribute
      return Type.DisplayName + ": " + Type.FormatValue(BaseValue, format);
    }
    #endregion

    #region IAttribute Members
    //******************************************************************************
    double IAttribute.BaseValue {
      get {
        return BaseValue;
      }
    }
    //******************************************************************************
    AttributeId IAttribute.Id {
      get {
        return Id;
      }
    }
    //******************************************************************************
    AttributeType IAttribute.Type {
      get {
        return Type;
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
        return Type.Icon;
      }
    }
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get {
        return Type.IconId;
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
        return CreateCompoundId(Entity.ItemTypeId, Id);
      }
    }
    #endregion
    #region IKeyItem<AttributeId> Members
    //******************************************************************************
    AttributeId IKeyItem<AttributeId>.Key {
      get {
        return Id;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of attributes.
  /// </summary>
  public class ReadOnlyAttributeValueCollection : ReadOnlyKeyedCollection<AttributeId, AttributeValue>,
                                                  IAttributeCollection {

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
    /// Gets the numeric value of the specified attribute, or the default value
    /// specified by the attribute type if a specific value for that attribute is
    /// not present.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// 
    /// <returns>
    /// The value of the specified attribute.
    /// </returns>
    public double GetAttributeValue(AttributeId id) {
      Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
      Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

      AttributeValue attribute;

      if (TryGetValue(id, out attribute)) {
        Contract.Assume(attribute != null);
        return attribute.BaseValue;
      }

      AttributeType type = Eve.General.DataSource.GetAttributeTypeById(id);
      return type.DefaultValue;
    }
    //******************************************************************************
    /// <summary>
    /// Gets the numeric value of the specified attribute, or the specified
    /// default value if that attribute is not present.
    /// </summary>
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
    /// The value of the specified attribute.
    /// </returns>
    public double GetAttributeValue(AttributeId id, double defaultValue) {
      AttributeValue attribute;

      if (TryGetValue(id, out attribute)) {
        Contract.Assume(attribute != null);
        return attribute.BaseValue;
      }

      AttributeType type = Eve.General.DataSource.GetAttributeTypeById(id);
      return type.DefaultValue;
    }
    //******************************************************************************
    /// <summary>
    /// Gets the value of the specified attribute, or the default value specified
    /// by the attribute type if a specific value for that attribute is
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
    public T GetAttributeValue<T>(AttributeId id) {
      return GetAttributeValue(id).ConvertTo<T>();
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
    public T GetAttributeValue<T>(AttributeId id, T defaultValue) {
      AttributeValue attribute;

      if (TryGetValue(id, out attribute)) {
        Contract.Assume(attribute != null);
        return attribute.BaseValue.ConvertTo<T>();
      }

      return defaultValue;
    }
    #endregion

    #region IAttributeCollection Members
    //******************************************************************************
    IAttribute IAttributeCollection.this[AttributeId attributeId] {
      get {
        var result = this[attributeId];

        Contract.Assume(result != null);
        return result;
      }
    }
    //******************************************************************************
    bool IAttributeCollection.TryGetValue(AttributeId attributeId, out IAttribute value) {
      AttributeValue containedValue;

      bool success = TryGetValue(attributeId, out containedValue);
      value = containedValue;

      Contract.Assume(!success || value != null);
      return success;
    }
    #endregion
    #region IEnumerator<IAttribute> Members
    //******************************************************************************
    IEnumerator<IAttribute> IEnumerable<IAttribute>.GetEnumerator() {
      return GetEnumerator();
    }
    #endregion
    #region IReadOnlyList<IAttribute> Members
    //******************************************************************************
    IAttribute IReadOnlyList<IAttribute>.this[int index] {
      get {
        return this[index];
      }
    }
    #endregion
  }
}