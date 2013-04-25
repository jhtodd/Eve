//-----------------------------------------------------------------------
// <copyright file="CertificateRecommendation.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet.Collections;

  /// <summary>
  /// Contains information about an icon associated with an EVE item.
  /// </summary>
  public sealed partial class CertificateRecommendation
    : EveEntityAdapter<CertificateRecommendationEntity>,
      IComparable,
      IComparable<CertificateRecommendation>,
      IEquatable<CertificateRecommendation>,
      IEveCacheable,
      IKeyItem<CertificateRecommendationId>
  {
    private Certificate certificate;
    private EveType shipType; // TODO: Change to ShipType

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateRecommendation class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CertificateRecommendation(IEveRepository repository, CertificateRecommendationEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the certificate specified by the recommendation.
    /// </summary>
    /// <value>
    /// The <see cref="Certificate" /> specified by the recommendation.
    /// </value>
    public Certificate Certificate
    {
      get
      {
        Contract.Ensures(Contract.Result<Certificate>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.certificate, this.Entity.CertificateId, () => this.Entity.Certificate);
      }
    }

    /// <summary>
    /// Gets the ID of the certificate specified by the recommendation.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Certificate" /> specified by the recommendation.
    /// </value>
    public CertificateId CertificateId
    {
      get { return this.Entity.CertificateId; }
    }

    /// <summary>
    /// Gets the ID of the recommendation.
    /// </summary>
    /// <value>
    /// The ID of the recommendation.
    /// </value>
    public CertificateRecommendationId Id
    {
      get { return this.Entity.Id; }
    }

    /// <summary>
    /// Gets the recommendation level.  Seems to always be 0.
    /// </summary>
    /// <value>
    /// The recommendation level.
    /// </value>
    public byte RecommendationLevel
    {
      get { return this.Entity.RecommendationLevel; }
    }

    /// <summary>
    /// Gets the type of ship the recommendation applies to.
    /// </summary>
    /// <value>
    /// The type of ship the recommendation applies to.
    /// </value>
    public EveType ShipType // TODO: Change to ShipType
    {
      get
      {
        Contract.Ensures(Contract.Result<EveType>() != null);
        
        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.shipType, this.Entity.ShipTypeId, () => this.Entity.ShipType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of ship the recommendation applies to.
    /// </summary>
    /// <value>
    /// The ID of the type of ship the recommendation applies to.
    /// </value>
    public TypeId ShipTypeId
    {
      get { return this.Entity.ShipTypeId; }
    }

    /* Methods */

    /// <inheritdoc />
    public int CompareTo(CertificateRecommendation other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.ToString().CompareTo(other.ToString());
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as CertificateRecommendation);
    }

    /// <inheritdoc />
    public bool Equals(CertificateRecommendation other)
    {
      if (other == null)
      {
        return false;
      }

      return this.Id.Equals(other.Id);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.ShipType.Name + ": " + this.Certificate.ToString() + " (" + this.RecommendationLevel.ToString() + ")";
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public sealed partial class CertificateRecommendation : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      CertificateRecommendation other = obj as CertificateRecommendation;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IKeyItem<CertificateRecommendationId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class CertificateRecommendation : IKeyItem<CertificateRecommendationId>
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Value is accessible via the Id property.")]
    CertificateRecommendationId IKeyItem<CertificateRecommendationId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}