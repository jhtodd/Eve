//-----------------------------------------------------------------------
// <copyright file="CertificateRelationshipEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="CertificateRelationshipEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class CertificateRelationshipEntityConfiguration : EntityTypeConfiguration<CertificateRelationshipEntity>
  {
    /// <summary>
    /// Initializes a new instance of the CertificateRelationshipEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public CertificateRelationshipEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("crtRelationships");
      this.HasKey(cr => cr.Id);
      
      // Column level mappings
      this.Property(cr => cr.Id).HasColumnName("relationshipID");
      this.Property(cr => cr.ChildId).HasColumnName("childID");
      this.Property(cr => cr.ParentId).HasColumnName("parentID");
      this.Property(cr => cr.ParentLevel).HasColumnName("parentLevel");
      this.Property(cr => cr.ParentTypeId).HasColumnName("parentTypeID");

      // Relationship mappings
      this.HasRequired(cr => cr.Child).WithMany(c => c.Prerequisites).HasForeignKey(cr => cr.ChildId);
      this.HasRequired(cr => cr.Parent).WithMany().HasForeignKey(cr => cr.ParentId);
      this.HasRequired(cr => cr.ParentType).WithMany().HasForeignKey(cr => cr.ParentTypeId);
    }
  }
}