//-----------------------------------------------------------------------
// <copyright file="IAttributeCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for collections of EVE attributes.
  /// </summary>
  [ContractClass(typeof(IAttributeCollectionContracts))]
  public interface IAttributeCollection : IReadOnlyList<IAttribute>
  {
    /* Indexers */

    /// <summary>
    /// Gets the attribute with the specified ID.
    /// </summary>
    /// <param name="attributeId">
    /// The ID of the attribute to retrieve.
    /// </param>
    /// <returns>
    /// The attribute with the specified ID.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no attribute with the specified ID was found in the collection.
    /// </exception>
    IAttribute this[AttributeId attributeId] { get; }

    /* Methods */

    /// <summary>
    /// Gets a value indicating whether an attribute with the specified ID is
    /// contained in the collection.
    /// </summary>
    /// <param name="attributeId">
    /// The ID of the attribute to locate.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if an attribute with the specified ID is contained
    /// in the collection; otherwise <see langword="false" />.
    /// </returns>
    bool ContainsKey(AttributeId attributeId);

    /// <summary>
    /// Gets the numeric value of the specified attribute, or the default value
    /// specified by the attribute type if a specific value for that attribute is
    /// not present.
    /// </summary>
    /// <param name="attributeId">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// <returns>
    /// The value of the specified attribute.
    /// </returns>
    double GetAttributeValue(AttributeId attributeId);

    /// <summary>
    /// Gets the numeric value of the specified attribute, or the specified
    /// default value if that attribute is not present.
    /// </summary>
    /// <param name="attributeId">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// <param name="defaultValue">
    /// The value to return if the specified attribute is not contained in the
    /// collection.
    /// </param>
    /// <returns>
    /// The value of the specified attribute.
    /// </returns>
    double GetAttributeValue(AttributeId attributeId, double defaultValue);

    /// <summary>
    /// Gets the value of the specified attribute, or the default value specified
    /// by the attribute type if a specific value for that attribute is
    /// not present.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <param name="attributeId">
    /// The ID of the attribute whose value to retrieve.
    /// </param>
    /// <returns>
    /// The value of the specified attribute, converted to
    /// <typeparamref name="T" />.
    /// </returns>
    T GetAttributeValue<T>(AttributeId attributeId);

    /// <summary>
    /// Gets the value of the specified attribute, or the specified default value
    /// if that attribute is not present.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value.
    /// </typeparam>
    /// <param name="attributeId">
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
    T GetAttributeValue<T>(AttributeId attributeId, T defaultValue);

    /// <summary>
    /// Attempts to retrieve the attribute with the specified ID, returning success
    /// or failure.
    /// </summary>
    /// <param name="attributeId">
    /// The ID of the attribute to retrieve.
    /// </param>
    /// <param name="value">
    /// Output parameter.  Will contain the specified attribute if found.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the specified attribute was found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetValue(AttributeId attributeId, out IAttribute value);
  }
}