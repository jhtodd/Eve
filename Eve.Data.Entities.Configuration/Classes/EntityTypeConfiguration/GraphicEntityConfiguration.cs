//-----------------------------------------------------------------------
// <copyright file="GraphicEntityConfiguration.cs" company="Jeremy H. Todd">
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
  /// <see cref="GraphicEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class GraphicEntityConfiguration : EntityTypeConfiguration<GraphicEntity>
  {
    /// <summary>
    /// Initializes a new instance of the GraphicEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public GraphicEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("eveGraphics");
      this.HasKey(a => a.Id);
      
      // Column level mappings
      this.Property(a => a.Collidable).HasColumnName("collidable");
      this.Property(a => a.ColorScheme).HasColumnName("colorScheme");
      this.Property(a => a.Description).HasColumnName("description");
      this.Property(a => a.DirectoryId).HasColumnName("directoryID");
      this.Property(a => a.ExplosionId).HasColumnName("explosionID");
      this.Property(a => a.GfxRaceId).HasColumnName("gfxRaceID");
      this.Property(a => a.GraphicFile).HasColumnName("graphicFile");
      this.Property(a => a.GraphicName).HasColumnName("graphicName");
      this.Property(a => a.GraphicType).HasColumnName("graphicType");
      this.Property(a => a.Id).HasColumnName("graphicID");
      this.Property(a => a.Obsolete).HasColumnName("obsolete");

      // Relationship mappings
    }
  }
}