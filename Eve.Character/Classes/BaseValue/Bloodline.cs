//-----------------------------------------------------------------------
// <copyright file="Bloodline.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  /// <summary>
  /// Contains information about an EVE bloodline.
  /// </summary>
  public sealed class Bloodline 
    : BaseValue<BloodlineId, BloodlineId, BloodlineEntity, Bloodline>,
      IHasIcon
  {
    private ReadOnlyAncestryCollection ancestries;
    private NpcCorporation corporation;
    private Icon icon;
    private Race race;
    private EveType shipType; // TODO: Change to ShipType

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Bloodline class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Bloodline(IEveRepository repository, BloodlineEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of ancestries belonging to the bloodline.
    /// </summary>
    /// <value>
    /// The collection of ancestries belonging to the bloodline.
    /// </value>
    public ReadOnlyAncestryCollection Ancestries
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAncestryCollection>() != null);

        return Bloodline.LazyInitialize(
          ref this.ancestries,
          () => new ReadOnlyAncestryCollection(this.Repository, this.Entity.Ancestries));
      }
    }

    /// <summary>
    /// Gets the base value of the Charisma attribute for characters
    /// belonging to this bloodline.
    /// </summary>
    /// <value>
    /// The base value of the bloodline's Charisma attribute.
    /// </value>
    public byte Charisma
    {
      get { return this.Entity.Charisma; }
    }

    /// <summary>
    /// Gets the <see cref="NpcCorporation" /> that characters of this
    /// bloodline join after leaving a player corporation.
    /// </summary>
    /// <value>
    /// The <see cref="NpcCorporation" />  that characters of this
    /// bloodline join after leaving a player corporation.
    /// </value>
    public NpcCorporation Corporation
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.corporation, this.Entity.CorporationId, () => this.Entity.Corporation);
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="NpcCorporation" />  that characters
    /// of this bloodline join after leaving a player corporation.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="NpcCorporation" /> that characters of
    /// this bloodline join after leaving a player corporation.
    /// </value>
    public NpcCorporationId CorporationId
    {
      get { return this.Entity.CorporationId; }
    }

    /// <summary>
    /// Gets the description of females of the bloodline.
    /// </summary>
    /// <value>
    /// A string containing the description of females of the bloodline.
    /// </value>
    public string FemaleDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return this.Entity.FemaleDescription ?? string.Empty;
      }
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
    /// Gets the base value of the Intelligence attribute for characters
    /// belonging to this bloodline.
    /// </summary>
    /// <value>
    /// The base value of the bloodline's Intelligence attribute.
    /// </value>
    public byte Intelligence
    {
      get { return this.Entity.Intelligence; }
    }

    /// <summary>
    /// Gets the description of males of the bloodline.
    /// </summary>
    /// <value>
    /// A string containing the description of males of the bloodline.
    /// </value>
    public string MaleDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return this.Entity.MaleDescription ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the base value of the Memory attribute for characters
    /// belonging to this bloodline.
    /// </summary>
    /// <value>
    /// The base value of the bloodline's Memory attribute.
    /// </value>
    public byte Memory
    {
      get { return this.Entity.Memory; }
    }

    /// <summary>
    /// Gets the base value of the Perception attribute for characters
    /// belonging to this bloodline.
    /// </summary>
    /// <value>
    /// The base value of the bloodline's Perception attribute.
    /// </value>
    public byte Perception
    {
      get { return this.Entity.Perception; }
    }

    /// <summary>
    /// Gets the type of rookie ship characters of the bloodline begin with.
    /// </summary>
    /// <value>
    /// The type of rookie ship characters of the bloodline begin with.
    /// </value>
    public EveType ShipType // TODO: Change to ShipType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.shipType, this.Entity.ShipTypeId, () => this.Entity.ShipType);
      }
    }

    /// <summary>
    /// Gets the ID of type of rookie ship characters of the bloodline begin with.
    /// </summary>
    /// <value>
    /// The ID of type of rookie ship characters of the bloodline begin with.
    /// </value>
    public TypeId ShipTypeId
    {
      get { return this.Entity.ShipTypeId; }
    }

    /// <summary>
    /// Gets the <see cref="Race" /> to which the bloodline belongs.
    /// </summary>
    /// <value>
    /// The <see cref="Race" /> to which the bloodline belongs.
    /// </value>
    public Race Race
    {
      get
      {
        Contract.Ensures(Contract.Result<Race>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.race, this.Entity.RaceId, () => this.Entity.Race);
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Race" /> the bloodline belongs to.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Race" /> the bloodline belongs to.
    /// </value>
    public RaceId RaceId
    {
      get { return this.Entity.RaceId; }
    }

    /// <summary>
    /// Gets the short description of the bloodline.
    /// </summary>
    /// <value>
    /// A string containing the short description of the bloodline.
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
    /// Gets the short description of females of the bloodline.
    /// </summary>
    /// <value>
    /// A string containing the short description of females of the bloodline.
    /// </value>
    public string ShortFemaleDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return this.Entity.ShortFemaleDescription ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the short description of males of the bloodline.
    /// </summary>
    /// <value>
    /// A string containing the short description of males of the bloodline.
    /// </value>
    public string ShortMaleDescription
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return this.Entity.ShortMaleDescription ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the base value of the Willpower attribute for characters
    /// belonging to this bloodline.
    /// </summary>
    /// <value>
    /// The base value of the bloodline's Willpower attribute.
    /// </value>
    public byte Willpower
    {
      get { return this.Entity.Willpower; }
    }
  }
}