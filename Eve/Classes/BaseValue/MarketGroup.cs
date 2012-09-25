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
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// Contains information about a market group to which an EVE item belongs.
  /// </summary>>
  [Table("invMarketGroups")]
  public class MarketGroup : BaseValue<MarketGroupId, MarketGroup>,
                             IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private bool _hasTypes;
    private int? _iconId;
    private MarketGroupId? _parentGroupId;

    private ReadOnlyMarketGroupCollection _childGroups;
    private Icon _icon;
    private ICollection<MarketGroup> _innerChildGroups;
    private ICollection<ItemType> _innerItemTypes;
    private ReadOnlyItemTypeCollection _itemTypes;
    private MarketGroup _parentGroup;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the MarketGroup class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public MarketGroup() : base(0, DEFAULT_NAME, string.Empty) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the MarketGroup class.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item.
    /// </param>
    /// 
    /// <param name="name">
    /// The name of the item.
    /// </param>
    /// 
    /// <param name="description">
    /// The description of the item.
    /// </param>
    /// 
    /// <param name="hasTypes">
    /// Indicates whether the group contains items or only subgroups.
    /// </param>
    /// 
    /// <param name="iconId">
    /// The ID of the icon associated with the item, if any.
    /// </param>
    /// 
    /// <param name="parentGroupId">
    /// The ID of the parent market group, if any.
    /// </param>
    public MarketGroup(MarketGroupId id, 
                       string name,
                       string description,
                       bool hasTypes,
                       int? iconId,
                       MarketGroupId? parentGroupId) : base(id, name, description) {
      Contract.Requires(!string.IsNullOrWhiteSpace(name), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);

      _hasTypes = hasTypes;
      _iconId = iconId;
      _parentGroupId = parentGroupId;
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

        // This returns a wrapper collection around the actual mapped collection.
        // This allows us to order the results as well as provide key-based
        // retrieval of the contents.  Initialization takes slightly longer than
        // it otherwise would, but memory is not an issue, since the same 
        // references are being copied to the new collection (i.e. 4 bytes
        // per element).

        // If the wrapper collection is null, it either hasn't been created yet,
        // or has been set after the mapped collection was modified, so we create
        // it on the fly.  Concurrent access could cause this to be generated
        // several times on different threads, but this will be (very!) rare,
        // and harmless apart from the cost of creating the wrapper.
        if (_childGroups == null) {
          if (InnerChildGroups != null) {
            _childGroups = new ReadOnlyMarketGroupCollection(InnerChildGroups.OrderBy(x => x.Name));
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
    [Column("hasTypes")]
    public bool HasTypes {
      get {
        return _hasTypes;
      }
      private set {
        _hasTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    [ForeignKey("IconId")]
    public virtual Icon Icon {
      get {
        return _icon;
      }
      private set {
        _icon = value;
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
    [Column("iconID")]
    public int? IconId {
      get {
        return _iconId;
      }
      private set {
        _iconId = value;
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

        // This returns a wrapper collection around the actual mapped collection.
        // This allows us to order the results.  Initialization takes slightly
        // longer than it otherwise would, but memory is not an issue, since the
        // same references are being copied to the new collection (i.e. 4 bytes
        // per element).

        // If the wrapper collection is null, it either hasn't been created yet,
        // or has been set after the mapped collection was modified, so we create
        // it on the fly.  Concurrent access could cause this to be generated
        // several times on different threads, but this will be (very!) rare,
        // and harmless apart from the cost of creating the wrapper.
        if (_itemTypes == null) {
          if (HasTypes && InnerItemTypes != null) {
            _itemTypes = new ReadOnlyItemTypeCollection(InnerItemTypes.OrderBy(x => x.Name));
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
    [ForeignKey("ParentGroupId")]
    public virtual MarketGroup ParentGroup {
      get {
        return _parentGroup;
      }
      private set {
        _parentGroup = value;
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
    [Column("parentGroupID")]
    public MarketGroupId? ParentGroupId {
      get {
        return _parentGroupId;
      }
      private set {
        _parentGroupId = value;
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

    #region Hidden Navigation Properties
    //******************************************************************************
    /// <summary>
    /// Hidden navigation property backing for the <see cref="ChildGroups" />
    /// property.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Necessary for so that the publicly accessible wrapper can enforce non-null
    /// ensures with Code Contracts, as well as to wrap the results in a 
    /// friendlier collection class.
    /// </para>
    /// </remarks>
    protected internal virtual ICollection<MarketGroup> InnerChildGroups {
      get {
        return _innerChildGroups;
      }
      set {
        _innerChildGroups = value;

        // Unset the wrapper collection so that it will be regenerated the next
        // time it is accessed.
        _childGroups = null;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Hidden navigation property backing for the <see cref="ItemTypes" />
    /// property.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Necessary for so that the publicly accessible wrapper can enforce non-null
    /// ensures with Code Contracts, as well as to wrap the results in a 
    /// friendlier collection class.
    /// </para>
    /// </remarks>
    protected internal virtual ICollection<ItemType> InnerItemTypes {
      get {
        return _innerItemTypes;
      }
      set {
        _innerItemTypes = value;

        // Unset the wrapper collection so that it will be regenerated the next
        // time it is accessed.
        _itemTypes = null;
      }
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