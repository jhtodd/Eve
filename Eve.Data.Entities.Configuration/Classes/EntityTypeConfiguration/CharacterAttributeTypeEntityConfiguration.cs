//-----------------------------------------------------------------------
// <copyright file="CharacterAttributeTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CharacterAttributeTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CharacterAttributeTypeEntityConfiguration : EntityTypeConfiguration<CharacterAttributeTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CharacterAttributeTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CharacterAttributeTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("chrAttributes"); m.MapInheritedProperties(); });
      this.HasKey(cat => cat.Id);
      
      // Column level mappings
      this.Property(cat => cat.Id).HasColumnName("attributeID");
      this.Property(cat => cat.Name).HasColumnName("attributeName");
      this.Property(cat => cat.Description).HasColumnName("description");

      this.Property(cat => cat.IconId).HasColumnName("iconID");
      this.Property(cat => cat.Notes).HasColumnName("notes");
      this.Property(cat => cat.ShortDescription).HasColumnName("shortDescription");

      // Relationship mappings
      this.HasRequired(cat => cat.Icon).WithMany().HasForeignKey(cat => cat.IconId);
    }
  }
}