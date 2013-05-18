//-----------------------------------------------------------------------
// <copyright file="IEveTypeInstance.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for classes that describe instances of an EVE type
  /// (e.g. skills, blueprints, modules, etc.).
  /// </summary>
  [ContractClass(typeof(IEveTypeInstanceContracts))]
  public interface IEveTypeInstance
  {
    /* Properties */

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The ID of the item.
    /// </value>
    EveTypeId Id { get; }

    /// <summary>
    /// Gets the type of the item.
    /// </summary>
    /// <value>
    /// The type of the item.
    /// </value>
    EveType Type { get; }
  }
}