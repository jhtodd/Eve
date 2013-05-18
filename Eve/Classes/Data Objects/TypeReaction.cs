//-----------------------------------------------------------------------
// <copyright file="TypeReaction.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;

  /// <summary>
  /// Contains information about the materials required to manufacture
  /// a type of EVE item.  This is also used to determine which materials
  /// are recovered when an item is reprocessed.
  /// </summary>
  public sealed partial class TypeReaction
    : EveEntityAdapter<TypeReactionEntity, TypeReaction>,
      IHasIcon,
      IKeyItem<EveTypeId>
  {
    private EveType reactionType;
    private EveType type;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the TypeReaction class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal TypeReaction(IEveRepository repository, TypeReactionEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets a value indicating whether the type is an input or output.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the type specified by <see cref="Type" /> is
    /// an input for the reaction; otherwise <see langword="false" />.
    /// </value>
    public bool Input
    {
      get { return this.Entity.Input; }
    }

    /// <summary>
    /// Gets the quantity of the item specified by <see cref="Type" />
    /// required or produced by the reaction.
    /// </summary>
    /// <value>
    /// The quantity required or produced by the reaction.
    /// </value>
    public int Quantity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);

        var result = this.Entity.Quantity;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the type of the reaction.
    /// </summary>
    /// <value>
    /// The type of the reaction.
    /// </value>
    public EveType ReactionType // TODO: Replace with ReactionType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.reactionType, this.Entity.ReactionTypeId, () => this.Entity.ReactionType);
      }
    }

    /// <summary>
    /// Gets the ID of the reaction.
    /// </summary>
    /// <value>
    /// The ID of the reaction.
    /// </value>
    public EveTypeId ReactionTypeId
    {
      get { return this.Entity.ReactionTypeId; }
    }

    /// <summary>
    /// Gets the type required by or produced by the reaction.
    /// </summary>
    /// <value>
    /// The type required by or produced by the reaction.
    /// </value>
    private EveType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.type, this.Entity.TypeId, () => this.Entity.Type);
      }
    }

    /// <summary>
    /// Gets the ID of the type required by or produced by the reaction.
    /// </summary>
    /// <value>
    /// The ID of the type required by or produced by the reaction.
    /// </value>
    private EveTypeId TypeId
    {
      get { return this.Entity.TypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(TypeReaction other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.ReactionType.CompareTo(other.ReactionType);

      if (result == 0)
      {
        result = this.Type.CompareTo(other.Type);
      }

      if (result == 0)
      {
        result = this.Quantity.CompareTo(other.Quantity);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Type.Name + " (" + this.Quantity.ToString("#,##0") + ", " + (this.Input ? "input" : "output") + ")";
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class TypeReaction : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.Type.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.Type.IconId; }
    }
  }
  #endregion

  #region IKeyItem<EveTypeId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class TypeReaction : IKeyItem<EveTypeId>
  {
    EveTypeId IKeyItem<EveTypeId>.Key
    {
      get { return this.TypeId; }
    }
  }
  #endregion
}