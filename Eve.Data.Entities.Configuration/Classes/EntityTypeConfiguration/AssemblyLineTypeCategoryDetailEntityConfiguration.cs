//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeCategoryDetailEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AssemblyLineTypeCategoryDetailEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AssemblyLineTypeCategoryDetailEntityConfiguration : EntityTypeConfiguration<AssemblyLineTypeCategoryDetailEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeCategoryDetailEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AssemblyLineTypeCategoryDetailEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("ramAssemblyLineTypeDetailPerCategory");
      this.HasKey(altcd => new { altcd.AssemblyLineTypeId, altcd.CategoryId });
      
      // Column level mappings
      this.Property(altcd => altcd.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");
      this.Property(altcd => altcd.CategoryId).HasColumnName("categoryID");
      this.Property(altcd => altcd.MaterialMultiplier).HasColumnName("materialMultiplier");
      this.Property(altcd => altcd.TimeMultiplier).HasColumnName("timeMultiplier");

      // Relationship mappings
      this.HasRequired(altcd => altcd.AssemblyLineType).WithMany(alt => alt.CategoryDetails).HasForeignKey(altcd => altcd.AssemblyLineTypeId);
      this.HasRequired(altcd => altcd.Category).WithMany().HasForeignKey(altcd => altcd.CategoryId);
    }
  }
}