//-----------------------------------------------------------------------
// <copyright file="CategoryAttributeDiscoveryMethod.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Windows;
  using System.Windows.Data;

  using FreeNet;

  /// <summary>
  /// Specifies how to find EVE attributes for a given category or group.
  /// </summary>
  public enum CategoryAttributeDiscoveryMethod
  {
    /// <summary>
    /// Find the attributes possessed by all items in the specified category.
    /// </summary>
    [Description("Possessed By All")]
    [LongDescription("Find the attributes possessed by all items in the specified category.")]
    PossessedByAll,

    /// <summary>
    /// Find the attributes possessed by at least a certain number of items
    /// in the category.
    /// </summary>
    [Description("Possessed By Minimum Number")]
    [LongDescription("Find the attributes possessed by at least a certain number of items in the category.")]
    PossessedByMinimumNumber
  }
}