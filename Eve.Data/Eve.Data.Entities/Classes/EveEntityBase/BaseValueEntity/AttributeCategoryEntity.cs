//-----------------------------------------------------------------------
// <copyright file="AttributeCategoryEntity.cs" company="Jeremy H. Todd">
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

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The data entity for the <see cref="AttributeCategory" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("dgmAttributeCategories")]
  public class AttributeCategoryEntity : BaseValueEntity<AttributeCategoryId>
  {
    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AttributeCategoryEntity class.
    /// </summary>
    public AttributeCategoryEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override IEveEntityAdapter<EveEntityBase> ToAdapter()
    {
      return new AttributeCategory(this);
    }
  }
}