//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeGroupDetail.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about which <see cref="Group">Groups</see> an assembly
  /// line can process.
  /// </summary>
  public sealed partial class AssemblyLineTypeGroupDetail
    : EveEntityAdapter<AssemblyLineTypeGroupDetailEntity>,
      IComparable<AssemblyLineTypeGroupDetail>,
      IEquatable<AssemblyLineTypeGroupDetail>,
      IEveCacheable,
      IKeyItem<long>
  {
    private AssemblyLineType assemblyLineType;
    private Group group;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeGroupDetail class.
    /// </summary>
    /// <param name="container">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AssemblyLineTypeGroupDetail(IEveRepository container, AssemblyLineTypeGroupDetailEntity entity) : base(container, entity)
    {
      Contract.Requires(container != null, "The containing repository cannot be null.");
      Contract.Requires(entity != null, "The entity cannot be null.");
    }

    /* Properties */

    /// <summary>
    /// Gets the type of assembly line to which this group detail information
    /// applies.
    /// </summary>
    /// <value>
    /// An <see cref="AssemblyLineType" /> describing the type of assembly
    /// line.
    /// </value>    
    public AssemblyLineType AssemblyLineType
    {
      get
      {
        Contract.Ensures(Contract.Result<AssemblyLineType>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.assemblyLineType ?? (this.assemblyLineType = this.Container.GetOrAdd<AssemblyLineType>(this.AssemblyLineTypeId, () => this.Entity.AssemblyLineType.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the type of assembly line to which this group detail
    /// information applies.
    /// </summary>
    /// <value>
    /// The ID of the type of assembly line to which this group detail
    /// information applies.
    /// </value>   
    public AssemblyLineTypeId AssemblyLineTypeId
    {
      get
      {
        return this.Entity.AssemblyLineTypeId;
      }
    }

    /// <summary>
    /// Gets the <see cref="Group" /> to which this group detail information
    /// applies.
    /// </summary>
    /// <value>
    /// The <see cref="Group" /> to which this group detail information
    /// applies.
    /// </value>    
    public Group Group
    {
      get
      {
        Contract.Ensures(Contract.Result<Group>() != null);

        // If not already set, load from the cache, or else create an instance from the base entity
        return this.group ?? (this.group = this.Container.GetOrAdd<Group>(this.GroupId, () => this.Entity.Group.ToAdapter(this.Container)));
      }
    }

    /// <summary>
    /// Gets the ID of the <see cref="Group" /> to which this group detail
    /// information applies.
    /// </summary>
    /// <value>
    /// The ID of the <see cref="Group" /> to which this group detail
    /// information applies.
    /// </value> 
    public GroupId GroupId
    {
      get
      {
        return this.Entity.GroupId;
      }
    }

    /// <summary>
    /// Gets the material multiplier for jobs performed on types in the
    /// specified group.
    /// </summary>
    /// <value>
    /// The material multiplier for jobs performed on types in the
    /// specified group.
    /// </value>    
    public double MaterialMultiplier
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.Entity.MaterialMultiplier;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /// <summary>
    /// Gets the time multiplier for jobs performed on types in the
    /// specified group.
    /// </summary>
    /// <value>
    /// The time multiplier for jobs performed on types in the
    /// specified group.
    /// </value>    
    public double TimeMultiplier
    {
      get
      {
        Contract.Ensures(!double.IsInfinity(Contract.Result<double>()));
        Contract.Ensures(!double.IsNaN(Contract.Result<double>()));
        Contract.Ensures(Contract.Result<double>() >= 0.0D);

        double result = this.Entity.TimeMultiplier;

        Contract.Assume(!double.IsInfinity(result));
        Contract.Assume(!double.IsNaN(result));
        Contract.Assume(result >= 0.0D);

        return result;
      }
    }

    /* Methods */

    /// <summary>
    /// Computes a compound ID for the specified sub-IDs.
    /// </summary>
    /// <param name="assemblyLineTypeId">
    /// The ID of the assembly line type.
    /// </param>
    /// <param name="groupId">
    /// The ID of the group.
    /// </param>
    /// <returns>
    /// A compound ID combining the two sub-IDs.
    /// </returns>
    public static long CreateCacheKey(AssemblyLineTypeId assemblyLineTypeId, GroupId groupId)
    {
      return (long)((((ulong)(long)assemblyLineTypeId.Value) << 32) | ((ulong)(long)groupId));
    }

    /// <inheritdoc />
    public int CompareTo(AssemblyLineTypeGroupDetail other)
    {
      if (other == null)
      {
        return 1;
      }

      int result = this.AssemblyLineType.Name.CompareTo(other.AssemblyLineType.Name);

      if (result == 0)
      {
        result = this.Group.Name.CompareTo(other.Group.Name);
      }

      return result;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as AssemblyLineTypeGroupDetail);
    }

    /// <inheritdoc />
    public bool Equals(AssemblyLineTypeGroupDetail other)
    {
      if (other == null)
      {
        return false;
      }

      return this.AssemblyLineTypeId == other.AssemblyLineTypeId && this.GroupId == other.GroupId;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return CompoundHashCode.Create(this.AssemblyLineTypeId, this.GroupId);
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this.AssemblyLineType.Name + " (" + this.Group.Name + ")";
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public sealed partial class AssemblyLineTypeGroupDetail : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      AssemblyLineTypeGroupDetail other = obj as AssemblyLineTypeGroupDetail;
      return this.CompareTo(other);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public sealed partial class AssemblyLineTypeGroupDetail : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return CreateCacheKey(this.AssemblyLineTypeId, this.GroupId); }
    }
  }
  #endregion

  #region IKeyItem<long> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class AssemblyLineTypeGroupDetail : IKeyItem<long>
  {
    long IKeyItem<long>.Key
    {
      get { return CreateCacheKey(this.AssemblyLineTypeId, this.GroupId); }
    }
  }
  #endregion
}