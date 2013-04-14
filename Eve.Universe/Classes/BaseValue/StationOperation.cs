//-----------------------------------------------------------------------
// <copyright file="StationOperation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
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
  /// Contains information about the operation performed by a station.
  /// </summary>
  public sealed class StationOperation
    : BaseValue<StationOperationId, StationOperationId, StationOperationEntity, StationOperation>,
      IDisposable
  {
    private CorporateActivity activity;
    private StationType amarrStationType;
    private StationType caldariStationType;
    private StationType gallenteStationType;
    private StationType joveStationType;
    private StationType minmatarStationType;
    private ReadOnlyStationServiceCollection services;

    /// <summary>
    /// Initializes a new instance of the StationOperation class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal StationOperation(IEveRepository container, StationOperationEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the activity associated with this type of station.
    /// </summary>
    /// <value>
    /// The <see cref="CorporateActivity" /> object containing information about
    /// the activity associated with this type of station.
    /// </value>
    public CorporateActivity Activity
    {
      get
      {
        Contract.Ensures(Contract.Result<CorporateActivity>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.activity ?? (this.activity = this.Container.Cache.GetOrAdd<CorporateActivity>(this.ActivityId, () => this.Entity.Activity.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the activity associated with this type of station.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="CorporateActivity" /> object containing
    /// information about the activity associated with this type of station.
    /// </value>
    public CorporateActivityId ActivityId
    {
      get { return Entity.ActivityId; }
    }

    /// <summary>
    /// Gets the type of the Amarrian version of this type of station.
    /// </summary>
    /// <value>
    /// The type of the Amarrian version of this type of station.
    /// </value>
    public StationType AmarrStationType
    {
      get
      {
        if (this.AmarrStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.amarrStationType ?? (this.amarrStationType = this.Container.Cache.GetOrAdd<StationType>(this.AmarrStationTypeId, () => (StationType)this.Entity.AmarrStationType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the Amarrian version of this type of station.
    /// </summary>
    /// <value>
    /// The ID of the type of the Amarrian version of this type of station.
    /// </value>
    public TypeId? AmarrStationTypeId
    {
      get { return Entity.AmarrStationTypeId; }
    }

    /// <summary>
    /// Gets the percentage of stations of this type that are in border systems.
    /// </summary>
    /// <value>
    /// The percentage of stations of this type that are in border systems.
    /// </value>
    public byte Border
    {
      get { return Entity.Border; }
    }

    /// <summary>
    /// Gets the type of the Caldari version of this type of station.
    /// </summary>
    /// <value>
    /// The type of the Caldari version of this type of station.
    /// </value>
    public EveType CaldariStationType
    {
      get
      {
        if (this.CaldariStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.caldariStationType ?? (this.caldariStationType = this.Container.Cache.GetOrAdd<StationType>(this.CaldariStationTypeId, () => (StationType)this.Entity.CaldariStationType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the Caldari version of this type of station.
    /// </summary>
    /// <value>
    /// The ID of the type of the Caldari version of this type of station.
    /// </value>
    public TypeId? CaldariStationTypeId
    {
      get { return Entity.CaldariStationTypeId; }
    }

    /// <summary>
    /// Gets the percentage of stations of this type that are in corridor systems.
    /// </summary>
    /// <value>
    /// The percentage of stations of this type that are in corridor systems.
    /// </value>
    public byte Corridor
    {
      get { return Entity.Corridor; }
    }

    /// <summary>
    /// Gets the percentage of stations of this type that are in fringe systems.
    /// </summary>
    /// <value>
    /// The percentage of stations of this type that are in fringe systems.
    /// </value>
    public byte Fringe
    {
      get { return Entity.Fringe; }
    }

    /// <summary>
    /// Gets the type of the Gallente version of this type of station.
    /// </summary>
    /// <value>
    /// The type of the Gallente version of this type of station.
    /// </value>
    public EveType GallenteStationType
    {
      get
      {
        if (this.GallenteStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.gallenteStationType ?? (this.gallenteStationType = this.Container.Cache.GetOrAdd<StationType>(this.GallenteStationTypeId, () => (StationType)this.Entity.GallenteStationType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the Gallente version of this type of station.
    /// </summary>
    /// <value>
    /// The ID of the type of the Gallente version of this type of station.
    /// </value>
    public TypeId? GallenteStationTypeId
    {
      get { return Entity.GallenteStationTypeId;  }
    }

    /// <summary>
    /// Gets the percentage of stations of this type that are in hub systems.
    /// </summary>
    /// <value>
    /// The percentage of stations of this type that are in hub systems.
    /// </value>
    public byte Hub
    {
      get { return Entity.Hub; }
    }

    /// <summary>
    /// Gets the type of the Jovian version of this type of station.
    /// </summary>
    /// <value>
    /// The type of the Jovian version of this type of station.
    /// </value>
    public EveType JoveStationType
    {
      get
      {
        if (this.JoveStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.joveStationType ?? (this.joveStationType = this.Container.Cache.GetOrAdd<StationType>(this.JoveStationTypeId, () => (StationType)this.Entity.JoveStationType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the Jovian version of this type of station.
    /// </summary>
    /// <value>
    /// The ID of the type of the Jovian version of this type of station.
    /// </value>
    public TypeId? JoveStationTypeId
    {
      get { return Entity.JoveStationTypeId; }
    }

    /// <summary>
    /// Gets the type of the Minmatar version of this type of station.
    /// </summary>
    /// <value>
    /// The type of the Minmatar version of this type of station.
    /// </value>
    public EveType MinmatarStationType
    {
      get
      {
        if (this.MinmatarStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.minmatarStationType ?? (this.minmatarStationType = this.Container.Cache.GetOrAdd<StationType>(this.MinmatarStationTypeId, () => (StationType)this.Entity.MinmatarStationType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the Minmatar version of this type of station.
    /// </summary>
    /// <value>
    /// The ID of the type of the Minmatar version of this type of station.
    /// </value>
    public TypeId? MinmatarStationTypeId
    {
      get { return Entity.MinmatarStationTypeId; }
    }

    /// <summary>
    /// Gets a value related to some kind of ratio for the station type.  The
    /// meaning of this property is unknown.
    /// </summary>
    /// <value>
    /// The meaning of this property is unknown.
    /// </value>
    public byte Ratio
    {
      get { return Entity.Ratio; }
    }

    /// <summary>
    /// Gets the collection of services offered by stations of this type.
    /// </summary>
    /// <value>
    /// The collection of services offered by stations of this type.
    /// </value>
    public ReadOnlyStationServiceCollection Services
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyStationServiceCollection>() != null);

        if (this.services == null)
        {
          if (this.Entity.Services == null)
          {
            this.services = new ReadOnlyStationServiceCollection(null);
          }
          else
          {
            this.services = new ReadOnlyStationServiceCollection(
              this.Entity.Services.Select(x => this.Container.Cache.GetOrAdd<StationService>(x.Id, () => x.ToAdapter(this.Container))).OrderBy(x => x));
          }
        }

        return this.services;
      }
    }

    /* Methods */

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    /// <param name="disposing">Specifies whether to dispose managed resources.</param>
    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.services.Dispose();
      }
    }
  }
}