﻿//-----------------------------------------------------------------------
// <copyright file="ItemPosition.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;

  /// <summary>
  /// Contains information about the position and orientation of an item
  /// in space.
  /// </summary>
  public sealed partial class ItemPosition
    : EveEntityAdapter<ItemPositionEntity, ItemPosition>,
      IKeyItem<ItemId>
  {
    private Item item;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemPosition" /> class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal ItemPosition(IEveRepository repository, ItemPositionEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the ID of the item whose position is being described.
    /// </summary>
    /// <value>
    /// The ID of the item whose position is being described.
    /// </value>
    public Item Item
    {
      get 
      {
        Contract.Ensures(Contract.Result<Item>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.item, this.Entity.ItemId, () => this.Entity.Item);
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The ID of the item.
    /// </value>
    public ItemId ItemId
    {
      get { return this.Entity.ItemId; }
    }

    /// <summary>
    /// Gets the pitch orientation of the item.  This property is not set for
    /// the vast majority of items.
    /// </summary>
    /// <value>
    /// The pitch orientation of the item.
    /// </value>
    public float Pitch
    {
      get
      {
        Contract.Ensures(!float.IsInfinity(Contract.Result<float>()));
        Contract.Ensures(!float.IsNaN(Contract.Result<float>()));

        float result = this.Entity.Pitch.HasValue ? this.Entity.Pitch.Value : 0.0F;

        Contract.Assume(!float.IsInfinity(result));
        Contract.Assume(!float.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the roll orientation of the item.  This property is not set for
    /// the vast majority of items.
    /// </summary>
    /// <value>
    /// The roll orientation of the item.
    /// </value>
    public float Roll
    {
      get
      {
        Contract.Ensures(!float.IsInfinity(Contract.Result<float>()));
        Contract.Ensures(!float.IsNaN(Contract.Result<float>()));

        float result = this.Entity.Roll.HasValue ? this.Entity.Roll.Value : 0.0F;

        Contract.Assume(!float.IsInfinity(result));
        Contract.Assume(!float.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the X coordinate of the item's position, relative to the
    /// position specified by its <see cref="Item.Location" /> property.
    /// </summary>
    /// <value>
    /// The X coordinate of the item's position.
    /// </value>
    public double X
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result = this.Entity.X;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Y coordinate of the item's position, relative to the
    /// position specified by its <see cref="Item.Location" /> property.
    /// </summary>
    /// <value>
    /// The Y coordinate of the item's position.
    /// </value>
    public double Y
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result = this.Entity.Y;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the yaw orientation of the item.  This property is not set for
    /// the vast majority of items.
    /// </summary>
    /// <value>
    /// The yaw orientation of the item.
    /// </value>
    public float Yaw
    {
      get
      {
        Contract.Ensures(!float.IsInfinity(Contract.Result<float>()));
        Contract.Ensures(!float.IsNaN(Contract.Result<float>()));

        float result = this.Entity.Yaw.HasValue ? this.Entity.Yaw.Value : 0.0F;

        Contract.Assume(!float.IsInfinity(result));
        Contract.Assume(!float.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Z coordinate of the item's position, relative to the
    /// position specified by its <see cref="Item.Location" /> property.
    /// </summary>
    /// <value>
    /// The Z coordinate of the item's position.
    /// </value>
    public double Z
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result = this.Entity.Z;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(ItemPosition other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.X.CompareTo(other.X);

      if (result == 0)
      {
        result = this.Y.CompareTo(other.Y);
      }

      if (result == 0)
      {
        result = this.Z.CompareTo(other.Z);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Item.Name + " (" + this.X.ToString() + ", " + this.Y.ToString() + ", " + this.Z.ToString() + ")";
    }
  }

  #region IKeyItem<ItemId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class ItemPosition : IKeyItem<ItemId>
  {
    ItemId IKeyItem<ItemId>.Key
    {
      get { return this.ItemId; }
    }
  }
  #endregion
}