//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindowViewModel.TestDataSourceSimpleCommand.cs" company="Jeremy H. Todd">
//   Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;

  using FreeNet.Windows;

  /// <content>
  /// Contains the definition of the <see cref="TestDataSourceSimpleCommand" /> nested class.
  /// </content>
  public partial class EveUpdaterWindowViewModel
  {
    /// <summary>
    /// The command to test data source TestDataSourceSimpleCommand settings.
    /// </summary>
    private class TestDataSourceSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel>
    {
      /* Constructors */
      
      /// <summary>
      /// Initializes a new instance of the TestDataSourceSimpleCommand class.
      /// </summary>
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public TestDataSourceSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel)
      {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }

      /* Methods */

      /// <inheritdoc />
      public override bool CanExecute(object parameter)
      {
        IList<object> connectionSettings = parameter as IList<object>;

        if (connectionSettings == null)
        {
          return false;
        }

        Contract.Assume(connectionSettings.Count > 1);

        string provider = (string)connectionSettings[0];
        string connectionString = (string)connectionSettings[1];

        return !string.IsNullOrWhiteSpace(provider) && !string.IsNullOrWhiteSpace(connectionString);
      }

      /// <inheritdoc />
      public override void Execute(object parameter)
      {
        Contract.Assume(parameter != null);
        IList<object> connectionSettings = (IList<object>)parameter;

        Contract.Assume(connectionSettings.Count > 1);
        string provider = (string)connectionSettings[0];
        string connectionString = (string)connectionSettings[1];

        if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(connectionString))
        {
          return;
        }

        this.ViewModel.TestDataSource(provider, connectionString);
      }
    }
  }
}
