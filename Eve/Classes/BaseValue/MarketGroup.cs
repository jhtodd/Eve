//-----------------------------------------------------------------------
// <copyright file="MarketGroup.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about a market group to which an EVE item belongs.
  /// </summary>
  public class MarketGroup : BaseValue<MarketGroupId, MarketGroupId, MarketGroupEntity, MarketGroup>,
                             IHasIcon {

    #region Instance Fields
    private ReadOnlyMarketGroupCollection _childGroups;
    private Icon _icon;
    private MarketGroup _parentGroup;
    private ReadOnlyTypeCollection _types;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the MarketGroup class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal MarketGroup(MarketGroupEntity entity) : base(entity) {
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
    /// Gets the collection of child market groups under the current market group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyMarketGroupCollection" /> containing the child market
    /// groups.
    /// </value>
    public ReadOnlyMarketGroupCollection ChildGroups {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyMarketGroupCollection>() != null);

        if (_childGroups == null) {
          _childGroups = new ReadOnlyMarketGroupCollection(Eve.General.DataSource.GetMarketGroups(x => x.ParentGroupId == this.Id).OrderBy(x => x));
        }

        return _childGroups;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the current group contains items or 
    /// only subgroups.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the group contains items, or 
    /// <see langword="false" /> if the group contains only subgroups.
    /// </value>
    public bool HasTypes {
      get {
        return Entity.HasTypes;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon {
      get {
        if (_icon == null) {
          if (IconId != null) {

            // Load the cached version if available
            _icon = Eve.General.Cache.GetOrAdd<Icon>(IconId, () => {
              IconEntity iconEntity = Entity.Icon;
              Contract.Assume(iconEntity != null);

              return new Icon(iconEntity);
            });
          }
        }

        return _icon;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId {
      get {
        return Entity.IconId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the current market group's parent group, if any.
    /// </summary>
    /// 
    /// <value>
    /// The parent market group, or <see langword="null" /> if the
    /// current group doesn't have a parent group.
    /// </value>
    public virtual MarketGroup ParentGroup {
      get {
        if (_parentGroup == null) {
          if (ParentGroupId != null) {

            // Load the cached version if available
            _parentGroup = Eve.General.Cache.GetOrAdd<MarketGroup>(ParentGroupId, () => {
              MarketGroupEntity parentGroupEntity = Entity.ParentGroup;
              Contract.Assume(parentGroupEntity != null);

              return new MarketGroup(parentGroupEntity);
            });
          }
        }

        return _parentGroup;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the current market group's parent group, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the parent market group, or <see langword="null" /> if the
    /// current group doesn't have a parent group.
    /// </value>
    public MarketGroupId? ParentGroupId {
      get {
        return Entity.ParentGroupId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of items that belong to the current market group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyTypeCollection" /> containing the items that
    /// belong to the current market group.
    /// </value>
    public ReadOnlyTypeCollection Types {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyTypeCollection>() != null);

        if (_types == null) {
          _types = new ReadOnlyTypeCollection(Eve.General.DataSource.GetEveTypes(x => x.MarketGroupId == this.Id).OrderBy(x => x));
        }

        return _types;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Determines whether the current group is a child group of (or the same 
    /// group as) the group with the specified ID.
    /// </summary>
    /// 
    /// <param name="groupId">
    /// The ID of the market group that may be a parent.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if the current group is a child of (or the same
    /// group as) the group identified by <paramref name="groupId" />.
    /// </returns>
    public bool IsChildOf(MarketGroupId groupId) {
      if (Id == groupId) {
        return true;
      }

      if (ParentGroupId == null) {
        return false;
      }

      if (ParentGroupId == groupId) {
        return true;
      }

      // Recurse upward
      Contract.Assume(ParentGroup != null); // Because we know ParentGroupId is not null
      return ParentGroup.IsChildOf(groupId);
    }
    #endregion

    //******************************************************************************
    /// <summary>
    /// A read-only collection of market groups.
    /// </summary>
    public class ReadOnlyMarketGroupCollection : ReadOnlyKeyedCollection<MarketGroupId, MarketGroup> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the ReadOnlyMarketGroupCollection class.
      /// </summary>
      /// 
      /// <param name="contents">
      /// The contents of the collection.
      /// </param>
      public ReadOnlyMarketGroupCollection(IEnumerable<MarketGroup> contents) : base() {
        if (contents != null) {
          foreach (MarketGroup group in contents) {
            Items.AddWithoutCallback(group);
          }
        }
      }
      #endregion
    }
  }
}