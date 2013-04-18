//-----------------------------------------------------------------------
// <copyright file="Constellation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// An EVE item describing an in-game constellation.
  /// </summary>
  public sealed class Constellation : Item
  {
    private Faction faction;
    private ReadOnlyConstellationJumpCollection jumps;
    private Region region;
    private ReadOnlySolarSystemCollection solarSystems;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Constellation class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Constellation(IEveRepository container, ItemEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsConstellation, "The entity must be a constellation.");
    }

    /* Properties */

    /// <summary>
    /// Gets the faction which holds sovereignty over the constellation, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Faction" /> which holds sovereignty over the 
    /// constellation, or <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public Faction Faction
    {
      get
      {
        if (this.FactionId == null)
        {
          return null;
        }

        if (this.faction != null)
        {
          return this.faction;
        }

        Contract.Assume(this.ConstellationInfo.Region.RegionInfo != null);
        ItemEntity factionEntity = this.ConstellationInfo.Faction ?? this.ConstellationInfo.Region.RegionInfo.Faction;

        if (factionEntity == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.faction = this.Container.GetOrAdd<Faction>(factionEntity.Id, () => (Faction)factionEntity.ToAdapter(this.Container));
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
        Contract.Assume(this.ConstellationInfo.Region != null);

        if (this.ConstellationInfo.FactionId.HasValue)
        {
          return (FactionId?)this.ConstellationInfo.FactionId;
        }

        Contract.Assume(this.ConstellationInfo.Region.RegionInfo != null);
        return (FactionId?)this.ConstellationInfo.Region.RegionInfo.FactionId;
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new ConstellationId Id
    {
      get { return (ConstellationId)base.Id.Value; }
    }

    /// <summary>
    /// Gets the collection of jumps connecting the constellation to other constellations.
    /// </summary>
    /// <value>
    /// The collection of jumps connecting the constellation to other constellations.
    /// </value>
    public ReadOnlyConstellationJumpCollection Jumps
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyConstellationJumpCollection>() != null);

        return this.jumps ?? (this.jumps = new ReadOnlyConstellationJumpCollection(this.Container.GetConstellationJumps(x => x.FromConstellationId == this.Id.Value).OrderBy(x => x)));
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

        double result = this.ConstellationInfo.Radius;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the region in which the constellation resides.
    /// </summary>
    /// <value>
    /// The region in which the constellation resides.
    /// </value>
    public Region Region
    {
      get
      {
        Contract.Ensures(Contract.Result<Region>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.region ?? (this.region = this.Container.GetOrAdd<Region>(this.RegionId, () => (Region)this.ConstellationInfo.Region.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the region in which the constellation resides.
    /// </summary>
    /// <value>
    /// The ID of the region in which the constellation resides.
    /// </value>
    public RegionId RegionId
    {
      get { return (RegionId)this.ConstellationInfo.RegionId; }
    }

    /// <summary>
    /// Gets the collection of solar systems in the constellation.
    /// </summary>
    /// <value>
    /// The collection of solar systems in the constellation.
    /// </value>
    public ReadOnlySolarSystemCollection SolarSystems
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySolarSystemCollection>() != null);

        return this.solarSystems ?? (this.solarSystems = new ReadOnlySolarSystemCollection(this.Container.GetSolarSystems(x => x.SolarSystemInfo.ConstellationId == this.Id.Value).OrderBy(x => x)));
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

        double result = this.ConstellationInfo.X;

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

        double result = this.ConstellationInfo.Y;

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

        double result = this.ConstellationInfo.Z;

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

        double result = this.ConstellationInfo.XMax;

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

        double result = this.ConstellationInfo.YMax;

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

        double result = this.ConstellationInfo.ZMax;

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

        double result = this.ConstellationInfo.XMin;

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

        double result = this.ConstellationInfo.YMin;

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

        double result = this.ConstellationInfo.ZMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    private ConstellationEntity ConstellationInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<ConstellationEntity>() != null);

        var result = this.Entity.ConstellationInfo;

        Contract.Assume(result != null);
        return result;
      }
    }
  }
}