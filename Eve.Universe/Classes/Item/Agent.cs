//-----------------------------------------------------------------------
// <copyright file="Agent.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections.ObjectModel;

  using Eve.Character;
  using Eve.Data.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// An agent belonging to an NPC corporation.
  /// </summary>
  public class Agent : Item,
                       IComparable<Agent> {

    #region Instance Fields
    private AgentType _agentType;
    private NpcCorporation _corporation;
    private Division _division;
    private Item _location;
    private ReadOnlySkillTypeCollection _researchFields;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Agent class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Agent(AgentEntity entity) : base(entity) {
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
    /// Gets the type of the agent.
    /// </summary>
    /// 
    /// <value>
    /// The type of the agent.
    /// </value>
    public AgentType AgentType {
      get {
        Contract.Ensures(Contract.Result<AgentType>() != null);

        if (_agentType == null) {

          // Load the cached version if available
          _agentType = Eve.General.Cache.GetOrAdd<AgentType>(AgentTypeId, () => {
            AgentTypeEntity typeEntity = Entity.AgentType;
            Contract.Assume(typeEntity != null);

            return new AgentType(typeEntity);
          });
        }

        return _agentType;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the type of the agent.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the type of the agent.
    /// </value>
    public AgentTypeId AgentTypeId {
      get {
        return Entity.AgentTypeId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the corporation to which the agent belongs.
    /// </summary>
    /// 
    /// <value>
    /// The corporation to which the agent belongs.
    /// </value>
    public NpcCorporation Corporation {
      get {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        if (_corporation == null) {

          // Load the cached version if available
          _corporation = Eve.General.Cache.GetOrAdd<NpcCorporation>(CorporationId, () => {
            NpcCorporationEntity corporationEntity = Entity.Corporation;
            Contract.Assume(corporationEntity != null);

            return new NpcCorporation(corporationEntity);
          });
        }

        return _corporation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the corporation to which the agent belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporation to which the agent belongs.
    /// </value>
    public NpcCorporationId CorporationId {
      get {
        return (NpcCorporationId) Entity.CorporationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the corporate division to which the agent belongs.
    /// </summary>
    /// 
    /// <value>
    /// The corporate division to which the agent belongs.
    /// </value>
    public Division Division {
      get {
        Contract.Ensures(Contract.Result<Division>() != null);

        if (_division == null) {

          // Load the cached version if available
          _division = Eve.General.Cache.GetOrAdd<Division>(DivisionId, () => {
            DivisionEntity divisionEntity = Entity.Division;
            Contract.Assume(divisionEntity != null);

            return new Division(divisionEntity);
          });
        }

        return _division;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the corporate division to which the agent belongs.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporate division to which the agent belongs.
    /// </value>
    public DivisionId DivisionId {
      get {
        return Entity.DivisionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// 
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public new AgentId Id {
      get {
        return (AgentId) base.Id.Value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets a value indicating whether the agent offers locator services.
    /// </summary>
    /// 
    /// <value>
    /// <see langword="true" /> if the agent offers locator servies; otherwise
    /// <see langword="false" />.
    /// </value>
    public bool IsLocator {
      get {
        return Entity.IsLocator;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the level of the agent.
    /// </summary>
    /// 
    /// <value>
    /// The level of the agent.
    /// </value>
    public byte Level {
      get {
        return Entity.Level;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the item where the agent is located.
    /// </summary>
    /// 
    /// <value>
    /// The item where the agent is located.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// This is usually a <see cref="Station" /> but is sometimes a
    /// <see cref="SolarSystem" /> for COSMOS agents and other agents in space.
    /// </para>
    /// 
    /// <para>
    /// The property obscures the base class (<c>invItems</c>) location property.
    /// The base class property contains incorrect locations for COSMOS agents
    /// (i.e. the station they were located at before being moved to COSMOS space).
    /// </para>
    /// </remarks>
    public new Item Location {
      get {
        Contract.Ensures(Contract.Result<Item>() != null);

        if (_location == null) {

          // Load the cached version if available
          _location = Eve.General.Cache.GetOrAdd<Item>(LocationId, () => {
            ItemEntity itemEntity = Entity.AgentLocation;
            Contract.Assume(itemEntity != null);

            return Item.Create(itemEntity);
          });
        }

        return _location;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the item where the agent is located.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the item where the agent is located.
    /// </value>
    public new ItemId LocationId {
      get {
        return Entity.AgentLocationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the quality of the agent.
    /// </summary>
    /// 
    /// <value>
    /// The quality of the agent.  This value is no longer used and is always
    /// equal to 20.
    /// </value>
    public short Quality {
      get {
        return Entity.Quality;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the collection of research fields offered by the agent.
    /// </summary>
    /// 
    /// <value>
    /// The collection of research fields offered by the agent.
    /// </value>
    public ReadOnlySkillTypeCollection ResearchFields {
      get {
        Contract.Ensures(Contract.Result<ReadOnlySkillTypeCollection>() != null);

        if (_researchFields == null) {

          if (Entity.ResearchFields == null) {
            _researchFields = new ReadOnlySkillTypeCollection(null);

          } else {

            // Filter through the cache
            _researchFields = new ReadOnlySkillTypeCollection(
              Entity.ResearchFields.Select(x => Eve.General.Cache.GetOrAdd<SkillType>(x.Id, () => (SkillType) EveType.Create(x)))
                                   .OrderBy(x => x)
            );
          }
        }

        return _researchFields;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public override int CompareTo(Item other) {
      Agent otherAgent = other as Agent;

      if (otherAgent != null) {
        return CompareTo(otherAgent);
      }

      return base.CompareTo(other);
    }
    //******************************************************************************
    /// <inheritdoc />
    public int CompareTo(Agent other) {
      if (other == null) {
        return 1;
      }

      int result = Level.CompareTo(other.Level);

      if (result == 0) {
        result = Quality.CompareTo(other.Quality);
      }

      if (result == 0) {
        result = Name.CompareTo(other.Name);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return Name + " (L" + Level.ToString() + " Q" + Quality.ToString() + ")";
    }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the data entity that forms the basis of the adapter.
    /// </summary>
    /// 
    /// <value>
    /// The data entity that forms the basis of the adapter.
    /// </value>
    protected new AgentEntity Entity {
      get {
        Contract.Ensures(Contract.Result<AgentEntity>() != null);

        return (AgentEntity) base.Entity;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of agents.
  /// </summary>
  public class ReadOnlyAgentCollection : ReadOnlyCollection<Agent> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyAgentCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAgentCollection(IEnumerable<Agent> contents) : base() {
      if (contents != null) {
        foreach (Agent agent in contents) {
          Items.AddWithoutCallback(agent);
        }
      }
    }
    #endregion
  }
}