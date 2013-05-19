//-----------------------------------------------------------------------
// <copyright file="BlueprintTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="BlueprintTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class BlueprintTypeEntityConfiguration : EntityTypeConfiguration<BlueprintTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the BlueprintTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public BlueprintTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invBlueprintTypes");
      
      // Column level mappings
      this.Property(bt => bt.MaterialModifier).HasColumnName("materialModifier");
      this.Property(bt => bt.MaxProductionLimit).HasColumnName("maxProductionLimit");
      this.Property(bt => bt.ParentBlueprintTypeId).HasColumnName("parentBlueprintTypeID");
      this.Property(bt => bt.ProductTypeId).HasColumnName("productTypeID");
      this.Property(bt => bt.ProductionTime).HasColumnName("productionTime");
      this.Property(bt => bt.ProductivityModifier).HasColumnName("productivityModifier");
      this.Property(bt => bt.ResearchCopyTime).HasColumnName("researchCopyTime");
      this.Property(bt => bt.ResearchMaterialTime).HasColumnName("researchMaterialTime");
      this.Property(bt => bt.ResearchProductivityTime).HasColumnName("researchProductivityTime");
      this.Property(bt => bt.ResearchTechTime).HasColumnName("researchTechTime");
      this.Property(bt => bt.TechLevel).HasColumnName("techLevel");
      this.Property(bt => bt.WasteFactor).HasColumnName("wasteFactor");

      // Relationship mappings
      this.HasOptional(bt => bt.ParentBlueprintType).WithMany().HasForeignKey(bt => bt.ParentBlueprintTypeId);
      this.HasMany(bt => bt.Materials).WithRequired(tm => tm.Type).HasForeignKey(tm => tm.TypeId);
      this.HasRequired(bt => bt.ProductType).WithMany().HasForeignKey(bt => bt.ProductTypeId);
    }
  }
}