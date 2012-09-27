//-----------------------------------------------------------------------
// <copyright file="AttributeValueEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Entities {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// The data entity for the <see cref="AttributeValue" /> class.
  /// </summary>
  [Table("dgmTypeAttributes")]
  public class AttributeValueEntity : ImmutableEntity {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeTypeEntity class.
    /// </summary>
    public AttributeValueEntity() : base() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute the current value applies to.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the attribute the current value applies to.
    /// </value>
    [Column("attributeID", Order = 2)]
    [Key()]
    public AttributeId AttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute type to which the value applies.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeTypeEntity" /> to which the value applies.
    /// </value>
    [ForeignKey("AttributeId")]
    public virtual AttributeTypeEntity AttributeType { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the item type the value describes.
    /// </summary>
    /// 
    /// <value>
    /// The item type the value describes.
    /// </value>
    [ForeignKey("ItemTypeId")]
    public virtual ItemTypeEntity ItemType { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item type the value describes.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the item type the value describes.
    /// </value>
    [Column("typeID", Order = 1)]
    [Key()]
    public int ItemTypeId { get; set; }
    //******************************************************************************
    [Column("valueFloat")]
    public double? ValueFloat { get; set; }
    //******************************************************************************
    [Column("valueInt")]
    public int? ValueInt { get; set; }
    #endregion
  }
}