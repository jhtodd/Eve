﻿//-----------------------------------------------------------------------
// <copyright file="ActivityEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Industry;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The data entity for the <see cref="Activity" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("ramActivities")]
  public class ActivityEntity : BaseValueEntity<ActivityId, Activity>
  {
    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ActivityEntity class.
    /// </summary>
    public ActivityEntity() : base()
    {
    }

    /* Constructors */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    [Column("iconNo")]
    public string IconNo { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("published")]
    public bool Published { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public override Activity ToAdapter(IEveRepository container)
    {
      Contract.Assume(container != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new Activity(container, this);
    }
  }
}