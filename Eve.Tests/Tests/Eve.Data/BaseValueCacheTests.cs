//-----------------------------------------------------------------------
// <copyright file="BaseValueCacheTests.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------

namespace Eve.Tests {
  using System;
  using System.Diagnostics.Contracts;

  using Moq;
  using Moq.Protected;
  using NUnit.Framework;

  using FreeNet;
  using FreeNet.Debug;

  using Eve;
  using Eve.Data;

  //******************************************************************************
  /// <summary>
  /// Contains test functions for the <see cref="BaseValueCache" />
  /// class.
  /// </summary>
  [TestFixture()]
  public class BaseValueCacheTests {

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

    #region Private Methods
    //******************************************************************************
    /// <summary>
    /// Returns a mocked string collection for testing purposes.
    /// </summary>
    /// 
    /// <param name="initialContents">
    /// The initial contents of the collection.
    /// </param>
    /// 
    /// <param name="keyGenerator">
    /// The key generator used to create keys for items added to the collection.
    /// </param>
    /// 
    /// <param name="comparer">
    /// The comparer used to determine equality between keys.
    /// </param>
    /// 
    /// <returns>
    /// The mocked collection containing the specified items, if any.
    /// </returns>
    private Mock<BaseValueCache> GetMockBaseValueCache() {
      Mock<BaseValueCache> result;

      result = new Mock<BaseValueCache>();
      result.CallBase = true;

      return result;
    }
    #endregion

    #region Test Methods
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="BaseValueCache.Clean" /> method.
    /// </summary>
    [Test()]
    public void TestClean() {

      // Create the cache and register our test types
      Mock<BaseValueCache> cache = GetMockBaseValueCache();
      BaseValueCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      BaseValueCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      TestItem[] items = new TestItem[10];
      TestChildItem[] childItems = new TestChildItem[10];
      TestStringItem[] stringItems = new TestStringItem[10];

      // Populate with initial items
      for (int i = 0; i < 10; i++) {
        items[i] = new TestItem(i);
        childItems[i] = new TestChildItem(i + 100); // Add 100 to avoid ID collisions
        stringItems[i] = new TestStringItem("Test" + i.ToString());
        cache.Object.GetOrAdd(items[i]);
        cache.Object.GetOrAdd(childItems[i]);
        cache.Object.GetOrAdd(stringItems[i]);
      }

      // Verify that all test items have been added
      Assert.AreEqual(cache.Object.InnerCache.Count, 30);

      // Now remove all external references and force garbage collection
      items = null;
      childItems = null;
      stringItems = null;
      GC.Collect();

      // Clean the cache
      cache.Object.Clean();

      // Verify that the objects have been removed
      Assert.AreEqual(cache.Object.InnerCache.Count, 0);
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="BaseValueCache.GetOrAdd" /> method.
    /// </summary>
    [Test()]
    public void TestGetOrAdd() {

      // Create the cache and register our test types
      Mock<BaseValueCache> cache = GetMockBaseValueCache();
      BaseValueCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      BaseValueCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      // Verify the cache is initially empty
      Assert.AreEqual(cache.Object.InnerCache.Count, 0);

      // Add a test item to the cache
      TestItem item = new TestItem(5);
      TestItem returnedItem = cache.Object.GetOrAdd(item);
      
      // Verify the method returns the correct value
      Assert.That(item == returnedItem);

      // Verify that the item was added to the cache
      Assert.AreEqual(cache.Object.InnerCache.Count, 1);

      // Add a new item with the same ID
      TestItem newItem = new TestItem(5);
      returnedItem = cache.Object.GetOrAdd(newItem);

      // Verify the method returns the original, cached value, not the new one
      Assert.That(item == returnedItem);

      // Verify that the cache count remains the same
      Assert.AreEqual(cache.Object.InnerCache.Count, 1);
    }
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="BaseValueCache.TryGetValue" /> method.
    /// </summary>
    [Test()]
    public void TestTryGetValue() {

      // Create the cache and register our test types
      Mock<BaseValueCache> cache = GetMockBaseValueCache();
      BaseValueCache.RegionMap.RegisterType(typeof(TestItem), "Test");
      BaseValueCache.RegionMap.RegisterType(typeof(TestStringItem), "StringTest");

      // Verify the cache is initially empty
      Assert.AreEqual(cache.Object.InnerCache.Count, 0);

      // Verify no item with ID 5 is in the cache
      TestItem result;
      bool success = cache.Object.TryGetValue(5, out result);
      Assert.IsFalse(success);
      Assert.IsNull(result);

      // Add a test item to the cache
      TestItem item = new TestItem(5);
      TestItem returnedItem = cache.Object.GetOrAdd(item);

      // Verify the test item is returned
      success = cache.Object.TryGetValue(5, out result);
      Assert.IsTrue(success);
      Assert.AreEqual(result, item);
    }
    #endregion
  }
}