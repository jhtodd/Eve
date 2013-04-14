//-----------------------------------------------------------------------
// <copyright file="UnitTests.cs" company="Jeremy H. Todd">
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
  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains test functions for the <see cref="Unit" /> class.
  /// </summary>
  [TestFixture()]
  public class UnitTests {

    #region Test Methods
    //******************************************************************************
    /// <summary>
    /// Test method for the <see cref="BaseValueCache.Clean" /> method.
    /// </summary>
    [Test()]
    public void TestFormatValue() {

      EveRepository repository = new EveRepository();
      UnitEntity unitEntity;
      Unit unit;

      // Test the unit formats that require special processing

      // Absolute percent
      unitEntity = new UnitEntity { Id = UnitId.AbsolutePercent, Name = "Absolute Percent", Description = string.Empty, DisplayName = string.Empty };
      unit = new Unit(repository, unitEntity);
      Assert.AreEqual(unit.FormatValue(1), "100%");
      Assert.AreEqual(unit.FormatValue(0), "0%");
      Assert.AreEqual(unit.FormatValue(0.5), "50%");
      Assert.AreEqual(unit.FormatValue(0.55), "55%");
      Assert.AreEqual(unit.FormatValue(-0.55), "-55%");

      // Boolean
      unitEntity = new UnitEntity { Id = UnitId.Boolean, Name = "Boolean", Description = string.Empty, DisplayName = string.Empty };
      unit = new Unit(repository, unitEntity);
      Assert.AreEqual(unit.FormatValue(1), "True");
      Assert.AreEqual(unit.FormatValue(0.1D), "True");
      Assert.AreEqual(unit.FormatValue(-243), "True");
      Assert.AreEqual(unit.FormatValue(0), "False");
      Assert.AreEqual(unit.FormatValue(0.0D), "False");

      // Inverse absolute percent
      unitEntity = new UnitEntity { Id = UnitId.InverseAbsolutePercent, Name = "Inverse Absolute Percent", Description = string.Empty, DisplayName = string.Empty };
      unit = new Unit(repository, unitEntity);
      Assert.AreEqual(unit.FormatValue(1), "0%");
      Assert.AreEqual(unit.FormatValue(0), "100%");
      Assert.AreEqual(unit.FormatValue(0.5), "50%");
      Assert.AreEqual(unit.FormatValue(0.55), "45%");
      Assert.AreEqual(unit.FormatValue(-0.55), "155%");

      // Inversed modifier percent
      unitEntity = new UnitEntity { Id = UnitId.InversedModifierPercent, Name = "Inversed Modifier Percent", Description = string.Empty, DisplayName = string.Empty };
      unit = new Unit(repository, unitEntity);
      Assert.AreEqual(unit.FormatValue(1), "0%");
      Assert.AreEqual(unit.FormatValue(0), "100%");
      Assert.AreEqual(unit.FormatValue(0.1), "90%");
      Assert.AreEqual(unit.FormatValue(0.9), "10%");

      // Modifier percent
      unitEntity = new UnitEntity { Id = UnitId.ModifierPercent, Name = "Modifier Percent", Description = string.Empty, DisplayName = string.Empty };
      unit = new Unit(repository, unitEntity);
      Assert.AreEqual(unit.FormatValue(1), "+0%");
      Assert.AreEqual(unit.FormatValue(0), "-100%");
      Assert.AreEqual(unit.FormatValue(1.1), "+10%");
      Assert.AreEqual(unit.FormatValue(0.1), "-90%");
      Assert.AreEqual(unit.FormatValue(0.9), "-10%");

      // Sex
      unitEntity = new UnitEntity { Id = UnitId.Sex, Name = "Sex", Description = string.Empty, DisplayName = string.Empty };
      unit = new Unit(repository, unitEntity);
      Assert.AreEqual(unit.FormatValue(1), "Male");
      Assert.AreEqual(unit.FormatValue(1.0D), "Male");
      Assert.AreEqual(unit.FormatValue(0.9999D), "Male");
      Assert.AreEqual(unit.FormatValue(1.1D), "Male");
      Assert.AreEqual(unit.FormatValue(2), "Unisex");
      Assert.AreEqual(unit.FormatValue(2.0D), "Unisex");
      Assert.AreEqual(unit.FormatValue(1.9999D), "Unisex");
      Assert.AreEqual(unit.FormatValue(2.1D), "Unisex");
      Assert.AreEqual(unit.FormatValue(3), "Female");
      Assert.AreEqual(unit.FormatValue(3.0D), "Female");
      Assert.AreEqual(unit.FormatValue(2.9999D), "Female");
      Assert.AreEqual(unit.FormatValue(3.1D), "Female");
      Assert.AreEqual(unit.FormatValue(0), "Unknown");
      Assert.AreEqual(unit.FormatValue(4), "Unknown");

      // Sizeclass
      unitEntity = new UnitEntity { Id = UnitId.Sizeclass, Name = "Sizeclass", Description = string.Empty, DisplayName = string.Empty };
      unit = new Unit(repository, unitEntity);
      Assert.AreEqual(unit.FormatValue(1), "Small");
      Assert.AreEqual(unit.FormatValue(1.0D), "Small");
      Assert.AreEqual(unit.FormatValue(0.9999D), "Small");
      Assert.AreEqual(unit.FormatValue(1.1D), "Small");
      Assert.AreEqual(unit.FormatValue(2), "Medium");
      Assert.AreEqual(unit.FormatValue(2.0D), "Medium");
      Assert.AreEqual(unit.FormatValue(1.9999D), "Medium");
      Assert.AreEqual(unit.FormatValue(2.1D), "Medium");
      Assert.AreEqual(unit.FormatValue(3), "Large");
      Assert.AreEqual(unit.FormatValue(3.0D), "Large");
      Assert.AreEqual(unit.FormatValue(2.9999D), "Large");
      Assert.AreEqual(unit.FormatValue(3.1D), "Large");
      Assert.AreEqual(unit.FormatValue(4), "X-Large");
      Assert.AreEqual(unit.FormatValue(4.0D), "X-Large");
      Assert.AreEqual(unit.FormatValue(3.9999D), "X-Large");
      Assert.AreEqual(unit.FormatValue(4.1D), "X-Large");
      Assert.AreEqual(unit.FormatValue(0), "Unknown");
      Assert.AreEqual(unit.FormatValue(5), "Unknown");

    }
    #endregion
  }
}