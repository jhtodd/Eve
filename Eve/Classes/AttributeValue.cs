﻿//-----------------------------------------------------------------------
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
    : EveEntityAdapter<AttributeValueEntity>,
      IAttribute,
      IComparable,
      IComparable<IAttribute>,
      IEquatable<AttributeValue>,
      IEveCacheable,
      IHasIcon,
      IKeyItem<AttributeId>
  {
    private AttributeType type;

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
    public AttributeId Id
    {
      get { return Entity.AttributeId; }
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
    /// Gets the attribute type to which the value applies.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> to which the value applies.
    /// </value>
    public AttributeType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<AttributeType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.type, this.Entity.AttributeId, () => this.Entity.AttributeType);
      }
    }

    /* Methods */

    /// <inheritdoc />
    public int CompareTo(IAttribute other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Type.CompareTo(other.Type);

      if (result == 0)
      {
        result = this.BaseValue.CompareTo(other.BaseValue);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AttributeValue);
    }

    /// <inheritdoc />
    public bool Equals(AttributeValue other)
    {
      if (other == null)
      {
        return false;
      }

      return this.Id.Equals(other.Id) && this.BaseValue.Equals(other.BaseValue);
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
      if (this.Type.UnitId != null && this.Type.Unit != null)
      {
        return this.Type.Unit.FormatValue(this.BaseValue, format);
      }

      // Otherwise, just format the number
      return this.BaseValue.ToString(format);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.Id, this.BaseValue);
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
      return this.Type.DisplayName + ": " + this.Type.FormatValue(this.BaseValue, format);
    }
  }

  #region IAttribute Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IAttribute" /> interface.
  /// </content>
  public partial class AttributeValue : IAttribute
  {
    double IAttribute.BaseValue
    {
      get { return this.BaseValue; }
    }

    AttributeId IAttribute.Id
    {
      get { return this.Id; }
    }

    AttributeType IAttribute.Type
    {
      get { return this.Type; }
    }
  }
  #endregion

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class AttributeValue : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      AttributeValue other = obj as AttributeValue;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class AttributeValue : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.Type.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.Type.IconId; }
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
      get { return this.Id; }
    }
  }
  #endregion
}