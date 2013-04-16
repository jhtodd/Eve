//-----------------------------------------------------------------------
// <copyright file="IHasAttributes.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for classes which possess a collection of EVE attributes.
  /// </summary>
  [ContractClass(typeof(IHasAttributesContracts))]
  public interface IHasAttributes
  {
    /* Properties */

    /// <summary>
    /// Gets the collection of attributes that apply to the item.
    /// </summary>
    /// <value>
    /// The collection of attributes that apply to the item.
    /// </value>
    IAttributeCollection Attributes { get; }
  }
}