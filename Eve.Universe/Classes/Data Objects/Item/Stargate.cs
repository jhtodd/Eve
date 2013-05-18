//-----------------------------------------------------------------------
// <copyright file="Stargate.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

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
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Stargate(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
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
        return this.LazyInitializeAdapter(ref this.destination, this.StargateInfo.DestinationId, () => this.StargateInfo.Destination);
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

    /// <summary>
    /// Gets the <see cref="StargateEntity" /> containing the specific
    /// information for the current item type.
    /// </summary>
    /// <value>
    /// The <see cref="StargateEntity" /> containing the specific
    /// information for the current item type.
    /// </value>
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