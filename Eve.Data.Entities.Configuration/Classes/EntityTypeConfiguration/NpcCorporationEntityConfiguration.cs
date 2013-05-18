//-----------------------------------------------------------------------
// <copyright file="NpcCorporationEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="NpcCorporationEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class NpcCorporationEntityConfiguration : EntityTypeConfiguration<NpcCorporationEntity>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NpcCorporationEntityConfiguration" /> class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public NpcCorporationEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("crpNPCCorporations");
      this.HasKey(c => c.Id);
      
      // Column level mappings
      this.Property(c => c.Border).HasColumnName("border");
      this.Property(c => c.Corridor).HasColumnName("corridor");
      this.Property(c => c.Description).HasColumnName("description");
      this.Property(c => c.EnemyId).HasColumnName("enemyID");
      this.Property(c => c.Extent).HasColumnName("extent");
      this.Property(c => c.FactionId).HasColumnName("factionID");
      this.Property(c => c.FriendId).HasColumnName("friendID");
      this.Property(c => c.Fringe).HasColumnName("fringe");
      this.Property(c => c.Hub).HasColumnName("hub");
      this.Property(c => c.IconId).HasColumnName("iconID");
      this.Property(c => c.Id).HasColumnName("corporationID");
      this.Property(c => c.InitialPrice).HasColumnName("initialPrice");
      this.Property(c => c.InvestorId1).HasColumnName("investorID1"); // TODO: Implements investors as complex type
      this.Property(c => c.InvestorId2).HasColumnName("investorID2");
      this.Property(c => c.InvestorId3).HasColumnName("investorID3");
      this.Property(c => c.InvestorId4).HasColumnName("investorID4");
      this.Property(c => c.InvestorShares1).HasColumnName("investorShares1");
      this.Property(c => c.InvestorShares2).HasColumnName("investorShares2");
      this.Property(c => c.InvestorShares3).HasColumnName("investorShares3");
      this.Property(c => c.InvestorShares4).HasColumnName("investorShares4");
      this.Property(c => c.MinSecurity).HasColumnName("minSecurity");
      this.Property(c => c.PublicShares).HasColumnName("publicShares");
      this.Property(c => c.Scattered).HasColumnName("scattered");
      this.Property(c => c.Size).HasColumnName("size");
      this.Property(c => c.SizeFactor).HasColumnName("sizeFactor");
      this.Property(c => c.SolarSystemId).HasColumnName("solarSystemID");
      this.Property(c => c.StationCount).HasColumnName("stationCount");
      this.Property(c => c.StationSystemCount).HasColumnName("stationSystemCount");

      // Relationship mappings
      this.HasMany(c => c.Agents).WithRequired(a => a.Corporation).HasForeignKey(a => a.CorporationId);
      this.HasMany(c => c.Divisions).WithRequired(d => d.Corporation).HasForeignKey(d => d.CorporationId);
      this.HasOptional(c => c.Enemy).WithMany().HasForeignKey(c => c.EnemyId);
      this.HasRequired(c => c.Faction).WithMany().HasForeignKey(c => c.FactionId);
      this.HasOptional(c => c.Friend).WithMany().HasForeignKey(c => c.FriendId);
      this.HasRequired(c => c.Icon).WithMany().HasForeignKey(c => c.IconId);
      this.HasOptional(c => c.Investor1).WithMany().HasForeignKey(c => c.InvestorId1);
      this.HasOptional(c => c.Investor2).WithMany().HasForeignKey(c => c.InvestorId2);
      this.HasOptional(c => c.Investor3).WithMany().HasForeignKey(c => c.InvestorId3);
      this.HasOptional(c => c.Investor4).WithMany().HasForeignKey(c => c.InvestorId4);
      this.HasRequired(c => c.ItemInfo).WithOptional(i => i.CorporationInfo);
      this.HasMany(c => c.ResearchFields).WithMany().Map(x => x.ToTable("crpNPCCorporationResearchFields").MapLeftKey("corporationID").MapRightKey("skillID"));
      this.HasRequired(c => c.SolarSystem).WithMany().HasForeignKey(c => c.SolarSystemId);
      this.HasMany(c => c.TradeGoods).WithMany().Map(m => m.ToTable("crpNPCCorporationTrades").MapLeftKey("corporationID").MapRightKey("typeID"));
    }
  }
}