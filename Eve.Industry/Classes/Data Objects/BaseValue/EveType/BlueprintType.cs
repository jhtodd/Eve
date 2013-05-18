//-----------------------------------------------------------------------
// <copyright file="BlueprintType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The type of an EVE station.
  /// </summary>
  public sealed partial class BlueprintType
    : EveType,
      IEveEntityAdapter<BlueprintTypeEntity>
  {
    private BlueprintType parentBlueprintType;
    private EveType productType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the BlueprintType class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal BlueprintType(IEveRepository repository, BlueprintTypeEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    public short MaterialModifier
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = this.Entity.MaterialModifier;

        Contract.Assume(result >= 0);

        return result;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int MaxProductionLimit { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public BlueprintType ParentBlueprintType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int? ParentBlueprintTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public virtual EveType ProductType { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ProductTypeId { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ProductionTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ProductivityModifier { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ResearchCopyTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ResearchMaterialTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ResearchProductivityTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ResearchTechTime { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public short TechLevel { get; internal set; }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public short WasteFactor { get; internal set; }

    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    private new BlueprintTypeEntity Entity
    {
      get
      {
        Contract.Ensures(Contract.Result<BlueprintTypeEntity>() != null);

        return (BlueprintTypeEntity)base.Entity;
      }
    }       
  }

  #region IEveEntityAdapter<BlueprintTypeEntity> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveEntityAdapter{TEntity}" /> interface.
  /// </content>
  public partial class BlueprintType : IEveEntityAdapter<BlueprintTypeEntity>
  {
    BlueprintTypeEntity IEntityAdapter<BlueprintTypeEntity>.Entity
    {
      get { return this.Entity; }
    }
  }
  #endregion
}