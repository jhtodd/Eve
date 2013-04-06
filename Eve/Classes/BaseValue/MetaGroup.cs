//-----------------------------------------------------------------------
// <copyright file="MetaGroup.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contains information about a meta group to which an EVE item belongs.
  /// </summary>
  public sealed class MetaGroup 
    : BaseValue<MetaGroupId, MetaGroupId, MetaGroupEntity, MetaGroup>,
      IHasIcon
  {
    private Icon icon;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the MetaGroup class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal MetaGroup(MetaGroupEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
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
        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.icon ?? (this.icon = Eve.General.Cache.GetOrAdd<Icon>(this.IconId, () => (Icon)this.Entity.Icon.ToAdapter()));
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
  }
}