//-----------------------------------------------------------------------
// <copyright file="IEveRepositoryItem.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for objects which are bound to an EVE repository.
  /// </summary>
  /// <remarks>
  /// <para>
  /// A repository item is an object which is associated with an
  /// <see cref="IEveRepository" />.  Normally this means that one
  /// or more properties are lazily loaded, and therefore need
  /// the ability to perform queries against the data source.
  /// </para>
  /// </remarks>
  [ContractClass(typeof(IEveRepositoryItemContracts))]
  public interface IEveRepositoryItem
  {
    /* Properties */

    /// <summary>
    /// Gets the <see cref="IEveRepository" /> the item is associated
    /// with.
    /// </summary>
    /// <value>
    /// The <see cref="IEveRepository" /> the item is associated with.
    /// </value>
    IEveRepository Repository { get; }
  }
}