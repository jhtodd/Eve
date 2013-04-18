//-----------------------------------------------------------------------
// <copyright file="EveEntity{TAdapter}.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities
{
  using System.Diagnostics.CodeAnalysis;

  using FreeNet.Data.Entity;

  /// <summary>
  /// The base class for EVE-related entities that have an "adapter"
  /// wrapper class that provides a friendlier interface for 
  /// developers.
  /// </summary>
  /// <typeparam name="TAdapter">
  /// The type of the entity adapter which encapsulates this class.
  /// </typeparam>
  public abstract class EveEntity<TAdapter>
    : EveEntity,
      IEveEntity<TAdapter>
    where TAdapter : IEveEntityAdapter<EveEntity<TAdapter>>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveEntity class.
    /// </summary>
    public EveEntity() : base()
    {
    }

    /// <inheritdoc />
    public abstract TAdapter ToAdapter(IEveRepository container);
  }
}