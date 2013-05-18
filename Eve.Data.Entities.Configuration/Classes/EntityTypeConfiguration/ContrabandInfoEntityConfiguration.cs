//-----------------------------------------------------------------------
// <copyright file="ContrabandInfoEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ContrabandInfoEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ContrabandInfoEntityConfiguration : EntityTypeConfiguration<ContrabandInfoEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ContrabandInfoEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ContrabandInfoEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invContrabandTypes");
      this.HasKey(ci => new { ci.FactionId, ci.TypeId });
      
      // Column level mappings
      this.Property(ci => ci.AttackMinimumSecurity).HasColumnName("attackMinSec");
      this.Property(ci => ci.ConfiscateMinimumSecurity).HasColumnName("confiscateMinSec");
      this.Property(ci => ci.FactionId).HasColumnName("factionID");
      this.Property(ci => ci.FineByValue).HasColumnName("fineByValue");
      this.Property(ci => ci.StandingLoss).HasColumnName("standingLoss");
      this.Property(ci => ci.TypeId).HasColumnName("typeID");

      // Relationship mappings
      this.HasRequired(ci => ci.Faction).WithMany().HasForeignKey(ci => ci.FactionId);
      this.HasRequired(ci => ci.Type).WithMany(et => et.ContrabandInfo).HasForeignKey(ci => ci.TypeId);
    }
  }
}