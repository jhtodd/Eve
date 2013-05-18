//-----------------------------------------------------------------------
// <copyright file="CorporateActivityEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CorporateActivityEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CorporateActivityEntityConfiguration : EntityTypeConfiguration<CorporateActivityEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CorporateActivityEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CorporateActivityEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("crpActivities"); m.MapInheritedProperties(); });
      this.HasKey(ca => ca.Id);
      
      // Column level mappings
      this.Property(ca => ca.Id).HasColumnName("activityID");
      this.Property(ca => ca.Name).HasColumnName("activityName");
      this.Property(ca => ca.Description).HasColumnName("description");

      // Relationship mappings
    }
  }
}