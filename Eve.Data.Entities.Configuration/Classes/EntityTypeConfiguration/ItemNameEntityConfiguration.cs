//-----------------------------------------------------------------------
// <copyright file="ItemNameEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ItemNameEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ItemNameEntityConfiguration : EntityTypeConfiguration<ItemNameEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ItemNameEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ItemNameEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invNames");
      this.HasKey(i => i.ItemId);
      
      // Column level mappings
      this.Property(i => i.ItemId).HasColumnName("itemID");
      this.Property(i => i.Value).HasColumnName("itemName");

      // Relationship mappings
    }
  }
}