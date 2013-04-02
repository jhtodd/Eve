//-----------------------------------------------------------------------
// <copyright file="EffectType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about an effect associated with an EVE item.
  /// </summary>
  public class EffectType : BaseValue<EffectId, short, EffectTypeEntity, EffectType>,
                            IComparable,
                            IComparable<IEffect>,
                            IEffect,
                            IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Instance Fields
    private AttributeType _dischargeAttribute;
    private AttributeType _durationAttribute;
    private AttributeType _falloffAttribute;
    private AttributeType _fittingUsageChanceAttribute;
    private Icon _icon;
    private AttributeType _npcActivationChanceAttribute;
    private AttributeType _npcUsageChanceAttribute;
    private AttributeType _rangeAttribute;
    private AttributeType _trackingSpeedAttribute;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EffectType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal EffectType(EffectTypeEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether auto-repeat is disabled for items with
    /// this effect.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if auto-repeat is disabled; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool DisallowAutoRepeat {
      get {
        return Entity.DisallowAutoRepeat;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute that specifies the discharge rate of the effect, if 
    /// applicable.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the discharge rate of the
    /// effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType DischargeAttribute {
      get {
        if (_dischargeAttribute == null) {
          if (DischargeAttributeId != null) {

            // Load the cached version if available
            _dischargeAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(DischargeAttributeId, () => {
              AttributeTypeEntity dischargeAttributeEntity = Entity.DischargeAttribute;
              Contract.Assume(dischargeAttributeEntity != null);

              return new AttributeType(dischargeAttributeEntity);
            });
          }
        }

        return _dischargeAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the discharge rate of the
    /// effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the discharge
    /// rate of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? DischargeAttributeId {
      get {
        return Entity.DischargeAttributeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the display name of the effect.
    /// </summary>
    /// 
    /// <value>
    /// The human-readable display name of the effect.
    /// </value>
    public string DisplayName {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        return (string.IsNullOrWhiteSpace(Entity.DisplayName) ? Name : Entity.DisplayName);
      }
    }
    //******************************************************************************
    /// <summary>
    /// The meaning of this property is unknown.
    /// </summary>
    /// 
    /// <value>
    /// Unknown.
    /// </value>
    public short? Distribution {
      get {
        return Entity.Distribution;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute that specifies the duration of the effect,
    /// if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the duration
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType DurationAttribute {
      get {
        if (_durationAttribute == null) {
          if (DurationAttributeId != null) {

            // Load the cached version if available
            _durationAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(DurationAttributeId, () => {
              AttributeTypeEntity durationAttributeEntity = Entity.DurationAttribute;
              Contract.Assume(durationAttributeEntity != null);

              return new AttributeType(durationAttributeEntity);
            });
          }
        }

        return _durationAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the duration of the effect,
    /// if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the duration
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? DurationAttributeId {
      get {
        return Entity.DurationAttributeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the category of the effect.  The categories remain unknown.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the effect category.  The categories remain unknown.
    /// </value>
    public EffectCategoryId EffectCategoryId {
      get {
        return Entity.EffectCategoryId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the effect has a chance of target
    /// jamming another object.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the effect has a chance of target jamming;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool ElectronicChance {
      get {
        return Entity.ElectronicChance;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute that specifies the falloff distance of the
    /// effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the falloff
    /// distance of the effect, or <see langword="null" /> if no such attribute
    /// exists.
    /// </value>
    public AttributeType FalloffAttribute { 
      get {
        if (_falloffAttribute == null) {
          if (FalloffAttributeId != null) {

            // Load the cached version if available
            _falloffAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(FalloffAttributeId, () => {
              AttributeTypeEntity falloffAttributeEntity = Entity.FalloffAttribute;
              Contract.Assume(falloffAttributeEntity != null);

              return new AttributeType(falloffAttributeEntity);
            });
          }
        }

        return _falloffAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the falloff distance of the
    /// effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the falloff
    /// distance of the effect, or <see langword="null" /> if no such attribute
    /// exists.
    /// </value>
    public AttributeId? FalloffAttributeId {
      get {
        return Entity.FalloffAttributeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute that specifies the probability that a booster
    /// (i.e. drug) will produce a side effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the probability
    /// that a booster (i.e. drug) will produce a side effect, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType FittingUsageChanceAttribute {
      get {
        if (_fittingUsageChanceAttribute == null) {
          if (FittingUsageChanceAttributeId != null) {

            // Load the cached version if available
            _fittingUsageChanceAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(FittingUsageChanceAttributeId, () => {
              AttributeTypeEntity fittingUsageChanceAttributeEntity = Entity.FittingUsageChanceAttribute;
              Contract.Assume(fittingUsageChanceAttributeEntity != null);

              return new AttributeType(fittingUsageChanceAttributeEntity);
            });
          }
        }

        return _fittingUsageChanceAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the probability that a booster
    /// (i.e. drug) will produce a side effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the probability
    /// that a booster (i.e. drug) will produce a side effect, or
    /// <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? FittingUsageChanceAttributeId {
      get {
        return Entity.FittingUsageChanceAttributeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the unique identifier for the effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The unique identifier for the effect.  Most effects seem not to have one.
    /// </value>
    public string Guid {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        if (Entity.Guid == null) {
          return string.Empty;
        }

        return Entity.Guid;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon {
      get {
        if (_icon == null) {
          if (IconId != null) {

            // Load the cached version if available
            _icon = Eve.General.Cache.GetOrAdd<Icon>(IconId, () => {
              IconEntity iconEntity = Entity.Icon;
              Contract.Assume(iconEntity != null);

              return new Icon(iconEntity);
            });
          }
        }

        return _icon;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the icon associated with the attribute, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the attribute, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId {
      get {
        return Entity.IconId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the effect provides assistance to the
    /// target.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the effect provides assistance to the target;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool IsAssistance {
      get {
        return Entity.IsAssistance;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the effect is considered aggression against
    /// the target.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the effect is considered aggression against the
    /// target; otherwise <see langword="false" />.
    /// </value>
    public bool IsOffensive {
      get {
        return Entity.IsOffensive;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the effect can be used in warp.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the effect can be used in warp; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool IsWarpSafe {
      get {
        return Entity.IsWarpSafe;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets attribute that specifies the probability that an NPC will
    /// activate this effect.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will activate this effect, or <see langword="null" /> if no
    /// such attribute exists.
    /// </value>
    public AttributeType NpcActivationChanceAttribute {
      get {
        if (_npcActivationChanceAttribute == null) {
          if (NpcActivationChanceAttributeId != null) {

            // Load the cached version if available
            _npcActivationChanceAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(NpcActivationChanceAttributeId, () => {
              AttributeTypeEntity npcActivationChanceAttributeEntity = Entity.NpcActivationChanceAttribute;
              Contract.Assume(npcActivationChanceAttributeEntity != null);

              return new AttributeType(npcActivationChanceAttributeEntity);
            });
          }
        }

        return _npcActivationChanceAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the probability that an NPC will
    /// activate this effect.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will activate this effect, or <see langword="null" /> if no
    /// such attribute exists.
    /// </value>
    public AttributeId? NpcActivationChanceAttributeId {
      get {
        return Entity.NpcActivationChanceAttributeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute that specifies the probability that an NPC will
    /// use this effect on a target.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will used this effect on a target, or <see langword="null" />
    /// if no such attribute exists.
    /// </value>
    public AttributeType NpcUsageChanceAttribute {
      get {
        if (_npcUsageChanceAttribute == null) {
          if (NpcUsageChanceAttributeId != null) {

            // Load the cached version if available
            _npcUsageChanceAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(NpcUsageChanceAttributeId, () => {
              AttributeTypeEntity npcUsageChanceAttributeEntity = Entity.NpcUsageChanceAttribute;
              Contract.Assume(npcUsageChanceAttributeEntity != null);

              return new AttributeType(npcUsageChanceAttributeEntity);
            });
          }
        }

        return _npcUsageChanceAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the probability that an NPC will
    /// use this effect on a target.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the probability
    /// that an NPC will used this effect on a target, or <see langword="null" />
    /// if no such attribute exists.
    /// </value>
    public AttributeId? NpcUsageChanceAttributeId {
      get {
        return Entity.NpcUsageChanceAttributeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the post expression of the effect.  The value of this property is
    /// unknown, most likely internal.
    /// </summary>
    /// 
    /// <value>
    /// Unknown.
    /// </value>
    public int PostExpression {
      get {
        return Entity.PostExpression;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the pre expression of the effect.  The value of this property is
    /// unknown, most likely internal.
    /// </summary>
    /// 
    /// <value>
    /// Unknown.
    /// </value>
    public int PreExpression {
      get {
        return Entity.PreExpression;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Not used.  Only one test effect and no corresponding item types have this
    /// value set.
    /// </summary>
    /// 
    /// <value>
    /// Not used.
    /// </value>
    public bool PropulsionChance {
      get {
        return Entity.PropulsionChance;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the attribute is marked as published for
    /// public consumption.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the attribute is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published {
      get {
        return Entity.Published;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute that specifies the range of the effect,
    /// if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the range
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType RangeAttribute {
      get {
        if (_rangeAttribute == null) {
          if (RangeAttributeId != null) {

            // Load the cached version if available
            _rangeAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(RangeAttributeId, () => {
              AttributeTypeEntity rangeAttributeEntity = Entity.RangeAttribute;
              Contract.Assume(rangeAttributeEntity != null);

              return new AttributeType(rangeAttributeEntity);
            });
          }
        }

        return _rangeAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the range of the effect,
    /// if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the range
    /// of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? RangeAttributeId {
      get {
        return Entity.RangeAttributeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating something to do with targeting range.  The
    /// meaning of this property is not entirely clear.
    /// </summary>
    /// 
    /// <value>
    /// Unclear.
    /// </value>
    public bool RangeChance {
      get {
        return Entity.RangeChance;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the name of a sound effect?  This property appears to be unused.
    /// </summary>
    /// 
    /// <value>
    /// The name of the associated sound effect, possibly.  The value is always
    /// either an empty string or "None".
    /// </value>
    public string SfxName {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        if (Entity.SfxName == null) {
          return string.Empty;
        }

        return Entity.SfxName;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the attribute that specifies the tracking speed of the
    /// effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="AttributeType" /> that specifies the tracking
    /// speed of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeType TrackingSpeedAttribute {
      get {
        if (_trackingSpeedAttribute == null) {
          if (TrackingSpeedAttributeId != null) {

            // Load the cached version if available
            _trackingSpeedAttribute = Eve.General.Cache.GetOrAdd<AttributeType>(TrackingSpeedAttributeId, () => {
              AttributeTypeEntity trackingSpeedAttributeEntity = Entity.TrackingSpeedAttribute;
              Contract.Assume(trackingSpeedAttributeEntity != null);

              return new AttributeType(trackingSpeedAttributeEntity);
            });
          }
        }

        return _trackingSpeedAttribute;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the attribute that specifies the tracking speed of the
    /// effect, if applicable.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="AttributeType" /> that specifies the tracking
    /// speed of the effect, or <see langword="null" /> if no such attribute exists.
    /// </value>
    public AttributeId? TrackingSpeedAttributeId {
      get {
        return Entity.TrackingSpeedAttributeId;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public override int CompareTo(EffectType other) {
      if (other == null) {
        return 1;
      }

      return DisplayName.CompareTo(other.DisplayName);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(IEffect other) {
      if (other == null) {
        return 1;
      }

      return DisplayName.CompareTo(other.Type.DisplayName);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return DisplayName;
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      return CompareTo(obj as IEffect);
    }
    #endregion
    #region IEffect Members
    //******************************************************************************
    EffectId IEffect.Id {
      get {
        return Id;
      }
    }
    //******************************************************************************
    bool IEffect.IsDefault {
      get {
        return false;
      }
    }
    //******************************************************************************
    EffectType IEffect.Type {
      get {
        return this;
      }
    }
    #endregion
  }
}