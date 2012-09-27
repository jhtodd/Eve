//-----------------------------------------------------------------------
// <copyright file="Icon.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about an icon associated with an EVE item.
  /// </summary>
  public class Icon : BaseValue<IconId, int, IconEntity, Icon>,
                      IHasIcon {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Icon class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    public Icon(IconEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the filename of the icon.
    /// </summary>
    /// 
    /// <value>
    /// A <see cref="string" /> that provides the filename of the item.
    /// </value>
    public string IconFile {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));
        return Name;
      }
    }
    #endregion

    #region IHasIcon Members
    //******************************************************************************
    Icon IHasIcon.Icon {
      get { return this; }
    }
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get { return Id; }
    }
    #endregion
  }
}