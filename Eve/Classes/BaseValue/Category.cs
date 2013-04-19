//-----------------------------------------------------------------------
// <copyright file="Category.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about a category to which an EVE item belongs.
  /// </summary>
  public sealed class Category
    : BaseValue<CategoryId, CategoryId, CategoryEntity, Category>,
      IHasIcon
  {
    private Icon icon;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Category class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Category(IEveRepository container, CategoryEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
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
        Contract.Ensures(this.IconId == null || Contract.Result<Icon>() != null);

        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.icon,
          () => this.Container.GetOrAdd<Icon>(this.IconId, () => (Icon)this.Entity.Icon.ToAdapter(this.Container)));

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
  }
}