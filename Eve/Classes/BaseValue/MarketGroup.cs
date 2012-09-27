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

  using Eve.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about a market group to which an EVE item belongs.
  /// </summary>
  public class MarketGroup : BaseValue<MarketGroupId, MarketGroupId, MarketGroupEntity, MarketGroup>,
                             IHasIcon {

    #region Instance Fields
    private ReadOnlyMarketGroupCollection _childGroups;
    private Icon _icon;
    private ReadOnlyItemTypeCollection _itemTypes;
    private MarketGroup _parentGroup;
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
    public MarketGroup(MarketGroupEntity entity) : base(entity) {
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
          if (Entity.ChildGroups != null) {
            _childGroups = new ReadOnlyMarketGroupCollection(Entity.ChildGroups.Select(x => new MarketGroup(x)).OrderBy(x => x));
          } else {
            _childGroups = new ReadOnlyMarketGroupCollection(null);
          }
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
    /// Gets the collection of items that belong to the current market group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyItemTypeCollection" /> containing the items that
    /// belong to the current market group.
    /// </value>
    public ReadOnlyItemTypeCollection ItemTypes {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyItemTypeCollection>() != null);

        if (_itemTypes == null) {
          if (Entity.ItemTypes != null) {

            // Load item types from the cache if available
            _itemTypes = new ReadOnlyItemTypeCollection(Entity.ItemTypes.Select(x => {
              return Eve.General.Cache.GetOrAdd<ItemType>(x.Id, () => ItemType.Create(x));
            }).OrderBy(x => x));

          } else {
            _itemTypes = new ReadOnlyItemTypeCollection(null);
          }
        }

        return _itemTypes;
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