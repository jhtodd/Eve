//-----------------------------------------------------------------------
// <copyright file="Group.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about a group to which an EVE item belongs.
  /// </summary>
  public sealed class Group
    : BaseValue<GroupId, GroupId, GroupEntity, Group>,
      IHasIcon
  {
    private Category category;
    private Icon icon;
    private ReadOnlyTypeCollection types;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Group class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Group(IEveRepository container, GroupEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets a value indicating whether items in the group can be manufactured.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if items in the group can be manufactured;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool AllowManufacture
    {
      get { return Entity.AllowManufacture; }
    }

    /// <summary>
    /// Gets a value indicating whether items in the group can be reprocessed.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if items in the group can be reprocessed;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool AllowRecycler
    {
      get { return Entity.AllowRecycler; }
    }

    /// <summary>
    /// Gets a value indicating whether items in the group can be anchored.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if items in the group can be anchored;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Anchorable
    {
      get { return Entity.Anchorable; }
    }

    /// <summary>
    /// Gets a value indicating whether items in the group are permanently anchored.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if items in the group are permanently anchored;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Anchored
    {
      get { return Entity.Anchored; }
    }

    /// <summary>
    /// Gets the category to which the group belongs.
    /// </summary>
    /// <value>
    /// The <see cref="Category" /> to which the group belongs.
    /// </value>
    public Category Category
    {
      get
      {
        Contract.Ensures(Contract.Result<Category>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.category,
          () => this.Container.GetOrAddStoredValue<Category>(this.CategoryId, () => this.Entity.Category.ToAdapter(this.Container)));

        Contract.Assume(this.category != null);
        return this.category;
      }
    }

    /// <summary>
    /// Gets the ID of the category to which the group belongs.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Category" /> to which the group belongs.
    /// </value>
    public CategoryId CategoryId
    {
      get { return Entity.CategoryId; }
    }

    /// <summary>
    /// Gets a value indicating whether multiple instances of items in the group
    /// can be fit on a ship.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if multiple instances can be fit; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool FittableNonSingleton
    {
      get { return Entity.FittableNonSingleton; }
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
        LazyInitializer.EnsureInitialized(
          ref this.icon,
          () => this.Container.GetOrAddStoredValue<Icon>(this.IconId, () => this.Entity.Icon.ToAdapter(this.Container)));

        Contract.Assume(this.icon != null);
        return this.icon;
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
    /// Gets a value indicating whether the item is marked as published for
    /// public consumption.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the item is marked as published;
    /// otherwise <see langword="false" />.
    /// </value>
    public bool Published
    {
      get { return Entity.Published; }
    }

    /// <summary>
    /// Gets the collection of items that belong to the current group.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlyTypeCollection" /> containing the items that
    /// belong to the current group.
    /// </value>
    public ReadOnlyTypeCollection Types
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyTypeCollection>() != null);

        LazyInitializer.EnsureInitialized(
          ref this.types, 
          () => new ReadOnlyTypeCollection(this.Container.GetEveTypes(q => q.Where(x => x.GroupId == this.Id)).OrderBy(x => x)));

        Contract.Assume(this.types != null);
        return this.types;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the base price should be used for items
    /// in the group.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the base price should be used for items in the
    /// group; otherwise <see langword="false" />.
    /// </value>
    public bool UseBasePrice
    {
      get { return Entity.UseBasePrice; }
    }
  }
}