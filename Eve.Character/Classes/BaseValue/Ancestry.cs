//-----------------------------------------------------------------------
// <copyright file="Ancestry.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// Contains information about an EVE ancestry.
  /// </summary>
  public sealed class Ancestry 
    : BaseValue<AncestryId, AncestryId, AncestryEntity, Ancestry>,
      IHasIcon
  {
    private Icon icon;
    private Bloodline bloodline;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Ancestry class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Ancestry(IEveRepository repository, AncestryEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /// <summary>
    /// Gets the <see cref="Bloodline" /> to which the ancestry belongs.
    /// </summary>
    /// <value>
    /// The <see cref="Bloodline" /> to which the ancestry belongs.
    /// </value>
    public Bloodline Bloodline
    {
      get
      {
        Contract.Ensures(Contract.Result<Bloodline>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.bloodline, this.Entity.BloodlineId, () => this.Entity.Bloodline);
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Bloodline" /> the ancestry belongs to.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Bloodline" /> the ancestry belongs to.
    /// </value>
    public BloodlineId BloodlineId
    {
      get { return this.Entity.BloodlineId; }
    }

    /* Properties */

    /// <summary>
    /// Gets the modifier applied to the Charisma attribute for characters
    /// belonging to this ancestry.
    /// </summary>
    /// <value>
    /// The modifier applied to the Charisma attribute.
    /// </value>
    public byte Charisma
    {
      get { return this.Entity.Charisma; }
    }

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
        Contract.Ensures(this.IconId == null || Contract.Result<Icon>() != null);

        if (this.IconId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.icon, this.Entity.IconId, () => this.Entity.Icon);
      }
    }

    /// <summary>
    /// Gets the ID of the icon associated with the item, if any.
    /// </summary>
    /// <value>
    /// The ID of the icon associated with the item, or
    /// <see langword="null" /> if no such icon exists.
    /// </value>
    public IconId? IconId
    {
      get { return Entity.IconId; }
    }

    /// <summary>
    /// Gets the modifier applied to the Intelligence attribute for characters
    /// belonging to this ancestry.
    /// </summary>
    /// <value>
    /// The modifier applied to the Intelligence attribute.
    /// </value>
    public byte Intelligence
    {
      get { return this.Entity.Intelligence; }
    }

    /// <summary>
    /// Gets the modifier applied to the Memory attribute for characters
    /// belonging to this ancestry.
    /// </summary>
    /// <value>
    /// The modifier applied to the Memory attribute.
    /// </value>
    public byte Memory
    {
      get { return this.Entity.Memory; }
    }

    /// <summary>
    /// Gets the modifier applied to the Perception attribute for characters
    /// belonging to this ancestry.
    /// </summary>
    /// <value>
    /// The modifier applied to the Perception attribute.
    /// </value>
    public byte Perception
    {
      get { return this.Entity.Perception; }
    }

    /// <summary>
    /// Gets the short description of the ancestry.
    /// </summary>
    /// <value>
    /// A string containing the short description of the ancestry.
    /// </value>
    public string ShortDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return this.Entity.ShortDescription ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the modifier applied to the Willpower attribute for characters
    /// belonging to this ancestry.
    /// </summary>
    /// <value>
    /// The modifier applied to the Willpower attribute.
    /// </value>
    public byte Willpower
    {
      get { return this.Entity.Willpower; }
    }
  }
}