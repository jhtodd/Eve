//-----------------------------------------------------------------------
// <copyright file="ItemPositionEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="ItemPositionEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class ItemPositionEntityConfiguration : EntityTypeConfiguration<ItemPositionEntity>
  {
    /// <summary>
    /// Initializes a new instance of the ItemPositionEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public ItemPositionEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invPositions");
      this.HasKey(ip => ip.ItemId);
      
      // Column level mappings
      this.Property(ip => ip.ItemId).HasColumnName("itemID");
      this.Property(ip => ip.Pitch).HasColumnName("pitch");
      this.Property(ip => ip.Roll).HasColumnName("roll");
      this.Property(ip => ip.X).HasColumnName("x");
      this.Property(ip => ip.Y).HasColumnName("y");
      this.Property(ip => ip.Yaw).HasColumnName("yaw");
      this.Property(ip => ip.Z).HasColumnName("z");

      // Relationship mappings
      this.HasRequired(ip => ip.Item).WithOptional(i => i.Position);
    }
  }
}