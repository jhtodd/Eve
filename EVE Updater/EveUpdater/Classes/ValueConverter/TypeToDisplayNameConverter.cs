//-----------------------------------------------------------------------
// <copyright file="TypeToDisplayNameConverter.cs" company="Jeremy H. Todd">
//     Copyright � Jeremy H. Todd 2012
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

  /// <summary>
  /// Returns the display name (name followed by namespace) of a type.
  /// </summary>
  public class TypeToDisplayNameConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the TypeToDisplayNameConverter class.
    /// </summary>
    public TypeToDisplayNameConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value == null)
      {
        return string.Empty;
      }

      Type valueType = value.GetType();

      return valueType.Name + " (" + valueType.Namespace + ")";
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
  }
}