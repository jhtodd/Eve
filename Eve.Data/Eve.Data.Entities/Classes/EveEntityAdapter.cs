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
    where TEntity : EveEntityBase
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveEntityAdapter class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected EveEntityAdapter(TEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
  }
}