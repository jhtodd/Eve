//-----------------------------------------------------------------------
// <copyright file="ConstellationJumpEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ConstellationJumpEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ConstellationJumpEntityConfiguration : EntityTypeConfiguration<ConstellationJumpEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ConstellationJumpEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ConstellationJumpEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapConstellationJumps");
      this.HasKey(cj => new { cj.FromConstellationId, cj.ToConstellationId });
      
      // Column level mappings
      this.Property(cj => cj.FromConstellationId).HasColumnName("fromConstellationID");
      this.Property(cj => cj.FromRegionId).HasColumnName("fromRegionID");
      this.Property(cj => cj.ToConstellationId).HasColumnName("toConstellationID");
      this.Property(cj => cj.ToRegionId).HasColumnName("toRegionID");

      // Relationship mappings
      this.HasRequired(cj => cj.FromConstellation).WithMany(c => c.Jumps).HasForeignKey(cj => cj.FromConstellationId);
      this.HasRequired(cj => cj.FromRegion).WithMany().HasForeignKey(cj => cj.FromRegionId);
      this.HasRequired(cj => cj.ToConstellation).WithMany().HasForeignKey(cj => cj.ToConstellationId);
      this.HasRequired(cj => cj.ToRegion).WithMany().HasForeignKey(cj => cj.ToRegionId);
    }
  }
}