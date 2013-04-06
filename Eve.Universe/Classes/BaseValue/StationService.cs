//-----------------------------------------------------------------------
// <copyright file="StationService.cs" company="Jeremy H. Todd">
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

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// Contains information about a service offered by a station.
  /// </summary>
  public sealed class StationService : BaseValue<StationServiceId, StationServiceId, StationServiceEntity, StationService>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the StationService class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal StationService(StationServiceEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
  }
}