//-----------------------------------------------------------------------
// <copyright file="General.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Diagnostics.Contracts;

  using FreeNet;
  using FreeNet.Configuration;

  using Eve.Data;

  //******************************************************************************
  public static partial class General {

    #region Static Fields
    private static EveCache _cache = new EveCache();
    private static IEveDataSource _dataSource = EveDataSource.Default;
    private static EveSettings _settings = new EveSettings();
    #endregion
    #region Static Events
    /// <summary>
    /// Notifies EVE-related classes to clean up and free any unneeded resources.
    /// </summary>
    public static event EventHandler Clean;
    #endregion

    #region Static Constructor
    //******************************************************************************
    /// <summary>
    /// Initializes static members of the General class.
    /// </summary>
    static General() {
      DataSource.PrepopulateCache(Cache);
    }
    #endregion
    #region Static Properties
    //******************************************************************************
    /// <summary>
    /// Gets or sets the cache used to store frequently used data.
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="EveCache" /> object used to store frequently used data.
    /// </value>
    public static EveCache Cache {
      get {
        Contract.Ensures(Contract.Result<EveCache>() != null);
        return _cache;
      }
      set {
        Contract.Requires(value != null, Resources.Messages.General_CacheCannotBeNull);

        // Free the previous cache, if any
        if (_cache != null) {
          ((IDisposable) _cache).Dispose();
        }

        // Prepopulate the new cache
        DataSource.PrepopulateCache(value);

        _cache = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the data source used to access the EVE database.
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="IEveDataSource" /> object that provides access to the EVE
    /// database.
    /// </value>
    public static IEveDataSource DataSource {
      get {
        Contract.Ensures(Contract.Result<IEveDataSource>() != null);
        return _dataSource;
      }
      set {
        Contract.Requires(value != null, Resources.Messages.General_DataSourceCannotBeNull);
        _dataSource = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the application settings.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="EveSettings" /> object which provides access to the
    /// application settings.
    /// </value>
    public static EveSettings Settings {
      get {
        Contract.Ensures(Contract.Result<EveSettings>() != null);
        return _settings;
      }
      set {
        Contract.Requires(value != null, Resources.Messages.General_SettingsCannotBeNull);
        _settings = value;
      }
    }
    #endregion
    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Notifies all EVE-related objects to clean up and free any unneeded
    /// resources.
    /// </summary>
    public static void CleanUp() {

      // Perform any garbage collecting that needs to be done.  This will
      // help clean out cached data.
      GC.Collect();

      // Raise the Clean event so that dependent objects know they should
      // put their house in order.
      EventHandler cleanHandler = Clean;
      if (cleanHandler != null) {
        cleanHandler(null, EventArgs.Empty); 
      }
    }
    #endregion
  }
}