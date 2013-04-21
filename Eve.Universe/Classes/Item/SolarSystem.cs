//-----------------------------------------------------------------------
// <copyright file="SolarSystem.cs" company="Jeremy H. Todd">
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
  /// An EVE item describing an in-game solar system.
  /// </summary>
  public sealed class SolarSystem : Item
  {
    private Constellation constellation;
    private EveType sunType;
    private Faction faction;
    private ReadOnlySolarSystemJumpCollection jumps;
    private Region region;

    /// <summary>
    /// Initializes a new instance of the SolarSystem class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal SolarSystem(IEveRepository container, ItemEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsSolarSystem, "The entity must be a solar system.");
    }

    /* Properties */
    
    /// <summary>
    /// Gets a value indicating whether the solar system is adjacent to a solar
    /// system controlled by a different faction.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the solar system is adjacent to a solar system
    /// controlled by a different faction.; otherwise <see langword="false" />.
    /// </value>
    public bool Border
    {
      get { return this.SolarSystemInfo.Border; }
    }

    /// <summary>
    /// Gets the constellation in which the solar system resides.
    /// </summary>
    /// <value>
    /// The constellation in which the solar system resides.
    /// </value>
    public Constellation Constellation
    {
      get
      {
        Contract.Ensures(Contract.Result<Constellation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.constellation,
          () => this.Container.GetOrAdd<Constellation>(this.ConstellationId, () => (Constellation)this.SolarSystemInfo.Constellation.ToAdapter(this.Container)));

        Contract.Assume(this.constellation != null);
        return this.constellation;
      }
    }

    /// <summary>
    /// Gets the ID of the constellation in which the solar system resides.
    /// </summary>
    /// <value>
    /// The ID of the constellation in which the solar system resides.
    /// </value>
    public ConstellationId ConstellationId
    {
      get { return (ConstellationId)this.SolarSystemInfo.ConstellationId; }
    }

    /// <summary>
    /// Gets a value indicating whether the solar system is adjacent to a solar
    /// system in another constellation.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the solar system is adjacent to a solar
    /// system in another constellation; otherwise <see langword="false" />.
    /// </value>
    public bool ConstellationBorder
    {
      get { return this.SolarSystemInfo.ConstellationBorder; }
    }

    /// <summary>
    /// Gets a value indicating whether the solar system is a corridor system
    /// (i.e. has two jumps).
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the solar system is a corridor system;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Corridor
    {
      get { return this.SolarSystemInfo.Corridor; }
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
        if (this.FactionId == null)
        {
          return null;
        }

        if (this.faction != null)
        {
          return this.faction;
        }

        Contract.Assume(this.SolarSystemInfo.Constellation.ConstellationInfo != null);
        Contract.Assume(this.SolarSystemInfo.Region.RegionInfo != null);

        ItemEntity factionEntity = this.SolarSystemInfo.Faction ??
                                   this.SolarSystemInfo.Constellation.ConstellationInfo.Faction ??
                                   this.SolarSystemInfo.Region.RegionInfo.Faction;

        if (factionEntity == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.faction = this.Container.GetOrAdd<Faction>(factionEntity.Id, () => (Faction)factionEntity.ToAdapter(this.Container));
      }
    }

    /// <summary>
    /// Gets the ID of the faction which holds sovereignty over the solar system,
    /// if any.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Faction" /> which holds sovereignty over the
    /// solar system, or <see langword="null" /> if no faction holds sovereignty.
    /// </value>
    public FactionId? FactionId
    {
      get
      {
        Contract.Assume(this.SolarSystemInfo.Constellation != null);
        Contract.Assume(this.SolarSystemInfo.Region != null);

        if (this.SolarSystemInfo.FactionId.HasValue)
        {
          return (FactionId?)this.SolarSystemInfo.FactionId;
        }

        Contract.Assume(this.SolarSystemInfo.Constellation.ConstellationInfo != null);
        if (this.SolarSystemInfo.Constellation.ConstellationInfo.FactionId.HasValue)
        {
          return (FactionId?)this.SolarSystemInfo.Constellation.ConstellationInfo.FactionId;
        }

        Contract.Assume(this.SolarSystemInfo.Region.RegionInfo != null);
        return (FactionId?)this.SolarSystemInfo.Region.RegionInfo.FactionId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the solar system is a fringe system
    /// (i.e. has only one jump).
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the solar system is a fringe system;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Fringe
    {
      get { return this.SolarSystemInfo.Fringe; }
    }

    /// <summary>
    /// Gets a value indicating whether the solar system is a hub system
    /// (i.e. has at least three jumps).
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the solar system is a hub system;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Hub
    {
      get { return this.SolarSystemInfo.Hub; }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new SolarSystemId Id
    {
      get { return (SolarSystemId)base.Id.Value; }
    }

    /// <summary>
    /// Gets a value indicating whether the solar system contains assets belonging
    /// to more than one faction.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the solar system contains assets belonging
    /// to more than one faction; otherwise <see langword="false" />.
    /// </value>
    public bool International
    {
      get { return this.SolarSystemInfo.International; }
    }

    /// <summary>
    /// Gets the collection of jumps connecting the solar system to other solar systems.
    /// </summary>
    /// <value>
    /// The collection of jumps connecting the solar system to other solar systems.
    /// </value>
    public ReadOnlySolarSystemJumpCollection Jumps
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySolarSystemJumpCollection>() != null);

        LazyInitializer.EnsureInitialized(
          ref this.jumps,
          () => new ReadOnlySolarSystemJumpCollection(this.Container.GetSolarSystemJumps(q => q.Where(x => x.FromSolarSystemId == this.Id.Value)).OrderBy(x => x)));

        Contract.Assume(this.jumps != null);
        return this.jumps;
      }
    }

    /// <summary>
    /// Gets the luminosity of the solar system's star.
    /// </summary>
    /// <value>
    /// The luminosity of the solar system's star.
    /// </value>
    public double Luminosity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.SolarSystemInfo.Luminosity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
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

        double result = this.SolarSystemInfo.Radius;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the region in which the solar system resides.
    /// </summary>
    /// <value>
    /// The region in which the solar system resides.
    /// </value>
    public Region Region
    {
      get
      {
        Contract.Ensures(Contract.Result<Region>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.region,
          () => this.Container.GetOrAdd<Region>(this.RegionId, () => (Region)this.SolarSystemInfo.Region.ToAdapter(this.Container)));

        Contract.Assume(this.region != null);
        return this.region;
      }
    }

    /// <summary>
    /// Gets the ID of the region in which the solar system resides.
    /// </summary>
    /// <value>
    /// The ID of the region in which the solar system resides.
    /// </value>
    public RegionId RegionId
    {
      get { return (RegionId)this.SolarSystemInfo.RegionId; }
    }

    /// <summary>
    /// Gets a value indicating whether the solar system is adjacent to a solar
    /// system in another region.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the solar system is adjacent to a solar
    /// system in another region; otherwise <see langword="false" />.
    /// </value>
    public bool Regional
    {
      get { return this.SolarSystemInfo.Regional; }
    }

    /// <summary>
    /// Gets the security status of the solar system.
    /// </summary>
    /// <value>
    /// The security status of the solar system.
    /// </value>
    public double Security
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        double result = this.SolarSystemInfo.Security;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the security class of the solar system.
    /// </summary>
    /// <value>
    /// The security class of the solar system.
    /// </value>
    public string SecurityClass
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return this.SolarSystemInfo.SecurityClass ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the type of the solar system's star.
    /// </summary>
    /// <value>
    /// The type of the solar system's star.
    /// </value>
    public EveType SunType
    { // TODO: Update with specified type
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.sunType,
          () => this.Container.GetOrAdd<EveType>(this.SunTypeId, () => this.SolarSystemInfo.SunType.ToAdapter(this.Container)));

        Contract.Assume(this.sunType != null);
        return this.sunType;
      }
    }

    /// <summary>
    /// Gets the ID of the type of the solar system's star.
    /// </summary>
    /// <value>
    /// The ID of the type of the solar system's star.
    /// </value>
    public TypeId SunTypeId
    {
      get { return this.SolarSystemInfo.SunTypeId; }
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

        double result = this.SolarSystemInfo.X;

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

        double result = this.SolarSystemInfo.Y;

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

        double result = this.SolarSystemInfo.Z;

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

        double result = this.SolarSystemInfo.XMax;

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

        double result = this.SolarSystemInfo.YMax;

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

        double result = this.SolarSystemInfo.ZMax;

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

        double result = this.SolarSystemInfo.XMin;

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

        double result = this.SolarSystemInfo.YMin;

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

        double result = this.SolarSystemInfo.ZMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    private SolarSystemEntity SolarSystemInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<SolarSystemEntity>() != null);

        var result = this.Entity.SolarSystemInfo;

        Contract.Assume(result != null);
        return result;
      }
    }
  }
}