//-----------------------------------------------------------------------
// <copyright file="StationOperation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

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
        LazyInitializer.EnsureInitialized(
          ref this.activity,
          () => this.Container.GetOrAddStoredValue<CorporateActivity>(this.ActivityId, () => this.Entity.Activity.ToAdapter(this.Container)));

        Contract.Assume(this.activity != null);
        return this.activity;
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
        Contract.Ensures(this.AmarrStationTypeId == null || Contract.Result<StationType>() != null);

        if (this.AmarrStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.amarrStationType,
          () => this.Container.GetOrAddStoredValue<StationType>(this.AmarrStationTypeId, () => (StationType)this.Entity.AmarrStationType.ToAdapter(this.Container)));

        Contract.Assume(this.amarrStationType != null);
        return this.amarrStationType;
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
    public StationType CaldariStationType
    {
      get
      {
        Contract.Ensures(this.CaldariStationTypeId == null || Contract.Result<StationType>() != null);

        if (this.CaldariStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.caldariStationType,
          () => this.Container.GetOrAddStoredValue<StationType>(this.CaldariStationTypeId, () => (StationType)this.Entity.CaldariStationType.ToAdapter(this.Container)));

        Contract.Assume(this.caldariStationType != null);
        return this.caldariStationType;
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
    public StationType GallenteStationType
    {
      get
      {
        Contract.Ensures(this.GallenteStationTypeId == null || Contract.Result<StationType>() != null);

        if (this.GallenteStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.gallenteStationType,
          () => this.Container.GetOrAddStoredValue<StationType>(this.GallenteStationTypeId, () => (StationType)this.Entity.GallenteStationType.ToAdapter(this.Container)));

        Contract.Assume(this.gallenteStationType != null);
        return this.gallenteStationType;
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
    public StationType JoveStationType
    {
      get
      {
        Contract.Ensures(this.JoveStationTypeId == null || Contract.Result<StationType>() != null);

        if (this.JoveStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.joveStationType,
          () => this.Container.GetOrAddStoredValue<StationType>(this.JoveStationTypeId, () => (StationType)this.Entity.JoveStationType.ToAdapter(this.Container)));

        Contract.Assume(this.joveStationType != null);
        return this.joveStationType;
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
    public StationType MinmatarStationType
    {
      get
      {
        Contract.Ensures(this.MinmatarStationTypeId == null || Contract.Result<StationType>() != null);

        if (this.MinmatarStationTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        LazyInitializer.EnsureInitialized(
          ref this.minmatarStationType,
          () => this.Container.GetOrAddStoredValue<StationType>(this.MinmatarStationTypeId, () => (StationType)this.Entity.MinmatarStationType.ToAdapter(this.Container)));

        Contract.Assume(this.minmatarStationType != null);
        return this.minmatarStationType;
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

        LazyInitializer.EnsureInitialized(
          ref this.services,
          () =>
          {
            if (this.Entity.Services == null)
            {
              return new ReadOnlyStationServiceCollection(null);
            }

            return new ReadOnlyStationServiceCollection(this.Entity.Services.Select(x => this.Container.GetOrAddStoredValue<StationService>(x.Id, () => x.ToAdapter(this.Container))).OrderBy(x => x));
          });

        Contract.Assume(this.services != null);
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
        if (this.services != null)
        {
          this.services.Dispose();
        }
      }
    }
  }
}