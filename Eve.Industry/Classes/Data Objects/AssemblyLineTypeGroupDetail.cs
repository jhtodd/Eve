//-----------------------------------------------------------------------
// <copyright file="AssemblyLineTypeGroupDetail.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Threading;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections;
  using FreeNet.Utilities;

  /// <summary>
  /// Contains information about which <see cref="Group">Groups</see> an assembly
  /// line can process.
  /// </summary>
  public sealed partial class AssemblyLineTypeGroupDetail
    : EveEntityAdapter<AssemblyLineTypeGroupDetailEntity, AssemblyLineTypeGroupDetail>,
      IKeyItem<GroupId>
  {
    private AssemblyLineType assemblyLineType;
    private Group group;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the AssemblyLineTypeGroupDetail class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> which contains the entity adapter.
    /// </param>
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    internal AssemblyLineTypeGroupDetail(IEveRepository repository, AssemblyLineTypeGroupDetailEntity entity) : base(repository, entity)
    {
      Contract.Requires(repository != null, "The repository associated with the object cannot be null.");
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
        return this.LazyInitializeAdapter(ref this.assemblyLineType, this.Entity.AssemblyLineTypeId, () => this.Entity.AssemblyLineType);
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
      get { return this.Entity.AssemblyLineTypeId; }
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
        return this.LazyInitializeAdapter(ref this.group, this.Entity.GroupId, () => this.Entity.Group);
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
      get { return this.Entity.GroupId; }
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

    /// <inheritdoc />
    public override int CompareTo(AssemblyLineTypeGroupDetail other)
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
    public override string ToString()
    {
      return this.AssemblyLineType.Name + " (" + this.Group.Name + ")";
    }
  }

  #region IKeyItem<GroupId> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IKeyItem{TKey}" /> interface.
  /// </content>
  public sealed partial class AssemblyLineTypeGroupDetail : IKeyItem<GroupId>
  {
    GroupId IKeyItem<GroupId>.Key
    {
      get { return this.GroupId; }
    }
  }
  #endregion
}