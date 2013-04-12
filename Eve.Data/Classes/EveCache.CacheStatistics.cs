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
      private ReaderWriterLockSlim padlock;
      private long writes;

      /* Constructors */

      /// <summary>
      /// Initializes a new instance of the CacheStatistics class.
      /// </summary>
      public CacheStatistics()
      {
        this.hits = 0L;
        this.misses = 0L;
        this.padlock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
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

          this.Padlock.EnterReadLock();

          try
          {
            return this.hits;
          }
          finally
          {
            this.Padlock.ExitReadLock();
          }
        }

        internal set
        {
          Contract.Requires(value >= 0L, "The number of cache hits cannot be less than zero.");

          this.Padlock.EnterWriteLock();

          try
          {
            this.hits = value;
          }
          finally
          {
            this.Padlock.ExitWriteLock();
          }
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

          this.Padlock.EnterReadLock();

          try
          {
            return this.misses;
          }
          finally
          {
            this.Padlock.ExitReadLock();
          }
        }

        internal set
        {
          Contract.Requires(value >= 0L, "The number of cache misses cannot be less than zero.");

          this.Padlock.EnterWriteLock();

          try
          {
            this.misses = value;
          }
          finally
          {
            this.Padlock.ExitWriteLock();
          }
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

          this.Padlock.EnterReadLock();

          try
          {
            return this.Hits + this.Misses;
          }
          finally
          {
            this.Padlock.ExitReadLock();
          }
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

          this.Padlock.EnterReadLock();

          try
          {
            return this.writes;
          }
          finally
          {
            this.Padlock.ExitReadLock();
          }
        }

        internal set
        {
          Contract.Requires(value >= 0L, "The number of writes cannot be less than zero.");

          this.Padlock.EnterWriteLock();

          try
          {
            this.writes = value;
          }
          finally
          {
            this.Padlock.ExitWriteLock();
          }
        }
      }

      /// <summary>
      /// Gets the object used to enforce concurrency for statistics
      /// operations.
      /// </summary>
      /// <value>
      /// A <see cref="ReaderWriterLockSlim" /> object used to
      /// enforce concurrency for statistics operations.
      /// </value>
      protected ReaderWriterLockSlim Padlock
      {
        get
        {
          Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
          return this.padlock;
        }
      }

      /* Methods */

      /// <summary>
      /// Resets all statistics to zero.
      /// </summary>
      public void Reset()
      {
        this.Padlock.EnterWriteLock();

        try
        {
          this.Hits = 0L;
          this.Misses = 0L;
          this.Writes = 0L;
        }
        finally
        {
          this.Padlock.ExitWriteLock();
        }
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
        Contract.Invariant(this.padlock != null);
        Contract.Invariant(this.writes >= 0L);
      }
    }
  }
}