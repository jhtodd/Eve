//-----------------------------------------------------------------------
// <copyright file="GenericItem.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// An EVE item which doesn't fall into any particular category.
  /// </summary>
  public sealed class GenericItem : Item
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the GenericItem class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal GenericItem(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}