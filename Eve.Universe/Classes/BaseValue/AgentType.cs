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

  using Eve.Data;
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
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AgentType(IEveRepository container, AgentTypeEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}