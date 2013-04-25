//-----------------------------------------------------------------------
// <copyright file="SchemaTwoColumnListViewConverter.cs" company="Jeremy H. Todd">
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
  /// Converts a two-column schema type (SchemaColumnComparison, 
  /// SchemaColumnComparisonCollection) to a type appropriate as the ItemsSource 
  /// of a ListView.
  /// </summary>
  public class SchemaTwoColumnListViewConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SchemaTwoColumnListViewConverter class.
    /// </summary>
    public SchemaTwoColumnListViewConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value is SchemaColumnComparison)
      {
        return new object[] { value };
      }

      if (value is SchemaColumnComparisonCollection)
      {
        return value;
      }

      return null;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
  }
}