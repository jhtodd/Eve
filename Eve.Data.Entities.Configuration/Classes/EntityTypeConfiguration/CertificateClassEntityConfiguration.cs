//-----------------------------------------------------------------------
// <copyright file="CertificateClassEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CertificateClassEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CertificateClassEntityConfiguration : EntityTypeConfiguration<CertificateClassEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CertificateClassEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CertificateClassEntityConfiguration() : base()
    {
      // Table level mappings
      this.Map(m => { m.ToTable("crtClasses"); m.MapInheritedProperties(); });
      this.HasKey(cc => cc.Id);
      
      // Column level mappings
      this.Property(cc => cc.Id).HasColumnName("classID");
      this.Property(cc => cc.Name).HasColumnName("className");
      this.Property(cc => cc.Description).HasColumnName("description");

      // Relationship mappings
    }
  }
}