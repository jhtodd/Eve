//-----------------------------------------------------------------------
// <copyright file="IEveCacheable.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// The base interface for objects which can be stored in the EVE cache.
  /// </summary>
  [ContractClass(typeof(IEveCacheableContracts))]
  public interface IEveCacheable
  {
    /* Properties */

    /// <summary>
    /// Gets the ID value used to store the object in the cache.
    /// </summary>
    /// <value>
    /// A value which uniquely identifies the item across its entire
    /// cache domain.
    /// </value>
    /// <remarks>
    /// <para>
    /// The item's cache domain is defined on a type-by-type basis
    /// using the optional <see cref="EveCacheDomainAttribute" /> class.
    /// For most types, the type itself is the domain (i.e. ID values
    /// will be unique across all instances of that type, but different
    /// types might have objects with the same ID value).
    /// </para>
    /// <para>
    /// For certain types, however, the cache domain spans several types.
    /// For example, the <see cref="EveType" /> class has several derived
    /// types (<see cref="SkillType" />, <see cref="StationType" />,
    /// and so on), but the ID of every instance of any of those types
    /// is guaranteed to be unique across all those types.  This is 
    /// specified by attaching a <see cref="EveCacheDomainAttribute" />
    /// to the <c>EveType</c> base class, which then filters down to all
    /// derived types.
    /// </para>
    /// <para>
    /// Classes which do not have an <see cref="EveCacheDomainAttribute" />
    /// attached (either directly or to a base class) assume they they
    /// form their own domains, by default.  Any derived classes will have
    /// their own separate domain.
    /// </para>
    /// </remarks>
    object CacheKey { get; }
  }
}