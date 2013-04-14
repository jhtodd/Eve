//-----------------------------------------------------------------------
// <copyright file="EveEntityAdapter.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// The base class for objects which serve as an adapter for EVE entities.
  /// </summary>
  /// <typeparam name="TEntity">
  /// The type of entity wrapped by the adapter.
  /// </typeparam>
  public abstract partial class EveEntityAdapter<TEntity> 
    : EntityAdapter<TEntity>,
      IEveEntityAdapter<TEntity>
    where TEntity : Entity
  {
    private readonly IEveRepository container;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveEntityAdapter class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected EveEntityAdapter(IEveRepository container, TEntity entity) : base(entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");

      this.container = container;
    }

    /* Properties */

    /// <summary>
    /// Gets the <see cref="IEveRepository" /> the item is associated
    /// with.
    /// </summary>
    /// <value>
    /// The <see cref="IEveRepository" /> the item is associated with.
    /// </value>
    protected IEveRepository Container
    {
      get
      {
        Contract.Ensures(Contract.Result<IEveRepository>() != null);
        return this.container;
      }
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.container != null);
    }
  }

  #region IEveRepositoryItem Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveRepositoryItem" /> interface.
  /// </content>
  public abstract partial class EveEntityAdapter<TEntity> : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Container
    {
      get { return this.Container; }
    }
  }
  #endregion
}