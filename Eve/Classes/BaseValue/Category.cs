//-----------------------------------------------------------------------
// <copyright file="Category.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
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
  /// Contains information about a category to which an EVE item belongs.
  /// </summary>>
  [Table("invCategories")]
  public class Category : BaseValue<CategoryId, Category>,
                          IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private int? _iconId;
    private bool _published;

    private Icon _icon;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Category class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public Category() : base(0, DEFAULT_NAME, string.Empty) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Category class.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item.
    /// </param>
    /// 
    /// <param name="name">
    /// The name of the item.
    /// </param>
    /// 
    /// <param name="description">
    /// The description of the item.
    /// </param>
    /// 
    /// <param name="iconId">
    /// The ID of the icon associated with the item, if any.
    /// </param>
    /// 
    /// <param name="published">
    /// Indicates whether the item is marked as published for 
    /// public consumption.
    /// </param>
    public Category(CategoryId id, 
                    string name,
                    string description,
                    int? iconId,
                    bool published) : base(id, name, description) {
      Contract.Requires(!string.IsNullOrWhiteSpace(name), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);

      _iconId = iconId;
      _published = published;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
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
    /// Gets a value indicating whether the item is marked as published for
    /// public consumption.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the item is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("published")]
    public bool Published {
      get {
        return _published;
      }
      private set {
        _published = value;
      }
    }
    #endregion
  }
}