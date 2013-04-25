//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindowViewModel.GenerateTableCodeSimpleCommand.cs" company="Jeremy H. Todd">
//   Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System.Diagnostics.Contracts;

  using FreeNet.Windows;

  /// <content>
  /// Contains the definition of the <see cref="GenerateTableCodeSimpleCommand" /> nested class.
  /// </content>
  public partial class EveUpdaterWindowViewModel
  {
    /// <summary>
    /// The command to generate database table code.
    /// </summary>
    private class GenerateTableCodeSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel>
    {
      /* Constructors */
      
      /// <summary>
      /// Initializes a new instance of the GenerateTableCodeSimpleCommand class.
      /// </summary>
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public GenerateTableCodeSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel)
      {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }

      /* Methods */

      /// <inheritdoc />
      public override bool CanExecute(object parameter)
      {
        return this.ViewModel.CanGenerateTableCode;
      }

      /// <inheritdoc />
      public async override void Execute(object parameter)
      {
        if (!this.ViewModel.CanGenerateTableCode)
        {
          return;
        }

        await this.ViewModel.GenerateTableCode();
      }
    }
  }
}
