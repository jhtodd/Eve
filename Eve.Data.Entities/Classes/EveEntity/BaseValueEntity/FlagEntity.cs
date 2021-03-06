﻿//-----------------------------------------------------------------------
// <copyright file="FlagEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// The base class for data entities for EVE flags.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public class FlagEntity : BaseValueEntity<FlagId, Flag>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the FlagEntity class.
    /// </summary>
    public FlagEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int OrderId { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public override Flag ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new Flag(repository, this);
    }
  }
}