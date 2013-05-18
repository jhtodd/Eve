//-----------------------------------------------------------------------
// <copyright file="ConstellationEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ConstellationEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ConstellationEntityConfiguration : EntityTypeConfiguration<ConstellationEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ConstellationEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ConstellationEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapConstellations");
      this.HasKey(c => c.Id);
      
      // Column level mappings
      this.Property(c => c.ConstellationName).HasColumnName("constellationName");
      this.Property(c => c.FactionId).HasColumnName("factionID");
      this.Property(c => c.Id).HasColumnName("constellationID");
      this.Property(c => c.Radius).HasColumnName("radius");
      this.Property(c => c.RegionId).HasColumnName("regionID");
      this.Property(c => c.X).HasColumnName("x");
      this.Property(c => c.XMax).HasColumnName("xMax");
      this.Property(c => c.XMin).HasColumnName("xMin");
      this.Property(c => c.Y).HasColumnName("y");
      this.Property(c => c.YMax).HasColumnName("yMax");
      this.Property(c => c.YMin).HasColumnName("yMin");
      this.Property(c => c.Z).HasColumnName("z");
      this.Property(c => c.ZMax).HasColumnName("zMax");
      this.Property(c => c.ZMin).HasColumnName("zMin");

      // Relationship mappings
      this.HasOptional(c => c.Faction).WithMany().HasForeignKey(c => c.FactionId);
      this.HasRequired(c => c.ItemInfo).WithOptional(i => i.ConstellationInfo);
      this.HasMany(c => c.Jumps).WithRequired(cj => cj.FromConstellation).HasForeignKey(cj => cj.FromConstellationId);
      this.HasRequired(c => c.Region).WithMany(r => r.Constellations).HasForeignKey(c => c.RegionId);
      this.HasMany(c => c.SolarSystems).WithRequired(s => s.Constellation).HasForeignKey(s => s.ConstellationId);
    }
  }
}