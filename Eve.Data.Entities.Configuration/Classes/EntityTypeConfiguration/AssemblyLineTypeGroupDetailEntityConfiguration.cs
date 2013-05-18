//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeGroupDetailEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AssemblyLineTypeGroupDetailEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AssemblyLineTypeGroupDetailEntityConfiguration : EntityTypeConfiguration<AssemblyLineTypeGroupDetailEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeGroupDetailEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AssemblyLineTypeGroupDetailEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("ramAssemblyLineTypeDetailPerGroup");
      this.HasKey(altgd => new { altgd.AssemblyLineTypeId, altgd.GroupId });
      
      // Column level mappings
      this.Property(altgd => altgd.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");
      this.Property(altgd => altgd.GroupId).HasColumnName("groupID");
      this.Property(altgd => altgd.MaterialMultiplier).HasColumnName("materialMultiplier");
      this.Property(altgd => altgd.TimeMultiplier).HasColumnName("timeMultiplier");

      // Relationship mappings
      this.HasRequired(altgd => altgd.AssemblyLineType).WithMany(alt => alt.GroupDetails).HasForeignKey(altgd => altgd.AssemblyLineTypeId);
      this.HasRequired(altgd => altgd.Group).WithMany().HasForeignKey(altgd => altgd.GroupId);
    }
  }
}