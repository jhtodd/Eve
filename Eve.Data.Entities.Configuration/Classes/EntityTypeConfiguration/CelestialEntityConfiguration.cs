//-----------------------------------------------------------------------
// <copyright file="CelestialEntityConfiguration.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities.Configuration
{
  using System.Data.Entity.ModelConfiguration;
  using System.Diagnostics.Contracts;

  using FreeNet.Data.Entity.Configuration;

  /// <summary>
  /// Contains Entity Framework configuration options for the
  /// <see cref="CelestialEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CelestialEntityConfiguration : EntityTypeConfiguration<CelestialEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CelestialEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CelestialEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapCelestialStatistics");
      this.HasKey(a => a.Id);
      
      // Column level mappings
      this.Property(c => c.Age).HasColumnName("age");
      this.Property(c => c.Density).HasColumnName("density");
      this.Property(c => c.Eccentricity).HasColumnName("eccentricity");
      this.Property(c => c.EscapeVelocity).HasColumnName("escapeVelocity");
      this.Property(c => c.Fragmented).HasColumnName("fragmented");
      this.Property(c => c.Id).HasColumnName("celestialID");
      this.Property(c => c.Life).HasColumnName("life");
      this.Property(c => c.Locked).HasColumnName("locked");
      this.Property(c => c.Luminosity).HasColumnName("luminosity");
      this.Property(c => c.Mass).HasColumnName("mass");
      this.Property(c => c.MassDust).HasColumnName("massDust");
      this.Property(c => c.MassGas).HasColumnName("massGas");
      this.Property(c => c.OrbitPeriod).HasColumnName("orbitPeriod");
      this.Property(c => c.OrbitRadius).HasColumnName("orbitRadius");
      this.Property(c => c.Pressure).HasColumnName("pressure");
      this.Property(c => c.Radius).HasColumnName("radius");
      this.Property(c => c.RotationRate).HasColumnName("rotationRate");
      this.Property(c => c.SpectralClass).HasColumnName("spectralClass");
      this.Property(c => c.SurfaceGravity).HasColumnName("surfaceGravity");
      this.Property(c => c.Temperature).HasColumnName("temperature");

      // Relationship mappings
      this.HasRequired(c => c.ItemInfo).WithOptional(i => i.CelestialInfo);
    }
  }
}