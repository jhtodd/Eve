//-----------------------------------------------------------------------
// <copyright file="AgentTypeEntity.cs" company="Jeremy H. Todd">
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
  /// The data entity for the <see cref="AgentType" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("agtAgentTypes")]
  public class AgentTypeEntity : BaseValueEntity<AgentTypeId, AgentType>
  {
    // Check InnerEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AgentTypeEntity class.
    /// </summary>
    public AgentTypeEntity() : base()
    {
    }

    /* Methods */

    /// <inheritdoc />
    public override AgentType ToAdapter(IEveRepository container)
    {
      Contract.Assume(container != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new AgentType(container, this);
    }
  }
}