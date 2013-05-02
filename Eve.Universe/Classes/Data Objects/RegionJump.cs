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
    : EveEntityAdapter<RegionJumpEntity, RegionJump>,
      IKeyItem<long>
  {
    private Region fromRegion;
    private Region toRegion;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the RegionJump class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal RegionJump(IEveRepository repository, RegionJumpEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
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
        return this.LazyInitializeAdapter(ref this.fromRegion, this.Entity.FromRegionId, () => this.Entity.FromRegion);
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
        return this.LazyInitializeAdapter(ref this.toRegion, this.Entity.ToRegionId, () => this.Entity.ToRegion);
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

    /// <inheritdoc />
    public override int CompareTo(RegionJump other)
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
    public override string ToString()
    {
      return this.FromRegion.Name + " to " + this.ToRegion.Name;
    }
  }

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class RegionJump : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return RegionJumpEntity.CreateCacheKey(this.FromRegionId, this.ToRegionId); }
    }
  }
  #endregion
}