//-----------------------------------------------------------------------
// <copyright file="BloodlineEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="BloodlineEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class BloodlineEntityConfiguration : EntityTypeConfiguration<BloodlineEntity>
  {
    /// <summary>
    /// Initializes a new instance of the BloodlineEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public BloodlineEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("chrBloodlines"); m.MapInheritedProperties(); });
      this.HasKey(b => b.Id);
      
      // Column level mappings
      this.Property(b => b.Id).HasColumnName("bloodlineID");
      this.Property(b => b.Name).HasColumnName("bloodlineName");
      this.Property(b => b.Description).HasColumnName("description");

      this.Property(b => b.Charisma).HasColumnName("charisma");
      this.Property(b => b.CorporationId).HasColumnName("corporationID");
      this.Property(b => b.FemaleDescription).HasColumnName("femaleDescription");
      this.Property(b => b.IconId).HasColumnName("iconID");
      this.Property(b => b.Intelligence).HasColumnName("intelligence");
      this.Property(b => b.MaleDescription).HasColumnName("maleDescription");
      this.Property(b => b.Memory).HasColumnName("memory");
      this.Property(b => b.Perception).HasColumnName("perception");
      this.Property(b => b.RaceId).HasColumnName("raceID");
      this.Property(b => b.ShipTypeId).HasColumnName("shipTypeID");
      this.Property(b => b.ShortDescription).HasColumnName("shortDescription");
      this.Property(b => b.ShortFemaleDescription).HasColumnName("shortFemaleDescription");
      this.Property(b => b.ShortMaleDescription).HasColumnName("shortMaleDescription");
      this.Property(b => b.Willpower).HasColumnName("willpower");

      // Relationship mappings
      this.HasMany(b => b.Ancestries).WithRequired(a => a.Bloodline).HasForeignKey(a => a.BloodlineId);
      this.HasRequired(b => b.Corporation).WithMany().HasForeignKey(b => b.CorporationId);
      this.HasOptional(b => b.Icon).WithMany().HasForeignKey(b => b.IconId);
      this.HasRequired(b => b.Race).WithMany().HasForeignKey(b => b.RaceId);
      this.HasRequired(b => b.ShipType).WithMany().HasForeignKey(b => b.ShipTypeId);
    }
  }
}