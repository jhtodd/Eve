//-----------------------------------------------------------------------
// <copyright file="Certificate.cs" company="Jeremy H. Todd">
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
  /// Contains information about a skill certificate granted to a character.
  /// </summary>
  public sealed partial class Certificate
    : EveEntityAdapter<CertificateEntity, Certificate>,
      IHasIcon,
      IKeyItem<CertificateId>
  {
    private CertificateCategory category;
    private CertificateClass certificateClass;
    private NpcCorporation corporation;
    private Icon icon;
    private ReadOnlyCertificateRelationshipCollection prerequisites;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the Certificate class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal Certificate(IEveRepository repository, CertificateEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the category to which the certificate belongs.
    /// </summary>
    /// <value>
    /// The <see cref="CertificateCategory" /> to which the
    /// certificate belongs.
    /// </value>
    public CertificateCategory Category
    {
      get
      {
        Contract.Ensures(Contract.Result<CertificateCategory>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.category, this.Entity.CategoryId, () => this.Entity.Category);
      }
    }

    /// <summary>
    /// Gets the ID of the category to which the certificate belongs.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="CertificateCategory" /> to which the
    /// certificate belongs.
    /// </value>
    public CertificateCategoryId CategoryId
    {
      get { return this.Entity.CategoryId; }
    }

    /// <summary>
    /// Gets the <see cref="CertificateClass" /> to which the certificate 
    /// belongs.
    /// </summary>
    /// <value>
    /// The <see cref="CertificateClass" /> to which the certificate 
    /// belongs.
    /// </value>
    public CertificateClass Class
    {
      get
      {
        Contract.Ensures(Contract.Result<CertificateClass>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.certificateClass, this.Entity.ClassId, () => this.Entity.Class);
      }
    }

    /// <summary>
    /// Gets ID of the <see cref="CertificateClass" /> to which the certificate 
    /// belongs.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="CertificateClass" /> to which the certificate 
    /// belongs.
    /// </value>
    public CertificateClassId ClassId
    {
      get { return this.Entity.ClassId; }
    }

    /// <summary>
    /// Gets the corporation associated with the certificate.
    /// </summary>
    /// <value>
    /// The <see cref="NpcCorporation" /> associated with the certificate.
    /// </value>
    public NpcCorporation Corporation
    {
      get
      {
        Contract.Ensures(Contract.Result<NpcCorporation>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.corporation, this.Entity.CorporationId, () => this.Entity.Corporation);
      }
    }

    /// <summary>
    /// Gets the ID of the corporation associated with the certificate.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="NpcCorporation" /> associated with the certificate.
    /// </value>
    public NpcCorporationId CorporationId 
    {
      get { return this.Entity.CorporationId; }
    }

    /// <summary>
    /// Gets the description of the item.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> that describes the item.
    /// </value>
    public string Description
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return Entity.Description ?? string.Empty;
      }
    }

    /// <summary>
    /// Gets the grade of the certificate.
    /// </summary>
    /// <value>
    /// A <see cref="CertificateGrade" /> value indicating the grade
    /// of the certificate.
    /// </value>
    public CertificateGrade Grade
    {
      get { return this.Entity.Grade; }
    }

    /// <summary>
    /// Gets the <see cref="Icon" /> associated with the item.
    /// </summary>
    /// <value>
    /// The <see cref="Icon" /> associated with the item.
    /// </value>
    public Icon Icon
    {
      get
      {
        Contract.Ensures(Contract.Result<Icon>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.icon, this.Entity.IconId, () => this.Entity.Icon);
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Icon" /> associated with the item.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Icon" /> associated with the item.
    /// </value>
    public IconId IconId
    {
      get { return this.Entity.IconId; }
    }

    /// <summary>
    /// Gets the ID of the item.
    /// </summary>
    /// <value>
    /// The unique value which identifies the item.
    /// </value>
    public CertificateId Id
    {
      get { return this.Entity.Id; }
    }

    /// <summary>
    /// Gets the collection of prerequisites for the certificate.
    /// </summary>
    /// <value>
    /// A <see cref="ReadOnlyCertificateRelationshipCollection"/> containing
    /// the prerequisites for the certificate.
    /// </value>
    public ReadOnlyCertificateRelationshipCollection Prerequisites
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCertificateRelationshipCollection>() != null);

        return Certificate.LazyInitialize(
          ref this.prerequisites,
          () => ReadOnlyCertificateRelationshipCollection.Create(this.Repository, this.Entity.Prerequisites));
      }
    } 

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(Certificate other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.Class.CompareTo(other.Class);

      if (result == 0)
      {
        result = this.Grade.CompareTo(other.Grade);
      }

      return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.Class.Name + " (" + this.Grade.ToString() + ")";
    }
  }

  #region IHasIcon Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IHasIcon" /> interface.
  /// </content>
  public partial class Certificate : IHasIcon
  {
    Icon IHasIcon.Icon
    {
      get { return this.Icon; }
    }

    IconId? IHasIcon.IconId
    {
      get { return this.IconId; }
    }
  }
  #endregion

  #region IKeyItem<CertificateId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class Certificate : IKeyItem<CertificateId>
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Value is accessible via the Id property.")]
    CertificateId IKeyItem<CertificateId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}