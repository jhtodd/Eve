//-----------------------------------------------------------------------
// <copyright file="CategoryListEntry.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Windows;
  using System.Windows.Data;

  using FreeNet;
  using FreeNet.Windows;

  /// <summary>
  /// A list entry for EVE categories.
  /// </summary>
  public class CategoryListEntry : ListEntry<int, string>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CategoryListEntry class.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="displayValue">
    /// The display value.
    /// </param>
    public CategoryListEntry(int value, string displayValue) : base(value, displayValue)
    {
    }
  }
}