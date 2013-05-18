//-----------------------------------------------------------------------
// <copyright file="AgentEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Universe;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The data entity for the <see cref="Agent" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  public partial class AgentEntity 
    : ItemExtensionEntity,
      IEveEntity<Agent>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AgentEntity class.
    /// </summary>
    public AgentEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual AgentTypeEntity AgentType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public AgentTypeId AgentTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual NpcCorporationEntity Corporation { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public long CorporationId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual DivisionEntity Division { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public DivisionId DivisionId { get; internal set; }

    /// <summary>
    /// Gets the ID of the agent.
    /// </summary>
    /// <value>
    /// The ID of the agent.
    /// </value>
    public long Id { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public bool IsLocator { get; internal set; }

    /// <summary>
    /// Gets the <see cref="ItemEntity" /> associated with the current object.
    /// This can be considered the "other half" of the current object: 
    /// <c>ItemInfo</c> holds the basic information about the item, while the
    /// current object holds information specific to the item's current type
    /// (e.g. agent, region, faction, solar system, etc.).
    /// </summary>
    /// <value>
    /// The <see cref="ItemEntity" /> associated with the current object.
    /// </value>
    public virtual ItemEntity ItemInfo { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public byte Level { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ItemEntity Location { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public long LocationId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public short Quality { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual ICollection<EveTypeEntity> ResearchFields { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return this.Id; }
    }

    /* Methods */

    /// <inheritdoc />
    public Agent ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null);
      Contract.Assume(this.ItemInfo != null);
      Contract.Assume(this.ItemInfo.IsAgent);
      return new Agent(repository, this.ItemInfo);
    }
  }
}