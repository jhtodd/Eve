//-----------------------------------------------------------------------
// <copyright file="AgentType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
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
  /// Contains information about the type of an agent.
  /// </summary>>
  [Table("agtAgentTypes")]
  public sealed class AgentType : BaseValue<AgentTypeId, AgentType> {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AgentType class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public AgentType() : base(0, DEFAULT_NAME, string.Empty) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AgentType class.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item.
    /// </param>
    /// 
    /// <param name="name">
    /// The name of the item.
    /// </param>
    public AgentType(AgentTypeId id, string name) : base(id, name, "") {
      Contract.Requires(!string.IsNullOrWhiteSpace(name), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
  }
}