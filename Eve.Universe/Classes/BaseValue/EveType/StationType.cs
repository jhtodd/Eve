//-----------------------------------------------------------------------
// <copyright file="StationType.cs" company="Jeremy H. Todd">
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
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// The type of an EVE station.
  /// </summary>
  public sealed class StationType : EveType
  {
    private StationOperation operation;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the StationType class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal StationType(IEveRepository container, StationTypeEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets a value indicating whether the station is conquerable.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the station is conquerable; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool Conquerable
    {
      get { return this.Entity.Conquerable; }
    }

    /// <summary>
    /// Gets the X coordinate of the station's docking entry point.
    /// </summary>
    /// <value>
    /// The X coordinate of the station's docking entry point.
    /// </value>
    public double DockEntryX
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.DockEntryX;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Y coordinate of the station's docking entry point.
    /// </summary>
    /// <value>
    /// The Y coordinate of the station's docking entry point.
    /// </value>
    public double DockEntryY
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.DockEntryY;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Z coordinate of the station's docking entry point.
    /// </summary>
    /// <value>
    /// The Z coordinate of the station's docking entry point.
    /// </value>
    public double DockEntryZ
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.DockEntryZ;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the X component of the station's docking vector.
    /// </summary>
    /// <value>
    /// The X component of the station's docking vector.
    /// </value>
    public double DockOrientationX
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.DockOrientationX;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Y component of the station's docking vector.
    /// </summary>
    /// <value>
    /// The Y component of the station's docking vector.
    /// </value>
    public double DockOrientationY
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.DockOrientationY;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the Z component of the station's docking vector.
    /// </summary>
    /// <value>
    /// The Z component of the station's docking vector.
    /// </value>
    public double DockOrientationZ
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));

        var result = this.Entity.DockOrientationZ;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));

        return result;
      }
    }

    /// <summary>
    /// Gets the number of office slots available at the station.  This value
    /// does not appear to be used and is null for most station types.
    /// </summary>
    /// <value>
    /// The number of office slots available at the station, or
    /// <see langword="null" /> if no information is available for this station
    /// type.
    /// </value>
    public byte? OfficeSlots
    {
      get { return this.Entity.OfficeSlots; }
    }

    /// <summary>
    /// Gets the operation associated with the station type.  This value does
    /// not appear to be used for most types; use the
    /// <see cref="Station.Operation">Operation</see> property of the
    /// <see cref="Station" /> class when possible.
    /// </summary>
    /// <value>
    /// The <see cref="StationOperation" /> associated with the station type.
    /// </value>
    public StationOperation Operation
    {
      get
      {
        if (this.OperationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.operation ?? (this.operation = this.Container.Cache.GetOrAdd<StationOperation>(this.OperationId, () => this.Entity.Operation.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the operation associated with the station type.  This 
    /// value does not appear to be used for most types; use the
    /// <see cref="Station.OperationId">OperationId</see> property of the
    /// <see cref="Station" /> class when possible.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="StationOperation" /> associated with the station
    /// type.
    /// </value>
    public StationOperationId? OperationId
    {
      get { return this.Entity.OperationId; }
    }

    /// <summary>
    /// Gets the reprocessing efficiency of the station type.  This 
    /// value does not appear to be used for most types; use the
    /// <see cref="Station.ReprocessingEfficiency">ReprocessingEfficiency</see>
    /// property of the <see cref="Station" /> class when possible.
    /// </summary>
    /// <value>
    /// The reprocessing efficiency of the station type, or
    /// <see langword="null" /> if no information is available for this station
    /// type.
    /// </value>
    public double? ReprocessingEfficiency
    {
      get { return this.Entity.ReprocessingEfficiency; }
    }

    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    private new StationTypeEntity Entity
    {
      get
      {
        Contract.Ensures(Contract.Result<StationTypeEntity>() != null);

        return (StationTypeEntity)base.Entity;
      }
    }
  }
}