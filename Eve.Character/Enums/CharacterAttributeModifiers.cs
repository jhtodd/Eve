//-----------------------------------------------------------------------
// <copyright file="CharacterAttributeModifiers.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.ComponentModel;

  using FreeNet;

  /// <summary>
  /// The modifiers which can affect the adjusted value of an attribute
  /// belonging to an EVE character.
  /// </summary>
  [Flags]
  public enum CharacterAttributeModifiers
  {
    /// <summary>
    /// No modifiers should be applied and the base value should be returned.
    /// </summary>
    [Description("None")]
    [LongDescription("No modifiers should be applied and the base value should be returned.")]
    None = 0x0,

    /// <summary>
    /// Adjustments from any implants installed by the character should be applied to
    /// the returned value.
    /// </summary>
    [Description("Implants")]
    [LongDescription("Adjustments from any implants installed by the character should be applied to the returned value.")]
    Implants = 0x1,

    /// <summary>
    /// All adjustments should be applied.
    /// </summary>
    [Description("All")]
    [LongDescription("All adjustments should be applied.")]
    All = Implants
  }
}