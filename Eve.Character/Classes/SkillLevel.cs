//-----------------------------------------------------------------------
// <copyright file="SkillLevel.cs" company="Jeremy H. Todd">
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

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Utilities;

  /// <summary>
  /// Describes a level of a particular skill.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This class represents an abstract, disconnected skill and level that's
  /// not associated with a character or any other object.  For an actual
  /// skill associated with an EVE character, see the <see cref="Skill" />
  /// class.
  /// </para>
  /// </remarks>
  public sealed partial class SkillLevel
    : IComparable,
      IComparable<ISkill>,
      IEquatable<SkillLevel>,
      IEveRepositoryItem,
      IHasIcon,
      IKeyItem<SkillId>,
      ISkill
  {
    private readonly IEveRepository container;
    private readonly byte level;
    private readonly SkillId skillId;
    private SkillType skillType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the SkillLevel class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="skillId">
    /// The ID of the skill.
    /// </param>
    /// <param name="level">
    /// The level of the skill.
    /// </param>
    public SkillLevel(IEveRepository container, SkillId skillId, byte level)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(level <= SkillType.MaxSkillLevel, Resources.Messages.ISkill_LevelMustBeValid);

      this.container = container;
      this.level = level;
      this.skillId = skillId;
    }

    /* Properties */

    /// <inheritdoc />
    public byte Level
    {
      get
      {
        Contract.Ensures(Contract.Result<byte>() >= 0);
        Contract.Ensures(Contract.Result<byte>() <= SkillType.MaxSkillLevel);
        return this.level;
      }
    }

    /// <inheritdoc />
    public SkillId SkillId
    {
      get { return this.skillId; }
    }

    /// <inheritdoc />
    public SkillType SkillType
    {
      get
      {
        Contract.Ensures(Contract.Result<SkillType>() != null);

        // If not already set, load from the data source
        return this.skillType ?? (this.skillType = this.Container.GetEveTypeById<SkillType>((TypeId)(int)SkillId));
      }
    }

    /// <summary>
    /// Gets the <see cref="IEveRepository" /> the item is associated
    /// with.
    /// </summary>
    /// <value>
    /// The <see cref="IEveRepository" /> the item is associated with.
    /// </value>
    private IEveRepository Container
    {
      get
      {
        Contract.Ensures(Contract.Result<IEveRepository>() != null);
        return this.container;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public int CompareTo(ISkill other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.SkillType.Name.CompareTo(other.Type.Name);

      if (result == 0)
      {
        result = this.Level.CompareTo(other.Level);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as SkillLevel);
    }

    /// <summary>
    /// Returns a value indicating whether the current instance is equal to the
    /// specified object.
    /// </summary>
    /// <param name="other">
    /// The object to compare to the current object.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="other" /> is equal to the current
    /// instance; otherwise <see langword="false" />.
    /// </returns>
    public bool Equals(SkillLevel other)
    {
      if (other == null)
      {
        return false;
      }

      return this.SkillId.Equals(other.SkillId) && this.Level.Equals(other.Level);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.SkillId, this.Level);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.SkillType.Name + ": " + this.Level.ToString();
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.container != null);
      Contract.Invariant(this.level >= 0);
      Contract.Invariant(this.level <= 5);
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class SkillLevel : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      ISkill other = obj as ISkill;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class SkillLevel : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return SkillType.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return SkillType.IconId; }
    }
  }
  #endregion

  #region IEveRepositoryItem Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveRepositoryItem" /> interface.
  /// </content>
  public sealed partial class SkillLevel : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Container
    {
      get { return this.Container; }
    }
  }
  #endregion

  #region IEveTypeInstance Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveTypeInstance" /> interface.
  /// </content>
  public partial class SkillLevel : IEveTypeInstance
  {
    TypeId IEveTypeInstance.Id
    {
      get { return (TypeId)(int)SkillId; }
    }

    EveType IEveTypeInstance.Type
    {
      get { return SkillType; }
    }
  }
  #endregion

  #region IKeyItem<SkillId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class SkillLevel : IKeyItem<SkillId>
  {
    SkillId IKeyItem<SkillId>.Key
    {
      get { return SkillId; }
    }
  }
  #endregion

  #region ISkill Implementation
  /// <content>
  /// Explicit implementation of the <see cref="ISkill" /> interface.
  /// </content>
  public partial class SkillLevel : ISkill
  {
    SkillId ISkill.Id
    {
      get { return SkillId; }
    }

    byte ISkill.Level
    {
      get { return this.Level; }
    }

    int ISkill.SkillPoints
    {
      get { return SkillType.GetSkillPointsForLevel(this.Level, this.SkillType.Rank); }
    }

    SkillType ISkill.Type
    {
      get { return this.SkillType; }
    }
  }
  #endregion
}