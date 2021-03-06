﻿//-----------------------------------------------------------------------
// <copyright file="IHasIcon.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  /// <summary>
  /// The base interface for classes which are associated with an icon.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Classes that implement this interface simply have a data field supporting
  /// an icon.  It doesn't mean the field necessarily has a value; the field
  /// could be null.
  /// </para>
  /// </remarks>
  public interface IHasIcon
  {
    /* Properties */

    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    Icon Icon { get; }

    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    IconId? IconId { get; }
  }
}