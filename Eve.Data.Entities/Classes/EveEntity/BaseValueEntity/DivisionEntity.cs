//-----------------------------------------------------------------------
// <copyright file="DivisionEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Universe;

  /// <summary>
  /// The data entity for the <see cref="Division" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public class DivisionEntity : BaseValueEntity<DivisionId, Division>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the DivisionEntity class.
    /// </summary>
    public DivisionEntity() : base()
    {
    }

    /* Properties */
    
    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public string LeaderType { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public override Division ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new Division(repository, this);
    }
  }
}