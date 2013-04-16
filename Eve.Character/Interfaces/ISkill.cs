//-----------------------------------------------------------------------
// <copyright file="ISkill.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base interface for classes which describe an EVE skill.
  /// </summary>
  [ContractClass(typeof(ISkillContracts))]
  public interface ISkill : IEveTypeInstance
  {
    /* Properties */

    /// <summary>
    /// Gets the level of the skill.
    /// </summary>
    /// <value>
    /// The level of the skill.
    /// </value>
    byte Level { get; }

    /// <summary>
    /// Gets the ID of the skill.
    /// </summary>
    /// <value>
    /// The ID of the skill.
    /// </value>
    new SkillId Id { get; }

    /// <summary>
    /// Gets the number of skill points invested in the skill.
    /// </summary>
    /// <value>
    /// The number of skill points invested in the skill.  For implementations
    /// that track only the level and not the specific number of skill points,
    /// this should return the minimum number of skill points needed for a
    /// character to attain that level.
    /// </value>
    int SkillPoints { get; }

    /// <summary>
    /// Gets the type of the skill.
    /// </summary>
    /// <value>
    /// The type of the skill.
    /// </value>
    new SkillType Type { get; }
  }
}