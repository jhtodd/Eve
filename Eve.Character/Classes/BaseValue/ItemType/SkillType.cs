//-----------------------------------------------------------------------
// <copyright file="SkillType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// Contains information about the type of a skill belonging to an EVE
  /// character.
  /// </summary>
  public sealed partial class SkillType 
    : EveType,
      IComparable,
      IComparable<ISkill>,
      ISkill
  {
    /// <summary>The maximum skill level.</summary>
    internal const int MaxSkillLevel = 5;

    private CharacterAttributeType primaryAttribute;
    private CharacterAttributeType secondaryAttribute;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SkillType class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal SkillType(EveTypeEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }

    /* Properties */
    
    /// <summary>
    /// Gets a value indicating whether the skill can be trained on trial
    /// accounts.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the skill cannot be trained on trial accounts;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool CannotBeTrainedOnTrialAccounts
    {
      get { return Attributes.GetAttributeValue<bool>(AttributeId.CanNotBeTrainedOnTrial); }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new SkillId Id
    {
      get { return (SkillId)base.Id.Value; }
    }

    /// <summary>
    /// Gets the primary attribute of the skill.
    /// </summary>
    /// <value>
    /// The primary attribute which determines the training time of
    /// the skill.
    /// </value>
    public CharacterAttributeType PrimaryAttribute
    {
      get
      {
        Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);

        // If not already set, load from the data source
        return this.primaryAttribute ?? (this.primaryAttribute = Eve.General.DataSource.GetCharacterAttributeTypeById(this.PrimaryAttributeId));
      }
    }

    /// <summary>
    /// Gets the ID of the primary attribute of the skill.
    /// </summary>
    /// <value>
    /// The ID of the primary attribute which determines the training time of
    /// the skill.
    /// </value>
    public CharacterAttributeId PrimaryAttributeId
    {
      get
      {
        return CharacterAttributeType.AttributeToCharacterAttribute(Attributes.GetAttributeValue<AttributeId>(AttributeId.PrimaryAttribute));
      }
    }

    /// <summary>
    /// Gets the training time multiplier for the skill.
    /// </summary>
    /// <value>
    /// The training time multiplier for the skill.
    /// </value>
    public int Rank
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 1);

        int result = Attributes.GetAttributeValue<int>(AttributeId.SkillTimeConstant);
        Contract.Assume(result >= 1);

        return result;
      }
    }

    /// <summary>
    /// Gets the secondary attribute of the skill.
    /// </summary>
    /// <value>
    /// The secondary attribute which determines the training time of
    /// the skill.
    /// </value>
    public CharacterAttributeType SecondaryAttribute
    {
      get
      {
        Contract.Ensures(Contract.Result<CharacterAttributeType>() != null);

        // If not already set, load from the data source
        return this.secondaryAttribute ?? (this.secondaryAttribute = Eve.General.DataSource.GetCharacterAttributeTypeById(this.SecondaryAttributeId));
      }
    }

    /// <summary>
    /// Gets the ID of the secondary attribute of the skill.
    /// </summary>
    /// <value>
    /// The ID of the secondary attribute which determines the training time of
    /// the skill.
    /// </value>
    public CharacterAttributeId SecondaryAttributeId
    {
      get
      {
        return CharacterAttributeType.AttributeToCharacterAttribute(Attributes.GetAttributeValue<AttributeId>(AttributeId.SecondaryAttribute));
      }
    }

    /* Methods */

    /// <summary>
    /// Gets the skill level achieved with the specified number of skill points.
    /// </summary>
    /// <param name="skillPoints">
    /// The number of skill points.
    /// </param>
    /// <param name="rank">
    /// The rank of the skill.
    /// </param>
    /// <returns>
    /// The skill level achieved with the specified number of skill points.
    /// </returns>
    public static byte GetLevelForSkillPoints(int skillPoints, int rank)
    {
      Contract.Requires(skillPoints >= 0, Resources.Messages.ISkill_SkillPointsCannotBeNegative);
      Contract.Requires(rank >= 0, Resources.Messages.ISkill_RankCannotBeNegative);
      Contract.Ensures(Contract.Result<byte>() >= 0);
      Contract.Ensures(Contract.Result<byte>() <= MaxSkillLevel);

      for (byte level = 1; level <= MaxSkillLevel; level++)
      {
        if (skillPoints < GetSkillPointsForLevel(level, rank))
        {
          return (byte)(level - 1);
        }
      }

      return MaxSkillLevel;
    }

    /// <summary>
    /// Gets the minimum number of skill points needed to qualify for the 
    /// specified level.
    /// </summary>
    /// <param name="level">
    /// The desired skill level.
    /// </param>
    /// <param name="rank">
    /// The rank of the skill.
    /// </param>
    /// <returns>
    /// The number of skill points needed to qualify for <paramref name="level" />.
    /// </returns>
    public static int GetSkillPointsForLevel(byte level, int rank)
    {
      Contract.Requires(level <= MaxSkillLevel, Resources.Messages.ISkill_LevelMustBeValid);
      Contract.Requires(rank >= 0, Resources.Messages.ISkill_RankCannotBeNegative);
      Contract.Ensures(Contract.Result<int>() >= 0);

      if (level == 0)
      {
        return 0;
      }

      int result = (int)Math.Ceiling(250.0D * rank * Math.Pow(2, 2.5D * (level - 1)));
      Contract.Assume(result >= 0);

      return result;
    }

    /// <inheritdoc />
    public int CompareTo(ISkill other)
    {
      if (other == null)
      {
        return 1;
      }

      return Name.CompareTo(other.Type.Name);
    }        
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class SkillType : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      ISkill skill = obj as ISkill;

      if (skill != null)
      {
        return this.CompareTo(obj as ISkill);
      }

      return this.CompareTo(obj as IEveTypeInstance);
    }
  }
  #endregion

  #region ISkill Implementation
  /// <content>
  /// Explicit implementation of the <see cref="ISkill" /> interface.
  /// </content>
  public partial class SkillType : ISkill
  {
    byte ISkill.Level
    {
      get { return 0; }
    }

    SkillId ISkill.Id
    {
      get { return this.Id; }
    }

    int ISkill.SkillPoints
    {
      get { return 0; }
    }

    SkillType ISkill.Type
    {
      get { return this; }
    }
  }
  #endregion
}