//-----------------------------------------------------------------------
// <copyright file="EveCache.CacheStatistics.cs" company="Jeremy H. Todd">
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
  using System.Runtime.Caching;
  using System.Threading;

  using Eve.Character;
  using Eve.Universe;

  using FreeNet;

  /// <content>
  /// Contains the definition of the <see cref="CacheStatistics" /> helper class.
  /// </content>
  public partial class EveCache
  {
    /// <summary>
    /// Contains statistics about cache usage.
    /// </summary>
    public class CacheStatistics
    {
      private long hits;
      private long misses;
      private long writes;

      /* Constructors */

      /// <summary>
      /// Initializes a new instance of the CacheStatistics class.
      /// </summary>
      public CacheStatistics()
      {
        this.hits = 0L;
        this.misses = 0L;
        this.writes = 0L;
      }

      /* Properties */

      /// <summary>
      /// Gets the number of requests in which a result was successfully found and
      /// returned.
      /// </summary>
      /// <value>
      /// The number of cache hits.
      /// </value>
      public long Hits
      {
        get
        {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return this.hits;
        }

        internal set
        {
          Contract.Requires(value >= 0L, "The number of cache hits cannot be less than zero.");
          this.hits = value;
        }
      }

      /// <summary>
      /// Gets the number of requests in which no result was found.
      /// </summary>
      /// <value>
      /// The number of cache misses.
      /// </value>
      public long Misses
      {
        get
        {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return this.misses;
        }

        internal set
        {
          Contract.Requires(value >= 0L, "The number of cache misses cannot be less than zero.");
          this.misses = value;
        }
      }

      /// <summary>
      /// Gets the total number of requests made to the cache.
      /// </summary>
      /// <value>
      /// The total number of requests (both hits and misses).
      /// </value>
      public long TotalRequests
      {
        get
        {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return this.Hits + this.Misses;
        }
      }

      /// <summary>
      /// Gets the total number of items written to the cache.
      /// </summary>
      /// <value>
      /// The number of items written to the cache.
      /// </value>
      public long Writes
      {
        get
        {
          Contract.Ensures(Contract.Result<long>() >= 0L);
          return this.writes;
        }

        internal set
        {
          Contract.Requires(value >= 0L, "The number of writes cannot be less than zero.");
          this.writes = value;
        }
      }

      /* Methods */

      /// <summary>
      /// Resets all statistics to zero.
      /// </summary>
      public void Reset()
      {
        this.Hits = 0L;
        this.Misses = 0L;
        this.Writes = 0L;
      }

      /// <inheritdoc />
      public override string ToString()
      {
        return "Writes: " + this.Writes.ToString() +
               ", Hits: " + this.Hits.ToString() +
               ", Misses: " + this.Misses.ToString() +
               ", Total Requests: " + this.TotalRequests.ToString();
      }

      /// <summary>
      /// Establishes object invariants of the class.
      /// </summary>
      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.hits >= 0L);
        Contract.Invariant(this.misses >= 0L);
        Contract.Invariant(this.writes >= 0L);
      }
    }
  }
}