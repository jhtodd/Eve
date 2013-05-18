//-----------------------------------------------------------------------
// <copyright file="GroupEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="GroupEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class GroupEntityConfiguration : EntityTypeConfiguration<GroupEntity>
  {
    /// <summary>
    /// Initializes a new instance of the GroupEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public GroupEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("invGroups"); m.MapInheritedProperties(); });
      this.HasKey(g => g.Id);
      
      // Column level mappings
      this.Property(g => g.Id).HasColumnName("groupID");
      this.Property(g => g.Name).HasColumnName("groupName");
      this.Property(g => g.Description).HasColumnName("description");

      this.Property(g => g.AllowManufacture).HasColumnName("allowManufacture");
      this.Property(g => g.AllowRecycler).HasColumnName("allowRecycler");
      this.Property(g => g.Anchorable).HasColumnName("anchorable");
      this.Property(g => g.Anchored).HasColumnName("anchored");
      this.Property(g => g.CategoryId).HasColumnName("categoryID");
      this.Property(g => g.FittableNonSingleton).HasColumnName("fittableNonSingleton");
      this.Property(g => g.IconId).HasColumnName("iconID");
      this.Property(g => g.Published).HasColumnName("published");
      this.Property(g => g.UseBasePrice).HasColumnName("useBasePrice");

      // Relationship mappings
      this.HasRequired(g => g.Category).WithMany(c => c.Groups).HasForeignKey(g => g.CategoryId);
      this.HasOptional(g => g.Icon).WithMany().HasForeignKey(g => g.IconId);
      this.HasMany(g => g.Types).WithRequired(et => et.Group).HasForeignKey(et => et.GroupId);
    }
  }
}