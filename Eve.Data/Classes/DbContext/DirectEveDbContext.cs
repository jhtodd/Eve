//-----------------------------------------------------------------------
// <copyright file="DirectEveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System.Data.Common;
  using System.Data.Entity;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;

  using Eve.Data.Entities;
  using Eve.Universe;

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
  internal class DirectEveDbContext : DbContext
  {
    /* Constructors */

    /// <summary>
    /// Initializes static members of the <see cref="DirectEveDbContext" /> class.
    /// </summary>
    static DirectEveDbContext()
    {
      Database.SetInitializer<DirectEveDbContext>(null);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectEveDbContext" /> class using
    /// conventions to create the name of the database to which a connection will
    /// be made. By convention the name is the full name (namespace + class name)
    /// of the derived context class.  For more information on how this is used
    /// to create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    internal DirectEveDbContext() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectEveDbContext" /> class using
    /// the given string as the name or connection string for the database to which
    /// a connection will be made.  For more information on how this is used to
    /// create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    /// <param name="nameOrConnectionString">
    /// Either the database name or a connection string.
    /// </param>
    internal DirectEveDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectEveDbContext" /> class using
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
    internal DirectEveDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
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
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "The many sections of code in the model creation method are more readable in regions.")]
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      #region ActivityEntity Mappings
      var activity = modelBuilder.Entity<ActivityEntity>();

      // Map properties inherited from BaseValueEntity<>
      activity.Map(m => m.MapInheritedProperties());
      activity.HasKey(a => a.Id);
      activity.Property(a => a.Description).HasColumnName("description");
      activity.Property(a => a.Id).HasColumnName("activityID");
      activity.Property(a => a.Name).HasColumnName("activityName");
      #endregion

      #region AgenEntity Mappings
      var agent = modelBuilder.Entity<AgentEntity>();

      agent.Map(m => m.MapInheritedProperties());
      agent.HasKey(a => a.Id);
      agent.Property(a => a.Id).HasColumnName("agentID");

      // Map the ResearchFields collection
      agent.HasMany<EveTypeEntity>(a => a.ResearchFields)
           .WithMany()
           .Map(m => m.ToTable("agtResearchAgents")
                      .MapLeftKey("agentID")
                      .MapRightKey("typeID"));
      #endregion

      #region AgentTypeEntity Mappings
      var agentType = modelBuilder.Entity<AgentTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      agentType.Map(m => m.MapInheritedProperties());
      agentType.HasKey(at => at.Id);
      agentType.Ignore(at => at.Description);
      agentType.Property(at => at.Id).HasColumnName("agentTypeID");
      agentType.Property(at => at.Name).HasColumnName("agentType");
      #endregion

      #region AncestryEntity Mappings
      var ancestry = modelBuilder.Entity<AncestryEntity>();

      // Map properties inherited from BaseValueEntity<>
      ancestry.Map(m => m.MapInheritedProperties());
      ancestry.HasKey(a => a.Id);
      ancestry.Property(a => a.Description).HasColumnName("description");
      ancestry.Property(a => a.Id).HasColumnName("ancestryID");
      ancestry.Property(a => a.Name).HasColumnName("ancestryName");
      #endregion

      #region AssemblyLineEntity Mappings
      var assemblyLine = modelBuilder.Entity<AssemblyLineEntity>();
      #endregion

      #region AssemblyLineStationEntity Mappings
      var assemblyLineStation = modelBuilder.Entity<AssemblyLineStationEntity>();
      #endregion

      #region AssemblyLineTypeEntity Mappings
      var assemblyLineType = modelBuilder.Entity<AssemblyLineTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      assemblyLineType.Map(m => m.MapInheritedProperties());
      assemblyLineType.HasKey(alt => alt.Id);
      assemblyLineType.Property(alt => alt.Description).HasColumnName("description");
      assemblyLineType.Property(alt => alt.Id).HasColumnName("assemblyLineTypeID");
      assemblyLineType.Property(alt => alt.Name).HasColumnName("assemblyLineTypeName");

      // Map the details collections
      assemblyLineType.HasMany(alt => alt.CategoryDetails).WithRequired(altcd => altcd.AssemblyLineType);
      assemblyLineType.HasMany(alt => alt.GroupDetails).WithRequired(altgd => altgd.AssemblyLineType);
      #endregion

      #region AssemblyLineTypeCategoryDetailEntity Mappings
      modelBuilder.Entity<AssemblyLineTypeCategoryDetailEntity>(); // Needed to allow the model to pick up the entity type for some reason
      #endregion

      #region AssemblyLineTypeGroupDetailEntity Mappings
      modelBuilder.Entity<AssemblyLineTypeGroupDetailEntity>(); // Needed to allow the model to pick up the entity type for some reason
      #endregion

      #region AttributeTypeEntity Mappings
      var attributeType = modelBuilder.Entity<AttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeType.Map(m => m.MapInheritedProperties());
      attributeType.HasKey(at => at.Id);
      attributeType.Property(at => at.Description).HasColumnName("description");
      attributeType.Property(at => at.Id).HasColumnName("attributeID");
      attributeType.Property(at => at.Name).HasColumnName("attributeName");
      #endregion

      #region AttributeValueEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region AttributeCategoryEntity Mappings
      var attributeCategory = modelBuilder.Entity<AttributeCategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      attributeCategory.Map(m => m.MapInheritedProperties());
      attributeCategory.HasKey(ac => ac.Id);
      attributeCategory.Property(ac => ac.Description).HasColumnName("categoryDescription");
      attributeCategory.Property(ac => ac.Id).HasColumnName("categoryID");
      attributeCategory.Property(ac => ac.Name).HasColumnName("categoryName");
      #endregion

      #region BloodlineEntity Mappings
      var bloodline = modelBuilder.Entity<BloodlineEntity>();

      // Map properties inherited from BaseValueEntity<>
      bloodline.Map(m => m.MapInheritedProperties());
      bloodline.HasKey(b => b.Id);
      bloodline.Property(b => b.Description).HasColumnName("description");
      bloodline.Property(b => b.Id).HasColumnName("bloodlineID");
      bloodline.Property(b => b.Name).HasColumnName("bloodlineName");
      #endregion

      #region CategoryEntity Mappings
      var category = modelBuilder.Entity<CategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      category.Map(m => m.MapInheritedProperties());
      category.HasKey(c => c.Id);
      category.Property(c => c.Description).HasColumnName("description");
      category.Property(c => c.Id).HasColumnName("categoryID");
      category.Property(c => c.Name).HasColumnName("categoryName");
      #endregion

      #region CelestialEntity Mappings
      var celestial = modelBuilder.Entity<CelestialEntity>();

      celestial.Map(m => m.MapInheritedProperties());
      celestial.HasKey(c => c.Id);
      celestial.Property(c => c.Id).HasColumnName("celestialID");
      #endregion

      #region CertificateEntity Mappings
      var certificate = modelBuilder.Entity<CertificateEntity>();
      #endregion

      #region CertificateCategoryEntity Mappings
      var certificateCategory = modelBuilder.Entity<CertificateCategoryEntity>();

      // Map properties inherited from BaseValueEntity<>
      certificateCategory.Map(m => m.MapInheritedProperties());
      certificateCategory.HasKey(cc => cc.Id);
      certificateCategory.Property(cc => cc.Description).HasColumnName("description");
      certificateCategory.Property(cc => cc.Id).HasColumnName("categoryID");
      certificateCategory.Property(cc => cc.Name).HasColumnName("categoryName");
      #endregion

      #region CertificateClassEntity Mappings
      var certificateClass = modelBuilder.Entity<CertificateClassEntity>();

      // Map properties inherited from BaseValueEntity<>
      certificateClass.Map(m => m.MapInheritedProperties());
      certificateClass.HasKey(cc => cc.Id);
      certificateClass.Property(cc => cc.Description).HasColumnName("description");
      certificateClass.Property(cc => cc.Id).HasColumnName("classID");
      certificateClass.Property(cc => cc.Name).HasColumnName("className");
      #endregion

      #region CertificateRecommendationEntity Mappings
      var certificateRecommendation = modelBuilder.Entity<CertificateRecommendationEntity>();
      #endregion

      #region CertificateRelationshipEntity Mappings
      var certificateRelationship = modelBuilder.Entity<CertificateRelationshipEntity>();
      #endregion

      #region CharacterAttributeTypeEntity Mappings
      var characterAttributeType = modelBuilder.Entity<CharacterAttributeTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      characterAttributeType.Map(m => m.MapInheritedProperties());
      characterAttributeType.HasKey(cat => cat.Id);
      characterAttributeType.Property(cat => cat.Description).HasColumnName("description");
      characterAttributeType.Property(cat => cat.Id).HasColumnName("attributeID");
      characterAttributeType.Property(cat => cat.Name).HasColumnName("attributeName");
      #endregion

      #region ConstellationEntity Mappings
      var constellation = modelBuilder.Entity<ConstellationEntity>();

      constellation.Map(m => m.MapInheritedProperties());
      constellation.HasKey(c => c.Id);
      constellation.Property(c => c.Id).HasColumnName("constellationID");

      // Map the Jumps collection
      constellation.HasMany(c => c.Jumps).WithRequired().HasForeignKey(cj => cj.FromConstellationId);
      #endregion

      #region ConstellationJumpEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region ContrabandInfoEntity Mappings
      var contrabandInfo = modelBuilder.Entity<ContrabandInfoEntity>();
      #endregion

      #region CorporateActivityEntity Mappings
      var corporateActivity = modelBuilder.Entity<CorporateActivityEntity>();

      // Map properties inherited from BaseValueEntity<>
      corporateActivity.Map(m => m.MapInheritedProperties());
      corporateActivity.HasKey(ca => ca.Id);
      corporateActivity.Property(ca => ca.Description).HasColumnName("description");
      corporateActivity.Property(ca => ca.Id).HasColumnName("activityID");
      corporateActivity.Property(ca => ca.Name).HasColumnName("activityName");
      #endregion

      #region DivisionEntity Mappings
      var division = modelBuilder.Entity<DivisionEntity>();

      // Map properties inherited from BaseValueEntity<>
      division.Map(m => m.MapInheritedProperties());
      division.HasKey(d => d.Id);
      division.Property(d => d.Description).HasColumnName("description");
      division.Property(d => d.Id).HasColumnName("divisionID");
      division.Property(d => d.Name).HasColumnName("divisionName");
      #endregion

      #region EffectEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region EffectTypeEntity Mappings
      var effectType = modelBuilder.Entity<EffectTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      effectType.Map(m => m.MapInheritedProperties());
      effectType.HasKey(et => et.Id);
      effectType.Property(et => et.Description).HasColumnName("description");
      effectType.Property(et => et.Id).HasColumnName("effectID");
      effectType.Property(et => et.Name).HasColumnName("effectName");
      #endregion

      #region EveTypeEntity Mappings
      var eveType = modelBuilder.Entity<EveTypeEntity>();

      // Map properties inherited from BaseValueEntity<>
      eveType.Map(m => m.MapInheritedProperties());
      eveType.HasKey(et => et.Id);
      eveType.Property(et => et.Description).HasColumnName("description");
      eveType.Property(et => et.Id).HasColumnName("typeID");
      eveType.Property(et => et.Name).HasColumnName("typeName");

      // Map the Attributes collection
      eveType.HasMany(et => et.Attributes).WithRequired().HasForeignKey(a => a.ItemTypeId);

      // Map the Effects collection
      eveType.HasMany(et => et.Effects).WithRequired().HasForeignKey(e => e.ItemTypeId);

      // Map the MetaType property
      eveType.HasOptional(et => et.MetaType).WithRequired(mt => mt.Type);

      // Map the VariantMetaTypes collection
      eveType.HasMany(et => et.ChildMetaTypes).WithRequired().HasForeignKey(mt => mt.ParentTypeId);
      #endregion

      #region FactionEntity Mappings
      var faction = modelBuilder.Entity<FactionEntity>();
      faction.Map(m => m.MapInheritedProperties());
      faction.HasKey(f => f.Id);
      faction.Property(f => f.Id).HasColumnName("factionID");

      faction.HasRequired(f => f.SolarSystem).WithMany().HasForeignKey(x => x.SolarSystemId);
      #endregion

      #region FlagEntity Mappings
      var flag = modelBuilder.Entity<FlagEntity>();

      // Map properties inherited from BaseValueEntity<>
      flag.Map(m => m.MapInheritedProperties());
      flag.HasKey(f => f.Id);
      flag.Property(f => f.Description).HasColumnName("flagText");
      flag.Property(f => f.Id).HasColumnName("flagID");
      flag.Property(f => f.Name).HasColumnName("flagName");
      #endregion

      #region GraphicEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region GroupEntity Mappings
      var group = modelBuilder.Entity<GroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      group.Map(m => m.MapInheritedProperties());
      group.HasKey(g => g.Id);
      group.Property(g => g.Description).HasColumnName("description");
      group.Property(g => g.Id).HasColumnName("groupID");
      group.Property(g => g.Name).HasColumnName("groupName");

      // Map the Types collection
      group.HasMany(g => g.Types).WithRequired(et => et.Group).HasForeignKey(et => et.GroupId);
      #endregion

      #region IconEntity Mappings
      var icon = modelBuilder.Entity<IconEntity>();

      // Map properties inherited from BaseValueEntity<>
      icon.Map(m => m.MapInheritedProperties());
      icon.HasKey(i => i.Id);
      icon.Property(i => i.Description).HasColumnName("description");
      icon.Property(i => i.Id).HasColumnName("iconID");
      icon.Property(i => i.Name).HasColumnName("iconFile");
      #endregion

      #region ItemEntity Mappings
      var item = modelBuilder.Entity<ItemEntity>();

      // Extension entities
      item.HasOptional(i => i.AgentInfo).WithRequired(a => a.ItemInfo);
      item.HasOptional(i => i.CelestialInfo).WithRequired(c => c.ItemInfo);
      item.HasOptional(i => i.ConstellationInfo).WithRequired(c => c.ItemInfo);
      item.HasOptional(i => i.CorporationInfo).WithRequired(c => c.ItemInfo);
      item.HasOptional(i => i.FactionInfo).WithRequired(f => f.ItemInfo);
      item.HasOptional(i => i.RegionInfo).WithRequired(r => r.ItemInfo);
      item.HasOptional(i => i.SolarSystemInfo).WithRequired(s => s.ItemInfo);
      item.HasOptional(i => i.StargateInfo).WithRequired(s => s.ItemInfo);
      item.HasOptional(i => i.StationInfo).WithRequired(s => s.ItemInfo);
      item.HasOptional(i => i.UniverseInfo).WithRequired(u => u.ItemInfo);

      item.HasOptional(i => i.Name).WithRequired();

      item.HasRequired(i => i.Location).WithMany().HasForeignKey(i => i.LocationId);
      item.HasRequired(i => i.Owner).WithMany().HasForeignKey(i => i.OwnerId);
      item.HasOptional(i => i.Position).WithRequired(i => i.Item);
      #endregion

      #region ItemNameEntity Mappings
      var itemName = modelBuilder.Entity<ItemNameEntity>();
      #endregion

      #region ItemPositionEntity Mappings
      var itemPosition = modelBuilder.Entity<ItemPositionEntity>();
      #endregion

      #region MarketGroupEntity Mappings
      var marketGroup = modelBuilder.Entity<MarketGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      marketGroup.Map(m => m.MapInheritedProperties());
      marketGroup.HasKey(mg => mg.Id);
      marketGroup.Property(mg => mg.Description).HasColumnName("description");
      marketGroup.Property(mg => mg.Id).HasColumnName("marketGroupID");
      marketGroup.Property(mg => mg.Name).HasColumnName("marketGroupName");

      // Map the ChildGroups collection
      marketGroup.HasMany(mg => mg.ChildGroups).WithOptional(mg => mg.ParentGroup).HasForeignKey(mg => mg.ParentGroupId);

      // Map the Types collection
      marketGroup.HasMany(mg => mg.Types).WithRequired(mg => mg.MarketGroup).HasForeignKey(mg => mg.MarketGroupId);
      #endregion

      #region MetaGroupEntity Mappings
      var metaGroup = modelBuilder.Entity<MetaGroupEntity>();

      // Map properties inherited from BaseValueEntity<>
      metaGroup.Map(m => m.MapInheritedProperties());
      metaGroup.HasKey(mg => mg.Id);
      metaGroup.Property(mg => mg.Description).HasColumnName("description");
      metaGroup.Property(mg => mg.Id).HasColumnName("metaGroupID");
      metaGroup.Property(mg => mg.Name).HasColumnName("metaGroupName");
      #endregion

      #region MetaTypeEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region NpcCorporationEntity Mappings
      var npcCorporation = modelBuilder.Entity<NpcCorporationEntity>();

      npcCorporation.Map(m => m.MapInheritedProperties());
      npcCorporation.HasKey(c => c.Id);
      npcCorporation.Property(c => c.Id).HasColumnName("corporationID");

      // Map the Agents collection
      npcCorporation.HasMany(c => c.Agents).WithRequired(a => a.Corporation).HasForeignKey(a => a.CorporationId);

      // Map the TradeGoods collection
      npcCorporation.HasMany<EveTypeEntity>(c => c.TradeGoods)
                    .WithMany().Map(m => m.ToTable("crpNPCCorporationTrades")
                               .MapLeftKey("corporationID")
                               .MapRightKey("typeID"));

      // Map the ResearchFields collection
      npcCorporation.HasMany<EveTypeEntity>(c => c.ResearchFields)
                    .WithMany().Map(x => x.ToTable("crpNPCCorporationResearchFields")
                    .MapLeftKey("corporationID")
                    .MapRightKey("skillID"));
      #endregion

      #region NpcCorporationDivisionEntity Mappings
      var corporationDivision = modelBuilder.Entity<NpcCorporationDivisionEntity>();

      // Map the Agents collection
      corporationDivision.HasMany(cd => cd.Agents)
                         .WithRequired()
                         .HasForeignKey(a => new { a.CorporationId, a.DivisionId });
      #endregion

      #region RaceEntity Mappings
      var race = modelBuilder.Entity<RaceEntity>();

      // Map properties inherited from BaseValueEntity<>
      race.Map(m => m.MapInheritedProperties());
      race.HasKey(r => r.Id);
      race.Property(r => r.Description).HasColumnName("description");
      race.Property(r => r.Id).HasColumnName("raceID");
      race.Property(r => r.Name).HasColumnName("raceName");
      #endregion

      #region RegionEntity Mappings
      var region = modelBuilder.Entity<RegionEntity>();

      region.Map(m => m.MapInheritedProperties());
      region.HasKey(r => r.Id);
      region.Property(r => r.Id).HasColumnName("regionID");

      // Map the Jumps collection
      region.HasMany(r => r.Jumps).WithRequired().HasForeignKey(rj => rj.FromRegionId);
      #endregion

      #region RegionJumpEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region SolarSystemEntity Mappings
      var solarSystem = modelBuilder.Entity<SolarSystemEntity>();

      solarSystem.Map(m => m.MapInheritedProperties());
      solarSystem.HasKey(s => s.Id);
      solarSystem.Property(s => s.Id).HasColumnName("solarSystemID");

      solarSystem.HasRequired(s => s.Constellation).WithMany().HasForeignKey(s => s.ConstellationId);
      solarSystem.HasOptional(s => s.Faction).WithMany().HasForeignKey(s => s.FactionId);
      solarSystem.HasRequired(s => s.Region).WithMany().HasForeignKey(s => s.RegionId);

      // Map the Jumps collection
      solarSystem.HasMany(s => s.Jumps).WithRequired().HasForeignKey(sj => sj.FromSolarSystemId);
      #endregion

      #region SolarSystemJumpEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region StargateEntity Mappings
      var stargate = modelBuilder.Entity<StargateEntity>();

      stargate.Map(m => m.MapInheritedProperties());
      stargate.HasKey(s => s.Id);
      stargate.Property(s => s.Id).HasColumnName("stargateID");
      #endregion

      #region StationEntity Mappings
      var station = modelBuilder.Entity<StationEntity>();

      station.Map(m => m.MapInheritedProperties());
      station.HasKey(s => s.Id);
      station.Property(s => s.Id).HasColumnName("stationID");

      station.HasRequired(s => s.SolarSystem).WithMany().HasForeignKey(s => s.SolarSystemId);
      #endregion

      #region StationOperationEntity Mappings
      var stationOperation = modelBuilder.Entity<StationOperationEntity>();

      // Map properties inherited from BaseValueEntity<>
      stationOperation.Map(m => m.MapInheritedProperties());
      stationOperation.HasKey(so => so.Id);
      stationOperation.Property(so => so.Description).HasColumnName("description");
      stationOperation.Property(so => so.Id).HasColumnName("operationID");
      stationOperation.Property(so => so.Name).HasColumnName("operationName");

      // Map the Services collection
      stationOperation.HasMany(so => so.Services).WithMany().Map(x => x.ToTable("staOperationServices")
                                                                       .MapLeftKey("operationID")
                                                                       .MapRightKey("serviceID"));
      #endregion

      #region StationServiceEntity Mappings
      var stationService = modelBuilder.Entity<StationServiceEntity>();

      // Map properties inherited from BaseValueEntity<>
      stationService.Map(m => m.MapInheritedProperties());
      stationService.HasKey(ss => ss.Id);
      stationService.Property(ss => ss.Description).HasColumnName("description");
      stationService.Property(ss => ss.Id).HasColumnName("serviceID");
      stationService.Property(ss => ss.Name).HasColumnName("serviceName");
      #endregion

      #region StationTypeEntity Mappings
      // All mappings defined by Data Annotations
      #endregion

      #region UnitEntity Mappings
      var unit = modelBuilder.Entity<UnitEntity>();

      unit.Map(m => m.MapInheritedProperties());
      unit.HasKey(u => u.Id);
      unit.Property(u => u.Description).HasColumnName("description");
      unit.Property(u => u.Id).HasColumnName("unitID");
      unit.Property(u => u.Name).HasColumnName("unitName");
      #endregion

      #region UniverseEntity Mappings
      var universe = modelBuilder.Entity<UniverseEntity>();

      universe.Map(m => m.MapInheritedProperties());
      universe.HasKey(s => s.Id);
      universe.Property(s => s.Id).HasColumnName("universeID");
      #endregion
    }
  }
}