//-----------------------------------------------------------------------
// <copyright file="Agent.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Linq;

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
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Agent(IEveRepository container, AgentEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the type of the agent.
    /// </summary>
    /// <value>
    /// The type of the agent.
    /// </value>
    public AgentType AgentType
    {
      get
      {
        Contract.Ensures(Contract.Result<AgentType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.agentType ?? (this.agentType = this.Container.GetOrAdd<AgentType>(this.AgentTypeId, () => this.Entity.AgentType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of the agent.
    /// </summary>
    /// <value>
    /// The ID of the type of the agent.
    /// </value>
    public AgentTypeId AgentTypeId
    {
      get { return this.Entity.AgentTypeId; }
    }

    /// <summary>
    /// Gets the corporation to which the agent belongs.
    /// </summary>
    /// <value>
    /// The corporation to which the agent belongs.
    /// </value>
    public NpcCorporation Corporation
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.corporation ?? (this.corporation = this.Container.GetOrAdd<NpcCorporation>(this.CorporationId, () => this.Entity.Corporation.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the corporation to which the agent belongs.
    /// </summary>
    /// <value>
    /// The ID of the corporation to which the agent belongs.
    /// </value>
    public NpcCorporationId CorporationId
    {
      get { return (NpcCorporationId)this.Entity.CorporationId; }
    }

    /// <summary>
    /// Gets the corporate division to which the agent belongs.
    /// </summary>
    /// <value>
    /// The corporate division to which the agent belongs.
    /// </value>
    public Division Division
    {
      get
      {
        Contract.Ensures(Contract.Result<Division>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.division ?? (this.division = this.Container.GetOrAdd<Division>(this.DivisionId, () => this.Entity.Division.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the corporate division to which the agent belongs.
    /// </summary>
    /// <value>
    /// The ID of the corporate division to which the agent belongs.
    /// </value>
    public DivisionId DivisionId
    {
      get { return this.Entity.DivisionId; }
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
      get { return this.Entity.IsLocator; }
    }

    /// <summary>
    /// Gets the level of the agent.
    /// </summary>
    /// <value>
    /// The level of the agent.
    /// </value>
    public byte Level
    {
      get { return this.Entity.Level; }
    }

    /// <summary>
    /// Gets the item where the agent is located.
    /// </summary>
    /// <value>
    /// The item where the agent is located.
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
        Contract.Ensures(Contract.Result<Item>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.location ?? (this.location = this.Container.GetOrAdd<Item>(this.LocationId, () => this.Entity.AgentLocation.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the item where the agent is located.
    /// </summary>
    /// <value>
    /// The ID of the item where the agent is located.
    /// </value>
    public new ItemId LocationId
    {
      get { return this.Entity.AgentLocationId; }
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
      get { return this.Entity.Quality; }
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

        if (this.researchFields == null)
        {
          if (this.Entity.ResearchFields == null)
          {
            this.researchFields = new ReadOnlySkillTypeCollection(null);
          }
          else
          {
            // Filter through the cache
            this.researchFields = new ReadOnlySkillTypeCollection(
              this.Entity.ResearchFields.Select(x => this.Container.GetOrAdd<SkillType>(x.Id, () => (SkillType)x.ToAdapter(this.Container)))
                                        .OrderBy(x => x));
          }
        }

        return this.researchFields;
      }
    }

    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    private new AgentEntity Entity
    {
      get
      {
        Contract.Ensures(Contract.Result<AgentEntity>() != null);

        return (AgentEntity)base.Entity;
      }
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