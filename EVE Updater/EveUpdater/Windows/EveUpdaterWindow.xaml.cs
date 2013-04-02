//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindow.xaml.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater {
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Data;
  using System.Windows.Documents;
  using System.Windows.Input;
  using System.Windows.Media;
  using System.Windows.Media.Imaging;
  using System.Windows.Shapes;

  using FreeNet.Data;

  /// <summary>
  /// Interaction logic for EveUpdaterWindow.xaml
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The disposable fields are dealt with in OnClosed().")]
  public partial class EveUpdaterWindow : Window {

    #region Instance Fields
    private EveUpdaterWindowViewModel _vm;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveUpdaterWindow class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Any virtual methods called are unavoidable: InitializeComponent() and ObjectInvariant() are both required.")]
    public EveUpdaterWindow()
      : base() {
      InitializeComponent();

      // Initialize the Data Source ViewModel
      _vm = new EveUpdaterWindowViewModel(new DefaultEveUpdaterWindowService());
      DataContext = _vm;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_vm != null);
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the view model for the current window.
    /// </summary>
    /// 
    /// <value>
    /// An object derived from <see cref="ViewModelBase" /> representing the
    /// view model of the current window.
    /// </value>
    public EveUpdaterWindowViewModel ViewModel {
      get {
        Contract.Ensures(Contract.Result<EveUpdaterWindowViewModel>() != null);
        return _vm;
      }
    }
    #endregion
    #region Protected Methods
    //******************************************************************************
    /// <inheritdoc />
    protected override void OnClosed(EventArgs e) {
      base.OnClosed(e);

      _vm.Dispose();
    }
    #endregion
    #region Private Methods
    //******************************************************************************
    // This is a kludge to get around the fact that the TreeView control doesn't
    // have a bindable SelectedItem property.
    private void CategoryTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
      ViewModel.SelectedAttributeCategory = e.NewValue;
    }
    //******************************************************************************
    // This is a kludge to get around the fact that the TreeView control doesn't
    // have a bindable SelectedItem property.
    private void DataSourceOverviewTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
      ViewModel.DataSourceComparisonSelectedValue = e.NewValue;
    }
    #endregion
  }
}
