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
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about an attribute of an EVE item.
  /// </summary>
  public class AttributeType : BaseValue<AttributeId, AttributeId, AttributeTypeEntity, AttributeType>,
                               IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private AttributeCategory _category;
    private Icon _icon;
    private Unit _unit;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public AttributeType(AttributeTypeEntity entity) : base(entity) {
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
    /// Gets the category to which the attribute belongs.
    /// </summary>
    /// 
    /// <value>
    /// The category to which the attribute belongs.
    /// </value>
    public virtual AttributeCategory Category {
      get {
        if (_category == null) {
          if (CategoryId != null) {

            // Load the cached version if available
            _category = Eve.General.Cache.GetOrAdd<AttributeCategory>(CategoryId, () => {
              AttributeCategoryEntity categoryEntity = Entity.Category;
              Contract.Assume(categoryEntity != null);

              return new AttributeCategory(categoryEntity);
            });
          }
        }

        return _category;
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
    public AttributeCategoryId? CategoryId {
      get {
        return Entity.CategoryId;
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
    public double DefaultValue {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DefaultValue;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        return result;
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
    public string DisplayName {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        return (string.IsNullOrWhiteSpace(Entity.DisplayName) ? Name : Entity.DisplayName);
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
    public bool HighIsGood {
      get {
        return Entity.HighIsGood;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon {
      get {
        if (_icon == null) {
          if (IconId != null) {

            // Load the cached version if available
            _icon = Eve.General.Cache.GetOrAdd<Icon>(IconId, () => {
              IconEntity iconEntity = Entity.Icon;
              Contract.Assume(iconEntity != null);

              return new Icon(iconEntity);
            });
          }
        }

        return _icon;
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
    public IconId? IconId {
      get {
        return Entity.IconId;
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
    public bool Published {
      get {
        return Entity.Published;
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
    public bool Stackable {
      get {
        return Entity.Stackable;
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
    public virtual Unit Unit {
      get {
        if (_unit == null) {
          if (UnitId != null) {

            // Load the cached version if available
            _unit = Eve.General.Cache.GetOrAdd<Unit>(UnitId, () => {
              UnitEntity unitEntity = Entity.Unit;
              Contract.Assume(unitEntity != null);

              return new Unit(unitEntity);
            });
          }
        }

        return _unit;
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
    public UnitId? UnitId {
      get {
        return Entity.UnitId;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public override int CompareTo(AttributeType other) {
      if (other == null) {
        return 1;
      }

      return DisplayName.CompareTo(other.DisplayName);
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