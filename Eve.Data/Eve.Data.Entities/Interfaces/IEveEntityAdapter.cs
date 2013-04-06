//-----------------------------------------------------------------------
// <copyright file="IEveEntityAdapter.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for objects which serve as an adapter for EVE entities.
  /// </summary>
  /// <typeparam name="TEntity">
  /// The type of entity wrapped by the adapter.
  /// </typeparam>
  public interface IEveEntityAdapter<out TEntity> : IEntityAdapter<TEntity>
    where TEntity : EveEntityBase
  {
  }
}