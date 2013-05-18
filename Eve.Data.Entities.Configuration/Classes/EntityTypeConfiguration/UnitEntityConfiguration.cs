//-----------------------------------------------------------------------
// <copyright file="UnitEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="UnitEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class UnitEntityConfiguration : EntityTypeConfiguration<UnitEntity>
  {
    /// <summary>
    /// Initializes a new instance of the UnitEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public UnitEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("eveUnits"); m.MapInheritedProperties(); });
      this.HasKey(u => u.Id);
      
      // Column level mappings
      this.Property(u => u.Id).HasColumnName("unitID");
      this.Property(u => u.Name).HasColumnName("unitName");
      this.Property(u => u.Description).HasColumnName("description");

      this.Property(u => u.DisplayName).HasColumnName("displayName");

      // Relationship mappings
    }
  }
}