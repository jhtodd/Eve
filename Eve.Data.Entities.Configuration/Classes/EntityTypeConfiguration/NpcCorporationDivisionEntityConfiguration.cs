//-----------------------------------------------------------------------
// <copyright file="NpcCorporationDivisionEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="NpcCorporationDivisionEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class NpcCorporationDivisionEntityConfiguration : EntityTypeConfiguration<NpcCorporationDivisionEntity>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NpcCorporationDivisionEntityConfiguration" /> class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public NpcCorporationDivisionEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("crpNPCCorporationDivisions");
      this.HasKey(cd => new { cd.CorporationId, cd.DivisionId });
      
      // Column level mappings
      this.Property(cd => cd.CorporationId).HasColumnName("corporationID");
      this.Property(cd => cd.DivisionId).HasColumnName("divisionID");
      this.Property(cd => cd.Size).HasColumnName("size");

      // Relationship mappings
      this.HasMany(cd => cd.Agents).WithRequired().HasForeignKey(a => new { a.CorporationId, a.DivisionId });
      this.HasRequired(cd => cd.Corporation).WithMany(c => c.Divisions).HasForeignKey(cd => cd.CorporationId);
      this.HasRequired(cd => cd.Division).WithMany().HasForeignKey(cd => cd.DivisionId);
    }
  }
}