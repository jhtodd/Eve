//-----------------------------------------------------------------------
// <copyright file="RegionJump.cs" company="Jeremy H. Todd">
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
  /// Contains information about a jump link between regions.
  /// </summary>
  public class RegionJump : EntityAdapter<RegionJumpEntity>,
                            IComparable,
                            IComparable<RegionJump>,
                            IEquatable<RegionJump>,
                            IHasId<long>,
                            IKeyItem<long> {

    #region Static Methods
    //******************************************************************************
    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// 
    /// <param name="fromId">
    /// The ID of the origin region.
    /// </param>
    /// 
    /// <param name="toId">
    /// The ID of the destination region.
    /// </param>
    /// 
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCompoundId(RegionId fromId, RegionId toId) {
      return (long) ((((ulong) (long) fromId.GetHashCode()) << 32) | ((ulong) (long) toId.GetHashCode()));
    }
    #endregion

    #region Instance Fields
    private Region _fromRegion;
    private Region _toRegion;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the RegionJump class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal RegionJump(RegionJumpEntity entity) : base(entity) {
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
        return (RegionId) Entity.ToRegionId;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(RegionJump other) {
      if (other == null) {
        return 1;
      }

      int result = FromRegion.Name.CompareTo(other.FromRegion.Name);

      if (result == 0) {
        result = ToRegion.Name.CompareTo(other.ToRegion.Name);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as RegionJump);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(RegionJump other) {
      if (other == null) {
        return false;
      }

      return FromRegionId == other.FromRegionId && ToRegionId == other.ToRegionId;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return CompoundHashCode.Create(FromRegionId, ToRegionId);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return FromRegion.Name + " to " + ToRegion.Name;
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      RegionJump other = obj as RegionJump;
      return CompareTo(other);
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get {
        return CreateCompoundId(FromRegionId, ToRegionId);
      }
    }
    #endregion
    #region IHasId<long> Members
    //******************************************************************************
    long IHasId<long>.Id {
      get {
        return CreateCompoundId(FromRegionId, ToRegionId);
      }
    }
    #endregion
    #region IKeyItem<long> Members
    //******************************************************************************
    long IKeyItem<long>.Key {
      get {
        return CreateCompoundId(FromRegionId, ToRegionId);
      }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of region jumps.
  /// </summary>
  public class ReadOnlyRegionJumpCollection : ReadOnlyCollection<RegionJump> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyRegionJumpCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyRegionJumpCollection(IEnumerable<RegionJump> contents) : base() {
      if (contents != null) {
        foreach (RegionJump regionJump in contents) {
          Items.AddWithoutCallback(regionJump);
        }
      }
    }
    #endregion
  }
}