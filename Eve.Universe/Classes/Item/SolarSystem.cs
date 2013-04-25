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
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal SolarSystem(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsSolarSystem, "The entity must be a solar system.");

      // Use Assume instead of Requires to avoid lazy loading on release build
      Contract.Assert(this.Entity.SolarSystemInfo != null);
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
      get { return this.Entity.SolarSystemInfo.Border; }
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
        return this.LazyInitializeAdapter(ref this.constellation, this.Entity.SolarSystemInfo.ConstellationId, () => this.Entity.SolarSystemInfo.Constellation);
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
      get { return (ConstellationId)this.Entity.SolarSystemInfo.ConstellationId; }
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
      get { return this.Entity.SolarSystemInfo.ConstellationBorder; }
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
      get { return this.Entity.SolarSystemInfo.Corridor; }
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

        Contract.Assume(this.Entity.SolarSystemInfo.Constellation != null);
        Contract.Assume(this.Entity.SolarSystemInfo.Region != null);

        FactionEntity factionEntity = this.Entity.SolarSystemInfo.Faction ??
                                      this.Entity.SolarSystemInfo.Constellation.Faction ??
                                      this.Entity.SolarSystemInfo.Region.Faction;

        if (factionEntity == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.faction = this.Repository.GetOrAddStoredValue<Faction>(factionEntity.Id, () => (Faction)factionEntity.ItemInfo.ToAdapter(this.Repository));
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
        if (this.Entity.SolarSystemInfo.FactionId.HasValue)
        {
          return (FactionId?)this.Entity.SolarSystemInfo.FactionId;
        }

        Contract.Assume(this.Entity.SolarSystemInfo.Constellation != null);
        if (this.Entity.SolarSystemInfo.Constellation.FactionId.HasValue)
        {
          return (FactionId?)this.Entity.SolarSystemInfo.Constellation.FactionId;
        }

        Contract.Assume(this.Entity.SolarSystemInfo.Region != null);
        return (FactionId?)this.Entity.SolarSystemInfo.Region.FactionId;
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
      get { return this.Entity.SolarSystemInfo.Fringe; }
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
      get { return this.Entity.SolarSystemInfo.Hub; }
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
      get { return this.Entity.SolarSystemInfo.International; }
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

        return SolarSystem.LazyInitialize(
          ref this.jumps,
          () => new ReadOnlySolarSystemJumpCollection(this.Repository, this.Entity.SolarSystemInfo.Jumps));
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

        double result = this.Entity.SolarSystemInfo.Luminosity;

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

        double result = this.Entity.SolarSystemInfo.Radius;

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
        return this.LazyInitializeAdapter(ref this.region, this.Entity.SolarSystemInfo.RegionId, () => this.Entity.SolarSystemInfo.Region);
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
      get { return (RegionId)this.Entity.SolarSystemInfo.RegionId; }
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
      get { return this.Entity.SolarSystemInfo.Regional; }
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

        double result = this.Entity.SolarSystemInfo.Security;

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

        return this.Entity.SolarSystemInfo.SecurityClass ?? string.Empty;
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
        return this.LazyInitializeAdapter(ref this.sunType, this.Entity.SolarSystemInfo.SunTypeId, () => this.Entity.SolarSystemInfo.SunType);
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
      get { return this.Entity.SolarSystemInfo.SunTypeId; }
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

        double result = this.Entity.SolarSystemInfo.X;

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

        double result = this.Entity.SolarSystemInfo.Y;

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

        double result = this.Entity.SolarSystemInfo.Z;

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

        double result = this.Entity.SolarSystemInfo.XMax;

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

        double result = this.Entity.SolarSystemInfo.YMax;

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

        double result = this.Entity.SolarSystemInfo.ZMax;

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

        double result = this.Entity.SolarSystemInfo.XMin;

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

        double result = this.Entity.SolarSystemInfo.YMin;

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

        double result = this.Entity.SolarSystemInfo.ZMin;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /* Methods */

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.Entity.SolarSystemInfo != null);
    }
  }
}