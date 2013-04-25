//-----------------------------------------------------------------------
// <copyright file="IEveEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for all EVE game-related data entities.
  /// </summary>
  [ContractClass(typeof(IEveEntityContracts))]
  public interface IEveEntity
  {
    /// <summary>
    /// Gets the ID value used to store the object in the cache.
    /// </summary>
    /// <value>
    /// A value which uniquely identifies the entity.
    /// </value>
    IConvertible CacheKey { get; }
  }
}