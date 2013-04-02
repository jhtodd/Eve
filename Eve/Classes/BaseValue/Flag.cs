//-----------------------------------------------------------------------
// <copyright file="Flag.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about a flag associated with an EVE item.
  /// </summary>
  public class Flag : BaseValue<FlagId, FlagId, FlagEntity, Flag> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Flag class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Flag(FlagEntity entity) : base(entity) {
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
    /// Gets the text associated with the flag.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="string" /> that provides the text associated with the flag.
    /// </value>
    public string FlagText {
      get {
        Contract.Ensures(Contract.Result<string>() != null);
        return Description;
      }
    }
    #endregion
  }
}