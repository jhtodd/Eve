//-----------------------------------------------------------------------
// <copyright file="ISkillCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for collections of EVE skills.
  /// </summary>
  [ContractClass(typeof(ISkillCollectionContracts))]
  public interface ISkillCollection : IReadOnlyList<ISkill>
  {
    /* Properties */

    /// <summary>
    /// Gets the skill with the specified ID.
    /// </summary>
    /// <param name="skillId">
    /// The ID of the skill to retrieve.
    /// </param>
    /// <returns>
    /// The skill with the specified ID.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown if no skill with the specified ID was found in the collection.
    /// </exception>
    ISkill this[SkillId skillId] { get; }

    /* Methods */

    /// <summary>
    /// Gets a value indicating whether a skill with the specified ID is
    /// contained in the collection.
    /// </summary>
    /// <param name="skillId">
    /// The ID of the skill to locate.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if a skill with the specified ID is contained
    /// in the collection; otherwise <see langword="false" />.
    /// </returns>
    bool ContainsKey(SkillId skillId);

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
    byte GetSkillLevel(SkillId skillId);

    /// <summary>
    /// Attempts to retrieve the skill with the specified ID, returning success
    /// or failure.
    /// </summary>
    /// <param name="skillId">
    /// The ID of the skill to retrieve.
    /// </param>
    /// <param name="value">
    /// Output parameter.  Will contain the specified skill if found.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the specified skill was found; otherwise
    /// <see langword="false" />.
    /// </returns>
    bool TryGetValue(SkillId skillId, out ISkill value);
  }
}