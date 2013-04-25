//-----------------------------------------------------------------------
// <copyright file="EveUpdaterWindowViewModel.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Data.Common;
  using System.Diagnostics.Contracts;
  using System.Reflection;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Windows;
  using System.Windows.Input;

  using Eve.Data;

  using FreeNet;
  using FreeNet.Data;
  using FreeNet.Data.Schema;
  using FreeNet.Utilities;
  using FreeNet.Windows;

  /// <summary>
  /// The view model for the <see cref="EveUpdaterWindow" /> class.
  /// </summary>
  public partial class EveUpdaterWindowViewModel : SimpleAsyncViewModelBase<EveUpdaterWindowViewModel, IEveUpdaterWindowService>
  {
    private DataTable attributesForSelectedCategory;
    private ICommand compareDataSourcesCommand;
    private string comparisonDataSourceConnectionString;
    private string comparisonDataSourceProvider;
    private string currentDataSourceConnectionString;
    private string currentDataSourceProvider;
    private IEnumerable<ListEntry<string, string>> dataProviders;
    private SchemaComparison dataSourceComparison;
    private object dataSourceComparisonSelectedValue;
    private IEnumerable<ListEntry<MethodInfo, string>> enumCodeGenerators;
    private IEnumerable<CategoryListEntry> eveCategories;
    private IEnumerable<ListEntry<MethodInfo, string>> eveQueryMethods;
    private CategoryAttributeDiscoveryMethod findAttributesDiscoveryMethod;
    private ICommand findAttributesForSelectedCategoryCommand;
    private int findAttributesMinimumNumber;
    private bool findAttributesPublishedItemsOnly;
    private string generatedEnumCode;
    private string generatedTableCode;
    private ICommand generateEnumCodeCommand;
    private ICommand generateTableCodeCommand;
    private ICommand loadCategoriesCommand;
    private MethodInfo selectedEnumCodeGenerator;
    private object selectedAttributeCategory;
    private string selectedQueryTestResults;
    private MethodInfo selectedQuery;
    private MethodInfo selectedTableCodeGenerator;
    private string statusText;
    private IEnumerable<ListEntry<MethodInfo, string>> tableCodeGenerators;
    private ICommand testDataSourceCommand;
    private ICommand testSelectedEntityCommand;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveUpdaterWindowViewModel class.
    /// </summary>
    /// <param name="service">
    /// A <see cref="EveUpdaterWindowService" /> which will provide external
    /// services for the ViewModel.
    /// </param>
    public EveUpdaterWindowViewModel(IEveUpdaterWindowService service) : base(service)
    {
      Contract.Requires(service != null, "The service cannot be null");

      this.attributesForSelectedCategory = null;
      this.compareDataSourcesCommand = new CompareDataSourcesSimpleCommand(this);
      this.comparisonDataSourceConnectionString = Properties.Settings.Default.ComparisonDataSourceConnectionString;
      this.comparisonDataSourceProvider = Properties.Settings.Default.ComparisonDataSourceProvider;
      this.currentDataSourceConnectionString = Properties.Settings.Default.CurrentDataSourceConnectionString;
      this.currentDataSourceProvider = Properties.Settings.Default.CurrentDataSourceProvider;
      this.dataSourceComparison = null;
      this.dataSourceComparisonSelectedValue = null;
      this.findAttributesDiscoveryMethod = CategoryAttributeDiscoveryMethod.PossessedByAll;
      this.findAttributesForSelectedCategoryCommand = new FindAttributesForCategorySimpleCommand(this);
      this.findAttributesMinimumNumber = 1;
      this.findAttributesPublishedItemsOnly = true;
      this.generatedEnumCode = string.Empty;
      this.generatedTableCode = string.Empty;
      this.generateEnumCodeCommand = new GenerateEnumCodeSimpleCommand(this);
      this.generateTableCodeCommand = new GenerateTableCodeSimpleCommand(this);
      this.loadCategoriesCommand = new LoadCategoriesSimpleCommand(this);
      this.selectedQueryTestResults = string.Empty;
      this.statusText = string.Empty;
      this.testDataSourceCommand = new TestDataSourceSimpleCommand(this);
      this.testSelectedEntityCommand = new TestEntitySimpleCommand(this);

      this.dataProviders = this.GetDataProviders();
      this.enumCodeGenerators = this.GetEnumCodeGenerators();
      this.eveQueryMethods = this.GetEveQueryMethods();
      this.tableCodeGenerators = this.GetTableCodeGenerators();
    }

    /* Properties */

    /// <summary>
    /// Gets or sets the table containing the attribute information for the
    /// selected category.
    /// </summary>
    /// <value>
    /// A <see cref="DataTable" /> containing information about the attributes
    /// that apply to the selected category.
    /// </value> 
    public DataTable AttributesForSelectedCategory
    {
      get
      {
        return this.attributesForSelectedCategory;
      }

      set
      {
        if (value == this.attributesForSelectedCategory)
        {
          return;
        }

        var oldValue = this.attributesForSelectedCategory;
        this.attributesForSelectedCategory = value;
        this.OnAttributesForSelectedCategoryChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets a value indicating whether the enumeration code can be generated.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the window is in a state where enumeration
    /// code can be generated; otherwise <see langword="false" />.
    /// </value>
    public bool CanGenerateEnumCode
    {
      get
      {
        return this.SelectedEnumCodeGenerator != null;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the database table code can be generated.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the window is in a state where database table
    /// code can be generated; otherwise <see langword="false" />.
    /// </value>
    public bool CanGenerateTableCode
    {
      get
      {
        return this.SelectedTableCodeGenerator != null;
      }
    }

    /// <summary>
    /// Gets the command to compare the data sources.
    /// </summary>
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of comparing the
    /// two data sources.
    /// </value>
    public ICommand CompareDataSourcesCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return this.compareDataSourcesCommand;
      }
    }

    /// <summary>
    /// Gets or sets the connection string used by the comparison data source.
    /// </summary>
    /// <value>
    /// The connection string used by the comparison data source.
    /// </value>
    public string ComparisonDataSourceConnectionString
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.comparisonDataSourceConnectionString;
      }

      set
      {
        if (value == this.comparisonDataSourceConnectionString)
        {
          return;
        }

        if (value == null)
        {
          value = string.Empty;
        }

        string oldValue = this.comparisonDataSourceConnectionString;
        this.comparisonDataSourceConnectionString = value;
        this.OnComparisonDataSourceConnectionStringChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the provider name used by the comparison data source.
    /// </summary>
    /// <value>
    /// The provider name string used by the comparison data source.
    /// </value>
    public string ComparisonDataSourceProvider
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.comparisonDataSourceProvider;
      }

      set
      {
        if (value == this.comparisonDataSourceProvider)
        {
          return;
        }

        if (value == null)
        {
          value = string.Empty;
        }

        string oldValue = this.comparisonDataSourceProvider;
        this.comparisonDataSourceProvider = value;
        this.OnComparisonDataSourceProviderChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the connection string used by the current data source.
    /// </summary>
    /// <value>
    /// The connection string used by the current data source.
    /// </value>
    public string CurrentDataSourceConnectionString
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.currentDataSourceConnectionString;
      }

      set
      {
        if (value == this.currentDataSourceConnectionString)
        {
          return;
        }

        if (value == null)
        {
          value = string.Empty;
        }

        string oldValue = this.currentDataSourceConnectionString;
        this.currentDataSourceConnectionString = value;
        this.OnCurrentDataSourceConnectionStringChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the provider name used by the current data source.
    /// </summary>
    /// <value>
    /// The provider name used by the current data source.
    /// </value>
    public string CurrentDataSourceProvider
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.currentDataSourceProvider;
      }

      set
      {
        if (value == this.currentDataSourceProvider)
        {
          return;
        }

        if (value == null)
        {
          value = string.Empty;
        }

        string oldValue = this.currentDataSourceProvider;
        this.currentDataSourceProvider = value;
        this.OnCurrentDataSourceProviderChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets the collection of available data providers.
    /// </summary>
    /// <value>
    /// The collection of available data providers.
    /// </value>
    public IEnumerable<ListEntry<string, string>> DataProviders
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<ListEntry<string, string>>>() != null);
        return this.dataProviders;
      }
    }

    /// <summary>
    /// Gets or sets the information for the data source comparison overview.
    /// </summary>
    /// <value>
    /// The data source comparison overview.
    /// </value>
    public SchemaComparison DataSourceComparison
    {
      get
      {
        return this.dataSourceComparison;
      }

      set
      {
        if (value == this.dataSourceComparison)
        {
          return;
        }

        var oldValue = this.dataSourceComparison;
        this.dataSourceComparison = value;
        this.OnDataSourceComparisonChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the selected value in the data source comparison overview.
    /// </summary>
    /// <value>
    /// The selected item in the data source comparison overview.
    /// </value>
    public object DataSourceComparisonSelectedValue
    {
      get
      {
        return this.dataSourceComparisonSelectedValue;
      }

      set
      {
        if (value == this.dataSourceComparisonSelectedValue)
        {
          return;
        }

        var oldValue = this.dataSourceComparisonSelectedValue;
        this.dataSourceComparisonSelectedValue = value;
        this.OnDataSourceComparisonSelectedValueChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets the collection of methods that generate enumeration code.
    /// </summary>
    /// <value>
    /// The collection of methods that generate enumeration code.
    /// </value>
    public IEnumerable<ListEntry<MethodInfo, string>> EnumCodeGenerators
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);
        return this.enumCodeGenerators;
      }
    }

    /// <summary>
    /// Gets the collection of EVE categories.
    /// </summary>
    /// <value>
    /// The collection of EVE categories.
    /// </value>
    public IEnumerable<CategoryListEntry> EveCategories
    {
      get { return this.eveCategories; }
    }

    /// <summary>
    /// Gets the collection of EVE game-related entity adapters.
    /// </summary>
    /// <value>
    /// The collection of EVE game-related entity adapters.
    /// </value>
    public IEnumerable<ListEntry<MethodInfo, string>> EveQueryMethods
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);
        return this.eveQueryMethods;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating how to find attributes for the selected category.
    /// </summary>
    /// <value>
    /// A <see cref="CategoryAttributeDiscoveryMethod" /> specifying how to find
    /// attributes for the selected category.
    /// </value>
    public CategoryAttributeDiscoveryMethod FindAttributesDiscoveryMethod
    {
      get
      {
        return this.findAttributesDiscoveryMethod;
      }

      set
      {
        if (value == this.findAttributesDiscoveryMethod)
        {
          return;
        }

        var oldValue = this.findAttributesDiscoveryMethod;
        this.findAttributesDiscoveryMethod = value;
        this.OnFindAttributesDiscoveryMethodChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the minimum number of items in the selected category that must 
    /// possess an attribute before it is included in the results, if
    /// <see cref="FindAttributesDiscoveryMethod" /> is
    /// <chars>PossessedByMinimumNumber</chars>.
    /// </summary>
    /// <value>
    /// The minimum number of items that must possess an attribute before it is
    /// included.
    /// </value>
    public int FindAttributesMinimumNumber
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 1);
        return this.findAttributesMinimumNumber;
      }

      set
      {
        Contract.Requires(value >= 1, "The minimum number cannot be less than 1.");

        if (value == this.findAttributesMinimumNumber)
        {
          return;
        }

        var oldValue = this.findAttributesMinimumNumber;
        this.findAttributesMinimumNumber = value;
        this.OnFindAttributesMinimumNumberChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether only published items should be considered
    /// when search for attributes possessed by the selected category.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if only published items should be considered;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool FindAttributesPublishedItemsOnly
    {
      get
      {
        return this.findAttributesPublishedItemsOnly;
      }

      set
      {
        if (value == this.findAttributesPublishedItemsOnly)
        {
          return;
        }

        var oldValue = this.findAttributesPublishedItemsOnly;
        this.findAttributesPublishedItemsOnly = value;
        this.OnFindAttributesPublishedItemsOnlyChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets the command to find the EVE attributes that apply to the selected
    /// category.
    /// </summary>
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of finding the
    /// applicable EVE attributes.
    /// </value>
    public ICommand FindAttributesForSelectedCategoryCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return this.findAttributesForSelectedCategoryCommand;
      }
    }

    /// <summary>
    /// Gets or sets the generated enumeration code.
    /// </summary>
    /// <value>
    /// The generated enumeration code.
    /// </value>
    public string GeneratedEnumCode
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.generatedEnumCode;
      }

      set
      {
        if (value == this.generatedEnumCode)
        {
          return;
        }

        if (value == null)
        {
          value = string.Empty;
        }

        string oldValue = this.generatedEnumCode;
        this.generatedEnumCode = value;
        this.OnGeneratedEnumCodeChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the generated database table code.
    /// </summary>
    /// <value>
    /// The generated database table code.
    /// </value>
    public string GeneratedTableCode
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.generatedTableCode;
      }

      set
      {
        if (value == this.generatedTableCode)
        {
          return;
        }

        if (value == null)
        {
          value = string.Empty;
        }

        string oldValue = this.generatedTableCode;
        this.generatedTableCode = value;
        this.OnGeneratedTableCodeChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets the command to generate enumeration code.
    /// </summary>
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of generating
    /// enumeration code.
    /// </value>
    public ICommand GenerateEnumCodeCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return this.generateEnumCodeCommand;
      }
    }

    /// <summary>
    /// Gets the command to generate database table code.
    /// </summary>
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of generating
    /// database table code.
    /// </value>
    public ICommand GenerateTableCodeCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return this.generateTableCodeCommand;
      }
    }

    /// <summary>
    /// Gets the command to load the category list.
    /// </summary>
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of loading the list
    /// of EVE categories.
    /// </value>
    public ICommand LoadCategoriesCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return this.loadCategoriesCommand;
      }
    }

    /// <summary>
    /// Gets or sets the selected value in the list of attribute categories.
    /// </summary>
    /// <value>
    /// The selected value in the list of attribute categories.
    /// </value>
    public object SelectedAttributeCategory
    {
      get
      {
        return this.selectedAttributeCategory;
      }

      set
      {
        if (value == this.selectedAttributeCategory)
        {
          return;
        }

        var oldValue = this.selectedAttributeCategory;
        this.selectedAttributeCategory = value;
        this.OnSelectedAttributeCategoryChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the selected enumeration code generator method.
    /// </summary>
    /// <value>
    /// The selected enumeration code generator method.
    /// </value>
    public MethodInfo SelectedEnumCodeGenerator
    {
      get
      {
        return this.selectedEnumCodeGenerator;
      }

      set
      {
        if (value == this.selectedEnumCodeGenerator)
        {
          return;
        }

        var oldValue = this.selectedEnumCodeGenerator;
        this.selectedEnumCodeGenerator = value;
        this.OnSelectedEnumCodeGeneratorChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the selected enumeration code generator method.
    /// </summary>
    /// <value>
    /// The selected enumeration code generator method.
    /// </value>
    public MethodInfo SelectedQuery
    {
      get
      {
        return this.selectedQuery;
      }

      set
      {
        if (value == this.selectedQuery)
        {
          return;
        }

        var oldValue = this.selectedQuery;
        this.selectedQuery = value;
        this.OnSelectedQueryChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the test results for the selected entity.
    /// </summary>
    /// <value>
    /// The test results for the selected entity.
    /// </value>
    public string SelectedQueryTestResults
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.selectedQueryTestResults;
      }

      set
      {
        Contract.Requires(value != null, "The test results cannot be null.");

        if (value == this.selectedQueryTestResults)
        {
          return;
        }

        var oldValue = this.selectedQueryTestResults;
        this.selectedQueryTestResults = value;
        this.OnSelectedQueryTestResultsChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the selected database table code generator method.
    /// </summary>
    /// <value>
    /// The selected database table code generator method.
    /// </value>
    public MethodInfo SelectedTableCodeGenerator
    {
      get
      {
        return this.selectedTableCodeGenerator;
      }

      set
      {
        if (value == this.selectedTableCodeGenerator)
        {
          return;
        }

        var oldValue = this.selectedTableCodeGenerator;
        this.selectedTableCodeGenerator = value;
        this.OnSelectedTableCodeGeneratorChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets or sets the text displayed in the status bar.
    /// </summary>
    /// <value>
    /// The text displayed in the status bar.
    /// </value>
    public string StatusText
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.statusText;
      }

      set
      {
        if (value == this.statusText)
        {
          return;
        }

        if (value == null)
        {
          value = string.Empty;
        }

        string oldValue = this.statusText;
        this.statusText = value;
        this.OnStatusTextChanged(oldValue, value);
      }
    }

    /// <summary>
    /// Gets the collection of methods that generate database table code.
    /// </summary>
    /// <value>
    /// The collection of methods that generate database table code.
    /// </value>
    public IEnumerable<ListEntry<MethodInfo, string>> TableCodeGenerators
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);
        return this.tableCodeGenerators;
      }
    }

    /// <summary>
    /// Gets the command to test a data source.
    /// </summary>
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of testing a data
    /// source.
    /// </value>
    public ICommand TestDataSourceCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return this.testDataSourceCommand;
      }
    }

    /// <summary>
    /// Gets the command to test the selected entity.
    /// </summary>
    /// <value>
    /// The <see cref="ICommand" /> which triggers the action of testing the
    /// selected entity.
    /// </value>
    public ICommand TestSelectedEntityCommand
    {
      get
      {
        Contract.Ensures(Contract.Result<ICommand>() != null);
        return this.testSelectedEntityCommand;
      }
    }

    /* Methods */

    /// <summary>
    /// Compares the current and comparison data sources and displays the results.
    /// </summary>
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task CompareDataSources()
    {
      // Define main task to be performed asynchronously
      Action operation = () =>
      {
        this.StatusText = "Analyzing database schemas...";

        IDbConnection currentConnection;
        IDbConnection comparisonConnection;

        try
        {
          currentConnection = this.GetCurrentDataSourceConnection();

          if (currentConnection.State != ConnectionState.Open)
          {
            currentConnection.Open();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Compare Data Sources", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        try
        {
          comparisonConnection = this.GetComparisonDataSourceConnection();

          if (comparisonConnection.State != ConnectionState.Open)
          {
            comparisonConnection.Open();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("A connection to the comparison data source could not be established:\n\n" + ex.Message, "Compare Data Sources", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        if (!(currentConnection is DbConnection))
        {
          MessageBox.Show("The connection to the current data source does not support the functionality needed to display the data structure.");
          return;
        }

        if (!(currentConnection is DbConnection))
        {
          MessageBox.Show("The connection to the comparison data source does not support the functionality needed to display the data structure.");
          return;
        }

        this.CancellationTokenSource.Token.ThrowIfCancellationRequested();
        Schema currentSchema = new Schema((DbConnection)currentConnection, true);

        this.CancellationTokenSource.Token.ThrowIfCancellationRequested();
        Schema comparisonSchema = new Schema((DbConnection)comparisonConnection, true);

        this.CancellationTokenSource.Token.ThrowIfCancellationRequested();
        this.DataSourceComparison = currentSchema.GetComparison(comparisonSchema);
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => this.StatusText = string.Empty;

      // Perform the action
      await this.BeginAsyncOperation(operation, onCleanup);
    }

    /// <summary>
    /// Compares the current and comparison data sources and displays the results.
    /// </summary>
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task FindAttributesForSelectedCategory()
    {
      // Define main task to be performed asynchronously
      Action operation = () =>
      {
        this.StatusText = "Finding attributes...";

        IDbConnection connection;

        try
        {
          connection = this.GetCurrentDataSourceConnection();

          if (connection.State != ConnectionState.Open)
          {
            connection.Open();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Find Attributes", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        string query = string.Empty;

        CategoryListEntry categoryEntry = this.SelectedAttributeCategory as CategoryListEntry;
        if (categoryEntry != null)
        {
          switch (this.FindAttributesDiscoveryMethod)
          {
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
                      (this.FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
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
                      (this.FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
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
                      (this.FindAttributesPublishedItemsOnly ? "     AND invTypes.published = 1\n" : string.Empty) +
                      "     AND dgmTypeAttributes.attributeID = dgmAttributeTypes.attributeID) >= " + this.FindAttributesMinimumNumber.ToString() + "\n" +
                      "ORDER BY attributeName\n";
              break;
          }
        }

        GroupListEntry groupEntry = this.SelectedAttributeCategory as GroupListEntry;
        if (groupEntry != null)
        {
          switch (FindAttributesDiscoveryMethod)
          {
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
                      (this.FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
                      "  )\n" +
                      "  AND NOT EXISTS (\n" +
                      "    SELECT *\n" +
                      "    FROM invTypes\n" +
                      "    WHERE\n" +
                      "      invTypes.groupID = " + groupEntry.Value.ToString() + "\n" +
                      "      AND NOT EXISTS (\n" +
                      "        SELECT * FROM dgmTypeAttributes WHERE typeID = invTypes.typeID AND attributeID = dgmAttributeTypes.attributeID\n" +
                      "      )\n" +
                      (this.FindAttributesPublishedItemsOnly ? "      AND invTypes.published = 1\n" : string.Empty) +
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
                      (this.FindAttributesPublishedItemsOnly ? "     AND invTypes.published = 1\n" : string.Empty) +
                      "     AND dgmTypeAttributes.attributeID = dgmAttributeTypes.attributeID) >= " + this.FindAttributesMinimumNumber.ToString() + "\n" +
                      "ORDER BY attributeName\n";
              break;
          }
        }

        var factory = DbProviderFactories.GetFactory((DbConnection)connection);
        var adapter = factory.CreateDataAdapter();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = query;

        DataTable results = new DataTable();
        adapter.SelectCommand = (DbCommand)command;
        adapter.Fill(results);

        this.AttributesForSelectedCategory = results;
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => this.StatusText = string.Empty;

      // Perform the action
      await this.BeginAsyncOperation(operation, onCleanup);
    }

    /// <summary>
    /// Generates the code for the selected enumeration.
    /// </summary>
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task GenerateEnumCode()
    {
      // Define main task to be performed asynchronously
      Action operation = () =>
      {
        this.StatusText = "Generating enumeration code...";

        IDbConnection connection;

        try
        {
          connection = this.GetCurrentDataSourceConnection();
        }
        catch (Exception ex)
        {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Generate Enum", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        MethodInfo generatorMethod = this.SelectedEnumCodeGenerator;

        if (generatorMethod.IsStatic)
        {
          this.GeneratedEnumCode = (string)generatorMethod.Invoke(null, new object[] { connection, null, null });
        }
        else
        {
          object instance = Activator.CreateInstance(generatorMethod.DeclaringType);
          this.GeneratedEnumCode = (string)generatorMethod.Invoke(instance, new object[] { connection, null, null });
        }
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => this.StatusText = string.Empty;

      // Perform the action
      await this.BeginAsyncOperation(operation, onCleanup);
    }

    /// <summary>
    /// Generates the code for the selected database table.
    /// </summary>
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task GenerateTableCode()
    {
      // Define main task to be performed asynchronously
      Action operation = () =>
      {
        this.StatusText = "Loading data file...";

        string filename = this.Service.PromptOpenFile("Select Data File", "YAML files|*.yaml|All Files|*.*");

        if (filename == null)
        {
          return;
        }

        this.StatusText = "Generating database table code...";

        MethodInfo generatorMethod = this.SelectedTableCodeGenerator;

        if (generatorMethod.IsStatic)
        {
          this.GeneratedTableCode = (string)generatorMethod.Invoke(null, new object[] { filename });
        }
        else
        {
          object instance = Activator.CreateInstance(generatorMethod.DeclaringType);
          this.GeneratedTableCode = (string)generatorMethod.Invoke(instance, new object[] { filename });
        }
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => this.StatusText = string.Empty;

      // Perform the action
      await this.BeginAsyncOperation(operation, onCleanup);
    }

    /// <summary>
    /// Compares the current and comparison data sources and displays the results.
    /// </summary>
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task LoadCategories()
    {
      // Define main task to be performed asynchronously
      Action operation = () =>
      {
        this.StatusText = "Loading categories...";

        List<CategoryListEntry> results = new List<CategoryListEntry>();

        try
        {
          string query = "SELECT categoryID, categoryName FROM invCategories ORDER BY categoryName";

          IDbConnection connection = GetCurrentDataSourceConnection();

          var command = connection.CreateCommand();
          command.CommandText = query;

          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              int categoryId = reader["categoryID"].ConvertTo<int>();
              string categoryName = reader["categoryName"].ConvertTo<string>();

              results.Add(new CategoryListEntry(categoryId, categoryName));
            }
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("An error occurred while loading categories: " + ex.Message, "GetOrAddStoredValue Categories", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        this.eveCategories = results.AsReadOnly();
        this.OnPropertyChanged(new PropertyChangedEventArgs("EveCategories"));
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => this.StatusText = string.Empty;

      // Perform the action
      await this.BeginAsyncOperation(operation, onCleanup);
    }

    /// <summary>
    /// Loads the collection of EVE groups for the specified category.
    /// </summary>
    /// <param name="categoryId">
    /// The ID of the category whose groups to return.
    /// </param>
    /// <returns>
    /// The list of groups that belong to the specified category.
    /// </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Only integer values, injection not possible.")]
    public IEnumerable<GroupListEntry> LoadGroupsForCategory(int categoryId)
    {
      List<GroupListEntry> results = new List<GroupListEntry>();

      try
      {
        string query = "SELECT groupID, groupName FROM invGroups WHERE categoryID = " + categoryId.ToString() + " ORDER BY groupName";

        IDbConnection connection = this.GetCurrentDataSourceConnection();

        var command = connection.CreateCommand();
        Contract.Assume(command != null);

        command.CommandText = query;

        using (var reader = command.ExecuteReader())
        {
          Contract.Assume(reader != null);

          while (reader.Read())
          {
            int groupId = reader["groupID"].ConvertTo<int>();
            string groupName = reader["groupName"].ConvertTo<string>();

            results.Add(new GroupListEntry(groupId, groupName));
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("An error occurred while loading groups: " + ex.Message, "GetOrAddStoredValue Groups", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return results.AsReadOnly();
    }

    /// <summary>
    /// Tests the specified connection string.
    /// </summary>
    /// <param name="providerName">
    /// The invariant name of the data provider for the connection source.
    /// </param>
    /// <param name="connectionString">
    /// The connection string used to connect to the data source.
    /// </param>
    public void TestDataSource(string providerName, string connectionString)
    {
      Contract.Requires(!string.IsNullOrWhiteSpace(providerName), "The provider name cannot be null or empty.");
      Contract.Requires(!string.IsNullOrWhiteSpace(connectionString), "The connection string cannot be null or empty.");

      ProviderConnectionSource source = new ProviderConnectionSource(providerName, connectionString);

      try
      {
        var connection = source.GetConnection();
        MessageBox.Show("Connection successful!", "Data Source Test", MessageBoxButton.OK, MessageBoxImage.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show("The connection failed:\n\n" + ex.Message, "Data Source Test", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    /// <summary>
    /// Tests the selected entity type by loading all instances from the database
    /// and attempting to access all public properties to make sure they
    /// load successfully.
    /// </summary>
    /// <returns>
    /// A <see cref="Task" /> which can be used to interact with the method.
    /// </returns>
    public async Task TestSelectedQuery()
    {
      // Define main task to be performed asynchronously
      Action operation = () =>
      {
        this.StatusText = "Analyzing data entities...";

        Type queryType = null;

        foreach (CustomAttributeData attribute in this.SelectedQuery.CustomAttributes)
        {
          if (attribute.AttributeType == typeof(Eve.Data.EveQueryMethodAttribute))
          {
            queryType = (Type)attribute.ConstructorArguments[0].Value;
            break;
          }
        }

        this.SelectedQueryTestResults = "Beginning testing of " + queryType.FullName + "..." + Environment.NewLine;
        this.SelectedQueryTestResults += "Loading items..." + Environment.NewLine;

        DbConnection connection = null;
        EveRepository dataSource;

        try
        {
          connection = (DbConnection)GetCurrentDataSourceConnection();
        }
        catch (Exception ex)
        {
          MessageBox.Show("A connection to the current data source could not be established:\n\n" + ex.Message, "Test Entities", MessageBoxButton.OK, MessageBoxImage.Error);
          this.SelectedQueryTestResults += "A data connection could not be established, aborting!" + Environment.NewLine;
          return;
        }

        try
        {
          // The connection must be closed for some reason
          if (connection.State == ConnectionState.Open)
          {
            connection.Close();
          }

          IEveDbContext context = new EveDbContext(connection, true);
          dataSource = new EveRepository(context, null);
        }
        catch (Exception ex)
        {
          MessageBox.Show("The data context could not be established:\n\n" + ex.Message, "Test Entities", MessageBoxButton.OK, MessageBoxImage.Error);
          this.SelectedQueryTestResults += "The data context could not be established, aborting!" + Environment.NewLine;
          return;
        }

        var parameters = SelectedQuery.GetParameters();

        Type arrayType = parameters[0].ParameterType;
        Type parameterType = arrayType.GetElementType();
        var parameter = Array.CreateInstance(parameterType, 0);

        IReadOnlyList<object> entities = (IReadOnlyList<object>)this.SelectedQuery.Invoke(dataSource, new object[] { parameter });
        var entityCount = entities.Count;

        this.SelectedQueryTestResults += entityCount.ToString() + " entities loaded, testing each entity." + Environment.NewLine;

        int succeeded = 0;
        int failed = 0;
        int total = 0;

        foreach (object entity in entities)
        {
          total++;

          if (total % 500 == 0)
          {
            this.SelectedQueryTestResults += "Tested " + total.ToString() + " entities..." + Environment.NewLine;
          }

          try
          {
            this.TestEntityProperties(entity);
            succeeded++;
          }
          catch (InvalidOperationException ex)
          {
            this.SelectedQueryTestResults += ex.Message;
            failed++;
          }
        }

        this.SelectedQueryTestResults += succeeded.ToString() + " succeeded, " + failed.ToString() + " failed." + Environment.NewLine;
        this.SelectedQueryTestResults += "Test complete." + Environment.NewLine;
      };

      // Define the operation to perform after the main task has completed or been canceled
      Action onCleanup = () => this.StatusText = string.Empty;

      // Perform the action
      await this.BeginAsyncOperation(operation, onCleanup);
    }

    /// <summary>
    /// Gets a connection for the current data source.
    /// </summary>
    /// <returns>
    /// An <see cref="IDbConnection" /> for the current data source.
    /// </returns>
    protected IDbConnection GetComparisonDataSourceConnection()
    {
      if (string.IsNullOrWhiteSpace(this.ComparisonDataSourceProvider) || string.IsNullOrWhiteSpace(this.ComparisonDataSourceConnectionString))
      {
        throw new InvalidOperationException("You must provide a value for the data provider and connection string.");
      }

      ProviderConnectionSource source = new ProviderConnectionSource(this.ComparisonDataSourceProvider, this.ComparisonDataSourceConnectionString);
      return source.GetConnection();
    }

    /// <summary>
    /// Gets a connection for the current data source.
    /// </summary>
    /// <returns>
    /// An <see cref="IDbConnection" /> for the current data source.
    /// </returns>
    protected IDbConnection GetCurrentDataSourceConnection()
    {
      if (string.IsNullOrWhiteSpace(this.CurrentDataSourceProvider) || string.IsNullOrWhiteSpace(this.CurrentDataSourceConnectionString))
      {
        throw new InvalidOperationException("You must provide a value for the data provider and connection string.");
      }

      ProviderConnectionSource source = new ProviderConnectionSource(this.CurrentDataSourceProvider, this.CurrentDataSourceConnectionString);
      return source.GetConnection();
    }

    /// <summary>
    /// Returns the collection of available data providers.
    /// </summary>
    /// <returns>
    /// The collection of available data providers.
    /// </returns>
    protected IEnumerable<ListEntry<string, string>> GetDataProviders()
    {
      List<ListEntry<string, string>> dataProviders = new List<ListEntry<string, string>>();

      using (DataTable providerTable = DbProviderFactories.GetFactoryClasses())
      {
        Contract.Assume(providerTable != null);
        Contract.Assume(providerTable.Rows != null);

        foreach (DataRow providerRow in providerTable.Rows)
        {
          Contract.Assume(providerRow != null);

          dataProviders.Add(new ListEntry<string, string>((string)providerRow["InvariantName"], (string)providerRow["Name"]));
        }
      }

      return dataProviders.AsReadOnly();
    }

    /// <summary>
    /// Returns the collection of enumeration code generator methods.
    /// </summary>
    /// <returns>
    /// The collection of enumeration code generator methods.
    /// </returns>
    protected IEnumerable<ListEntry<MethodInfo, string>> GetEnumCodeGenerators()
    {
      Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);

      List<ListEntry<MethodInfo, string>> generatorMethods = new List<ListEntry<MethodInfo, string>>();

      Type[] eveTypes = typeof(Eve.Meta.EnumCodeGenerator).Assembly.GetTypes();

      foreach (Type eveType in eveTypes)
      {
        MethodInfo[] methods = eveType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
          Contract.Assume(method != null);

          var attribute = method.GetCustomAttribute<Eve.Meta.EveEnumGeneratorAttribute>();
          if (attribute != null)
          {
            string enumName = (string)attribute.EnumName;

            // If no enum name assigned in the attribute, use the method name
            if (string.IsNullOrWhiteSpace(enumName))
            {
              enumName = method.Name;
            }

            generatorMethods.Add(new ListEntry<MethodInfo, string>(method, enumName));
          }
        }
      }

      generatorMethods.Sort((x, y) => x.Value.Name.CompareTo(y.Value.Name));
      return generatorMethods.AsReadOnly();
    }

    /// <summary>
    /// Returns the collection of EVE query methods.
    /// </summary>
    /// <returns>
    /// The collection of EVE query methods.
    /// </returns>
    protected IEnumerable<ListEntry<MethodInfo, string>> GetEveQueryMethods()
    {
      Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);

      List<ListEntry<MethodInfo, string>> queryMethods = new List<ListEntry<MethodInfo, string>>();

      Type[] eveTypes = typeof(Eve.General).Assembly.GetTypes();

      foreach (Type eveType in eveTypes)
      {
        MethodInfo[] methods = eveType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
          Contract.Assume(method != null);

          var attribute = method.GetCustomAttribute<Eve.Data.EveQueryMethodAttribute>();

          if (attribute != null)
          {
            Type queryType = attribute.QueryType;
            queryMethods.Add(new ListEntry<MethodInfo, string>(method, queryType.Name + " (" + queryType.Namespace + ")"));
          }
        }
      }

      queryMethods.Sort((x, y) => x.Value.Name.CompareTo(y.Value.Name));
      return queryMethods.AsReadOnly();
    }

    /// <summary>
    /// Returns the collection of database table code generator methods.
    /// </summary>
    /// <returns>
    /// The collection of database table code generator methods.
    /// </returns>
    protected IEnumerable<ListEntry<MethodInfo, string>> GetTableCodeGenerators()
    {
      Contract.Ensures(Contract.Result<IEnumerable<ListEntry<MethodInfo, string>>>() != null);

      List<ListEntry<MethodInfo, string>> generatorMethods = new List<ListEntry<MethodInfo, string>>();

      Type[] eveTypes = typeof(Eve.Meta.TableCodeGenerator).Assembly.GetTypes();

      foreach (Type eveType in eveTypes)
      {
        MethodInfo[] methods = eveType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

        foreach (MethodInfo method in methods)
        {
          Contract.Assume(method != null);

          var attribute = method.GetCustomAttribute<Eve.Meta.EveTableGeneratorAttribute>();
          if (attribute != null)
          {
            string tableName = (string)attribute.TableName;

            // If no enum name assigned in the attribute, use the method name
            if (string.IsNullOrWhiteSpace(tableName))
            {
              tableName = method.Name;
            }

            generatorMethods.Add(new ListEntry<MethodInfo, string>(method, tableName));
          }
        }
      }

      generatorMethods.Sort((x, y) => x.Value.Name.CompareTo(y.Value.Name));
      return generatorMethods.AsReadOnly();
    }

    /// <summary>
    /// Occurs when the value of the <see cref="AttributesForSelectedCategory" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnAttributesForSelectedCategoryChanged(DataTable oldValue, DataTable newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("AttributesForSelectedCategory"));
    }

    /// <inheritdoc />
    protected override void OnClosing()
    {
      base.OnClosing();

      Properties.Settings.Default["ComparisonDataSourceConnectionString"] = this.ComparisonDataSourceConnectionString;
      Properties.Settings.Default["ComparisonDataSourceProvider"] = this.ComparisonDataSourceProvider;
      Properties.Settings.Default["CurrentDataSourceConnectionString"] = this.CurrentDataSourceConnectionString;
      Properties.Settings.Default["CurrentDataSourceProvider"] = this.CurrentDataSourceProvider;
      Properties.Settings.Default.Save();
    }

    /// <summary>
    /// Occurs when the value of the <see cref="ComparisonDataSourceConnectionString" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnComparisonDataSourceConnectionStringChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("ComparisonDataSourceConnectionString"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="ComparisonDataSourceProvider" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnComparisonDataSourceProviderChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("ComparisonDataSourceProvider"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="CurrentDataSourceConnectionString" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnCurrentDataSourceConnectionStringChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("CurrentDataSourceConnectionString"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="CurrentDataSourceProvider" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnCurrentDataSourceProviderChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("CurrentDataSourceProvider"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="DataSourceComparison" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnDataSourceComparisonChanged(SchemaComparison oldValue, SchemaComparison newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("DataSourceComparison"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="DataSourceComparison" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnDataSourceComparisonSelectedValueChanged(object oldValue, object newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("DataSourceComparisonSelectedValue"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="FindAttributesDiscoveryMethod" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnFindAttributesDiscoveryMethodChanged(CategoryAttributeDiscoveryMethod oldValue, CategoryAttributeDiscoveryMethod newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("FindAttributesDiscoveryMethod"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="FindAttributesMinimumNumber" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnFindAttributesMinimumNumberChanged(int oldValue, int newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("FindAttributesMinimumNumber"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="FindAttributesPublishedItemsOnly" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnFindAttributesPublishedItemsOnlyChanged(bool oldValue, bool newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("FindAttributesPublishedItemsOnly"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="GeneratedEnumCode" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnGeneratedEnumCodeChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("GeneratedEnumCode"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="GeneratedTableCode" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnGeneratedTableCodeChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("GeneratedTableCode"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="SelectedAttributeCategory" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedAttributeCategoryChanged(object oldValue, object newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("SelectedAttributeCategory"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="SelectedEnumCodeGenerator" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedEnumCodeGeneratorChanged(MethodInfo oldValue, MethodInfo newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("SelectedEnumCodeGenerator"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="SelectedQuery" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedQueryChanged(MethodInfo oldValue, MethodInfo newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("SelectedQuery"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="SelectedQueryTestResults" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedQueryTestResultsChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("SelectedQueryTestResults"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="SelectedTableCodeGenerator" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnSelectedTableCodeGeneratorChanged(MethodInfo oldValue, MethodInfo newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("SelectedTableCodeGenerator"));
    }

    /// <summary>
    /// Occurs when the value of the <see cref="StatusText" /> property has
    /// changed.
    /// </summary>
    /// <param name="oldValue">
    /// The value of the property before it changed.
    /// </param>
    /// <param name="newValue">
    /// The current value of the property.
    /// </param>
    protected virtual void OnStatusTextChanged(string oldValue, string newValue)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs("StatusText"));
    }

    /// <summary>
    /// Tests all public properties of the specified type.
    /// </summary>
    /// <param name="entity">
    /// The entity underlying the adapter type.
    /// </param>
    protected void TestEntityProperties(object entity)
    {
      Contract.Requires(entity != null, "The entity cannot be null.");

      Type entityType = entity.GetType();

      // Get all public properties
      PropertyInfo[] properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (PropertyInfo property in properties)
      {
        Contract.Assume(property != null);

        try
        {
          if (property.CanRead && property.GetIndexParameters().Length == 0)
          {
            object value = property.GetValue(entity);
          }
        }
        catch (Exception ex)
        {
          string entityName;

          try
          {
            entityName = entity.ToString();
          }
          catch
          {
            entityName = "[Unknown]";
          }

          Exception innermost = ex;

          while (innermost.InnerException != null)
          {
            innermost = innermost.InnerException;
          }

          string message = ex.Message + (innermost == null ? string.Empty : " (" + innermost.Message + ")");

          throw new InvalidOperationException("Entity " + entityName + " encountered an error accessing property " + property.Name + ": " + message + Environment.NewLine);
        }
      }
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.compareDataSourcesCommand != null);
      Contract.Invariant(this.comparisonDataSourceConnectionString != null);
      Contract.Invariant(this.comparisonDataSourceProvider != null);
      Contract.Invariant(this.currentDataSourceConnectionString != null);
      Contract.Invariant(this.currentDataSourceProvider != null);
      Contract.Invariant(this.dataProviders != null);
      Contract.Invariant(this.enumCodeGenerators != null);
      Contract.Invariant(this.eveQueryMethods != null);
      Contract.Invariant(this.findAttributesForSelectedCategoryCommand != null);
      Contract.Invariant(this.findAttributesMinimumNumber >= 1);
      Contract.Invariant(this.generatedEnumCode != null);
      Contract.Invariant(this.generatedTableCode != null);
      Contract.Invariant(this.generateEnumCodeCommand != null);
      Contract.Invariant(this.generateTableCodeCommand != null);
      Contract.Invariant(this.loadCategoriesCommand != null);
      Contract.Invariant(this.selectedQueryTestResults != null);
      Contract.Invariant(this.statusText != null);
      Contract.Invariant(this.tableCodeGenerators != null);
      Contract.Invariant(this.testDataSourceCommand != null);
      Contract.Invariant(this.testSelectedEntityCommand != null);
    }
  }
}