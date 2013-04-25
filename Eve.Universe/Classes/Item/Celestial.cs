//-----------------------------------------------------------------------
// <copyright file="Celestial.cs" company="Jeremy H. Todd">
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
  /// An EVE item describing an in-game region.
  /// </summary>
  public sealed class Celestial : Item
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Celestial class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Celestial(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsCelestial, "The entity must be a celestial object.");

      // Use Assume instead of Requires to avoid lazy loading on release build
      Contract.Assert(this.Entity.CelestialInfo != null);
    }

    /* Properties */

    /// <summary>
    /// Gets the age of the celestial object, in seconds.  Appears to apply
    /// only to stars.
    /// </summary>
    /// <value>
    /// The age of the celestial object, in seconds.
    /// </value>
    public double Age
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Age;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the density of the celestial object, in kilograms per cubic meter.
    /// </summary>
    /// <value>
    /// The density of the celestial object, in kilograms per cubic meter.
    /// </value>
    public double Density
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Density;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the orbital eccentricity of the celestial object.
    /// </summary>
    /// <value>
    /// The orbital eccentricity of the celestial object.
    /// </value>
    public double Eccentricity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Eccentricity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the escape velocity of the celestial object, in meters per second.
    /// </summary>
    /// <value>
    /// The escape velocity of the celestial object, in meters per second.
    /// </value>
    public double EscapeVelocity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.EscapeVelocity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the celestial object is fragmented,
    /// e.g. an asteroid belt.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the object is fragmented; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool Fragmented
    {
      get 
      {
        return this.Entity.CelestialInfo == null ? false : this.Entity.CelestialInfo.Fragmented; 
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new CelestialId Id
    {
      get { return (CelestialId)base.Id.Value; }
    }

    /// <summary>
    /// Gets a value indicating the amount of life in a solar system.
    /// Appears to apply only to stars.
    /// </summary>
    /// <value>
    /// A value indicating the amount of life in a solar system.
    /// </value>
    public double Life
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Life;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the celestial object is tidally
    /// locked to its parent.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the object is tidally locked;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Locked
    {
      get 
      {
        return this.Entity.CelestialInfo == null ? false : this.Entity.CelestialInfo.Locked; 
      }
    }

    /// <summary>
    /// Gets the luminosity of the celestial object, relative to the sun.
    /// Applies only to stars.
    /// </summary>
    /// <value>
    /// The luminosity of the star.
    /// </value>
    public double Luminosity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Luminosity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the mass of the object (?).  This property appears to be unused
    /// and is always zero.
    /// </summary>
    /// <value>
    /// The mass of the object.  This property appears to be unused.
    /// </value>
    public double Mass
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() == 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Mass;
        Contract.Assume(result == 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the mass of the solid portion of a celestial object.  This
    /// applies only to planets and moons.
    /// </summary>
    /// <value>
    /// The mass of the solid portion of a celestial object.
    /// </value>
    public double MassDust
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.MassDust;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the mass of the atmosphere of a celestial object.  This
    /// applies only to planets and moons.
    /// </summary>
    /// <value>
    /// The mass of the atmosphere of a celestial object.
    /// </value>
    public double MassGas
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.MassGas;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the orbital period of the celestial object, in tenths of a
    /// second.
    /// </summary>
    /// <value>
    /// The orbital period of the celestial object, in tenths of a
    /// second.
    /// </value>
    public double OrbitPeriod
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.OrbitPeriod;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the orbital radius (semi-major axis) of the celestial 
    /// object's orbit, in meters.
    /// </summary>
    /// <value>
    /// The orbital radius, in meters.
    /// </value>
    public double OrbitRadius
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.OrbitRadius;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the atmospheric pressure of the celestial object, in pascals.
    /// </summary>
    /// <value>
    /// The atmospheric pressure of the celestial object, in pascals.
    /// </value>
    public double Pressure
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Pressure;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the radius of the celestial object, in meters.
    /// </summary>
    /// <value>
    /// The radius of the celestial object, in meters.
    /// </value>
    public double Radius
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Radius;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the celestial object's period of rotation, in seconds.
    /// </summary>
    /// <value>
    /// The celestial object's period of rotation, in seconds.
    /// </value>
    public double RotationRate
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.RotationRate;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the spectral class of the celestial object.  Applies only to
    /// stars.
    /// </summary>
    /// <value>
    /// The spectral class of the celestial object.
    /// </value>
    public string SpectralClass
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        var result = this.Entity.CelestialInfo == null ? string.Empty : this.Entity.CelestialInfo.SpectralClass;
        Contract.Assume(result != null);
        return result;
      }
    }

    /// <summary>
    /// Gets the surface gravity of the celestial object, in meters per second 
    /// squared.
    /// </summary>
    /// <value>
    /// The surface gravity of the celestial object, in meters per second
    /// squared.
    /// </value>
    public double SurfaceGravity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.SurfaceGravity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the surface temperature of the celestial object, in kelvins.
    /// </summary>
    /// <value>
    /// The surface temperature of the celestial object, in kelvins.
    /// </value>
    public double Temperature
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        var result = this.Entity.CelestialInfo == null ? 0.0D : this.Entity.CelestialInfo.Temperature;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /* Methods */

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.Entity.CelestialInfo != null);
    }
  }
}