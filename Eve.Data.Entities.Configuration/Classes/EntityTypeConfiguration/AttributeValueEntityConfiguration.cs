//-----------------------------------------------------------------------
// <copyright file="AttributeValueEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AttributeValueEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AttributeValueEntityConfiguration : EntityTypeConfiguration<AttributeValueEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AttributeValueEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AttributeValueEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("dgmTypeAttributes");
      this.HasKey(a => new { a.AttributeId, a.ItemTypeId });
      
      // Column level mappings
      this.Property(a => a.AttributeId).HasColumnName("attributeID");
      this.Property(a => a.ItemTypeId).HasColumnName("typeID");
      this.Property(a => a.ValueFloat).HasColumnName("valueFloat");
      this.Property(a => a.ValueInt).HasColumnName("valueInt");

      // Relationship mappings
      this.HasRequired(a => a.AttributeType).WithMany().HasForeignKey(a => a.AttributeId);
      this.HasRequired(a => a.ItemType).WithMany().HasForeignKey(a => a.ItemTypeId);
    }
  }
}