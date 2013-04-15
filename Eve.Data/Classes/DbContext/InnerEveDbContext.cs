//-----------------------------------------------------------------------
// <copyright file="InnerEveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
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

  using Eve;
  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet;
  using FreeNet.Configuration;
  using FreeNet.Data.Entity;

  /// <summary>
  /// A <see cref="DbContext" /> that provides data access to the EVE database.
  /// This is the "real" <c>DbContext</c> for EVE-related entities -- i.e.,
  /// this is the class that actually interacts directly with the Entity
  /// Framework.  Because EVE data is read-only, there is no need to expose this
  /// class to the outside.  It serves primarily to define the entity model and
  /// provide access to the database for other classes in the library.  For most
  /// purposes, you should use the <see cref="EveDbContext" /> wrapper class
  /// instead.
  /// </summary>
  internal class InnerEveDbContext : DbContext
  {
    /* Constructors */

    /// <summary>
    /// Initializes static members of the <see cref="InnerEveDbContext" /> class.
    /// </summary>
    static InnerEveDbContext()
    {
      Database.SetInitializer<InnerEveDbContext>(null);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InnerEveDbContext" /> class using
    /// conventions to create the name of the database to which a connection will
    /// be made. By convention the name is the full name (namespace + class name)
    /// of the derived context class.  For more information on how this is used
    /// to create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    internal InnerEveDbContext() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InnerEveDbContext" /> class using
    /// the given string as the name or connection string for the database to which
    /// a connection will be made.  For more information on how this is used to
    /// create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    /// <param name="nameOrConnectionString">
    /// Either the database name or a connection string.
    /// </param>
    internal InnerEveDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InnerEveDbContext" /> class using
    /// the existing connection to connect to a database. The connection will not be
    /// disposed when the context is disposed.
    /// </summary>
    /// <param name="existingConnection">
    /// An existing connection to use for the new context.
    /// </param>
    /// <param name="contextOwnsConnection">
    /// If set to true the connection is disposed when the context is disposed,
    /// otherwise the caller must dispose the connection.
    /// </param>
    internal InnerEveDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
    {
    }

    /* Methods */

    /// <inheritdoc />
    /// <remarks>
    /// <para>
    /// Because the context is read-only and does not track changes, the
    /// <c>SaveChanges</c> method performs no actions and exists merely
    /// for compatibility.
    /// </para>
    /// </remarks>
    public override int SaveChanges()
    {
      return 0;
    }

    /// <summary>
    /// Returns an <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity for which to return a <see cref="IDbSet{TEntity}" />.
    /// </typeparam>
    /// <returns>
    /// An <see cref="IDbSet{TEntity}" /> for the specified entity type.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method simply returns the value of the base class's
    /// <see cref="DbContext.Set{TEntity}()" /> method as an
    /// <see cref="IDbSet{TEntity}" /> instead of a <see cref="DbSet{TEntity}" />
    /// object, for ease of testing and mocking.
    /// </para>
    /// </remarks>
    public new virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class
    {
      Contract.Ensures(Contract.Result<IDbSet<TEntity>>() != null);

      var result = base.Set<TEntity>();

      Contract.Assume(result != null);
      return result;
    }

    /// <inheritdoc />
    [ContractVerification(false)]
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      /* ActivityEntity Mappings *******************************************/
      var activity = modelBuilder.Entity<ActivityEntity>();

      // Map properties inherited from BaseValueEntity<>
      activity.Map(x => x.MapInheritedProperties());
      activity.HasKey(x => x.Id);
      activity.Property(x => x.Description).HasColumnName("description");
      activity.Property(x => x.Id).HasColumnName("activityID");
      activity.Property(x => x.Name).HasColumnName("activityName");

      /* AgentEntity Mappings *******************************************************/
      var agent = modelBuilder.Entity<AgentEntity>();

      // Map the ResearchFields collection
      agent.HasMany<EveTypeEntity>(x => x.ResearchFields)
           .WithMany()
           .Map(x => x.ToTable("agtResearchAgents")
                      .MapLeftKey("agentID")
                      .MapRightKey("typeID"));

      /* AgentTypeEntity Mappings ***************************************************/
      var agentType = modelBuilder.Entity<AgentTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      agentType.Map(x => x.MapInheritedProperties());
      agentType.HasKey(x => x.Id);
      agentType.Ignore(x => x.Description);
      agentType.Property(x => x.Id).HasColumnName("agentTypeID");
      agentType.Property(x => x.Name).HasColumnName("agentType");

      /* AncestryEntity Mappings *******************************************/
      var ancestry = modelBuilder.Entity<AncestryEntity>();

      // Map properties inherited from BaseValueEntity<>
      ancestry.Map(x => x.MapInheritedProperties());
      ancestry.HasKey(x => x.Id);
      ancestry.Property(x => x.Description).HasColumnName("description");
      ancestry.Property(x => x.Id).HasColumnName("ancestryID");
      ancestry.Property(x => x.Name).HasColumnName("ancestryName");

      /* AssemblyLineTypeEntity Mappings *******************************************/
      var assemblyLineType = modelBuilder.Entity<AssemblyLineTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      assemblyLineType.Map(x => x.MapInheritedProperties());
      assemblyLineType.HasKey(x => x.Id);
      assemblyLineType.Property(x => x.Description).HasColumnName("description");
      assemblyLineType.Property(x => x.Id).HasColumnName("assemblyLineTypeID");
      assemblyLineType.Property(x => x.Name).HasColumnName("assemblyLineTypeName");

      // Map the details collections
      assemblyLineType.HasMany(x => x.CategoryDetails).WithRequired(x => x.AssemblyLineType);
      assemblyLineType.HasMany(x => x.GroupDetails).WithRequired(x => x.AssemblyLineType);

      /* AssemblyLineTypeCategoryDetailEntity Mappings ******************************/

      // All mappings defined by Data Annotations
      modelBuilder.Entity<AssemblyLineTypeCategoryDetailEntity>(); // Needed to allow the model to pick up the entity type for some reason

      /* AssemblyLineTypeGroupDetailEntity Mappings *********************************/

      // All mappings defined by Data Annotations
      modelBuilder.Entity<AssemblyLineTypeGroupDetailEntity>(); // Needed to allow the model to pick up the entity type for some reason

      /* AttributeTypeEntity Mappings ***********************************************/
      var attributeType = modelBuilder.Entity<AttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeType.Map(x => x.MapInheritedProperties());
      attributeType.HasKey(x => x.Id);
      attributeType.Property(x => x.Description).HasColumnName("description");
      attributeType.Property(x => x.Id).HasColumnName("attributeID");
      attributeType.Property(x => x.Name).HasColumnName("attributeName");

      /* AttributeValueEntity Mappings **********************************************/

      // All mappings defined by Data Annotations

      /* AttributeCategoryEntity Mappings *******************************************/
      var attributeCategory = modelBuilder.Entity<AttributeCategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeCategory.Map(x => x.MapInheritedProperties());
      attributeCategory.HasKey(x => x.Id);
      attributeCategory.Property(x => x.Description).HasColumnName("categoryDescription");
      attributeCategory.Property(x => x.Id).HasColumnName("categoryID");
      attributeCategory.Property(x => x.Name).HasColumnName("categoryName");

      /* BloodlineEntity Mappings *******************************************/
      var bloodline = modelBuilder.Entity<BloodlineEntity>();

      // Map properties inherited from BaseValueEntity<>
      bloodline.Map(x => x.MapInheritedProperties());
      bloodline.HasKey(x => x.Id);
      bloodline.Property(x => x.Description).HasColumnName("description");
      bloodline.Property(x => x.Id).HasColumnName("bloodlineID");
      bloodline.Property(x => x.Name).HasColumnName("bloodlineName");

      /* CategoryEntity Mappings *******************************************/
      var category = modelBuilder.Entity<CategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      category.Map(x => x.MapInheritedProperties());
      category.HasKey(x => x.Id);
      category.Property(x => x.Description).HasColumnName("description");
      category.Property(x => x.Id).HasColumnName("categoryID");
      category.Property(x => x.Name).HasColumnName("categoryName");

      /* CharacterAttributeTypeEntity Mappings **************************************/
      var characterAttributeType = modelBuilder.Entity<CharacterAttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      characterAttributeType.Map(x => x.MapInheritedProperties());
      characterAttributeType.HasKey(x => x.Id);
      characterAttributeType.Property(x => x.Description).HasColumnName("description");
      characterAttributeType.Property(x => x.Id).HasColumnName("attributeID");
      characterAttributeType.Property(x => x.Name).HasColumnName("attributeName");

      /* ConstellationEntity Mappings ***********************************************/
      var constellation = modelBuilder.Entity<ConstellationEntity>();

      // Map the Jumps collection
      constellation.HasMany(x => x.Jumps).WithRequired(x => x.FromConstellation).HasForeignKey(x => x.FromConstellationId);

      /* ConstellationJumpEntity Mappings *******************************************/

      // All mappings defined by Data Annotations

      /* CorporateActivityEntity Mappings *******************************************/
      var corporateActivity = modelBuilder.Entity<CorporateActivityEntity>();

      // Map properties inherited from BaseValueEntity<>
      corporateActivity.Map(x => x.MapInheritedProperties());
      corporateActivity.HasKey(x => x.Id);
      corporateActivity.Property(x => x.Description).HasColumnName("description");
      corporateActivity.Property(x => x.Id).HasColumnName("activityID");
      corporateActivity.Property(x => x.Name).HasColumnName("activityName");

      /* DivisionEntity Mappings ****************************************************/
      var division = modelBuilder.Entity<DivisionEntity>();

      // Map properties inherited from BaseValueEntity<>
      division.Map(x => x.MapInheritedProperties());
      division.HasKey(x => x.Id);
      division.Property(x => x.Description).HasColumnName("description");
      division.Property(x => x.Id).HasColumnName("divisionID");
      division.Property(x => x.Name).HasColumnName("divisionName");

      /* EffectEntity Mappings ******************************************************/

      // All mappings defined by Data Annotations

      /* EffectTypeEntity Mappings **************************************************/
      var effectType = modelBuilder.Entity<EffectTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      effectType.Map(x => x.MapInheritedProperties());
      effectType.HasKey(x => x.Id);
      effectType.Property(x => x.Description).HasColumnName("description");
      effectType.Property(x => x.Id).HasColumnName("effectID");
      effectType.Property(x => x.Name).HasColumnName("effectName");

      /* EveTypeEntity Mappings *****************************************************/
      var type = modelBuilder.Entity<EveTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      type.Map(x => x.MapInheritedProperties());
      type.HasKey(x => x.Id);
      type.Property(x => x.Description).HasColumnName("description");
      type.Property(x => x.Id).HasColumnName("typeID");
      type.Property(x => x.Name).HasColumnName("typeName");

      // Map the Attributes collection
      type.HasMany(x => x.Attributes).WithRequired().HasForeignKey(x => x.ItemTypeId);

      // Map the Effects collection
      type.HasMany(x => x.Effects).WithRequired().HasForeignKey(x => x.ItemTypeId);

      // Map the MetaType property
      type.HasOptional(x => x.MetaType).WithRequired(x => x.Type);

      // Map the VariantMetaTypes collection
      type.HasMany(x => x.ChildMetaTypes).WithRequired().HasForeignKey(x => x.ParentTypeId);

      /* FactionEntity Mappings *****************************************************/
      var faction = modelBuilder.Entity<FactionEntity>();

      // Map properties inherited from BaseValueEntity<>
      faction.Map(x => x.MapInheritedProperties());
      faction.HasKey(x => x.Id);
      faction.Property(x => x.Description).HasColumnName("description");
      faction.Property(x => x.Id).HasColumnName("factionID");
      faction.Property(x => x.Name).HasColumnName("factionName");

      faction.HasRequired(x => x.SolarSystem).WithMany().HasForeignKey(x => x.SolarSystemId);

      /* FlagEntity Mappings ********************************************************/
      var flag = modelBuilder.Entity<FlagEntity>();

      // Map properties inherited from BaseValueEntity<>
      flag.Map(x => x.MapInheritedProperties());
      flag.HasKey(x => x.Id);
      flag.Property(x => x.Description).HasColumnName("flagText");
      flag.Property(x => x.Id).HasColumnName("flagID");
      flag.Property(x => x.Name).HasColumnName("flagName");

      /* GraphicEntity Mappings ********************************************************/

      // All mappings defined by Data Annotations

      /* GroupEntity Mappings *******************************************************/
      var group = modelBuilder.Entity<GroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      group.Map(x => x.MapInheritedProperties());
      group.HasKey(x => x.Id);
      group.Property(x => x.Description).HasColumnName("description");
      group.Property(x => x.Id).HasColumnName("groupID");
      group.Property(x => x.Name).HasColumnName("groupName");

      // Map the Types collection
      group.HasMany(x => x.Types).WithRequired(x => x.Group).HasForeignKey(x => x.GroupId);

      /* IconEntity Mappings ********************************************************/
      var icon = modelBuilder.Entity<IconEntity>();

      // Map properties inherited from BaseValueEntity<>
      icon.Map(x => x.MapInheritedProperties());
      icon.HasKey(x => x.Id);
      icon.Property(x => x.Description).HasColumnName("description");
      icon.Property(x => x.Id).HasColumnName("iconID");
      icon.Property(x => x.Name).HasColumnName("iconFile");

      /* ItemEntity Mappings ********************************************************/
      var item = modelBuilder.Entity<ItemEntity>();

      item.HasRequired(x => x.Location).WithMany().HasForeignKey(x => x.LocationId);
      item.HasRequired(x => x.Owner).WithMany().HasForeignKey(x => x.OwnerId);
      item.HasOptional(x => x.Position).WithRequired(x => x.Item);
      
      // Map the Name property to the invNames table
      item.Map(m => 
      {
        m.Properties(x => x.Name);
        m.ToTable("invNames");
      });

      // Map all other properties to the invTypes table
      item.Map(m =>
      {
        m.Properties(x => new 
        {
          x.FlagId,
          x.ItemTypeId,
          x.LocationId,
          x.OwnerId,
          x.Quantity
        });

        m.ToTable("invItems");
      });

      /* ItemPositionEntity Mappings ************************************************/

      // All mappings defined by Data Annotations
      var itemPosition = modelBuilder.Entity<ItemPositionEntity>();

      /* MarketGroupEntity Mappings *************************************************/
      var marketGroup = modelBuilder.Entity<MarketGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      marketGroup.Map(x => x.MapInheritedProperties());
      marketGroup.HasKey(x => x.Id);
      marketGroup.Property(x => x.Description).HasColumnName("description");
      marketGroup.Property(x => x.Id).HasColumnName("marketGroupID");
      marketGroup.Property(x => x.Name).HasColumnName("marketGroupName");

      // Map the ChildGroups collection
      marketGroup.HasMany(x => x.ChildGroups).WithOptional(x => x.ParentGroup).HasForeignKey(x => x.ParentGroupId);

      // Map the Types collection
      marketGroup.HasMany(x => x.Types).WithRequired(x => x.MarketGroup).HasForeignKey(x => x.MarketGroupId);

      /* MetaGroupEntity Mappings ***************************************************/
      var metaGroup = modelBuilder.Entity<MetaGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      metaGroup.Map(x => x.MapInheritedProperties());
      metaGroup.HasKey(x => x.Id);
      metaGroup.Property(x => x.Description).HasColumnName("description");
      metaGroup.Property(x => x.Id).HasColumnName("metaGroupID");
      metaGroup.Property(x => x.Name).HasColumnName("metaGroupName");

      /* MetaTypeEntity Mappings ****************************************************/

      // All mappings defined by Data Annotations

      /* NpcCorporationEntity Mappings **********************************************/
      var corporation = modelBuilder.Entity<NpcCorporationEntity>();

      // Map the Agents collection
      corporation.HasMany(x => x.Agents)
                 .WithRequired(x => x.Corporation)
                 .HasForeignKey(x => x.CorporationId);

      // Map the TradeGoods collection
      corporation.HasMany<EveTypeEntity>(x => x.TradeGoods)
                 .WithMany().Map(x => x.ToTable("crpNPCCorporationTrades")
                            .MapLeftKey("corporationID")
                            .MapRightKey("typeID"));

      // Map the ResearchFields collection
      corporation.HasMany<EveTypeEntity>(x => x.ResearchFields)
                 .WithMany().Map(x => x.ToTable("crpNPCCorporationResearchFields")
                 .MapLeftKey("corporationID")
                 .MapRightKey("skillID"));

      /* NpcCorporationDivisionEntity Mappings **************************************/
      var corporationDivision = modelBuilder.Entity<NpcCorporationDivisionEntity>();

      // Map the Agents collection
      corporationDivision.HasMany(x => x.Agents)
                         .WithRequired()
                         .HasForeignKey(x => new { x.CorporationId, x.DivisionId });

      /* RaceEntity Mappings ********************************************************/
      var race = modelBuilder.Entity<RaceEntity>();

      // Map properties inherited from BaseValueEntity<>
      race.Map(x => x.MapInheritedProperties());
      race.HasKey(x => x.Id);
      race.Property(x => x.Description).HasColumnName("description");
      race.Property(x => x.Id).HasColumnName("raceID");
      race.Property(x => x.Name).HasColumnName("raceName");

      /* RegionEntity Mappings ******************************************************/
      var region = modelBuilder.Entity<RegionEntity>();

      // Map the Jumps collection
      region.HasMany(x => x.Jumps).WithRequired(x => x.FromRegion).HasForeignKey(x => x.FromRegionId);

      /* RegionJumpEntity Mappings **************************************************/

      // All mappings defined by Data Annotations

      /* SolarSystemEntity Mappings *************************************************/
      var solarSystem = modelBuilder.Entity<SolarSystemEntity>();

      solarSystem.HasRequired(x => x.Constellation).WithMany().HasForeignKey(x => x.ConstellationId);
      solarSystem.HasOptional(x => x.Faction).WithMany().HasForeignKey(x => x.FactionId);

      // Map the Jumps collection
      solarSystem.HasMany(x => x.Jumps).WithRequired(x => x.FromSolarSystem).HasForeignKey(x => x.FromSolarSystemId);

      /* SolarSystemJumpEntity Mappings *********************************************/

      // All mappings defined by Data Annotations

      /* StationOperationEntity Mappings ********************************************/
      var stationOperation = modelBuilder.Entity<StationOperationEntity>();

      // Map properties inherited from BaseValueEntity<>
      stationOperation.Map(x => x.MapInheritedProperties());
      stationOperation.HasKey(x => x.Id);
      stationOperation.Property(x => x.Description).HasColumnName("description");
      stationOperation.Property(x => x.Id).HasColumnName("operationID");
      stationOperation.Property(x => x.Name).HasColumnName("operationName");

      // Map the Services collection
      stationOperation.HasMany(x => x.Services).WithMany().Map(x => x.ToTable("staOperationServices")
                                                                     .MapLeftKey("operationID")
                                                                     .MapRightKey("serviceID"));

      /* StationServiceEntity Mappings **********************************************/
      var stationService = modelBuilder.Entity<StationServiceEntity>();

      // Map properties inherited from BaseValueEntity<>
      stationService.Map(x => x.MapInheritedProperties());
      stationService.HasKey(x => x.Id);
      stationService.Property(x => x.Description).HasColumnName("description");
      stationService.Property(x => x.Id).HasColumnName("serviceID");
      stationService.Property(x => x.Name).HasColumnName("serviceName");

      /* StationTypeEntity Mappings *************************************************/

      // All mappings defined by Data Annotations

      /* UnitEntity Mappings ********************************************************/
      var unit = modelBuilder.Entity<UnitEntity>();

      // Map properties inherited from BaseValueEntity<>
      unit.Map(x => x.MapInheritedProperties());
      unit.HasKey(x => x.Id);
      unit.Property(x => x.Description).HasColumnName("description");
      unit.Property(x => x.Id).HasColumnName("unitID");
      unit.Property(x => x.Name).HasColumnName("unitName");

      /* UniverseEntity Mappings ****************************************************/

      // All mappings defined by Data Annotations
    }
  }
}