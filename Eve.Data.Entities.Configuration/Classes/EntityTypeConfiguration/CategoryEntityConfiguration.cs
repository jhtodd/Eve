//-----------------------------------------------------------------------
// <copyright file="CategoryEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CategoryEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CategoryEntityConfiguration : EntityTypeConfiguration<CategoryEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CategoryEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CategoryEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("invCategories"); m.MapInheritedProperties(); });
      this.HasKey(c => c.Id);
      
      // Column level mappings
      this.Property(c => c.Id).HasColumnName("categoryID");
      this.Property(c => c.Name).HasColumnName("categoryName");
      this.Property(c => c.Description).HasColumnName("description");

      this.Property(c => c.IconId).HasColumnName("iconID");
      this.Property(c => c.Published).HasColumnName("published");

      // Relationship mappings
      this.HasMany(c => c.Groups).WithRequired(g => g.Category).HasForeignKey(g => g.CategoryId);
      this.HasOptional(c => c.Icon).WithMany().HasForeignKey(c => c.IconId);
    }
  }
}