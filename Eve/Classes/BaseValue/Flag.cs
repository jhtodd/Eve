//-----------------------------------------------------------------------
// <copyright file="Flag.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about a flag associated with an EVE item.
  /// </summary>
  public sealed class Flag : BaseValue<FlagId, FlagId, FlagEntity, Flag>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Flag class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Flag(IEveRepository container, FlagEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the text associated with the flag.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that provides the text associated with the flag.
    /// </value>
    public string FlagText
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return this.Description;
      }
    }
  }
}