//-----------------------------------------------------------------------
// <copyright file="IEveEntityAdapter.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;

  using Eve.Data;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for objects which serve as an adapter for EVE entities.
  /// </summary>
  /// <typeparam name="TEntity">
  /// The type of entity wrapped by the adapter.
  /// </typeparam>
  /// <remarks>
  /// <para>
  /// An <em>adapter</em> is a wrapper around a basic entity that provides
  /// a friendlier interface for developers, including additional interface
  /// implementations, Code Contracts support, and general hiding of
  /// unnecessary "under-the-hood" processing.
  /// </para>
  /// <para>
  /// An adapter is bound to an <see cref="IEveRepository" /> object
  /// which provides the caching and data access functionality it needs
  /// to load related objects on demand.
  /// </para>
  /// </remarks>
  public interface IEveEntityAdapter<out TEntity> 
    : IEntityAdapter<TEntity>,
      IEveCacheable,
      IEveRepositoryItem
    where TEntity : IEveEntity
  {
  }
}