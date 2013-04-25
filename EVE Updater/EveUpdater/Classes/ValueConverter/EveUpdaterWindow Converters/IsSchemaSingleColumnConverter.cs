//-----------------------------------------------------------------------
// <copyright file="IsSchemaSingleColumnConverter.cs" company="Jeremy H. Todd">
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
  /// Returns whether the provided value is a single-column schema type.
  /// </summary>
  public class IsSchemaSingleColumnConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the IsSchemaSingleColumnConverter class.
    /// </summary>
    public IsSchemaSingleColumnConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value is SchemaColumn)
      {
        return true;
      }

      if (value is SchemaColumnCollection)
      {
        return true;
      }

      if (value is SchemaTable)
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