﻿//-----------------------------------------------------------------------
// <copyright file="IHasRaces.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  /// <summary>
  /// The base interface for classes which are associated with one or more
  /// EVE races.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Classes that implement this interface simply have a data field supporting
  /// race flags.  It doesn't mean the field necessarily has a value; the field
  /// could be null.
  /// </para>
  /// </remarks>
  public interface IHasRaces
  {
    /* Properties */

    /// <summary>
    /// Gets the ID(s) of the race(s) associated with the item, if any.
    /// </summary>
    /// <value>
    /// A combination of <see cref="RaceId" /> enumeration values indicating which
    /// races the current item is associated with, or <see langword="null" /> if the
    /// item is not associated with any races.
    /// </value>
    RaceId? RaceIds { get; }

    // TODO: Add collection for races
  }
}