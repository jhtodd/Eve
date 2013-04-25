//-----------------------------------------------------------------------
// <copyright file="AttributeModifiers.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.ComponentModel;

  using FreeNet;

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
    [LongDescription("No modifiers should be applied and the base value should be returned.")]
    None = 0x0,

    /// <summary>
    /// Adjustments from attributes of the parent item should be applied to
    /// the returned value.
    /// </summary>
    [Description("Include Parent Attributes")]
    [LongDescription("Adjustments from attributes of the parent item should be applied to the returned value.")]
    IncludeParentAttributes = 0x1,

    /// <summary>
    /// Adjustments from attributes of any child items should be applied to
    /// the returned value.
    /// </summary>
    [Description("Include Child Attributes")]
    [LongDescription("Adjustments from attributes of any child items should be applied to the returned value.")]
    IncludeChildAttributes = 0x2,

    /// <summary>
    /// All adjustments should be applied.
    /// </summary>
    [Description("All")]
    [LongDescription("All adjustments should be applied.")]
    All = IncludeChildAttributes | IncludeParentAttributes
  }
}