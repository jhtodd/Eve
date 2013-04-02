﻿//-----------------------------------------------------------------------
// <copyright file="EveEntityBase.cs" company="Jeremy H. Todd">
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

  using Eve.Meta;

  //******************************************************************************
  /// <summary>
  /// The base class for all EVE game-related entities.
  /// </summary>
  public class EveEntityBase : ImmutableEntity {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveEntityBase class.
    /// </summary>
    public EveEntityBase() : base() {
    }
    #endregion
  }
}