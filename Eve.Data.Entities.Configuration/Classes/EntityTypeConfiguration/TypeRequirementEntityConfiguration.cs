//-----------------------------------------------------------------------
// <copyright file="TypeRequirementEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="TypeRequirementEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class TypeRequirementEntityConfiguration : EntityTypeConfiguration<TypeRequirementEntity>
  {
    /// <summary>
    /// Initializes a new instance of the TypeRequirementEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public TypeRequirementEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("ramTypeRequirements");
      this.HasKey(tr => new { tr.TypeId, tr.ActivityId, tr.RequiredTypeId });
      
      // Column level mappings
      this.Property(tr => tr.ActivityId).HasColumnName("activityID");
      this.Property(tr => tr.DamagePerJob).HasColumnName("damagePerJob");
      this.Property(tr => tr.Quantity).HasColumnName("quantity");
      this.Property(tr => tr.Recycle).HasColumnName("recycle");
      this.Property(tr => tr.RequiredTypeId).HasColumnName("requiredTypeID");
      this.Property(tr => tr.TypeId).HasColumnName("typeID");

      // Relationship mappings
      this.HasRequired(tr => tr.Activity).WithMany().HasForeignKey(tr => tr.ActivityId);
      this.HasRequired(tr => tr.RequiredType).WithMany().HasForeignKey(tr => tr.RequiredTypeId);
      this.HasRequired(tr => tr.Type).WithMany().HasForeignKey(tr => tr.TypeId);
    }
  }
}