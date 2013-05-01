//-----------------------------------------------------------------------
// <copyright file="EveCache.ReferenceCollection.cs" company="Jeremy H. Todd">
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
  /// Contains the definition of the <see cref="ReferenceCollection" /> helper class.
  /// </content>
  public partial class EveCache
  {
    /// <summary>
    /// A collection that keeps track of active references to objects.
    /// </summary>
    internal class ReferenceCollection
    {
      private Dictionary<string, object> innerItems;

      /// <summary>
      /// Initializes a new instance of the <see cref="ReferenceCollection" /> class.
      /// </summary>
      public ReferenceCollection()
      {
        this.innerItems = new Dictionary<string, object>();
      }

      /* Properties */

      /// <summary>
      /// Gets the low-level collection of items stored in the reference cache.
      /// </summary>
      /// <value>
      /// The <see cref="Dictionary{TKey, TValue}" /> containing the item in
      /// the reference cache.
      /// </value>
      protected internal Dictionary<string, object> InnerItems
      {
        get
        {
          Contract.Ensures(Contract.Result<Dictionary<string, object>>() != null);
          return this.innerItems;
        }
      }

      /* Methods */

      /// <summary>
      /// Clears the unused contents of the collection.
      /// </summary>
      /// <remarks>
      /// <para>
      /// Only the unused contents of the collection are removed.  Objects which
      /// have been added permanently, or which still have active references, are
      /// not affected.
      /// </para>
      /// </remarks>
      public void Clear()
      {
        // Construct the list of keys of references that are no longer alive
        var keysToDelete = this.InnerItems.Where(x => x.Value is WeakReference && !((WeakReference)x.Value).IsAlive).Select(x => x.Key).ToArray();

        // Remove them.  Leave all non-weak references, and those weak references
        // that are still alive.
        foreach (string key in keysToDelete)
        {
          this.InnerItems.Remove(key);
        }
      }

      /// <summary>
      /// Returns whether an item with the specified key is contained in
      /// the collection.
      /// </summary>
      /// <param name="key">
      /// The key to locate in the collection.
      /// </param>
      /// <returns>
      /// <see langword="true" /> if an item with the specified key is
      /// contained in the collection; otherwise <see langword="false" />.
      /// </returns>
      public bool Contains(string key)
      {
        Contract.Requires(key != null, "The key cannot be null.");

        object value;

        if (!this.InnerItems.TryGetValue(key, out value))
        {
          return false;
        }

        WeakReference weakValue = value as WeakReference;

        // The item is not a weak reference, but a direct object -- return it.
        if (weakValue == null)
        {
          return true;
        }

        value = weakValue.Target;

        // If the weak reference target is null, it's been collected -- clean
        // up by removing that element from the inner dictionary.
        if (value == null)
        {
          this.InnerItems.Remove(key);
          return false;
        }

        return true;
      }

      /// <summary>
      /// Removes and returns the item with the specified key.
      /// </summary>
      /// <param name="key">
      /// The key of the item to remove.
      /// </param>
      /// <returns>
      /// The value that was removed, or <see langword="null" />
      /// if no item was removed.
      /// </returns>
      public object Remove(string key)
      {
        Contract.Requires(key != null, "The key cannot be null.");

        object value;

        if (this.InnerItems.TryGetValue(key, out value))
        {
          if (value is WeakReference)
          {
            value = ((WeakReference)value).Target;
          }

          this.InnerItems.Remove(key);
        }

        return value;
      }

      /// <summary>
      /// Sets the value with the specified key.
      /// </summary>
      /// <param name="key">
      /// The key under which to add the specified value.
      /// </param>
      /// <param name="value">
      /// The value to add.
      /// </param>
      /// <param name="permanent">
      /// Specifies whether to add the item permanently.
      /// </param>
      /// <returns>
      /// <see langword="true" /> if the specified value was successfully
      /// stored under the given key, or <see langword="false" /> if an
      /// item is already stored permanently under that key.
      /// </returns>
      public bool Set(string key, object value, bool permanent)
      {
        Contract.Requires(key != null, "The key cannot be null.");
        Contract.Requires(value != null, "The value to be added cannot be null.");

        object storedValue;

        if (this.InnerItems.TryGetValue(key, out storedValue))
        {
          if (!(storedValue is WeakReference))
          {
            return false;
          }
        }

        this.InnerItems[key] = permanent ? value : new WeakReference(value);
        return true;
      }

      /// <summary>
      /// Attempts to retrieve the item with the specified key, returning
      /// success or failure.
      /// </summary>
      /// <param name="key">
      /// The key of the item to retrieve.
      /// </param>
      /// <param name="value">
      /// The object which will hold the retrieved value.  Output parameter.
      /// </param>
      /// <returns>
      /// <see langword="true" /> if an item with the specified key was
      /// found; otherwise <see langword="false" />.
      /// </returns>
      public bool TryGetValue(string key, out object value)
      {
        Contract.Requires(key != null, "The key cannot be null.");
        Contract.Ensures(Contract.Result<bool>() == false || Contract.ValueAtReturn(out value) != null);

        if (!this.InnerItems.TryGetValue(key, out value))
        {
          return false;
        }

        WeakReference weakValue = value as WeakReference;

        // The item is not a weak reference, but a direct object -- return it.
        if (weakValue == null)
        {
          Contract.Assume(value != null); // TODO: Revisit if contracts are added to the ObjectCache class
          return true;
        }

        value = weakValue.Target;

        // If the weak reference target is null, it's been collected -- clean
        // up by removing that element from the inner dictionary.
        if (value == null)
        {
          this.InnerItems.Remove(key);
          return false;
        }

        return true;
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.innerItems != null);
      }
    }
  }
}