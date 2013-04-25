//-----------------------------------------------------------------------
// <copyright file="CorporateActivityEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Universe;

  /// <summary>
  /// The data entity for the <see cref="CorporateActivity" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("crpActivities")]
  public class CorporateActivityEntity : BaseValueEntity<CorporateActivityId, CorporateActivity>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CorporateActivityEntity class.
    /// </summary>
    public CorporateActivityEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override CorporateActivity ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new CorporateActivity(repository, this);
    }
  }
}