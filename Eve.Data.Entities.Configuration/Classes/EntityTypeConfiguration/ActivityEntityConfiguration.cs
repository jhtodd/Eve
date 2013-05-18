//-----------------------------------------------------------------------
// <copyright file="ActivityEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ActivityEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ActivityEntityConfiguration : EntityTypeConfiguration<ActivityEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ActivityEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ActivityEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("ramActivities"); m.MapInheritedProperties(); });
      this.HasKey(a => a.Id);
      
      // Column level mappings
      this.Property(a => a.Id).HasColumnName("activityID");
      this.Property(a => a.Name).HasColumnName("activityName");
      this.Property(a => a.Description).HasColumnName("description");

      this.Property(a => a.IconNo).HasColumnName("iconNo");
      this.Property(a => a.Published).HasColumnName("published");

      // Relationship mappings
    }
  }
}