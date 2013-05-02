//-----------------------------------------------------------------------
// <copyright file="EveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System.Data.Common;
  using System.Data.Entity;
  using System.Data.Entity.Infrastructure;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;
  using Eve.Universe;

  using FreeNet.Data.Entity;

  /// <summary>
  /// A <see cref="DbContext" />-style object that provides low-level data
  /// access to the EVE database. This access is read-only.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Because it only needs to provide read access, the <c>EveDbContext</c>
  /// class is not an actual Entity Framework <see cref="DbContext" />, but
  /// a custom object that provides similar functionality while obscuring
  /// some <see cref="DbContext" /> methods that are unnecessary or even
  /// harmful in a read-only environment.
  /// </para>
  /// <para>
  /// Instead of a <c>Set&lt;TEntity&gt;</c> method, it provides a
  /// <see cref="Query{TEntity}" /> method that returns an
  /// <see cref="IQueryable{T}" /> that can be used to query the desired
  /// entity type. It also provides predefined <see cref="IQueryable{T}" />
  /// properties for each EVE entity type, for convenience and some
  /// "under-the-hood" processing.
  /// </para>
  /// <para>
  /// All queries initiated by the context are performed without entity
  /// tracking, which means that it's acceptable (and possibly even
  /// preferred) to retain a single <c>EveDbContext</c> instance for the
  /// lifetime of the application.
  /// </para>
  /// <para>
  /// The results of queries performed by the context are entity types
  /// in the <c>Eve.Data.Entities</c> namespace, which can be used to
  /// display data, but which by and large do not implement the interfaces
  /// needed to interact with the game-related functionality of the
  /// EVE library.  You can create an <em>adapter</em> which can be used
  /// for those purposes by calling the
  /// <see cref="IEveEntity{TAdapter}.ToAdapter" /> method on the entities
  /// returned by direct queries.
  /// </para>
  /// </remarks>
  public class EveDbContext : IEveDbContext
  {
    private DirectEveDbContext directContext;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDbContext" /> class using
    /// conventions to create the name of the database to which a connection will
    /// be made. By convention the name is the full name (namespace + class name)
    /// of the derived context class.  For more information on how this is used
    /// to create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    public EveDbContext()
    {
      // By default, use the name of the current type (including derived
      // types) as the key for finding the connection string in the application
      // configuration, in order to follow EF naming conventions.
      string connectionStringLookup = this.GetType().FullName;
      this.directContext = new DirectEveDbContext(connectionStringLookup);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDbContext" /> class using
    /// the given string as the name or connection string for the database to which
    /// a connection will be made.  For more information on how this is used to
    /// create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    /// <param name="nameOrConnectionString">
    /// Either the database name or a connection string.
    /// </param>
    public EveDbContext(string nameOrConnectionString) : this(new DirectEveDbContext(nameOrConnectionString))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDbContext" /> class using
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
    public EveDbContext(DbConnection existingConnection, bool contextOwnsConnection) : this(new DirectEveDbContext(existingConnection, contextOwnsConnection))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDbContext" /> class,
    /// using the specified <see cref="DirectEveDbContext" /> to provide access to the database.
    /// </summary>
    /// <param name="directContext">
    /// The <see cref="DirectEveDbContext" /> which will be used to provide access to the database.
    /// </param>
    private EveDbContext(DirectEveDbContext directContext)
    {
      Contract.Requires(directContext != null, "The inner DirectEveDbContext cannot be null.");
      this.directContext = directContext;
    }

    /* Properties */

    /// <inheritdoc />
    public IQueryable<ActivityEntity> Activities
    {
      get { return this.Query<ActivityEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AgentEntity> Agents
    {
      get
      {
        var result = this.Query<AgentEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<AgentTypeEntity> AgentTypes
    {
      get { return this.Query<AgentTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AncestryEntity> Ancestries
    {
      get { return this.Query<AncestryEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AssemblyLineEntity> AssemblyLines
    {
      get { return this.Query<AssemblyLineEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AssemblyLineStationEntity> AssemblyLineStations
    {
      get { return this.Query<AssemblyLineStationEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AssemblyLineTypeEntity> AssemblyLineTypes
    {
      get { return this.Query<AssemblyLineTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AssemblyLineTypeCategoryDetailEntity> AssemblyLineTypeCategoryDetails
    {
      get { return this.Query<AssemblyLineTypeCategoryDetailEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AssemblyLineTypeGroupDetailEntity> AssemblyLineTypeGroupDetails
    {
      get { return this.Query<AssemblyLineTypeGroupDetailEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AttributeCategoryEntity> AttributeCategories
    {
      get { return this.Query<AttributeCategoryEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AttributeTypeEntity> AttributeTypes
    {
      get { return this.Query<AttributeTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AttributeValueEntity> AttributeValues
    {
      get { return this.Query<AttributeValueEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<BloodlineEntity> Bloodlines
    {
      get { return this.Query<BloodlineEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CategoryEntity> Categories
    {
      get { return this.Query<CategoryEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CelestialEntity> Celestials
    {
      get
      {
        var result = this.Query<CelestialEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<CertificateEntity> Certificates
    {
      get { return this.Query<CertificateEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CertificateCategoryEntity> CertificateCategories
    {
      get { return this.Query<CertificateCategoryEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CertificateClassEntity> CertificateClasses
    {
      get { return this.Query<CertificateClassEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CertificateRecommendationEntity> CertificateRecommendations
    {
      get { return this.Query<CertificateRecommendationEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CertificateRelationshipEntity> CertificateRelationships
    {
      get { return this.Query<CertificateRelationshipEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CharacterAttributeTypeEntity> CharacterAttributeTypes
    {
      get { return this.Query<CharacterAttributeTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<ConstellationEntity> Constellations
    {
      get
      {
        var result = this.Query<ConstellationEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<ConstellationJumpEntity> ConstellationJumps
    {
      get { return this.Query<ConstellationJumpEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<ContrabandInfoEntity> ContrabandInfo
    {
      get { return this.Query<ContrabandInfoEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<ControlTowerResourceEntity> ControlTowerResources
    {
      get { return this.Query<ControlTowerResourceEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CorporateActivityEntity> CorporateActivities
    {
      get { return this.Query<CorporateActivityEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<DivisionEntity> Divisions
    {
      get { return this.Query<DivisionEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<EffectTypeEntity> EffectTypes
    {
      get { return this.Query<EffectTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<EffectEntity> Effects
    {
      get { return this.Query<EffectEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<EveTypeEntity> EveTypes
    {
      get { return this.Query<EveTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<FactionEntity> Factions
    {
      get
      {
        var result = this.Query<FactionEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<FlagEntity> Flags
    {
      get { return this.Query<FlagEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<GraphicEntity> Graphics
    {
      get { return this.Query<GraphicEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<GroupEntity> Groups
    {
      get { return this.Query<GroupEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<IconEntity> Icons
    {
      get { return this.Query<IconEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<ItemEntity> Items
    {
      get { return this.Query<ItemEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<ItemPositionEntity> ItemPositions
    {
      get { return this.Query<ItemPositionEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<MarketGroupEntity> MarketGroups
    {
      get { return this.Query<MarketGroupEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<MetaGroupEntity> MetaGroups
    {
      get { return this.Query<MetaGroupEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<MetaTypeEntity> MetaTypes
    {
      get { return this.Query<MetaTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<NpcCorporationEntity> NpcCorporations
    {
      get
      {
        var result = this.Query<NpcCorporationEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<NpcCorporationDivisionEntity> NpcCorporationDivisions
    {
      get { return this.Query<NpcCorporationDivisionEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<RaceEntity> Races
    {
      get { return this.Query<RaceEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<RegionEntity> Regions
    {
      get
      {
        var result = this.Query<RegionEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<RegionJumpEntity> RegionJumps
    {
      get { return this.Query<RegionJumpEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<SolarSystemEntity> SolarSystems
    {
      get
      {
        var result = this.Query<SolarSystemEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<SolarSystemJumpEntity> SolarSystemJumps
    {
      get { return this.Query<SolarSystemJumpEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<StargateEntity> Stargates
    {
      get
      {
        var result = this.Query<StargateEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<StationEntity> Stations
    {
      get
      {
        var result = this.Query<StationEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <inheritdoc />
    public IQueryable<StationOperationEntity> StationOperations
    {
      get { return this.Query<StationOperationEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<StationServiceEntity> StationServices
    {
      get { return this.Query<StationServiceEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<StationTypeEntity> StationTypes
    {
      get { return this.Query<StationTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<TypeMaterialEntity> TypeMaterials
    {
      get { return this.Query<TypeMaterialEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<UnitEntity> Units
    {
      get { return this.Query<UnitEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<UniverseEntity> Universes
    {
      get
      {
        var result = this.Query<UniverseEntity>()
                         .Include(x => x.ItemInfo);

        Contract.Assume(result != null);
        return result;
      }
    }

    /// <summary>
    /// Gets the inner context.
    /// </summary>
    /// <value>
    /// The <see cref="DirectEveDbContext" /> wrapped by the current instance.
    /// </value>
    internal DirectEveDbContext DirectContext
    {
      get
      {
        Contract.Ensures(Contract.Result<DirectEveDbContext>() != null);
        return this.directContext;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <inheritdoc />
    public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEveEntity
    {
      var result = this.DirectContext.Set<TEntity>().AsNoTracking();

      Contract.Assume(result != null);
      return result;
    }

    /// <summary>
    /// Disposes the current object.
    /// </summary>
    /// <param name="disposing">
    /// Indicates whether to dispose managed resources.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.directContext.Dispose();
      }
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.directContext != null);
    }
  }
}