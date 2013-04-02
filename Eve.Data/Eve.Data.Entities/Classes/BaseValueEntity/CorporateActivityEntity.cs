//-----------------------------------------------------------------------
// <copyright file="CorporateActivityEntity.cs" company="Jeremy H. Todd">
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

  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// The data entity for the <see cref="CorporateActivity" /> class.
  /// </summary>
  [Table("crpActivities")]
  public class CorporateActivityEntity : BaseValueEntity<CorporateActivityId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the CorporateActivityEntity class.
    /// </summary>
    public CorporateActivityEntity() : base() {
    }
    #endregion
  }
}