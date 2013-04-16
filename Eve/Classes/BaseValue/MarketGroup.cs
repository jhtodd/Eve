//-----------------------------------------------------------------------
// <copyright file="MarketGroup.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about a market group to which an EVE item belongs.
  /// </summary>
  public sealed class MarketGroup 
    : BaseValue<MarketGroupId, MarketGroupId, MarketGroupEntity, MarketGroup>,
      IHasIcon
  {
    private ReadOnlyMarketGroupCollection childGroups;
    private Icon icon;
    private MarketGroup parentGroup;
    private ReadOnlyTypeCollection types;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the MarketGroup class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal MarketGroup(IEveRepository container, MarketGroupEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of child market groups under the current market group.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlyMarketGroupCollection" /> containing the child market
    /// groups.
    /// </value>
    public ReadOnlyMarketGroupCollection ChildGroups
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyMarketGroupCollection>() != null);

        return this.childGroups ?? (this.childGroups = new ReadOnlyMarketGroupCollection(this.Container.GetMarketGroups(x => x.ParentGroupId == this.Id).OrderBy(x => x)));
      }
    }

    /// <summary>
    /// Gets a value indicating whether the current group contains items or 
    /// only subgroups.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the group contains items, or 
    /// <see langword="false" /> if the group contains only subgroups.
    /// </value>
    public bool HasTypes
    {
      get { return Entity.HasTypes; }
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
        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.icon ?? (this.icon = this.Container.GetOrAdd<Icon>(this.IconId, () => this.Entity.Icon.ToAdapter(this.Container)));
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
    /// Gets the current market group's parent group, if any.
    /// </summary>
    /// <value>
    /// The parent market group, or <see langword="null" /> if the
    /// current group doesn't have a parent group.
    /// </value>
    public MarketGroup ParentGroup
    {
      get
      {
        if (this.ParentGroupId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.parentGroup ?? (this.parentGroup = this.Container.GetOrAdd<MarketGroup>(this.ParentGroupId, () => this.Entity.ParentGroup.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the current market group's parent group, if any.
    /// </summary>
    /// <value>
    /// The ID of the parent market group, or <see langword="null" /> if the
    /// current group doesn't have a parent group.
    /// </value>
    public MarketGroupId? ParentGroupId
    {
      get { return Entity.ParentGroupId; }
    }

    /// <summary>
    /// Gets the collection of items that belong to the current market group.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlyTypeCollection" /> containing the items that
    /// belong to the current market group.
    /// </value>
    public ReadOnlyTypeCollection Types
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyTypeCollection>() != null);

        return this.types ?? (this.types = new ReadOnlyTypeCollection(this.Container.GetEveTypes(x => x.MarketGroupId == this.Id).OrderBy(x => x)));
      }
    }

    /* Methods */
    
    /// <summary>
    /// Determines whether the current group is a child group of (or the same 
    /// group as) the group with the specified ID.
    /// </summary>
    /// <param name="groupId">
    /// The ID of the market group that may be a parent.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the current group is a child of (or the same
    /// group as) the group identified by <paramref name="groupId" />.
    /// </returns>
    public bool IsChildOf(MarketGroupId groupId)
    {
      if (this.Id == groupId)
      {
        return true;
      }

      if (this.ParentGroupId == null)
      {
        return false;
      }

      if (this.ParentGroupId == groupId)
      {
        return true;
      }

      // Recurse upward
      Contract.Assume(this.ParentGroup != null); // Because we know ParentGroupId is not null
      return this.ParentGroup.IsChildOf(groupId);
    }
  }
}