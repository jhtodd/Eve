﻿//-----------------------------------------------------------------------
// <copyright file="StationServiceEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Universe;

  /// <summary>
  /// The data entity for the <see cref="StationService" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public class StationServiceEntity : BaseValueEntity<StationServiceId, StationService>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the StationServiceEntity class.
    /// </summary>
    public StationServiceEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override StationService ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new StationService(repository, this);
    }
  }
}