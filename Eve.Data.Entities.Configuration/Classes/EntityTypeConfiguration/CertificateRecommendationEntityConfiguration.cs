//-----------------------------------------------------------------------
// <copyright file="CertificateRecommendationEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CertificateRecommendationEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CertificateRecommendationEntityConfiguration : EntityTypeConfiguration<CertificateRecommendationEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CertificateRecommendationEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CertificateRecommendationEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("crtRecommendations");
      this.HasKey(cr => cr.Id);
      
      // Column level mappings
      this.Property(cr => cr.CertificateId).HasColumnName("certificateID");
      this.Property(cr => cr.Id).HasColumnName("recommendationID");
      this.Property(cr => cr.RecommendationLevel).HasColumnName("recommendationLevel");
      this.Property(cr => cr.ShipTypeId).HasColumnName("shipTypeID");

      // Relationship mappings
      this.HasRequired(cr => cr.Certificate).WithMany().HasForeignKey(cr => cr.CertificateId);
      this.HasRequired(cr => cr.ShipType).WithMany().HasForeignKey(cr => cr.ShipTypeId);
    }
  }
}