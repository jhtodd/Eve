//-----------------------------------------------------------------------
// <copyright file="StargateEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="StargateEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class StargateEntityConfiguration : EntityTypeConfiguration<StargateEntity>
  {
    /// <summary>
    /// Initializes a new instance of the StargateEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public StargateEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapJumps");
      this.HasKey(s => s.Id);
      
      // Column level mappings
      this.Property(s => s.DestinationId).HasColumnName("celestialID");
      this.Property(s => s.Id).HasColumnName("stargateID");

      // Relationship mappings
      this.HasRequired(s => s.Destination).WithMany().HasForeignKey(s => s.DestinationId);
      this.HasRequired(s => s.ItemInfo).WithOptional(i => i.StargateInfo);
    }
  }
}