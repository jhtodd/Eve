//-----------------------------------------------------------------------
// <copyright file="IEveEntity{TAdapter}.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;

  using Eve.Data;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for EVE-related data entities which are
  /// associated with an adapter class.
  /// </summary>
  /// <typeparam name="TAdapter">
  /// The type of adapter which wraps the entity.
  /// </typeparam>
  public interface IEveEntity<out TAdapter> : IEveEntity
  {
    /// <summary>
    /// Returns an instance of an adapter wrapping the current entity.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which will host the adapter.
    /// </param>
    /// <returns>
    /// A class implementing the <see cref="IEveEntityAdapter{TEntity}" />
    /// interface that encapsulates the current entity.
    /// </returns>
    TAdapter ToAdapter(IEveRepository container);
  }
}