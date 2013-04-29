//-----------------------------------------------------------------------
// <copyright file="EffectType.cs" company="Jeremy H. Todd">
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

  /// <summary>
  /// Contains information about an effect associated with an EVE item.
  /// </summary>
  public sealed partial class EffectType 
    : BaseValue<EffectId, EffectId, EffectTypeEntity, EffectType>,
      IComparable<IEffect>,
      IEffect,
      IHasIcon
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.
    private AttributeType dischargeAttribute;
    private AttributeType durationAttribute;
    private AttributeType falloffAttribute;
    private AttributeType fittingUsageChanceAttribute;
    private Icon icon;
    private AttributeType npcActivationChanceAttribute;
    private AttributeType npcUsageChanceAttribute;
    private AttributeType rangeAttribute;
    private AttributeType trackingSpeedAttribute;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EffectType class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal EffectType(IEveRepository repository, EffectTypeEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */
    
    /// <summary>
    /// Gets a value indicating whether auto-repeat is disabled for items with
    /// this effect.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if auto-repeat is disabled; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool DisallowAutoRepeat
    {
      get { return Entity.DisallowAutoRepeat; }
    }

    /// <summary>
    /// Gets the attribute that specifies the discharge rate of the effect, if 
    /// applicable.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the discharge rate of the
    /// effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType DischargeAttribute
    {
      get
      {
        Contract.Ensures(this.DischargeAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.DischargeAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.dischargeAttribute, this.Entity.DischargeAttributeId, () => this.Entity.DischargeAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the discharge rate of the
    /// effect, if applicable.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the discharge
    /// rate of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? DischargeAttributeId
    {
      get { return Entity.DischargeAttributeId; }
    }

    /// <summary>
    /// Gets the display name of the effect.
    /// </summary>
    /// <value>
    /// The human-readable display name of the effect.
    /// </value>
    public string DisplayName
    {
      get
      {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        return string.IsNullOrWhiteSpace(Entity.DisplayName) ? this.Name : this.Entity.DisplayName;
      }
    }

    /// <summary>
    /// Gets the value of the property.
    /// </summary>
    /// <value>
    /// The meaning of this property is unknown.
    /// </value>
    public short? Distribution
    {
      get { return Entity.Distribution; }
    }

    /// <summary>
    /// Gets the attribute that specifies the duration of the effect,
    /// if applicable.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the duration
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType DurationAttribute
    {
      get
      {
        Contract.Ensures(this.DurationAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.DurationAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.durationAttribute, this.Entity.DurationAttributeId, () => this.Entity.DurationAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the duration of the effect,
    /// if applicable.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the duration
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? DurationAttributeId
    {
      get { return Entity.DurationAttributeId; }
    }

    /// <summary>
    /// Gets the ID of the category of the effect.  The categories remain unknown.
    /// </summary>
    /// <value>
    /// The ID of the effect category.  The categories remain unknown.
    /// </value>
    public EffectCategoryId EffectCategoryId
    {
      get { return Entity.EffectCategoryId; }
    }

    /// <summary>
    /// Gets a value indicating whether the effect has a chance of target
    /// jamming another object.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the effect has a chance of target jamming;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool ElectronicChance
    {
      get { return Entity.ElectronicChance; }
    }

    /// <summary>
    /// Gets the attribute that specifies the falloff distance of the
    /// effect, if applicable.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the falloff
    /// distance of the effect, or <see langword="null" /> if no such attribute
    /// exists.
    /// </value>
    public AttributeType FalloffAttribute
    {
      get
      {
        Contract.Ensures(this.FalloffAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.FalloffAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.falloffAttribute, this.Entity.FalloffAttributeId, () => this.Entity.FalloffAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the falloff distance of the
    /// effect, if applicable.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the falloff
    /// distance of the effect, or <see langword="null" /> if no such attribute
    /// exists.
    /// </value>
    public AttributeId? FalloffAttributeId
    {
      get { return Entity.FalloffAttributeId; }
    }

    /// <summary>
    /// Gets the attribute that specifies the probability that a booster
    /// (i.e. drug) will produce a side effect, if applicable.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the probability
    /// that a booster (i.e. drug) will produce a side effect, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType FittingUsageChanceAttribute
    {
      get
      {
        Contract.Ensures(this.FittingUsageChanceAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.FittingUsageChanceAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.fittingUsageChanceAttribute, this.Entity.FittingUsageChanceAttributeId, () => this.Entity.FittingUsageChanceAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the probability that a booster
    /// (i.e. drug) will produce a side effect, if applicable.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the probability
    /// that a booster (i.e. drug) will produce a side effect, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? FittingUsageChanceAttributeId
    {
      get { return Entity.FittingUsageChanceAttributeId; }
    }

    /// <summary>
    /// Gets the unique identifier for the effect, if applicable.
    /// </summary>
    /// <value>
    /// The unique identifier for the effect.  Most effects seem not to have one.
    /// </value>
    public string Guid
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.Guid ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon
    {
      get
      {
        Contract.Ensures(this.IconId == null || Contract.Result<Icon>() != null);

        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.icon, this.Entity.IconId, () => this.Entity.Icon);
      }
    }

    /// <summary>
    /// Gets the ID of the icon associated with the attribute, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the attribute, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId
    {
      get { return Entity.IconId; }
    }

    /// <summary>
    /// Gets a value indicating whether the effect provides assistance to the
    /// target.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the effect provides assistance to the target;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool IsAssistance
    {
      get { return Entity.IsAssistance; }
    }

    /// <summary>
    /// Gets a value indicating whether the effect is considered aggression against
    /// the target.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the effect is considered aggression against the
    /// target; otherwise <see langword="false" />.
    /// </value>
    public bool IsOffensive
    {
      get { return Entity.IsOffensive; }
    }

    /// <summary>
    /// Gets a value indicating whether the effect can be used in warp.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the effect can be used in warp; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool IsWarpSafe
    {
      get { return Entity.IsWarpSafe; }
    }

    /// <summary>
    /// Gets attribute that specifies the probability that an NPC will
    /// activate this effect.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will activate this effect, or <see langword="null" /> if no
    /// such attribute exists.
    /// </value>
    public AttributeType NpcActivationChanceAttribute
    {
      get
      {
        Contract.Ensures(this.NpcActivationChanceAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.NpcActivationChanceAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.npcActivationChanceAttribute, this.Entity.NpcActivationChanceAttributeId, () => this.Entity.NpcActivationChanceAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the probability that an NPC will
    /// activate this effect.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will activate this effect, or <see langword="null" /> if no
    /// such attribute exists.
    /// </value>
    public AttributeId? NpcActivationChanceAttributeId
    {
      get { return Entity.NpcActivationChanceAttributeId; }
    }

    /// <summary>
    /// Gets the attribute that specifies the probability that an NPC will
    /// use this effect on a target.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will used this effect on a target, or <see langword="null" />
    /// if no such attribute exists.
    /// </value>
    public AttributeType NpcUsageChanceAttribute
    {
      get
      {
        Contract.Ensures(this.NpcUsageChanceAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.NpcUsageChanceAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.npcUsageChanceAttribute, this.Entity.NpcUsageChanceAttributeId, () => this.Entity.NpcUsageChanceAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the probability that an NPC will
    /// use this effect on a target.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will used this effect on a target, or <see langword="null" />
    /// if no such attribute exists.
    /// </value>
    public AttributeId? NpcUsageChanceAttributeId
    {
      get { return Entity.NpcUsageChanceAttributeId; }
    }

    /// <summary>
    /// Gets the post expression of the effect.  The meaning of this property is
    /// unknown, most likely internal.
    /// </summary>
    /// <value>
    /// The meaning of this property is unknown.
    /// </value>
    public int PostExpression
    {
      get { return Entity.PostExpression; }
    }

    /// <summary>
    /// Gets the pre expression of the effect.  The meaning of this property is
    /// unknown, most likely internal.
    /// </summary>
    /// <value>
    /// The meaning of this property is unknown.
    /// </value>
    public int PreExpression
    {
      get { return Entity.PreExpression; }
    }

    /// <summary>
    /// Gets a value indicating whether the effect can affect propulsion(?)
    /// Not used.  Only one test effect and no corresponding item types have this
    /// value set.
    /// </summary>
    /// <value>
    /// This property is not used.
    /// </value>
    public bool PropulsionChance
    {
      get { return Entity.PropulsionChance; }
    }

    /// <summary>
    /// Gets a value indicating whether the attribute is marked as published for
    /// public consumption.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the attribute is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published
    {
      get { return Entity.Published; }
    }

    /// <summary>
    /// Gets the attribute that specifies the range of the effect,
    /// if applicable.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the range
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType RangeAttribute
    {
      get
      {
        Contract.Ensures(this.RangeAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.RangeAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.rangeAttribute, this.Entity.RangeAttributeId, () => this.Entity.RangeAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the range of the effect,
    /// if applicable.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the range
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? RangeAttributeId
    {
      get { return Entity.RangeAttributeId; }
    }

    /// <summary>
    /// Gets a value indicating whether the effect can affect targeting range(?).
    /// The meaning of this property is not entirely clear.
    /// </summary>
    /// <value>
    /// The meaning of this property is unclear.
    /// </value>
    public bool RangeChance
    {
      get { return Entity.RangeChance; }
    }

    /// <summary>
    /// Gets the name of a sound effect?  This property appears to be unused.
    /// </summary>
    /// <value>
    /// The name of the associated sound effect, possibly.  The value is always
    /// either an empty string or "None".
    /// </value>
    public string SfxName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.SfxName ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the attribute that specifies the tracking speed of the
    /// effect, if applicable.
    /// </summary>
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the tracking
    /// speed of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType TrackingSpeedAttribute
    {
      get
      {
        Contract.Ensures(this.TrackingSpeedAttributeId == null || Contract.Result<AttributeType>() != null);

        if (this.TrackingSpeedAttributeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.trackingSpeedAttribute, this.Entity.TrackingSpeedAttributeId, () => this.Entity.TrackingSpeedAttribute);
      }
    }

    /// <summary>
    /// Gets the ID of the attribute that specifies the tracking speed of the
    /// effect, if applicable.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the tracking
    /// speed of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? TrackingSpeedAttributeId
    {
      get { return Entity.TrackingSpeedAttributeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(EffectType other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.DisplayName.CompareTo(other.DisplayName);
    }

    /// <inheritdoc />
    public int CompareTo(IEffect other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.DisplayName.CompareTo(other.EffectType.DisplayName);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.DisplayName;
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class EffectType : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      return this.CompareTo(obj as IEffect);
    }
  }
  #endregion

  #region IEffect Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEffect" /> interface.
  /// </content>
  public partial class EffectType : IEffect
  {
    EffectId IEffect.EffectId
    {
      get { return Id; }
    }

    EffectType IEffect.EffectType
    {
      get { return this; }
    }

    bool IEffect.IsDefault
    {
      get { return false; }
    }
  }
  #endregion
}