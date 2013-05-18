//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AssemblyLineTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AssemblyLineTypeEntityConfiguration : EntityTypeConfiguration<AssemblyLineTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AssemblyLineTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("ramAssemblyLineTypes"); m.MapInheritedProperties(); });
      this.HasKey(at => at.Id);
      
      // Column level mappings
      this.Property(alt => alt.Id).HasColumnName("assemblyLineTypeID");
      this.Property(alt => alt.Name).HasColumnName("assemblyLineTypeName");
      this.Property(alt => alt.Description).HasColumnName("description");

      this.Property(alt => alt.ActivityId).HasColumnName("activityID");
      this.Property(alt => alt.BaseMaterialMultiplier).HasColumnName("baseMaterialMultiplier");
      this.Property(alt => alt.BaseTimeMultiplier).HasColumnName("baseTimeMultiplier");
      this.Property(alt => alt.MinCostPerHour).HasColumnName("minCostPerHour");
      this.Property(alt => alt.Volume).HasColumnName("volume");

      // Relationship mappings
      this.HasRequired(alt => alt.Activity).WithMany().HasForeignKey(alt => alt.ActivityId);
      this.HasMany(alt => alt.CategoryDetails).WithRequired(altcd => altcd.AssemblyLineType).HasForeignKey(altcd => altcd.AssemblyLineTypeId);
      this.HasMany(alt => alt.GroupDetails).WithRequired(altgd => altgd.AssemblyLineType).HasForeignKey(altgd => altgd.AssemblyLineTypeId);
    }
  }
}