//-----------------------------------------------------------------------
// <copyright file="BaseValue.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;
  using FreeNet.Extensions;

  using Eve.Data;
  using Eve.Entities;

  //******************************************************************************
  /// <summary>
  /// The base class for various immutable values contained in the EVE database.
  /// This class provides only the basic fields that are common to most, if not
  /// all, records in the database.  Derived classes will provide additional
  /// fields.
  /// </summary>
  /// 
  /// <typeparam name="TId">
  /// The type of value that uniquely identifies an instance of a particular
  /// type of value.
  /// </typeparam>
  /// 
  /// <typeparam name="TEntityId">
  /// The type of value that uniquely identifies an instance of 
  /// <typeparamref name="TEntity" />.  This is usually the same as
  /// <typeparamref name="TId" /> unless the entity adapter has a special
  /// ID type.
  /// </typeparam>
  /// 
  /// <typeparam name="TEntity">
  /// The type of the data entity which provides data backing for
  /// <typeparamref name="TDerived" />.
  /// </typeparam>
  /// 
  /// <typeparam name="TDerived">
  /// The type of the concrete derived class; i.e. to declare a "Ship"
  /// base value, you would do <c>public class Ship : BaseValue<int, ShipEntity, Ship></c>.
  /// I know this sort of pseudo-circular "Curiously Recurring Template Pattern"
  /// is generally bad practice, but it allows me to write many useful methods
  /// (Equals, CompareTo, etc.) at this base class level and save myself an
  /// awful lot of hard-to-maintain boilerplate in derived classes.  Since I
  /// have a lot(!) of derived classes to write, and not many people will ever
  /// see or care about these inner workings, I'm going to do it anyway in the
  /// interests of pragmatism. :)
  /// </typeparam>
  /// 
  /// <remarks>
  /// For classes derived from <c>BaseValue&lt;TId, TEntity, TDerived&gt;</c>,
  /// the values of the <see cref="Id" /> property must be unique across all
  /// instances of the derived class as well as all classes that inherit
  /// from it.
  /// </remarks>
  public abstract class BaseValue<TId, TEntityId, TEntity, TDerived> : EntityAdapter<TEntity>,
                                                                       IComparable,
                                                                       IComparable<TDerived>,
                                                                       IEquatable<TDerived>,
                                                                       IHasId<TId>, 
                                                                       IKeyItem<TId>
                                                                       where TId : new()
                                                                       where TEntity : BaseValueEntity<TEntityId>
                                                                       where TDerived : BaseValue<TId, TEntityId, TEntity, TDerived> {

    #region Instance Fields
    private TId _id;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the BaseValue class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public BaseValue(TEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);

      Contract.Assume(entity.Id != null);
      _id = ConvertEntityId(entity.Id);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
      Contract.Invariant(_id != null);
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the description of the item.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="string" /> that describes the item.
    /// </value>
    public string Description {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        var result = Entity.Description;
        Contract.Assume(result != null);

        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public TId Id {
      get {
        Contract.Ensures(Contract.Result<TId>() != null);
        return _id;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="string" /> that provides the name of the item.  This value is
    /// not necessarily unique.
    /// </value>
    public string Name {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        var result = Entity.Name;
        Contract.Assume(!string.IsNullOrWhiteSpace(result));

        return result;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(TDerived other) {
      if (other == null) {
        return 1;
      }

      return Name.CompareTo(other.Name);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as TDerived);
    }
    //******************************************************************************
    /// <summary>
    /// Returns a value indicating whether the current instance is equal to the
    /// specified object.
    /// </summary>
    /// 
    /// <param name="obj">
    /// The object to compare to the current object.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> is equal to the current
    /// instance; otherwise <see langword="false" />.
    /// </returns>
    public virtual bool Equals(TDerived other) {
      if (other == null) {
        return false;
      }

      return Id.Equals(other.Id);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return FreeNet.Methods.GetCompoundHashCode(this.GetType().GetHashCode(), Id.GetHashCode());
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return Name;
    }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item within its cache region.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="Int32" /> specifying a unique ID for the item within the
    /// cache region established for the item's type.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// For all known EVE values with 32-bit or 64-bit integer IDs, the value
    /// of <see cref="GetHashCode"/> is sufficient to serve as a cache ID.
    /// </para>
    /// </remarks>
    protected virtual int CacheID {
      get {
        return Id.GetHashCode();
      }
    }
    #endregion
    #region Protected Methods
    //******************************************************************************
    /// <summary>
    /// Converts the specified entity ID to the adapter ID type.
    /// </summary>
    /// 
    /// <param name="entityId">
    /// The entity ID to convert.
    /// </param>
    /// 
    /// <returns>
    /// The converted ID value.
    /// </returns>
    protected virtual TId ConvertEntityId(TEntityId entityId) {
      Contract.Requires(entityId != null, "The entity ID cannot be null.");
      Contract.Ensures(Contract.Result<TId>() != null);

      // Attempt automatic conversion
      TId result = entityId.ConvertTo<TId>();
      Contract.Assume(result != null);

      return result;
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      TDerived other = obj as TDerived;
      return CompareTo(other);
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get { return Id; }
    }
    #endregion
    #region IKeyItem<TId> Members
    //******************************************************************************
    TId IKeyItem<TId>.Key {
      get { return Id; }
    }
    #endregion
  }
}