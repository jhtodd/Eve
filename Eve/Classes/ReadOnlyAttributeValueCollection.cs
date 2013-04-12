﻿//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAttributeValueCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// A read-only collection of attributes.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyAttributeValueCollection 
    : ReadOnlyKeyedCollection<AttributeId, AttributeValue>,
      IAttributeCollection
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAttributeValueCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAttributeValueCollection(IEnumerable<AttributeValue> contents) : base()
    {
      if (contents != null)
      {
        foreach (AttributeValue attribute in contents)
        {
          Items.AddWithoutCallback(attribute);
        }
      }
    }

    /* Methods */
    
    /// <summary>
    /// Gets the numeric value of the specified attribute, or the default value
    /// specified by the attribute type if a specific value for that attribute is
    /// not present.
    /// </summary>
    /// <param name="id">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// <returns>
    /// The value of the specified attribute.
    /// </returns>
    public double GetAttributeValue(AttributeId id)
    {
      Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
      Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

      AttributeValue attribute;

      if (this.TryGetValue(id, out attribute))
      {
        Contract.Assume(attribute != null);
        return attribute.BaseValue;
      }

      AttributeType type = Eve.General.DataSource.GetAttributeTypeById(id);
      return type.DefaultValue;
    }

    /// <summary>
    /// Gets the numeric value of the specified attribute, or the specified
    /// default value if that attribute is not present.
    /// </summary>
    /// <param name="id">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// <param name="defaultValue">
    /// The value to return if the specified attribute is not contained in the
    /// collection.
    /// </param>
    /// <returns>
    /// The value of the specified attribute.
    /// </returns>
    public double GetAttributeValue(AttributeId id, double defaultValue)
    {
      AttributeValue attribute;

      if (this.TryGetValue(id, out attribute))
      {
        Contract.Assume(attribute != null);
        return attribute.BaseValue;
      }

      AttributeType type = Eve.General.DataSource.GetAttributeTypeById(id);
      return type.DefaultValue;
    }

    /// <summary>
    /// Gets the value of the specified attribute, or the default value specified
    /// by the attribute type if a specific value for that attribute is
    /// not present.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// <returns>
    /// The value of the specified attribute, converted to
    /// <typeparamref name="T" />.
    /// </returns>
    public T GetAttributeValue<T>(AttributeId id)
    {
      return this.GetAttributeValue(id).ConvertTo<T>();
    }

    /// <summary>
    /// Gets the value of the specified attribute, or the specified default value
    /// if that attribute is not present.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <param name="id">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// <param name="defaultValue">
    /// The value to return if the specified attribute is not contained in the
    /// collection.
    /// </param>
    /// <returns>
    /// The value of the specified attribute, converted to
    /// <typeparamref name="T" />.
    /// </returns>
    public T GetAttributeValue<T>(AttributeId id, T defaultValue)
    {
      AttributeValue attribute;

      if (this.TryGetValue(id, out attribute))
      {
        Contract.Assume(attribute != null);
        return attribute.BaseValue.ConvertTo<T>();
      }

      return defaultValue;
    }
  }

  #region IAttributeCollection Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IAttributeCollection" /> interface.
  /// </content>
  public partial class ReadOnlyAttributeValueCollection : IAttributeCollection
  {
    IAttribute IAttributeCollection.this[AttributeId attributeId]
    {
      get
      {
        var result = this[attributeId];

        Contract.Assume(result != null);
        return result;
      }
    }

    bool IAttributeCollection.TryGetValue(AttributeId attributeId, out IAttribute value)
    {
      AttributeValue containedValue;

      bool success = TryGetValue(attributeId, out containedValue);
      value = containedValue;

      Contract.Assume(!success || value != null);
      return success;
    }
  }
  #endregion

  #region IEnumerable<IAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable{T}" /> interface.
  /// </content>
  public partial class ReadOnlyAttributeValueCollection : IEnumerable<IAttribute>
  {
    IEnumerator<IAttribute> IEnumerable<IAttribute>.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
  #endregion

  #region IReadOnlyList<IAttribute> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyList{T}" /> interface.
  /// </content>
  public partial class ReadOnlyAttributeValueCollection : IReadOnlyList<IAttribute>
  {
    IAttribute IReadOnlyList<IAttribute>.this[int index]
    {
      get { return this[index]; }
    }
  }
  #endregion
}