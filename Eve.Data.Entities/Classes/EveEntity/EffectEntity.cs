//-----------------------------------------------------------------------
// <copyright file="EffectEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The data entity for the <see cref="Effect" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("dgmTypeEffects")]
  public class EffectEntity : EveEntity<Effect>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EffectEntity class.
    /// </summary>
    public EffectEntity() : base()
    {
    }

    /* Properties */
    
    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("effectID", Order = 1)]
    [Key]
    public EffectId EffectId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("EffectId")]
    public virtual EffectTypeEntity EffectType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("isDefault")]
    public bool IsDefault { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("ItemTypeId")]
    public virtual EveTypeEntity ItemType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("typeID", Order = 2)]
    [Key]
    public int ItemTypeId { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return CreateCacheKey(this.ItemTypeId, this.EffectId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="itemTypeId">
    /// The item type ID.
    /// </param>
    /// <param name="effectId">
    /// The effect ID.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCacheKey(int itemTypeId, EffectId effectId)
    {
      return (long)((((ulong)(long)itemTypeId) << 32) | ((ulong)(long)effectId));
    }

    /// <inheritdoc />
    public override Effect ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new Effect(repository, this);
    }
  }
}