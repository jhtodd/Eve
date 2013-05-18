//-----------------------------------------------------------------------
// <copyright file="AgentTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="AgentTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class AgentTypeEntityConfiguration : EntityTypeConfiguration<AgentTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the AgentTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public AgentTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("agtAgentTypes"); m.MapInheritedProperties(); });
      this.HasKey(at => at.Id);
      
      // Column level mappings
      this.Property(at => at.Id).HasColumnName("agentTypeID");
      this.Property(at => at.Name).HasColumnName("agentType");
      this.Ignore(at => at.Description);

      // Relationship mappings
    }
  }
}