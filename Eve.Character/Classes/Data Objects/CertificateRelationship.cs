//-----------------------------------------------------------------------
// <copyright file="CertificateRelationship.cs" company="Jeremy H. Todd">
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
  /// Contains information about a relationship between certificates.
  /// </summary>
  public sealed partial class CertificateRelationship
    : EveEntityAdapter<CertificateRelationshipEntity, CertificateRelationship>,
      IKeyItem<CertificateRelationshipId>
  {
    private Certificate child;
    private Certificate parent;
    private SkillType parentType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the CertificateRelationship class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal CertificateRelationship(IEveRepository repository, CertificateRelationshipEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the child <see cref="Certificate" />.
    /// </summary>
    /// <value>
    /// The child <see cref="Certificate" />.
    /// </value>
    public Certificate Child
    {
      get
      {
        Contract.Ensures(Contract.Result<Certificate>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.child, this.Entity.ChildId, () => this.Entity.Child);
      }
    }

    /// <summary>
    /// Gets the ID of the child <see cref="Certificate" />.
    /// </summary>
    /// <value>
    /// The ID of the child <see cref="Certificate" />.
    /// </value>
    public CertificateId ChildId
    {
      get { return this.Entity.ChildId; }
    }

    /// <summary>
    /// Gets the parent <see cref="Certificate" />.
    /// </summary>
    /// <value>
    /// The parent <see cref="Certificate" />, or
    /// <see langword="null" /> if no parent certificate
    /// exists.
    /// </value>
    /// <remarks>
    /// <para>
    /// If <c>Parent</c> is <see langword="null" />, then the
    /// <see cref="ParentType" /> and <see cref="ParentLevel" />
    /// properties will contain skill requirement information.
    /// </para>
    /// </remarks>
    public Certificate Parent
    {
      get
      {
        if (this.ParentId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter(ref this.parent, this.Entity.ParentId, () => this.Entity.Parent);
      }
    }

    /// <summary>
    /// Gets the ID of the parent <see cref="Certificate" />.
    /// </summary>
    /// <value>
    /// The ID of the parent <see cref="Certificate" />, or
    /// <see langword="null" /> if no parent certificate
    /// exists.
    /// </value>
    public CertificateId? ParentId
    {
      get { return this.Entity.ParentId; }
    }

    /// <summary>
    /// Gets the required level of the skill specified by the
    /// <see cref="ParentType" /> property (or 0 if <c>ParentType</c>
    /// is <see langword="null" />.
    /// </summary>
    /// <value>
    /// The required level of the skill specified by the
    /// <see cref="ParentType" /> property (or 0 if <c>ParentType</c>
    /// is <see langword="null" />.
    /// </value>
    public byte ParentLevel
    {
      get { return this.Entity.ParentLevel.HasValue ? this.Entity.ParentLevel.Value : (byte)0; }
    }

    /// <summary>
    /// Gets the type of the parent required skill.
    /// </summary>
    /// <value>
    /// The type of the parent required skill, or
    /// <see langword="null" /> if no parent skill is specified.
    /// </value>
    /// <remarks>
    /// <para>
    /// If <c>ParentType</c> is <see langword="null" />, then the
    /// <see cref="Parent" /> property will specify a prerequisite
    /// certificate.
    /// </para>
    /// </remarks>
    public SkillType ParentType
    {
      get
      {
        if (this.ParentTypeId == null)
        {
          return null;
        }

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.LazyInitializeAdapter<EveTypeEntity, EveType, SkillType>(ref this.parentType, this.Entity.ParentTypeId, () => this.Entity.ParentType);
      }
    }

    /// <summary>
    /// Gets the ID of the type of the parent required skill.
    /// </summary>
    /// <value>
    /// The ID of the type of the parent required skill, or
    /// <see langword="null" /> if no parent skill is specified.
    /// </value>
    public TypeId? ParentTypeId
    {
      get
      {
        // Non-skill records contain a 0, but we want to translate to a null
        return this.Entity.ParentTypeId == 0 ? (TypeId?)null : (TypeId?)this.Entity.ParentTypeId;
      }
    }

    /// <summary>
    /// Gets the underlying database value of the corresponding adapter property.
    /// </summary>
    /// <value>
    /// The underlying database value of the corresponding adapter property.
    /// </value>
    public CertificateRelationshipId Id
    {
      get { return this.Entity.Id; }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(CertificateRelationship other)
    {
      if (other == null)
      {
        return 1;
      }

      return this.ToString().CompareTo(other.ToString());
    }

    /// <inheritdoc />
    public override bool Equals(CertificateRelationship other)
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
      return this.Child.Class.Name;
    }
  }

  #region IKeyItem<CertificateRelationshipId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class CertificateRelationship : IKeyItem<CertificateRelationshipId>
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Value is accessible via the Id property.")]
    CertificateRelationshipId IKeyItem<CertificateRelationshipId>.Key
    {
      get { return this.Id; }
    }
  }
  #endregion
}