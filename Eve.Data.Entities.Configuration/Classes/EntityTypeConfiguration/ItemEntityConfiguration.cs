//-----------------------------------------------------------------------
// <copyright file="ItemEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ItemEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ItemEntityConfiguration : EntityTypeConfiguration<ItemEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ItemEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ItemEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invItems");
      this.HasKey(i => i.Id);
      
      // Column level mappings
      this.Property(i => i.FlagId).HasColumnName("flagID");
      this.Property(i => i.Id).HasColumnName("itemID");
      this.Ignore(i => i.IsAgent);
      this.Ignore(i => i.IsCelestial);
      this.Ignore(i => i.IsConstellation);
      this.Ignore(i => i.IsCorporation);
      this.Ignore(i => i.IsFaction);
      this.Ignore(i => i.IsRegion);
      this.Ignore(i => i.IsSolarSystem);
      this.Ignore(i => i.IsStargate);
      this.Ignore(i => i.IsStation);
      this.Ignore(i => i.IsUniverse);
      this.Property(i => i.ItemTypeId).HasColumnName("typeID");
      this.Property(i => i.LocationId).HasColumnName("locationID");
      this.Property(i => i.OwnerId).HasColumnName("ownerID");
      this.Property(i => i.Quantity).HasColumnName("quantity");

      // Relationship mappings
      this.HasOptional(i => i.AgentInfo).WithRequired(ai => ai.ItemInfo);
      this.HasOptional(i => i.CelestialInfo).WithRequired(ci => ci.ItemInfo);
      this.HasOptional(i => i.ConstellationInfo).WithRequired(ci => ci.ItemInfo);
      this.HasOptional(i => i.CorporationInfo).WithRequired(ci => ci.ItemInfo);
      this.HasOptional(i => i.FactionInfo).WithRequired(fi => fi.ItemInfo);
      this.HasRequired(i => i.Flag).WithMany().HasForeignKey(i => i.FlagId);
      this.HasRequired(i => i.ItemType).WithMany().HasForeignKey(i => i.ItemTypeId);
      this.HasRequired(i => i.Location).WithMany().HasForeignKey(i => i.LocationId);
      this.HasOptional(i => i.Name).WithRequired();
      this.HasRequired(i => i.Owner).WithMany().HasForeignKey(i => i.OwnerId);
      this.HasOptional(i => i.Position).WithRequired(ip => ip.Item);
      this.HasOptional(i => i.RegionInfo).WithRequired(ri => ri.ItemInfo);
      this.HasOptional(i => i.SolarSystemInfo).WithRequired(si => si.ItemInfo);
      this.HasOptional(i => i.StargateInfo).WithRequired(si => si.ItemInfo);
      this.HasOptional(i => i.StationInfo).WithRequired(si => si.ItemInfo);
      this.HasOptional(i => i.UniverseInfo).WithRequired(ui => ui.ItemInfo);
    }
  }
}