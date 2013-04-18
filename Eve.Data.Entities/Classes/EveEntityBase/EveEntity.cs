//-----------------------------------------------------------------------
// <copyright file="EveEntity.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for all EVE game-related data entities.
  /// </summary>
  public abstract class EveEntity 
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
  }
}