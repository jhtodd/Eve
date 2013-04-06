//-----------------------------------------------------------------------
// <copyright file="AttributeModifiers.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  /// <summary>
  /// The modifiers which can affect the adjusted value of an EVE attribute.
  /// </summary>
  [Flags]
  public enum AttributeModifiers
  {
    /// <summary>
    /// No modifiers should be applied and the base value should be returned.
    /// </summary>
    [Description("None")]
    None = 0x0,

    /// <summary>
    /// Adjustments from attributes of the parent item should be applied to
    /// the returned value.
    /// </summary>
    [Description("Include Parent Attributes")]
    IncludeParentAttributes = 0x1,

    /// <summary>
    /// Adjustments from attributes of any child items should be applied to
    /// the returned value.
    /// </summary>
    [Description("Include Child Attributes")]
    IncludeChildAttributes = 0x2,

    /// <summary>
    /// All adjustments should be applied.
    /// </summary>
    [Description("All")]
    All = IncludeChildAttributes | IncludeParentAttributes
  }
}