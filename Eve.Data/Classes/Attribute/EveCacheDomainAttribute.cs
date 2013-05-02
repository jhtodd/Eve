//-----------------------------------------------------------------------
// <copyright file="EveCacheDomainAttribute.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Character;
  using Eve.Universe;

  /// <summary>
  /// Indicates that the target type has a cache domain other than the
  /// type itself.  This also applies to all derived classes as well,
  /// unless they have a <c>EveCacheDomainAttribute</c> of their own.
  /// </summary>
  /// <remarks>
  /// <para>
  /// A cache domain is the domain in which a cache key is unique.  For
  /// example, the <see cref="EveType" /> class has several derived
  /// classes: <see cref="SkillType" />, <see cref="StationType" />,
  /// and so on.  However, the value of each type's <c>Id</c>
  /// property is unique across all types derived from <see cref="EveType" />,
  /// and so the cache domain for all of those types should be set to
  /// <see cref="EveType" />.  This simplifies the cache retrieval process.
  /// </para>
  /// <para>
  /// If no instance is attached to a class (or one of its base classes),
  /// then the class itself it considered the cache domain.
  /// </para>
  /// <para>
  /// This attribute is only meaningful when attached to a type that
  /// implements the <see cref="IEveCacheable" /> interface.  In general it
  /// should be attached to any type that could have derived types stored
  /// in the cache.
  /// </para>
  /// </remarks>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
  public class EveCacheDomainAttribute : Attribute
  {
    private Type cacheDomain;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="EveCacheDomainAttribute" /> class.
    /// </summary>
    /// <param name="cacheDomain">
    /// The <see cref="AttributeType" /> specifying the cache domain of the target class.
    /// </param>
    public EveCacheDomainAttribute(Type cacheDomain)
    {
      Contract.Requires(cacheDomain != null, "The cache domain cannot be null.");
      this.cacheDomain = cacheDomain;
    }

    /* Properties */

    /// <summary>
    /// Gets the EVE cache domain of the target class.
    /// </summary>
    /// <value>
    /// The EVE cache domain of the target class.
    /// </value>
    public Type CacheDomain
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return this.cacheDomain;
      }
    }

    /* Methods */

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.cacheDomain != null);
    }
  }
}
