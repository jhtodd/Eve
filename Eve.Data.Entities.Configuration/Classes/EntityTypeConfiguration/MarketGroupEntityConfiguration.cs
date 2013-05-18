//-----------------------------------------------------------------------
// <copyright file="MarketGroupEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="MarketGroupEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class MarketGroupEntityConfiguration : EntityTypeConfiguration<MarketGroupEntity>
  {
    /// <summary>
    /// Initializes a new instance of the MarketGroupEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public MarketGroupEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("invMarketGroups"); m.MapInheritedProperties(); });
      this.HasKey(mg => mg.Id);
      
      // Column level mappings
      this.Property(mg => mg.Id).HasColumnName("marketGroupID");
      this.Property(mg => mg.Name).HasColumnName("marketGroupName");
      this.Property(mg => mg.Description).HasColumnName("description");

      this.Property(mg => mg.HasTypes).HasColumnName("hasTypes");
      this.Property(mg => mg.IconId).HasColumnName("iconID");
      this.Property(mg => mg.ParentGroupId).HasColumnName("parentGroupID");

      // Relationship mappings
      this.HasMany(mg => mg.ChildGroups).WithOptional(mg => mg.ParentGroup).HasForeignKey(mg => mg.ParentGroupId);
      this.HasOptional(mg => mg.Icon).WithMany().HasForeignKey(mg => mg.IconId);
      this.HasMany(mg => mg.Types).WithOptional(et => et.MarketGroup).HasForeignKey(et => et.MarketGroupId);
    }
  }
}