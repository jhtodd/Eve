//-----------------------------------------------------------------------
// <copyright file="NpcCorporationDivision.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  using Eve.Data.Entities;
  using Eve.Meta;

  //******************************************************************************
  /// <summary>
  /// Contains information about a division of an NPC corporation.
  /// </summary>
  public class NpcCorporationDivision : EntityAdapter<NpcCorporationDivisionEntity>,
                                        IComparable,
                                        IComparable<NpcCorporationDivision>,
                                        IEquatable<NpcCorporationDivision>,
                                        IHasId<long>,
                                        IKeyItem<DivisionId> {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// 
    /// <param name="corporationId">
    /// The ID of the corporation.
    /// </param>
    /// 
    /// <param name="divisionId">
    /// The ID of the division.
    /// </param>
    /// 
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(NpcCorporationId corporationId, DivisionId divisionId) {
      return (long) ((((ulong) (long) divisionId) << 32) | (ulong) (long) corporationId);
    }
    #endregion

    #region Instance Fields
    private ReadOnlyAgentCollection _agents;
    private Division _division;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the NpcCorporationDivision class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal NpcCorporationDivision(NpcCorporationDivisionEntity entity) : base(entity) {
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
    /// Gets the collection of agents in the division.
    /// </summary>
    /// 
    /// <value>
    /// The collection of agents in the division.
    /// </value>
    public ReadOnlyAgentCollection Agents {
      get {
        Contract.Ensures(Contract.Result<ReadOnlyAgentCollection>() != null);

        if (_agents == null) {
          _agents = new ReadOnlyAgentCollection(Eve.General.DataSource.GetAgents(x => x.CorporationId == this.CorporationId.Value && x.DivisionId == this.DivisionId).OrderBy(x => x));
        }

        return _agents;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the division.
    /// </summary>
    /// 
    /// <value>
    /// The division.
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
    /// Gets the ID of the division.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the division.
    /// </value>
    public DivisionId DivisionId {
      get {
        return Entity.DivisionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the size of the division.
    /// </summary>
    /// 
    /// <value>
    /// The size of the division.
    /// </value>
    public byte Size {
      get {
        return Entity.Size;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(NpcCorporationDivision other) {
      if (other == null) {
        return 1;
      }

      return Division.CompareTo(other.Division);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as NpcCorporationDivision);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(NpcCorporationDivision other) {
      if (other == null) {
        return false;
      }

      return CorporationId == other.CorporationId &&
             DivisionId == other.DivisionId &&
             Size == other.Size;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return CompoundHashCode.Create(CorporationId, DivisionId, Size);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return Division.Name;
    }
    #endregion
    #region Protected Properties
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the corporation.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the corporation.
    /// </value>
    public NpcCorporationId CorporationId {
      get {
        return (NpcCorporationId) Entity.CorporationId;
      }
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      NpcCorporationDivision other = obj as NpcCorporationDivision;
      return CompareTo(other);
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get {
        return CreateCompoundId(CorporationId, DivisionId);
      }
    }
    #endregion
    #region IHasId<long> Members
    //******************************************************************************
    long IHasId<long>.Id {
      get {
        return CreateCompoundId(CorporationId, DivisionId);
      }
    }
    #endregion
    #region IKeyItem<DivisionId> Members
    //******************************************************************************
    DivisionId IKeyItem<DivisionId>.Key {
      get {
        return DivisionId;
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of corporation divisions.
  /// </summary>
  public class ReadOnlyNpcCorporationDivisionCollection : ReadOnlyKeyedCollection<DivisionId, NpcCorporationDivision> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyNpcCorporationDivisionCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyNpcCorporationDivisionCollection(IEnumerable<NpcCorporationDivision> contents) : base() {
      if (contents != null) {
        foreach (NpcCorporationDivision division in contents) {
          Items.AddWithoutCallback(division);
        }
      }
    }
    #endregion
  }
}