//-----------------------------------------------------------------------
// <copyright file="GroupEntity.cs" company="Jeremy H. Todd">
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
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// The data entity for the <see cref="Group" /> class.
  /// </summary>
  [Table("invGroups")]
  public class GroupEntity : BaseValueEntity<GroupId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the GroupEntity class.
    /// </summary>
    public GroupEntity() : base() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether items in the group can be manufactured.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group can be manufactured;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("allowManufacture")]
    public bool AllowManufacture { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether items in the group can be reprocessed.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group can be reprocessed;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("allowRecycler")]
    public bool AllowRecycler { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether items in the group can be anchored.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group can be anchored;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("anchorable")]
    public bool Anchorable { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether items in the group are permanently anchored.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group are permanently anchored;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("anchored")]
    public bool Anchored { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the category to which the group belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Category" /> to which the group belongs.
    /// </value>
    [ForeignKey("CategoryId")]
    public virtual CategoryEntity Category { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the category to which the group belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Category" /> to which the group belongs.
    /// </value>
    [Column("categoryID")]
    public CategoryId CategoryId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether multiple instances of items in the group
    /// can be fit on a ship.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if multiple instances can be fit; otherwise
    /// <see langword="false" />.
    /// </value>
    [Column("fittableNonSingleton")]
    public bool FittableNonSingleton { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    [ForeignKey("IconId")]
    public virtual IconEntity Icon { get; set; }
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
    public int? IconId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Hidden navigation property backing for the <see cref="ItemTypes" />
    /// property.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Necessary for so that the publicly accessible wrapper can enforce non-null
    /// ensures with Code Contracts, as well as to wrap the results in a 
    /// friendlier collection class.
    /// </para>
    /// </remarks>
    public virtual ICollection<ItemTypeEntity> ItemTypes { get; set; }
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
    public bool Published { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the base price should be used for items
    /// in the group.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the base price should be used for items in the
    /// group; otherwise <see langword="false" />.
    /// </value>
    [Column("useBasePrice")]
    public bool UseBasePrice { get; set; }
    #endregion
  }
}