//-----------------------------------------------------------------------
// <copyright file="SolarSystemJumpEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="SolarSystemJumpEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class SolarSystemJumpEntityConfiguration : EntityTypeConfiguration<SolarSystemJumpEntity>
  {
    /// <summary>
    /// Initializes a new instance of the SolarSystemJumpEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public SolarSystemJumpEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapSolarSystemJumps");
      this.HasKey(sj => new { sj.FromSolarSystemId, sj.ToSolarSystemId });
      
      // Column level mappings
      this.Property(sj => sj.FromConstellationId).HasColumnName("fromConstellationID");
      this.Property(sj => sj.FromRegionId).HasColumnName("fromRegionID");
      this.Property(sj => sj.FromSolarSystemId).HasColumnName("fromSolarSystemID");
      this.Property(sj => sj.ToConstellationId).HasColumnName("toConstellationID");
      this.Property(sj => sj.ToRegionId).HasColumnName("toRegionID");
      this.Property(sj => sj.ToSolarSystemId).HasColumnName("toSolarSystemID");

      // Relationship mappings
      this.HasRequired(sj => sj.FromConstellation).WithMany().HasForeignKey(sj => sj.FromConstellationId);
      this.HasRequired(sj => sj.FromRegion).WithMany().HasForeignKey(sj => sj.FromRegionId);
      this.HasRequired(sj => sj.FromSolarSystem).WithMany(s => s.Jumps).HasForeignKey(sj => sj.FromSolarSystemId);
      this.HasRequired(sj => sj.ToConstellation).WithMany().HasForeignKey(sj => sj.ToConstellationId);
      this.HasRequired(sj => sj.ToRegion).WithMany().HasForeignKey(sj => sj.ToRegionId);
      this.HasRequired(sj => sj.ToSolarSystem).WithMany().HasForeignKey(sj => sj.ToSolarSystemId);
    }
  }
}