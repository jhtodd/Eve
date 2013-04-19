//-----------------------------------------------------------------------
// <copyright file="NpcCorporationInvestor.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;

  using FreeNet.Collections;

  /// <summary>
  /// Contains information about an investor in a corporation.
  /// </summary>
  public sealed partial class NpcCorporationInvestor 
    : IComparable,
      IComparable<NpcCorporationInvestor>,
      IEquatable<NpcCorporationInvestor>,
      IEveCacheable,
      IEveRepositoryItem,
      IKeyItem<NpcCorporationId>
  {
    private readonly IEveRepository container;
    private readonly NpcCorporationId investorId;
    private readonly byte shares;
    private NpcCorporation investor;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="NpcCorporationInvestor" /> class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="investorId">
    /// The ID of the investing corporation.
    /// </param>
    /// <param name="shares">
    /// The percentage of shares owned by the investing corporation.
    /// </param>
    internal NpcCorporationInvestor(IEveRepository container, NpcCorporationId investorId, byte shares)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      
      this.container = container;
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

        LazyInitializer.EnsureInitialized(
          ref this.investor,
          () => this.Container.GetNpcCorporationById(this.InvestorId));

        Contract.Assume(this.investor != null);
        return this.investor;
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

    /// <summary>
    /// Gets the <see cref="IEveRepository" /> which contains the current
    /// entity adapter.
    /// </summary>
    /// <value>
    /// The <see cref="IEveRepository" /> which contains the current
    /// entity adapter.
    /// </value>
    private IEveRepository Container
    {
      get
      {
        Contract.Ensures(Contract.Result<IEveRepository>() != null);
        return this.container;
      }
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

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.container != null);
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
    IConvertible IEveCacheable.CacheKey
    {
      get { return this.InvestorId; }
    }
  }
  #endregion

  #region IEveRepositoryItem Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveRepositoryItem" /> interface.
  /// </content>
  public sealed partial class NpcCorporationInvestor : IEveRepositoryItem
  {
    IEveRepository IEveRepositoryItem.Container
    {
      get { return this.Container; }
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