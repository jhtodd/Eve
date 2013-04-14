//-----------------------------------------------------------------------
// <copyright file="Icon.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contains information about an icon associated with an EVE item.
  /// </summary>
  public sealed partial class Icon
    : BaseValue<IconId, int, IconEntity, Icon>,
      IHasIcon
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Icon class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Icon(IEveRepository container, IconEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the filename of the icon.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that provides the filename of the item.
    /// </value>
    public string IconFile
    {
      get
      {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        return this.Name;
      }
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class Icon : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this; }
    }

    IconId? IHasIcon.IconId
    {
      get { return Id; }
    }
  }
  #endregion
}