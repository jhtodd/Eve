//-----------------------------------------------------------------------
// <copyright file="IEveDataSource.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// The base interface for a data source which provides access to the EVE
  /// database.
  /// </summary>
  [ContractClass(typeof(IEveDataSourceContracts))]
  public interface IEveDataSource {

    #region Interface Methods
    //******************************************************************************
    /// <summary>
    /// Returns a list containing all entities of type
    /// <typeparamref name="TEntity" />, modified by the specified query modifier.
    /// </summary>
    /// 
    /// <typeparam name="TEntity">
    /// The type of entity to retrieve.
    /// </typeparam>
    /// 
    /// <param name="modifier">
    /// The query modifier used to filter, sort, or otherwise manipulate the
    /// results of the query.
    /// </param>
    /// 
    /// <returns>
    /// An <see cref="IList{T}" /> containing the results of the query.
    /// </returns>
    IList<TEntity> Get<TEntity>(IQueryModifier<TEntity> modifier) where TEntity : class, IHasId;
    //******************************************************************************
    /// <summary>
    /// Returns a list containing all entities of type
    /// <typeparamref name="TEntity" /> that meet the specified filter criteria.
    /// </summary>
    /// 
    /// <typeparam name="TEntity">
    /// The type of entity to retrieve.
    /// </typeparam>
    /// 
    /// <param name="filter">
    /// An expression specifying the filter criteria for the entities to return.
    /// </param>
    /// 
    /// <returns>
    /// An <see cref="IList{T}" /> containing the results of the query.
    /// </returns>
    IList<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IHasId;
    //******************************************************************************
    /// <summary>
    /// Returns a list containing all entities of type
    /// <typeparamref name="TEntity" />.
    /// </summary>
    /// 
    /// <typeparam name="TEntity">
    /// The type of entity to retrieve.
    /// </typeparam>
    /// 
    /// <returns>
    /// An <see cref="IList{T}" /> containing the results of the query.
    /// </returns>
    /// 
    /// <remarks>
    /// <para>
    /// <strong>Caution:</strong> This method performs the query immediately and
    /// loads all entities of the desired type from the database into a list in
    /// memory.  Depending on the type being requested, this may use a large amount
    /// of resources.
    /// </para>
    /// </remarks>
    IList<TEntity> GetAll<TEntity>() where TEntity : class, IHasId;

    TEntity GetById<TEntity>(object id) where TEntity : class, IHasId;
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// Contract class for the <see cref="IEveDataSource" /> interface.
  /// </summary>
  [ContractClassFor(typeof(IEveDataSource))]
  internal abstract class IEveDataSourceContracts : IEveDataSource {

    #region IEveDataSource Members

    //******************************************************************************
    IList<TEntity> IEveDataSource.Get<TEntity>(IQueryModifier<TEntity> modifier) {
      Contract.Requires(modifier != null, Resources.Messages.IEveDataSource_QueryModifierCannotBeNull);
      Contract.Ensures(Contract.Result<IList<TEntity>>() != null);

      throw new NotImplementedException();
    }
    //******************************************************************************
    IList<TEntity> IEveDataSource.Get<TEntity>(Expression<Func<TEntity, bool>> filter) {
      Contract.Requires(filter != null, Resources.Messages.IEveDataSource_FilterCannotBeNull);
      Contract.Ensures(Contract.Result<IList<TEntity>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    IList<TEntity> IEveDataSource.GetAll<TEntity>() {
      Contract.Ensures(Contract.Result<IList<TEntity>>() != null);
      throw new NotImplementedException();
    }
    //******************************************************************************
    TEntity IEveDataSource.GetById<TEntity>(object id) {
      Contract.Requires(id != null, Resources.Messages.IEveDataSource_IdCannotBeNull);
      Contract.Ensures(Contract.Result<TEntity>() != null);
      throw new NotImplementedException();
    }
    #endregion



  }
}