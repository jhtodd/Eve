//-----------------------------------------------------------------------
// <copyright file="TypeRequirementEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Industry;

  /// <summary>
  /// The data entity for the <see cref="TypeRequirement" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public class TypeRequirementEntity : EveEntity<TypeRequirement>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the TypeRequirementEntity class.
    /// </summary>
    public TypeRequirementEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ActivityEntity Activity { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public ActivityId ActivityId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public double DamagePerJob { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int Quantity { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public bool Recycle { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual EveTypeEntity RequiredType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int RequiredTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual EveTypeEntity Type { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int TypeId { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return CreateCacheKey(this.TypeId, this.ActivityId, this.RequiredTypeId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="typeId">
    /// The ID of the type.
    /// </param>
    /// <param name="activityId">
    /// The ID of the activity.
    /// </param>
    /// <param name="requiredTypeId">
    /// The ID of the required type.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static IConvertible CreateCacheKey(int typeId, ActivityId activityId, int requiredTypeId)
    {
      return EveCache.CreateCompoundCacheKey(typeId, activityId, requiredTypeId);
    }

    /// <inheritdoc />
    public override TypeRequirement ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new TypeRequirement(repository, this);
    }
  }
}