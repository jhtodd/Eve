//-----------------------------------------------------------------------
// <copyright file="EveSettings.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;
  using System.Globalization;

  using FreeNet.Configuration;

  /// <summary>
  /// Contains settings specific to the Eve library.
  /// </summary>
  public class EveSettings
  {
    private readonly ConnectionStringManager connectionStrings;
    private readonly SettingsManager settings;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveSettings class using the
    /// default settings and connection string managers.
    /// </summary>
    public EveSettings() : this(SettingsManager.Default, ConnectionStringManager.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the EveSettings class using the
    /// specified settings and connection string managers.
    /// </summary>
    /// <param name="settings">
    /// The <see cref="SettingsManager" /> used to read application settings.
    /// </param>
    /// <param name="connectionStrings">
    /// The <see cref="ConnectionStringManager" /> used to read connection
    /// strings used by the application.
    /// </param>
    public EveSettings(SettingsManager settings, ConnectionStringManager connectionStrings)
    {
      Contract.Requires(settings != null, "The settings manager cannot be null.");
      Contract.Requires(connectionStrings != null, "The connection string manager cannot be null.");

      this.connectionStrings = connectionStrings;
      this.settings = settings;
    }

    /* Properties */

    /// <summary>
    /// Gets the amount of memory (in megabytes) the cache is allowed to use.
    /// </summary>
    /// <value>
    /// The amount of memory (in megabytes) the cache is allowed to use.
    /// </value>
    public int CacheSize
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        int cacheSize = this.Settings.GetValue<int>(SettingKeys.CacheSizeKey, SettingKeys.CacheSizeDefaultValue);

        if (cacheSize < 0)
        {
          cacheSize = 0;
        }

        return cacheSize;
      }
    }

    /// <summary>
    /// Gets the connection string settings.
    /// </summary>
    /// <value>
    /// A <see cref="ConnectionStringManager" /> object containing the
    /// connection strings used by the application.
    /// </value>
    public ConnectionStringManager ConnectionStrings
    {
      get
      {
        Contract.Ensures(Contract.Result<ConnectionStringManager>() != null);
        return this.connectionStrings;
      }
    }

    /// <summary>
    /// Gets the culture used by the application.
    /// </summary>
    /// <value>
    /// A <see cref="CultureInfo" /> describing the culture settings used by the
    /// application.
    /// </value>
    public CultureInfo Culture
    {
      get
      {
        Contract.Ensures(Contract.Result<CultureInfo>() != null);

        string culture = this.Settings.GetValue(SettingKeys.CultureKey, SettingKeys.CultureDefaultValue);

        if (culture == "default")
        {
          return CultureInfo.CurrentCulture;
        }

        return new CultureInfo(culture);
      }
    }

    /// <summary>
    /// Gets the application settings.
    /// </summary>
    /// <value>
    /// A <see cref="SettingsManager" /> object containing the application
    /// settings.
    /// </value>
    public SettingsManager Settings
    {
      get
      {
        Contract.Ensures(Contract.Result<SettingsManager>() != null);
        return this.settings;
      }
    }

    /* Methods */

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.connectionStrings != null);
      Contract.Invariant(this.settings != null);
    }

    /* Classes */

    /// <summary>
    /// Contains settings keys and default values.
    /// </summary>
    internal static class SettingKeys
    {
      // Keys and default values for settings

      /// <summary>The key for the CacheSize setting.</summary>
      internal const string CacheSizeKey = "CacheSize";

      /// <summary>The default value for the CacheSize setting.</summary>
      internal const int CacheSizeDefaultValue = 50;

      /// <summary>The key for the Culture setting.</summary>
      internal const string CultureKey = "Culture";

      /// <summary>The default value for the Culture setting.</summary>
      internal const string CultureDefaultValue = "default"; // Can be any valid culture identifier or "default" to use the current culture
    }
  }
}