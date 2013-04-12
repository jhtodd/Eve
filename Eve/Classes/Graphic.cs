﻿//-----------------------------------------------------------------------
// <copyright file="Graphic.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about an icon associated with an EVE item.
  /// </summary>
  public sealed partial class Graphic
    : EveEntityAdapter<GraphicEntity>,
      IComparable,
      IComparable<Graphic>,
      IEquatable<Graphic>,
      IEveCacheable,
      IKeyItem<GraphicId>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Graphic class.
    /// </summary>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Graphic(GraphicEntity entity) : base(entity)
    {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }

    /* Properties */

    /// <summary>
    /// Gets a value indicating whether the graphic is collidable.
    /// </summary>
    /// <value>
    /// The value of this property is unknown.
    /// </value>
    public bool Collidable
    {
      get
      {
        return this.Entity.Collidable;
      }
    }

    /// <summary>
    /// Gets the color scheme of the graphic.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> indicating the color scheme of the graphic.
    /// </value>
    public string ColorScheme
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        var result = this.Entity.ColorScheme;

        Contract.Assume(result != null);
        return result;
      }
    }

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
    /// Gets a value indicating the ID of the graphic's directory.
    /// </summary>
    /// <value>
    /// The value of this property is unknown.
    /// </value>
    public int? DirectoryId
    {
      get
      {
        return this.Entity.DirectoryId;
      }
    }

    /// <summary>
    /// Gets a value indicating the ID of the graphic's explosion.
    /// </summary>
    /// <value>
    /// The value of this property is unknown.
    /// </value>
    public int? ExplosionId
    {
      get
      {
        return this.Entity.ExplosionId;
      }
    }

    /// <summary>
    /// Gets the race ID of the graphic.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> specifying the ID of the graphic's race.
    /// </value>
    public string GfxRaceId
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        var result = this.Entity.GfxRaceId;

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <summary>
    /// Gets the location of the graphic as an in-game resource.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that provides the location of the graphic.
    /// </value>
    public string GraphicFile
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        var result = this.Entity.GraphicFile;

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <summary>
    /// Gets the location of the graphic as an in-game resource.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that provides the location of the graphic.
    /// </value>
    public string GraphicName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        var result = this.Entity.GraphicName;

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <summary>
    /// Gets the location of the graphic as an in-game resource.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that provides the location of the graphic.
    /// </value>
    public string GraphicType
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        var result = this.Entity.GraphicType;

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public GraphicId Id
    {
      get
      {
        return this.Entity.Id;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the graphic is obsolete.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the graphic is obsolete; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool Obsolete
    {
      get
      {
        return this.Entity.Obsolete;
      }
    }

    /// <summary>
    /// Gets the key used to cache the current item.
    /// </summary>
    /// <value>
    /// The key used to cache the current item.
    /// </value>
    private object CacheKey
    {
      get { return this.Id; }
    }

    /* Methods */

    /// <inheritdoc />
    public int CompareTo(Graphic other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.Id.Value.CompareTo(other.Id.Value);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as Graphic);
    }

    /// <inheritdoc />
    public bool Equals(Graphic other)
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
      return this.Id.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.GraphicFile;
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public sealed partial class Graphic : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      Graphic other = obj as Graphic;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class Graphic : IEveCacheable
  {
    object IEveCacheable.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion

  #region IKeyItem<GraphicId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class Graphic : IKeyItem<GraphicId>
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Value is accessible via the Id property.")]
    GraphicId IKeyItem<GraphicId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}