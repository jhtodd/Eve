//-----------------------------------------------------------------------
// <copyright file="EveType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet.Collections;

  /// <summary>
  /// The base class for EVE item types.
  /// </summary>
  /// <remarks>
  /// <para>
  /// The <c>EveType</c> class represents an item described by the <c>invTypes</c>
  /// table, possibly incorporating data from other related tables, depending on
  /// the type's category.
  /// </para>
  /// <para>
  /// Use the <see cref="Create" /> static method to create instances of types
  /// derived from this class.
  /// </para>
  /// </remarks>
  [EveCacheDomain(typeof(EveType))]
  public abstract partial class EveType 
    : BaseValue<TypeId, int, EveTypeEntity, EveType>,
      IComparable,
      IComparable<IEveTypeInstance>,
      IDisposable,
      IHasAttributes,
      IHasEffects,
      IHasIcon,
      IEveTypeInstance
  {
    // This is the list of attributes specifying which skills are required for a type.
    // This list may need to be expanded later if additional required skill attributes
    // are added.
    private static readonly AttributeId[] RequiredSkillIdAttributes = 
    {
      AttributeId.RequiredSkill1,
      AttributeId.RequiredSkill2,
      AttributeId.RequiredSkill3,
      AttributeId.RequiredSkill4,
      AttributeId.RequiredSkill5,
      AttributeId.RequiredSkill6,
    };

    private static readonly AttributeId[] RequiredSkillLevelAttributes =
    {
      AttributeId.RequiredSkill1Level,
      AttributeId.RequiredSkill2Level,
      AttributeId.RequiredSkill3Level,
      AttributeId.RequiredSkill4Level,
      AttributeId.RequiredSkill5Level,
      AttributeId.RequiredSkill6Level,
    };

    private ReadOnlyAttributeValueCollection attributes;
    private ReadOnlyEffectCollection effects;
    private Graphic graphic;
    private Group group;
    private Icon icon;
    private MarketGroup marketGroup;
    private MetaGroup metaGroup;
    private MetaType metaType;
    private ReadOnlySkillLevelCollection requiredSkills;
    private ReadOnlyEveTypeCollection variations;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveType class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected EveType(IEveRepository repository, EveTypeEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of attributes which apply to the item.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlyAttributeValueCollection" /> containing the attributes.
    /// </value>
    public ReadOnlyAttributeValueCollection Attributes
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAttributeValueCollection>() != null);

        // If not already set, construct a collection of this type's attribute values.
        return EveType.LazyInitialize(
          ref this.attributes,
          () => ReadOnlyAttributeValueCollection.Create(this.Repository, this.Entity.Attributes));
      }
    }

    /// <summary>
    /// Gets the base price of the item.
    /// </summary>
    /// <value>
    /// The base price of the item.
    /// </value>
    /// <remarks>
    /// <para>
    /// This value is so inaccurate as to be almost meaningless in game terms.
    /// </para>
    /// </remarks>
    public decimal BasePrice
    {
      get { return Entity.BasePrice; }
    }

    /// <summary>
    /// Gets the cargo capacity of the item.
    /// </summary>
    /// <value>
    /// The cargo capacity of the item.
    /// </value>
    public double Capacity
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Capacity;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the category to which the item belongs.
    /// </summary>
    /// <value>
    /// The <see cref="Category" /> to which the item belongs.
    /// </value>
    public Category Category
    {
      get
      {
        Contract.Ensures(Contract.Result<Category>() != null);
        return Group.Category;
      }
    }

    /// <summary>
    /// Gets the ID of the category to which the item belongs.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Category" /> to which the item belongs.
    /// </value>
    public CategoryId CategoryId
    {
      get { return Group.CategoryId; }
    }

    /// <summary>
    /// Gets the chance of duplication of the item.  This value is not used.
    /// </summary>
    /// <value>
    /// The chance of duplication of the item.
    /// </value>
    public double ChanceOfDuplicating
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.ChanceOfDuplicating;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the collection of effects which apply to the item.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlyEffectCollection" /> containing the effects.
    /// </value>
    public ReadOnlyEffectCollection Effects
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyEffectCollection>() != null);

        // If not already set, construct a collection of this type's effects.
        return EveType.LazyInitialize(
          ref this.effects,
          () => ReadOnlyEffectCollection.Create(this.Repository, this.Entity.Effects));
      }
    }

    /// <summary>
    /// Gets the graphic for the the item.
    /// </summary>
    /// <value>
    /// The <see cref="Graphic" /> for the item.
    /// </value>
    public Graphic Graphic
    {
      get
      {
        Contract.Ensures(this.GraphicId == null || Contract.Result<Graphic>() != null);

        if (this.GraphicId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.graphic, this.Entity.GraphicId, () => this.Entity.Graphic);
      }
    }

    /// <summary>
    /// Gets the ID of the graphic for the item.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Graphic" /> for the item.
    /// </value>
    public GraphicId? GraphicId
    {
      get { return Entity.GraphicId; }
    }

    /// <summary>
    /// Gets the group to which the item belongs.
    /// </summary>
    /// <value>
    /// The <see cref="Group" /> to which the item belongs.
    /// </value>
    public Group Group
    {
      get
      {
        Contract.Ensures(Contract.Result<Group>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.group, this.Entity.GroupId, () => this.Entity.Group);
      }
    }

    /// <summary>
    /// Gets the ID of the group to which the item belongs.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Group" /> to which the item belongs.
    /// </value>
    public GroupId GroupId
    {
      get { return Entity.GroupId; }
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
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId
    {
      get { return Entity.IconId; }
    }

    /// <summary>
    /// Gets the market group to which the item belongs.
    /// </summary>
    /// <value>
    /// The <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    public virtual MarketGroup MarketGroup
    {
      get
      {
        Contract.Ensures(this.MarketGroupId == null || Contract.Result<MarketGroup>() != null);

        if (this.MarketGroupId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.marketGroup, this.Entity.MarketGroupId, () => this.Entity.MarketGroup);
      }
    }

    /// <summary>
    /// Gets the ID of the market group to which the item belongs.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="MarketGroup" /> to which the item belongs.
    /// </value>
    public MarketGroupId? MarketGroupId
    {
      get { return Entity.MarketGroupId; }
    }

    /// <summary>
    /// Gets the mass of the item.
    /// </summary>
    /// <value>
    /// The mass of the item.
    /// </value>
    public double Mass
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Mass;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the meta group the current item is a member of.
    /// </summary>
    /// <value>
    /// The meta group the current item is a member of.
    /// </value>
    public MetaGroup MetaGroup
    {
      get
      {
        if (this.MetaGroupId == null)
        {
          return null;
        }

        return EveType.LazyInitialize(ref this.metaGroup, () => this.MetaType.MetaGroup);
      }
    }

    /// <summary>
    /// Gets the ID of the meta group the current item is a member of.
    /// </summary>
    /// <value>
    /// The ID of the meta group the current item is a member of.
    /// </value>
    public MetaGroupId? MetaGroupId
    {
      get
      {
        // Default to Tech I if no meta type information is available
        return (MetaType != null) ? MetaType.MetaGroupId : (MetaGroupId?)null;
      }
    }

    /// <summary>
    /// Gets the meta level of the item.
    /// </summary>
    /// <value>
    /// The meta level of the item, or 0 if no meta level is specified.
    /// </value>
    public int MetaLevel
    {
      get { return this.Attributes.GetAttributeValue<int>(AttributeId.MetaLevel, 0); }
    }

    /// <summary>
    /// Gets information related to the meta group of the current item.
    /// </summary>
    /// <value>
    /// A <see cref="MetaType" /> providing information related to the meta group
    /// of the current item, or <see langword="null" /> if no meta type exists.
    /// </value>
    public MetaType MetaType
    {
      get
      {
        if (Entity.MetaType == null)
        {
          return null;
        }

        // If not already set, create an instance from the base entity.  Do not cache, because the
        // MetaType only has relevance to the current EveType, and that will be cached already.
        return LazyInitializer.EnsureInitialized(ref this.metaType, () => this.Entity.MetaType.ToAdapter(this.Repository));
      }
    }

    /// <summary>
    /// Gets the number of items which constitute one "lot."  A lot is the number of
    /// items produced in a manufacturing job, or that must be stacked in order to
    /// be reprocessed.
    /// </summary>
    /// <value>
    /// The number of items which constitute one "lot".
    /// </value>
    public int PortionSize
    {
      get { return Entity.PortionSize; }
    }

    /// <summary>
    /// Gets a value indicating whether the item is marked as published for
    /// public consumption.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published
    {
      get { return Entity.Published; }
    }

    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the item, if any.
    /// </summary>
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current item is associated with, or <see langword="null" /> if the
    /// item is not associated with any races.
    /// </value>
    public RaceId? RaceId
    {
      get { return Entity.RaceId; }
    }

    /// <summary>
    /// Gets the radius of the item.
    /// </summary>
    /// <value>
    /// The radius of the item.
    /// </value>
    public double Radius
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Radius;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the collection of skills required to use the item.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlySkillLevelCollection" /> containing information
    /// about the skills required to use the item.
    /// </value>
    public ReadOnlySkillLevelCollection RequiredSkills
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySkillLevelCollection>() != null);

        return EveType.LazyInitialize(
          ref this.requiredSkills,
          () =>
          {
            List<SkillLevel> skills = new List<SkillLevel>(RequiredSkillIdAttributes.Length);

            AttributeValue skillAttribute;
            AttributeValue levelAttribute;

            // Iterate through the static array of required skill attribute IDs,
            // adding each one to the list as needed.
            for (int i = 0; i < RequiredSkillIdAttributes.Length; i++)
            {
              if (this.Attributes.TryGetValue(RequiredSkillIdAttributes[i], out skillAttribute) &&
                  this.Attributes.TryGetValue(RequiredSkillLevelAttributes[i], out levelAttribute))
              {
                Contract.Assume(skillAttribute != null);
                Contract.Assume(levelAttribute != null);

                // As of 84566, a handful of types have invalid skill IDs listed
                // as requirements, so we have to check
                if (Enum.IsDefined(typeof(SkillId), (SkillId)skillAttribute.BaseValue))
                {
                  SkillId skillId = (SkillId)skillAttribute.BaseValue;
                  byte skillLevel = (byte)levelAttribute.BaseValue;
                  Contract.Assume(skillLevel <= SkillType.MaxSkillLevel);

                  // Some items have duplicate skills
                  if (!skills.Any(x => x.SkillId == skillId))
                  {
                    skills.Add(new SkillLevel(this.Repository, skillId, skillLevel));
                  }
                }
              }
            }

            return ReadOnlySkillLevelCollection.Create(this.Repository, skills);
          });
      }
    }

    /// <summary>
    /// Gets the ID of the sound associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the sound associated with the item, or
    /// <see langword="null" /> if no such icon exists.  The meaning of
    /// this property is unknown.
    /// </value>
    public SoundId? SoundId
    {
      get { return Entity.SoundId; }
    }

    /// <summary>
    /// Gets the collection of variations of the item.
    /// </summary>
    /// <value>
    /// The collection of items which are variations of the current item type.
    /// </value>
    /// <remarks>
    /// <para>
    /// The collection will include the current item type.
    /// </para>
    /// </remarks>
    public ReadOnlyEveTypeCollection Variations
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyEveTypeCollection>() != null);

        return EveType.LazyInitialize(
          ref this.variations,
          () => 
          {
            List<EveType> variations = new List<EveType>(20);

            // If the meta type is not null, then this is one variant of a parent
            // item -- it and its children are the variants.
            if (MetaType != null)
            {
              variations.Add(MetaType.ParentType);

              foreach (EveType type in this.Repository.GetEveTypes(q => q.Where(x => x.MetaType.ParentTypeId == MetaType.ParentTypeId.Value)))
              {
                variations.Add(type);
              }

            // If the meta type is null, then the current item is the parent --
            // load its children
            }
            else
            {
              variations.Add(this);

              foreach (EveType type in this.Repository.GetEveTypes(q => q.Where(x => x.MetaType.ParentTypeId == this.Id.Value)))
              {
                variations.Add(type);
              }
            }

            variations.Sort(
              (x, y) =>
              {
                int compareResult = Nullable.Compare(x.MetaGroupId, y.MetaGroupId);

                if (compareResult == 0)
                {
                  compareResult = x.CompareTo(y);
                }

                return compareResult;
              });

            return ReadOnlyEveTypeCollection.Create(this.Repository, variations);
          });
      }
    }

    /// <summary>
    /// Gets the volume of the item.
    /// </summary>
    /// <value>
    /// The volume of the item.
    /// </value>
    public double Volume
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.Volume;
        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /* Methods */

    /// <summary>
    /// Creates an appropriate type for the specified entity.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity.
    /// </param>
    /// <returns>
    /// An <see cref="EveType" /> of the appropriate derived type, based on the
    /// contents of <paramref name="entity" />.
    /// </returns>
    public static EveType Create(IEveRepository repository, EveTypeEntity entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Ensures(Contract.Result<EveType>() != null);

      // First, check for derived entity types.  These are entities that
      // have additional fields in ancillary tables.

      // StationTypeEntities always map to StationType
      StationTypeEntity stationTypeEntity = entity as StationTypeEntity;
      if (stationTypeEntity != null)
      {
        return new StationType(repository, stationTypeEntity);
      }

      // For the remainder, the actual entity is the same type, and we need to
      // determine the EVE type based on its property values.

      // Use the item's group and category to determine the correct derived type
      Group group = repository.GetGroupById(entity.GroupId);

      switch (group.CategoryId)
      {
        // All items under category Skill map to SkillType
        case CategoryId.Skill:
          return new SkillType(repository, entity);
      }

      // If we don't have a specific derived type for the provided entity, 
      // fall back on a GenericType wrapper.
      return new GenericType(repository, entity);
    }

    /// <inheritdoc />
    public virtual int CompareTo(IEveTypeInstance other)
    {
      if (other == null)
      {
        return 1;
      }

      return Name.CompareTo(other.Type.Name);
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    /// Returns a value indicating whether the specified character meets the
    /// prerequisites necessary to use the item.
    /// </summary>
    /// <param name="character">
    /// The character to test against the skill requirements.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="character" /> meets the skill
    /// requirements to use the item; otherwise <see langword="false" />.
    /// </returns>
    public bool MeetsPrerequisites(IHasSkills character)
    {
      Contract.Requires(character != null, Resources.Messages.EveType_MeetsSkillRequirementsCharacterCannotBeNull);

      foreach (SkillLevel requiredSkill in this.RequiredSkills)
      {
        Contract.Assume(requiredSkill != null);

        ISkill characterSkill;

        if (!character.Skills.TryGetValue(requiredSkill.SkillId, out characterSkill))
        {
          return false;
        }

        if (characterSkill.Level < requiredSkill.Level)
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    /// <param name="disposing">Specifies whether to dispose managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.attributes != null)
        {
          this.attributes.Dispose();
        }

        if (this.effects != null)
        {
          this.effects.Dispose();
        }

        if (this.requiredSkills != null)
        {
          this.requiredSkills.Dispose();
        }

        if (this.variations != null)
        {
          this.variations.Dispose();
        }
      }
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public abstract partial class EveType : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      return this.CompareTo(obj as IEveTypeInstance);
    }
  }
  #endregion

  #region IHasAttributes Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasAttributes" /> interface.
  /// </content>
  public abstract partial class EveType : IHasAttributes
  {
    IAttributeCollection IHasAttributes.Attributes
    {
      get { return this.Attributes; }
    }
  }
  #endregion

  #region IHasEffects Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasEffects" /> interface.
  /// </content>
  public abstract partial class EveType : IHasEffects
  {
    IEffectCollection IHasEffects.Effects
    {
      get { return this.Effects; }
    }
  }
  #endregion

  #region IEveTypeInstance Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveTypeInstance" /> interface.
  /// </content>
  public abstract partial class EveType : IEveTypeInstance
  {
    TypeId IEveTypeInstance.Id
    {
      get { return Id; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Value is accessible via the this (or base) keyword.")]
    EveType IEveTypeInstance.Type
    {
      get { return this; }
    }
  }
  #endregion
}