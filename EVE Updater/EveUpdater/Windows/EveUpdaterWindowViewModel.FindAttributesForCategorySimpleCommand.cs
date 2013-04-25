//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindowViewModel.FindAttributesForCategorySimpleCommand.cs" company="Jeremy H. Todd">
//   Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System.Diagnostics.Contracts;

  using FreeNet.Windows;

  /// <content>
  /// Contains the definition of the FindAttributesForCategorySimpleCommand nested class.
  /// </content>
  public partial class EveUpdaterWindowViewModel
  {
    /// <summary>
    /// The command to test data source settings.
    /// </summary>
    private class FindAttributesForCategorySimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel>
    {
      /* Constructors */

      /// <summary>
      /// Initializes a new instance of the FindAttributesForCategorySimpleCommand class.
      /// </summary>
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public FindAttributesForCategorySimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel)
      {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }

      /* Methods */

      /// <inheritdoc />
      public override bool CanExecute(object parameter)
      {
        return this.ViewModel.SelectedAttributeCategory != null;
      }

      /// <inheritdoc />
      public async override void Execute(object parameter)
      {
        await this.ViewModel.FindAttributesForSelectedCategory();
      }
    }
  }
}
