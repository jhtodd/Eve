//-----------------------------------------------------------------------
// <copyright file="FlagEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="FlagEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class FlagEntityConfiguration : EntityTypeConfiguration<FlagEntity>
  {
    /// <summary>
    /// Initializes a new instance of the FlagEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public FlagEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("invFlags"); m.MapInheritedProperties(); });
      this.HasKey(f => f.Id);
      
      // Column level mappings
      this.Property(f => f.Id).HasColumnName("flagID");
      this.Property(f => f.Name).HasColumnName("flagName");
      this.Property(f => f.Description).HasColumnName("flagText");

      this.Property(f => f.OrderId).HasColumnName("orderID");

      // Relationship mappings
    }
  }
}