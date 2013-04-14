//-----------------------------------------------------------------------
// <copyright file="EveCache.CacheRegionMap.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections;
  using System.Collections.Concurrent;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Reflection;
  using System.Runtime.Caching;
  using System.Threading;

  using Eve.Character;
  using Eve.Universe;

  using FreeNet;

  /// <content>
  /// Contains the definition of the <see cref="CacheRegionMap" /> helper class.
  /// </content>
  public partial class EveCache
  {
    /// <summary>
    /// Defines a mapping between cacheable types and the region strings that
    /// define the domain of their ID values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Some cacheable types don't define a domain for their ID values directly,
    /// but rely on the domain defined by a parent type.  For example, ID values
    /// for <see cref="BlueprintType" /> aren't just unique among blueprints,
    /// but among all EVE types.  So, we define a mapping between
    /// <see cref="BlueprintType" /> and <see cref="EveType" /> to let the
    /// cache know that <see cref="EveType" /> actually defines the ID domain
    /// for blueprints.
    /// </para>
    /// </remarks>
    internal class CacheRegionMap : IDisposable
    {
      // Used to generate the shortest possible region string for a type.
      // Incremented every time a new type is registered.  The resulting
      // numeric value is then used to generate the region string.
      private static long currentKeyIndex = 0L;

      private readonly Dictionary<Type, string> innerRegionMap;
      private readonly ReaderWriterLockSlim padlock;

      /* Constructors */

      /// <summary>
      /// Initializes a new instance of the CacheRegionMap class.
      /// </summary>
      public CacheRegionMap()
      {
        this.padlock = new ReaderWriterLockSlim();
        this.innerRegionMap = new Dictionary<Type, string>();
      }

      /* Properties */

      /// <summary>
      /// Gets the mapping of types to cache regions.
      /// </summary>
      /// <value>
      /// A <see cref="Dictionary{TKey, TValue}" /> containing the mapping from
      /// types to cache regions.
      /// </value>
      protected internal Dictionary<Type, string> InnerRegionMap
      {
        get
        {
          Contract.Ensures(Contract.Result<Dictionary<Type, string>>() != null);
          return this.innerRegionMap;
        }
      }

      /// <summary>
      /// Gets the lock used to synchronize the map.
      /// </summary>
      /// <value>
      /// The <see cref="ReaderWriterLockSlim" /> used to synchronize the map.
      /// </value>
      protected internal ReaderWriterLockSlim Lock
      {
        get
        {
          Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
          return this.padlock;
        }
      }

      /* Methods */

      /// <summary>
      /// Disposes the current instance.
      /// </summary>
      public void Dispose()
      {
        this.Dispose(true);
      }

      /// <summary>
      /// Returns the cache region associated with the specified type.
      /// </summary>
      /// <param name="type">
      /// The type whose cache region to return.
      /// </param>
      /// <returns>
      /// The cache region for the specified type, or the cache region for the first
      /// type up the inheritance chain if no region has been explicitly defined for
      /// the specified type.
      /// </returns>
      /// <exception cref="InvalidOperationException">
      /// Thrown if no region has been registered for <paramref name="type" /> or
      /// any of its base types.
      /// </exception>
      public string GetRegion(Type type)
      {
        Contract.Requires(type != null, "The type cannot be null.");
        Contract.Ensures(Contract.Result<string>() != null);

        string region = null;

        this.Lock.EnterReadLock();

        try
        {
          // First check to see if the current type is already assigned to a region
          if (this.InnerRegionMap.TryGetValue(type, out region))
          {
            Contract.Assume(region != null);
            return region;
          }

          // If not, check to see if the type (or a base type) has an
          // [EveCacheDomainAttribute] attached.  
          var cacheDomainAttributes = type.GetCustomAttributes<EveCacheDomainAttribute>(true);
          Contract.Assume(cacheDomainAttributes != null);
          var cacheDomainAttribute = cacheDomainAttributes.SingleOrDefault();

          if (cacheDomainAttribute != null)
          {
            region = this.ConstructRegionForType(cacheDomainAttribute.CacheDomain);
          }
          else
          {
            // If no EveCacheDomainAttribute was found, just use the type itself
            // as the domain.
            region = this.ConstructRegionForType(type);
          }

          // Associate the region with the type and return
          this.InnerRegionMap.Add(type, region);
          return region;
        }
        finally
        {
          this.Lock.ExitReadLock();
        }

        throw new InvalidOperationException("No cache region has been defined for " + type.FullName + ".");
      }

      /// <summary>
      /// Disposes the current instance.
      /// </summary>
      /// <param name="disposing">Specifies whether to dispose managed resources.</param>
      protected virtual void Dispose(bool disposing)
      {
        if (disposing)
        {
          this.padlock.Dispose();
        }
      }

      /// <summary>
      /// Given a type, constructs a string to represent the cache region key
      /// for that type.
      /// </summary>
      /// <param name="type">
      /// The type for which to generate a cache region key.
      /// </param>
      /// <returns>
      /// A <see cref="String" /> containing a generated cache region key for
      /// <paramref name="type" />.
      /// </returns>
      /// <remarks>
      /// <para>
      /// The method is not guaranteed to return the same value for repeated
      /// calls with the same type.  The returned value is cached after the
      /// first call for a given type, and the method is never called again
      /// for that type.
      /// </para>
      /// </remarks>
      private string ConstructRegionForType(Type type)
      {
        Contract.Requires(type != null, "The type cannot be null.");
        Contract.Ensures(Contract.Result<string>() != null);

        byte[] bytes = BitConverter.GetBytes(Interlocked.Increment(ref currentKeyIndex));

        if (BitConverter.IsLittleEndian) 
        {
          Array.Reverse(bytes);
        }

        return EveCache.ByteArrayToShortString(bytes);
      }

      /// <summary>
      /// Establishes object invariants of the class.
      /// </summary>
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.innerRegionMap != null);
        Contract.Invariant(this.padlock != null);
      }
    }
  }
}