//-----------------------------------------------------------------------
// <copyright file="AttributeCategory.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about the category of an attribute.
  /// </summary>
  public sealed class AttributeCategory : BaseValue<AttributeCategoryId, AttributeCategoryId, AttributeCategoryEntity, AttributeCategory> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the AttributeCategory class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public AttributeCategory(AttributeCategoryEntity entity) : base(entity) {
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
  }
}