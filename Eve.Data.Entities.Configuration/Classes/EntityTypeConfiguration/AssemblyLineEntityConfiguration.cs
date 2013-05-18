//-----------------------------------------------------------------------
// <copyright file="AssemblyLineEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AssemblyLineEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AssemblyLineEntityConfiguration : EntityTypeConfiguration<AssemblyLineEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AssemblyLineEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AssemblyLineEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("ramAssemblyLines");
      this.HasKey(al => al.Id);
      
      // Column level mappings
      this.Property(al => al.ActivityId).HasColumnName("activityID");
      this.Property(al => al.AssemblyLineTypeId).HasColumnName("assemblyLineTypeID");
      this.Property(al => al.ContainerId).HasColumnName("containerID");
      this.Property(al => al.CostInstall).HasColumnName("costInstall");
      this.Property(al => al.CostPerHour).HasColumnName("costPerHour");
      this.Property(al => al.DiscountPerGoodStandingPoint).HasColumnName("discountPerGoodStandingPoint");
      this.Property(al => al.Id).HasColumnName("assemblyLineID");
      this.Property(al => al.MaximumCharSecurity).HasColumnName("maximumCharSecurity");
      this.Property(al => al.MaximumCorpSecurity).HasColumnName("maximumCorpSecurity");
      this.Property(al => al.MinimumCharSecurity).HasColumnName("minimumCharSecurity");
      this.Property(al => al.MinimumCorpSecurity).HasColumnName("minimumCorpSecurity");
      this.Property(al => al.MinimumStanding).HasColumnName("minimumStanding");
      this.Property(al => al.NextFreeTime).HasColumnName("nextFreeTime");
      this.Property(al => al.OwnerId).HasColumnName("ownerID");
      this.Property(al => al.RestrictionMask).HasColumnName("restrictionMask");
      this.Property(al => al.SurchargePerBadStandingPoint).HasColumnName("surchargePerBadStandingPoint");
      this.Property(al => al.UiGroupingId).HasColumnName("UIGroupingID");

      // Relationship mappings
      this.HasRequired(al => al.Activity).WithMany().HasForeignKey(al => al.ActivityId);
      this.HasRequired(al => al.AssemblyLineType).WithMany().HasForeignKey(al => al.AssemblyLineTypeId);
      this.HasRequired(al => al.Container).WithMany(s => s.AssemblyLines).HasForeignKey(al => al.ContainerId);
      this.HasRequired(al => al.Owner).WithMany().HasForeignKey(al => al.OwnerId);
    }
  }
}