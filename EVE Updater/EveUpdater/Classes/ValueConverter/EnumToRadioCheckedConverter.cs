//-----------------------------------------------------------------------
// <copyright file="EnumToRadioCheckedConverter.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater {
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Data.Common;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Windows;
  using System.Windows.Data;

  using FreeNet;
  using FreeNet.Data;

  //******************************************************************************
  /// <summary>
  /// Returns a list of EVE groups from a category ID.
  /// </summary>
  public class EnumToRadioCheckedConverter : IValueConverter {

    #region IValueConverter Members
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      if (value == null || parameter == null) {
        return false;
      }

      Type valueType = value.GetType();
      Type parameterType = parameter.GetType();

      if (!valueType.IsEnum) {
        return false;
      }

      object otherValue;


      if (parameterType == valueType) {
        otherValue = parameter;

      } else {
        try {
          otherValue = Enum.Parse(valueType, parameter.ToString(), true);
        } catch {
          return false;
        }
      }

      return Enum.Equals(value, otherValue);
    }
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      if (value == null || parameter == null) {
        return null;
      }

      bool useValue = (bool) value;

      if (useValue) {
        if (parameter.GetType() == targetType) {
          return parameter;
        }

        Contract.Assume(targetType != null);
        string targetValue = parameter.ToString();
        return Enum.Parse(targetType, targetValue);
      }

      return null;
    }
    #endregion
  }
}