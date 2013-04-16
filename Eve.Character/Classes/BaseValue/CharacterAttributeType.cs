//-----------------------------------------------------------------------
// <copyright file="CharacterAttributeType.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about a type of attribute belonging to an EVE
  /// character.
  /// </summary>
  public sealed partial class CharacterAttributeType 
    : BaseValue<CharacterAttributeId, CharacterAttributeId, CharacterAttributeTypeEntity, CharacterAttributeType>,
      IHasIcon
  {
    private Icon icon;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CharacterAttributeType class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CharacterAttributeType(IEveRepository container, CharacterAttributeTypeEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The <see cref="Icon" /> associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public Icon Icon
    {
      get
      {
        Contract.Ensures(Contract.Result<Icon>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.icon ?? (this.icon = this.Container.GetOrAdd<Icon>(this.IconId, () => this.Entity.Icon.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId IconId
    {
      get { return Entity.IconId; }
    }

    /// <summary>
    /// Gets any additional notes about the item.
    /// </summary>
    /// <value>
    /// A string containing additional notes about the item.
    /// </value>
    public string Notes
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.ShortDescription ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the short description of the item.
    /// </summary>
    /// <value>
    /// A string containing the short description of the item.
    /// </value>
    public string ShortDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.ShortDescription ?? string.Empty;
      }
    }

    /* Methods */

    /// <summary>
    /// For an <see cref="AttributeId" /> value, returns the corresponding
    /// <see cref="CharacterAttributeId" /> value, if any.
    /// </summary>
    /// <param name="attributeId">
    /// The ID of the EVE attribute to convert to a character attribute.
    /// </param>
    /// <returns>
    /// The <see cref="CharacterAttributeId" /> which corresponds to the value
    /// specified in <paramref name="attributeId" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if <paramref name="attributeId" /> does not correspond to a
    /// character attribute ID.
    /// </exception>
    public static CharacterAttributeId AttributeToCharacterAttribute(AttributeId attributeId)
    {
      switch (attributeId)
      {
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
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class CharacterAttributeType : IHasIcon
  {
    IconId? IHasIcon.IconId
    {
      get { return IconId; }
    }
  }
  #endregion
}