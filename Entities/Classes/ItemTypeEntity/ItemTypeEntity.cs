//-----------------------------------------------------------------------
// <copyright file="ItemTypeEntity.cs" company="Jeremy H. Todd">
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

  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// The data entity for the <see cref="ItemType" /> class.
  /// </summary>
  [Table("invTypes")]
  public class ItemTypeEntity : BaseValueEntity<int> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ItemTypeEntity class.
    /// </summary>
    public ItemTypeEntity() : base() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the collection of child market groups under the current market group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyAttributeValueCollection" /> containing the child market
    /// groups.
    /// </value>
    public virtual ICollection<AttributeValueEntity> Attributes { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the base price of the item.
    /// </summary>
    /// 
    /// <value>
    /// The base price of the item.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// This value is so inaccurate as to be almost meaningless in game terms.
    /// </para>
    /// </remarks>
    [Column("basePrice")]
    public decimal BasePrice { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the cargo capacity of the item.
    /// </summary>
    /// 
    /// <value>
    /// The cargo capacity of the item.
    /// </value>
    [Column("capacity")]
    public double Capacity { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the chance of duplication of the item.  This value is not used.
    /// </summary>
    /// 
    /// <value>
    /// The chance of duplication of the item.
    /// </value>
    [Column("chanceOfDuplicating")]
    public double ChanceOfDuplicating { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Group" /> to which the item belongs.
    /// </value>
    [ForeignKey("GroupId")]
    public virtual GroupEntity Group { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Group" /> to which the item belongs.
    /// </value>
    [Column("groupID")]
    public GroupId GroupId { get; set; }
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
    /// Gets the market group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    [ForeignKey("MarketGroupId")]
    public virtual MarketGroupEntity MarketGroup { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the market group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    [Column("marketGroupID")]
    public MarketGroupId? MarketGroupId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the mass of the item.
    /// </summary>
    /// 
    /// <value>
    /// The mass of the item.
    /// </value>
    [Column("mass")]
    public double Mass { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current item is associated with, or <see cref="null" /> if the
    /// item is not associated with any races.
    /// </value>
    [Column("raceID")]
    public RaceId? RaceId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the number of items which constitute one "lot."  A lot is the number of
    /// items produced in a manufacturing job, or that must be stacked in order to
    /// be reprocessed.
    /// </summary>
    /// 
    /// <value>
    /// The number of items which constitute one "lot."
    /// </value>
    [Column("portionSize")]
    public int PortionSize { get; set; }
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
    /// Gets the volume of the item.
    /// </summary>
    /// 
    /// <value>
    /// The volume of the item.
    /// </value>
    [Column("volume")]
    public double Volume { get; set; }
    #endregion

  }
}