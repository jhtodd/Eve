//-----------------------------------------------------------------------
// <copyright file="Division.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about the type of an agent.
  /// </summary>
  public class Division : BaseValue<DivisionId, DivisionId, DivisionEntity, Division> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Division class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Division(DivisionEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the title of the leader of the division.
    /// </summary>
    /// 
    /// <value>
    /// The title of the leader of the division.
    /// </value>
    public string LeaderType {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.LeaderType ?? string.Empty;
      }
    }
    #endregion
  }
}