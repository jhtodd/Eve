//-----------------------------------------------------------------------
// <copyright file="StationService.cs" company="Jeremy H. Todd">
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
  using FreeNet.Collections.ObjectModel;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about a service offered by a station.
  /// </summary>
  public class StationService : BaseValue<StationServiceId, StationServiceId, StationServiceEntity, StationService> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the StationService class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal StationService(StationServiceEntity entity) : base(entity) {
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
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of station services.
  /// </summary>
  public class ReadOnlyStationServiceCollection : ReadOnlyKeyedCollection<StationServiceId, StationService> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyStationServiceCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyStationServiceCollection(IEnumerable<StationService> contents) : base() {
      if (contents != null) {
        foreach (StationService service in contents) {
          Items.AddWithoutCallback(service);
        }
      }
    }
    #endregion
  }
}