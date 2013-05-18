//-----------------------------------------------------------------------
// <copyright file="AssemblyLineStationEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AssemblyLineStationEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AssemblyLineStationEntityConfiguration : EntityTypeConfiguration<AssemblyLineStationEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AssemblyLineStationEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AssemblyLineStationEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("ramAssemblyLineStations");
      this.HasKey(als => new { als.StationId, als.AssemblyLineTypeId });
      
      // Column level mappings
      this.Property(a => a.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");
      this.Property(a => a.OwnerId).HasColumnName("ownerID");
      this.Property(a => a.Quantity).HasColumnName("quantity");
      this.Property(a => a.RegionId).HasColumnName("regionID");
      this.Property(a => a.SolarSystemId).HasColumnName("solarSystemID");
      this.Property(a => a.StationId).HasColumnName("stationID");
      this.Property(a => a.StationTypeId).HasColumnName("stationTypeID");

      // Relationship mappings
      this.HasRequired(als => als.AssemblyLineType).WithMany().HasForeignKey(als => als.AssemblyLineTypeId);
      this.HasRequired(als => als.Owner).WithMany().HasForeignKey(als => als.OwnerId);
      this.HasRequired(als => als.Region).WithMany().HasForeignKey(als => als.RegionId);
      this.HasRequired(als => als.SolarSystem).WithMany().HasForeignKey(als => als.SolarSystemId);
      this.HasRequired(als => als.Station).WithMany(s => s.AssemblyLineTypes).HasForeignKey(als => als.StationId);
      this.HasRequired(als => als.StationType).WithMany().HasForeignKey(als => als.StationTypeId);
    }
  }
}