//-----------------------------------------------------------------------
// <copyright file="StationOperationEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="StationOperationEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class StationOperationEntityConfiguration : EntityTypeConfiguration<StationOperationEntity>
  {
    /// <summary>
    /// Initializes a new instance of the StationOperationEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public StationOperationEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("staOperations"); m.MapInheritedProperties(); });
      this.HasKey(so => so.Id);
      
      // Column level mappings
      this.Property(so => so.Id).HasColumnName("operationID");
      this.Property(so => so.Name).HasColumnName("operationName");
      this.Property(so => so.Description).HasColumnName("description");

      this.Property(so => so.ActivityId).HasColumnName("activityID");
      this.Property(so => so.AmarrStationTypeId).HasColumnName("amarrStationTypeID");
      this.Property(so => so.Border).HasColumnName("border");
      this.Property(so => so.CaldariStationTypeId).HasColumnName("caldariStationTypeID");
      this.Property(so => so.Corridor).HasColumnName("corridor");
      this.Property(so => so.Fringe).HasColumnName("fringe");
      this.Property(so => so.GallenteStationTypeId).HasColumnName("gallenteStationTypeID");
      this.Property(so => so.Hub).HasColumnName("hub");
      this.Property(so => so.JoveStationTypeId).HasColumnName("joveStationTypeID");
      this.Property(so => so.MinmatarStationTypeId).HasColumnName("minmatarStationTypeID");
      this.Property(so => so.Ratio).HasColumnName("ratio");

      // Relationship mappings
      this.HasRequired(so => so.Activity).WithMany().HasForeignKey(so => so.ActivityId);
      this.HasOptional(so => so.AmarrStationType).WithMany().HasForeignKey(so => so.AmarrStationTypeId);
      this.HasOptional(so => so.CaldariStationType).WithMany().HasForeignKey(so => so.CaldariStationTypeId);
      this.HasOptional(so => so.GallenteStationType).WithMany().HasForeignKey(so => so.GallenteStationTypeId);
      this.HasOptional(so => so.JoveStationType).WithMany().HasForeignKey(so => so.JoveStationTypeId);
      this.HasOptional(so => so.MinmatarStationType).WithMany().HasForeignKey(so => so.MinmatarStationTypeId);
      this.HasMany(so => so.Services).WithMany().Map(x => x.ToTable("staOperationServices").MapLeftKey("operationID").MapRightKey("serviceID"));
    }
  }
}