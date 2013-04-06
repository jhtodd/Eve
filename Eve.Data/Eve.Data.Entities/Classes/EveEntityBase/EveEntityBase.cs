//-----------------------------------------------------------------------
// <copyright file="EveEntityBase.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.CodeAnalysis;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for all EVE game-related entities.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public abstract class EveEntityBase : ImmutableEntity
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveEntityBase class.
    /// </summary>
    public EveEntityBase() : base()
    {
    }

    /* Methods */

    /// <summary>
    /// Returns an instance of an adapter wrapping the current entity.
    /// </summary>
    /// <returns>
    /// A class implementing the <see cref="IEveEntityAdapter{TEntity}" />
    /// interface that encapsulates the current entity, or
    /// <see langword="null" /> if no matching adapter can be created.
    /// </returns>
    public abstract IEveEntityAdapter<EveEntityBase> ToAdapter();
  }
}