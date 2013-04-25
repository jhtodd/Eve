//-----------------------------------------------------------------------
// <copyright file="IsSchemaTwoColumnConverter.cs" company="Jeremy H. Todd">
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
  /// Returns whether the provided value is a two-column schema type.
  /// </summary>
  public class IsSchemaTwoColumnConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the IsSchemaTwoColumnConverter class.
    /// </summary>
    public IsSchemaTwoColumnConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value is SchemaColumnComparison)
      {
        return true;
      }

      if (value is SchemaColumnComparisonCollection)
      {
        return true;
      }

      return false;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
  }
}