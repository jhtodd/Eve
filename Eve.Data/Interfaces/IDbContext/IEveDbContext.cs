//-----------------------------------------------------------------------
// <copyright file="IEveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for objects that provide data access and change tracking
  /// for the application.
  /// </summary>
  [ContractClass(typeof(IEveDbContextContracts))]
  public interface IEveDbContext : IDbContext
  {
    /* Properties */

    /// <summary>
    /// Gets the <see cref="DbSet{T}" /> for icons.
    /// </summary>
    /// <value>
    /// The <see cref="DbSet{T}" /> for icons.
    /// </value>
    IDbSet<IconEntity> Icons { get; }
  }
}