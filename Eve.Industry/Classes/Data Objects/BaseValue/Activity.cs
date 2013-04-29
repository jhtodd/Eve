//-----------------------------------------------------------------------
// <copyright file="Activity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about an activity performed by an industrial
  /// facility.
  /// </summary>
  public sealed partial class Activity
    : BaseValue<ActivityId, ActivityId, ActivityEntity, Activity>,
      IHasIcon
  {
    private Icon icon;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Activity class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Activity(IEveRepository repository, ActivityEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

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
        Contract.Ensures(this.IconNo == null || Contract.Result<Icon>() != null);

        if (this.IconNo == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return Activity.LazyInitialize(
          ref this.icon,
          () => 
          {
            Icon iconResult = this.Repository.GetIcons(q => q.Where(x => x.Name == this.IconNo)).FirstOrDefault();

            // TODO: As of 84566, some iconNo values don't have a corresponding
            // entry in eveIcons, although the matching image file is available.
            // In that case, we created a hard-coded entity so that the icon
            // provider can still work.
            if (iconResult == null)
            {
              IconEntity iconEntity = new IconEntity() { Id = 0, Name = this.IconNo, Description = "Missing Icon" };
              iconResult = new Icon(this.Repository, iconEntity);
            }

            return iconResult;
          });
      }
    }

    /// <summary>
    /// Gets the identifier string of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The identifier string of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public string IconNo
    {
      get { return this.Entity.IconNo; }
    }

    /// <summary>
    /// Gets a value indicating whether the item is marked as published for
    /// public consumption.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published
    {
      get { return this.Entity.Published; }
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public sealed partial class Activity : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return (this.Icon == null) ? (IconId?)null : this.Icon.Id; }
    }
  }
  #endregion
}