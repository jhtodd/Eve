//-----------------------------------------------------------------------
// <copyright file="EveEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.CodeAnalysis;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for all EVE game-related data entities.
  /// </summary>
  public abstract partial class EveEntity 
    : ImmutableEntity,
      IEveCacheable,
      IEveEntity
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveEntity class.
    /// </summary>
    public EveEntity() : base()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the ID value used to store the object in the cache.
    /// </summary>
    /// <value>
    /// A value which uniquely identifies the entity.
    /// </value>
    protected internal abstract IConvertible CacheKey { get; }
  }
  
  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public partial class EveEntity : IEveCacheable
  {
    System.IConvertible IEveCacheable.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion
}