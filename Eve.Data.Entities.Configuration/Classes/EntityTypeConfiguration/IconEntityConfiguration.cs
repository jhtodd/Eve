//-----------------------------------------------------------------------
// <copyright file="IconEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="IconEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class IconEntityConfiguration : EntityTypeConfiguration<IconEntity>
  {
    /// <summary>
    /// Initializes a new instance of the IconEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public IconEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("eveIcons"); m.MapInheritedProperties(); });
      this.HasKey(d => d.Id);
      
      // Column level mappings
      this.Property(d => d.Id).HasColumnName("iconID");
      this.Property(d => d.Name).HasColumnName("iconFile");
      this.Property(d => d.Description).HasColumnName("description");

      // Relationship mappings
    }
  }
}