//-----------------------------------------------------------------------
// <copyright file="SchemaComparisonTreeViewConverter.cs" company="Jeremy H. Todd">
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
  using FreeNet.Data;
  using FreeNet.Data.Schema;

  /// <summary>
  /// Converts a <see cref="SchemaComparison" /> object to appear in a TreeView.
  /// </summary>
  public class SchemaComparisonTreeViewConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SchemaComparisonTreeViewConverter class.
    /// </summary>
    public SchemaComparisonTreeViewConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      SchemaComparison comparison = value as SchemaComparison;

      if (comparison == null)
      {
        return null;
      }

      return new object[]
      { 
        comparison.AddedTables,
        comparison.RemovedTables,
        comparison.ChangedTables,
        comparison.UnchangedTables 
      };
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
  }
}