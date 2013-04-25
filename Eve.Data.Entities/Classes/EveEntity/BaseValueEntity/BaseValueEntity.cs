//-----------------------------------------------------------------------
// <copyright file="BaseValueEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.CodeAnalysis;

  /// <summary>
  /// The base class for data entities for basic EVE values.
  /// </summary>
  /// <typeparam name="TId">
  /// The type of the ID value.
  /// </typeparam>
  /// <typeparam name="TAdapter">
  /// The type of the entity adapter.
  /// </typeparam>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public abstract partial class BaseValueEntity<TId, TAdapter> 
    : EveEntity<TAdapter>
    where TId : struct, IConvertible
    where TAdapter : IEveEntityAdapter<EveEntity<TAdapter>>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the BaseValueEntity class.
    /// </summary>
    public BaseValueEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public string Description { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public TId Id { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public string Name { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return this.Id; }
    }
  }
}