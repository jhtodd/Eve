//-----------------------------------------------------------------------
// <copyright file="IEveUpdaterWindowService.cs" company="Jeremy H. Todd">
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
  using FreeNet.Windows;

  /// <summary>
  /// The base interface for services related to the
  /// <see cref="EveUpdaterWindow" /> class.
  /// </summary>
  public interface IEveUpdaterWindowService : IViewModelService<EveUpdaterWindowViewModel, IEveUpdaterWindowService>
  {
    /* Methods */

    /// <summary>
    /// Prompts the user to select a file to open.
    /// </summary>
    /// <param name="title">
    /// The title to display to the user.
    /// </param>
    /// <param name="filter">
    /// The filename filter.
    /// </param>
    /// <returns>
    /// The full path of the file selected by the user, or <see langword="null" />
    /// if the user canceled the operation.
    /// </returns>
    string PromptOpenFile(string title, string filter);
  }
}