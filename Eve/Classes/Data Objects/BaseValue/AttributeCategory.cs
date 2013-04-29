//-----------------------------------------------------------------------
// <copyright file="AttributeCategory.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about the category of an attribute.
  /// </summary>
  public sealed class AttributeCategory : BaseValue<AttributeCategoryId, AttributeCategoryId, AttributeCategoryEntity, AttributeCategory>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AttributeCategory class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AttributeCategory(IEveRepository repository, AttributeCategoryEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}