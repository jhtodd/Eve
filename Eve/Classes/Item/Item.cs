//-----------------------------------------------------------------------
// <copyright file="Item.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

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
    private string name;
    private Item owner;
    private ItemPosition position;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Item class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected Item(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
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
        return this.LazyInitializeAdapter(ref this.flag, this.Entity.FlagId, () => this.Entity.Flag);
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
        return this.LazyInitializeAdapter(ref this.itemType, this.Entity.ItemTypeId, () => this.Entity.ItemType);
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
        return this.LazyInitializeAdapter(ref this.location, this.Entity.LocationId, () => this.Entity.Location);
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

        LazyInitializer.EnsureInitialized(
          ref this.name,
          () =>
          {
            // Start by checking whether the item has an entry in the invNames table
            if (this.Entity.Name != null && !string.IsNullOrWhiteSpace(this.Entity.Name.Value))
            {
              return this.Entity.Name.Value;
            }

            // Next, fall back on the constellation name, if applicable
            if (this.Entity.IsConstellation && this.Entity.ConstellationInfo != null && !string.IsNullOrWhiteSpace(this.Entity.ConstellationInfo.ConstellationName))
            {
              return this.Entity.ConstellationInfo.ConstellationName;
            }

            // Then the faction name
            if (this.Entity.IsFaction && this.Entity.FactionInfo != null && !string.IsNullOrWhiteSpace(this.Entity.FactionInfo.FactionName))
            {
              return this.Entity.FactionInfo.FactionName;
            }

            // Then the region name
            if (this.Entity.IsRegion && this.Entity.RegionInfo != null && !string.IsNullOrWhiteSpace(this.Entity.RegionInfo.RegionName))
            {
              return this.Entity.RegionInfo.RegionName;
            }

            // Then the solar system name
            if (this.Entity.IsSolarSystem && this.Entity.SolarSystemInfo != null && !string.IsNullOrWhiteSpace(this.Entity.SolarSystemInfo.SolarSystemName))
            {
              return this.Entity.RegionInfo.RegionName;
            }

            // Then the station name
            if (this.Entity.IsStation && this.Entity.StationInfo != null && !string.IsNullOrWhiteSpace(this.Entity.StationInfo.StationName))
            {
              return this.Entity.StationInfo.StationName;
            }

            // Then the universe name
            if (this.Entity.IsUniverse && this.Entity.UniverseInfo != null && !string.IsNullOrWhiteSpace(this.Entity.UniverseInfo.UniverseName))
            {
              return this.Entity.UniverseInfo.UniverseName;
            }

            // If no name could be found, generate one dynamically.
            return "[Unnamed Item " + this.Id.ToString() + "]";
          });

        Contract.Assume(!string.IsNullOrWhiteSpace(this.name));
        return this.name;
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
        // This property can sometimes be null even with a OwnerId value,
        // because a small number of records have an invalid OwnerId.  Return 
        // null in that case.
        if (this.Entity.Owner == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.owner, this.Entity.OwnerId, () => this.Entity.Owner);
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
        return this.LazyInitializeAdapter(ref this.position, this.Entity.Id, () => this.Entity.Position);
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

    /* Methods */

    /// <summary>
    /// Creates an appropriate item for the specified entity.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity.
    /// </param>
    /// <returns>
    /// An <see cref="Item" /> of the appropriate derived type, based on the
    /// contents of <paramref name="entity" />.
    /// </returns>
    public static Item Create(IEveRepository repository, ItemEntity entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Ensures(Contract.Result<Item>() != null);

      if (entity.IsAgent)
      {
        return new Agent(repository, entity);
      }

      if (entity.IsCelestial)
      {
        return new Celestial(repository, entity);
      }

      if (entity.IsConstellation)
      {
        return new Constellation(repository, entity);
      }

      if (entity.IsCorporation)
      {
        return new NpcCorporation(repository, entity);
      }

      if (entity.IsFaction)
      {
        return new Character.Faction(repository, entity);
      }

      if (entity.IsRegion)
      {
        return new Region(repository, entity);
      }

      if (entity.IsSolarSystem)
      {
        return new SolarSystem(repository, entity);
      }

      if (entity.IsStargate)
      {
        return new Stargate(repository, entity);
      }

      if (entity.IsStation)
      {
        return new Station(repository, entity);
      }

      if (entity.IsUniverse)
      {
        return new Universe.Universe(repository, entity);
      }

      // If we've failed to identify the specific item type, fall back on a 
      // generic item.
      return new GenericItem(repository, entity);
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