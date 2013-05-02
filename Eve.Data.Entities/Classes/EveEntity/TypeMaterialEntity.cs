//-----------------------------------------------------------------------
// <copyright file="TypeMaterialEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The data entity for the <see cref="TypeMaterial" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("invTypeMaterials")]
  public class TypeMaterialEntity : EveEntity<TypeMaterial>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the TypeMaterialEntity class.
    /// </summary>
    public TypeMaterialEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("MaterialTypeId")]
    public virtual EveTypeEntity MaterialType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("materialTypeID", Order = 2)]
    [Key]
    public int MaterialTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("quantity")]
    public int Quantity { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [ForeignKey("TypeId")]
    public virtual EveTypeEntity Type { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("typeID", Order = 1)]
    [Key]
    public int TypeId { get; internal set; }

    /// <inheritdoc />
    [NotMapped]
    protected internal override IConvertible CacheKey
    {
      get { return CreateCacheKey(this.TypeId, this.MaterialTypeId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="typeId">
    /// The ID of the type.
    /// </param>
    /// <param name="materialTypeId">
    /// The ID of the material type.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCacheKey(int typeId, int materialTypeId)
    {
      return (long)((((ulong)(long)typeId) << 32) | ((ulong)(long)materialTypeId));
    }

    /// <inheritdoc />
    public override TypeMaterial ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new TypeMaterial(repository, this);
    }
  }
}