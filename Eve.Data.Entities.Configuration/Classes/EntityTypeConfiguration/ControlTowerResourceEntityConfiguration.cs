//-----------------------------------------------------------------------
// <copyright file="ControlTowerResourceEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ControlTowerResourceEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ControlTowerResourceEntityConfiguration : EntityTypeConfiguration<ControlTowerResourceEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ControlTowerResourceEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ControlTowerResourceEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invControlTowerResources");
      this.HasKey(ctr => new { ctr.ControlTowerTypeId, ctr.ResourceTypeId });
      
      // Column level mappings
      this.Property(ctr => ctr.ControlTowerTypeId).HasColumnName("controlTowerTypeID");
      this.Property(ctr => ctr.FactionId).HasColumnName("factionID");
      this.Property(ctr => ctr.MinimumSecurityLevel).HasColumnName("minSecurityLevel");
      this.Property(ctr => ctr.Purpose).HasColumnName("purpose");
      this.Property(ctr => ctr.Quantity).HasColumnName("quantity");
      this.Property(ctr => ctr.ResourceTypeId).HasColumnName("resourceTypeID");

      // Relationship mappings
      this.HasRequired(ctr => ctr.ControlTowerType).WithMany().HasForeignKey(ctr => ctr.ControlTowerTypeId);
      this.HasOptional(ctr => ctr.Faction).WithMany().HasForeignKey(ctr => ctr.FactionId);
      this.HasRequired(ctr => ctr.ResourceType).WithMany().HasForeignKey(ctr => ctr.ResourceTypeId);
    }
  }
}