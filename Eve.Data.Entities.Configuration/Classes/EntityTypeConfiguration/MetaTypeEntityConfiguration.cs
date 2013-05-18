//-----------------------------------------------------------------------
// <copyright file="MetaTypeEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="MetaTypeEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class MetaTypeEntityConfiguration : EntityTypeConfiguration<MetaTypeEntity>
  {
    /// <summary>
    /// Initializes a new instance of the MetaTypeEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public MetaTypeEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invMetaTypes");
      this.HasKey(mt => mt.TypeId);
      
      // Column level mappings
      this.Property(mt => mt.MetaGroupId).HasColumnName("metaGroupID");
      this.Property(mt => mt.ParentTypeId).HasColumnName("parentTypeID");
      this.Property(mt => mt.TypeId).HasColumnName("typeID");

      // Relationship mappings
      this.HasRequired(mt => mt.MetaGroup).WithMany().HasForeignKey(mt => mt.MetaGroupId);
      this.HasRequired(mt => mt.ParentType).WithMany().HasForeignKey(my => my.ParentTypeId);
      this.HasRequired(mt => mt.Type).WithOptional(et => et.MetaType);
    }
  }
}