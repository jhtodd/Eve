//-----------------------------------------------------------------------
// <copyright file="Race.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about an EVE race.
  /// </summary>
  public sealed class Race 
    : BaseValue<RaceId, RaceId, RaceEntity, Race>,
      IHasIcon
  {
    private ReadOnlyBloodlineCollection bloodlines;
    private Icon icon;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Race class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Race(IEveRepository repository, RaceEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of bloodlines belonging to the race.
    /// </summary>
    /// <value>
    /// The collection of bloodlines belonging to the race.
    /// </value>
    public ReadOnlyBloodlineCollection Bloodlines
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyBloodlineCollection>() != null);

        return Race.LazyInitialize(
          ref this.bloodlines,
          () => ReadOnlyBloodlineCollection.Create(this.Repository, this.Entity.Bloodlines));
      }
    }

    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon
    {
      get
      {
        Contract.Ensures(this.IconId == null || Contract.Result<Icon>() != null);

        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.icon, this.Entity.IconId, () => this.Entity.Icon);
      }
    }

    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId
    {
      get { return Entity.IconId; }
    }

    /// <summary>
    /// Gets the short description of the race.
    /// </summary>
    /// <value>
    /// A string containing the short description of the race.
    /// </value>
    public string ShortDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.ShortDescription ?? string.Empty;
      }
    }
  }
}