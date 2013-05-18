//-----------------------------------------------------------------------
// <copyright file="Region.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// An EVE item describing an in-game region.
  /// </summary>
  public sealed class Region : Item
  {
    private ReadOnlyConstellationCollection constellations;
    private Faction faction;
    private ReadOnlyRegionJumpCollection jumps;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Region class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Region(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsRegion, "The entity must be a region.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of constellations in the region.
    /// </summary>
    /// <value>
    /// The collection of constellations in the region.
    /// </value>
    public ReadOnlyConstellationCollection Constellations
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyConstellationCollection>() != null);

        return Region.LazyInitialize(
          ref this.constellations,
          () => ReadOnlyConstellationCollection.Create(this.Repository, this.RegionInfo.Constellations));
      }
    }

    /// <summary>
    /// Gets the faction which holds sovereignty over the region, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Faction" /> which holds sovereignty over the region, or
    /// <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public Faction Faction
    {
      get
      {
        Contract.Ensures(this.FactionId == null || Contract.Result<Faction>() != null);

        if (this.FactionId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.faction, this.RegionInfo.FactionId, () => this.RegionInfo.Faction);
      }
    }

    /// <summary>
    /// Gets the ID of the faction which holds sovereignty over the region, if any.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Faction" /> which holds sovereignty over the
    /// region, or <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public FactionId? FactionId
    {
      get 
      {
        return (FactionId?)this.RegionInfo.FactionId;
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new RegionId Id
    {
      get { return (RegionId)base.Id.Value; }
    }

    /// <summary>
    /// Gets the collection of jumps connecting the region to other regions.
    /// </summary>
    /// <value>
    /// The collection of jumps connecting the region to other regions.
    /// </value>
    public ReadOnlyRegionJumpCollection Jumps
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyRegionJumpCollection>() != null);

        return Region.LazyInitialize(
          ref this.jumps,
          () => ReadOnlyRegionJumpCollection.Create(this.Repository, this.RegionInfo.Jumps));
      }
    }

    /// <summary>
    /// Gets the radius of the item.
    /// </summary>
    /// <value>
    /// The radius of the item.
    /// </value>
    public double Radius
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.RegionInfo.Radius;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the X component of the item's location.
    /// </summary>
    /// <value>
    /// The X component of the item's location.
    /// </value>
    public double X
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.X;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Y component of the item's location.
    /// </summary>
    /// <value>
    /// The Y component of the item's location.
    /// </value>
    public double Y
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.Y;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Z component of the item's location.
    /// </summary>
    /// <value>
    /// The Z component of the item's location.
    /// </value>
    public double Z
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.Z;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the maximum extent of the item in the X direction.
    /// </summary>
    /// <value>
    /// The maximum extent of the item in the X direction.
    /// </value>
    public double XMax
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.XMax;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the maximum extent of the item in the Y direction.
    /// </summary>
    /// <value>
    /// The maximum extent of the item in the Y direction.
    /// </value>
    public double YMax
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.YMax;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the maximum extent of the item in the Z direction.
    /// </summary>
    /// <value>
    /// The maximum extent of the item in the Z direction.
    /// </value>
    public double ZMax
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.ZMax;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the minimum extent of the item in the X direction.
    /// </summary>
    /// <value>
    /// The minimum extent of the item in the X direction.
    /// </value>
    public double XMin
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.XMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the minimum extent of the item in the Y direction.
    /// </summary>
    /// <value>
    /// The minimum extent of the item in the Y direction.
    /// </value>
    public double YMin
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.YMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the minimum extent of the item in the Z direction.
    /// </summary>
    /// <value>
    /// The minimum extent of the item in the Z direction.
    /// </value>
    public double ZMin
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));

        double result = this.RegionInfo.ZMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the <see cref="RegionEntity" /> containing the specific
    /// information for the current item type.
    /// </summary>
    /// <value>
    /// The <see cref="RegionEntity" /> containing the specific
    /// information for the current item type.
    /// </value>
    private RegionEntity RegionInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<RegionEntity>() != null);

        var result = this.Entity.RegionInfo;

        Contract.Assume(result != null);
        return result;
      }
    }
  }
}