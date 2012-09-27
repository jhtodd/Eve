//-----------------------------------------------------------------------
// <copyright file="EveCacheTests.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------

namespace Eve.Tests {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Moq;
  using Moq.Protected;
  using NUnit.Framework;

  using FreeNet;
  using FreeNet.Debug;

  using Eve;
  using Eve.Data;
  using System.Runtime.Caching;

  //******************************************************************************
  /// <summary>
  /// Contains test functions for the <see cref="EveCache" />
  /// class.
  /// </summary>
  [TestFixture()]
  public class EveCacheTests {

    #region Classes
    //******************************************************************************
    /// <summary>
    /// A class for testing cache storage functions.
    /// </summary>
    public class TestItem : IHasId<int> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the TestItem class.
      /// </summary>
      /// 
      /// <param name="id">
      /// The ID value.
      /// </param>
      public TestItem(int id) {
        Id = id;
      }
      #endregion
      #region Public Properties
      //******************************************************************************
      /// <summary>
      /// Gets or sets the ID value.
      /// </summary>
      /// 
      /// <value>
      /// The ID value.
      /// </value>
      public int Id { get; set; }
      #endregion
      #region IHasId Members
      //******************************************************************************
      object IHasId.Id {
        get { return Id; }
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// A class for testing cache storage functions of derived types.
    /// </summary>
    public class TestChildItem : TestItem {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the TestItem class.
      /// </summary>
      /// 
      /// <param name="id">
      /// The ID value.
      /// </param>
      public TestChildItem(int id) : base(id) {
      }
      #endregion
    }

    //******************************************************************************
    /// <summary>
    /// Another class for testing cache storage functions with string IDs.
    /// </summary>
    public class TestStringItem : IHasId<string> {

      #region Constructors/Finalizers
      //******************************************************************************
      /// <summary>
      /// Initializes a new instance of the TestItem class.
      /// </summary>
      /// 
      /// <param name="id">
      /// The ID value.
      /// </param>
      public TestStringItem(string id) {
        Id = id;
      }
      #endregion
      #region Public Properties
      //******************************************************************************
      /// <summary>
      /// Gets or sets the ID value.
      /// </summary>
      /// 
      /// <value>
      /// The ID value.
      /// </value>
      public string Id { get; set; }
      #endregion
      #region IHasId Members
      //******************************************************************************
      object IHasId.Id {
        get { return Id; }
      }
      #endregion
    }
    #endregion

    #region Test Methods
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="EveCache.AddOrReplace" /> method.
    /// </summary>
    [Test()]
    public void TestAddOrReplace() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      // Verify the cache is initially empty
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 0);

      // Add a test item to the cache
      TestItem item = new TestItem(5);
      TestItem returnedItem = cache.GetOrAdd(item);
      Assert.AreEqual(cache.Statistics.Hits, 0);
      Assert.AreEqual(cache.Statistics.Misses, 1);

      // Verify the method returns the correct value
      Assert.That(item == returnedItem);
      Assert.AreEqual(cache.Statistics.Writes, 1);

      // Verify that the item was added to the cache
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 1);

      // Add a new item with the same ID
      TestItem newItem = new TestItem(5);
      cache.AddOrReplace(newItem);

      // Retrieve the newly-added item and make sure the cache returns
      // the new value
      cache.TryGetValue(5, out item);
      Assert.That(item == newItem);
      Assert.AreEqual(cache.Statistics.Writes, 2);

      // Verify that the cache count remains the same
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 1);
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="EveCache.Clear" /> method.
    /// </summary>
    [Test()]
    public void TestClear() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      TestItem[] items = new TestItem[10];
      TestChildItem[] childItems = new TestChildItem[10];
      TestStringItem[] stringItems = new TestStringItem[10];

      // Populate with initial items
      for (int i = 0; i < 10; i++) {
        items[i] = new TestItem(i);
        childItems[i] = new TestChildItem(i + 100); // Add 100 to avoid ID collisions
        stringItems[i] = new TestStringItem("Test" + i.ToString());
        cache.GetOrAdd(items[i]);
        cache.GetOrAdd(childItems[i]);
        cache.GetOrAdd(stringItems[i]);
      }

      // Verify that all test items have been added
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 30);
      Assert.AreEqual(cache.Statistics.Writes, 30);

      // Clean the cache
      cache.Clear();

      // Verify that the objects have been removed
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 0);
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="EveCache.Clear" /> method when items with
    /// a key other than the EVE prefix are present.
    /// </summary>
    [Test()]
    public void TestClearNonPrefix() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      TestItem[] items = new TestItem[10];
      TestChildItem[] childItems = new TestChildItem[10];
      TestStringItem[] stringItems = new TestStringItem[10];

      // Populate with initial items
      for (int i = 0; i < 10; i++) {
        items[i] = new TestItem(i);
        childItems[i] = new TestChildItem(i + 100); // Add 100 to avoid ID collisions
        stringItems[i] = new TestStringItem("Test" + i.ToString());
        cache.GetOrAdd(items[i]);
        cache.GetOrAdd(childItems[i]);
        cache.GetOrAdd(stringItems[i]);
      }

      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 30);
      Assert.AreEqual(cache.Statistics.Writes, 30);

      cache.InnerCache.Add("NON-PREFIX-1", "Test1", new CacheItemPolicy());
      cache.InnerCache.Add("NON-PREFIX-2", "Test2", new CacheItemPolicy());

      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 32);

      // Verify that all test items have been added

      // Clean the cache
      cache.Clear();

      // Verify that the objects with the EVE prefix were removed but the others were not affected
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 2);
      Assert.That(cache.InnerCache.Contains("NON-PREFIX-1"));
      Assert.That(cache.InnerCache.Contains("NON-PREFIX-2"));
    }
    //******************************************************************************
    /// <summary>
    /// Test method for enumerating the <see cref="EveCache" />.
    /// </summary>
    [Test()]
    public void TestEnumerate() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      TestItem[] items = new TestItem[10];
      TestChildItem[] childItems = new TestChildItem[10];
      TestStringItem[] stringItems = new TestStringItem[10];

      // Populate with initial items
      for (int i = 0; i < 10; i++) {
        items[i] = new TestItem(i);
        childItems[i] = new TestChildItem(i + 100); // Add 100 to avoid ID collisions
        stringItems[i] = new TestStringItem("Test" + i.ToString());
        cache.GetOrAdd(items[i]);
        cache.GetOrAdd(childItems[i]);
        cache.GetOrAdd(stringItems[i]);
      }

      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 30);
      Assert.AreEqual(cache.Statistics.Writes, 30);

      cache.InnerCache.Add("NON-PREFIX-1", "Test1", new CacheItemPolicy());
      cache.InnerCache.Add("NON-PREFIX-2", "Test2", new CacheItemPolicy());

      // Enumerate through the cache
      var enumItems = ((IEnumerable<KeyValuePair<string, object>>) cache).ToArray();

      // Verify that the correct objects were returns
      Assert.AreEqual(enumItems.Length, 30);
      Assert.That(enumItems.All(x => x.Key.StartsWith(EveCache.PREFIX)));
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="EveCache.GetOrAdd" /> method.
    /// </summary>
    [Test()]
    public void TestGetOrAdd() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      // Verify the cache is initially empty
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 0);

      // Add a test item to the cache
      TestItem item = new TestItem(5);
      TestItem returnedItem = cache.GetOrAdd(item);
      Assert.AreEqual(cache.Statistics.Hits, 0);
      Assert.AreEqual(cache.Statistics.Misses, 1);
      
      // Verify the method returns the correct value
      Assert.That(item == returnedItem);
      Assert.AreEqual(cache.Statistics.Writes, 1);

      // Verify that the item was added to the cache
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 1);

      // Add a new item with the same ID
      TestItem newItem = new TestItem(5);
      returnedItem = cache.GetOrAdd(newItem);
      Assert.AreEqual(cache.Statistics.Hits, 1);
      Assert.AreEqual(cache.Statistics.Misses, 1);

      // Verify the method returns the original, cached value, not the new one
      Assert.That(item == returnedItem);
      Assert.AreEqual(cache.Statistics.Writes, 1);

      // Verify that the cache count remains the same
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 1);
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="EveCache.GetOrAdd" /> method, passing in
    /// a value factory.
    /// </summary>
    [Test()]
    public void TestGetOrAddValueFactory() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      // Verify the cache is initially empty
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 0);

      // Add a test item to the cache
      TestItem item = new TestItem(5);
      TestItem returnedItem = cache.GetOrAdd(item.Id, () => item);
      Assert.AreEqual(cache.Statistics.Hits, 0);
      Assert.AreEqual(cache.Statistics.Misses, 1);

      // Verify the method returns the correct value
      Assert.That(item == returnedItem);
      Assert.AreEqual(cache.Statistics.Writes, 1);

      // Verify that the item was added to the cache
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 1);

      // Add a new item with the same ID
      TestItem newItem = new TestItem(5);
      returnedItem = cache.GetOrAdd(newItem.Id, () => newItem);
      Assert.AreEqual(cache.Statistics.Hits, 1);
      Assert.AreEqual(cache.Statistics.Misses, 1);

      // Verify the method returns the original, cached value, not the new one
      Assert.That(item == returnedItem);
      Assert.AreEqual(cache.Statistics.Writes, 1);

      // Verify that the cache count remains the same
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 1);
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="EveCache.Clear" /> method.
    /// </summary>
    [Test()]
    public void TestRemove() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      TestItem[] items = new TestItem[10];
      TestChildItem[] childItems = new TestChildItem[10];
      TestStringItem[] stringItems = new TestStringItem[10];

      // Populate with initial items
      for (int i = 0; i < 10; i++) {
        items[i] = new TestItem(i);
        childItems[i] = new TestChildItem(i + 100); // Add 100 to avoid ID collisions
        stringItems[i] = new TestStringItem("Test" + i.ToString());
        cache.GetOrAdd(items[i]);
        cache.GetOrAdd(childItems[i]);
        cache.GetOrAdd(stringItems[i]);
      }

      // Verify that all test items have been added
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 30);

      // Remove the first 10 items
      foreach (TestItem test1 in items) {
        var result = cache.Remove<TestItem>(test1.Id);

        // Verify the return value
        Assert.AreEqual(result, test1);
      }

      // Verify that the objects have been removed
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 20);

      // Try to remove a non-existent item
      var value = cache.Remove<TestItem>("Nonexistent");
      Assert.IsNull(value);

      // Remove the next 10 items
      foreach (TestChildItem test2 in childItems) {
        var result = cache.Remove<TestChildItem>(test2.Id);

        // Verify the return value
        Assert.AreEqual(result, test2);
      }

      // Verify that the objects have been removed
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 10);

      // Remove the last 10 items
      foreach (TestStringItem test3 in stringItems) {
        var result = cache.Remove<TestStringItem>(test3.Id);

        // Verify the return value
        Assert.AreEqual(result, test3);
      }

      // Verify that the objects have been removed
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 0);
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="EveCache.TryGetValue" /> method.
    /// </summary>
    [Test()]
    public void TestTryGetValue() {

      // Create the cache and register our test types
      EveCache cache = new EveCache(new MemoryCache("Eve.Tests"));
      EveCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      EveCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      // Verify the cache is initially empty
      Assert.AreEqual(((IEnumerable<KeyValuePair<string, object>>) cache.InnerCache).Count(), 0);

      // Verify no item with ID 5 is in the cache
      TestItem result;
      bool success = cache.TryGetValue(5, out result);
      Assert.IsFalse(success);
      Assert.IsNull(result);
      Assert.AreEqual(cache.Statistics.Hits, 0);
      Assert.AreEqual(cache.Statistics.Misses, 1);

      // Add a test item to the cache
      TestItem item = new TestItem(5);
      TestItem returnedItem = cache.GetOrAdd(item);
      Assert.AreEqual(cache.Statistics.Hits, 0);
      Assert.AreEqual(cache.Statistics.Misses, 2);

      // Verify the test item is returned
      success = cache.TryGetValue(5, out result);
      Assert.IsTrue(success);
      Assert.AreEqual(result, item);
      Assert.AreEqual(cache.Statistics.Hits, 1);
      Assert.AreEqual(cache.Statistics.Misses, 2);
    }
    #endregion
  }
}