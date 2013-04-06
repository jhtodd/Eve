﻿//-----------------------------------------------------------------------
// <copyright file="MetaType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains meta-information about an EVE item type.
  /// </summary>
  public sealed partial class MetaType
    : EveEntityAdapter<MetaTypeEntity>,
      IComparable,
      IComparable<MetaType>,
      IEquatable<MetaType>,
      IEveCacheable,
      IHasIcon,
      IKeyItem<TypeId>
  {
    private MetaGroup metaGroup;
    private EveType parentType;
    private EveType type;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the MetaType class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal MetaType(MetaTypeEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }

    /* Properties */

    /// <summary>
    /// Gets the meta group of the item.
    /// </summary>
    /// <value>
    /// The meta group of the item.
    /// </value>
    public MetaGroup MetaGroup
    {
      get
      {
        Contract.Ensures(Contract.Result<MetaGroup>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.metaGroup ?? (this.metaGroup = Eve.General.Cache.GetOrAdd<MetaGroup>(this.MetaGroupId, () => (MetaGroup)this.Entity.MetaGroup.ToAdapter()));
      }
    }

    /// <summary>
    /// Gets the ID of the meta group of the item.
    /// </summary>
    /// <value>
    /// The ID of the meta group of the item.
    /// </value>
    public MetaGroupId MetaGroupId
    {
      get { return Entity.MetaGroupId; }
    }

    /// <summary>
    /// Gets the parent type.
    /// </summary>
    /// <value>
    /// The parent type.
    /// </value>
    public EveType ParentType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.parentType ?? (this.parentType = Eve.General.Cache.GetOrAdd<EveType>(this.ParentTypeId, () => EveType.Create(Entity.ParentType)));
      }
    }

    /// <summary>
    /// Gets the ID of the parent type.
    /// </summary>
    /// <value>
    /// The ID of the parent type.
    /// </value>
    public TypeId ParentTypeId
    {
      get { return Entity.ParentTypeId; }
    }

    /// <summary>
    /// Gets the item type the value describes.
    /// </summary>
    /// <value>
    /// The item type the value describes.
    /// </value>
    private EveType Type
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.type ?? (this.type = Eve.General.Cache.GetOrAdd<EveType>(this.TypeId, () => EveType.Create(Entity.Type)));
      }
    }

    /// <summary>
    /// Gets the ID of the item type the value describes.
    /// </summary>
    /// <value>
    /// The ID of the item type the value describes.
    /// </value>
    private TypeId TypeId
    {
      get { return Entity.TypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public int CompareTo(MetaType other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.MetaGroupId.CompareTo(other.MetaGroupId);

      if (result == 0)
      {
        result = this.Type.MetaLevel.CompareTo(other.Type.MetaLevel);
      }

      if (result == 0)
      {
        result = this.Type.CompareTo(other.Type);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as MetaType);
    }

    /// <inheritdoc />
    public bool Equals(MetaType other)
    {
      if (other == null)
      {
        return false;
      }

      return this.TypeId == other.TypeId && this.ParentTypeId == other.ParentTypeId && this.MetaGroupId == other.MetaGroupId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this.TypeId.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Type.Name + " (" + this.MetaGroup.Name + ")";
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class MetaType : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      MetaType other = obj as MetaType;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public partial class MetaType : IEveCacheable
  {
    object IEveCacheable.CacheKey
    {
      get { return this.TypeId; }
    }
  }
  #endregion

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class MetaType : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.Type.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.Type.IconId; }
    }
  }
  #endregion

  #region IKeyItem<TypeId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class MetaType : IKeyItem<TypeId>
  {
    TypeId IKeyItem<TypeId>.Key
    {
      get { return this.TypeId; }
    }
  }
  #endregion
}