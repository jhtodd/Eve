//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindowViewModel.LoadCategoriesSimpleCommand.cs" company="Jeremy H. Todd">
//   Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System.Diagnostics.Contracts;

  using FreeNet.Windows;

  /// <content>
  /// Contains the definition of the <see cref="LoadCategoriesCodeSimpleCommand" /> nested class.
  /// </content>
  public partial class EveUpdaterWindowViewModel
  {
    /// <summary>
    /// The command to test data source settings.
    /// </summary>
    private class LoadCategoriesSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel>
    {
      /* Constructors */

      /// <summary>
      /// Initializes a new instance of the LoadCategoriesSimpleCommand class.
      /// </summary>
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public LoadCategoriesSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel)
      {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }

      /* Methods */

      /// <inheritdoc />
      public override bool CanExecute(object parameter)
      {
        return !string.IsNullOrWhiteSpace(this.ViewModel.CurrentDataSourceProvider) &&
               !string.IsNullOrWhiteSpace(this.ViewModel.CurrentDataSourceConnectionString);
      }

      /// <inheritdoc />
      public async override void Execute(object parameter)
      {
        await this.ViewModel.LoadCategories();
      }
    }
  }
}
