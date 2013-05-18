//-----------------------------------------------------------------------
// <copyright file="TypeMaterial.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;

  /// <summary>
  /// Contains information about the materials required to manufacture
  /// a type of EVE item.  This is also used to determine which materials
  /// are recovered when an item is reprocessed.
  /// </summary>
  public sealed partial class TypeMaterial
    : EveEntityAdapter<TypeMaterialEntity, TypeMaterial>,
      IHasIcon,
      IKeyItem<EveTypeId>
  {
    private EveType materialType;
    private EveType type;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the TypeMaterial class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal TypeMaterial(IEveRepository repository, TypeMaterialEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the type of the required material.
    /// </summary>
    /// <value>
    /// The type of the required material.
    /// </value>
    public EveType MaterialType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.materialType, this.Entity.MaterialTypeId, () => this.Entity.MaterialType);
      }
    }

    /// <summary>
    /// Gets the ID of the required material.
    /// </summary>
    /// <value>
    /// The ID of the required material.
    /// </value>
    public EveTypeId MaterialTypeId
    {
      get { return this.Entity.MaterialTypeId; }
    }

    /// <summary>
    /// Gets the required quantity of the material.
    /// </summary>
    /// <value>
    /// The required quantity of the material.
    /// </value>
    public int Quantity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);

        var result = this.Entity.Quantity;

        Contract.Assume(result > 0);
        return result;
      }
    }

    /// <summary>
    /// Gets the type to which the material requirement applies.
    /// </summary>
    /// <value>
    /// The type to which the material requirement applies.
    /// </value>
    private EveType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.type, this.Entity.TypeId, () => this.Entity.Type);
      }
    }

    /// <summary>
    /// Gets the ID of the type to which the material requirement applies.
    /// </summary>
    /// <value>
    /// The ID of the type to which the material requirement applies.
    /// </value>
    private EveTypeId TypeId
    {
      get { return this.Entity.TypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(TypeMaterial other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Type.CompareTo(other.Type);

      if (result == 0)
      {
        result = this.MaterialType.CompareTo(other.MaterialType);
      }

      if (result == 0)
      {
        result = this.Quantity.CompareTo(other.Quantity);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.MaterialType.Name + " (" + this.Quantity.ToString("#,##0") + ")";
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class TypeMaterial : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.MaterialType.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.MaterialType.IconId; }
    }
  }
  #endregion

  #region IKeyItem<EveTypeId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class TypeMaterial : IKeyItem<EveTypeId>
  {
    EveTypeId IKeyItem<EveTypeId>.Key
    {
      get { return this.MaterialTypeId; }
    }
  }
  #endregion
}