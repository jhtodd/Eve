//-----------------------------------------------------------------------
// <copyright file="ReadOnlySkillLevelCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Collections;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of <see cref="SkillLevel" /> objects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlySkillLevelCollection
    : ReadOnlyKeyedRepositoryItemCollection<SkillId, SkillLevel>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlySkillLevelCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    internal ReadOnlySkillLevelCollection(IEveRepository repository) : base(repository)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }

    /* Methods */

    /// <summary>
    /// Creates a new instance of the ReadOnlySkillLevelCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    /// <returns>
    /// A newly created collection containing the specified items.
    /// </returns>
    public static ReadOnlySkillLevelCollection Create(IEveRepository repository, IEnumerable<SkillLevel> contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");

      var result = new ReadOnlySkillLevelCollection(repository);

      if (contents != null)
      {
        foreach (var item in contents)
        {
          result.Items.AddWithoutCallback(item);
        }
      }

      return result;
    }

    /// <summary>
    /// Gets the level of the specified skill, or 0 if no matching skill is found
    /// in the collection.
    /// </summary>
    /// <param name="skillId">
    /// The ID of the skill whose level to retrieve.
    /// </param>
    /// <returns>
    /// The level of the specified skill, or 0 if no matching skill is found in
    /// the collection.
    /// </returns>
    public byte GetSkillLevel(SkillId skillId)
    {
      SkillLevel skill;

      if (this.TryGetValue(skillId, out skill))
      {
        Contract.Assume(skill != null);
        return skill.Level;
      }

      return 0;
    }
  }

  #region IEnumerable<ISkill> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable{T}" /> interface.
  /// </content>
  public sealed partial class ReadOnlySkillLevelCollection : IEnumerable<ISkill>
  {
    IEnumerator<ISkill> IEnumerable<ISkill>.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
  #endregion

  #region IReadOnlyList<ISkill> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyList{T}" /> interface.
  /// </content>
  public sealed partial class ReadOnlySkillLevelCollection : IReadOnlyList<ISkill>
  {
    ISkill IReadOnlyList<ISkill>.this[int index]
    {
      get { return this[index]; }
    }
  }
  #endregion

  #region ISkillCollection Implementation
  /// <content>
  /// Explicit implementation of the <see cref="ISkillCollection" /> interface.
  /// </content>
  public sealed partial class ReadOnlySkillLevelCollection : ISkillCollection
  {
    ISkill ISkillCollection.this[SkillId skillId]
    {
      get
      {
        var result = this[skillId];

        Contract.Assume(result != null);
        return result;
      }
    }

    bool ISkillCollection.TryGetValue(SkillId skillId, out ISkill value)
    {
      SkillLevel containedValue;

      bool success = TryGetValue(skillId, out containedValue);
      value = containedValue;

      Contract.Assume(!success || value != null);
      return success;
    }
  }
  #endregion
}