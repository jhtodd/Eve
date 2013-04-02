//-----------------------------------------------------------------------
// <copyright file="ConstellationJump.cs" company="Jeremy H. Todd">
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
  /// Contains information about a jump link between constellations.
  /// </summary>
  public class ConstellationJump : EntityAdapter<ConstellationJumpEntity>,
                            IComparable,
                            IComparable<ConstellationJump>,
                            IEquatable<ConstellationJump>,
                            IHasId<long>,
                            IKeyItem<long> {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// 
    /// <param name="fromId">
    /// The ID of the origin constellation.
    /// </param>
    /// 
    /// <param name="toId">
    /// The ID of the destination constellation.
    /// </param>
    /// 
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(ConstellationId fromId, ConstellationId toId) {
      return (long) ((((ulong) (long) fromId.GetHashCode()) << 32) | ((ulong) (long) toId.GetHashCode()));
    }
    #endregion

    #region Instance Fields
    private Constellation _fromConstellation;
    private Region _fromRegion;
    private Constellation _toConstellation;
    private Region _toRegion;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ConstellationJump class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal ConstellationJump(ConstellationJumpEntity entity) : base(entity) {
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
    /// Gets the origin constellation.
    /// </summary>
    /// 
    /// <value>
    /// The origin constellation.
    /// </value>
    public Constellation FromConstellation {
      get {
        Contract.Ensures(Contract.Result<Constellation>() != null);

        if (_fromConstellation == null) {

          // Load the cached version if available
          _fromConstellation = Eve.General.Cache.GetOrAdd<Constellation>(FromConstellationId, () => {
            ConstellationEntity constellationEntity = Entity.FromConstellation;
            Contract.Assume(constellationEntity != null);

            return new Constellation(constellationEntity);
          });
        }

        return _fromConstellation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the origin constellation.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the origin constellation.
    /// </value>
    public ConstellationId FromConstellationId {
      get {
        return Entity.FromConstellationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the origin region.
    /// </summary>
    /// 
    /// <value>
    /// The origin region.
    /// </value>
    public Region FromRegion {
      get {
        Contract.Ensures(Contract.Result<Region>() != null);

        if (_fromRegion == null) {

          // Load the cached version if available
          _fromRegion = Eve.General.Cache.GetOrAdd<Region>(FromRegionId, () => {
            RegionEntity regionEntity = Entity.FromRegion;
            Contract.Assume(regionEntity != null);

            return new Region(regionEntity);
          });
        }

        return _fromRegion;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the origin region.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the origin region.
    /// </value>
    public RegionId FromRegionId {
      get {
        return Entity.FromRegionId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the destination constellation.
    /// </summary>
    /// 
    /// <value>
    /// The destination constellation.
    /// </value>
    public Constellation ToConstellation {
      get {
        Contract.Ensures(Contract.Result<Constellation>() != null);

        if (_toConstellation == null) {

          // Load the cached version if available
          _toConstellation = Eve.General.Cache.GetOrAdd<Constellation>(ToConstellationId, () => {
            ConstellationEntity constellationEntity = Entity.ToConstellation;
            Contract.Assume(constellationEntity != null);

            return new Constellation(constellationEntity);
          });
        }

        return _toConstellation;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the destination constellation.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the destination constellation.
    /// </value>
    public ConstellationId ToConstellationId {
      get {
        return Entity.ToConstellationId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the destination region.
    /// </summary>
    /// 
    /// <value>
    /// The destination region.
    /// </value>
    public Region ToRegion {
      get {
        Contract.Ensures(Contract.Result<Region>() != null);

        if (_toRegion == null) {

          // Load the cached version if available
          _toRegion = Eve.General.Cache.GetOrAdd<Region>(ToRegionId, () => {
            RegionEntity regionEntity = Entity.ToRegion;
            Contract.Assume(regionEntity != null);

            return new Region(regionEntity);
          });
        }

        return _toRegion;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the destination region.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the destination region.
    /// </value>
    public RegionId ToRegionId {
      get {
        return Entity.ToRegionId;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(ConstellationJump other) {
      if (other == null) {
        return 1;
      }

      int result = FromConstellation.Name.CompareTo(other.FromConstellation.Name);

      if (result == 0) {
        result = ToConstellation.Name.CompareTo(other.ToConstellation.Name);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as ConstellationJump);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(ConstellationJump other) {
      if (other == null) {
        return false;
      }

      return FromConstellationId == other.FromConstellationId && ToConstellationId == other.ToConstellationId;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return CompoundHashCode.Create(FromConstellationId, ToConstellationId);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return FromConstellation.Name + " to " + ToConstellation.Name;
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      ConstellationJump other = obj as ConstellationJump;
      return CompareTo(other);
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get {
        return CreateCompoundId(FromConstellationId, ToConstellationId);
      }
    }
    #endregion
    #region IHasId<long> Members
    //******************************************************************************
    long IHasId<long>.Id {
      get {
        return CreateCompoundId(FromConstellationId, ToConstellationId);
      }
    }
    #endregion
    #region IKeyItem<long> Members
    //******************************************************************************
    long IKeyItem<long>.Key {
      get {
        return CreateCompoundId(FromConstellationId, ToConstellationId);
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of constellation jumps.
  /// </summary>
  public class ReadOnlyConstellationJumpCollection : ReadOnlyCollection<ConstellationJump> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyConstellationJumpCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyConstellationJumpCollection(IEnumerable<ConstellationJump> contents) : base() {
      if (contents != null) {
        foreach (ConstellationJump constellationJump in contents) {
          Items.AddWithoutCallback(constellationJump);
        }
      }
    }
    #endregion
  }
}