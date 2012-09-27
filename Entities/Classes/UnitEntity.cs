//-----------------------------------------------------------------------
// <copyright file="UnitEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="Unit" /> class.
  /// </summary>
  [Table("eveUnits")]
  public class UnitEntity : BaseValueEntity<UnitId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the UnitEntity class.
    /// </summary>
    public UnitEntity() : base() {
    }
    #endregion

    #region Public Properties
    //******************************************************************************
    /// <summary>Gets or sets the human-readable display name of the item.</summary>
    /// <value>The human-readable display name of the item.</value>
    [Column("displayName")]
    public string DisplayName { get; set; }
    #endregion
  }
}