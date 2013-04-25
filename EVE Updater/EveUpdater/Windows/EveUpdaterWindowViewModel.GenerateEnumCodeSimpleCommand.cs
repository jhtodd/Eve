//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindowViewModel.GenerateEnumCodeSimpleCommand.cs" company="Jeremy H. Todd">
//   Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System.Diagnostics.Contracts;

  using FreeNet.Windows;

  /// <content>
  /// Contains the definition of the <see cref="GenerateEnumCodeSimpleCommand" /> nested class.
  /// </content>
  public partial class EveUpdaterWindowViewModel
  {
    /// <summary>
    /// The command to generate enumeration code.
    /// </summary>
    private class GenerateEnumCodeSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel>
    {
      /* Constructors */

      /// <summary>
      /// Initializes a new instance of the <see cref="GenerateEnumCodeSimpleCommand" /> class.
      /// </summary>
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public GenerateEnumCodeSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel)
      {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }

      /* Methods */

      /// <inheritdoc />
      public override bool CanExecute(object parameter)
      {
        return this.ViewModel.CanGenerateEnumCode;
      }

      /// <inheritdoc />
      public async override void Execute(object parameter)
      {
        if (!this.ViewModel.CanGenerateEnumCode)
        {
          return;
        }

        await this.ViewModel.GenerateEnumCode();
      }
    }
  }
}
