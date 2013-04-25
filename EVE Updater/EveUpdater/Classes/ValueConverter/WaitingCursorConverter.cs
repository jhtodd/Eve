//-----------------------------------------------------------------------
// <copyright file="WaitingCursorConverter.cs" company="Jeremy H. Todd">
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
  using System.Windows.Input;

  using FreeNet;
  using FreeNet.Data;

  /// <summary>
  /// Returns a cursor value based on a boolean value indicating whether the
  /// bound item is in a waiting state.
  /// </summary>
  public class WaitingCursorConverter : IValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the WaitingCursorConverter class.
    /// </summary>
    public WaitingCursorConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (!(value is bool))
      {
        return Cursors.Arrow;
      }

      return ((bool)value) ? Cursors.Wait : Cursors.Arrow;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
  }
}