//-----------------------------------------------------------------------
// <copyright file="EffectTypeEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities {
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
  /// The data entity for the <see cref="EffectType" /> class.
  /// </summary>
  [Table("dgmEffects")]
  public class EffectTypeEntity : BaseValueEntity<short> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EffectTypeEntity class.
    /// </summary>
    public EffectTypeEntity() : base() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("disallowAutoRepeat")]
    public bool DisallowAutoRepeat { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("DischargeAttributeId")]
    public virtual AttributeTypeEntity DischargeAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("dischargeAttributeID")]
    public AttributeId? DischargeAttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("displayName")]
    public string DisplayName { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("distribution")]
    public byte? Distribution { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("DurationAttributeId")]
    public virtual AttributeTypeEntity DurationAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("durationAttributeID")]
    public AttributeId? DurationAttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("effectCategory")]
    public short EffectCategoryId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("electronicChance")]
    public bool ElectronicChance { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FalloffAttributeId")]
    public virtual AttributeTypeEntity FalloffAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("falloffAttributeID")]
    public AttributeId? FalloffAttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("FittingUsageChanceAttributeId")]
    public virtual AttributeTypeEntity FittingUsageChanceAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("fittingUsageChanceAttributeID")]
    public AttributeId? FittingUsageChanceAttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("guid")]
    public string Guid { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("IconId")]
    public virtual IconEntity Icon { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("iconID")]
    public int? IconId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("isAssistance")]
    public bool IsAssistance { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("isOffensive")]
    public bool IsOffensive { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("isWarpSafe")]
    public bool IsWarpSafe { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("NpcActivationChanceAttributeId")]
    public virtual AttributeTypeEntity NpcActivationChanceAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("npcActivationChanceAttributeID")]
    public AttributeId? NpcActivationChanceAttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("NpcUsageChanceAttributeId")]
    public virtual AttributeTypeEntity NpcUsageChanceAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("npcUsageChanceAttributeID")]
    public AttributeId? NpcUsageChanceAttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("postExpression")]
    public int PostExpression { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("preExpression")]
    public int PreExpression { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("propulsionChance")]
    public bool PropulsionChance { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("published")]
    public bool Published { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("RangeAttributeId")]
    public virtual AttributeTypeEntity RangeAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("rangeAttributeID")]
    public AttributeId? RangeAttributeId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("rangeChance")]
    public bool RangeChance { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("sfxName")]
    public string SfxName { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("TrackingSpeedAttributeId")]
    public virtual AttributeTypeEntity TrackingSpeedAttribute { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("trackingSpeedAttributeID")]
    public AttributeId? TrackingSpeedAttributeId { get; set; }
    #endregion
  }
}