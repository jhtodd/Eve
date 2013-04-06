//-----------------------------------------------------------------------
// <copyright file="IEveTypeInstance.cs" company="Jeremy H. Todd">
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
  using FreeNet.Data.Entity;

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
    TypeId Id { get; }

    /// <summary>
    /// Gets the type of the item.
    /// </summary>
    /// <value>
    /// The type of the item.
    /// </value>
    EveType Type { get; }
  }
}