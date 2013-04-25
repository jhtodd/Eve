//-----------------------------------------------------------------------
// <copyright file="SchemaToImageConverter.cs" company="Jeremy H. Todd">
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
  public class SchemaToImageConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SchemaToImageConverter class.
    /// </summary>
    public SchemaToImageConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value is Schema)
      {
        return Application.Current.Resources["DatabaseImage"];
      }

      if (value is SchemaTable || value is SchemaTableComparison)
      {
        return Application.Current.Resources["TableImage"];
      }

      if (value is SchemaColumn)
      {
        if (((SchemaColumn)value).IsPrimaryKey)
        {
          return Application.Current.Resources["KeyImage"];
        }
        else
        {
          return Application.Current.Resources["ColumnImage"];
        }
      }

      if (value is SchemaColumnComparison)
      {
        if (((SchemaColumnComparison)value).NewIsPrimaryKey)
        {
          return Application.Current.Resources["KeyImage"];
        }
        else
        {
          return Application.Current.Resources["ColumnImage"];
        }
      }

      if (value is SchemaColumnCollection ||
          value is SchemaColumnComparisonCollection ||
          value is SchemaTableCollection ||
          value is SchemaTableComparisonCollection)
      {
        return Application.Current.Resources["FolderImage"];
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