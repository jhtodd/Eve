//-----------------------------------------------------------------------
// <copyright file="UniverseEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="UniverseEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class UniverseEntityConfiguration : EntityTypeConfiguration<UniverseEntity>
  {
    /// <summary>
    /// Initializes a new instance of the UniverseEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public UniverseEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("mapUniverse");
      this.HasKey(u => u.Id);
      
      // Column level mappings
      this.Property(u => u.Id).HasColumnName("universeID");
      this.Property(u => u.Radius).HasColumnName("radius");
      this.Property(u => u.UniverseName).HasColumnName("universeName");
      this.Property(u => u.X).HasColumnName("x");
      this.Property(u => u.XMax).HasColumnName("xMax");
      this.Property(u => u.XMin).HasColumnName("xMin");
      this.Property(u => u.Y).HasColumnName("y");
      this.Property(u => u.YMax).HasColumnName("yMax");
      this.Property(u => u.YMin).HasColumnName("yMin");
      this.Property(u => u.Z).HasColumnName("z");
      this.Property(u => u.ZMax).HasColumnName("zMax");
      this.Property(u => u.ZMin).HasColumnName("zMin");

      // Relationship mappings
      this.HasRequired(a => a.ItemInfo).WithOptional(i => i.UniverseInfo);
    }
  }
}