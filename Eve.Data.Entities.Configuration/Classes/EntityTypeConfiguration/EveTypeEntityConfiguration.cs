//-----------------------------------------------------------------------
// <copyright file="EveTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="EveTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class EveTypeEntityConfiguration : EntityTypeConfiguration<EveTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the EveTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public EveTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("invTypes"); m.MapInheritedProperties(); });
      this.HasKey(et => et.Id);
      
      // Column level mappings
      this.Property(et => et.Id).HasColumnName("typeID");
      this.Property(et => et.Name).HasColumnName("typeName");
      this.Property(et => et.Description).HasColumnName("description");

      this.Property(et => et.BasePrice).HasColumnName("basePrice");
      this.Property(et => et.Capacity).HasColumnName("capacity");
      this.Property(et => et.ChanceOfDuplicating).HasColumnName("chanceOfDuplicating");
      this.Property(et => et.GraphicId).HasColumnName("graphicID");
      this.Property(et => et.GroupId).HasColumnName("groupID");
      this.Property(et => et.IconId).HasColumnName("iconID");
      this.Property(et => et.MarketGroupId).HasColumnName("marketGroupID");
      this.Property(et => et.Mass).HasColumnName("mass");
      this.Property(et => et.RaceId).HasColumnName("raceID");
      this.Property(et => et.Radius).HasColumnName("radius");
      this.Property(et => et.PortionSize).HasColumnName("portionSize");
      this.Property(et => et.Published).HasColumnName("published");
      this.Property(et => et.SoundId).HasColumnName("soundID");
      this.Property(et => et.Volume).HasColumnName("volume");

      // Relationship mappings
      this.HasMany(et => et.Attributes).WithRequired(a => a.ItemType).HasForeignKey(a => a.ItemTypeId);
      this.HasMany(et => et.ContrabandInfo).WithRequired(ci => ci.Type).HasForeignKey(ci => ci.TypeId);
      this.HasMany(et => et.Effects).WithRequired(e => e.ItemType).HasForeignKey(e => e.ItemTypeId);
      this.HasOptional(et => et.Graphic).WithMany().HasForeignKey(et => et.GraphicId);
      this.HasRequired(et => et.Group).WithMany(g => g.Types).HasForeignKey(et => et.GroupId);
      this.HasOptional(et => et.Icon).WithMany().HasForeignKey(et => et.IconId);
      this.HasOptional(et => et.MarketGroup).WithMany(mg => mg.Types).HasForeignKey(et => et.MarketGroupId);
      this.HasMany(et => et.Materials).WithRequired(tm => tm.Type).HasForeignKey(tm => tm.TypeId);
      this.HasMany(et => et.ChildMetaTypes).WithRequired(mt => mt.ParentType).HasForeignKey(mt => mt.ParentTypeId);
    }
  }
}