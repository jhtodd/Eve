//-----------------------------------------------------------------------
// <copyright file="Item.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

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

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Item class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected Item(ItemEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);

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
        return this.flag ?? (this.flag = Eve.General.Cache.GetOrAdd<Flag>(this.FlagId, () => (Flag)this.Entity.Flag.ToAdapter()));
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
        return this.itemType ?? (this.itemType = Eve.General.Cache.GetOrAdd<EveType>(this.ItemTypeId, () => (EveType)this.Entity.ItemType.ToAdapter()));
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
        return this.location ?? (this.location = Eve.General.Cache.GetOrAdd<Item>(this.LocationId, () => Item.Create(this.Entity.Location)));
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

        Contract.Assume(Entity.Name != null);
        return !string.IsNullOrWhiteSpace(Entity.Name.Name) ? Entity.Name.Name : "Unknown";
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
        Contract.Ensures(Contract.Result<Item>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.owner ?? (this.owner = Eve.General.Cache.GetOrAdd<Item>(this.OwnerId, () => Item.Create(this.Entity.Owner)));
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
    protected virtual object CacheKey
    {
      get { return this.Id; }
    }

    /* Methods */

    /// <summary>
    /// Creates an appropriate item for the specified entity.
    /// </summary>
    /// <param name="entity">
    /// The data entity.
    /// </param>
    /// <returns>
    /// An <see cref="Item" /> of the appropriate derived type, based on the
    /// contents of <paramref name="entity" />.
    /// </returns>
    public static Item Create(ItemEntity entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
      Contract.Ensures(Contract.Result<Item>() != null);

      // Universes
      UniverseEntity universeEntity = entity as UniverseEntity;
      if (universeEntity != null)
      {
        return new Universe.Universe(universeEntity);
      }

      // Regions
      RegionEntity regionEntity = entity as RegionEntity;
      if (regionEntity != null)
      {
        return new Region(regionEntity);
      }

      // Constellations
      ConstellationEntity constellationEntity = entity as ConstellationEntity;
      if (constellationEntity != null)
      {
        return new Constellation(constellationEntity);
      }

      // Solar Systems
      SolarSystemEntity solarSystemEntity = entity as SolarSystemEntity;
      if (solarSystemEntity != null)
      {
        return new SolarSystem(solarSystemEntity);
      }

      // Corporations
      NpcCorporationEntity corporationEntity = entity as NpcCorporationEntity;
      if (corporationEntity != null)
      {
        return new NpcCorporation(corporationEntity);
      }

      // Stations
      StationEntity stationEntity = entity as StationEntity;
      if (stationEntity != null)
      {
        return new Station(stationEntity);
      }

      // Agents
      AgentEntity agentEntity = entity as AgentEntity;
      if (agentEntity != null)
      {
        return new Agent(agentEntity);
      }

      // If we've failed to identify the specified item type, fall back on a 
      // generic item.
      return new GenericItem(entity);
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
    object IEveCacheable.CacheKey
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