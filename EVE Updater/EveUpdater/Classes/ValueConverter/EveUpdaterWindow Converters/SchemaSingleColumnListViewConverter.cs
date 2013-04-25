//-----------------------------------------------------------------------
// <copyright file="SchemaSingleColumnListViewConverter.cs" company="Jeremy H. Todd">
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
  /// Converts a single-column schema type (SchemaColumn, SchemaTable,
  /// SchemaColumnCollection) to a type appropriate as the ItemsSource of a
  /// ListView.
  /// </summary>
  public class SchemaSingleColumnListViewConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SchemaSingleColumnListViewConverter class.
    /// </summary>
    public SchemaSingleColumnListViewConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value is SchemaColumn)
      {
        return new object[] { value };
      }

      if (value is SchemaColumnCollection)
      {
        return value;
      }

      if (value is SchemaTable)
      {
        return ((SchemaTable)value).Columns;
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