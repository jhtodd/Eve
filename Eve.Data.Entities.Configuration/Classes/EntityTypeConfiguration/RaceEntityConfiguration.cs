//-----------------------------------------------------------------------
// <copyright file="RaceEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="RaceEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class RaceEntityConfiguration : EntityTypeConfiguration<RaceEntity>
  {
    /// <summary>
    /// Initializes a new instance of the RaceEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public RaceEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("chrRaces"); m.MapInheritedProperties(); });
      this.HasKey(r => r.Id);
      
      // Column level mappings
      this.Property(r => r.Id).HasColumnName("raceID");
      this.Property(r => r.Name).HasColumnName("raceName");
      this.Property(r => r.Description).HasColumnName("description");

      this.Property(r => r.IconId).HasColumnName("iconID");
      this.Property(r => r.ShortDescription).HasColumnName("shortDescription");

      // Relationship mappings
      this.HasMany(r => r.Bloodlines).WithRequired(b => b.Race).HasForeignKey(b => b.RaceId);
      this.HasOptional(r => r.Icon).WithMany().HasForeignKey(r => r.IconId);
    }
  }
}