//-----------------------------------------------------------------------
// <copyright file="AttributeType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// Contains information about an attribute of an EVE item.
  /// </summary>>
  [Table("dgmAttributeTypes")]
  public class AttributeType : BaseValue<AttributeId, AttributeType>,
                               IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private AttributeCategoryId? _categoryId;
    private double _defaultValue;
    private string _displayName;
    private bool _highIsGood;
    private Icon _icon;
    private int? _iconId;
    private bool _published;
    private bool _stackable;
    private UnitId? _unitId;

    private AttributeCategory _category;
    private Unit _unit;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeType class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public AttributeType() : base(0, DEFAULT_NAME, string.Empty) {
      _defaultValue = 0.0D;
      _displayName = DEFAULT_NAME;

      Contract.Assume(!double.IsInfinity(_defaultValue));
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeType class.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item.
    /// </param>
    /// 
    /// <param name="name">
    /// The name of the item.
    /// </param>
    /// 
    /// <param name="description">
    /// The description of the item.
    /// </param>
    /// 
    /// <param name="categoryId">
    /// The ID of the attribute category.
    /// </param>
    /// 
    /// <param name="defaultValue">
    /// The default value for the attribute.
    /// </param>
    /// 
    /// <param name="displayName">
    /// The display name of the item.
    /// </param>
    /// 
    /// <param name="highIsGood">
    /// Specifies whether a high value for the attribute is "good."
    /// </param>
    /// 
    /// <param name="iconId">
    /// The ID of the icon associated with the item, if any.
    /// </param>
    /// 
    /// <param name="published">
    /// Specifies whether the item is marked as published for
    /// public consumption.
    /// </param>
    /// 
    /// <param name="stackable">
    /// Specifies whether the attribute is stackable without penalty.
    /// </param>
    /// 
    /// <param name="unitId">
    /// The ID of the attribute's unit, if any.
    /// </param>
    public AttributeType(AttributeId id,
                         string name,
                         string description,
                         AttributeCategoryId? categoryId,
                         double defaultValue,
                         string displayName,
                         bool highIsGood,
                         int? iconId,
                         bool published,
                         bool stackable,
                         UnitId? unitId) : base(id, name, description) {

      Contract.Requires(!string.IsNullOrWhiteSpace(name), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);
      Contract.Requires(!double.IsInfinity(defaultValue), Resources.Messages.AttributeType_DefaultValueMustBeNumeric);
      Contract.Requires(!double.IsNaN(defaultValue), Resources.Messages.AttributeType_DefaultValueMustBeNumeric);

      if (string.IsNullOrWhiteSpace(displayName)) {
        displayName = name;
      }

      _categoryId = categoryId;
      _defaultValue = defaultValue;
      _displayName = displayName;
      _highIsGood = highIsGood;
      _iconId = iconId;
      _published = published;
      _stackable = stackable;
      _unitId = unitId;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(!double.IsInfinity(_defaultValue));
      Contract.Invariant(!double.IsNaN(_defaultValue));
      Contract.Invariant(!string.IsNullOrWhiteSpace(_displayName));
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the category to which the attribute belongs.
    /// </summary>
    /// 
    /// <value>
    /// The category to which the attribute belongs.
    /// </value>
    [ForeignKey("CategoryId")]
    public virtual AttributeCategory Category {
      get {
        return _category;
      }
      private set {
        _category = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the category to which the attribute belongs.
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="AttributeCategoryId" /> value specifying the category to
    /// which the attribute belongs.
    /// </value>
    [Column("categoryID")]
    public AttributeCategoryId? CategoryId {
      get {
        return _categoryId;
      }
      private set {
        _categoryId = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the default value of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The default value of the attribute.
    /// </value>
    [Column("defaultValue")]
    public double DefaultValue {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        return _defaultValue;
      }
      private set {
        Contract.Requires(!double.IsInfinity(value), Resources.Messages.AttributeType_DefaultValueMustBeNumeric);
        Contract.Requires(!double.IsNaN(value), Resources.Messages.AttributeType_DefaultValueMustBeNumeric);
        _defaultValue = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the display name of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The human-readable display name of the attribute.
    /// </value>
    [Column("displayName")]
    public string DisplayName {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        return _displayName;
      }
      private set {
        if (string.IsNullOrWhiteSpace(value)) {
          value = Name;
        }

        _displayName = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether a high value for the attribute is considered
    /// good.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if a high value is good; otherwise
    /// <see langword="false" />.
    /// </value>
    [Column("highIsGood")]
    public bool HighIsGood {
      get {
        return _highIsGood;
      }
      private set {
        _highIsGood = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the attribute, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    [ForeignKey("IconId")]
    public virtual Icon Icon {
      get {
        return _icon;
      }
      private set {
        _icon = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the icon associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the attribute, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    [Column("iconID")]
    public int? IconId {
      get {
        return _iconId;
      }
      private set {
        _iconId = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the attribute is marked as published for
    /// public consumption.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the attribute is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("published")]
    public bool Published {
      get {
        return _published;
      }
      private set {
        _published = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the attribute can be stacked without
    /// penalty.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the attribute can be stacked with penalty;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("stackable")]
    public bool Stackable {
      get {
        return _stackable;
      }
      private set {
        _stackable = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the unit associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Unit" /> associated with the attribute, or
    /// <see langword="null" /> if no such unit exists.
    /// </value>
    [ForeignKey("UnitId")]
    public virtual Unit Unit {
      get {
        return _unit;
      }
      private set {
        _unit = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the unit associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the unit associated with the attribute, or
    /// <see langword="null" /> if no such unit exists.
    /// </value>
    [Column("unitID")]
    public UnitId? UnitId {
      get {
        return _unitId;
      }
      private set {
        _unitId = value;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Formats a numeric value according to the current attribute.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value to format.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the specified value.
    /// </returns>
    public string FormatValue(double value) {
      return FormatValue(value, string.Empty);
    }
    //******************************************************************************
    /// <summary>
    /// Formats a numeric value according to the current attribute.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value to format.
    /// </param>
    /// 
    /// <param name="numericFormat">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the specified value.
    /// </returns>
    public string FormatValue(double value, string numericFormat) {

      // If the attribute has a unit, use it for the formatting
      if (UnitId != null && Unit != null) {
        return Unit.FormatValue(value, numericFormat);
      }

      // Otherwise, just format the number
      return value.ToString(numericFormat);
    }
    #endregion
  }
}