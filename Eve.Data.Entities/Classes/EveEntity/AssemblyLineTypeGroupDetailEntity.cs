//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeGroupDetailEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Industry;

  /// <summary>
  /// The data entity for the <see cref="AssemblyLineTypeGroupDetail" /> class.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Boilerplate classes do not need details documentation headers.")]
  [Table("ramAssemblyLineTypeDetailPerGroup")]
  public class AssemblyLineTypeGroupDetailEntity : EveEntity<AssemblyLineTypeGroupDetail>
  {
    // Check DirectEveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeGroupDetailEntity class.
    /// </summary>
    public AssemblyLineTypeGroupDetailEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [ForeignKey("AssemblyLineTypeId")]
    public virtual AssemblyLineTypeEntity AssemblyLineType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("assemblyLineTypeID", Order = 1)]
    [Key]
    public byte AssemblyLineTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [ForeignKey("GroupId")]
    public virtual GroupEntity Group { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("groupID", Order = 2)]
    [Key]
    public GroupId GroupId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("materialMultiplier")]
    public double MaterialMultiplier { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>    
    [Column("timeMultiplier")]
    public double TimeMultiplier { get; internal set; }

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get { return CreateCacheKey(this.AssemblyLineTypeId, this.GroupId); }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="groupId">
    /// The ID of the group.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCacheKey(byte assemblyLineTypeId, GroupId groupId)
    {
      return (long)((((ulong)(long)assemblyLineTypeId) << 32) | ((ulong)(long)groupId));
    }

    /// <inheritdoc />
    public override AssemblyLineTypeGroupDetail ToAdapter(IEveRepository repository)
    {
      Contract.Assume(repository != null); // TODO: Should not be necessary due to base class requires -- check in future version of static checker
      return new AssemblyLineTypeGroupDetail(repository, this);
    }
  }
}