//-----------------------------------------------------------------------
// <copyright file="Icon.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// Contains information about an icon associated with an EVE item.
  /// </summary>>
  [Table("eveIcons")]
  public sealed class Icon : BaseValue<int, Icon>,
                             IHasIcon {

    // Check EveDbContext.OnModelCreating() for customization of this type's
    // data mappings.

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Icon class.  This overload is
    /// provided for compatibility with the Entity Framework and should not be
    /// used.
    /// </summary>
    [Obsolete("Provided for compatibility with the Entity Framework.", true)]
    public Icon() : base(0, DEFAULT_NAME, string.Empty) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Icon class.
    /// </summary>
    /// 
    /// <param name="id">
    /// The ID of the item.
    /// </param>
    /// 
    /// <param name="name">
    /// The name of the item.
    /// </param>
    /// 
    /// <param name="description">
    /// The description of the item.
    /// </param>
    public Icon(int id, string iconFile, string description) : base(id, iconFile, description) {
      Contract.Requires(iconFile != null, Resources.Messages.Icon_IconFileCannotBeNull);
      Contract.Requires(description != null, Resources.Messages.BaseValue_DescriptionCannotBeNull);
      Contract.Requires(!string.IsNullOrWhiteSpace(iconFile), Resources.Messages.BaseValue_NameCannotBeNullOrEmpty);
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
    [NotMapped()]
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
    int? IHasIcon.IconId {
      get { return Id; }
    }
    #endregion
  }
}