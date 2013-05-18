//-----------------------------------------------------------------------
// <copyright file="BlueprintTypeEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;

  using Eve.Universe;

  /// <summary>
  /// The data entity for the <see cref="BlueprintType" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("staBlueprintTypes")]
  public partial class BlueprintTypeEntity
    : EveTypeEntity,
      IEveEntity<BlueprintType>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the BlueprintTypeEntity class.
    /// </summary>
    public BlueprintTypeEntity()
      : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("materialModifier")]
    public short MaterialModifier { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("maxProductionLimit")]
    public int MaxProductionLimit { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ParentBlueprintTypeId")]
    public virtual BlueprintTypeEntity ParentBlueprintType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("parentBlueprintTypeId")]
    public int? ParentBlueprintTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ProductTypeId")]
    public virtual EveType ProductType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("productTypeId")]
    public int ProductTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("productionTime")]
    public int ProductionTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("productivityModifier")]
    public int ProductivityModifier { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("researchCopyTime")]
    public int ResearchCopyTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("researchMaterialTime")]
    public int ResearchMaterialTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("researchProductivityTime")]
    public int ResearchProductivityTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("researchTechTime")]
    public int ResearchTechTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("techLevel")]
    public short TechLevel { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("wasteFactor")]
    public short WasteFactor { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public new BlueprintType ToAdapter(IEveRepository repository)
    {
      return (BlueprintType)base.ToAdapter(repository);
    }
  }

  #region IEveEntity Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveEntity" /> interface.
  /// </content>
  public partial class BlueprintTypeEntity : IEveEntity
  {
    // Necessary to override explicitly for static contract verification
    System.IConvertible IEveEntity.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion
}