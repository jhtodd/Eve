//-----------------------------------------------------------------------
// <copyright file="NpcCorporationExtent.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.ComponentModel;

  /// <summary>
  /// Describes the extent of an NPC corporation.
  /// </summary>
  public enum NpcCorporationExtent : byte
  {
    /// <summary>
    /// The corporation is limited to a small local area.
    /// </summary>
    [Description("Local")]
    Local,

    /// <summary>
    /// The corporation is limited to a constellation.
    /// </summary>
    [Description("Constellation")]
    Constellation,

    /// <summary>
    /// The corporation is limited to a region.
    /// </summary>
    [Description("Regional")]
    Regional,

    /// <summary>
    /// The corporation is limited to a single nation.
    /// </summary>
    [Description("National")]
    National,

    /// <summary>
    /// The corporation is global in extent.
    /// </summary>
    [Description("Global")]
    Global
  }
}