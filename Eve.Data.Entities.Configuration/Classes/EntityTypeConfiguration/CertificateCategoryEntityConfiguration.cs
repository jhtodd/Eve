//-----------------------------------------------------------------------
// <copyright file="CertificateCategoryEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CertificateCategoryEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CertificateCategoryEntityConfiguration : EntityTypeConfiguration<CertificateCategoryEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CertificateCategoryEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CertificateCategoryEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("crtCategories"); m.MapInheritedProperties(); });
      this.HasKey(cc => cc.Id);
      
      // Column level mappings
      this.Property(cc => cc.Id).HasColumnName("categoryID");
      this.Property(cc => cc.Name).HasColumnName("categoryName");
      this.Property(cc => cc.Description).HasColumnName("description");

      // Relationship mappings
    }
  }
}