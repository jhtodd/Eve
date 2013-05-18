//-----------------------------------------------------------------------
// <copyright file="ActivityEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Industry;

  /// <summary>
  /// The data entity for the <see cref="Activity" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public class ActivityEntity : BaseValueEntity<ActivityId, Activity>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ActivityEntity class.
    /// </summary>
    public ActivityEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public string IconNo { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    public bool Published { get; internal set; }

    /* Methods */

    /// <inheritdoc />
    public override Activity ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new Activity(repository, this);
    }
  }
}