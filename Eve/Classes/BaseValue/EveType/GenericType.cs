//-----------------------------------------------------------------------
// <copyright file="GenericType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// An EVE type which doesn't fall into any particular category.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This is a "fallback" class for all EVE types which don't belong in a more
  /// specific class (e.g. <see cref="SkillType" />).  It provides access to
  /// the basic data for the type (name, description, attributes, effects, etc.),
  /// but doesn't provide any convenience attributes or special functionality.
  /// </para>
  /// </remarks>
  public sealed class GenericType : EveType
  {
    /* Constructors */
    
    /// <summary>
    /// Initializes a new instance of the GenericType class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal GenericType(IEveRepository container, EveTypeEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}