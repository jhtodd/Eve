//-----------------------------------------------------------------------
// <copyright file="TypeMaterialEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="TypeMaterialEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class TypeMaterialEntityConfiguration : EntityTypeConfiguration<TypeMaterialEntity>
  {
    /// <summary>
    /// Initializes a new instance of the TypeMaterialEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public TypeMaterialEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invTypeMaterials");
      this.HasKey(tm => new { tm.TypeId, tm.MaterialTypeId });
      
      // Column level mappings
      this.Property(tm => tm.MaterialTypeId).HasColumnName("materialTypeID");
      this.Property(tm => tm.Quantity).HasColumnName("quantity");
      this.Property(tm => tm.TypeId).HasColumnName("typeID");

      // Relationship mappings
      this.HasRequired(tm => tm.MaterialType).WithMany().HasForeignKey(tm => tm.MaterialTypeId);
      this.HasRequired(tm => tm.Type).WithMany(et => et.Materials).HasForeignKey(tm => tm.TypeId);
    }
  }
}