//-----------------------------------------------------------------------
// <copyright file="DivisionEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="DivisionEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class DivisionEntityConfiguration : EntityTypeConfiguration<DivisionEntity>
  {
    /// <summary>
    /// Initializes a new instance of the DivisionEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public DivisionEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("crpNPCDivisions"); m.MapInheritedProperties(); });
      this.HasKey(d => d.Id);
      
      // Column level mappings
      this.Property(d => d.Id).HasColumnName("divisionID");
      this.Property(d => d.Name).HasColumnName("divisionName");
      this.Property(d => d.Description).HasColumnName("description");

      this.Property(d => d.LeaderType).HasColumnName("leaderType");

      // Relationship mappings
    }
  }
}