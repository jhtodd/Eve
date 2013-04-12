﻿//-----------------------------------------------------------------------
// <copyright file="ConstellationJump.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about a jump link between constellations.
  /// </summary>
  public sealed partial class ConstellationJump
    : EveEntityAdapter<ConstellationJumpEntity>,
      IComparable,
      IComparable<ConstellationJump>,
      IEquatable<ConstellationJump>,
      IEveCacheable,
      IKeyItem<long>
  {
    private Constellation fromConstellation;
    private Region fromRegion;
    private Constellation toConstellation;
    private Region toRegion;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ConstellationJump class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal ConstellationJump(ConstellationJumpEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }

    /* Properties */

    /// <summary>
    /// Gets the origin constellation.
    /// </summary>
    /// <value>
    /// The origin constellation.
    /// </value>
    public Constellation FromConstellation
    {
      get
      {
        Contract.Ensures(Contract.Result<Constellation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.fromConstellation ?? (this.fromConstellation = Eve.General.Cache.GetOrAdd<Constellation>(this.FromConstellationId, () => (Constellation)this.Entity.FromConstellation.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the origin constellation.
    /// </summary>
    /// <value>
    /// The ID of the origin constellation.
    /// </value>
    public ConstellationId FromConstellationId
    {
      get { return Entity.FromConstellationId; }
    }

    /// <summary>
    /// Gets the origin region.
    /// </summary>
    /// <value>
    /// The origin region.
    /// </value>
    public Region FromRegion
    {
      get
      {
        Contract.Ensures(Contract.Result<Region>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.fromRegion ?? (this.fromRegion = Eve.General.Cache.GetOrAdd<Region>(this.FromRegionId, () => (Region)this.Entity.FromRegion.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the origin region.
    /// </summary>
    /// <value>
    /// The ID of the origin region.
    /// </value>
    public RegionId FromRegionId
    {
      get { return Entity.FromRegionId; }
    }

    /// <summary>
    /// Gets the destination constellation.
    /// </summary>
    /// <value>
    /// The destination constellation.
    /// </value>
    public Constellation ToConstellation
    {
      get
      {
        Contract.Ensures(Contract.Result<Constellation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.toConstellation ?? (this.toConstellation = Eve.General.Cache.GetOrAdd<Constellation>(this.ToConstellationId, () => (Constellation)this.Entity.ToConstellation.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the destination constellation.
    /// </summary>
    /// <value>
    /// The ID of the destination constellation.
    /// </value>
    public ConstellationId ToConstellationId
    {
      get { return Entity.ToConstellationId; }
    }

    /// <summary>
    /// Gets the destination region.
    /// </summary>
    /// <value>
    /// The destination region.
    /// </value>
    public Region ToRegion
    {
      get
      {
        Contract.Ensures(Contract.Result<Region>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.toRegion ?? (this.toRegion = Eve.General.Cache.GetOrAdd<Region>(this.ToRegionId, () => (Region)this.Entity.ToRegion.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the destination region.
    /// </summary>
    /// <value>
    /// The ID of the destination region.
    /// </value>
    public RegionId ToRegionId
    {
      get { return Entity.ToRegionId; }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="fromId">
    /// The ID of the origin constellation.
    /// </param>
    /// <param name="toId">
    /// The ID of the destination constellation.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(ConstellationId fromId, ConstellationId toId)
    {
      return (long)((((ulong)(long)fromId.GetHashCode()) << 32) | ((ulong)(long)toId.GetHashCode()));
    }

    /// <inheritdoc />
    public int CompareTo(ConstellationJump other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.FromConstellation.Name.CompareTo(other.FromConstellation.Name);

      if (result == 0)
      {
        result = this.ToConstellation.Name.CompareTo(other.ToConstellation.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as ConstellationJump);
    }

    /// <inheritdoc />
    public bool Equals(ConstellationJump other)
    {
      if (other == null)
      {
        return false;
      }

      return this.FromConstellationId == other.FromConstellationId && this.ToConstellationId == other.ToConstellationId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.FromConstellationId, this.ToConstellationId);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.FromConstellation.Name + " to " + this.ToConstellation.Name;
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class ConstellationJump : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      ConstellationJump other = obj as ConstellationJump;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class ConstellationJump : IEveCacheable
  {
    object IEveCacheable.CacheKey
    {
      get { return CreateCompoundId(this.FromConstellationId, this.ToConstellationId); }
    }
  }
  #endregion

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class ConstellationJump : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return CreateCompoundId(this.FromConstellationId, this.ToConstellationId); }
    }
  }
  #endregion
}