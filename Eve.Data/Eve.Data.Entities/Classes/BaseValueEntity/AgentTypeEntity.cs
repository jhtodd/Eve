//-----------------------------------------------------------------------
// <copyright file="AgentTypeEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="AgentType" /> class.
  /// </summary>
  [Table("agtAgentTypes")]
  public class AgentTypeEntity : BaseValueEntity<AgentTypeId> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AgentTypeEntity class.
    /// </summary>
    public AgentTypeEntity() : base() {
    }
    #endregion
  }
}