//-----------------------------------------------------------------------
// <copyright file="IAttribute.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for classes that describe the value of an EVE attribute.
  /// </summary>
  [ContractClass(typeof(IAttributeContracts))]
  public interface IAttribute
  {
    /* Properties */

    /// <summary>
    /// Gets the ID of the attribute.
    /// </summary>
    /// <value>
    /// The ID of the attribute.
    /// </value>
    AttributeId AttributeId { get; }

    /// <summary>
    /// Gets the type of the attribute.
    /// </summary>
    /// <value>
    /// The type of the attribute.
    /// </value>
    AttributeType AttributeType { get; }

    /// <summary>
    /// Gets the base value of the attribute.
    /// </summary>
    /// <value>
    /// The base value of the attribute.
    /// </value>
    double BaseValue { get; }
  }
}