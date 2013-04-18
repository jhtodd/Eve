//-----------------------------------------------------------------------
// <copyright file="Item.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet.Collections;

  /// <summary>
  /// The base class for specific, concrete items within the EVE universe, such
  /// as solar systems, stations, agents, corporations, and so on.
  /// </summary>
  [EveCacheDomain(typeof(Item))]
  public abstract partial class Item 
    : EveEntityAdapter<ItemEntity>,
      IComparable,
      IComparable<Item>,
      IEveCacheable,
      IEquatable<Item>,
      IKeyItem<ItemId>
  {
    private Flag flag;
    private ItemId id;
    private EveType itemType;
    private Item location;
    private Item owner;
    private ItemPosition position;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Item class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected Item(IEveRepository container, ItemEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");

      this.id = entity.Id;
    }

    /* Properties */

    /// <summary>
    /// Gets the <see cref="Flag" /> value associated with the item.
    /// </summary>
    /// <value>
    /// The <see cref="Flag" /> value associated with the item.
    /// </value>
    public Flag Flag
    {
      get
      {
        Contract.Ensures(Contract.Result<Flag>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.flag ?? (this.flag = this.Container.GetOrAdd<Flag>(this.FlagId, () => this.Entity.Flag.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Flag" /> value associated with the item.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Flag" /> value associated with the item.
    /// </value>
    public FlagId FlagId
    {
      get { return Entity.FlagId; }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public ItemId Id
    {
      get { return this.id; }
    }

    /// <summary>
    /// Gets the type of the item.
    /// </summary>
    /// <value>
    /// An <see cref="EveType" /> specifying the type of the item.
    /// </value>
    public EveType ItemType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.itemType ?? (this.itemType = this.Container.GetOrAdd<EveType>(this.ItemTypeId, () => this.Entity.ItemType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the item.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="EveType" /> specifying the type of the item.
    /// </value>
    public TypeId ItemTypeId
    {
      get { return Entity.ItemTypeId; }
    }

    /// <summary>
    /// Gets the object that describes the location of the item.
    /// </summary>
    /// <value>
    /// An <see cref="Item" /> specifying the location of the item.
    /// </value>
    public Item Location
    {
      get
      {
        Contract.Ensures(Contract.Result<Item>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.location ?? (this.location = this.Container.GetOrAdd<Item>(this.LocationId, () => this.Entity.Location.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the object that describes the item's location.
    /// </summary>
    /// <value>
    /// The ID of the object which describes the item's location.
    /// </value>
    public ItemId LocationId
    {
      get { return Entity.LocationId; }
    }

    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that provides the name of the item.  This value is
    /// not necessarily unique.
    /// </value>
    public string Name
    {
      get
      {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        var result = this.Entity.Name;

        if (string.IsNullOrWhiteSpace(result))
        {
          result = "[Unknown]";
        }

        return result;
      }
    }

    /// <summary>
    /// Gets the item that owns the current item.
    /// </summary>
    /// <value>
    /// The <see cref="Item" /> that owns the current item.
    /// </value>
    public Item Owner
    {
      get
      {
        // This property can sometimes be null even with a FactionId value,
        // because a small number of records have an invalid OwnerId.  Return 
        // null in that case.
        if (this.Entity.Owner == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.owner ?? (this.owner = this.Container.GetOrAdd<Item>(this.OwnerId, () => this.Entity.Owner.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the item that owns the current item.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Item" /> that owns the current item.
    /// </value>
    public ItemId OwnerId
    {
      get { return Entity.OwnerId; }
    }

    /// <summary>
    /// Gets the position of the item in space, if applicable.
    /// </summary>
    /// <value>
    /// An <see cref="ItemPosition" /> object specifying the position
    /// of the item in space.
    /// </value>
    public ItemPosition Position
    {
      get
      {
        if (this.Entity.Position == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.position ?? (this.position = this.Container.GetOrAdd<ItemPosition>(this.Id, () => this.Entity.Position.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the quantity of the item.
    /// </summary>
    /// <value>
    /// The quantity of the item.
    /// </value>
    public int Quantity
    {
      get { return Entity.Quantity; }
    }

    /// <summary>
    /// Gets the key used to cache the current item.
    /// </summary>
    /// <value>
    /// The key used to cache the current item.
    /// </value>
    protected virtual IConvertible CacheKey
    {
      get 
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        return this.Id;
      }
    }

    /* Methods */

    /// <summary>
    /// Creates an appropriate item for the specified entity.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity.
    /// </param>
    /// <returns>
    /// An <see cref="Item" /> of the appropriate derived type, based on the
    /// contents of <paramref name="entity" />.
    /// </returns>
    public static Item Create(IEveRepository container, ItemEntity entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Ensures(Contract.Result<Item>() != null);

      if (entity.IsAgent)
      {
        return new Agent(container, entity);
      }

      if (entity.IsCelestial)
      {
        return new Celestial(container, entity);
      }

      if (entity.IsConstellation)
      {
        return new Constellation(container, entity);
      }

      if (entity.IsCorporation)
      {
        return new NpcCorporation(container, entity);
      }

      if (entity.IsFaction)
      {
        return new Character.Faction(container, entity);
      }

      if (entity.IsRegion)
      {
        return new Region(container, entity);
      }

      if (entity.IsSolarSystem)
      {
        return new SolarSystem(container, entity);
      }

      if (entity.IsStargate)
      {
        return new Stargate(container, entity);
      }

      if (entity.IsStation)
      {
        return new Station(container, entity);
      }

      if (entity.IsUniverse)
      {
        return new Universe.Universe(container, entity);
      }

      // If we've failed to identify the specific item type, fall back on a 
      // generic item.
      return new GenericItem(container, entity);
    }

    /// <inheritdoc />
    public virtual int CompareTo(Item other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.Name.CompareTo(other.Name);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as Item);
    }

    /// <inheritdoc />
    public virtual bool Equals(Item other)
    {
      if (other == null)
      {
        return false;
      }

      return this.Id.Equals(other.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Name;
    }       
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public abstract partial class Item : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      Item other = obj as Item;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public abstract partial class Item : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion

  #region IKeyItem<ItemId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public abstract partial class Item : IKeyItem<ItemId>
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Value is accessible via the Id property.")]
    ItemId IKeyItem<ItemId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}