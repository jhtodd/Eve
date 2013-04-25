//-----------------------------------------------------------------------
// <copyright file="EveCache.DomainMap.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Reflection;
  using System.Threading;

  /// <content>
  /// Contains the definition of the <see cref="DomainMap" /> helper class.
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
    internal class DomainMap : IDisposable
    {
      // Used to generate the shortest possible region string for a type.
      // Incremented every time a new type is registered.  The resulting
      // numeric value is then used to generate the region string.
      private static long currentKeyIndex = 0L;

      private readonly Dictionary<Type, string> innerDomainMap;
      private readonly ReaderWriterLockSlim domainMapLock;

      /* Constructors */

      /// <summary>
      /// Initializes a new instance of the DomainMap class.
      /// </summary>
      public DomainMap()
      {
        this.domainMapLock = new ReaderWriterLockSlim();
        this.innerDomainMap = new Dictionary<Type, string>();
      }

      /* Properties */

      /// <summary>
      /// Gets the mapping of types to cache region strings.
      /// </summary>
      /// <value>
      /// A <see cref="Dictionary{TKey, TValue}" /> containing the mapping from
      /// types to cache region strings.
      /// </value>
      protected internal Dictionary<Type, string> InnerDomainMap
      {
        get
        {
          Contract.Ensures(Contract.Result<Dictionary<Type, string>>() != null);
          return this.innerDomainMap;
        }
      }

      /// <summary>
      /// Gets the lock used to synchronize the map.
      /// </summary>
      /// <value>
      /// The <see cref="ReaderWriterLockSlim" /> used to synchronize the map.
      /// </value>
      protected internal ReaderWriterLockSlim DomainMapLock
      {
        get
        {
          Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
          return this.domainMapLock;
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
      /// Returns the cache region string associated with the specified type.
      /// </summary>
      /// <param name="type">
      /// The type whose cache region string to return.
      /// </param>
      /// <returns>
      /// The region string for the cache domain associated with the specified
      /// type.  If <paramref name="type" /> or one of its parent classes has
      /// a <see cref="EveCacheDomainAttribute" /> attached, that will be used
      /// as the domain.  Otherwise, the domain will be <paramref name="type" />
      /// itself.
      /// </returns>
      public string GetRegion(Type type)
      {
        Contract.Requires(type != null, "The type cannot be null.");
        Contract.Ensures(Contract.Result<string>() != null);

        string region = null;

        this.DomainMapLock.EnterReadLock();

        try
        {
          // First check to see if the current type is already assigned to a region
          if (this.InnerDomainMap.TryGetValue(type, out region))
          {
            Contract.Assume(region != null);
            return region;
          }
        }
        finally
        {
          this.DomainMapLock.ExitReadLock();
        }

        // If no region has already been assigned, create one now
        this.DomainMapLock.EnterWriteLock();

        try
        {
          // Make sure no region was added while we were acquiring the
          // write lock.
          if (this.InnerDomainMap.TryGetValue(type, out region))
          {
            Contract.Assume(region != null);
            return region;
          }

          // First, check to see if the type (or a base type) has an
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
          this.InnerDomainMap.Add(type, region);
          return region;
        }
        finally
        {
          this.DomainMapLock.ExitWriteLock();
        }
      }

      /// <summary>
      /// Disposes the current instance.
      /// </summary>
      /// <param name="disposing">Specifies whether to dispose managed resources.</param>
      protected virtual void Dispose(bool disposing)
      {
        if (disposing)
        {
          this.domainMapLock.Dispose();
        }
      }

      /// <summary>
      /// Given a type, constructs a string to represent the cache region key
      /// for that type's cache domain.
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
        Contract.Invariant(this.innerDomainMap != null);
        Contract.Invariant(this.domainMapLock != null);
      }
    }
  }
}