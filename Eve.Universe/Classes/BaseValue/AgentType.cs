//-----------------------------------------------------------------------
// <copyright file="AgentType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;

  /// <summary>
  /// Contains information about the type of an agent.
  /// </summary>
  public sealed class AgentType : BaseValue<AgentTypeId, AgentTypeId, AgentTypeEntity, AgentType>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AgentType class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AgentType(AgentTypeEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
  }
}