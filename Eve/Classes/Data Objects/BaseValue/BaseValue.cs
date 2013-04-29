//-----------------------------------------------------------------------
// <copyright file="BaseValue.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// The base class for various immutable values contained in the EVE database.
  /// This class provides only the basic fields that are common to most, if not
  /// all, records in the database.  Derived classes will provide additional
  /// fields.
  /// </summary>
  /// <typeparam name="TId">
  /// The type of value that uniquely identifies an instance of a particular
  /// type of value.
  /// </typeparam>
  /// <typeparam name="TEntityId">
  /// The type of value that uniquely identifies an instance of 
  /// <typeparamref name="TEntity" />.  This is usually the same as
  /// <typeparamref name="TId" /> unless the entity adapter has a special
  /// ID type.
  /// </typeparam>
  /// <typeparam name="TEntity">
  /// The type of the data entity which provides data backing for
  /// <typeparamref name="TDerived" />.
  /// </typeparam>
  /// <typeparam name="TDerived">
  /// The type of the concrete derived class.  For more information, see
  /// the <see cref="EveDataObject{TDerived}" /> class.
  /// </typeparam>
  /// <remarks>
  /// For classes derived from <c>BaseValue&lt;TId, TEntityId, TEntity, TDerived&gt;</c>,
  /// the values of the <see cref="Id" /> property must be unique across all
  /// instances of the derived class as well as all classes that inherit
  /// from it.
  /// </remarks>
  public abstract partial class BaseValue<TId, TEntityId, TEntity, TDerived>
    : EveEntityAdapter<TEntity, TDerived>,
      IKeyItem<TId>
    where TId : IConvertible, new()
    where TEntityId : struct, IConvertible
    where TEntity : BaseValueEntity<TEntityId, TDerived>
    where TDerived : BaseValue<TId, TEntityId, TEntity, TDerived>
  {
    private readonly TId id;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the BaseValue class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected BaseValue(IEveRepository repository, TEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");

      this.id = this.ConvertEntityId(entity.Id);
    }

    /* Properties */

    /// <summary>
    /// Gets the description of the item.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that describes the item.
    /// </value>
    public string Description
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.Description ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public TId Id
    {
      get
      {
        Contract.Ensures(Contract.Result<TId>() != null);
        return this.id;
      }
    }

    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that provides the name of the item.  This value is
    /// not necessarily unique.
    /// </value>
    public string Name
    {
      get
      {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        var result = Entity.Name;

        // A very few entries have null names.  Convert to a reasonable default.
        // Performance is not a concern since it's literally like a dozen or so.
        if (string.IsNullOrWhiteSpace(result))
        {
          result = "[Unnamed " + typeof(TDerived).Name + " " + this.Id.ToString() + "]";
        }

        Contract.Assume(!string.IsNullOrWhiteSpace(result));
        return result;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(TDerived other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.Name.CompareTo(other.Name);
    }

    /// <inheritdoc />
    public override bool Equals(TDerived other)
    {
      if (other == null)
      {
        return false;
      }

      return this.Id.Equals(other.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.GetType().GetHashCode(), this.Id.GetHashCode());
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Name;
    }

    /// <summary>
    /// Converts the specified entity ID to the adapter ID type.
    /// </summary>
    /// <param name="entityId">
    /// The entity ID to convert.
    /// </param>
    /// <returns>
    /// The converted ID value.
    /// </returns>
    protected virtual TId ConvertEntityId(TEntityId entityId)
    {
      Contract.Ensures(Contract.Result<TId>() != null);

      // Attempt automatic conversion
      TId result = entityId.ConvertTo<TId>();
      Contract.Assume(result != null);

      return result;
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.id != null);
    }
  }

  #region IKeyItem<TId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public abstract partial class BaseValue<TId, TEntityId, TEntity, TDerived> : IKeyItem<TId>
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Value is accessible via the Id property.")]
    TId IKeyItem<TId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}