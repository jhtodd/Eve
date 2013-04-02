//-----------------------------------------------------------------------
// <copyright file="EveType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// The base class for EVE item types.
  /// </summary>
  public abstract class EveType : BaseValue<TypeId, int, EveTypeEntity, EveType>,
                                  IComparable,
                                  IComparable<IEveTypeInstance>,
                                  IHasAttributes,
                                  IHasEffects,
                                  IHasIcon,
                                  IEveTypeInstance {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Creates an appropriate type for the specified entity.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity.
    /// </param>
    /// 
    /// <returns>
    /// An <see cref="EveType" /> of the appropriate derived type, based on the
    /// contents of <paramref name="entity" />.
    /// </returns>
    public static EveType Create(EveTypeEntity entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
      Contract.Ensures(Contract.Result<EveType>() != null);

      // First, check for derived entity types.  These are entities that
      // have additional fields in ancillary tables.

      // StationTypeEntities always map to StationType
      StationTypeEntity stationTypeEntity = entity as StationTypeEntity;
      if (stationTypeEntity != null) {
        return new StationType(stationTypeEntity);
      }


      // For the remainder, the actual entity is the same type, and we need to
      // determine the EVE type based on its property values.

      // Use the item's group and category to determine the correct derived type
      Group group = Eve.General.DataSource.GetGroupById(entity.GroupId);

      switch (group.CategoryId) {

        // All items under category Skill map to SkillType
        case CategoryId.Skill:
          return new SkillType(entity);
      }

      // If we don't have a specific derived type for the provided entity, 
      // fall back on a GenericType wrapper.
      return new GenericType(entity);
    }
    #endregion

    #region Instance Fields
    private ReadOnlyAttributeValueCollection _attributes;
    private ReadOnlyEffectCollection _effects;
    private Group _group;
    private Icon _icon;
    private MarketGroup _marketGroup;
    private MetaGroup _metaGroup;
    private MetaType _metaType;
    private ReadOnlySkillLevelCollection _requiredSkills;
    private ReadOnlyTypeCollection _variations;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected EveType(EveTypeEntity entity) : base(entity) {
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
    /// Gets the collection of attributes which apply to the item.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyAttributeValueCollection" /> containing the attributes.
    /// </value>
    public ReadOnlyAttributeValueCollection Attributes {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyAttributeValueCollection>() != null);

        if (_attributes == null) {
          _attributes = new ReadOnlyAttributeValueCollection(Eve.General.DataSource.GetAttributeValues(x => x.ItemTypeId == this.Id.Value).OrderBy(x => x));
        }

        return _attributes;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the base price of the item.
    /// </summary>
    /// 
    /// <value>
    /// The base price of the item.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// This value is so inaccurate as to be almost meaningless in game terms.
    /// </para>
    /// </remarks>
    public decimal BasePrice {
      get {
        return Entity.BasePrice;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the cargo capacity of the item.
    /// </summary>
    /// 
    /// <value>
    /// The cargo capacity of the item.
    /// </value>
    public double Capacity {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Capacity;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the category to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Category" /> to which the item belongs.
    /// </value>
    public Category Category {
      get {
        Contract.Ensures(Contract.Result<Category>() != null);
        return Group.Category;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the category to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Category" /> to which the item belongs.
    /// </value>
    public CategoryId CategoryId {
      get {
        return Group.CategoryId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the chance of duplication of the item.  This value is not used.
    /// </summary>
    /// 
    /// <value>
    /// The chance of duplication of the item.
    /// </value>
    public double ChanceOfDuplicating {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.ChanceOfDuplicating;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of effects which apply to the item.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlyEffectCollection" /> containing the effects.
    /// </value>
    public ReadOnlyEffectCollection Effects {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyEffectCollection>() != null);

        if (_effects == null) {
          _effects = new ReadOnlyEffectCollection(Eve.General.DataSource.GetEffects(x => x.ItemTypeId == this.Id.Value).OrderBy(x => x));
        }

        return _effects;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Group" /> to which the item belongs.
    /// </value>
    public Group Group {
      get {
        Contract.Ensures(Contract.Result<Group>() != null);

        if (_group == null) {

          // Load the cached version if available
          _group = Eve.General.Cache.GetOrAdd<Group>(GroupId, () => {
            GroupEntity groupEntity = Entity.Group;
            Contract.Assume(groupEntity != null);

            return new Group(groupEntity);
          });
        }

        return _group;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="Group" /> to which the item belongs.
    /// </value>
    public GroupId GroupId {
      get {
        return Entity.GroupId;
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
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId {
      get {
        return Entity.IconId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the market group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    public virtual MarketGroup MarketGroup {
      get {
        if (_marketGroup == null) {
          if (MarketGroupId != null) {

            // Load the cached version if available
            _marketGroup = Eve.General.Cache.GetOrAdd<MarketGroup>(MarketGroupId, () => {
              MarketGroupEntity marketGroupEntity = Entity.MarketGroup;
              Contract.Assume(marketGroupEntity != null);

              return new MarketGroup(marketGroupEntity);
            });
          }
        }

        return _marketGroup;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the market group to which the item belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    public MarketGroupId? MarketGroupId {
      get {
        return Entity.MarketGroupId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the mass of the item.
    /// </summary>
    /// 
    /// <value>
    /// The mass of the item.
    /// </value>
    public double Mass {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Mass;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the meta group the current item is a member of.
    /// </summary>
    /// 
    /// <value>
    /// The meta group the current item is a member of.
    /// </value>
    public MetaGroup MetaGroup {
      get {
        Contract.Ensures(Contract.Result<MetaGroup>() != null);

        if (_metaGroup == null) {

          if (MetaType == null) {

            // Default to Tech I if no meta type information is available
            _metaGroup = Eve.General.DataSource.GetMetaGroupById(MetaGroupId.TechI);

          } else {
            _metaGroup = MetaType.MetaGroup;
          }
        }

        return _metaGroup;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the meta group the current item is a member of.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the meta group the current item is a member of.
    /// </value>
    public MetaGroupId MetaGroupId {
      get {
        if (MetaType != null) {
          return MetaType.MetaGroupId;
        }

        // Default to Tech I if no meta type information is available
        return MetaGroupId.TechI;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the meta level of the item.
    /// </summary>
    /// 
    /// <value>
    /// The meta level of the item, or 0 if no meta level is specified.
    /// </value>
    public int MetaLevel {
      get {
        return Attributes.GetAttributeValue<int>(AttributeId.MetaLevel, 0);
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets information related to the meta group of the current item.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="MetaType" /> providing information related to the meta group
    /// of the current item, or <see langword="null" /> if no meta type exists.
    /// </value>
    public MetaType MetaType {
      get {
        if (_metaType == null) {

          if (Entity.MetaType != null) {
            _metaType = new MetaType(Entity.MetaType);
          }
        }

        return _metaType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current item is associated with, or <see cref="null" /> if the
    /// item is not associated with any races.
    /// </value>
    public RaceId? RaceId {
      get {
        return Entity.RaceId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of items which constitute one "lot."  A lot is the number of
    /// items produced in a manufacturing job, or that must be stacked in order to
    /// be reprocessed.
    /// </summary>
    /// 
    /// <value>
    /// The number of items which constitute one "lot."
    /// </value>
    public int PortionSize {
      get {
        return Entity.PortionSize;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the item is marked as published for
    /// public consumption.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the item is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published {
      get {
        return Entity.Published;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of skills required to use the item.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="ReadOnlySkillLevelCollection" /> containing information
    /// about the skills required to use the item.
    /// </value>
    public ReadOnlySkillLevelCollection RequiredSkills {
      get {
        Contract.Ensures(Contract.Result<ReadOnlySkillLevelCollection>() != null);

        if (_requiredSkills == null) {
          List<SkillLevel> skills = new List<SkillLevel>();

          AttributeValue skillAttribute;
          AttributeValue levelAttribute;

          // Add the first skill
          if (Attributes.TryGetValue(AttributeId.RequiredSkill1, out skillAttribute) &&
              Attributes.TryGetValue(AttributeId.RequiredSkill1Level, out levelAttribute)) {

            Contract.Assume(skillAttribute != null);
            Contract.Assume(levelAttribute != null);
            Contract.Assert(Enum.IsDefined(typeof(SkillId), (SkillId) skillAttribute.BaseValue));

            SkillId skillId = (SkillId) skillAttribute.BaseValue;
            byte skillLevel = (byte) levelAttribute.BaseValue;
            Contract.Assume(skillLevel <= SkillType.MAX_SKILL_LEVEL);

            // Some items have duplicate skills
            if (!skills.Any(x => x.SkillId == skillId)) {
              skills.Add(new SkillLevel(skillId, skillLevel));
            }
          }

          // Add the second skill
          if (Attributes.TryGetValue(AttributeId.RequiredSkill2, out skillAttribute) &&
              Attributes.TryGetValue(AttributeId.RequiredSkill2Level, out levelAttribute)) {

            Contract.Assume(skillAttribute != null);
            Contract.Assume(levelAttribute != null);
            Contract.Assert(Enum.IsDefined(typeof(SkillId), (SkillId) skillAttribute.BaseValue));

            SkillId skillId = (SkillId) skillAttribute.BaseValue;
            byte skillLevel = (byte) levelAttribute.BaseValue;
            Contract.Assume(skillLevel <= SkillType.MAX_SKILL_LEVEL);

            // Some items have duplicate skills
            if (!skills.Any(x => x.SkillId == skillId)) {
              skills.Add(new SkillLevel(skillId, skillLevel));
            }
          }

          // Add the third skill
          if (Attributes.TryGetValue(AttributeId.RequiredSkill3, out skillAttribute) &&
              Attributes.TryGetValue(AttributeId.RequiredSkill3Level, out levelAttribute)) {

            Contract.Assume(skillAttribute != null);
            Contract.Assume(levelAttribute != null);
            Contract.Assert(Enum.IsDefined(typeof(SkillId), (SkillId) skillAttribute.BaseValue));

            SkillId skillId = (SkillId) skillAttribute.BaseValue;
            byte skillLevel = (byte) levelAttribute.BaseValue;
            Contract.Assume(skillLevel <= SkillType.MAX_SKILL_LEVEL);

            // Some items have duplicate skills
            if (!skills.Any(x => x.SkillId == skillId)) {
              skills.Add(new SkillLevel(skillId, skillLevel));
            }
          }

          // Add the fourth skill
          if (Attributes.TryGetValue(AttributeId.RequiredSkill4, out skillAttribute) &&
              Attributes.TryGetValue(AttributeId.RequiredSkill4Level, out levelAttribute)) {

            Contract.Assume(skillAttribute != null);
            Contract.Assume(levelAttribute != null);
            Contract.Assert(Enum.IsDefined(typeof(SkillId), (SkillId) skillAttribute.BaseValue));

            SkillId skillId = (SkillId) skillAttribute.BaseValue;
            byte skillLevel = (byte) levelAttribute.BaseValue;
            Contract.Assume(skillLevel <= SkillType.MAX_SKILL_LEVEL);

            // Some items have duplicate skills
            if (!skills.Any(x => x.SkillId == skillId)) {
              skills.Add(new SkillLevel(skillId, skillLevel));
            }
          }

          // Add the fifth skill
          if (Attributes.TryGetValue(AttributeId.RequiredSkill5, out skillAttribute) &&
              Attributes.TryGetValue(AttributeId.RequiredSkill5Level, out levelAttribute)) {

            Contract.Assume(skillAttribute != null);
            Contract.Assume(levelAttribute != null);
            Contract.Assert(Enum.IsDefined(typeof(SkillId), (SkillId) skillAttribute.BaseValue));

            SkillId skillId = (SkillId) skillAttribute.BaseValue;
            byte skillLevel = (byte) levelAttribute.BaseValue;
            Contract.Assume(skillLevel <= SkillType.MAX_SKILL_LEVEL);

            // Some items have duplicate skills
            if (!skills.Any(x => x.SkillId == skillId)) {
              skills.Add(new SkillLevel(skillId, skillLevel));
            }
          }

          // Add the sixth skill
          if (Attributes.TryGetValue(AttributeId.RequiredSkill6, out skillAttribute) &&
              Attributes.TryGetValue(AttributeId.RequiredSkill6Level, out levelAttribute)) {

            Contract.Assume(skillAttribute != null);
            Contract.Assume(levelAttribute != null);
            Contract.Assert(Enum.IsDefined(typeof(SkillId), (SkillId) skillAttribute.BaseValue));

            SkillId skillId = (SkillId) skillAttribute.BaseValue;
            byte skillLevel = (byte) levelAttribute.BaseValue;
            Contract.Assume(skillLevel <= SkillType.MAX_SKILL_LEVEL);

            // Some items have duplicate skills
            if (!skills.Any(x => x.SkillId == skillId)) {
              skills.Add(new SkillLevel(skillId, skillLevel));
            }
          }

          _requiredSkills = new ReadOnlySkillLevelCollection(skills.ToArray());
        }

        return _requiredSkills;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of variations of the item.
    /// </summary>
    /// 
    /// <value>
    /// The collection of items which are variations of the current item type.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// The collection will include the current item type.
    /// </para>
    /// </remarks>
    public ReadOnlyTypeCollection Variations {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyTypeCollection>() != null);

        if (_variations == null) {

          List<EveType> variations = new List<EveType>();

          // If the meta type is not null, then this is one variant of a parent
          // item -- it and its children are the variants.
          if (MetaType != null) {
            variations.Add(MetaType.ParentType);

            foreach (EveType type in Eve.General.DataSource.GetEveTypes(x => x.MetaType.ParentTypeId == MetaType.ParentTypeId.Value)) {
              variations.Add(type);
            }

          // If the meta type is null, then the current item is the parent --
          // load its children
          } else {

            variations.Add(this);

            foreach (EveType type in Eve.General.DataSource.GetEveTypes(x => x.MetaType.ParentTypeId == this.Id.Value)) {
              variations.Add(type);
            }
          }

          variations.Sort((x, y) => {
            int result = x.MetaGroupId.CompareTo(y.MetaGroupId);

            if (result == 0) {
              result = x.CompareTo(y);
            }

            return result;
          });

          _variations = new ReadOnlyTypeCollection(variations.ToArray());
        }

        return _variations;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the volume of the item.
    /// </summary>
    /// 
    /// <value>
    /// The volume of the item.
    /// </value>
    public double Volume {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Volume;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(IEveTypeInstance other) {
      if (other == null) {
        return 1;
      }

      return Name.CompareTo(other.Type.Name);
    }
    //******************************************************************************
    /// <summary>
    /// Returns a value indicating whether the specified character has the skills
    /// needed to use the item.
    /// </summary>
    /// 
    /// <param name="character">
    /// The character to test against the skill requirements.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if <paramref name="character" /> meets the skill
    /// requirements to use the item; otherwise <see langword="false" />.
    /// </returns>
    public bool MeetsSkillRequirements(IHasSkills character) {
      Contract.Requires(character != null, Resources.Messages.EveType_MeetsSkillRequirementsCharacterCannotBeNull);

      foreach (SkillLevel requiredSkill in RequiredSkills) {
        Contract.Assume(requiredSkill != null);

        ISkill characterSkill;

        if (!character.Skills.TryGetValue(requiredSkill.SkillId, out characterSkill)) {
          return false;
        }

        if (characterSkill.Level < requiredSkill.Level) {
          return false;
        }
      }

      return true;
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      return CompareTo(obj as IEveTypeInstance);
    }
    #endregion
    #region IHasAttributes Members
    //******************************************************************************
    IAttributeCollection IHasAttributes.Attributes {
      get {
        return Attributes;
      }
    }
    #endregion
    #region IHasEffects Members
    //******************************************************************************
    IEffectCollection IHasEffects.Effects {
      get {
        return Effects;
      }
    }
    #endregion
    #region IEveTypeInstance Members
    //******************************************************************************
    TypeId IEveTypeInstance.Id {
      get {
        return Id;
      }
    }
    //******************************************************************************
    EveType IEveTypeInstance.Type {
      get {
        return this;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of item types.
  /// </summary>
  public class ReadOnlyTypeCollection : ReadOnlyCollection<EveType> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyTypeCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyTypeCollection(IEnumerable<EveType> contents) : base() {
      if (contents != null) {
        foreach (EveType item in contents) {
          Items.AddWithoutCallback(item);
        }
      }
    }
    #endregion
  }
}