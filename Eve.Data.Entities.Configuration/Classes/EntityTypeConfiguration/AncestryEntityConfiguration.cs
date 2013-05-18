//-----------------------------------------------------------------------
// <copyright file="AncestryEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AncestryEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AncestryEntityConfiguration : EntityTypeConfiguration<AncestryEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AncestryEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AncestryEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("chrAncestries"); m.MapInheritedProperties(); });
      this.HasKey(a => a.Id);
      
      // Column level mappings
      this.Property(a => a.Id).HasColumnName("ancestryID");
      this.Property(a => a.Name).HasColumnName("ancestryName");
      this.Property(a => a.Description).HasColumnName("description");

      this.Property(a => a.BloodlineId).HasColumnName("bloodlineID");
      this.Property(a => a.Charisma).HasColumnName("charisma");
      this.Property(a => a.IconId).HasColumnName("iconID");
      this.Property(a => a.Intelligence).HasColumnName("intelligence");
      this.Property(a => a.Memory).HasColumnName("memory");
      this.Property(a => a.Perception).HasColumnName("perception");
      this.Property(a => a.ShortDescription).HasColumnName("shortDescription");
      this.Property(a => a.Willpower).HasColumnName("willpower");

      // Relationship mappings
      this.HasRequired(a => a.Bloodline).WithMany(b => b.Ancestries).HasForeignKey(a => a.BloodlineId);
      this.HasOptional(a => a.Icon).WithMany().HasForeignKey(a => a.IconId);
    }
  }
}