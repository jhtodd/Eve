//-----------------------------------------------------------------------
// <copyright file="CacheRegionMapTests.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------

namespace Eve.Tests {
  using System;
  using System.Diagnostics.Contracts;

  using Moq;
  using NUnit.Framework;

  using FreeNet;
  using FreeNet.Debug;

  using Eve;
  using Eve.Data;

  //******************************************************************************
  /// <summary>
  /// Contains test functions for the <see cref="BaseValueCache.CacheRegionMap" />
  /// class.
  /// </summary>
  [TestFixture()]
  public class CacheRegionMapTests {

    #region Classes
    //******************************************************************************
    public class BaseClass {
    }
    //******************************************************************************
    public class ChildClass {
    }
    //******************************************************************************
    public class GrandchildClass : ChildClass {
    }
    #endregion

    #region Test Methods
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="BaseValueCache.CacheRegionMap" /> class.
    /// </summary>
    [Test()]
    public void TestObject() {
      BaseValueCache.CacheRegionMap map = new BaseValueCache.CacheRegionMap();

      Assert.AreEqual(0, map.InnerRegionMap.Count);

      // Register a top-level class
      string addedRegion = typeof(BaseClass).Name;
      map.RegisterType(typeof(BaseClass), addedRegion);

      // Make sure it was added properly to the inner map
      Assert.AreEqual(1, map.InnerRegionMap.Count);
      Assert.That(map.InnerRegionMap[typeof(BaseClass)] == addedRegion);

      // Verify the top-level class is returned successfully
      string region = map.GetRegion(typeof(BaseClass));
      Assert.AreEqual(region, addedRegion);
      Assert.IsTrue(map.IsRegistered(typeof(BaseClass)));

      // Verify that other classes are not
      Assert.Throws<InvalidOperationException>(() => map.GetRegion(typeof(ChildClass)));
      Assert.IsFalse(map.IsRegistered(typeof(ChildClass)));

      // Register a child class
      map.RegisterType(typeof(ChildClass), addedRegion);

      // Verify it was added properly to the inner map
      Assert.AreEqual(2, map.InnerRegionMap.Count);
      Assert.That(map.InnerRegionMap[typeof(ChildClass)] == addedRegion);

      // Verify the newly-registered class is returned successfully
      region = map.GetRegion(typeof(ChildClass));
      Assert.AreEqual(region, addedRegion);
      Assert.IsTrue(map.IsRegistered(typeof(ChildClass)));

      // Verify the grandchild class is not yet in the inner map
      Assert.IsFalse(map.InnerRegionMap.ContainsKey(typeof(GrandchildClass)));

      // Verify that the grandchild class inherits its base class mapping
      region = map.GetRegion(typeof(GrandchildClass));
      Assert.AreEqual(region, addedRegion);
      Assert.IsTrue(map.IsRegistered(typeof(GrandchildClass)));
      
      // Verify that the grandchild class is now in the inner map after being accessed
      Assert.AreEqual(3, map.InnerRegionMap.Count);
      Assert.That(map.InnerRegionMap[typeof(GrandchildClass)] == addedRegion);

      // Verify that the grandchild class is still returned successfully
      region = map.GetRegion(typeof(GrandchildClass));
      Assert.AreEqual(region, addedRegion);
      Assert.IsTrue(map.IsRegistered(typeof(GrandchildClass)));
    }
    #endregion
  }
}