//-----------------------------------------------------------------------
// <copyright file="CorporateActivity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about a corporate activity.
  /// </summary>
  public sealed class CorporateActivity : BaseValue<CorporateActivityId, CorporateActivityId, CorporateActivityEntity, CorporateActivity>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CorporateActivity class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CorporateActivity(IEveRepository repository, CorporateActivityEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}