//-----------------------------------------------------------------------
// <copyright file="Stargate.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Industry;

  /// <summary>
  /// An EVE item describing an in-game stargate.
  /// </summary>
  public sealed class Stargate : Item
  {
    private Stargate destination;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Stargate class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Stargate(IEveRepository container, ItemEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsStargate, "The entity must be a station.");
    }

    /* Properties */

    /// <summary>
    /// Gets the destination stargate.
    /// </summary>
    /// <value>
    /// The destination stargate.
    /// </value>
    public Stargate Destination
    {
      get
      {
        Contract.Ensures(Contract.Result<Stargate>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.destination ?? (this.destination = this.Container.GetOrAdd<Stargate>(this.DestinationId, () => (Stargate)this.StargateInfo.Destination.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the destination stargate.
    /// </summary>
    /// <value>
    /// The ID of the destination stargate.
    /// </value>
    public StargateId DestinationId
    {
      get { return this.StargateInfo.DestinationId; }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new StargateId Id
    {
      get { return (StargateId)base.Id.Value; }
    }

    private StargateEntity StargateInfo
    {
      get
      {
        Contract.Ensures(Contract.Result<StargateEntity>() != null);

        var result = this.Entity.StargateInfo;

        Contract.Assume(result != null);
        return result;
      }
    }
  }
}