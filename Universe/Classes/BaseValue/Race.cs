//-----------------------------------------------------------------------
// <copyright file="Race.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// Contains information about an EVE race.
  /// </summary>>
  [Table("chrRaces")]
  public class Race : BaseValue<RaceId, Race>,
                      IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private int? _iconId;
    private string _shortDescription;

    private Icon _icon;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Race class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public Race() : base(0, DEFAULT_NAME, string.Empty) {
      _shortDescription = string.Empty;
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Race class.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item.
    /// </param>
    /// 
    /// <param name="name">
    /// The name of the item.
    /// </param>
    public Race(RaceId id,
                string name,
                string description,
                int? iconId,
                string shortDescription) : base(id, name, description) {

      Contract.Requires(!string.IsNullOrWhiteSpace(name), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);

      if (shortDescription == null) {
        shortDescription = string.Empty;
      }

      _iconId = iconId;
      _shortDescription = shortDescription;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_shortDescription != null);
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    [ForeignKey("IconId")]
    public virtual Icon Icon {
      get {
        return _icon;
      }
      private set {
        _icon = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    [Column("iconID")]
    public int? IconId {
      get {
        return _iconId;
      }
      private set {
        _iconId = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the short description of the race.
    /// </summary>
    /// 
    /// <value>
    /// A string containing the short description of the race.
    /// </value>
    [Column("shortDescription")]
    public string ShortDescription {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return _shortDescription;
      }
      private set {
        if (value == null) {
          value = string.Empty;
        }

        _shortDescription = value;
      }
    }
    #endregion
  }
}