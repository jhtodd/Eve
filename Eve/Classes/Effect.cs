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
    : EveEntityAdapter<EffectEntity>,
      IComparable,
      IComparable<IEffect>,
      IEffect,
      IEquatable<Effect>,
      IEveCacheable,
      IHasIcon,
      IKeyItem<EffectId>
  {
    private EffectType effectType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Effect class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public Effect(IEveRepository container, EffectEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the ID of the effect the current value applies to.
    /// </summary>
    /// <value>
    /// The ID of the effect the current value applies to.
    /// </value>
    public EffectId Id
    {
      get { return (EffectId)Entity.EffectId; }
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
      get { return Entity.IsDefault; }
    }

    /// <summary>
    /// Gets the effect type to which the value applies.
    /// </summary>
    /// <value>
    /// The <see cref="EffectType" /> to which the value applies.
    /// </value>
    public EffectType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EffectType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.effectType,
          () => this.Container.GetOrAdd<EffectType>(this.Id, () => this.Entity.EffectType.ToAdapter(this.Container)));

        Contract.Assume(this.effectType != null);
        return this.effectType;
      }
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
    public static long CreateCacheKey(TypeId itemTypeId, EffectId effectId)
    {
      return (long)((((ulong)(long)itemTypeId) << 32) | ((ulong)(long)effectId));
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
        result = this.Type.CompareTo(other.Type);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as Effect);
    }

    /// <inheritdoc />
    public bool Equals(Effect other)
    {
      if (other == null)
      {
        return false;
      }

      return this.Id.Equals(other.Id) && this.IsDefault.Equals(other.IsDefault);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.Id, this.IsDefault);
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
      return this.Type.DisplayName + (this.IsDefault ? " (default)" : string.Empty);
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class Effect : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      Effect other = obj as Effect;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEffect Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEffect" /> interface.
  /// </content>
  public partial class Effect : IEffect
  {
    EffectId IEffect.Id
    {
      get { return this.Id; }
    }

    bool IEffect.IsDefault
    {
      get { return this.IsDefault; }
    }

    EffectType IEffect.Type
    {
      get { return this.Type; }
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class Effect : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return CreateCacheKey(this.Entity.ItemTypeId, this.Id); }
    }
  }
  #endregion

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class Effect : IHasIcon
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

  #region IKeyItem<EffectId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class Effect : IKeyItem<EffectId>
  {
    EffectId IKeyItem<EffectId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}