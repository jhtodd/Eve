﻿//-----------------------------------------------------------------------
// <copyright file="Race.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;

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
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Race(IEveRepository container, RaceEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
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

        return this.bloodlines ?? (this.bloodlines = new ReadOnlyBloodlineCollection(this.Container.GetBloodlines(x => x.RaceId == this.Id).OrderBy(x => x)));
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
        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.icon ?? (this.icon = this.Container.Cache.GetOrAdd<Icon>(this.IconId, () => this.Entity.Icon.ToAdapter(this.Container)));
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