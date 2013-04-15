//-----------------------------------------------------------------------
// <copyright file="NpcCorporationDivision.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about a division of an NPC corporation.
  /// </summary>
  public sealed partial class NpcCorporationDivision
    : EveEntityAdapter<NpcCorporationDivisionEntity>,
      IComparable,
      IComparable<NpcCorporationDivision>,
      IEquatable<NpcCorporationDivision>,
      IEveCacheable,
      IKeyItem<DivisionId>
  {
    private ReadOnlyAgentCollection agents;
    private Division division;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="NpcCorporationDivision" /> class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal NpcCorporationDivision(IEveRepository container, NpcCorporationDivisionEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the collection of agents in the division.
    /// </summary>
    /// <value>
    /// The collection of agents in the division.
    /// </value>
    public ReadOnlyAgentCollection Agents
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyAgentCollection>() != null);

        return this.agents ?? (this.agents = new ReadOnlyAgentCollection(this.Container.GetAgents(x => x.CorporationId == this.CorporationId.Value && x.DivisionId == this.DivisionId).OrderBy(x => x)));
      }
    }

    /// <summary>
    /// Gets the ID of the corporation.
    /// </summary>
    /// <value>
    /// The ID of the corporation.
    /// </value>
    public NpcCorporationId CorporationId
    {
      get { return (NpcCorporationId)Entity.CorporationId; }
    }

    /// <summary>
    /// Gets the division.
    /// </summary>
    /// <value>
    /// The division.
    /// </value>
    public Division Division
    {
      get
      {
        Contract.Ensures(Contract.Result<Division>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.division ?? (this.division = this.Container.Load<Division>(this.DivisionId, () => this.Entity.Division.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the division.
    /// </summary>
    /// <value>
    /// The ID of the division.
    /// </value>
    public DivisionId DivisionId
    {
      get { return Entity.DivisionId; }
    }

    /// <summary>
    /// Gets the size of the division.
    /// </summary>
    /// <value>
    /// The size of the division.
    /// </value>
    public byte Size
    {
      get { return Entity.Size; }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="corporationId">
    /// The ID of the corporation.
    /// </param>
    /// <param name="divisionId">
    /// The ID of the division.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(NpcCorporationId corporationId, DivisionId divisionId)
    {
      return (long)((((ulong)(long)divisionId) << 32) | (ulong)(long)corporationId);
    }

    /// <inheritdoc />
    public int CompareTo(NpcCorporationDivision other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.Division.CompareTo(other.Division);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as NpcCorporationDivision);
    }

    /// <inheritdoc />
    public bool Equals(NpcCorporationDivision other)
    {
      if (other == null)
      {
        return false;
      }

      return this.CorporationId == other.CorporationId &&
             this.DivisionId == other.DivisionId &&
             this.Size == other.Size;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.CorporationId, this.DivisionId, this.Size);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Division.Name;
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class NpcCorporationDivision : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      NpcCorporationDivision other = obj as NpcCorporationDivision;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class NpcCorporationDivision : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return CreateCompoundId(this.CorporationId, this.DivisionId); }
    }
  }
  #endregion

  #region IKeyItem<DivisionId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class NpcCorporationDivision : IKeyItem<DivisionId>
  {
    DivisionId IKeyItem<DivisionId>.Key
    {
      get { return this.DivisionId; }
    }
  }
  #endregion
}