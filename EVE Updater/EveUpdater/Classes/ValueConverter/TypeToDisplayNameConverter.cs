//-----------------------------------------------------------------------
// <copyright file="TypeToDisplayNameConverter.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater {
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Windows;
  using System.Windows.Data;

  using FreeNet;
  using FreeNet.Data;

  //******************************************************************************
  /// <summary>
  /// Returns the display name (name followed by namespace) of a type.
  /// </summary>
  public class TypeToDisplayNameConverter : IValueConverter {

    #region IValueConverter Members
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      if (value == null) {
        return string.Empty;
      }

      Type valueType = value.GetType();

      return valueType.Name + " (" + valueType.Namespace + ")";
    }
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
    #endregion
  }
}