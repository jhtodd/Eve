//-----------------------------------------------------------------------
// <copyright file="MarketGroup.cs" company="Jeremy H. Todd">
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
  /// Contains information about a market group to which an EVE item belongs.
  /// </summary>
  public sealed class MarketGroup 
    : BaseValue<MarketGroupId, MarketGroupId, MarketGroupEntity, MarketGroup>,
      IHasIcon
  {
    private ReadOnlyMarketGroupCollection childGroups;
    private Icon icon;
    private MarketGroup parentGroup;
    private ReadOnlyEveTypeCollection types;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the MarketGroup class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal MarketGroup(IEveRepository repository, MarketGroupEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
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

        return MarketGroup.LazyInitialize(
          ref this.childGroups,
          () => new ReadOnlyMarketGroupCollection(this.Repository, this.Entity.ChildGroups));
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
        Contract.Ensures(this.ParentGroupId == null || Contract.Result<MarketGroup>() != null);

        if (this.ParentGroupId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.parentGroup, this.Entity.ParentGroupId, () => this.Entity.ParentGroup);
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
    /// A <see cref="ReadOnlyEveTypeCollection" /> containing the items that
    /// belong to the current market group.
    /// </value>
    public ReadOnlyEveTypeCollection Types
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyEveTypeCollection>() != null);

        return MarketGroup.LazyInitialize(
          ref this.types,
          () => new ReadOnlyEveTypeCollection(this.Repository, this.Entity.Types));
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
      return this.ParentGroup.IsChildOf(groupId);
    }
  }
}