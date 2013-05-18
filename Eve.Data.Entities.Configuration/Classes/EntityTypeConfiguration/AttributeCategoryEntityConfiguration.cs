//-----------------------------------------------------------------------
// <copyright file="AttributeCategoryEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AttributeCategoryEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AttributeCategoryEntityConfiguration : EntityTypeConfiguration<AttributeCategoryEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AttributeCategoryEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AttributeCategoryEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("dgmAttributeCategories"); m.MapInheritedProperties(); });
      this.HasKey(a => a.Id);
      
      // Column level mappings
      this.Property(a => a.Id).HasColumnName("categoryID");
      this.Property(a => a.Name).HasColumnName("categoryName");
      this.Property(a => a.Description).HasColumnName("categoryDescription");

      // Relationship mappings
    }
  }
}