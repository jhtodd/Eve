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
        return this.flag ?? (this.flag = this.Container.Cache.GetOrAdd<Flag>(this.FlagId, () => this.Entity.Flag.ToAdapter(this.Container)));
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
        return this.itemType ?? (this.itemType = this.Container.Cache.GetOrAdd<EveType>(this.ItemTypeId, () => this.Entity.ItemType.ToAdapter(this.Container)));
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
        return this.location ?? (this.location = this.Container.Cache.GetOrAdd<Item>(this.LocationId, () => this.Entity.Location.ToAdapter(this.Container)));
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

        var result = Entity.Name;
        Contract.Assert(!string.IsNullOrWhiteSpace(result));
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
        return this.owner ?? (this.owner = this.Container.Cache.GetOrAdd<Item>(this.OwnerId, () => this.Entity.Owner.ToAdapter(this.Container)));
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
    protected virtual IConvertible CacheKey
    {
      get { return this.Id; }
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

      // Universes
      UniverseEntity universeEntity = entity as UniverseEntity;
      if (universeEntity != null)
      {
        return new Universe.Universe(container, universeEntity);
      }

      // Regions
      RegionEntity regionEntity = entity as RegionEntity;
      if (regionEntity != null)
      {
        return new Region(container, regionEntity);
      }

      // Constellations
      ConstellationEntity constellationEntity = entity as ConstellationEntity;
      if (constellationEntity != null)
      {
        return new Constellation(container, constellationEntity);
      }

      // Solar Systems
      SolarSystemEntity solarSystemEntity = entity as SolarSystemEntity;
      if (solarSystemEntity != null)
      {
        return new SolarSystem(container, solarSystemEntity);
      }

      // Corporations
      NpcCorporationEntity corporationEntity = entity as NpcCorporationEntity;
      if (corporationEntity != null)
      {
        return new NpcCorporation(container, corporationEntity);
      }

      // Stations
      StationEntity stationEntity = entity as StationEntity;
      if (stationEntity != null)
      {
        return new Station(container, stationEntity);
      }

      // Agents
      AgentEntity agentEntity = entity as AgentEntity;
      if (agentEntity != null)
      {
        return new Agent(container, agentEntity);
      }

      // If we've failed to identify the specified item type, fall back on a 
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