//-----------------------------------------------------------------------
// <copyright file="CertificateEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CertificateEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CertificateEntityConfiguration : EntityTypeConfiguration<CertificateEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CertificateEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CertificateEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("crtCertificates");
      this.HasKey(c => c.Id);
      
      // Column level mappings
      this.Property(c => c.CategoryId).HasColumnName("categoryID");
      this.Property(c => c.ClassId).HasColumnName("classID");
      this.Property(c => c.CorporationId).HasColumnName("corpID");
      this.Property(c => c.Description).HasColumnName("description");
      this.Property(c => c.Grade).HasColumnName("grade");
      this.Property(c => c.Id).HasColumnName("certificateID");
      this.Property(c => c.IconId).HasColumnName("iconID");

      // Relationship mappings
      this.HasRequired(c => c.Category).WithMany().HasForeignKey(c => c.CategoryId);
      this.HasRequired(c => c.Class).WithMany().HasForeignKey(c => c.ClassId);
      this.HasRequired(c => c.Corporation).WithMany().HasForeignKey(c => c.CorporationId);
      this.HasRequired(c => c.Icon).WithMany().HasForeignKey(c => c.IconId);
      this.HasMany(c => c.Prerequisites).WithRequired(cr => cr.Child).HasForeignKey(cr => cr.ChildId);
    }
  }
}