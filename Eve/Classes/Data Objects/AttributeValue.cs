//-----------------------------------------------------------------------
// <copyright file="AttributeValue.cs" company="Jeremy H. Todd">
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

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about the value of an EVE attribute.
  /// </summary>
  public sealed partial class AttributeValue
    : EveEntityAdapter<AttributeValueEntity, AttributeValue>,
      IAttribute,
      IComparable<IAttribute>,
      IHasIcon,
      IKeyItem<AttributeId>,
      IKeyItem<EveTypeId>
  {
    private AttributeType attributeType;
    private EveType itemType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AttributeValue class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public AttributeValue(IEveRepository repository, AttributeValueEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */
    
    /// <summary>
    /// Gets the ID of the attribute the current value applies to.
    /// </summary>
    /// <value>
    /// The ID of the attribute the current value applies to.
    /// </value>
    public AttributeId AttributeId
    {
      get { return this.Entity.AttributeId; }
    }

    /// <summary>
    /// Gets the attribute type to which the value applies.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> to which the value applies.
    /// </value>
    public AttributeType AttributeType
    {
      get
      {
        Contract.Ensures(Contract.Result<AttributeType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.attributeType, this.Entity.AttributeId, () => this.Entity.AttributeType);
      }
    }

    /// <summary>
    /// Gets the value of the attribute before any modifiers are applied.
    /// </summary>
    /// <value>
    /// The value of the attribute before any modifiers are applied.
    /// </value>
    public double BaseValue
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result;
        double? valueFloat = Entity.ValueFloat;
        double? valueInt = Entity.ValueInt;

        if (valueFloat.HasValue)
        {
          result = valueFloat.Value;
        }
        else if (valueInt.HasValue)
        {
          result = valueInt.Value;
        }
        else
        {
          result = 0.0D; // Should never happen if database is consistent
        }

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the ID of the type of item to which the value applies.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="EveType" /> to which the value applies.
    /// </value>
    public EveTypeId ItemTypeId
    {
      get { return (EveTypeId)this.Entity.ItemTypeId; }
    }

    /// <summary>
    /// Gets the type of item to which the value applies.
    /// </summary>
    /// <value>
    /// The <see cref="EveType" /> to which the value applies.
    /// </value>
    public EveType ItemType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.itemType, this.Entity.ItemTypeId, () => this.Entity.ItemType);
      }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(AttributeValue other)
    {
      return this.CompareTo((IAttribute)other);
    }

    /// <inheritdoc />
    public int CompareTo(IAttribute other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.AttributeType.CompareTo(other.AttributeType);

      if (result == 0)
      {
        result = this.BaseValue.CompareTo(other.BaseValue);
      }

      return result;
    }

    /// <summary>
    /// Formats the current value according to the attribute type.
    /// </summary>
    /// <returns>
    /// A string containing a formatted version of the attribute value.
    /// </returns>
    public string FormatValue()
    {
      return this.FormatValue(string.Empty);
    }

    /// <summary>
    /// Formats the current value according to the attribute type.
    /// </summary>
    /// <param name="format">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// <returns>
    /// A string containing a formatted version of the attribute value.
    /// </returns>
    public string FormatValue(string format)
    {
      // If the attribute has a unit, use it for the formatting
      if (this.AttributeType.UnitId != null && this.AttributeType.Unit != null)
      {
        return this.AttributeType.Unit.FormatValue(this.BaseValue, format);
      }

      // Otherwise, just format the number
      return this.BaseValue.ToString(format);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.ToString(string.Empty);
    }

    /// <inheritdoc />
    /// <param name="format">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// <returns>
    /// A string containing a formatted version of the attribute value.
    /// </returns>
    public string ToString(string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      // Format the value according to the attribute
      return this.AttributeType.DisplayName + ": " + this.AttributeType.FormatValue(this.BaseValue, format);
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class AttributeValue : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.AttributeType.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.AttributeType.IconId; }
    }
  }
  #endregion

  #region IKeyItem<AttributeId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class AttributeValue : IKeyItem<AttributeId>
  {
    AttributeId IKeyItem<AttributeId>.Key
    {
      get { return this.AttributeId; }
    }
  }
  #endregion

  #region IKeyItem<EveTypeId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class AttributeValue : IKeyItem<EveTypeId>
  {
    EveTypeId IKeyItem<EveTypeId>.Key
    {
      get { return this.ItemTypeId; }
    }
  }
  #endregion
}