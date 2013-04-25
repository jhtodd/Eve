//-----------------------------------------------------------------------
// <copyright file="AttributeType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about an attribute of an EVE item.
  /// </summary>
  public sealed partial class AttributeType 
    : BaseValue<AttributeId, AttributeId, AttributeTypeEntity, AttributeType>,
      IAttribute,
      IComparable,
      IComparable<IAttribute>,
      IHasIcon
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.
    private AttributeCategory category;
    private Icon icon;
    private Unit unit;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AttributeType class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AttributeType(IEveRepository repository, AttributeTypeEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */
    
    /// <summary>
    /// Gets the category to which the attribute belongs.
    /// </summary>
    /// <value>
    /// The category to which the attribute belongs.
    /// </value>
    public AttributeCategory Category
    {
      get
      {
        Contract.Ensures(this.CategoryId == null || Contract.Result<AttributeCategory>() != null);

        if (this.CategoryId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.category, this.Entity.CategoryId, () => this.Entity.Category);
      }
    }

    /// <summary>
    /// Gets the ID of the category to which the attribute belongs.
    /// </summary>
    /// <value>
    /// An <see cref="AttributeCategoryId" /> value specifying the category to
    /// which the attribute belongs.
    /// </value>
    public AttributeCategoryId? CategoryId
    {
      get { return Entity.CategoryId; }
    }

    /// <summary>
    /// Gets the default value of the attribute.
    /// </summary>
    /// <value>
    /// The default value of the attribute.
    /// </value>
    public double DefaultValue
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DefaultValue;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the display name of the attribute.
    /// </summary>
    /// <value>
    /// The human-readable display name of the attribute.
    /// </value>
    public string DisplayName
    {
      get
      {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        return string.IsNullOrWhiteSpace(this.Entity.DisplayName) ? this.Name : this.Entity.DisplayName;
      }
    }

    /// <summary>
    /// Gets a value indicating whether a high value for the attribute is considered
    /// good.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if a high value is good; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool HighIsGood
    {
      get { return Entity.HighIsGood; }
    }

    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon
    {
      get
      {
        Contract.Ensures(this.IconId == null || Contract.Result<Icon>() != null);

        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.icon, this.Entity.IconId, () => this.Entity.Icon);
      }
    }

    /// <summary>
    /// Gets the ID of the icon associated with the attribute, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the attribute, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId
    {
      get { return Entity.IconId; }
    }

    /// <summary>
    /// Gets a value indicating whether the attribute is marked as published for
    /// public consumption.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the attribute is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published
    {
      get { return Entity.Published; }
    }

    /// <summary>
    /// Gets a value indicating whether the attribute can be stacked without
    /// penalty.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the attribute can be stacked with penalty;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Stackable
    {
      get { return Entity.Stackable; }
    }

    /// <summary>
    /// Gets the unit associated with the attribute, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Unit" /> associated with the attribute, or
    /// <see langword="null" /> if no such unit exists.
    /// </value>
    public Unit Unit
    {
      get
      {
        Contract.Ensures(this.UnitId == null || Contract.Result<Unit>() != null);

        if (this.UnitId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.unit, this.Entity.UnitId, () => this.Entity.Unit);
      }
    }

    /// <summary>
    /// Gets the ID of the unit associated with the attribute, if any.
    /// </summary>
    /// <value>
    /// The ID of the unit associated with the attribute, or
    /// <see langword="null" /> if no such unit exists.
    /// </value>
    public UnitId? UnitId
    {
      get { return Entity.UnitId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(AttributeType other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.DisplayName.CompareTo(other.DisplayName);
    }

    /// <inheritdoc />
    public int CompareTo(IAttribute other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.DisplayName.CompareTo(other.Type.DisplayName);
    }

    /// <summary>
    /// Formats a numeric value according to the current attribute.
    /// </summary>
    /// <param name="value">
    /// The value to format.
    /// </param>
    /// <returns>
    /// A string containing a formatted version of the specified value.
    /// </returns>
    public string FormatValue(double value)
    {
      return this.FormatValue(value, string.Empty);
    }

    /// <summary>
    /// Formats a numeric value according to the current attribute.
    /// </summary>
    /// <param name="value">
    /// The value to format.
    /// </param>
    /// <param name="numericFormat">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// <returns>
    /// A string containing a formatted version of the specified value.
    /// </returns>
    public string FormatValue(double value, string numericFormat)
    {
      // If the attribute has a unit, use it for the formatting
      if (this.UnitId != null && this.Unit != null)
      {
        return this.Unit.FormatValue(value, numericFormat);
      }

      // Otherwise, just format the number
      return value.ToString(numericFormat);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.DisplayName;
    }
  }

  #region IAttribute Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IAttribute" /> interface.
  /// </content>
  public partial class AttributeType : IAttribute
  {
    double IAttribute.BaseValue
    {
      get { return this.DefaultValue; }
    }

    AttributeId IAttribute.Id
    {
      get { return this.Id; }
    }

    AttributeType IAttribute.Type
    {
      get { return this; }
    }
  }
  #endregion

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class AttributeType : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      return this.CompareTo(obj as IAttribute);
    }
  }
  #endregion
}