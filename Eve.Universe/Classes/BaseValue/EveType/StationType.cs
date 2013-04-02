﻿//-----------------------------------------------------------------------
// <copyright file="GenericType.cs" company="Jeremy H. Todd">
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
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// The type of an EVE station.
  /// </summary>
  public class StationType : EveType {

    #region Instance Fields
    private StationOperation _operation;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the StationType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal StationType(StationTypeEntity entity) : base(entity) {
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
    /// Gets a value indicating whether the station is conquerable.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the station is conquerable; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool Conquerable {
      get {
        return Entity.Conquerable;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the X coordinate of the station's docking entry point.
    /// </summary>
    /// 
    /// <value>
    /// The X coordinate of the station's docking entry point.
    /// </value>
    public double DockEntryX {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DockEntryX;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the Y coordinate of the station's docking entry point.
    /// </summary>
    /// 
    /// <value>
    /// The Y coordinate of the station's docking entry point.
    /// </value>
    public double DockEntryY {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DockEntryY;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the Z coordinate of the station's docking entry point.
    /// </summary>
    /// 
    /// <value>
    /// The Z coordinate of the station's docking entry point.
    /// </value>
    public double DockEntryZ {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DockEntryZ;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the X component of the station's docking vector.
    /// </summary>
    /// 
    /// <value>
    /// The X component of the station's docking vector.
    /// </value>
    public double DockOrientationX {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DockOrientationX;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the Y component of the station's docking vector.
    /// </summary>
    /// 
    /// <value>
    /// The Y component of the station's docking vector.
    /// </value>
    public double DockOrientationY {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DockOrientationY;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the Z component of the station's docking vector.
    /// </summary>
    /// 
    /// <value>
    /// The Z component of the station's docking vector.
    /// </value>
    public double DockOrientationZ {
      get {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = Entity.DockOrientationZ;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of office slots available at the station.  This value
    /// does not appear to be used and is null for most station types.
    /// </summary>
    /// 
    /// <value>
    /// The number of office slots available at the station, or
    /// <see langword="null" /> if no information is available for this station
    /// type.
    /// </value>
    public byte? OfficeSlots {
      get {
        return Entity.OfficeSlots;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the operation associated with the station type.  This value does
    /// not appear to be used for most types; use the
    /// <see cref="Station.Operation">Operation</see> property of the
    /// <see cref="Station" /> class when possible.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="StationOperation" /> associated with the station type.
    /// </value>
    public StationOperation Operation {
      get {
        if (_operation == null) {
          if (OperationId != null) {

            // Load the cached version if available
            _operation = Eve.General.Cache.GetOrAdd<StationOperation>(OperationId, () => {
              StationOperationEntity operationEntity = Entity.Operation;
              Contract.Assume(operationEntity != null);

              return new StationOperation(operationEntity);
            });
          }
        }

        return _operation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the operation associated with the station type.  This 
    /// value does not appear to be used for most types; use the
    /// <see cref="Station.OperationId">OperationId</see> property of the
    /// <see cref="Station" /> class when possible.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the <see cref="StationOperation" /> associated with the station
    /// type.
    /// </value>
    public StationOperationId? OperationId {
      get {
        return Entity.OperationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the reprocessing efficiency of the station type.  This 
    /// value does not appear to be used for most types; use the
    /// <see cref="Station.ReprocessingEfficiency">ReprocessingEfficiency</see>
    /// property of the <see cref="Station" /> class when possible.
    /// </summary>
    /// 
    /// <value>
    /// The reprocessing efficiency of the station type, or
    /// <see langword="null" /> if no information is available for this station
    /// type.
    /// </value>
    public double? ReprocessingEfficiency { get; set; }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
    /// 
    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    protected new StationTypeEntity Entity {
      get {
        Contract.Ensures(Contract.Result<StationTypeEntity>() != null);

        return (StationTypeEntity) base.Entity;
      }
    }
    #endregion
  }
}