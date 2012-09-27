//-----------------------------------------------------------------------
// <copyright file="AttributeTypeEntity.cs" company="Jeremy H. Todd">
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
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// The data entity for the <see cref="AttributeType" /> class.
  /// </summary>
  [Table("dgmAttributeTypes")]
  public class AttributeTypeEntity : BaseValueEntity<AttributeId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeTypeEntity class.
    /// </summary>
    public AttributeTypeEntity() : base() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the category to which the attribute belongs.
    /// </summary>
    /// 
    /// <value>
    /// The category to which the attribute belongs.
    /// </value>
    [ForeignKey("CategoryId")]
    public virtual AttributeCategoryEntity Category { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the category to which the attribute belongs.
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="AttributeCategoryId" /> value specifying the category to
    /// which the attribute belongs.
    /// </value>
    [Column("categoryID")]
    public AttributeCategoryId? CategoryId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the default value of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The default value of the attribute.
    /// </value>
    [Column("defaultValue")]
    public double DefaultValue { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the display name of the attribute.
    /// </summary>
    /// 
    /// <value>
    /// The human-readable display name of the attribute.
    /// </value>
    [Column("displayName")]
    public string DisplayName { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether a high value for the attribute is considered
    /// good.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if a high value is good; otherwise
    /// <see langword="false" />.
    /// </value>
    [Column("highIsGood")]
    public bool HighIsGood { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="IconEntity" /> associated with the attribute, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    [ForeignKey("IconId")]
    public virtual IconEntity Icon { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the icon associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the attribute, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    [Column("iconID")]
    public int? IconId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the attribute is marked as published for
    /// public consumption.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the attribute is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("published")]
    public bool Published { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the attribute can be stacked without
    /// penalty.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the attribute can be stacked with penalty;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("stackable")]
    public bool Stackable { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the unit associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="UnitEntity" /> associated with the attribute, or
    /// <see langword="null" /> if no such unit exists.
    /// </value>
    [ForeignKey("UnitId")]
    public virtual UnitEntity Unit { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the unit associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the unit associated with the attribute, or
    /// <see langword="null" /> if no such unit exists.
    /// </value>
    [Column("unitID")]
    public UnitId? UnitId { get; set; }
    #endregion
  }
}