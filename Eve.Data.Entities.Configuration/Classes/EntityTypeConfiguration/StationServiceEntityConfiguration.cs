//-----------------------------------------------------------------------
// <copyright file="StationServiceEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="StationServiceEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class StationServiceEntityConfiguration : EntityTypeConfiguration<StationServiceEntity>
  {
    /// <summary>
    /// Initializes a new instance of the StationServiceEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public StationServiceEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("staServices"); m.MapInheritedProperties(); });
      this.HasKey(ss => ss.Id);
      
      // Column level mappings
      this.Property(ss => ss.Id).HasColumnName("serviceID");
      this.Property(ss => ss.Name).HasColumnName("serviceName");
      this.Property(ss => ss.Description).HasColumnName("description");

      // Relationship mappings
    }
  }
}