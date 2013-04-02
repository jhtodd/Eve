//-----------------------------------------------------------------------
// <copyright file="SchemaTableComparisonTreeViewConverter.cs" company="Jeremy H. Todd">
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
  /// Converts a <see cref="SchemaTableComparison" /> object to appear in a
  /// treeview.
  /// </summary>
  public class SchemaTableComparisonTreeViewConverter : IValueConverter {

    #region IValueConverter Members
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      SchemaTableComparison comparison = value as SchemaTableComparison;

      if (comparison == null) {
        return null;
      }

      return new object[] { 
        comparison.AddedColumns,
        comparison.RemovedColumns,
        comparison.ChangedColumns,
        comparison.UnchangedColumns
      };
    }
    //******************************************************************************
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "The purpose of the class is to implement the interface and it will not be accessed in another context.")]
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
    #endregion
  }
}