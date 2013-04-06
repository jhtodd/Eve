//-----------------------------------------------------------------------
// <copyright file="IHasSkills.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for classes which possess a collection of EVE skills.
  /// </summary>
  [ContractClass(typeof(IHasSkillsContracts))]
  public interface IHasSkills
  {
    /* Properties */

    /// <summary>
    /// Gets the collection of skills belonging to the item.
    /// </summary>
    /// <value>
    /// The collection of skills belonging to the item.
    /// </value>
    /// <remarks>
    /// <para>
    /// The returned collection should contain all skills that have been injected,
    /// even if they have no skill points applied.
    /// </para>
    /// </remarks>
    ISkillCollection Skills { get; }
  }
}