//-----------------------------------------------------------------------
// <copyright file="SolarSystemJump.cs" company="Jeremy H. Todd">
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
  /// Contains information about a jump link between solar systems.
  /// </summary>
  public sealed partial class SolarSystemJump
    : EveEntityAdapter<SolarSystemJumpEntity>,
      IComparable,
      IComparable<SolarSystemJump>,
      IEquatable<SolarSystemJump>,
      IEveCacheable,
      IKeyItem<long>
  {
    private Constellation fromConstellation;
    private Region fromRegion;
    private SolarSystem fromSolarSystem;
    private Constellation toConstellation;
    private Region toRegion;
    private SolarSystem toSolarSystem;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SolarSystemJump class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal SolarSystemJump(SolarSystemJumpEntity entity) : base(entity)
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
    /// Gets the origin solar system.
    /// </summary>
    /// <value>
    /// The origin solar system.
    /// </value>
    public SolarSystem FromSolarSystem
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystem>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.fromSolarSystem ?? (this.fromSolarSystem = Eve.General.Cache.GetOrAdd<SolarSystem>(this.FromSolarSystemId, () => (SolarSystem)this.Entity.FromSolarSystem.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the origin solar system.
    /// </summary>
    /// <value>
    /// The ID of the origin solar system.
    /// </value>
    public SolarSystemId FromSolarSystemId
    {
      get { return Entity.FromSolarSystemId; }
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

    /// <summary>
    /// Gets the destination solar system.
    /// </summary>
    /// <value>
    /// The destination solar system.
    /// </value>
    public SolarSystem ToSolarSystem
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystem>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.toSolarSystem ?? (this.toSolarSystem = Eve.General.Cache.GetOrAdd<SolarSystem>(this.ToSolarSystemId, () => (SolarSystem)this.Entity.ToSolarSystem.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the destination solar system.
    /// </summary>
    /// <value>
    /// The ID of the destination solar system.
    /// </value>
    public SolarSystemId ToSolarSystemId
    {
      get { return Entity.ToSolarSystemId; }
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
    public static long CreateCompoundId(SolarSystemId fromId, SolarSystemId toId)
    {
      return (long)((((ulong)(long)fromId.GetHashCode()) << 32) | ((ulong)(long)toId.GetHashCode()));
    }

    /// <inheritdoc />
    public int CompareTo(SolarSystemJump other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.FromSolarSystem.Name.CompareTo(other.FromSolarSystem.Name);

      if (result == 0)
      {
        result = this.ToSolarSystem.Name.CompareTo(other.ToSolarSystem.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SolarSystemJump);
    }

    /// <inheritdoc />
    public bool Equals(SolarSystemJump other)
    {
      if (other == null)
      {
        return false;
      }

      return this.FromSolarSystemId == other.FromSolarSystemId && this.ToSolarSystemId == other.ToSolarSystemId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.FromSolarSystemId, this.ToSolarSystemId);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.FromSolarSystem.Name + " to " + this.ToSolarSystem.Name;
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class SolarSystemJump : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      SolarSystemJump other = obj as SolarSystemJump;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class SolarSystemJump : IEveCacheable
  {
    object IEveCacheable.CacheKey
    {
      get { return CreateCompoundId(this.FromSolarSystemId, this.ToSolarSystemId); }
    }
  }
  #endregion

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class SolarSystemJump : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return CreateCompoundId(this.FromSolarSystemId, this.ToSolarSystemId); }
    }
  }
  #endregion
}