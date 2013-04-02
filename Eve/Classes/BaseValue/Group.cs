//-----------------------------------------------------------------------
// <copyright file="Group.cs" company="Jeremy H. Todd">
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
  using FreeNet.Data.Entity;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about a group to which an EVE item belongs.
  /// </summary>
  public class Group : BaseValue<GroupId, GroupId, GroupEntity, Group>,
                       IHasIcon {

    #region Instance Fields
    private Category _category;
    private Icon _icon;
    private ReadOnlyTypeCollection _types;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Group class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Group(GroupEntity entity) : base(entity) {
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
    /// Gets a value indicating whether items in the group can be manufactured.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group can be manufactured;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool AllowManufacture {
      get {
        return Entity.AllowManufacture;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether items in the group can be reprocessed.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group can be reprocessed;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool AllowRecycler {
      get {
        return Entity.AllowRecycler;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether items in the group can be anchored.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group can be anchored;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Anchorable {
      get {
        return Entity.Anchorable;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether items in the group are permanently anchored.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if items in the group are permanently anchored;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Anchored {
      get {
        return Entity.Anchored;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the category to which the group belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Category" /> to which the group belongs.
    /// </value>
    public Category Category {
      get {
        Contract.Ensures(Contract.Result<Category>() != null);

        if (_category == null) {

          // Load the cached version if available
          _category = Eve.General.Cache.GetOrAdd<Category>(CategoryId, () => {
            CategoryEntity categoryEntity = Entity.Category;
            Contract.Assume(categoryEntity != null);

            return new Category(categoryEntity);
          });
        }

        return _category;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the category to which the group belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Category" /> to which the group belongs.
    /// </value>
    public CategoryId CategoryId {
      get {
        return Entity.CategoryId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether multiple instances of items in the group
    /// can be fit on a ship.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if multiple instances can be fit; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool FittableNonSingleton {
      get {
        return Entity.FittableNonSingleton;
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
    /// Gets a value indicating whether the item is marked as published for
    /// public consumption.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the item is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published {
      get {
        return Entity.Published;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of items that belong to the current group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyTypeCollection" /> containing the items that
    /// belong to the current group.
    /// </value>
    public ReadOnlyTypeCollection Types {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyTypeCollection>() != null);

        if (_types == null) {
          _types = new ReadOnlyTypeCollection(Eve.General.DataSource.GetEveTypes(x => x.GroupId == this.Id).OrderBy(x => x));
        }

        return _types;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the base price should be used for items
    /// in the group.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the base price should be used for items in the
    /// group; otherwise <see langword="false" />.
    /// </value>
    public bool UseBasePrice {
      get {
        return Entity.UseBasePrice;
      }
    }
    #endregion
  }
}