//-----------------------------------------------------------------------
// <copyright file="ItemType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  using Eve.Character;
  using Eve.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// The base class for EVE types.
  /// </summary>
  public abstract class ItemType : BaseValue<ItemTypeId, int, ItemTypeEntity, ItemType>,
                                   IHasIcon {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Creates an appropriate item type for the specified entity.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity.
    /// </param>
    /// 
    /// <returns>
    /// An <see cref="ItemType" /> of the appropriate derived type, based on the
    /// contents of <paramref name="entity" />.
    /// </returns>
    public static ItemType Create(ItemTypeEntity entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
      Contract.Ensures(Contract.Result<ItemType>() != null);

      // Use the item's group and category to determine the correct derived type
      Group group = Eve.General.DataSource.GetGroupById(entity.GroupId);

      switch (group.CategoryId) {

        // All items under category Skill map to SkillType
        case CategoryId.Skill:
          return new SkillType(entity);
      }

      return new GenericItemType(entity);
    }
    #endregion

    #region Instance Fields
    private ReadOnlyAttributeValueCollection _attributes;
    private Group _group;
    private Icon _icon;
    private MarketGroup _marketGroup;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ItemType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public ItemType(ItemTypeEntity entity) : base(entity) {
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
    /// A <see cref="ReadOnlyAttributeValueCollection" /> containing the child market
    /// groups.
    /// </value>
    public ReadOnlyAttributeValueCollection Attributes {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyAttributeValueCollection>() != null);

        if (_attributes == null) {
          if (Entity.Attributes != null) {

            // No need to cache individual attributes since the whole ItemType is
            // already being cached, and they have no usefulness outside the
            // current ItemType
            _attributes = new ReadOnlyAttributeValueCollection(Entity.Attributes.Select(x => new AttributeValue(x)).OrderBy(x => x));
          } else {
            _attributes = new ReadOnlyAttributeValueCollection(null);
          }
        }

        return _attributes;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the base price of the item.
    /// </summary>
    /// 
    /// <value>
    /// The base price of the item.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// This value is so inaccurate as to be almost meaningless in game terms.
    /// </para>
    /// </remarks>
    public decimal BasePrice {
      get {
        return Entity.BasePrice;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the cargo capacity of the item.
    /// </summary>
    /// 
    /// <value>
    /// The cargo capacity of the item.
    /// </value>
    public double Capacity {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Capacity;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the category to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Category" /> to which the item belongs.
    /// </value>
    public Category Category {
      get {
        Contract.Ensures(Contract.Result<Category>() != null);
        return Group.Category;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the category to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Category" /> to which the item belongs.
    /// </value>
    public CategoryId CategoryId {
      get {
        return Group.CategoryId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the chance of duplication of the item.  This value is not used.
    /// </summary>
    /// 
    /// <value>
    /// The chance of duplication of the item.
    /// </value>
    public double ChanceOfDuplicating {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.ChanceOfDuplicating;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Group" /> to which the item belongs.
    /// </value>
    public Group Group {
      get {
        Contract.Ensures(Contract.Result<Group>() != null);

        if (_group == null) {

          // Load the cached version if available
          _group = Eve.General.Cache.GetOrAdd<Group>(GroupId, () => {
            GroupEntity groupEntity = Entity.Group;
            Contract.Assume(groupEntity != null);

            return new Group(groupEntity);
          });
        }

        return _group;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Group" /> to which the item belongs.
    /// </value>
    public GroupId GroupId {
      get {
        return Entity.GroupId;
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
    /// Gets the market group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    public virtual MarketGroup MarketGroup {
      get {
        if (_marketGroup == null) {
          if (MarketGroupId != null) {

            // Load the cached version if available
            _marketGroup = Eve.General.Cache.GetOrAdd<MarketGroup>(MarketGroupId, () => {
              MarketGroupEntity marketGroupEntity = Entity.MarketGroup;
              Contract.Assume(marketGroupEntity != null);

              return new MarketGroup(marketGroupEntity);
            });
          }
        }

        return _marketGroup;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the market group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    public MarketGroupId? MarketGroupId {
      get {
        return Entity.MarketGroupId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the mass of the item.
    /// </summary>
    /// 
    /// <value>
    /// The mass of the item.
    /// </value>
    public double Mass {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Mass;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current item is associated with, or <see cref="null" /> if the
    /// item is not associated with any races.
    /// </value>
    public RaceId? RaceId {
      get {
        return Entity.RaceId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of items which constitute one "lot."  A lot is the number of
    /// items produced in a manufacturing job, or that must be stacked in order to
    /// be reprocessed.
    /// </summary>
    /// 
    /// <value>
    /// The number of items which constitute one "lot."
    /// </value>
    public int PortionSize {
      get {
        return Entity.PortionSize;
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
    /// Gets the volume of the item.
    /// </summary>
    /// 
    /// <value>
    /// The volume of the item.
    /// </value>
    public double Volume {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Volume;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

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