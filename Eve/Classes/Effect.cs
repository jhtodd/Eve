//-----------------------------------------------------------------------
// <copyright file="Effect.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  using Eve.Data.Entities;
  using Eve.Meta;

  //******************************************************************************
  /// <summary>
  /// Contains information about an effect associated with an EVE item.
  /// </summary>
  public class Effect : EntityAdapter<EffectEntity>,
                                      IComparable,
                                      IComparable<IEffect>,
                                      IEffect,
                                      IEquatable<Effect>,
                                      IHasIcon,
                                      IHasId<long>,
                                      IKeyItem<EffectId> {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// 
    /// <param name="itemTypeId">
    /// The item type ID.
    /// </param>
    /// 
    /// <param name="effectId">
    /// The effect ID.
    /// </param>
    /// 
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(TypeId itemTypeId, EffectId effectId) {
      return (long) ((((ulong) (long) itemTypeId) << 32) | ((ulong) (long) effectId));
    }
    #endregion

    #region Instance Fields
    private EffectType _effectType;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Effect class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public Effect(EffectEntity entity) : base(entity) {
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
    /// Gets the ID of the effect the current value applies to.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the effect the current value applies to.
    /// </value>
    public EffectId Id {
      get {
        return (EffectId) Entity.EffectId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the effect type to which the value applies.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="EffectType" /> to which the value applies.
    /// </value>
    public EffectType Type {
      get {
        Contract.Ensures(Contract.Result<EffectType>() != null);

        if (_effectType == null) {

          // Load the cached version if available
          _effectType = Eve.General.Cache.GetOrAdd<EffectType>(Id, () => {
            EffectTypeEntity effectTypeEntity = Entity.EffectType;
            Contract.Assume(effectTypeEntity != null);

            return new EffectType(effectTypeEntity);
          });
        }

        return _effectType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether it is the default effect for the item.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the effect is the default effect for the item;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool IsDefault {
      get {
        return Entity.IsDefault;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(IEffect other) {
      if (other == null) {
        return 1;
      }

      // Put default effects at the top
      int result = other.IsDefault.CompareTo(IsDefault);

      if (result == 0) {
        result = Type.CompareTo(other.Type);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as Effect);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(Effect other) {
      if (other == null) {
        return false;
      }

      return Id.Equals(other.Id) && IsDefault.Equals(other.IsDefault);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return CompoundHashCode.Create(Id, IsDefault);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return ToString(string.Empty);
    }
    //******************************************************************************
    /// <inheritdoc />
    /// 
    /// <param name="format">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the effect value.
    /// </returns>
    public virtual string ToString(string format) {
      Contract.Ensures(Contract.Result<string>() != null);

      // Format the value according to the effect
      return Type.DisplayName + (IsDefault ? " (default)" : string.Empty);
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      Effect other = obj as Effect;
      return CompareTo(other);
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
        return IsDefault;
      }
    }
    //******************************************************************************
    EffectType IEffect.Type {
      get {
        return Type;
      }
    }
    #endregion
    #region IHasIcon Members
    //******************************************************************************
    Icon IHasIcon.Icon {
      get {
        return Type.Icon;
      }
    }
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get {
        return Type.IconId;
      }
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get {
        return ((IHasId<long>) this).Id;
      }
    }
    #endregion
    #region IHasId<long> Members
    //******************************************************************************
    long IHasId<long>.Id {
      get {
        return CreateCompoundId(Entity.ItemTypeId, Id);
      }
    }
    #endregion
    #region IKeyItem<EffectId> Members
    //******************************************************************************
    EffectId IKeyItem<EffectId>.Key {
      get {
        return Id;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of effects.
  /// </summary>
  public class ReadOnlyEffectCollection : ReadOnlyKeyedCollection<EffectId, Effect>,
                                          IEffectCollection {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyEffectCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyEffectCollection(IEnumerable<Effect> contents) : base() {
      if (contents != null) {
        foreach (Effect effect in contents) {
          Items.AddWithoutCallback(effect);
        }
      }
    }
    #endregion

    #region IEffectCollection Members
    //******************************************************************************
    IEffect IEffectCollection.this[EffectId effectId] {
      get {
        var result = this[effectId];

        Contract.Assume(result != null);
        return result;
      }
    }
    //******************************************************************************
    bool IEffectCollection.TryGetValue(EffectId effectId, out IEffect value) {
      Effect containedValue;

      bool success = TryGetValue(effectId, out containedValue);
      value = containedValue;

      Contract.Assume(!success || value != null);
      return success;
    }
    #endregion
    #region IEnumerator<IEffect> Members
    //******************************************************************************
    IEnumerator<IEffect> IEnumerable<IEffect>.GetEnumerator() {
      return GetEnumerator();
    }
    #endregion
    #region IReadOnlyList<IEffect> Members
    //******************************************************************************
    IEffect IReadOnlyList<IEffect>.this[int index] {
      get {
        return this[index];
      }
    }
    #endregion
  }
}