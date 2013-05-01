//-----------------------------------------------------------------------
// <copyright file="DomainMapTests.cs" company="Jeremy H. Todd">
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
  /// Contains test functions for the <see cref="EveCache.DomainMap" />
  /// class.
  /// </summary>
  [TestFixture]
  public class DomainMapTests
  {
    #region Test Methods
    /// <summary>
    /// Test method for the <see cref="EveCache.DomainMap.GetRegion" /> method.
    /// </summary>
    [Test]
    public void TestGetRegion()
    {
      EveCache.DomainMap map = new EveCache.DomainMap();

      // Verify that the map is initially empty
      Assert.That(map.InnerDomainMap.Count == 0);

      // Get the region for the default parent class and verify it was cached
      string defaultParentRegion = map.GetRegion(typeof(ParentWithDefaultDomain));
      Assert.IsTrue(map.InnerDomainMap.ContainsKey(typeof(ParentWithDefaultDomain)));
      Assert.AreEqual(map.InnerDomainMap[typeof(ParentWithDefaultDomain)], defaultParentRegion);

      // Get the region for the default child class and verify it was cached
      string defaultChildRegion = map.GetRegion(typeof(ChildWithDefaultDomain));
      Assert.IsTrue(map.InnerDomainMap.ContainsKey(typeof(ChildWithDefaultDomain)));
      Assert.AreEqual(map.InnerDomainMap[typeof(ChildWithDefaultDomain)], defaultChildRegion);

      // The default parent and child domains should be different
      Assert.AreNotEqual(defaultParentRegion, defaultChildRegion);

      // Get the region for the custom parent class and verify it was cached
      string customParentRegion = map.GetRegion(typeof(ParentWithCustomDomain));
      Assert.IsTrue(map.InnerDomainMap.ContainsKey(typeof(ParentWithCustomDomain)));
      Assert.AreEqual(map.InnerDomainMap[typeof(ParentWithCustomDomain)], customParentRegion);

      // Get the region for the child of the custom parent class and verify it was cached
      string customChildRegion = map.GetRegion(typeof(ChildWithCustomDomain));
      Assert.IsTrue(map.InnerDomainMap.ContainsKey(typeof(ChildWithCustomDomain)));
      Assert.AreEqual(map.InnerDomainMap[typeof(ChildWithCustomDomain)], customChildRegion);

      // The custom parent and child domains should be the same
      Assert.AreEqual(customParentRegion, customChildRegion);

      // Get the region for the child class with the overridden domain and verify it was cached
      string overriddenChildRegion = map.GetRegion(typeof(ChildWithOverriddenDomain));
      Assert.IsTrue(map.InnerDomainMap.ContainsKey(typeof(ChildWithOverriddenDomain)));
      Assert.AreEqual(map.InnerDomainMap[typeof(ChildWithOverriddenDomain)], overriddenChildRegion);

      // The custom parent and the overridden child should be different
      Assert.AreNotEqual(customParentRegion, overriddenChildRegion);

      // Get the region for the child class that inherits the overridden domain and verify it was cached
      string overriddenGrandchildRegion = map.GetRegion(typeof(GrandchildOfOverriddenDomain));
      Assert.IsTrue(map.InnerDomainMap.ContainsKey(typeof(GrandchildOfOverriddenDomain)));
      Assert.AreEqual(map.InnerDomainMap[typeof(GrandchildOfOverriddenDomain)], overriddenGrandchildRegion);

      // The overridden child and the grandchild should be the same
      Assert.AreEqual(overriddenChildRegion, overriddenGrandchildRegion);
    }
    #endregion

    #region Helper Classes
    // A parent class using the default domain
    private class ParentWithDefaultDomain
    {
    }

    // A child of the class with the default domain -- should have its own domain
    private class ChildWithDefaultDomain : ParentWithDefaultDomain
    {
    }

    // A parent class with a custom domain
    [EveCacheDomain(typeof(ParentWithCustomDomain))]
    private class ParentWithCustomDomain
    {
    }

    // A child of the parent class with the custom domain -- should inherit the parent domain
    private class ChildWithCustomDomain : ParentWithCustomDomain
    {
    }

    // A child class which defines its own custom domain
    [EveCacheDomain(typeof(object))]
    private class ChildWithOverriddenDomain : ParentWithCustomDomain
    {
    }

    // A grandchild class -- should inherit its direct parent's domain.
    private class GrandchildOfOverriddenDomain : ChildWithOverriddenDomain
    {
    }
    #endregion
  }
}