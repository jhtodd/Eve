//-----------------------------------------------------------------------
// <copyright file="GroupEntity.cs" company="Jeremy H. Todd">
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
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("allowManufacture")]
    public bool AllowManufacture { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("allowRecycler")]
    public bool AllowRecycler { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("anchorable")]
    public bool Anchorable { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("anchored")]
    public bool Anchored { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("CategoryId")]
    public virtual CategoryEntity Category { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("categoryID")]
    public CategoryId CategoryId { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("fittableNonSingleton")]
    public bool FittableNonSingleton { get; set; }
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
    public virtual ICollection<EveTypeEntity> Types { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("useBasePrice")]
    public bool UseBasePrice { get; set; }
    #endregion
  }
}