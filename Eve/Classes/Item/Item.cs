//-----------------------------------------------------------------------
// <copyright file="Item.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Meta;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// The base class for specific, concrete items within the EVE universe, such
  /// as solar systems, stations, agents, corporations, and so on.
  /// </summary>
  public abstract class Item : EntityAdapter<ItemEntity>,
                               IComparable,
                               IComparable<Item>,
                               IEquatable<Item>,
                               IHasId<ItemId>, 
                               IKeyItem<ItemId> {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Creates an appropriate item for the specified entity.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity.
    /// </param>
    /// 
    /// <returns>
    /// An <see cref="Item" /> of the appropriate derived type, based on the
    /// contents of <paramref name="entity" />.
    /// </returns>
    public static Item Create(ItemEntity entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
      Contract.Ensures(Contract.Result<Item>() != null);

      // Universes
      UniverseEntity universeEntity = entity as UniverseEntity;
      if (universeEntity != null) {
        return new Universe.Universe(universeEntity);
      }

      // Regions
      RegionEntity regionEntity = entity as RegionEntity;
      if (regionEntity != null) {
        return new Region(regionEntity);
      }

      // Constellations
      ConstellationEntity constellationEntity = entity as ConstellationEntity;
      if (constellationEntity != null) {
        return new Constellation(constellationEntity);
      }

      // Solar Systems
      SolarSystemEntity solarSystemEntity = entity as SolarSystemEntity;
      if (solarSystemEntity != null) {
        return new SolarSystem(solarSystemEntity);
      }

      // Corporations
      NpcCorporationEntity corporationEntity = entity as NpcCorporationEntity;
      if (corporationEntity != null) {
        return new NpcCorporation(corporationEntity);
      }

      //// Stations
      //StationEntity stationEntity = entity as StationEntity;
      //if (stationEntity != null) {
      //  return new Station(stationEntity);
      //}

      // Agents
      AgentEntity agentEntity = entity as AgentEntity;
      if (agentEntity != null) {
        return new Agent(agentEntity);
      }

      // If we've failed to identify the specified item type, fall back on a 
      // generic item.
      return new GenericItem(entity);
    }
    #endregion

    #region Instance Fields
    private Flag _flag;
    private ItemId _id;
    private EveType _itemType;
    private Item _location;
    private Item _owner;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Item class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected Item(ItemEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);

      _id = entity.Id;
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
    /// Gets the <see cref="Flag" /> value associated with the item.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Flag" /> value associated with the item.
    /// </value>
    public Flag Flag {
      get {
        Contract.Ensures(Contract.Result<Flag>() != null);

        if (_flag == null) {

          // Load the cached version if available
          _flag = Eve.General.Cache.GetOrAdd<Flag>(FlagId, () => {
            FlagEntity flagEntity = Entity.Flag;
            Contract.Assume(flagEntity != null);

            return new Flag(flagEntity);
          });
        }

        return _flag;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the <see cref="Flag" /> value associated with the item.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Flag" /> value associated with the item.
    public FlagId FlagId {
      get {
        return Entity.FlagId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public ItemId Id {
      get {
        return _id;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the item.
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="EveType" /> specifying the type of the item.
    /// </value>
    public EveType ItemType {
      get {
        Contract.Ensures(Contract.Result<EveType>() != null);

        if (_itemType == null) {

          // Load the cached version if available
          _itemType = Eve.General.Cache.GetOrAdd<EveType>(ItemTypeId, () => {
            EveTypeEntity typeEntity = Entity.ItemType;
            Contract.Assume(typeEntity != null);

            return EveType.Create(typeEntity);
          });
        }

        return _itemType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the item.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="EveType" /> specifying the type of the item.
    /// </value>
    public TypeId ItemTypeId {
      get {
        return Entity.ItemTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the object that describes the location of the item.
    /// </summary>
    /// 
    /// <value>
    /// An <see cref="Item" /> specifying the location of the item.
    /// </value>
    public Item Location {
      get {
        Contract.Ensures(Contract.Result<Item>() != null);

        if (_location == null) {

          // Load the cached version if available
          _location = Eve.General.Cache.GetOrAdd<Item>(LocationId, () => {
            ItemEntity itemEntity = Entity.Location;
            Contract.Assume(itemEntity != null);

            return Item.Create(itemEntity);
          });
        }

        return _location;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the object that describes the item's location.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the object which describes the item's location.
    /// </value>
    public ItemId LocationId {
      get {
        return Entity.LocationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="string" /> that provides the name of the item.  This value is
    /// not necessarily unique.
    /// </value>
    public string Name {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        Contract.Assume(Entity.Name != null);
        return !string.IsNullOrWhiteSpace(Entity.Name.Name) ? Entity.Name.Name : "Unknown";
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the item that owns the current item.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Item" /> that owns the current item.
    /// </value>
    public Item Owner {
      get {
        Contract.Ensures(Contract.Result<Item>() != null);

        if (_owner == null) {

          // Load the cached version if available
          _owner = Eve.General.Cache.GetOrAdd<Item>(OwnerId, () => {
            ItemEntity itemEntity = Entity.Owner;
            Contract.Assume(itemEntity != null);

            return Item.Create(itemEntity);
          });
        }

        return _owner;
        
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item that owns the current item.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Item" /> that owns the current item.
    /// </value>
    public ItemId OwnerId {
      get {
        return Entity.OwnerId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the quantity of the item.
    /// </summary>
    /// 
    /// <value>
    /// The quantity of the item.
    /// </value>
    public int Quantity {
      get {
        return Entity.Quantity;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(Item other) {
      if (other == null) {
        return 1;
      }

      return Name.CompareTo(other.Name);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as Item);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(Item other) {
      if (other == null) {
        return false;
      }

      return Id.Equals(other.Id);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return Id.GetHashCode();
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return Name;
    }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item within its cache region.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="Int32" /> specifying a unique ID for the item within the
    /// cache region established for the item's type.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// For all known EVE values with 32-bit or 64-bit integer IDs, the value
    /// of <see cref="GetHashCode"/> is sufficient to serve as a cache ID.
    /// </para>
    /// </remarks>
    protected virtual int CacheID {
      get {
        return Id.GetHashCode();
      }
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      Item other = obj as Item;
      return CompareTo(other);
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get { return Id; }
    }
    #endregion
    #region IKeyItem<ItemId> Members
    //******************************************************************************
    ItemId IKeyItem<ItemId>.Key {
      get { return Id; }
    }
    #endregion
  }
}