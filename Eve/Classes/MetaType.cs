//-----------------------------------------------------------------------
// <copyright file="MetaType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  using Eve.Data.Entities;
  using Eve.Meta;

  //******************************************************************************
  /// <summary>
  /// Contains meta-information about an EVE item type.
  /// </summary>
  public class MetaType : EntityAdapter<MetaTypeEntity>,
                          IComparable,
                          IComparable<MetaType>,
                          IEquatable<MetaType>,
                          IHasIcon,
                          IHasId<TypeId>,
                          IKeyItem<TypeId> {

    #region Instance Fields
    private MetaGroup _metaGroup;
    private EveType _parentType;
    private EveType _type;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the MetaType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal MetaType(MetaTypeEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the meta group of the item.
    /// </summary>
    /// 
    /// <value>
    /// The meta group of the item.
    /// </value>
    public MetaGroup MetaGroup {
      get {
        Contract.Ensures(Contract.Result<MetaGroup>() != null);

        if (_metaGroup == null) {

          // Load the cached version if available
          _metaGroup = Eve.General.Cache.GetOrAdd<MetaGroup>(MetaGroupId, () => {
            MetaGroupEntity metaGroupEntity = Entity.MetaGroup;
            Contract.Assume(metaGroupEntity != null);

            return new MetaGroup(metaGroupEntity);
          });
        }

        return _metaGroup;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the meta group of the item.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the meta group of the item.
    /// </value>
    public MetaGroupId MetaGroupId {
      get {
        return Entity.MetaGroupId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the parent type.
    /// </summary>
    /// 
    /// <value>
    /// The parent type.
    /// </value>
    public EveType ParentType {
      get {
        Contract.Ensures(Contract.Result<EveType>() != null);

        if (_parentType == null) {

          // Load the cached version if available
          _parentType = Eve.General.Cache.GetOrAdd<EveType>(ParentTypeId, () => {
            EveTypeEntity typeEntity = Entity.ParentType;
            Contract.Assume(typeEntity != null);

            return EveType.Create(typeEntity);
          });
        }

        return _parentType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the parent type.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the parent type.
    /// </value>
    public TypeId ParentTypeId {
      get {
        return Entity.ParentTypeId;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(MetaType other) {
      if (other == null) {
        return 1;
      }

      int result = MetaGroupId.CompareTo(other.MetaGroupId);

      if (result == 0) {
        result = Type.MetaLevel.CompareTo(other.Type.MetaLevel);
      }

      if (result == 0) {
        result = Type.CompareTo(other.Type);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as MetaType);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(MetaType other) {
      if (other == null) {
        return false;
      }

      return TypeId == other.TypeId && ParentTypeId == other.ParentTypeId && MetaGroupId == other.MetaGroupId;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return TypeId.GetHashCode();
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return Type.Name + " (" + MetaGroup.Name + ")";
    }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the item type the value describes.
    /// </summary>
    /// 
    /// <value>
    /// The item type the value describes.
    /// </value>
    protected EveType Type {
      get {
        Contract.Ensures(Contract.Result<EveType>() != null);

        if (_type == null) {

          // Load the cached version if available
          _type = Eve.General.Cache.GetOrAdd<EveType>(TypeId, () => {
            EveTypeEntity typeEntity = Entity.Type;
            Contract.Assume(typeEntity != null);

            return EveType.Create(typeEntity);
          });
        }

        return _type;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item type the value describes.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the item type the value describes.
    /// </value>
    protected TypeId TypeId {
      get {
        return Entity.TypeId;
      }
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      MetaType other = obj as MetaType;
      return CompareTo(other);
    }
    #endregion
    #region IHasIcon Members
    //******************************************************************************
    Icon IHasIcon.Icon {
      get {
        return Type.Icon;
      }
    }
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get {
        return Type.IconId;
      }
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get {
        return TypeId;
      }
    }
    #endregion
    #region IHasId<TypeId> Members
    //******************************************************************************
    TypeId IHasId<TypeId>.Id {
      get {
        return TypeId;
      }
    }
    #endregion
    #region IKeyItem<TypeId> Members
    //******************************************************************************
    TypeId IKeyItem<TypeId>.Key {
      get {
        return TypeId;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of meta types.
  /// </summary>
  public class ReadOnlyMetaTypeCollection : ReadOnlyCollection<MetaType> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyMetaTypeCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyMetaTypeCollection(IEnumerable<MetaType> contents) : base() {
      if (contents != null) {
        foreach (MetaType metaType in contents) {
          Items.AddWithoutCallback(metaType);
        }
      }
    }
    #endregion
  }
}