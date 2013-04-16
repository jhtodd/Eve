//-----------------------------------------------------------------------
// <copyright file="NpcCorporationSize.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.ComponentModel;

  /// <summary>
  /// Describes the size of an NPC corporation.
  /// </summary>
  public enum NpcCorporationSize : byte
  {
    /// <summary>
    /// The corporation is tiny.
    /// </summary>
    [Description("Tiny")]
    Tiny,

    /// <summary>
    /// The corporation is small.
    /// </summary>
    [Description("Small")]
    Small,

    /// <summary>
    /// The corporation is medium-sized.
    /// </summary>
    [Description("Medium")]
    Medium,

    /// <summary>
    /// The corporation is large.
    /// </summary>
    [Description("Large")]
    Large,

    /// <summary>
    /// The corporation is huge.
    /// </summary>
    [Description("Huge")]
    Huge
  }
}