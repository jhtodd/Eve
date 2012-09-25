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
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// Contains information about a group to which an EVE item belongs.
  /// </summary>>
  [Table("invGroups")]
  public class Group : BaseValue<GroupId, Group>,
                       IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private bool _allowManufacture;
    private bool _allowRecycler;
    private bool _anchorable;
    private bool _anchored;
    private CategoryId _categoryId;
    private bool _fittableNonSingleton;
    private int? _iconId;
    private bool _published;
    private bool _useBasePrice;

    private Icon _icon;
    private Category _innerCategory;
    private ICollection<ItemType> _innerItemTypes;
    private ReadOnlyItemTypeCollection _itemTypes;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Group class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public Group() : base(0, DEFAULT_NAME, string.Empty) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Group class.
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
    /// <param name="allowManufacture">
    /// Indicates whether items in the group can be manufactured.
    /// </param>
    /// 
    /// <param name="allowRecycler">
    /// Indicates whether items in the group can be reprocessed.
    /// </param>
    /// 
    /// <param name="anchorable">
    /// Indicates whether items in the group can be anchored.
    /// </param>
    /// 
    /// <param name="anchored">
    /// Indicates whether items in the group are permanently anchored.
    /// </param>
    /// 
    /// <param name="categoryId">
    /// The ID of the category to which the item belongs.
    /// </param>
    /// 
    /// <param name="fittableNonSingleton">
    /// Indicates whether multiple instances of items in the group can be
    /// fitted.
    /// </param>
    /// 
    /// <param name="iconId">
    /// The ID of the icon associated with the item, if any.
    /// </param>
    /// 
    /// <param name="published">
    /// Indicates whether the item is marked as published for public
    /// consumption.
    /// </param>
    /// 
    /// <param name="useBasePrice">
    /// Indicates whether to use the base price for items in the group.
    /// </param>
    public Group(GroupId id, 
                 string name,
                 string description,
                 bool allowManufacture,
                 bool allowRecycler,
                 bool anchorable,
                 bool anchored,
                 CategoryId categoryId,
                 bool fittableNonSingleton,
                 int? iconId,
                 bool published,
                 bool useBasePrice) : base(id, name, description) {
      Contract.Requires(!string.IsNullOrWhiteSpace(name), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);

      _allowManufacture = allowManufacture;
      _allowRecycler = allowRecycler;
      _anchorable = anchorable;
      _anchored = anchored;
      _categoryId = categoryId;
      _fittableNonSingleton = fittableNonSingleton;
      _iconId = iconId;
      _published = published;
      _useBasePrice = useBasePrice;
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
    [Column("allowManufacture")]
    public bool AllowManufacture {
      get {
        return _allowManufacture;
      }
      private set {
        _allowManufacture = value;
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
    [Column("allowRecycler")]
    public bool AllowRecycler {
      get {
        return _allowRecycler;
      }
      private set {
        _allowRecycler = value;
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
    [Column("anchorable")]
    public bool Anchorable {
      get {
        return _anchorable;
      }
      private set {
        _anchorable = value;
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
    [Column("anchored")]
    public bool Anchored {
      get {
        return _anchored;
      }
      private set {
        _anchorable = value;
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

        Category result = InnerCategory;
        Contract.Assume(result != null);
        return result;
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
    [Column("categoryID")]
    public CategoryId CategoryId {
      get {
        return _categoryId;
      }
      private set {
        _categoryId = value;
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
    [Column("fittableNonSingleton")]
    public bool FittableNonSingleton {
      get {
        return _fittableNonSingleton;
      }
      private set {
        _fittableNonSingleton = value;
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
    /// Gets the collection of items that belong to the current group.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyItemTypeCollection" /> containing the items that
    /// belong to the current group.
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
          if (InnerItemTypes != null) {
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
    /// Gets a value indicating whether the item is marked as published for
    /// public consumption.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the item is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    [Column("published")]
    public bool Published {
      get {
        return _published;
      }
      private set {
        _published = value;
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
    [Column("useBasePrice")]
    public bool UseBasePrice {
      get {
        return _useBasePrice;
      }
      private set {
        _useBasePrice = value;
      }
    }
    #endregion

    #region Hidden Navigation Properties
    //******************************************************************************
    /// <summary>
    /// Hidden navigation property backing for the <see cref="Category" /> property.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Necessary for required navigation properties so that the publicly accessible
    /// wrapper can enforce non-null ensures with Code Contracts.
    /// </para>
    /// </remarks>
    [ForeignKey("CategoryId")]
    protected internal virtual Category InnerCategory {
      get {
        return _innerCategory;
      }
      set {
        Contract.Requires(value != null, Resources.Messages.Group_CategoryCannotBeNull);
        _innerCategory = value;
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
  }
}