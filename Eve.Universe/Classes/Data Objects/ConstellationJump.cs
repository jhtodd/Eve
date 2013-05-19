//-----------------------------------------------------------------------
// <copyright file="ConstellationJump.cs" company="Jeremy H. Todd">
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
  /// Contains information about a jump link between constellations.
  /// </summary>
  public sealed partial class ConstellationJump : EveEntityAdapter<ConstellationJumpEntity, ConstellationJump>
  {
    private Constellation fromConstellation;
    private Region fromRegion;
    private Constellation toConstellation;
    private Region toRegion;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ConstellationJump class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal ConstellationJump(IEveRepository repository, ConstellationJumpEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
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
        return this.LazyInitializeAdapter(ref this.fromConstellation, this.Entity.FromConstellationId, () => this.Entity.FromConstellation);
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
        return this.LazyInitializeAdapter(ref this.toConstellation, this.Entity.ToConstellationId, () => this.Entity.ToConstellation);
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
      get { return Entity.ToRegionId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(ConstellationJump other)
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
    public override string ToString()
    {
      return this.FromConstellation.Name + " to " + this.ToConstellation.Name;
    }
  }
}