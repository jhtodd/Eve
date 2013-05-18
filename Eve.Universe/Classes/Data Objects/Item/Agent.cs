//-----------------------------------------------------------------------
// <copyright file="Agent.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Threading;

  using Eve.Character;
  using Eve.Data;
  using Eve.Data.Entities;

  /// <summary>
  /// An agent belonging to an NPC corporation.
  /// </summary>
  public sealed partial class Agent 
    : Item,
      IComparable<Agent>,
      IDisposable
  {
    private AgentType agentType;
    private NpcCorporation corporation;
    private Division division;
    private Item location;
    private ReadOnlySkillTypeCollection researchFields;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Agent class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Agent(IEveRepository repository, ItemEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
      Contract.Requires(entity.IsAgent, "The entity must be an agent.");
    }

    /* Properties */

    /// <summary>
    /// Gets the type of the agent.
    /// </summary>
    /// <value>
    /// The type of the agent, or <see langword="null" /> if no agent
    /// type information exists.
    /// </value>
    public AgentType AgentType
    {
      get
      {
        Contract.Ensures(this.AgentTypeId == null || Contract.Result<AgentType>() != null);

        if (this.AgentTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.agentType, this.AgentInfo.AgentTypeId, () => this.AgentInfo.AgentType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of the agent.
    /// </summary>
    /// <value>
    /// The ID of the type of the agent, or <see langword="null" />
    /// if no agent type information exists.
    /// </value>
    public AgentTypeId? AgentTypeId
    {
      get 
      {
        Contract.Ensures(this.AgentInfo != null || Contract.Result<AgentTypeId?>() == null);
        return this.AgentInfo == null ? (AgentTypeId?)null : (AgentTypeId?)this.AgentInfo.AgentTypeId; 
      }
    }

    /// <summary>
    /// Gets the corporation to which the agent belongs.
    /// </summary>
    /// <value>
    /// The corporation to which the agent belongs, or <see langword="null"/>
    /// if no corporation information exists.
    /// </value>
    public NpcCorporation Corporation
    {
      get
      {
        Contract.Ensures(this.AgentInfo == null || Contract.Result<NpcCorporation>() != null);

        if (this.AgentInfo == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.corporation, this.AgentInfo.CorporationId, () => this.AgentInfo.Corporation);
      }
    }

    /// <summary>
    /// Gets the ID of the corporation to which the agent belongs.
    /// </summary>
    /// <value>
    /// The ID of the corporation to which the agent belongs, or
    /// <see langword="null"/> if no corporation information exists..
    /// </value>
    public NpcCorporationId? CorporationId
    {
      get
      {
        return this.AgentInfo == null ? (NpcCorporationId?)null : (NpcCorporationId?)this.AgentInfo.CorporationId;
      }
    }

    /// <summary>
    /// Gets the corporate division to which the agent belongs.
    /// </summary>
    /// <value>
    /// The corporate division to which the agent belongs, or
    /// <see langword="null"/> if no division information exists.
    /// </value>
    public Division Division
    {
      get
      {
        Contract.Ensures(this.DivisionId == null || Contract.Result<Division>() != null);

        if (this.DivisionId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.division, this.AgentInfo.DivisionId, () => this.AgentInfo.Division);
      }
    }

    /// <summary>
    /// Gets the ID of the corporate division to which the agent belongs.
    /// </summary>
    /// <value>
    /// The ID of the corporate division to which the agent belongs, or
    /// <see langword="null"/> if no division information exists.
    /// </value>
    public DivisionId? DivisionId
    {
      get
      {
        Contract.Ensures(this.AgentInfo != null || Contract.Result<DivisionId?>() == null);
        return this.AgentInfo == null ? (DivisionId?)null : (DivisionId?)this.AgentInfo.DivisionId;
      }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new AgentId Id
    {
      get { return (AgentId)base.Id.Value; }
    }

    /// <summary>
    /// Gets a value indicating whether the agent offers locator services.
    /// </summary>
    /// <value>
    /// <see langword="true" /> if the agent offers locator services; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool IsLocator
    {
      get
      {
        return this.AgentInfo == null ? false : this.AgentInfo.IsLocator;
      }
    }

    /// <summary>
    /// Gets the level of the agent.
    /// </summary>
    /// <value>
    /// The level of the agent.
    /// </value>
    public byte Level
    {
      get
      {
        return this.AgentInfo == null ? (byte)1 : this.AgentInfo.Level;
      }
    }

    /// <summary>
    /// Gets the item where the agent is located.
    /// </summary>
    /// <value>
    /// The item where the agent is located, or
    /// <see langword="null"/> if no location information exists.
    /// </value>
    /// <remarks>
    /// <para>
    /// This is usually a <see cref="Station" /> but is sometimes a
    /// <see cref="SolarSystem" /> for COSMOS agents and other agents in space.
    /// </para>
    /// <para>
    /// The property obscures the base class (<c>invItems</c>) location property.
    /// The base class property contains incorrect locations for COSMOS agents
    /// (i.e. the station they were located at before being moved to COSMOS space).
    /// </para>
    /// </remarks>
    public new Item Location
    {
      get
      {
        Contract.Ensures(this.LocationId == null || Contract.Result<Item>() != null);

        if (this.LocationId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.location, this.AgentInfo.LocationId, () => this.AgentInfo.Location);
      }
    }

    /// <summary>
    /// Gets the ID of the item where the agent is located.
    /// </summary>
    /// <value>
    /// The ID of the item where the agent is located, or
    /// <see langword="null"/> if no location information exists.
    /// </value>
    public new ItemId? LocationId
    {
      get
      {
        Contract.Ensures(this.AgentInfo != null || Contract.Result<ItemId?>() == null);
        return this.AgentInfo == null ? (ItemId?)null : this.AgentInfo.LocationId;
      }
    }

    /// <summary>
    /// Gets the quality of the agent.
    /// </summary>
    /// <value>
    /// The quality of the agent.  This value is no longer used and is always
    /// equal to 20.
    /// </value>
    public short Quality
    {
      get 
      { 
        return this.AgentInfo == null ? (short)0 : this.AgentInfo.Quality;
      }
    }

    /// <summary>
    /// Gets the collection of research fields offered by the agent.
    /// </summary>
    /// <value>
    /// The collection of research fields offered by the agent.
    /// </value>
    public ReadOnlySkillTypeCollection ResearchFields
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlySkillTypeCollection>() != null);

        return Agent.LazyInitialize(
          ref this.researchFields,
          () => ReadOnlySkillTypeCollection.Create(this.Repository, this.AgentInfo == null ? null : this.AgentInfo.ResearchFields));
      }
    }

    /// <summary>
    /// Gets the <see cref="AgentEntity" /> containing the specific
    /// information for the current item type.
    /// </summary>
    /// <value>
    /// The <see cref="AgentEntity" /> containing the specific
    /// information for the current item type.
    /// </value>
    private AgentEntity AgentInfo
    {
      get { return this.Entity.AgentInfo; }
    }

    /* Methods */
    
    /// <inheritdoc />
    public override int CompareTo(Item other)
    {
      Agent otherAgent = other as Agent;

      if (otherAgent != null)
      {
        return this.CompareTo(otherAgent);
      }

      return base.CompareTo(other);
    }

    /// <inheritdoc />
    public int CompareTo(Agent other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Level.CompareTo(other.Level);

      if (result == 0)
      {
        result = this.Quality.CompareTo(other.Quality);
      }

      if (result == 0)
      {
        result = this.Name.CompareTo(other.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Name + " (L" + this.Level.ToString() + " Q" + this.Quality.ToString() + ")";
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    /// Disposes the current instance.
    /// </summary>
    /// <param name="disposing">Specifies whether to dispose managed resources.</param>
    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.researchFields != null)
        {
          this.researchFields.Dispose();
        }
      }
    }
  }
}