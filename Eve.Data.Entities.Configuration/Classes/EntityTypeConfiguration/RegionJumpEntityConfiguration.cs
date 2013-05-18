//-----------------------------------------------------------------------
// <copyright file="RegionJumpEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="RegionJumpEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class RegionJumpEntityConfiguration : EntityTypeConfiguration<RegionJumpEntity>
  {
    /// <summary>
    /// Initializes a new instance of the RegionJumpEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public RegionJumpEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapRegionJumps");
      this.HasKey(rj => new { rj.FromRegionId, rj.ToRegionId });
      
      // Column level mappings
      this.Property(rj => rj.FromRegionId).HasColumnName("fromRegionID");
      this.Property(rj => rj.ToRegionId).HasColumnName("toRegionID");

      // Relationship mappings
      this.HasRequired(rj => rj.FromRegion).WithMany(r => r.Jumps).HasForeignKey(rj => rj.FromRegionId);
      this.HasRequired(rj => rj.ToRegion).WithMany().HasForeignKey(rj => rj.ToRegionId);
    }
  }
}