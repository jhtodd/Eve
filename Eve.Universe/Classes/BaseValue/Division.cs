//-----------------------------------------------------------------------
// <copyright file="Division.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about the type of an agent.
  /// </summary>
  public sealed class Division : BaseValue<DivisionId, DivisionId, DivisionEntity, Division>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Division class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Division(IEveRepository container, DivisionEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the title of the leader of the division.
    /// </summary>
    /// <value>
    /// The title of the leader of the division.
    /// </value>
    public string LeaderType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.LeaderType ?? string.Empty;
      }
    }
  }
}