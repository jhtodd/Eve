//-----------------------------------------------------------------------
// <copyright file="EveEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for all EVE game-related data entities.
  /// </summary>
  [ContractClass(typeof(EveEntityContracts))]
  public abstract partial class EveEntity 
    : ImmutableEntity,
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

  #region IEveEntity Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveEntity" /> interface.
  /// </content>
  public partial class EveEntity : IEveEntity
  {
    IConvertible IEveEntity.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion
}