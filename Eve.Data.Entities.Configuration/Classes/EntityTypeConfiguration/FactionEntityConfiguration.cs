//-----------------------------------------------------------------------
// <copyright file="FactionEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="FactionEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class FactionEntityConfiguration : EntityTypeConfiguration<FactionEntity>
  {
    /// <summary>
    /// Initializes a new instance of the FactionEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public FactionEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("chrFactions");
      this.HasKey(f => f.Id);
      
      // Column level mappings
      this.Property(f => f.CorporationId).HasColumnName("corporationID");
      this.Property(f => f.Description).HasColumnName("description");
      this.Property(f => f.FactionName).HasColumnName("factionName");
      this.Property(f => f.IconId).HasColumnName("iconID");
      this.Property(f => f.Id).HasColumnName("factionID");
      this.Property(f => f.MilitiaCorporationId).HasColumnName("militiaCorporationID");
      this.Property(f => f.RaceIds).HasColumnName("raceIDs");
      this.Property(f => f.SizeFactor).HasColumnName("sizeFactor");
      this.Property(f => f.SolarSystemId).HasColumnName("solarSystemID");
      this.Property(f => f.StationCount).HasColumnName("stationCount");
      this.Property(f => f.StationSystemCount).HasColumnName("stationSystemCount");

      // Relationship mappings
      this.HasRequired(f => f.Corporation).WithMany().HasForeignKey(f => f.CorporationId);
      this.HasRequired(f => f.Icon).WithMany().HasForeignKey(f => f.IconId);
      this.HasRequired(f => f.ItemInfo).WithOptional(i => i.FactionInfo);
      this.HasOptional(f => f.MilitiaCorporation).WithMany().HasForeignKey(f => f.MilitiaCorporationId);
      this.HasRequired(f => f.SolarSystem).WithMany().HasForeignKey(f => f.SolarSystemId);
    }
  }
}