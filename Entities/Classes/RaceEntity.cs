//-----------------------------------------------------------------------
// <copyright file="RaceEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="Race" /> class.
  /// </summary>
  [Table("chrRaces")]
  public class RaceEntity : BaseValueEntity<RaceId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the RaceEntity class.
    /// </summary>
    public RaceEntity() : base() {
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
    /// Gets the short description of the race.
    /// </summary>
    /// 
    /// <value>
    /// A string containing the short description of the race.
    /// </value>
    [Column("shortDescription")]
    public string ShortDescription { get; set; }
    #endregion
  }
}