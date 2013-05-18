//-----------------------------------------------------------------------
// <copyright file="RegionEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="RegionEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class RegionEntityConfiguration : EntityTypeConfiguration<RegionEntity>
  {
    /// <summary>
    /// Initializes a new instance of the RegionEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public RegionEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapRegions");
      this.HasKey(r => r.Id);
      
      // Column level mappings
      this.Property(r => r.Id).HasColumnName("regionID");
      this.Property(r => r.FactionId).HasColumnName("factionID");
      this.Property(r => r.Radius).HasColumnName("radius");
      this.Property(r => r.RegionName).HasColumnName("regionName");
      this.Property(r => r.X).HasColumnName("x");
      this.Property(r => r.XMax).HasColumnName("xMax");
      this.Property(r => r.XMin).HasColumnName("xMin");
      this.Property(r => r.Y).HasColumnName("y");
      this.Property(r => r.YMax).HasColumnName("yMax");
      this.Property(r => r.YMin).HasColumnName("yMin");
      this.Property(r => r.Z).HasColumnName("z");
      this.Property(r => r.ZMax).HasColumnName("zMax");
      this.Property(r => r.ZMin).HasColumnName("zMin");

      // Relationship mappings
      this.HasMany(r => r.Constellations).WithRequired(c => c.Region).HasForeignKey(c => c.RegionId);
      this.HasOptional(r => r.Faction).WithMany().HasForeignKey(r => r.FactionId);
      this.HasRequired(r => r.ItemInfo).WithOptional(i => i.RegionInfo);
      this.HasMany(r => r.Jumps).WithRequired(rj => rj.FromRegion).HasForeignKey(rj => rj.FromRegionId);
    }
  }
}