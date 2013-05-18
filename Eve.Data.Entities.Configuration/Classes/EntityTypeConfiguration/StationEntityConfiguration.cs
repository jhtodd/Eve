//-----------------------------------------------------------------------
// <copyright file="StationEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="StationEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class StationEntityConfiguration : EntityTypeConfiguration<StationEntity>
  {
    /// <summary>
    /// Initializes a new instance of the StationEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public StationEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("staStations");
      this.HasKey(s => s.Id);
      
      // Column level mappings
      this.Property(s => s.ConstellationId).HasColumnName("constellationID");
      this.Property(s => s.CorporationId).HasColumnName("corporationID");
      this.Property(s => s.DockingCostPerVolume).HasColumnName("dockingCostPerVolume");
      this.Property(s => s.Id).HasColumnName("stationID");
      this.Property(s => s.MaxShipVolumeDockable).HasColumnName("maxShipVolumeDockable");
      this.Property(s => s.OfficeRentalCost).HasColumnName("officeRentalCost");
      this.Property(s => s.OperationId).HasColumnName("operationID");
      this.Property(s => s.RegionId).HasColumnName("regionID");
      this.Property(s => s.ReprocessingEfficiency).HasColumnName("reprocessingEfficiency");
      this.Property(s => s.ReprocessingHangarFlag).HasColumnName("reprocessingHangarFlag");
      this.Property(s => s.ReprocessingStationsTake).HasColumnName("reprocessingStationsTake");
      this.Property(s => s.Security).HasColumnName("security");
      this.Property(s => s.SolarSystemId).HasColumnName("solarSystemID");
      this.Property(s => s.StationName).HasColumnName("stationName");
      this.Property(s => s.StationTypeId).HasColumnName("stationTypeID");
      this.Property(s => s.X).HasColumnName("x");
      this.Property(s => s.Y).HasColumnName("y");
      this.Property(s => s.Z).HasColumnName("z");

      // Relationship mappings
      this.HasMany(s => s.Agents).WithRequired().HasForeignKey(a => a.LocationId);
      this.HasMany(s => s.AssemblyLines).WithRequired(al => al.Container).HasForeignKey(al => al.ContainerId);
      this.HasMany(s => s.AssemblyLineTypes).WithRequired(alt => alt.Station).HasForeignKey(alt => alt.StationId);
      this.HasRequired(s => s.Constellation).WithMany().HasForeignKey(s => s.ConstellationId);
      this.HasRequired(s => s.Corporation).WithMany().HasForeignKey(s => s.CorporationId);
      this.HasRequired(s => s.ItemInfo).WithOptional(i => i.StationInfo);
      this.HasRequired(s => s.Operation).WithMany().HasForeignKey(s => s.OperationId);
      this.HasRequired(s => s.Region).WithMany().HasForeignKey(s => s.RegionId);
      this.HasRequired(s => s.SolarSystem).WithMany().HasForeignKey(s => s.SolarSystemId);
      this.HasRequired(s => s.StationType).WithMany().HasForeignKey(s => s.StationTypeId);
    }
  }
}