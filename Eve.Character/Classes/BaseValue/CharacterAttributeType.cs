//-----------------------------------------------------------------------
// <copyright file="CharacterAttributeType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about a type of attribute belonging to an EVE
  /// character.
  /// </summary>
  public class CharacterAttributeType : BaseValue<CharacterAttributeId, CharacterAttributeId, CharacterAttributeTypeEntity, CharacterAttributeType>,
                                        IHasIcon {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// For an <see cref="AttributeId" /> value, returns the corresponding
    /// <see cref="CharacterAttributeId" /> value, if any.
    /// </summary>
    /// 
    /// <param name="attributeId">
    /// The ID of the EVE attribute to convert to a character attribute.
    /// </param>
    /// 
    /// <returns>
    /// The <see cref="CharacterAttributeId" /> which corresponds to the value
    /// specified in <paramref name="attributeId" />.
    /// </returns>
    /// 
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="attributeId" /> does not correspond to a
    /// character attribute ID.
    /// </exception>
    public static CharacterAttributeId AttributeToCharacterAttribute(AttributeId attributeId) {
      switch (attributeId) {
        case AttributeId.Charisma:
          return CharacterAttributeId.Charisma;

        case AttributeId.Intelligence: 
          return CharacterAttributeId.Intelligence;

        case AttributeId.Memory:
          return CharacterAttributeId.Memory;

        case AttributeId.Perception:
          return CharacterAttributeId.Perception;

        case AttributeId.Willpower:
          return CharacterAttributeId.Willpower;

        default:
          throw new InvalidOperationException("The specified EVE attribute does not correspond to a character attribute.");
      }
    }
    #endregion

    #region Instance Fields
    private Icon _icon;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the CharacterAttributeType class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal CharacterAttributeType(CharacterAttributeTypeEntity entity) : base(entity) {
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
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon {
      get {
        Contract.Ensures(Contract.Result<Icon>() != null);

        if (_icon == null) {

          // Load the cached version if available
          _icon = Eve.General.Cache.GetOrAdd<Icon>(IconId, () => {
            IconEntity iconEntity = Entity.Icon;
            Contract.Assume(iconEntity != null);

            return new Icon(iconEntity);
          });
        }

        return _icon;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId IconId {
      get {
        return Entity.IconId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets any additional notes about the item.
    /// </summary>
    /// 
    /// <value>
    /// A string containing additional notes about the item.
    /// </value>
    public string Notes {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        var result = Entity.ShortDescription ?? string.Empty;
        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the short description of the item.
    /// </summary>
    /// 
    /// <value>
    /// A string containing the short description of the item.
    /// </value>
    public string ShortDescription {
      get {
        Contract.Ensures(Contract.Result<string>() != null);

        var result = Entity.ShortDescription ?? string.Empty;
        return result;
      }
    }
    #endregion

    #region IHasIcon Members
    //******************************************************************************
    IconId? IHasIcon.IconId {
      get { return IconId; }
    }
    #endregion
  }
}