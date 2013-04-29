//-----------------------------------------------------------------------
// <copyright file="Effect.cs" company="Jeremy H. Todd">
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
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about an effect associated with an EVE item.
  /// </summary>
  public sealed partial class Effect
    : EveEntityAdapter<EffectEntity, Effect>,
      IComparable<IEffect>,
      IEffect,
      IHasIcon,
      IKeyItem<EffectId>,
      IKeyItem<TypeId>
  {
    private EffectType effectType;
    private EveType itemType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Effect class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public Effect(IEveRepository repository, EffectEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the ID of the effect the current value applies to.
    /// </summary>
    /// <value>
    /// The ID of the effect the current value applies to.
    /// </value>
    public EffectId EffectId
    {
      get { return (EffectId)this.Entity.EffectId; }
    }

    /// <summary>
    /// Gets the effect type to which the value applies.
    /// </summary>
    /// <value>
    /// The <see cref="EffectType" /> to which the value applies.
    /// </value>
    public EffectType EffectType
    {
      get
      {
        Contract.Ensures(Contract.Result<EffectType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.effectType, this.Entity.EffectId, () => this.Entity.EffectType);
      }
    }

    /// <summary>
    /// Gets a value indicating whether it is the default effect for the item.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the effect is the default effect for the item;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool IsDefault
    {
      get { return this.Entity.IsDefault; }
    }

    /// <summary>
    /// Gets the ID of the type of item to which the effect applies.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="EveType" /> to which the effect applies.
    /// </value>
    public TypeId ItemTypeId
    {
      get { return (TypeId)this.Entity.ItemTypeId; }
    }

    /// <summary>
    /// Gets the type of item to which the effect applies.
    /// </summary>
    /// <value>
    /// The <see cref="EveType" /> to which the effect applies.
    /// </value>
    public EveType ItemType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.itemType, this.Entity.ItemTypeId, () => this.Entity.ItemType);
      }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(Effect other)
    {
      return this.CompareTo((IEffect)other);
    }

    /// <inheritdoc />
    public int CompareTo(IEffect other)
    {
      if (other == null)
      {
        return 1;
      }

      // Put default effects at the top
      int result = other.IsDefault.CompareTo(this.IsDefault);

      if (result == 0)
      {
        result = this.EffectType.CompareTo(other.EffectType);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(Effect other)
    {
      if (other == null)
      {
        return false;
      }

      return this.EffectId.Equals(other.EffectId) && this.ItemTypeId.Equals(other.ItemTypeId);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.EffectId, this.ItemTypeId);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.ToString(string.Empty);
    }

    /// <inheritdoc />
    /// <param name="format">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// <returns>
    /// A string containing a formatted version of the effect value.
    /// </returns>
    public string ToString(string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      // Format the value according to the effect
      return this.EffectType.DisplayName + (this.IsDefault ? " (default)" : string.Empty);
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class Effect : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.EffectType.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.EffectType.IconId; }
    }
  }
  #endregion

  #region IKeyItem<EffectId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class Effect : IKeyItem<EffectId>
  {
    EffectId IKeyItem<EffectId>.Key
    {
      get { return this.EffectId; }
    }
  }
  #endregion

  #region IKeyItem<TypeId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class Effect : IKeyItem<TypeId>
  {
    TypeId IKeyItem<TypeId>.Key
    {
      get { return this.ItemTypeId; }
    }
  }
  #endregion
}