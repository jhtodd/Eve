//-----------------------------------------------------------------------
// <copyright file="SolarSystemEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="SolarSystemEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class SolarSystemEntityConfiguration : EntityTypeConfiguration<SolarSystemEntity>
  {
    /// <summary>
    /// Initializes a new instance of the SolarSystemEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public SolarSystemEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapSolarSystems");
      this.HasKey(s => s.Id);
      
      // Column level mappings
      this.Property(s => s.Border).HasColumnName("border");
      this.Property(s => s.ConstellationBorder).HasColumnName("constellation");
      this.Property(s => s.ConstellationId).HasColumnName("constellationID");
      this.Property(s => s.Corridor).HasColumnName("corridor");
      this.Property(s => s.FactionId).HasColumnName("factionID");
      this.Property(s => s.Fringe).HasColumnName("fringe");
      this.Property(s => s.Hub).HasColumnName("hub");
      this.Property(s => s.Id).HasColumnName("solarSystemID");
      this.Property(s => s.International).HasColumnName("international");
      this.Property(s => s.Luminosity).HasColumnName("luminosity");
      this.Property(s => s.Radius).HasColumnName("radius");
      this.Property(s => s.Regional).HasColumnName("regional");
      this.Property(s => s.RegionId).HasColumnName("regionID");
      this.Property(s => s.Security).HasColumnName("security");
      this.Property(s => s.SecurityClass).HasColumnName("securityClass");
      this.Property(s => s.SolarSystemName).HasColumnName("solarSystemName");
      this.Property(s => s.SunTypeId).HasColumnName("sunTypeID");
      this.Property(s => s.X).HasColumnName("x");
      this.Property(s => s.XMax).HasColumnName("xMax");
      this.Property(s => s.XMin).HasColumnName("xMin");
      this.Property(s => s.Y).HasColumnName("y");
      this.Property(s => s.YMax).HasColumnName("yMax");
      this.Property(s => s.YMin).HasColumnName("yMin");
      this.Property(s => s.Z).HasColumnName("z");
      this.Property(s => s.ZMax).HasColumnName("zMax");
      this.Property(s => s.ZMin).HasColumnName("zMin");

      // Relationship mappings
      this.HasRequired(s => s.Constellation).WithMany(c => c.SolarSystems).HasForeignKey(s => s.ConstellationId);
      this.HasOptional(s => s.Faction).WithMany().HasForeignKey(s => s.FactionId);
      this.HasRequired(s => s.ItemInfo).WithOptional(i => i.SolarSystemInfo);
      this.HasMany(s => s.Jumps).WithRequired(sj => sj.FromSolarSystem).HasForeignKey(sj => sj.FromSolarSystemId);
      this.HasRequired(s => s.Region).WithMany().HasForeignKey(s => s.RegionId);
      this.HasMany(s => s.Stations).WithRequired(s => s.SolarSystem).HasForeignKey(s => s.SolarSystemId);
      this.HasRequired(s => s.SunType).WithMany().HasForeignKey(s => s.SunTypeId);
    }
  }
}