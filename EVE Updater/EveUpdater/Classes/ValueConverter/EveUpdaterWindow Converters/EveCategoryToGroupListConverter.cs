//-----------------------------------------------------------------------
// <copyright file="EveCategoryToGroupListConverter.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
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

  /// <summary>
  /// Returns a list of EVE groups from a category ID.
  /// </summary>
  public class EveCategoryToGroupListConverter : IMultiValueConverter
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveCategoryToGroupListConverter class.
    /// </summary>
    public EveCategoryToGroupListConverter()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (values == null || values.Length < 2)
      {
        return null;
      }

      CategoryListEntry categoryEntry = values[0] as CategoryListEntry;

      if (categoryEntry == null)
      {
        return null;
      }

      EveUpdaterWindowViewModel viewModel = values[1] as EveUpdaterWindowViewModel;

      if (viewModel == null)
      {
        return null;
      }

      return viewModel.LoadGroupsForCategory(categoryEntry.Value);
    }

    /// <inheritdoc />
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new InvalidOperationException("This converter is for one-way binding only.");
    }
  }
}