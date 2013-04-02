//-----------------------------------------------------------------------
// <copyright file="BaseValueEntity.cs" company="Jeremy H. Todd">
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
  /// The base class for data entities for basic EVE values.
  /// </summary>
  /// 
  /// <typeparam name="TId">
  /// The type of the ID value.
  /// </typeparam>
  public class BaseValueEntity<TId> : EveEntityBase {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the BaseValueEntity class.
    /// </summary>
    public BaseValueEntity() : base() {
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
    public string Description { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public TId Id { get; set; }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// 
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public string Name { get; set; }
    #endregion
  }
}