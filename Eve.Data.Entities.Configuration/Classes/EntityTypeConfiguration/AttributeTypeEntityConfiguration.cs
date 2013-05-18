//-----------------------------------------------------------------------
// <copyright file="AttributeTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AttributeTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AttributeTypeEntityConfiguration : EntityTypeConfiguration<AttributeTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AttributeTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AttributeTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("dgmAttributeTypes"); m.MapInheritedProperties(); });
      this.HasKey(at => at.Id);
      
      // Column level mappings
      this.Property(at => at.Id).HasColumnName("attributeID");
      this.Property(at => at.Name).HasColumnName("attributeName");
      this.Property(at => at.Description).HasColumnName("description");

      this.Property(at => at.CategoryId).HasColumnName("categoryID");
      this.Property(at => at.DefaultValue).HasColumnName("defaultValue");
      this.Property(at => at.DisplayName).HasColumnName("displayName");
      this.Property(at => at.HighIsGood).HasColumnName("highIsGood");
      this.Property(at => at.IconId).HasColumnName("iconID");
      this.Property(at => at.Published).HasColumnName("published");
      this.Property(at => at.Stackable).HasColumnName("stackable");
      this.Property(at => at.UnitId).HasColumnName("unitID");

      // Relationship mappings
      this.HasOptional(at => at.Category).WithMany().HasForeignKey(at => at.CategoryId);
      this.HasOptional(at => at.Icon).WithMany().HasForeignKey(at => at.IconId);
      this.HasOptional(at => at.Unit).WithMany().HasForeignKey(at => at.UnitId);
    }
  }
}