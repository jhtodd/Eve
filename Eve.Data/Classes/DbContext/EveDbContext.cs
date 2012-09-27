//-----------------------------------------------------------------------
// <copyright file="EveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Common;
  using System.Data.Entity;
  using System.Data.Objects;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  using Eve;
  using Eve.Entities;
  using Eve.Universe;

  //******************************************************************************
  /// <summary>
  /// A <see cref="DbContext" /> that provides data access to the EVE database.
  /// This access is read-only.
  /// </summary>
  public class EveDbContext : DbContext, IEveDbContext {

    #region Static Fields
    private static readonly EveDbContext _default = new EveDbContext();
    #endregion
    #region Static Properties
    //******************************************************************************
    /// <summary>
    /// Gets the default data context.  This should be used in most circumstances.
    /// </summary>
    /// 
    /// <value>
    /// The default <see cref="EveDbContext" />.
    /// </value>
    public static EveDbContext Default {
      get {
        Contract.Ensures(Contract.Result<EveDbContext>() != null);
        return _default;
      }
    }
    #endregion

    #region Instance Fields
    private IDbSet<AgentTypeEntity> _agentTypes;
    private IDbSet<AttributeCategoryEntity> _attributeCategories;
    private IDbSet<AttributeTypeEntity> _attributeTypes;
    private IDbSet<AttributeValueEntity> _attributeValues;
    private IDbSet<CategoryEntity> _categories;
    private IDbSet<GroupEntity> _groups;
    private IDbSet<IconEntity> _icons;
    private IDbSet<ItemTypeEntity> _itemTypes;
    private IDbSet<MarketGroupEntity> _marketGroups;
    private IDbSet<RaceEntity> _races;
    private IDbSet<UnitEntity> _units;
    #endregion

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDbContext class using conventions to
    /// create the name of the database to which a connection will be made. By
    /// convention the name is the full name (namespace + class name) of the
    /// derived context class.  For more information on how this is used to create
    /// a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    public EveDbContext() : base() {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDbContext class using the given string
    /// as the name or connection string for the database to which a connection
    /// will be made.  For more information on how this is used to create a
    /// connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    /// 
    /// <param name="nameOrConnectionString">
    /// Either the database name or a connection string.
    /// </param>
    public EveDbContext(string nameOrConnectionString) : base(nameOrConnectionString) {
    }
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the EveDbContext class using the existing
    /// connection to connect to a database. The connection will not be disposed
    /// when the context is disposed.
    /// </summary>
    /// 
    /// <param name="existingConnection">
    /// An existing connection to use for the new context.
    /// </param>
    /// 
    /// <param name="contextOwnsConnection">
    /// If set to true the connection is disposed when the context is disposed,
    /// otherwise the caller must dispose the connection.
    /// </param>
    public EveDbContext(DbConnection existingConnection, bool contextOwnsConnection)
      : base(existingConnection, contextOwnsConnection) {
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
    /// Gets or sets the <see cref="DbSet{T}" /> for agent types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for agent types.
    /// </value>
    public IDbSet<AgentTypeEntity> AgentTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AgentTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_agentTypes != null);
        return _agentTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _agentTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute categories.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute categories.
    /// </value>
    public IDbSet<AttributeCategoryEntity> AttributeCategories {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AttributeCategoryEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_attributeCategories != null);
        return _attributeCategories;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _attributeCategories = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute types.
    /// </value>
    public IDbSet<AttributeTypeEntity> AttributeTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AttributeTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_attributeTypes != null);
        return _attributeTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _attributeTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for attribute values.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for attribute values.
    /// </value>
    public IDbSet<AttributeValueEntity> AttributeValues {
      get {
        Contract.Ensures(Contract.Result<IDbSet<AttributeValueEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_attributeValues != null);
        return _attributeValues;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _attributeValues = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for categories.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for categories.
    /// </value>
    public IDbSet<CategoryEntity> Categories {
      get {
        Contract.Ensures(Contract.Result<IDbSet<CategoryEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_categories != null);
        return _categories;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _categories = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for groups.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for groups.
    /// </value>
    public IDbSet<GroupEntity> Groups {
      get {
        Contract.Ensures(Contract.Result<IDbSet<GroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_groups != null);
        return _groups;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _groups = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for icons.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for icons.
    /// </value>
    public IDbSet<IconEntity> Icons {
      get {
        Contract.Ensures(Contract.Result<IDbSet<IconEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_icons != null);
        return _icons;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _icons = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for item types.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for item types.
    /// </value>
    public IDbSet<ItemTypeEntity> ItemTypes {
      get {
        Contract.Ensures(Contract.Result<IDbSet<ItemTypeEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_itemTypes != null);
        return _itemTypes;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _itemTypes = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for market groups.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for market groups.
    /// </value>
    public IDbSet<MarketGroupEntity> MarketGroups {
      get {
        Contract.Ensures(Contract.Result<IDbSet<MarketGroupEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_marketGroups != null);
        return _marketGroups;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _marketGroups = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Returns the <see cref="ObjectContext" /> underlying the current
    /// <see cref="DbContext" />.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="ObjectContext" /> underlying the current
    /// <see cref="DbContext" />.
    /// </value>
    public ObjectContext ObjectContext {
      get {
        Contract.Ensures(Contract.Result<ObjectContext>() != null);

        var result = ((System.Data.Entity.Infrastructure.IObjectContextAdapter) this).ObjectContext;
        Contract.Assume(result != null);
        return result;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for races.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for races.
    /// </value>
    public IDbSet<RaceEntity> Races {
      get {
        Contract.Ensures(Contract.Result<IDbSet<RaceEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_races != null);
        return _races;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _races = value;
      }
    }
    //******************************************************************************
    /// <summary>
    /// Gets or sets the <see cref="DbSet{T}" /> for units.
    /// </summary>
    /// 
    /// <value>
    /// The <see cref="DbSet{T}" /> for units.
    /// </value>
    public IDbSet<UnitEntity> Units {
      get {
        Contract.Ensures(Contract.Result<IDbSet<UnitEntity>>() != null);

        // The field will be non-null by the time the property is accessed.
        Contract.Assume(_units != null);
        return _units;
      }
      set {
        Contract.Requires(value != null, "The DbSet cannot be null.");
        _units = value;
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <inheritdoc />
    public override int SaveChanges() {
      return 0;
    }
    //******************************************************************************
    /// <summary>
    /// Returns an <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </summary>
    /// 
    /// <typeparam name="TEntity">
    /// The type of entity for which to return a <see cref="IDbSet{TEntity}" />.
    /// </typeparam>
    /// 
    /// <returns>
    /// An <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </returns>
    /// 
    /// <remarks>
    /// <para>
    /// This method simply returns the value of the base class's
    /// <see cref="DbContext.Set{TEntity}()" /> method as an
    /// <see cref="IDbSet{TEntity}" /> instead of a <see cref="DbSet{TEntity}" />
    /// object, for ease of testing and mocking.
    /// </para>
    /// </remarks>
    public new virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class {
      return base.Set<TEntity>();
    }
    #endregion
    #region Protected Methods
    //******************************************************************************
    /// <inheritdoc />
    [ContractVerification(false)]
    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

      #region AgentTypeEntity Mappings
      var agentType = modelBuilder.Entity<AgentTypeEntity>();
      
      // Map properties inherited from BaseValueEntity<>
      agentType.Map(x => x.MapInheritedProperties());
      agentType.HasKey(x => x.Id);
      agentType.Ignore(x => x.Description);
      agentType.Property(x => x.Id).HasColumnName("agentTypeID");
      agentType.Property(x => x.Name).HasColumnName("agentType");
      #endregion

      #region AttributeTypeEntity Mappings
      var attributeType = modelBuilder.Entity<AttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeType.Map(x => x.MapInheritedProperties());
      attributeType.HasKey(x => x.Id);
      attributeType.Property(x => x.Description).HasColumnName("description");
      attributeType.Property(x => x.Id).HasColumnName("attributeID");
      attributeType.Property(x => x.Name).HasColumnName("attributeName");
      #endregion

      #region AttributeValueEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region AttributeCategoryEntity Mappings
      var attributeCategory = modelBuilder.Entity<AttributeCategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeCategory.Map(x => x.MapInheritedProperties());
      attributeCategory.HasKey(x => x.Id);
      attributeCategory.Property(x => x.Description).HasColumnName("categoryDescription");
      attributeCategory.Property(x => x.Id).HasColumnName("categoryID");
      attributeCategory.Property(x => x.Name).HasColumnName("categoryName");
      #endregion

      #region CategoryEntity Mappings
      var category = modelBuilder.Entity<CategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      category.Map(x => x.MapInheritedProperties());
      category.HasKey(x => x.Id);
      category.Property(x => x.Description).HasColumnName("description");
      category.Property(x => x.Id).HasColumnName("categoryID");
      category.Property(x => x.Name).HasColumnName("categoryName");
      #endregion

      #region GroupEntity Mappings
      var group = modelBuilder.Entity<GroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      group.Map(x => x.MapInheritedProperties());
      group.HasKey(x => x.Id);
      group.Property(x => x.Description).HasColumnName("description");
      group.Property(x => x.Id).HasColumnName("groupID");
      group.Property(x => x.Name).HasColumnName("groupName");

      // Map the ItemTypes collection
      group.HasMany(x => x.ItemTypes).WithRequired(x => x.Group).HasForeignKey(x => x.GroupId);
      #endregion

      #region IconEntity Mappings
      var icon = modelBuilder.Entity<IconEntity>();

      // Map properties inherited from BaseValueEntity<>
      icon.Map(x => x.MapInheritedProperties());
      icon.HasKey(x => x.Id);
      icon.Property(x => x.Description).HasColumnName("description");
      icon.Property(x => x.Id).HasColumnName("iconID");
      icon.Property(x => x.Name).HasColumnName("iconFile");
      #endregion

      #region ItemTypeEntity Mappings
      var itemType = modelBuilder.Entity<ItemTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      itemType.Map(x => x.MapInheritedProperties());
      itemType.HasKey(x => x.Id);
      itemType.Property(x => x.Description).HasColumnName("description");
      itemType.Property(x => x.Id).HasColumnName("typeID");
      itemType.Property(x => x.Name).HasColumnName("typeName");
      
      // Map the Attributes collection
      itemType.HasMany(x => x.Attributes).WithRequired().HasForeignKey(x => x.ItemTypeId);
      #endregion

      #region MarketGroupEntity Mappings
      var marketGroup = modelBuilder.Entity<MarketGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      marketGroup.Map(x => x.MapInheritedProperties());
      marketGroup.HasKey(x => x.Id);
      marketGroup.Property(x => x.Description).HasColumnName("description");
      marketGroup.Property(x => x.Id).HasColumnName("marketGroupID");
      marketGroup.Property(x => x.Name).HasColumnName("marketGroupName");

      // Map the ChildGroups collection
      marketGroup.HasMany(x => x.ChildGroups).WithOptional(x => x.ParentGroup).HasForeignKey(x => x.ParentGroupId);

      // Map the ItemTypes collection
      marketGroup.HasMany(x => x.ItemTypes).WithRequired(x => x.MarketGroup).HasForeignKey(x => x.MarketGroupId);
      #endregion

      #region RaceEntity Mappings
      var race = modelBuilder.Entity<RaceEntity>();

      // Map properties inherited from BaseValueEntity<>
      race.Map(x => x.MapInheritedProperties());
      race.HasKey(x => x.Id);
      race.Property(x => x.Description).HasColumnName("description");
      race.Property(x => x.Id).HasColumnName("raceID");
      race.Property(x => x.Name).HasColumnName("raceName");
      #endregion

      #region UnitEntity Mappings
      var unit = modelBuilder.Entity<UnitEntity>();

      // Map properties inherited from BaseValueEntity<>
      unit.Map(x => x.MapInheritedProperties());
      unit.HasKey(x => x.Id);
      unit.Property(x => x.Description).HasColumnName("description");
      unit.Property(x => x.Id).HasColumnName("unitID");
      unit.Property(x => x.Name).HasColumnName("unitName");
      #endregion
    }
    #endregion

    #region IDbContext Members
    //******************************************************************************
    IDbSet<TEntity> IDbContext.Set<TEntity>() {
      return Set<TEntity>();
    }
    //******************************************************************************
    int IDbContext.SaveChanges() {
      int result = SaveChanges();
      Contract.Assume(result >= 0);
      return result;
    }
    #endregion
  }
}