//-----------------------------------------------------------------------
// <copyright file="RegionJump.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about a jump link between regions.
  /// </summary>
  public sealed partial class RegionJump 
    : EveEntityAdapter<RegionJumpEntity>,
      IComparable,
      IComparable<RegionJump>,
      IEquatable<RegionJump>,
      IEveCacheable,
      IKeyItem<long>
  {
    private Region fromRegion;
    private Region toRegion;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the RegionJump class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal RegionJump(IEveRepository container, RegionJumpEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

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
        LazyInitializer.EnsureInitialized(
          ref this.fromRegion,
          () => this.Container.GetOrAdd<Region>(this.FromRegionId, () => (Region)this.Entity.FromRegion.ToAdapter(this.Container)));

        Contract.Assume(this.fromRegion != null);
        return this.fromRegion;
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
        LazyInitializer.EnsureInitialized(
          ref this.toRegion,
          () => this.Container.GetOrAdd<Region>(this.ToRegionId, () => (Region)this.Entity.ToRegion.ToAdapter(this.Container)));

        Contract.Assume(this.toRegion != null);
        return this.toRegion;
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
      get { return (RegionId)Entity.ToRegionId; }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="fromId">
    /// The ID of the origin region.
    /// </param>
    /// <param name="toId">
    /// The ID of the destination region.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCacheKey(RegionId fromId, RegionId toId)
    {
      return (long)((((ulong)(long)fromId.GetHashCode()) << 32) | ((ulong)(long)toId.GetHashCode()));
    }

    /// <inheritdoc />
    public int CompareTo(RegionJump other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.FromRegion.Name.CompareTo(other.FromRegion.Name);

      if (result == 0)
      {
        result = this.ToRegion.Name.CompareTo(other.ToRegion.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as RegionJump);
    }

    /// <inheritdoc />
    public bool Equals(RegionJump other)
    {
      if (other == null)
      {
        return false;
      }

      return this.FromRegionId == other.FromRegionId && this.ToRegionId == other.ToRegionId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.FromRegionId, this.ToRegionId);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.FromRegion.Name + " to " + this.ToRegion.Name;
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class RegionJump : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      RegionJump other = obj as RegionJump;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class RegionJump : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return CreateCacheKey(this.FromRegionId, this.ToRegionId); }
    }
  }
  #endregion

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class RegionJump : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return CreateCacheKey(this.FromRegionId, this.ToRegionId); }
    }
  }
  #endregion
}