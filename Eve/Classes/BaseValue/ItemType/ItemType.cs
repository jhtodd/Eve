//-----------------------------------------------------------------------
// <copyright file="ItemType.cs" company="Jeremy H. Todd">
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
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;

  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// Contains information about a group to which an EVE item belongs.
  /// </summary>>
  [Table("invTypes")]
  public class ItemType : BaseValue<int, ItemType>,
                          IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private decimal _basePrice;
    private double _capacity;
    private double _chanceOfDuplicating;
    private GroupId _groupId;
    private int? _iconId;
    private MarketGroupId? _marketGroupId;
    private double _mass;
    private RaceId? _raceId;
    private int _portionSize;
    private bool _published;
    private double _volume;

    private Icon _icon;
    private Group _innerGroup;
    private MarketGroup _marketGroup;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ItemType class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public ItemType() : base(0, DEFAULT_NAME, string.Empty) {
      _basePrice = 0M;
      _capacity = 0.0D;
      _chanceOfDuplicating = 0.0D;
      _mass = 0.0D;
      _portionSize = 0;
      _volume = 0.0D;
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ItemType class.
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
    /// <param name="iconId">
    /// The ID of the icon associated with the item, if any.
    /// </param>
    /// 
    /// <param name="published">
    /// Indicates whether the item is marked as published for public
    /// consumption.
    /// </param>
    public ItemType(int id, 
                    string name,
                    string description,
                    decimal basePrice,
                    double capacity,
                    double chanceOfDuplicating,
                    GroupId groupId,
                    int? iconId,
                    MarketGroupId? marketGroupId,
                    double mass,
                    RaceId? raceId,
                    int portionSize,
                    bool published,
                    double volume) : base(id, name, description) {
      
      Contract.Requires(!string.IsNullOrWhiteSpace(name), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);
      Contract.Requires(!double.IsInfinity(capacity), Resources.Messages.ItemType_CapacityMustBeNumeric);
      Contract.Requires(!double.IsNaN(capacity), Resources.Messages.ItemType_CapacityMustBeNumeric);
      Contract.Requires(!double.IsInfinity(chanceOfDuplicating), Resources.Messages.ItemType_ChanceOfDuplicatingMustBeNumeric);
      Contract.Requires(!double.IsNaN(chanceOfDuplicating), Resources.Messages.ItemType_ChanceOfDuplicatingMustBeNumeric);
      Contract.Requires(!double.IsInfinity(mass), Resources.Messages.ItemType_MassMustBeNumeric);
      Contract.Requires(!double.IsNaN(mass), Resources.Messages.ItemType_MassMustBeNumeric);
      Contract.Requires(!double.IsInfinity(volume), Resources.Messages.ItemType_VolumeMustBeNumeric);
      Contract.Requires(!double.IsNaN(volume), Resources.Messages.ItemType_VolumeMustBeNumeric);

      _basePrice = basePrice;
      _capacity = capacity;
      _chanceOfDuplicating = chanceOfDuplicating;
      _groupId = groupId;
      _iconId = iconId;
      _marketGroupId = marketGroupId;
      _mass = mass;
      _raceId = raceId;
      _portionSize = portionSize;
      _published = published;
      _volume = volume;

      Contract.Assume(!double.IsInfinity(_capacity)); // Shouldn't be needed, but is
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(!double.IsInfinity(_capacity));
      Contract.Invariant(!double.IsNaN(_capacity));
      Contract.Invariant(!double.IsInfinity(_chanceOfDuplicating));
      Contract.Invariant(!double.IsNaN(_chanceOfDuplicating));
      Contract.Invariant(!double.IsInfinity(_mass));
      Contract.Invariant(!double.IsNaN(_mass));
      Contract.Invariant(!double.IsInfinity(_volume));
      Contract.Invariant(!double.IsNaN(_volume));
    }
    #endregion
    #region Public Properties
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
    [Column("basePrice")]
    public decimal BasePrice {
      get {
        return _basePrice;
      }
      protected set {
        _basePrice = value;
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
    [Column("capacity")]
    public double Capacity {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        return _capacity;
      }
      protected set {
        Contract.Requires(!double.IsInfinity(value), Resources.Messages.ItemType_CapacityMustBeNumeric);
        Contract.Requires(!double.IsNaN(value), Resources.Messages.ItemType_CapacityMustBeNumeric);
        _capacity = value;
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
    [NotMapped()]
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
    [NotMapped()]
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
    [Column("chanceOfDuplicating")]
    public double ChanceOfDuplicating {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        return _chanceOfDuplicating;
      }
      protected set {
        Contract.Requires(!double.IsInfinity(value), Resources.Messages.ItemType_ChanceOfDuplicatingMustBeNumeric);
        Contract.Requires(!double.IsNaN(value), Resources.Messages.ItemType_ChanceOfDuplicatingMustBeNumeric);
        _chanceOfDuplicating = value;
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

        // This property uses a hidden navigation property backing to enforce
        // non-null ensures
        Group result = InnerGroup;
        Contract.Assume(result != null); // This will be set by this point
        return result;
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
    [Column("groupID")]
    public GroupId GroupId {
      get {
        return _groupId;
      }
      protected set {
        _groupId = value;
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
      protected set {
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
      protected set {
        _iconId = value;
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
    [ForeignKey("MarketGroupId")]
    public virtual MarketGroup MarketGroup {
      get {
        return _marketGroup;
      }
      protected set {
        _marketGroup = value;
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
    [Column("marketGroupID")]
    public MarketGroupId? MarketGroupId {
      get {
        return _marketGroupId;
      }
      protected set {
        _marketGroupId = value;
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
    [Column("mass")]
    public double Mass {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        return _mass;
      }
      protected set {
        Contract.Requires(!double.IsInfinity(value), Resources.Messages.ItemType_MassMustBeNumeric);
        Contract.Requires(!double.IsNaN(value), Resources.Messages.ItemType_MassMustBeNumeric);
        _mass = value;
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
    [Column("raceID")]
    public RaceId? RaceId {
      get {
        return _raceId;
      }
      protected set {
        _raceId = value;
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
    [Column("portionSize")]
    public int PortionSize {
      get {
        return _portionSize;
      }
      protected set {
        _portionSize = value;
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
      protected set {
        _published = value;
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
    [Column("volume")]
    public double Volume {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        return _volume;
      }
      protected set {
        Contract.Requires(!double.IsInfinity(value), Resources.Messages.ItemType_VolumeMustBeNumeric);
        Contract.Requires(!double.IsNaN(value), Resources.Messages.ItemType_VolumeMustBeNumeric);
        _volume = value;
      }
    }
    #endregion

    #region Hidden Navigation Properties
    //******************************************************************************
    /// <summary>
    /// Hidden navigation property backing for the <see cref="Group" /> property.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Necessary for required navigation properties so that the publicly accessible
    /// wrapper can enforce non-null ensures with Code Contracts.
    /// </para>
    /// </remarks>
    [ForeignKey("GroupId")]
    protected internal virtual Group InnerGroup {
      get {
        return _innerGroup;
      }
      set {
        Contract.Requires(value != null, Resources.Messages.ItemType_GroupCannotBeNull);
        _innerGroup = value;
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