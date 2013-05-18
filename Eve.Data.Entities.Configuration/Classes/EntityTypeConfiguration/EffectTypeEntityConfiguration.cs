//-----------------------------------------------------------------------
// <copyright file="EffectTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="EffectTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class EffectTypeEntityConfiguration : EntityTypeConfiguration<EffectTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the EffectTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public EffectTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("dgmEffects"); m.MapInheritedProperties(); });
      this.HasKey(et => et.Id);
      
      // Column level mappings
      this.Property(et => et.Id).HasColumnName("effectID");
      this.Property(et => et.Name).HasColumnName("effectName");
      this.Property(et => et.Description).HasColumnName("description");

      this.Property(et => et.DisallowAutoRepeat).HasColumnName("disallowAutoRepeat");
      this.Property(et => et.DischargeAttributeId).HasColumnName("dischargeAttributeID");
      this.Property(et => et.DisplayName).HasColumnName("displayName");
      this.Property(et => et.Distribution).HasColumnName("distribution");
      this.Property(et => et.DurationAttributeId).HasColumnName("durationAttributeID");
      this.Property(et => et.EffectCategoryId).HasColumnName("effectCategory");
      this.Property(et => et.ElectronicChance).HasColumnName("electronicChance");
      this.Property(et => et.FalloffAttributeId).HasColumnName("falloffAttributeID");
      this.Property(et => et.FittingUsageChanceAttributeId).HasColumnName("fittingUsageChanceAttributeID");
      this.Property(et => et.Guid).HasColumnName("guid");
      this.Property(et => et.IconId).HasColumnName("iconID");
      this.Property(et => et.IsAssistance).HasColumnName("isAssistance");
      this.Property(et => et.IsOffensive).HasColumnName("isOffensive");
      this.Property(et => et.IsWarpSafe).HasColumnName("isWarpSafe");
      this.Property(et => et.NpcActivationChanceAttributeId).HasColumnName("npcActivationChanceAttributeID");
      this.Property(et => et.NpcUsageChanceAttributeId).HasColumnName("npcUsageChanceAttributeID");
      this.Property(et => et.PostExpression).HasColumnName("postExpression");
      this.Property(et => et.PreExpression).HasColumnName("preExpression");
      this.Property(et => et.PropulsionChance).HasColumnName("propulsionChance");
      this.Property(et => et.Published).HasColumnName("published");
      this.Property(et => et.RangeAttributeId).HasColumnName("rangeAttributeID");
      this.Property(et => et.RangeChance).HasColumnName("rangeChance");
      this.Property(et => et.SfxName).HasColumnName("sfxName");
      this.Property(et => et.TrackingSpeedAttributeId).HasColumnName("trackingSpeedAttributeID");

      // Relationship mappings
      this.HasOptional(et => et.DischargeAttribute).WithMany().HasForeignKey(et => et.DischargeAttributeId);
      this.HasOptional(et => et.DurationAttribute).WithMany().HasForeignKey(et => et.DurationAttributeId);
      this.HasOptional(et => et.FalloffAttribute).WithMany().HasForeignKey(et => et.FalloffAttributeId);
      this.HasOptional(et => et.FittingUsageChanceAttribute).WithMany().HasForeignKey(et => et.FittingUsageChanceAttributeId);
      this.HasOptional(et => et.Icon).WithMany().HasForeignKey(et => et.IconId);
      this.HasOptional(et => et.NpcActivationChanceAttribute).WithMany().HasForeignKey(et => et.NpcActivationChanceAttributeId);
      this.HasOptional(et => et.NpcUsageChanceAttribute).WithMany().HasForeignKey(et => et.NpcUsageChanceAttributeId);
      this.HasOptional(et => et.RangeAttribute).WithMany().HasForeignKey(et => et.RangeAttributeId);
      this.HasOptional(et => et.TrackingSpeedAttribute).WithMany().HasForeignKey(et => et.TrackingSpeedAttributeId);
    }
  }
}