//-----------------------------------------------------------------------
// <copyright file="SchemaToImageConverter.cs" company="Jeremy H. Todd">
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
  using FreeNet.Data.Schema;

  //******************************************************************************
  /// <summary>
  /// Converts a schema-related object to its associated icon.
  /// </summary>
  public class SchemaToImageConverter : IValueConverter {

    #region IValueConverter Members
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

      if (value is Schema) {
        return Application.Current.Resources["DatabaseImage"];
      }

      if (value is SchemaTable || value is SchemaTableComparison) {
        return Application.Current.Resources["TableImage"];
      }

      if (value is SchemaColumn) {
        if (((SchemaColumn) value).IsPrimaryKey) {
          return Application.Current.Resources["KeyImage"];
        } else {
          return Application.Current.Resources["ColumnImage"];
        }
      }

      if (value is SchemaColumnComparison) {
        if (((SchemaColumnComparison) value).NewIsPrimaryKey) {
          return Application.Current.Resources["KeyImage"];
        } else {
          return Application.Current.Resources["ColumnImage"];
        }
      }

      if (value is SchemaColumnCollection ||
          value is SchemaColumnComparisonCollection ||
          value is SchemaTableCollection ||
          value is SchemaTableComparisonCollection) {
        return Application.Current.Resources["FolderImage"];
      }

      return null;
    }
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
    #endregion
  }
}