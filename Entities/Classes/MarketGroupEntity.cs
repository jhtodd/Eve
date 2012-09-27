//-----------------------------------------------------------------------
// <copyright file="MarketGroupEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="MarketGroup" /> class.
  /// </summary>
  [Table("invMarketGroups")]
  public class MarketGroupEntity : BaseValueEntity<MarketGroupId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the MarketGroupEntity class.
    /// </summary>
    public MarketGroupEntity() : base() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the collection of child market groups under the current market group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyMarketGroupCollection" /> containing the child market
    /// groups.
    /// </value>
    [ForeignKey("ParentGroupId")]
    public virtual ICollection<MarketGroupEntity> ChildGroups { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the current group contains items or 
    /// only subgroups.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the group contains items, or 
    /// <see langword="false" /> if the group contains only subgroups.
    /// </value>
    [Column("hasTypes")]
    public bool HasTypes { get; set; }
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
    /// Gets the collection of items that belong to the current market group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyItemTypeCollection" /> containing the items that
    /// belong to the current market group.
    /// </value>
    [ForeignKey("MarketGroupId")]
    public virtual ICollection<ItemTypeEntity> ItemTypes { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the current market group's parent group, if any.
    /// </summary>
    /// 
    /// <value>
    /// The parent market group, or <see langword="null" /> if the
    /// current group doesn't have a parent group.
    /// </value>
    [ForeignKey("ParentGroupId")]
    public virtual MarketGroupEntity ParentGroup { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the current market group's parent group, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the parent market group, or <see langword="null" /> if the
    /// current group doesn't have a parent group.
    /// </value>
    [Column("parentGroupID")]
    public MarketGroupId? ParentGroupId { get; set; }
    #endregion
  }
}