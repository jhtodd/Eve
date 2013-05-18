//-----------------------------------------------------------------------
// <copyright file="TypeReactionEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The data entity for the <see cref="TypeReaction" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public class TypeReactionEntity : EveEntity<TypeReaction>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the TypeReactionEntity class.
    /// </summary>
    public TypeReactionEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public bool Input { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public short Quantity { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual EveTypeEntity ReactionType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ReactionTypeId { get; internal set; }

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
      get { return CreateCacheKey(this.ReactionTypeId, this.Input, this.TypeId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="reactionTypeId">
    /// The ID of the reaction type.
    /// </param>
    /// <param name="input">
    /// Specifies whether the type is required by or produced by the reaction.
    /// </param>
    /// <param name="typeId">
    /// The ID of the type required or produced by the reaction.
    /// </param>
    /// <returns>
    /// A compound ID combining the sub-IDs.
    /// </returns>
    public static long CreateCacheKey(int reactionTypeId, bool input, int typeId)
    {
      return (long)((((ulong)(long)reactionTypeId) << 32) | ((ulong)(input ? (ulong)1 << 31 : 0)) | ((ulong)(long)typeId));
    }

    /// <inheritdoc />
    public override TypeReaction ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new TypeReaction(repository, this);
    }
  }
}