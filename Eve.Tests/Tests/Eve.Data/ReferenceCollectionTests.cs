//-----------------------------------------------------------------------
// <copyright file="ReferenceCollectionTests.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Caching;

  using Eve.Data;

  using NUnit.Framework;

  /// <summary>
  /// Contains test functions for the <see cref="EveCache.ReferenceCollection" />
  /// class.
  /// </summary>
  [TestFixture]
  public class ReferenceCollectionTests
  {
    #region Test Methods
    /// <summary>
    /// Tests adding a permanent reference.
    /// </summary>
    [Test]
    public void TestAddPermanent()
    {
      EveCache.ReferenceCollection collection = new EveCache.ReferenceCollection();

      string addedKey = "Permanent";
      object addedValue = new object();

      // Add our test item to the collection
      collection.Set(addedKey, addedValue, true);
      Assert.That(collection.Contains(addedKey));

      // Ensure it was added properly
      object storedValue;
      Assert.IsTrue(collection.TryGetValue(addedKey, out storedValue));
      Assert.AreEqual(storedValue, addedValue);

      // Make sure it wasn't removed when the collection was cleared
      collection.Clear();
      Assert.That(collection.Contains(addedKey));
      Assert.IsTrue(collection.TryGetValue(addedKey, out storedValue));
      Assert.AreEqual(storedValue, addedValue);
    }

    /// <summary>
    /// Tests adding a weak reference.
    /// </summary>
    [Test]
    public void TestAddMixed()
    {
      EveCache.ReferenceCollection collection = new EveCache.ReferenceCollection();

      // The collection of permanent items to add
      var permanentItems = new[] 
      { 
        new { Key = "Permanent1", Value = new object() },
        new { Key = "Permanent2", Value = new object() },
        new { Key = "Permanent3", Value = new object() },
      };

      // The collection of weak items to add
      var weakItems = new[] 
      { 
        new { Key = "Weak1", Value = new object() },
        new { Key = "Weak2", Value = new object() },
        new { Key = "Weak3", Value = new object() },
      };

      foreach (var item in permanentItems)
      {
        collection.Set(item.Key, item.Value, true);
        Assert.That(collection.Contains(item.Key));

        // Ensure it was added properly
        object storedValue;
        Assert.IsTrue(collection.TryGetValue(item.Key, out storedValue));
        Assert.AreEqual(storedValue, item.Value);
        Assert.That(collection.InnerItems[item.Key].GetType() == item.Value.GetType());
      }

      // Make sure the permanent items were added successfully
      Assert.That(collection.InnerItems.Count == permanentItems.Length);

      // Add our weak items to the collection
      foreach (var item in weakItems)
      {
        collection.Set(item.Key, item.Value, false);
        Assert.That(collection.Contains(item.Key));

        // Ensure it was added properly
        object storedValue;
        Assert.IsTrue(collection.TryGetValue(item.Key, out storedValue));
        Assert.AreEqual(storedValue, item.Value);
        Assert.That(collection.InnerItems[item.Key].GetType() == typeof(WeakReference));
      }

      // Make sure all items have been added
      Assert.That(collection.InnerItems.Count == permanentItems.Length + weakItems.Length);

      // Clear the collection -- nothing should happen at this point
      collection.Clear();

      // Weak references are still alive -- count should be the same
      Assert.That(collection.InnerItems.Count == permanentItems.Length + weakItems.Length);
    }
    #endregion
  }
}