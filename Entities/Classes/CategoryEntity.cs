//-----------------------------------------------------------------------
// <copyright file="CategoryEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Entities {
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
  /// The data entity for the <see cref="Category" /> class.
  /// </summary>
  [Table("invCategories")]
  public class CategoryEntity : BaseValueEntity<CategoryId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the CategoryEntity class.
    /// </summary>
    public CategoryEntity() : base() {
    }
    #endregion

    #region Public Properties
    //******************************************************************************
    /// <summary>Gets or sets the icon associated with the item.</summary>
    /// <value>The ID of the icon associated with the item.</value>
    [ForeignKey("IconId")]
    public IconEntity Icon { get; set; }
    //******************************************************************************
    /// <summary>Gets or sets the ID of the icon associated with the item.</summary>
    /// <value>The ID of the icon associated with the item.</value>
    [Column("iconID")]
    public int? IconId { get; set; }
    //******************************************************************************
    /// <summary>Gets or sets a value indicating whether the item is marked as published.</summary>
    /// <value><see langword="true" /> if the item is marked as published; otherwise <see langword="false" />.</value>
    [Column("published")]
    public bool Published { get; set; }
    #endregion
  }
}