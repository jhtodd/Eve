//-----------------------------------------------------------------------
// <copyright file="SkillLevel.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Utilities;

  using Eve.Data.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// Describes a level of a particular skill.
  /// </summary>
  /// 
  /// <remarks>
  /// <para>
  /// This class represents an abstract, disconnected skill and level that's
  /// not associated with a character or any other object.  For an actual
  /// skill associated with an EVE character, see the <see cref="Skill" />
  /// class.
  /// </para>
  /// </remarks>
  [Serializable]
  public class SkillLevel : IComparable,
                            IComparable<ISkill>,
                            IEquatable<SkillLevel>,
                            IHasIcon,
                            IKeyItem<SkillId>,
                            ISkill {

    #region Instance Fields
    private byte _level;
    private SkillId _skillId;
    
    [NonSerialized] private SkillType _skillType;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the SkillLevel class.
    /// </summary>
    /// 
    /// <param name="skillId">
    /// The ID of the skill.
    /// </param>
    /// 
    /// <param name="level">
    /// The level of the skill.
    /// </param>
    public SkillLevel(SkillId skillId, byte level) {
      Contract.Requires(level <= SkillType.MAX_SKILL_LEVEL, Resources.Messages.ISkill_LevelMustBeValid);

      _level = level;
      _skillId = skillId;
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_level >= 0);
      Contract.Invariant(_level <= 5);
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <inheritdoc />
    public byte Level {
      get {
        Contract.Ensures(Contract.Result<byte>() >= 0);
        Contract.Ensures(Contract.Result<byte>() <= SkillType.MAX_SKILL_LEVEL);
        return _level;
      }
    }
    //******************************************************************************
    /// <inheritdoc />
    public SkillId SkillId {
      get {
        return _skillId;
      }
    }
    //******************************************************************************
    /// <inheritdoc />
    public SkillType SkillType {
      get {
        Contract.Ensures(Contract.Result<SkillType>() != null);

        if (_skillType == null) {

          // Load the cached version if available
          _skillType = Eve.General.DataSource.GetEveTypeById<SkillType>((TypeId) (int) SkillId);
        }

        return _skillType;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(ISkill other) {
      if (other == null) {
        return 1;
      }

      int result = SkillType.Name.CompareTo(other.Type.Name);

      if (result == 0) {
        result = Level.CompareTo(other.Level);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as SkillLevel);
    }
    //******************************************************************************
    /// <summary>
    /// Returns a value indicating whether the current instance is equal to the
    /// specified object.
    /// </summary>
    /// 
    /// <param name="obj">
    /// The object to compare to the current object.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> is equal to the current
    /// instance; otherwise <see langword="false" />.
    /// </returns>
    public virtual bool Equals(SkillLevel other) {
      if (other == null) {
        return false;
      }

      return SkillId.Equals(other.SkillId) && Level.Equals(other.Level);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return CompoundHashCode.Create(SkillId, Level);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return SkillType.Name + ": " + Level.ToString();
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      ISkill other = obj as ISkill;
      return CompareTo(other);
    }
    #endregion
    #region IHasIcon Members
    //******************************************************************************
    Icon IHasIcon.Icon {
      get {
        return SkillType.Icon;
      }
    }
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get {
        return SkillType.IconId;
      }
    }
    #endregion
    #region IEveTypeInstance Members
    //******************************************************************************
    TypeId IEveTypeInstance.Id {
      get {
        return (TypeId) (int) SkillId;
      }
    }
    //******************************************************************************
    EveType IEveTypeInstance.Type {
      get {
        return SkillType;
      }
    }
    #endregion
    #region IKeyItem<SkillId> Members
    //******************************************************************************
    SkillId IKeyItem<SkillId>.Key {
      get {
        return SkillId;
      }
    }
    #endregion
    #region ISkill Members
    //******************************************************************************
    SkillId ISkill.Id {
      get {
        return SkillId;  
      }
    }
    //******************************************************************************
    byte ISkill.Level {
      get {
        return Level;
      }
    }
    //******************************************************************************
    int ISkill.SkillPoints {
      get {
        return SkillType.GetSkillPointsForLevel(Level, SkillType.Rank);
      }
    }
    //******************************************************************************
    SkillType ISkill.Type {
      get {
        return SkillType;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of skill levels.
  /// </summary>
  public class ReadOnlySkillLevelCollection : ReadOnlyKeyedCollection<SkillId, SkillLevel>,
                                              ISkillCollection {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlySkillLevelCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlySkillLevelCollection(IEnumerable<SkillLevel> contents) : base() {
      if (contents != null) {
        foreach (SkillLevel item in contents) {
          Items.AddWithoutCallback(item);
        }
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Gets the level of the specified skill, or 0 if no matching skill is found
    /// in the collection.
    /// </summary>
    /// 
    /// <param name="skillId">
    /// The ID of the skill whose level to retrieve.
    /// </param>
    /// 
    /// <returns>
    /// The level of the specified skill, or 0 if no matching skill is found in
    /// the collection.
    /// </returns>
    public byte GetSkillLevel(SkillId skillId) {
      SkillLevel skill;

      if (TryGetValue(skillId, out skill)) {
        Contract.Assume(skill != null);
        return skill.Level;
      }

      return 0;
    }
    #endregion

    #region IEnumerable<ISkill> Members
    //******************************************************************************
    IEnumerator<ISkill> IEnumerable<ISkill>.GetEnumerator() {
      return GetEnumerator();
    }
    #endregion
    #region IReadOnlyList<ISkill> Members
    //******************************************************************************
    ISkill IReadOnlyList<ISkill>.this[int index] {
      get {
        return this[index];
      }
    }
    #endregion
    #region ISkillCollection Members
    //******************************************************************************
    ISkill ISkillCollection.this[SkillId skillId] {
      get {
        var result = this[skillId];

        Contract.Assume(result != null);
        return result;
      }
    }
    //******************************************************************************
    bool ISkillCollection.TryGetValue(SkillId skillId, out ISkill value) {
      SkillLevel containedValue;

      bool success = TryGetValue(skillId, out containedValue);
      value = containedValue;

      Contract.Assume(!success || value != null);
      return success;
    }
    #endregion
  }
}