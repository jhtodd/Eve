//-----------------------------------------------------------------------
// <copyright file="NpcCorporationSize.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Data.Entity;

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