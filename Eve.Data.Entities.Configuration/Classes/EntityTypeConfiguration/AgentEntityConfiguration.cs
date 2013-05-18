//-----------------------------------------------------------------------
// <copyright file="AgentEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AgentEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AgentEntityConfiguration : EntityTypeConfiguration<AgentEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AgentEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AgentEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("agtAgents");
      this.HasKey(a => a.Id);
      
      // Column level mappings
      this.Property(a => a.AgentTypeId).HasColumnName("agentTypeID");
      this.Property(a => a.CorporationId).HasColumnName("corporationID");
      this.Property(a => a.DivisionId).HasColumnName("divisionID");
      this.Property(a => a.Id).HasColumnName("agentID");
      this.Property(a => a.IsLocator).HasColumnName("isLocator");
      this.Property(a => a.Level).HasColumnName("level");
      this.Property(a => a.LocationId).HasColumnName("locationID");
      this.Property(a => a.Quality).HasColumnName("quality");

      // Relationship mappings
      this.HasRequired(a => a.AgentType).WithMany().HasForeignKey(a => a.AgentTypeId);
      this.HasRequired(a => a.Corporation).WithMany(c => c.Agents).HasForeignKey(a => a.CorporationId);
      this.HasRequired(a => a.Division).WithMany().HasForeignKey(a => a.DivisionId);
      this.HasRequired(a => a.ItemInfo).WithOptional(i => i.AgentInfo);
      this.HasRequired(a => a.Location).WithMany().HasForeignKey(a => a.LocationId);
      this.HasMany(a => a.ResearchFields).WithMany().Map(m => m.ToTable("agtResearchAgents").MapLeftKey("agentID").MapRightKey("typeID"));
    }
  }
}