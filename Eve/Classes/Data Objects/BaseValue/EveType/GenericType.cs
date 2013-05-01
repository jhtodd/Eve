//-----------------------------------------------------------------------
// <copyright file="GenericType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// An EVE type which doesn't fall into any particular category.
  /// </summary>
  /// <remarks>
  /// <para>
  /// This is a "fallback" class for all EVE types which don't belong in a more
  /// specific class (e.g. <see cref="SkillType" />).  It provides access to
  /// the basic data for the type (name, description, attributes, effects, etc.),
  /// but doesn't provide any convenience properties or special functionality.
  /// </para>
  /// </remarks>
  public sealed class GenericType : EveType
  {
    /* Constructors */
    
    /// <summary>
    /// Initializes a new instance of the GenericType class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal GenericType(IEveRepository repository, EveTypeEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}