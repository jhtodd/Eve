//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindowViewModel.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Collections.Specialized;
  using System.ComponentModel;
  using System.Data;
  using System.Data.Common;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Reflection;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Data;
  using System.Windows.Input;
  using System.Windows.Threading;

  using Eve;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Data;
  using FreeNet.Data.Schema;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;
  using FreeNet.Windows;

  //******************************************************************************
  /// <summary>
  /// The view model for the <see cref="EveUpdaterWindow" /> class.
  /// </summary>
  public class EveUpdaterWindowViewModel : SimpleAsyncViewModelBase<EveUpdaterWindowViewModel, IEveUpdaterWindowService> {

    #region Command Definitions
    //******************************************************************************
    /// <summary>
    /// The command to compare data sources.
    /// </summary>
    public class CompareDataSourcesSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the CompareDataSourcesSimpleCommand class.
      /// </summary>
      /// 
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public CompareDataSourcesSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel) {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Returns a boolean value specifying whether the command can be executed.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      /// 
      /// <returns>
      /// <see langword="true" /> if the command can be executed; otherwise
      /// <see langword="false" />.
      /// </returns>
      public override bool CanExecute(object parameter) {
        return !(string.IsNullOrWhiteSpace(ViewModel.CurrentDataSourceProvider) ||
                 string.IsNullOrWhiteSpace(ViewModel.CurrentDataSourceConnectionString) ||
                 string.IsNullOrWhiteSpace(ViewModel.ComparisonDataSourceProvider) ||
                 string.IsNullOrWhiteSpace(ViewModel.ComparisonDataSourceConnectionString));
      }
      //******************************************************************************
      /// <summary>
      /// Executes the command.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      public async override void Execute(object parameter) {
        await ViewModel.CompareDataSources();
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// The command to test data source settings.
    /// </summary>
    public class FindAttributesForCategorySimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the FindAttributesForCategorySimpleCommand class.
      /// </summary>
      /// 
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public FindAttributesForCategorySimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel) {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Returns a boolean value specifying whether the command can be executed.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      /// 
      /// <returns>
      /// <see langword="true" /> if the command can be executed; otherwise
      /// <see langword="false" />.
      /// </returns>
      public override bool CanExecute(object parameter) {
        return ViewModel.SelectedAttributeCategory != null;
      }
      //******************************************************************************
      /// <summary>
      /// Executes the command.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      public async override void Execute(object parameter) {
        await ViewModel.FindAttributesForSelectedCategory();
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// The command to test data source settings.
    /// </summary>
    public class GenerateEnumCodeSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the GenerateEnumCodeSimpleCommand class.
      /// </summary>
      /// 
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public GenerateEnumCodeSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel) {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Returns a boolean value specifying whether the command can be executed.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      /// 
      /// <returns>
      /// <see langword="true" /> if the command can be executed; otherwise
      /// <see langword="false" />.
      /// </returns>
      public override bool CanExecute(object parameter) {
        return ViewModel.CanGenerateEnumCode;
      }
      //******************************************************************************
      /// <summary>
      /// Executes the command.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      public async override void Execute(object parameter) {
        if (!ViewModel.CanGenerateEnumCode) {
          return;
        }

        await ViewModel.GenerateEnumCode();
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// The command to test data source settings.
    /// </summary>
    public class LoadCategoriesSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the LoadCategoriesSimpleCommand class.
      /// </summary>
      /// 
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public LoadCategoriesSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel) {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Returns a boolean value specifying whether the command can be executed.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      /// 
      /// <returns>
      /// <see langword="true" /> if the command can be executed; otherwise
      /// <see langword="false" />.
      /// </returns>
      public override bool CanExecute(object parameter) {
        return !string.IsNullOrWhiteSpace(ViewModel.CurrentDataSourceProvider) &&
               !string.IsNullOrWhiteSpace(ViewModel.CurrentDataSourceConnectionString);
      }
      //******************************************************************************
      /// <summary>
      /// Executes the command.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      public async override void Execute(object parameter) {
        await ViewModel.LoadCategories();
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// The command to test data source TestDataSourceSimpleCommand settings.
    /// </summary>
    public class TestDataSourceSimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the TestDataSourceSimpleCommand class.
      /// </summary>
      /// 
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public TestDataSourceSimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel) {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Returns a boolean value specifying whether the command can be executed.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      /// 
      /// <returns>
      /// <see langword="true" /> if the command can be executed; otherwise
      /// <see langword="false" />.
      /// </returns>
      public override bool CanExecute(object parameter) {
        IList<object> connectionSettings = parameter as IList<object>;

        if (connectionSettings == null) {
          return false;
        }

        Contract.Assume(connectionSettings.Count > 1);

        string provider = (string) connectionSettings[0];
        string connectionString = (string) connectionSettings[1];

        return !string.IsNullOrWhiteSpace(provider) && !string.IsNullOrWhiteSpace(connectionString);
      }
      //******************************************************************************
      /// <summary>
      /// Executes the command.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      public override void Execute(object parameter) {
        IList<object> connectionSettings = (IList<object>) parameter;

        string provider = (string) connectionSettings[0];
        string connectionString = (string) connectionSettings[1];

        if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(connectionString)) {
          return;
        }

        ViewModel.TestDataSource(provider, connectionString);
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// The command to test the selected entity.
    /// </summary>
    public class TestEntitySimpleCommand : ViewModelSimpleCommand<EveUpdaterWindowViewModel> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the TestEntitySimpleCommand class.
      /// </summary>
      /// 
      /// <param name="viewModel">
      /// The view model associated with the command.
      /// </param>
      public TestEntitySimpleCommand(EveUpdaterWindowViewModel viewModel) : base(viewModel) {
        Contract.Requires(viewModel != null, "The view model cannot be null.");
      }
      #endregion
      #region Public Methods
      //******************************************************************************
      /// <summary>
      /// Returns a boolean value specifying whether the command can be executed.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      /// 
      /// <returns>
      /// <see langword="true" /> if the command can be executed; otherwise
      /// <see langword="false" />.
      /// </returns>
      public override bool CanExecute(object parameter) {
        return ViewModel.SelectedQuery != null;
      }
      //******************************************************************************
      /// <summary>
      /// Executes the command.
      /// </summary>
      /// 
      /// <param name="parameter">
      /// A parameter value passed to the command.
      /// </param>
      public async override void Execute(object parameter) {
        await ViewModel.TestSelectedQuery();
      }
      #endregion
    }
    #endregion

    #region Instance Fields
    private DataTable _attributesForSelectedCategory;
    private ICommand _compareDataSourcesCommand;
    private string _comparisonDataSourceConnectionString;
    private string _comparisonDataSourceProvider;
    private string _currentDataSourceConnectionString;
    private string _currentDataSourceProvider;
    private IEnumerable<ListEntry<string, string>> _dataProviders;
    private SchemaComparison _dataSourceComparison;
    private object _dataSourceComparisonSelectedValue;
    private IEnumerable<ListEntry<MethodInfo, string>> _enumCodeGenerators;
    private IEnumerable<CategoryListEntry> _eveCategories;
    private IEnumerable<ListEntry<MethodInfo, string>> _eveQueryMethods;
    private CategoryAttributeDiscoveryMethod _findAttributesDiscoveryMethod;
    private ICommand _findAttributesForSelectedCategoryCommand;
    private int _findAttributesMinimumNumber;
    private bool _findAttributesPublishedItemsOnly;
    private string _generatedEnumCode;
    private ICommand _generateEnumCodeCommand;
    private ICommand _loadCategoriesCommand;
    private MethodInfo _selectedEnumCodeGenerator;
    private object _selectedAttributeCategory;
    private string _selectedQueryTestResults;
    private MethodInfo _selectedQuery;
    private string _statusText;
    private ICommand _testDataSourceCommand;
    private ICommand _testSelectedEntityCommand;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveUpdaterWindowViewModel class.
    /// </summary>
    /// 
    /// <param name="service">
    /// A <see cref="EveUpdaterWindowService" /> which will provide external
    /// services for the ViewModel.
    /// </param>
    public EveUpdaterWindowViewModel(IEveUpdaterWindowService service) : base(service) {
      Contract.Requires(service != null, "The service cannot be null");

      _attributesForSelectedCategory = null;
      _compareDataSourcesCommand = new CompareDataSourcesSimpleCommand(this);
      _comparisonDataSourceConnectionString = Properties.Settings.Default.ComparisonDataSourceConnectionString;
      _comparisonDataSourceProvider = Properties.Settings.Default.ComparisonDataSourceProvider;
      _currentDataSourceConnectionString = Properties.Settings.Default.CurrentDataSourceConnectionString;
      _currentDataSourceProvider = Properties.Settings.Default.CurrentDataSourceProvider;
      _dataSourceComparison = null;
      _dataSourceComparisonSelectedValue = null;
      _findAttributesDiscoveryMethod = CategoryAttributeDiscoveryMethod.PossessedByAll;
      _findAttributesForSelectedCategoryCommand = new FindAttributesForCategorySimpleCommand(this);
      _findAttributesMinimumNumber = 1;
      _findAttributesPublishedItemsOnly = true;
      _generatedEnumCode = string.Empty;
      _generateEnumCodeCommand = new GenerateEnumCodeSimpleCommand(this);
      _loadCategoriesCommand = new LoadCategoriesSimpleCommand(this);
      _selectedQueryTestResults = string.Empty;
      _statusText = string.Empty;
      _testDataSourceCommand = new TestDataSourceSimpleCommand(this);
      _testSelectedEntityCommand = new TestEntitySimpleCommand(this);

      _dataProviders = GetDataProviders();
      _enumCodeGenerators = GetEnumCodeGenerators();
      _eveQueryMethods = GetEveQueryMethods();
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_compareDataSourcesCommand != null);
      Contract.Invariant(_comparisonDataSourceConnectionString != null);
      Contract.Invariant(_comparisonDataSourceProvider != null);
      Contract.Invariant(_currentDataSourceConnectionString != null);
      Contract.Invariant(_currentDataSourceProvider != null);
      Contract.Invariant(_dataProviders != null);
      Contract.Invariant(_enumCodeGenerators != null);
      Contract.Invariant(_eveQueryMethods != null);
      Contract.Invariant(_findAttributesForSelectedCategoryCommand != null);
      Contract.Invariant(_findAttributesMinimumNumber >= 1);
      Contract.Invariant(_generatedEnumCode != null);
      Contract.Invariant(_generateEnumCodeCommand != null);
      Contract.Invariant(_loadCategoriesCommand != null);
      Contract.Invariant(_selectedQueryTestResults != null);
      Contract.Invariant(_statusText != null);
      Contract.Invariant(_testDataSourceCommand != null);
      Contract.Invariant(_testSelectedEntityCommand != null);
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets or sets the table containing the attribute information for the
    /// selected category.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="DataTable" /> containing information about the attributes
    /// that apply to the selected category.
    /// </value> 
    public DataTable AttributesForSelectedCategory {
      get {
        return _attributesForSelectedCategory;
      }
      set {
        if (value == _attributesForSelectedCategory) {
          return;
        }

        var oldValue = _attributesForSelectedCategory;
        _attributesForSelectedCategory = value;
        OnAttributesForSelectedCategoryChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the enumeration code can be generated.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the window is in a state where enumeration
    /// code can be generated; otherwise <see langword="false" />.
    /// </value>
    public bool CanGenerateEnumCode {
      get {
        return SelectedEnumCodeGenerator != null;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the command to compare the data sources.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of comparing the
    /// two data sources.
    /// </value>
    public ICommand CompareDataSourcesCommand {
      get {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return _compareDataSourcesCommand;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the connection string used by the comparison data source.
    /// </summary>
    /// 
    /// <value>
    /// The connection string used by the comparison data source.
    /// </value>
    public string ComparisonDataSourceConnectionString {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _comparisonDataSourceConnectionString;
      }
      set {
        if (value == _comparisonDataSourceConnectionString) {
          return;
        }

        if (value == null) {
          value = string.Empty;
        }

        string oldValue = _comparisonDataSourceConnectionString;
        _comparisonDataSourceConnectionString = value;
        OnComparisonDataSourceConnectionStringChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the provider name used by the comparison data source.
    /// </summary>
    /// 
    /// <value>
    /// The provider name string used by the comparison data source.
    /// </value>
    public string ComparisonDataSourceProvider {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _comparisonDataSourceProvider;
      }
      set {
        if (value == _comparisonDataSourceProvider) {
          return;
        }

        if (value == null) {
          value = string.Empty;
        }

        string oldValue = _comparisonDataSourceProvider;
        _comparisonDataSourceProvider = value;
        OnComparisonDataSourceProviderChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the connection string used by the current data source.
    /// </summary>
    /// 
    /// <value>
    /// The connection string used by the current data source.
    /// </value>
    public string CurrentDataSourceConnectionString {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _currentDataSourceConnectionString;
      }
      set {
        if (value == _currentDataSourceConnectionString) {
          return;
        }

        if (value == null) {
          value = string.Empty;
        }

        string oldValue = _currentDataSourceConnectionString;
        _currentDataSourceConnectionString = value;
        OnCurrentDataSourceConnectionStringChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the provider name used by the current data source.
    /// </summary>
    /// 
    /// <value>
    /// The provider name used by the current data source.
    /// </value>
    public string CurrentDataSourceProvider {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _currentDataSourceProvider;
      }
      set {
        if (value == _currentDataSourceProvider) {
          return;
        }

        if (value == null) {
          value = string.Empty;
        }

        string oldValue = _currentDataSourceProvider;
        _currentDataSourceProvider = value;
        OnCurrentDataSourceProviderChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of available data providers.
    /// </summary>
    /// 
    /// <value>
    /// The collection of available data providers.
    /// </value>
    public IEnumerable<ListEntry<string, string>> DataProviders {
      get {
        Contract.Ensures(Contract.Result<IEnumerable<ListEntry<string, string>>>() != null);
        return _dataProviders;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the information for the data source comparison overview.
    /// </summary>
    /// 
    /// <value>
    /// The data source comparison overview.
    /// </value>
    public SchemaComparison DataSourceComparison {
      get {
        return _dataSourceComparison;
      }
      set {
        if (value == _dataSourceComparison) {
          return;
        }

        var oldValue = _dataSourceComparison;
        _dataSourceComparison = value;
        OnDataSourceComparisonChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the selected value in the data source comparison overview.
    /// </summary>
    /// 
    /// <value>
    /// The selected item in the data source comparison overview.
    /// </value>
    public object DataSourceComparisonSelectedValue {
      get {
        return _dataSourceComparisonSelectedValue;
      }
      set {
        if (value == _dataSourceComparisonSelectedValue) {
          return;
        }

        var oldValue = _dataSourceComparisonSelectedValue;
        _dataSourceComparisonSelectedValue = value;
        OnDataSourceComparisonSelectedValueChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of methods that generate enumeration code.
    /// </summary>
    /// 
    /// <value>
    /// The collection of methods that generate enumeration code.
    /// </value>
    public IEnumerable<ListEntry<MethodInfo, string>> EnumCodeGenerators {
      get {
        Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);
        return _enumCodeGenerators;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of EVE categories.
    /// </summary>
    /// 
    /// <value>
    /// The collection of EVE categories.
    /// </value>
    public IEnumerable<CategoryListEntry> EveCategories {
      get {
        return _eveCategories;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of EVE game-related entity adapters.
    /// </summary>
    /// 
    /// <value>
    /// The collection of EVE game-related entity adapters.
    /// </value>
    public IEnumerable<ListEntry<MethodInfo, string>> EveQueryMethods {
      get {
        Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);
        return _eveQueryMethods;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating how to find attributes for the selected category.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="CategoryAttributeDiscoveryMethod" /> specifying how to find
    /// attributes for the selected category.
    /// </value>
    public CategoryAttributeDiscoveryMethod FindAttributesDiscoveryMethod {
      get {
        return _findAttributesDiscoveryMethod;
      }
      set {
        if (value == _findAttributesDiscoveryMethod) {
          return;
        }

        var oldValue = _findAttributesDiscoveryMethod;
        _findAttributesDiscoveryMethod = value;
        OnFindAttributesDiscoveryMethodChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the minimum number of items in the selected category that must 
    /// possess an attribute before it is included in the results, if
    /// <see cref="FindAttributesDiscoveryMethod" /> is
    /// <c>PossessedByMinimumNumber</c>.
    /// </summary>
    /// 
    /// <value>
    /// The minimum number of items that must possess an attribute before it is
    /// included.
    /// </value>
    public int FindAttributesMinimumNumber {
      get {
        Contract.Ensures(Contract.Result<int>() >= 1);
        return _findAttributesMinimumNumber;
      }
      set {
        Contract.Requires(value >= 1, "The minimum number cannot be less than 1.");

        if (value == _findAttributesMinimumNumber) {
          return;
        }

        var oldValue = _findAttributesMinimumNumber;
        _findAttributesMinimumNumber = value;
        OnFindAttributesMinimumNumberChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether only published items should be considered
    /// when search for attributes possessed by the selected category.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if only published items should be considered;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool FindAttributesPublishedItemsOnly {
      get {
        return _findAttributesPublishedItemsOnly;
      }
      set {
        if (value == _findAttributesPublishedItemsOnly) {
          return;
        }

        var oldValue = _findAttributesPublishedItemsOnly;
        _findAttributesPublishedItemsOnly = value;
        OnFindAttributesPublishedItemsOnlyChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the command to find the EVE attributes that apply to the selected
    /// category.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of finding the
    /// applicable EVE attributes.
    /// </value>
    public ICommand FindAttributesForSelectedCategoryCommand {
      get {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return _findAttributesForSelectedCategoryCommand;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the generated enumeration code.
    /// </summary>
    /// 
    /// <value>
    /// The generated enumeration code.
    /// </value>
    public string GeneratedEnumCode {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _generatedEnumCode;
      }
      set {
        if (value == _generatedEnumCode) {
          return;
        }

        if (value == null) {
          value = string.Empty;
        }

        string oldValue = _generatedEnumCode;
        _generatedEnumCode = value;
        OnGeneratedEnumCodeChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the command to test a data source.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of testing a data
    /// source.
    /// </value>
    public ICommand GenerateEnumCodeCommand {
      get {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return _generateEnumCodeCommand;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the command to load the category list.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of loading the list
    /// of EVE categories.
    /// </value>
    public ICommand LoadCategoriesCommand {
      get {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return _loadCategoriesCommand;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the selected value in the list of attribute categories.
    /// </summary>
    /// 
    /// <value>
    /// The selected value in the list of attribute categories.
    /// </value>
    public object SelectedAttributeCategory {
      get {
        return _selectedAttributeCategory;
      }
      set {
        if (value == _selectedAttributeCategory) {
          return;
        }

        var oldValue = _selectedAttributeCategory;
        _selectedAttributeCategory = value;
        OnSelectedAttributeCategoryChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the selected enumeration code generator method.
    /// </summary>
    /// 
    /// <value>
    /// The selected enumeration code generator method.
    /// </value>
    public MethodInfo SelectedEnumCodeGenerator {
      get {
        return _selectedEnumCodeGenerator;
      }
      set {
        if (value == _selectedEnumCodeGenerator) {
          return;
        }

        var oldValue = _selectedEnumCodeGenerator;
        _selectedEnumCodeGenerator = value;
        OnSelectedEnumCodeGeneratorChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the selected enumeration code generator method.
    /// </summary>
    /// 
    /// <value>
    /// The selected enumeration code generator method.
    /// </value>
    public MethodInfo SelectedQuery {
      get {
        return _selectedQuery;
      }
      set {
        if (value == _selectedQuery) {
          return;
        }

        var oldValue = _selectedQuery;
        _selectedQuery = value;
        OnSelectedQueryChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the test results for the selected entity.
    /// </summary>
    /// 
    /// <value>
    /// The test results for the selected entity.
    /// </value>
    public string SelectedQueryTestResults {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _selectedQueryTestResults;
      }
      set {
        Contract.Requires(value != null, "The test results cannot be null.");

        if (value == _selectedQueryTestResults) {
          return;
        }

        var oldValue = _selectedQueryTestResults;
        _selectedQueryTestResults = value;
        OnSelectedQueryTestResultsChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the text displayed in the status bar.
    /// </summary>
    /// 
    /// <value>
    /// The text displayed in the status bar.
    /// </value>
    public string StatusText {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _statusText;
      }
      set {
        if (value == _statusText) {
          return;
        }

        if (value == null) {
          value = string.Empty;
        }

        string oldValue = _statusText;
        _statusText = value;
        OnStatusTextChanged(oldValue, value);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the command to test a data source.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of testing a data
    /// source.
    /// </value>
    public ICommand TestDataSourceCommand {
      get {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return _testDataSourceCommand;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the command to test the selected entity.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of testing the
    /// selected entity.
    /// </value>
    public ICommand TestSelectedEntityCommand {
      get {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return _testSelectedEntityCommand;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Compares the current and comparison data sources and displays the results.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task CompareDataSources() {

      // Define main task to be performed asynchronously
      Action operation = () => {

        StatusText = "Analyzing database schemas...";

        IDbConnection currentConnection;
        IDbConnection comparisonConnection;

        try {
          currentConnection = GetCurrentDataSourceConnection();

          if (currentConnection.State != ConnectionState.Open) {
            currentConnection.Open();
          }

        } catch (Exception ex) {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Compare Data Sources", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        try {
          comparisonConnection = GetComparisonDataSourceConnection();

          if (comparisonConnection.State != ConnectionState.Open) {
            comparisonConnection.Open();
          }

        } catch (Exception ex) {
          MessageBox.Show("A connection to the comparison data source could not be established:\n\n" + ex.Message, "Compare Data Sources", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        if (!(currentConnection is DbConnection)) {
          MessageBox.Show("The connection to the current data source does not support the functionality needed to display the data structure.");
          return;
        }

        if (!(currentConnection is DbConnection)) {
          MessageBox.Show("The connection to the comparison data source does not support the functionality needed to display the data structure.");
          return;
        }

        CancellationTokenSource.Token.ThrowIfCancellationRequested();
        Schema currentSchema = new Schema((DbConnection) currentConnection, true);

        CancellationTokenSource.Token.ThrowIfCancellationRequested();
        Schema comparisonSchema = new Schema((DbConnection) comparisonConnection, true);

        CancellationTokenSource.Token.ThrowIfCancellationRequested();
        DataSourceComparison = currentSchema.GetComparison(comparisonSchema);
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => {
        StatusText = string.Empty;
      };

      // Perform the action
      await BeginAsyncOperation(operation, onCleanup);
    }
    //******************************************************************************
    /// <summary>
    /// Compares the current and comparison data sources and displays the results.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task FindAttributesForSelectedCategory() {

      // Define main task to be performed asynchronously
      Action operation = () => {

        StatusText = "Finding attributes...";

        IDbConnection connection;

        try {
          connection = GetCurrentDataSourceConnection();

          if (connection.State != ConnectionState.Open) {
            connection.Open();
          }

        } catch (Exception ex) {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Find Attributes", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        string query = string.Empty;

        CategoryListEntry categoryEntry = SelectedAttributeCategory as CategoryListEntry;
        if (categoryEntry != null) {

          switch (FindAttributesDiscoveryMethod) {
            case CategoryAttributeDiscoveryMethod.PossessedByAll:
              query = "SELECT\n" +
                      "  attributeID,\n" +
                      "  attributeName,\n" +
                      "  description,\n" +
                      "  displayName,\n" +
                      "  published\n" +
                      "FROM\n" +
                      "  dgmAttributeTypes\n" +
                      "WHERE\n" +
                      "  EXISTS (\n" +
                      "    SELECT *\n" +
                      "    FROM\n" +
                      "      invTypes\n" +
                      "      INNER JOIN invGroups ON invGroups.groupID = invTypes.groupID\n" +
                      "    WHERE\n" +
                      "      invGroups.categoryID = " + categoryEntry.Value.ToString() + "\n" +
                      (FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
                      "  )\n" +
                      "  AND NOT EXISTS (\n" +
                      "    SELECT *\n" +
                      "    FROM\n" +
                      "      invTypes\n" +
                      "      INNER JOIN invGroups ON invGroups.groupID = invTypes.groupID\n" +
                      "    WHERE\n" +
                      "      invGroups.categoryID = " + categoryEntry.Value.ToString() + "\n" +
                      "      AND NOT EXISTS (\n" +
                      "        SELECT * FROM dgmTypeAttributes WHERE typeID = invTypes.typeID AND attributeID = dgmAttributeTypes.attributeID\n" +
                      "      )\n" +
                      (FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
                      "  )\n" +
                      "ORDER BY attributeName\n";
              break;

            case CategoryAttributeDiscoveryMethod.PossessedByMinimumNumber:
              query = "SELECT\n" +
                      "  attributeID,\n" +
                      "  attributeName,\n" +
                      "  description,\n" +
                      "  displayName,\n" +
                      "  published\n" +
                      "FROM\n" +
                      "  dgmAttributeTypes\n" +
                      "WHERE\n" +
                      "  (SELECT COUNT(*)\n" +
                      "   FROM\n" +
                      "     dgmTypeAttributes\n" +
                      "     INNER JOIN invTypes ON invTypes.typeID = dgmTypeAttributes.typeID\n" +
                      "     INNER JOIN invGroups ON invGroups.groupID = invTypes.groupID\n" +
                      "   WHERE\n" +
                      "     invGroups.categoryID = " + categoryEntry.Value.ToString() + "\n" +
                      (FindAttributesPublishedItemsOnly ? "     AND invTypes.published = 1\n" : string.Empty) +
                      "     AND dgmTypeAttributes.attributeID = dgmAttributeTypes.attributeID) >= " + FindAttributesMinimumNumber.ToString() + "\n" +
                      "ORDER BY attributeName\n";
              break;
          }
        }

        GroupListEntry groupEntry = SelectedAttributeCategory as GroupListEntry;
        if (groupEntry != null) {

          switch (FindAttributesDiscoveryMethod) {
            case CategoryAttributeDiscoveryMethod.PossessedByAll:
              query = "SELECT\n" +
                      "  attributeID,\n" +
                      "  attributeName,\n" +
                      "  description,\n" +
                      "  displayName,\n" +
                      "  published\n" +
                      "FROM\n" +
                      "  dgmAttributeTypes\n" +
                      "WHERE\n" +
                      "  EXISTS (\n" +
                      "    SELECT *\n" +
                      "    FROM invTypes\n" +
                      "    WHERE\n" +
                      "      invTypes.groupID = " + groupEntry.Value.ToString() + "\n" +
                      (FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
                      "  )\n" +
                      "  AND NOT EXISTS (\n" +
                      "    SELECT *\n" +
                      "    FROM invTypes\n" +
                      "    WHERE\n" +
                      "      invTypes.groupID = " + groupEntry.Value.ToString() + "\n" +
                      "      AND NOT EXISTS (\n" +
                      "        SELECT * FROM dgmTypeAttributes WHERE typeID = invTypes.typeID AND attributeID = dgmAttributeTypes.attributeID\n" +
                      "      )\n" +
                      (FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
                      "  )\n" +
                      "ORDER BY attributeName\n";
              break;

            case CategoryAttributeDiscoveryMethod.PossessedByMinimumNumber:
              query = "SELECT\n" +
                      "  attributeID,\n" +
                      "  attributeName,\n" +
                      "  description,\n" +
                      "  displayName\n," +
                      "  published\n" +
                      "FROM\n" +
                      "  dgmAttributeTypes\n" +
                      "WHERE\n" +
                      "  (SELECT COUNT(*)\n" +
                      "   FROM\n" +
                      "     dgmTypeAttributes\n" +
                      "     INNER JOIN invTypes ON invTypes.typeID = dgmTypeAttributes.typeID\n" +
                      "   WHERE\n" +
                      "     invTypes.groupID = " + groupEntry.Value.ToString() + "\n" +
                      (FindAttributesPublishedItemsOnly ? "     AND invTypes.published = 1\n" : string.Empty) +
                      "     AND dgmTypeAttributes.attributeID = dgmAttributeTypes.attributeID) >= " + FindAttributesMinimumNumber.ToString() + "\n" +
                      "ORDER BY attributeName\n";
              break;
          }
        }

        var factory = DbProviderFactories.GetFactory((DbConnection) connection);
        var adapter = factory.CreateDataAdapter();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = query;

        DataTable results = new DataTable();
        adapter.SelectCommand = (DbCommand) command;
        adapter.Fill(results);

        AttributesForSelectedCategory = results;

      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => {
        StatusText = string.Empty;
      };

      // Perform the action
      await BeginAsyncOperation(operation, onCleanup);
    }
    //******************************************************************************
    /// <summary>
    /// Generates the code for the selected enumeration.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task GenerateEnumCode() {

      // Define main task to be performed asynchronously
      Action operation = () => {
        StatusText = "Generating enumeration code...";

        IDbConnection connection;

        try {
          connection = GetCurrentDataSourceConnection();
        } catch (Exception ex) {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Generate Enum", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        MethodInfo generatorMethod = SelectedEnumCodeGenerator;

        if (generatorMethod.IsStatic) {
          GeneratedEnumCode = (string) generatorMethod.Invoke(null, new object[] { connection, null, null });

        } else {
          object instance = Activator.CreateInstance(generatorMethod.DeclaringType);
          GeneratedEnumCode = (string) generatorMethod.Invoke(instance, new object[] { connection, null, null });
        }
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => {
        StatusText = string.Empty;
      };

      // Perform the action
      await BeginAsyncOperation(operation, onCleanup);
    }
    //******************************************************************************
    /// <summary>
    /// Compares the current and comparison data sources and displays the results.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task LoadCategories() {

      // Define main task to be performed asynchronously
      Action operation = () => {

        StatusText = "Loading categories...";

        List<CategoryListEntry> results = new List<CategoryListEntry>();

        try {
          string query = "SELECT categoryID, categoryName FROM invCategories ORDER BY categoryName";

          IDbConnection connection = GetCurrentDataSourceConnection();

          var command = connection.CreateCommand();
          command.CommandText = query;

          using (var reader = command.ExecuteReader()) {

            while (reader.Read()) {
              int categoryId = reader["categoryID"].ConvertTo<int>();
              string categoryName = reader["categoryName"].ConvertTo<string>();

              results.Add(new CategoryListEntry(categoryId, categoryName));
            }
          }

        } catch (Exception ex) {
          MessageBox.Show("An error occurred while loading categories: " + ex.Message, "Load Categories", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        _eveCategories = results.AsReadOnly();
        OnPropertyChanged(new PropertyChangedEventArgs("EveCategories"));
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => {
        StatusText = string.Empty;
      };

      // Perform the action
      await BeginAsyncOperation(operation, onCleanup);
    }
    //******************************************************************************
    /// <summary>
    /// Loads the collection of EVE groups for the specified category.
    /// </summary>
    /// 
    /// <param name="categoryId">
    /// The ID of the category whose groups to return.
    /// </param>
    /// 
    /// <returns>
    /// The list of groups that belong to the specified category.
    /// </returns>
    public IEnumerable<GroupListEntry> LoadGroupsForCategory(int categoryId) {

      List<GroupListEntry> results = new List<GroupListEntry>();

      try {
        string query = "SELECT groupID, groupName FROM invGroups WHERE categoryID = " + categoryId.ToString() + " ORDER BY groupName";

        IDbConnection connection = GetCurrentDataSourceConnection();

        var command = connection.CreateCommand();
        Contract.Assume(command != null);

        command.CommandText = query;

        using (var reader = command.ExecuteReader()) {
          Contract.Assume(reader != null);

          while (reader.Read()) {
            int groupId = reader["groupID"].ConvertTo<int>();
            string groupName = reader["groupName"].ConvertTo<string>();

            results.Add(new GroupListEntry(groupId, groupName));
          }
        }

      } catch (Exception ex) {
        MessageBox.Show("An error occurred while loading groups: " + ex.Message, "Load Groups", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return results.AsReadOnly();
    }
    //******************************************************************************
    /// <summary>
    /// Tests the specified connection string.
    /// </summary>
    /// 
    /// <param name="providerName">
    /// The invariant name of the data provider for the connection source.
    /// </param>
    /// 
    /// <param name="connectionString">
    /// The connection string used to connect to the data source.
    /// </param>
    public void TestDataSource(string providerName, string connectionString) {
      Contract.Requires(!string.IsNullOrWhiteSpace(providerName), "The provider name cannot be null or empty.");
      Contract.Requires(!string.IsNullOrWhiteSpace(connectionString), "The connection string cannot be null or empty.");
      
      ProviderConnectionSource source = new ProviderConnectionSource(providerName, connectionString);

      try {
        var connection = source.GetConnection();
        MessageBox.Show("Connection successful!", "Data Source Test", MessageBoxButton.OK, MessageBoxImage.Information);
      
      } catch (Exception ex) {
        MessageBox.Show("The connection failed:\n\n" + ex.Message, "Data Source Test", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Tests the selected entity type by loading all instances from the database
    /// and attempting to access all public properties to make sure they
    /// load successfully.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task TestSelectedQuery() {

      // Define main task to be performed asynchronously
      Action operation = () => {
        StatusText = "Analyzing data entities...";

        Type queryType = null;

        foreach (CustomAttributeData attribute in SelectedQuery.CustomAttributes) {
          if (attribute.AttributeType == typeof(Eve.Data.EveQueryMethodAttribute)) {
            queryType = (Type) attribute.ConstructorArguments[0].Value;
            break;
          }
        }

        SelectedQueryTestResults = "Beginning testing of " + queryType.FullName + "..." + Environment.NewLine;
        SelectedQueryTestResults += "Loading items..." + Environment.NewLine;
        
        DbConnection connection = null;
        EveDataSource dataSource;

        try {
          connection = (DbConnection) GetCurrentDataSourceConnection();
        
        } catch (Exception ex) {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Test Entities", MessageBoxButton.OK, MessageBoxImage.Error);
          SelectedQueryTestResults += "A data connection could not be established, aborting!" + Environment.NewLine;
          return;
        }

        try {

          // The connection must be closed for some reason
          if (connection.State == ConnectionState.Open) {
            connection.Close();
          }

          EveDbContext context = new EveDbContext(connection, true);
          dataSource = new EveDataSource(context);

        } catch (Exception ex) {
          MessageBox.Show("The data context could not be established:\n\n" + ex.Message, "Test Entities", MessageBoxButton.OK, MessageBoxImage.Error);
          SelectedQueryTestResults += "The data context could not be established, aborting!" + Environment.NewLine;
          return;
        }

        var parameters = SelectedQuery.GetParameters();

        Type arrayType = parameters[0].ParameterType;
        Type parameterType = arrayType.GetElementType();
        var parameter = Array.CreateInstance(parameterType, 0);

        IReadOnlyList<object> entities = (IReadOnlyList<object>) SelectedQuery.Invoke(dataSource, new object[] { parameter });
                
        int entityCount = entities.Count;

        SelectedQueryTestResults += entityCount.ToString() + " entities loaded, testing each entity." + Environment.NewLine;

        int succeeded = 0;
        int failed = 0;

        foreach (object entity in entities) {
          try {
            TestEntityProperties(entity);
            succeeded++;

          } catch (InvalidOperationException ex) {
            SelectedQueryTestResults += ex.Message;
            failed++;
          }
        }

        SelectedQueryTestResults += succeeded.ToString() + " succeeded, " + failed.ToString() + " failed." + Environment.NewLine;
        SelectedQueryTestResults += "Test complete." + Environment.NewLine;
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => {
        StatusText = string.Empty;
      };

      // Perform the action
      await BeginAsyncOperation(operation, onCleanup);
    }
    #endregion
    #region Protected Methods
    //******************************************************************************
    /// <summary>
    /// Gets a connection for the current data source.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="IDbConnection" /> for the current data source.
    /// </returns>
    protected IDbConnection GetComparisonDataSourceConnection() {
      if (string.IsNullOrWhiteSpace(ComparisonDataSourceProvider) || string.IsNullOrWhiteSpace(ComparisonDataSourceConnectionString)) {
        throw new InvalidOperationException("You must provide a value for the data provider and connection string.");
      }

      ProviderConnectionSource source = new ProviderConnectionSource(ComparisonDataSourceProvider, ComparisonDataSourceConnectionString);
      return source.GetConnection();
    }
    //******************************************************************************
    /// <summary>
    /// Gets a connection for the current data source.
    /// </summary>
    /// 
    /// <returns>
    /// An <see cref="IDbConnection" /> for the current data source.
    /// </returns>
    protected IDbConnection GetCurrentDataSourceConnection() {
      if (string.IsNullOrWhiteSpace(CurrentDataSourceProvider) || string.IsNullOrWhiteSpace(CurrentDataSourceConnectionString)) {
        throw new InvalidOperationException("You must provide a value for the data provider and connection string.");
      }

      ProviderConnectionSource source = new ProviderConnectionSource(CurrentDataSourceProvider, CurrentDataSourceConnectionString);
      return source.GetConnection();
    }
    //******************************************************************************
    /// <summary>
    /// Returns the collection of available data providers.
    /// </summary>
    /// 
    /// <returns>
    /// The collection of available data providers.
    /// </returns>
    protected IEnumerable<ListEntry<string, string>> GetDataProviders() {
      List<ListEntry<string, string>> dataProviders = new List<ListEntry<string, string>>();

      using (DataTable providerTable = DbProviderFactories.GetFactoryClasses()) {
        Contract.Assume(providerTable != null);
        Contract.Assume(providerTable.Rows != null);

        foreach (DataRow providerRow in providerTable.Rows) {
          Contract.Assume(providerRow != null);

          dataProviders.Add(new ListEntry<string, string>((string) providerRow["InvariantName"], (string) providerRow["Name"]));
        }
      }

      return dataProviders.AsReadOnly();
    }
    //******************************************************************************
    /// <summary>
    /// Returns the collection of enumeration code generator methods.
    /// </summary>
    /// 
    /// <returns>
    /// The collection of enumeration code generator methods.
    /// </returns>
    protected IEnumerable<ListEntry<MethodInfo, string>> GetEnumCodeGenerators() {
      Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);

      List<ListEntry<MethodInfo, string>> generatorMethods = new List<ListEntry<MethodInfo, string>>();

      Type[] eveTypes = typeof(Eve.Meta.EnumCodeGenerator).Assembly.GetTypes();

      foreach (Type eveType in eveTypes) {
        MethodInfo[] methods = eveType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

        foreach (MethodInfo method in methods) {
          Contract.Assume(method != null);
          Contract.Assume(method.CustomAttributes != null);

          foreach (CustomAttributeData attribute in method.CustomAttributes) {
            Contract.Assume(attribute != null);

            if (attribute.AttributeType == typeof(Eve.Meta.EveEnumGeneratorAttribute)) {
              Contract.Assume(attribute.ConstructorArguments != null);
              Contract.Assume(0 < attribute.ConstructorArguments.Count);

              string enumName = (string) attribute.ConstructorArguments[0].Value;
              
              // If no enum name assigned in the attribute, use the method name
              if (string.IsNullOrWhiteSpace(enumName)) {
                enumName = method.Name;
              }

              generatorMethods.Add(new ListEntry<MethodInfo, string>(method, enumName));
              break;
            }
          }
        }
      }

      generatorMethods.Sort((x, y) => x.Value.Name.CompareTo(y.Value.Name));
      return generatorMethods.AsReadOnly();
    }
    //******************************************************************************
    /// <summary>
    /// Returns the collection of EVE query methods.
    /// </summary>
    /// 
    /// <returns>
    /// The collection of EVE query methods.
    /// </returns>
    protected IEnumerable<ListEntry<MethodInfo, string>> GetEveQueryMethods() {
      Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);

      List<ListEntry<MethodInfo, string>> queryMethods = new List<ListEntry<MethodInfo, string>>();

      Type[] eveTypes = typeof(Eve.General).Assembly.GetTypes();

      foreach (Type eveType in eveTypes) {
        MethodInfo[] methods = eveType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

        foreach (MethodInfo method in methods) {
          Contract.Assume(method != null);
          Contract.Assume(method.CustomAttributes != null);

          foreach (CustomAttributeData attribute in method.CustomAttributes) {
            Contract.Assume(attribute != null);

            if (attribute.AttributeType == typeof(Eve.Data.EveQueryMethodAttribute))
            {
              Contract.Assume(attribute.ConstructorArguments != null);
              Contract.Assume(0 < attribute.ConstructorArguments.Count);

              Type queryType = (Type) attribute.ConstructorArguments[0].Value;
              Contract.Assume(queryType != null);

              queryMethods.Add(new ListEntry<MethodInfo, string>(method, queryType.Name + " (" + queryType.Namespace + ")"));
              break;
            }
          }
        }
      }

      queryMethods.Sort((x, y) => x.Value.Name.CompareTo(y.Value.Name));
      return queryMethods.AsReadOnly();
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="AttributesForSelectedCategory" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnAttributesForSelectedCategoryChanged(DataTable oldValue, DataTable newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("AttributesForSelectedCategory"));
    }
    //******************************************************************************
    /// <inheritdoc />
    protected override void OnClosing() {
      base.OnClosing();

      Properties.Settings.Default["ComparisonDataSourceConnectionString"] = ComparisonDataSourceConnectionString;
      Properties.Settings.Default["ComparisonDataSourceProvider"] = ComparisonDataSourceProvider;
      Properties.Settings.Default["CurrentDataSourceConnectionString"] = CurrentDataSourceConnectionString;
      Properties.Settings.Default["CurrentDataSourceProvider"] = CurrentDataSourceProvider;
      Properties.Settings.Default.Save();
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="ComparisonDataSourceConnectionString" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnComparisonDataSourceConnectionStringChanged(string oldValue, string newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("ComparisonDataSourceConnectionString"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="ComparisonDataSourceProvider" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnComparisonDataSourceProviderChanged(string oldValue, string newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("ComparisonDataSourceProvider"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="CurrentDataSourceConnectionString" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnCurrentDataSourceConnectionStringChanged(string oldValue, string newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("CurrentDataSourceConnectionString"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="CurrentDataSourceProvider" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnCurrentDataSourceProviderChanged(string oldValue, string newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("CurrentDataSourceProvider"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="DataSourceComparison" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnDataSourceComparisonChanged(SchemaComparison oldValue, SchemaComparison newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("DataSourceComparison"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="DataSourceComparison" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnDataSourceComparisonSelectedValueChanged(object oldValue, object newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("DataSourceComparisonSelectedValue"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="FindAttributesDiscoveryMethod" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnFindAttributesDiscoveryMethodChanged(CategoryAttributeDiscoveryMethod oldValue, CategoryAttributeDiscoveryMethod newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("FindAttributesDiscoveryMethod"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="FindAttributesMinimumNumber" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnFindAttributesMinimumNumberChanged(int oldValue, int newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("FindAttributesMinimumNumber"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="FindAttributesPublishedItemsOnly" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnFindAttributesPublishedItemsOnlyChanged(bool oldValue, bool newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("FindAttributesPublishedItemsOnly"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="GeneratedEnumCode" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnGeneratedEnumCodeChanged(string oldValue, string newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("GeneratedEnumCode"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="SelectedAttributeCategory" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedAttributeCategoryChanged(object oldValue, object newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("SelectedAttributeCategory"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="SelectedEnumCodeGenerator" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedEnumCodeGeneratorChanged(MethodInfo oldValue, MethodInfo newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("SelectedEnumCodeGenerator"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="SelectedQuery" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedQueryChanged(MethodInfo oldValue, MethodInfo newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("SelectedQuery"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="SelectedQueryTestResults" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedQueryTestResultsChanged(string oldValue, string newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("SelectedQueryTestResults"));
    }
    //******************************************************************************
    /// <summary>
    /// Occurs when the value of the <see cref="StatusText" /> property has
    /// changed.
    /// </summary>
    /// 
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// 
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnStatusTextChanged(string oldValue, string newValue) {
      OnPropertyChanged(new PropertyChangedEventArgs("StatusText"));
    }
    //******************************************************************************
    /// <summary>
    /// Tests all public properties of the specified type.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The entity underlying the adapter type.
    /// </param>
    protected void TestEntityProperties(object entity) {
      Contract.Requires(entity != null, "The entity cannot be null.");

      Type entityType = entity.GetType();

      // Get all public properties
      PropertyInfo[] properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (PropertyInfo property in properties) {
        Contract.Assume(property != null);

        try {
          if (property.CanRead && property.GetIndexParameters().Length == 0) {
            object value = property.GetValue(entity);
          }

        } catch (Exception ex) {

          string entityName;

          try {
            entityName = entity.ToString();
          } catch {
            entityName = "[Unknown]";
          }

          Exception innermost = ex;

          while (innermost.InnerException != null) {
            innermost = innermost.InnerException;
          }

          string message = ex.Message + (innermost == null ? string.Empty : " (" + innermost.Message + ")");

          throw new InvalidOperationException("Entity " + entityName + " encountered an error accessing property " + property.Name + ": " + message + Environment.NewLine);
        }
      }
    }
    #endregion
  }
}