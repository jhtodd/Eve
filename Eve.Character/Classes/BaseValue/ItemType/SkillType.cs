//-----------------------------------------------------------------------
// <copyright file="SkillType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  using Eve.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// Contains information about a group to which an EVE item belongs.
  /// </summary>
  public class SkillType : ItemType {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ItemType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal SkillType(ItemTypeEntity entity) : base(entity) {
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
    /// Returns a value indicating whether the skill can be trained on trial
    /// accounts.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the skill cannot be trained on trial accounts;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool CannotBeTrainedOnTrialAccounts {
      get {
        return Attributes.GetValue<bool>(AttributeId.CanNotBeTrainedOnTrial);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new SkillId Id {
      get {
        return (SkillId) base.Id.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the training time multiplier for the skill.
    /// </summary>
    /// 
    /// <value>
    /// The training time multiplier for the skill.
    /// </value>
    public int Rank {
      get {
        Contract.Ensures(Contract.Result<int>() >= 1);

        int result = Attributes.GetValue<int>(AttributeId.SkillTimeConstant);
        Contract.Assume(result >= 1);

        return result;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of item types.
  /// </summary>
  public class ReadOnlyItemTypeCollection : ReadOnlyCollection<ItemType> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyMarketGroupCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyItemTypeCollection(IEnumerable<ItemType> contents) : base() {
      if (contents != null) {
        foreach (ItemType item in contents) {
          Items.AddWithoutCallback(item);
        }
      }
    }
    #endregion
  }
}