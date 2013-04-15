﻿//-----------------------------------------------------------------------
// <copyright file="StationServiceEntity.cs" company="Jeremy H. Todd">
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

  using Eve.Universe;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The data entity for the <see cref="StationService" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("staServices")]
  public class StationServiceEntity : BaseValueEntity<StationServiceId, StationService>
  {
    // Check InnerEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the StationServiceEntity class.
    /// </summary>
    public StationServiceEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override StationService ToAdapter(IEveRepository container)
    {
      Contract.Assume(container != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new StationService(container, this);
    }
  }
}