//-----------------------------------------------------------------------
// <copyright file="NpcCorporationInvestor.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Linq.Expressions;

  using Eve.Data;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;

  /// <summary>
  /// Contains information about an investor in a corporation.
  /// </summary>
  public sealed partial class NpcCorporationInvestor 
    : IComparable,
      IComparable<NpcCorporationInvestor>,
      IEquatable<NpcCorporationInvestor>,
      IEveCacheable,
      IKeyItem<NpcCorporationId>
  {
    private NpcCorporation investor;
    private NpcCorporationId investorId;
    private byte shares;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="NpcCorporationInvestor" /> class.
    /// </summary>
    /// <param name="investorId">
    /// The ID of the investing corporation.
    /// </param>
    /// <param name="shares">
    /// The percentage of shares owned by the investing corporation.
    /// </param>
    public NpcCorporationInvestor(NpcCorporationId investorId, byte shares)
    {
      this.investorId = investorId;
      this.shares = shares;
    }

    /* Properties */

    /// <summary>
    /// Gets the investing corporation.
    /// </summary>
    /// <value>
    /// The investing corporation.
    /// </value>
    public NpcCorporation Investor
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        return this.investor ?? (this.investor = Eve.General.DataSource.GetNpcCorporationById(this.InvestorId));
      }
    }

    /// <summary>
    /// Gets the ID of the investing corporation.
    /// </summary>
    /// <value>
    /// The ID of the investing corporation.
    /// </value>
    public NpcCorporationId InvestorId
    {
      get { return this.investorId; }
    }

    /// <summary>
    /// Gets the number of shares invested.
    /// </summary>
    /// <value>
    /// The number of shares invested.
    /// </value>
    public byte Shares
    {
      get { return this.shares; }
    }

    /* Methods */

    /// <inheritdoc />
    public int CompareTo(NpcCorporationInvestor other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Shares.CompareTo(other.Shares);

      if (result == 0)
      {
        result = this.Investor.CompareTo(other.Investor);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as Item);
    }

    /// <inheritdoc />
    public bool Equals(NpcCorporationInvestor other)
    {
      if (other == null)
      {
        return false;
      }

      return this.InvestorId.Equals(other.InvestorId);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this.InvestorId.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Investor.Name + " (" + this.Shares.ToString() + " shares)";
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class NpcCorporationInvestor : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      NpcCorporationInvestor other = obj as NpcCorporationInvestor;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class NpcCorporationInvestor : IEveCacheable
  {
    object IEveCacheable.CacheKey
    {
      get { return this.InvestorId; }
    }
  }
  #endregion

  #region IKeyItem<NpcCorporationId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public partial class NpcCorporationInvestor : IKeyItem<NpcCorporationId>
  {
    NpcCorporationId IKeyItem<NpcCorporationId>.Key
    {
      get { return this.InvestorId; }
    }
  }
  #endregion
}