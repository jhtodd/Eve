//-----------------------------------------------------------------------
// <copyright file="MetaGroupEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="MetaGroupEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class MetaGroupEntityConfiguration : EntityTypeConfiguration<MetaGroupEntity>
  {
    /// <summary>
    /// Initializes a new instance of the MetaGroupEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public MetaGroupEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("invMetaGroups"); m.MapInheritedProperties(); });
      this.HasKey(mg => mg.Id);
      
      // Column level mappings
      this.Property(mg => mg.Id).HasColumnName("metaGroupID");
      this.Property(mg => mg.Name).HasColumnName("metaGroupName");
      this.Property(mg => mg.Description).HasColumnName("description");

      this.Property(mg => mg.IconId).HasColumnName("iconID");

      // Relationship mappings
      this.HasOptional(mg => mg.Icon).WithMany().HasForeignKey(mg => mg.IconId);
    }
  }
}