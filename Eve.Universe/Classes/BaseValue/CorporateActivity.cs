﻿//-----------------------------------------------------------------------
// <copyright file="CorporateActivity.cs" company="Jeremy H. Todd">
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

  using FreeNet;

  /// <summary>
  /// Contains information about a corporate activity.
  /// </summary>
  public sealed class CorporateActivity : BaseValue<CorporateActivityId, CorporateActivityId, CorporateActivityEntity, CorporateActivity>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CorporateActivity class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CorporateActivity(IEveRepository container, CorporateActivityEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }
  }
}