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
    private ReadOnlyTypeMaterialCollection materials;
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

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
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
    /// Gets the collection of materials required to manufacture the product
    /// type.  This is also used to determine which materials are recovered
    /// when a full lot of the product type is reprocessed.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlyTypeMaterialCollection" /> containing the materials.
    /// </value>
    /// <remarks>
    /// <para>
    /// For some types (especially T2 items), additional materials may be
    /// required.  These will be contained in the blueprint type's
    /// <see cref="EveType.Requirements" /> collection.
    /// </para>
    /// <para>
    /// This property is only relevant to types that can be manufactured
    /// or reprocessed.
    /// </para>
    /// </remarks>
    public ReadOnlyTypeMaterialCollection Materials
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyTypeMaterialCollection>() != null);

        // If not already set, construct a collection of this type's attribute values.
        return EveType.LazyInitialize(
          ref this.materials,
          () => ReadOnlyTypeMaterialCollection.Create(this.Repository, this.Entity.Materials));
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int MaxProductionLimit
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 1);

        var result = this.Entity.MaxProductionLimit;

        Contract.Assume(result >= 1);
        return result;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public BlueprintType ParentBlueprintType
    {
      get
      {
        if (this.Entity.ParentBlueprintTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.parentBlueprintType, this.Entity.ParentBlueprintTypeId, () => this.Entity.ParentBlueprintType);
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int? ParentBlueprintTypeId
    {
      get { return this.Entity.ParentBlueprintTypeId; }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public EveType ProductType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.productType, this.Entity.ProductTypeId, () => this.Entity.ProductType);
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ProductTypeId 
    {
      get { return this.Entity.ProductTypeId; }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ProductionTime
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);

        var result = this.Entity.ProductionTime;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ProductivityModifier
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        var result = this.Entity.ProductivityModifier;

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
    public int ResearchCopyTime
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);

        var result = this.Entity.ResearchCopyTime;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ResearchMaterialTime
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);

        var result = this.Entity.ResearchMaterialTime;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ResearchProductivityTime
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);

        var result = this.Entity.ResearchProductivityTime;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public int ResearchTechTime
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        var result = this.Entity.ResearchTechTime;

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
    public short TechLevel
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() > 0);

        var result = this.Entity.TechLevel;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public short WasteFactor
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() >= 0);

        var result = this.Entity.WasteFactor;

        Contract.Assume(result >= 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
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