//-----------------------------------------------------------------------
// <copyright file="EveCacheStatistics.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  /// <summary>
  /// Contains statistics about cache usage.
  /// </summary>
  public class EveCacheStatistics : IDisposable
  {
    private long hits;
    private long misses;
    private ReaderWriterLockSlim statisticsLock;
    private long writes;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveCacheStatistics class.
    /// </summary>
    public EveCacheStatistics()
    {
      this.hits = 0L;
      this.misses = 0L;
      this.statisticsLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
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

        this.StatisticsLock.EnterReadLock();

        try
        {
          return this.hits;
        }
        finally
        {
          this.StatisticsLock.ExitReadLock();
        }
      }

      internal set
      {
        Contract.Requires(value >= 0L, "The number of cache hits cannot be less than zero.");

        this.StatisticsLock.EnterWriteLock();

        try
        {
          this.hits = value;
        }
        finally
        {
          this.StatisticsLock.ExitWriteLock();
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

        this.StatisticsLock.EnterReadLock();

        try
        {
          return this.misses;
        }
        finally
        {
          this.StatisticsLock.ExitReadLock();
        }
      }

      internal set
      {
        Contract.Requires(value >= 0L, "The number of cache misses cannot be less than zero.");

        this.StatisticsLock.EnterWriteLock();

        try
        {
          this.misses = value;
        }
        finally
        {
          this.StatisticsLock.ExitWriteLock();
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

        this.StatisticsLock.EnterReadLock();

        try
        {
          return this.Hits + this.Misses;
        }
        finally
        {
          this.StatisticsLock.ExitReadLock();
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

        this.StatisticsLock.EnterReadLock();

        try
        {
          return this.writes;
        }
        finally
        {
          this.StatisticsLock.ExitReadLock();
        }
      }

      internal set
      {
        Contract.Requires(value >= 0L, "The number of writes cannot be less than zero.");

        this.StatisticsLock.EnterWriteLock();

        try
        {
          this.writes = value;
        }
        finally
        {
          this.StatisticsLock.ExitWriteLock();
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
    protected ReaderWriterLockSlim StatisticsLock
    {
      get
      {
        Contract.Ensures(Contract.Result<ReaderWriterLockSlim>() != null);
        return this.statisticsLock;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    /// Resets all statistics to zero.
    /// </summary>
    public void Reset()
    {
      this.StatisticsLock.EnterWriteLock();

      try
      {
        this.Hits = 0L;
        this.Misses = 0L;
        this.Writes = 0L;
      }
      finally
      {
        this.StatisticsLock.ExitWriteLock();
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
    /// Disposes the current object.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether to dispose managed resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.statisticsLock.Dispose();
      }
    }

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.hits >= 0L);
      Contract.Invariant(this.misses >= 0L);
      Contract.Invariant(this.statisticsLock != null);
      Contract.Invariant(this.writes >= 0L);
    }
  }
}