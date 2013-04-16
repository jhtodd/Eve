//-----------------------------------------------------------------------
// <copyright file="EveEntityBase.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for all EVE game-related entities.
  /// </summary>
  /// <typeparam name="TAdapter">
  /// The type of the entity adapter which encapsulates this class.
  /// </typeparam>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public abstract class EveEntityBase<TAdapter> : ImmutableEntity
    where TAdapter : IEveEntityAdapter<EveEntityBase<TAdapter>>
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
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which will host the adapter.
    /// </param>
    /// <returns>
    /// A class implementing the <see cref="IEveEntityAdapter{TEntity}" />
    /// interface that encapsulates the current entity, or
    /// <see langword="null" /> if no matching adapter can be created.
    /// </returns>
    public abstract TAdapter ToAdapter(IEveRepository container);
  }
}