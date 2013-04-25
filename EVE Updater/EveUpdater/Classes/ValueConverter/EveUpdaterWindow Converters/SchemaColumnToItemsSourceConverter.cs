//-----------------------------------------------------------------------
// <copyright file="SchemaColumnToItemsSourceConverter.cs" company="Jeremy H. Todd">
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
  /// Converts a schema-related object to its associated icon.
  /// </summary>
  public class SchemaColumnToItemsSourceConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SchemaColumnToItemsSourceConverter class.
    /// </summary>
    public SchemaColumnToItemsSourceConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      SchemaColumn column = value as SchemaColumn;

      if (column == null)
      {
        return null;
      }

      return new SchemaColumn[] { column };
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
  }
}