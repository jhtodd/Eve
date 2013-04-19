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
  /// The type of the concrete derived class; i.e. to declare a "Ship"
  /// base value, you would do <c>public class Ship : BaseValue&lt;TypeId, int, ShipEntity, Ship&gt;</c>.
  /// I know this sort of pseudo-circular "Curiously Recurring Template Pattern"
  /// is generally bad practice, but it allows me to write many useful methods
  /// (Equals, CompareTo, etc.) at this base class level and save myself an
  /// awful lot of hard-to-maintain boilerplate in derived classes.  Since I
  /// have a lot(!) of derived classes to write, and not many people will ever
  /// see or care about these inner workings, I'm going to do it anyway in the
  /// interests of pragmatism :).
  /// </typeparam>
  /// <remarks>
  /// For classes derived from <c>BaseValue&lt;TId, TEntityId, TEntity, TDerived&gt;</c>,
  /// the values of the <see cref="Id" /> property must be unique across all
  /// instances of the derived class as well as all classes that inherit
  /// from it.
  /// </remarks>
  public abstract partial class BaseValue<TId, TEntityId, TEntity, TDerived>
    : EveEntityAdapter<TEntity>,
      IComparable,
      IComparable<TDerived>,
      IEquatable<TDerived>,
      IEveCacheable,
      IKeyItem<TId>
    where TId : IConvertible, new()
    where TEntity : BaseValueEntity<TEntityId, TDerived>
    where TDerived : BaseValue<TId, TEntityId, TEntity, TDerived>
  {
    private TId id;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the BaseValue class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected BaseValue(IEveRepository container, TEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");

      Contract.Assume(entity.Id != null);
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
          result = "[Unknown " + typeof(TDerived).Name + " (ID " + this.Id.ToString() + ")]";
        }

        Contract.Assume(!string.IsNullOrWhiteSpace(result));
        return result;
      }
    }

    /// <summary>
    /// Gets the key used to cache the current item.
    /// </summary>
    /// <value>
    /// The key used to cache the current item.
    /// </value>
    protected virtual IConvertible CacheKey
    {
      get
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        return this.Id;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public virtual int CompareTo(TDerived other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.Name.CompareTo(other.Name);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as TDerived);
    }

    /// <inheritdoc />
    public virtual bool Equals(TDerived other)
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
      Contract.Requires(entityId != null, "The entity ID cannot be null.");
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

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public abstract partial class BaseValue<TId, TEntityId, TEntity, TDerived> : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      TDerived other = obj as TDerived;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public abstract partial class BaseValue<TId, TEntityId, TEntity, TDerived> : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion

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