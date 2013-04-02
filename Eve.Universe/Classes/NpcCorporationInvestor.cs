//-----------------------------------------------------------------------
// <copyright file="NpcCorporationInvestor.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;

  //******************************************************************************
  /// <summary>
  /// Contains information about an investor in a corporation.
  /// </summary>
  public class NpcCorporationInvestor : IComparable,
                                        IComparable<NpcCorporationInvestor>,
                                        IEquatable<NpcCorporationInvestor>,
                                        IHasId<NpcCorporationId>,
                                        IKeyItem<NpcCorporationId> {

    #region Instance Fields
    private NpcCorporation _investor;
    private NpcCorporationId _investorId;
    private byte _shares;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the NpcCorporationInvestor class.
    /// </summary>
    /// 
    /// <param name="investorId">
    /// The ID of the investing corporation.
    /// </param>
    /// 
    /// <param name="shares">
    /// The percentage of shares owned by the investing corporation.
    /// </param>
    public NpcCorporationInvestor(NpcCorporationId investorId, byte shares) {
      _investorId = investorId;
      _shares = shares;
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
    /// Gets the investing corporation.
    /// </summary>
    /// 
    /// <value>
    /// The investing corporation.
    /// </value>
    public NpcCorporation Investor {
      get {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        if (_investor == null) {
          _investor = Eve.General.DataSource.GetNpcCorporationById(InvestorId);
        }

        return _investor;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the ID of the investing corporation.
    /// </summary>
    /// 
    /// <value>
    /// The ID of the investing corporation.
    /// </value>
    public NpcCorporationId InvestorId {
      get {
        return _investorId;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets the number of shares invested.
    /// </summary>
    /// 
    /// <value>
    /// The number of shares invested.
    /// </value>
    public byte Shares {
      get {
        return _shares;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public virtual int CompareTo(NpcCorporationInvestor other) {
      if (other == null) {
        return 1;
      }

      int result = other.Shares.CompareTo(Shares);

      if (result == 0) {
        result = Investor.CompareTo(other.Investor);
      }

      return result;
    }
    //******************************************************************************
    /// <inheritdoc />
    public override bool Equals(object obj) {
      return Equals(obj as Item);
    }
    //******************************************************************************
    /// <inheritdoc />
    public virtual bool Equals(NpcCorporationInvestor other) {
      if (other == null) {
        return false;
      }

      return InvestorId.Equals(other.InvestorId);
    }
    //******************************************************************************
    /// <inheritdoc />
    public override int GetHashCode() {
      return InvestorId.GetHashCode();
    }
    //******************************************************************************
    /// <inheritdoc />
    public override string ToString() {
      return Investor.Name + " (" + Shares.ToString() + " shares)";
    }
    #endregion

    #region IComparable Members
    //******************************************************************************
    int IComparable.CompareTo(object obj) {
      NpcCorporationInvestor other = obj as NpcCorporationInvestor;
      return CompareTo(other);
    }
    #endregion
    #region IHasId Members
    //******************************************************************************
    object IHasId.Id {
      get { return InvestorId; }
    }
    #endregion
    #region IHasId<NpcCorporationId> Members
    //******************************************************************************
    NpcCorporationId IHasId<NpcCorporationId>.Id {
      get { return InvestorId; }
    }
    #endregion
    #region IKeyItem<NpcCorporationId> Members
    //******************************************************************************
    NpcCorporationId IKeyItem<NpcCorporationId>.Key {
      get { return InvestorId; }
    }
    #endregion
  }

  //******************************************************************************
  /// <summary>
  /// A read-only collection of corporate investors.
  /// </summary>
  public class ReadOnlyNpcCorporationInvestorCollection : ReadOnlyCollection<NpcCorporationInvestor> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the ReadOnlyNpcCorporationInvestorCollection class.
    /// </summary>
    /// 
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyNpcCorporationInvestorCollection(IEnumerable<NpcCorporationInvestor> contents) : base() {
      if (contents != null) {
        foreach (NpcCorporationInvestor investor in contents) {
          Items.AddWithoutCallback(investor);
        }
      }
    }
    #endregion
  }
}