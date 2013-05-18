//-----------------------------------------------------------------------
// <copyright file="EffectEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="EffectEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class EffectEntityConfiguration : EntityTypeConfiguration<EffectEntity>
  {
    /// <summary>
    /// Initializes a new instance of the EffectEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public EffectEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("dgmTypeEffects");
      this.HasKey(e => new { e.EffectId, e.ItemTypeId });
      
      // Column level mappings
      this.Property(e => e.EffectId).HasColumnName("effectID");
      this.Property(e => e.IsDefault).HasColumnName("isDefault");
      this.Property(e => e.ItemTypeId).HasColumnName("typeID");

      // Relationship mappings
      this.HasRequired(e => e.EffectType).WithMany().HasForeignKey(e => e.EffectId);
      this.HasRequired(e => e.ItemType).WithMany().HasForeignKey(e => e.ItemTypeId);
    }
  }
}