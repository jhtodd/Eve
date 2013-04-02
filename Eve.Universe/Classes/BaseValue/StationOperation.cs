//-----------------------------------------------------------------------
// <copyright file="StationOperation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about the operation performed by a station.
  /// </summary>
  public class StationOperation : BaseValue<StationOperationId, StationOperationId, StationOperationEntity, StationOperation> {

    #region Instance Fields
    private CorporateActivity _activity;
    private EveType _amarrStationType;
    private EveType _caldariStationType;
    private EveType _gallenteStationType;
    private EveType _joveStationType;
    private EveType _minmatarStationType;
    private ReadOnlyStationServiceCollection _services;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the StationOperation class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal StationOperation(StationOperationEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the activity associated with this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="CorporateActivity" /> object containing information about
    /// the activity associated with this type of station.
    /// </value>
    public CorporateActivity Activity {
      get {
        Contract.Ensures(Contract.Result<CorporateActivity>() != null);

        if (_activity == null) {

            // Load the cached version if available
          _activity = Eve.General.Cache.GetOrAdd<CorporateActivity>(ActivityId, () => {
              CorporateActivityEntity activityEntity = Entity.Activity;
              Contract.Assume(activityEntity != null);

              return new CorporateActivity(activityEntity);
            });
        }

        return _activity;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the activity associated with this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="CorporateActivity" /> object containing
    /// information about the activity associated with this type of station.
    /// </value>
    public CorporateActivityId ActivityId {
      get {
        return Entity.ActivityId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the Amarrian version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The type of the Amarrian version of this type of station.
    /// </value>
    public EveType AmarrStationType { // TODO: Change to StationType
      get {
        if (_amarrStationType == null) {

          if (AmarrStationTypeId != null) {

            // Load the cached version if available
            _amarrStationType = Eve.General.Cache.GetOrAdd<EveType>(AmarrStationTypeId, () => {
              EveTypeEntity typeEntity = Entity.AmarrStationType;
              Contract.Assume(typeEntity != null);

              return EveType.Create(typeEntity);
            });
          }
        }

        return _amarrStationType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the Amarrian version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the Amarrian version of this type of station.
    /// </value>
    public TypeId? AmarrStationTypeId {
      get {
        return Entity.AmarrStationTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the percentage of stations of this type that are in border systems.
    /// </summary>
    /// 
    /// <value>
    /// The percentage of stations of this type that are in border systems.
    /// </value>
    public byte Border {
      get {
        return Entity.Border;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the Caldari version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The type of the Caldari version of this type of station.
    /// </value>
    public EveType CaldariStationType { // TODO: Change to StationType
      get {
        if (_caldariStationType == null) {

          if (CaldariStationTypeId != null) {

            // Load the cached version if available
            _caldariStationType = Eve.General.Cache.GetOrAdd<EveType>(CaldariStationTypeId, () => {
              EveTypeEntity typeEntity = Entity.CaldariStationType;
              Contract.Assume(typeEntity != null);

              return EveType.Create(typeEntity);
            });
          }
        }

        return _caldariStationType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the Caldari version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the Caldari version of this type of station.
    /// </value>
    public TypeId? CaldariStationTypeId {
      get {
        return Entity.CaldariStationTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the percentage of stations of this type that are in corridor systems.
    /// </summary>
    /// 
    /// <value>
    /// The percentage of stations of this type that are in corridor systems.
    /// </value>
    public byte Corridor {
      get {
        return Entity.Corridor;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the percentage of stations of this type that are in fringe systems.
    /// </summary>
    /// 
    /// <value>
    /// The percentage of stations of this type that are in fringe systems.
    /// </value>
    public byte Fringe {
      get {
        return Entity.Fringe;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the Gallente version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The type of the Gallente version of this type of station.
    /// </value>
    public EveType GallenteStationType { // TODO: Change to StationType
      get {
        if (_gallenteStationType == null) {

          if (GallenteStationTypeId != null) {

            // Load the cached version if available
            _gallenteStationType = Eve.General.Cache.GetOrAdd<EveType>(GallenteStationTypeId, () => {
              EveTypeEntity typeEntity = Entity.GallenteStationType;
              Contract.Assume(typeEntity != null);

              return EveType.Create(typeEntity);
            });
          }
        }

        return _gallenteStationType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the Gallente version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the Gallente version of this type of station.
    /// </value>
    public TypeId? GallenteStationTypeId {
      get {
        return Entity.GallenteStationTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the percentage of stations of this type that are in hub systems.
    /// </summary>
    /// 
    /// <value>
    /// The percentage of stations of this type that are in hub systems.
    /// </value>
    public byte Hub {
      get {
        return Entity.Hub;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the Jovian version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The type of the Jovian version of this type of station.
    /// </value>
    public EveType JoveStationType { // TODO: Change to StationType
      get {
        if (_joveStationType == null) {

          if (JoveStationTypeId != null) {

            // Load the cached version if available
            _joveStationType = Eve.General.Cache.GetOrAdd<EveType>(JoveStationTypeId, () => {
              EveTypeEntity typeEntity = Entity.JoveStationType;
              Contract.Assume(typeEntity != null);

              return EveType.Create(typeEntity);
            });
          }
        }

        return _joveStationType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the Jovian version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the Jovian version of this type of station.
    /// </value>
    public TypeId? JoveStationTypeId {
      get {
        return Entity.JoveStationTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the type of the Minmatar version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The type of the Minmatar version of this type of station.
    /// </value>
    public EveType MinmatarStationType { // TODO: Change to StationType
      get {
        if (_minmatarStationType == null) {

          if (MinmatarStationTypeId != null) {

            // Load the cached version if available
            _minmatarStationType = Eve.General.Cache.GetOrAdd<EveType>(MinmatarStationTypeId, () => {
              EveTypeEntity typeEntity = Entity.MinmatarStationType;
              Contract.Assume(typeEntity != null);

              return EveType.Create(typeEntity);
            });
          }
        }

        return _minmatarStationType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the Minmatar version of this type of station.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the Minmatar version of this type of station.
    /// </value>
    public TypeId? MinmatarStationTypeId {
      get {
        return Entity.MinmatarStationTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value related to some kind of ratio for the station type.  The
    /// meaning of this property is unknown.
    /// </summary>
    /// 
    /// <value>
    /// The meaning of this property is unknown.
    /// </value>
    public byte Ratio {
      get {
        return Entity.Ratio;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of services offered by stations of this type.
    /// </summary>
    /// 
    /// <value>
    /// The collection of services offered by stations of this type.
    /// </value>
    public ReadOnlyStationServiceCollection Services {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyStationServiceCollection>() != null);

        if (_services == null) {
          if (Entity.Services == null) {
            _services = new ReadOnlyStationServiceCollection(null);
          } else {
            _services = new ReadOnlyStationServiceCollection(
              Entity.Services.Select(x => Eve.General.Cache.GetOrAdd<StationService>(x.Id, () => new StationService(x))).OrderBy(x => x)
            );
          }
        }

        return _services;
      }
    }
    #endregion
  }
}