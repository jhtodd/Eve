//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAttributeValueCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Collections;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;
  using FreeNet.Utilities;

  /// <summary>
  /// A read-only collection of <see cref="AttributeValue" /> objects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlyAttributeValueCollection
    : ReadOnlyKeyedRepositoryItemCollection<AttributeId, AttributeValue>,
      IAttributeCollection
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAttributeValueCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    internal ReadOnlyAttributeValueCollection(IEveRepository repository) : base(repository)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }

    /* Methods */

    /// <summary>
    /// Creates a new instance of the ReadOnlyAttributeValueCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    /// <returns>
    /// A newly created collection containing the specified items.
    /// </returns>
    public static ReadOnlyAttributeValueCollection Create(IEveRepository repository, IEnumerable<AttributeValue> contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");

      var result = new ReadOnlyAttributeValueCollection(repository);

      if (contents != null)
      {
        foreach (var item in contents)
        {
          result.Items.AddWithoutCallback(item);
        }
      }

      return result;
    }

    /// <summary>
    /// Creates a new instance of the ReadOnlyAttributeValueCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    /// <returns>
    /// A newly created collection containing the specified items.
    /// </returns>
    public static ReadOnlyAttributeValueCollection Create(IEveRepository repository, IEnumerable<AttributeValueEntity> entities)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");

      return Create(repository, entities == null ? null : entities.Select(x => x.ToAdapter(repository)));
    }

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

      AttributeType type = this.Repository.GetAttributeTypeById(id);
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

      AttributeType type = this.Repository.GetAttributeTypeById(id);
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
  public sealed partial class ReadOnlyAttributeValueCollection : IAttributeCollection
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
  public sealed partial class ReadOnlyAttributeValueCollection : IEnumerable<IAttribute>
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
  public sealed partial class ReadOnlyAttributeValueCollection : IReadOnlyList<IAttribute>
  {
    IAttribute IReadOnlyList<IAttribute>.this[int index]
    {
      get { return this[index]; }
    }
  }
  #endregion
}