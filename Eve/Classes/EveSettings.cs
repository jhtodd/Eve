//-----------------------------------------------------------------------
// <copyright file="EveSettings.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Globalization;

  using FreeNet;
  using FreeNet.Configuration;

  //******************************************************************************
  /// <summary>
  /// Contains settings specific to the Eve library.
  /// </summary>
  public class EveSettings {

    #region Classes
    //******************************************************************************
    /// <summary>
    /// Contains settings keys and default values.
    /// </summary>
    internal static class SettingKeys {

      // Keys and default values for settings
      internal const string SETTINGKEY_CACHESIZE = "CacheSize";
      internal const int SETTINGDEFAULT_CACHESIZE = 50;

      internal const string SETTINGKEY_CULTURE = "Culture";
      internal const string SETTINGDEFAULT_CULTURE = "default"; // Can be any valid culture identifier or "default" to use the current culture
    }
    #endregion

    #region Instance Fields
    private readonly ConnectionStringManager _connectionStrings;
    private readonly SettingsManager _settings;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveSettings class using the
    /// default settings and connection string managers.
    /// </summary>
    public EveSettings() : this(SettingsManager.Default, ConnectionStringManager.Default) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveSettings class using the
    /// specified settings and connection string managers.
    /// </summary>
    /// 
    /// <param name="settings">
    /// The <see cref="SettingsManager" /> used to read application settings.
    /// </param>
    /// 
    /// <param name="connectionStrings">
    /// The <see cref="ConnectionStringManager" /> used to read connection
    /// strings used by the application.
    /// </param>
    public EveSettings(SettingsManager settings, ConnectionStringManager connectionStrings) {
      Contract.Requires(settings != null, "The settings manager cannot be null.");
      Contract.Requires(connectionStrings != null, "The connection string manager cannot be null.");

      _connectionStrings = connectionStrings;
      _settings = settings;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_connectionStrings != null);
      Contract.Invariant(_settings != null);
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the connection string settings.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ConnectionStringManager" /> object containing the
    /// connection strings used by the application.
    /// </value>
    public ConnectionStringManager ConnectionStrings {
      get {
        Contract.Ensures(Contract.Result<ConnectionStringManager>() != null);
        return _connectionStrings;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the application settings.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="SettingsManager" /> object containing the application
    /// settings.
    /// </value>
    public SettingsManager Settings {
      get {
        Contract.Ensures(Contract.Result<SettingsManager>() != null);
        return _settings;
      }
    }
    #endregion

    #region Settings Properties
    //******************************************************************************
    /// <summary>
    /// Gets the amount of memory (in megabytes) the cache is allowed to use.
    /// </summary>
    /// 
    /// <value>
    /// The amount of memory (in megabytes) the cache is allowed to use.
    /// </value>
    public int CacheSize {
      get {
        Contract.Ensures(Contract.Result<int>() >= 0);

        int cacheSize = Settings.GetValue<int>(SettingKeys.SETTINGKEY_CACHESIZE, SettingKeys.SETTINGDEFAULT_CACHESIZE);
        
        if (cacheSize < 0) {
          cacheSize = 0;
        }

        return cacheSize;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the culture used by the application.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="CultureInfo" /> describing the culture settings used by the
    /// application.
    /// </value>
    public CultureInfo Culture {
      get {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);

        string culture = Settings.GetValue(SettingKeys.SETTINGKEY_CULTURE, SettingKeys.SETTINGDEFAULT_CULTURE);

        if (culture == "default") {
          return CultureInfo.CurrentCulture;
        }

        return new CultureInfo(culture);
      }
    }
    #endregion
  }
}