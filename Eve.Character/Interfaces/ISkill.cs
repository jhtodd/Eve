//-----------------------------------------------------------------------
// <copyright file="ISkill.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// The base interface for classes which describe an EVE skill.
  /// </summary>
  [ContractClass(typeof(ISkillContracts))]
  public interface ISkill : IEveTypeInstance {

    #region Interface Properties
    //******************************************************************************
    /// <summary>
    /// Gets the level of the skill.
    /// </summary>
    /// 
    /// <value>
    /// The level of the skill.
    /// </value>
    byte Level { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the skill.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the skill.
    /// </value>
    new SkillId Id { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the number of skill points invested in the skill.
    /// </summary>
    /// 
    /// <value>
    /// The number of skill points invested in the skill.  For implementations
    /// that track only the level and not the specific number of skill points,
    /// this should return the minimum number of skill points needed for a
    /// character to attain that level.
    /// </value>
    int SkillPoints { get; }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the skill.
    /// </summary>
    /// 
    /// <value>
    /// The type of the skill.
    /// </value>
    new SkillType Type { get; }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="ISkill" /> interface.
  /// </summary>
  [ContractClassFor(typeof(ISkill))]
  internal abstract class ISkillContracts : ISkill {

    #region ISkill Members
    //******************************************************************************
    byte ISkill.Level {
      get {
        Contract.Ensures(Contract.Result<byte>() >= 0);
        Contract.Ensures(Contract.Result<byte>() <= SkillType.MAX_SKILL_LEVEL);

        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    SkillId ISkill.Id {
      get { 
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    int ISkill.SkillPoints {
      get {
        Contract.Ensures(Contract.Result<int>() >= 0);
        throw new NotImplementedException();
      }
    }
    //******************************************************************************
    SkillType ISkill.Type {
      get {
        Contract.Ensures(Contract.Result<SkillType>() != null);

        throw new NotImplementedException();
      }
    }
    #endregion

    #region IEveTypeInstance Members
    //******************************************************************************
    TypeId IEveTypeInstance.Id {
      get { throw new NotImplementedException(); }
    }
    //******************************************************************************
    EveType IEveTypeInstance.Type {
      get { throw new NotImplementedException(); }
    }
    #endregion
  }
}