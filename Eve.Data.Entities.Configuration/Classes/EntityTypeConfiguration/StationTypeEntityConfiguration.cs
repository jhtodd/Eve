//-----------------------------------------------------------------------
// <copyright file="StationTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="StationTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class StationTypeEntityConfiguration : EntityTypeConfiguration<StationTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the StationTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public StationTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("staStationTypes");
      
      // Column level mappings
      this.Property(st => st.Conquerable).HasColumnName("conquerable");
      this.Property(st => st.DockEntryX).HasColumnName("dockEntryX");
      this.Property(st => st.DockEntryY).HasColumnName("dockEntryY");
      this.Property(st => st.DockEntryZ).HasColumnName("dockEntryZ");
      this.Property(st => st.DockOrientationX).HasColumnName("dockOrientationX");
      this.Property(st => st.DockOrientationY).HasColumnName("dockOrientationY");
      this.Property(st => st.DockOrientationZ).HasColumnName("dockOrientationZ");
      this.Property(st => st.OfficeSlots).HasColumnName("officeSlots");
      this.Property(st => st.OperationId).HasColumnName("operationID");
      this.Property(st => st.ReprocessingEfficiency).HasColumnName("reprocessingEfficiency");

      // Relationship mappings
      this.HasOptional(st => st.Operation).WithMany().HasForeignKey(st => st.OperationId);
    }
  }
}