//-----------------------------------------------------------------------
// <copyright file="ReadOnlyEveDbContext.cs" company="Jeremy H. Todd">
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
  /// This access is read-only.
  /// </summary>
  public class ReadOnlyEveDbContext : IEveDbContext
  {
    private EveDbContext innerContext;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyEveDbContext" /> class using
    /// conventions to create the name of the database to which a connection will
    /// be made. By convention the name is the full name (namespace + class name)
    /// of the derived context class.  For more information on how this is used
    /// to create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    public ReadOnlyEveDbContext()
    {
      this.innerContext = new EveDbContext();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyEveDbContext" /> class,
    /// using the specified <see cref="EveDbContext" /> to provide access to the database.
    /// </summary>
    /// <param name="innerContext">
    /// The <see cref="EveDbContext" /> which will be used to provide access to the database.
    /// </param>
    public ReadOnlyEveDbContext(EveDbContext innerContext)
    {
      Contract.Requires(innerContext != null, "The inner EveDbContext cannot be null.");
      this.innerContext = innerContext;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyEveDbContext" /> class using
    /// the given string as the name or connection string for the database to which
    /// a connection will be made.  For more information on how this is used to
    /// create a connection, see the remarks section for <see cref="DbContext" />.
    /// </summary>
    /// <param name="nameOrConnectionString">
    /// Either the database name or a connection string.
    /// </param>
    public ReadOnlyEveDbContext(string nameOrConnectionString)
    {
      this.innerContext = new EveDbContext(nameOrConnectionString);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyEveDbContext" /> class using
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
    public ReadOnlyEveDbContext(DbConnection existingConnection, bool contextOwnsConnection)
    {
      this.innerContext = new EveDbContext(existingConnection, contextOwnsConnection);
    }

    /* Properties */

    /// <inheritdoc />
    public IQueryable<AgentEntity> Agents
    {
      get { return this.Query<AgentEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<AgentTypeEntity> AgentTypes
    {
      get { return this.Query<AgentTypeEntity>(); }
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
    public IQueryable<CategoryEntity> Categories
    {
      get { return this.Query<CategoryEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<CharacterAttributeTypeEntity> CharacterAttributeTypes
    {
      get { return this.Query<CharacterAttributeTypeEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<ConstellationEntity> Constellations
    {
      get { return this.Query<ConstellationEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<ConstellationJumpEntity> ConstellationJumps
    {
      get { return this.Query<ConstellationJumpEntity>(); }
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
      get { return this.Query<NpcCorporationEntity>(); }
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
      get { return this.Query<RegionEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<RegionJumpEntity> RegionJumps
    {
      get { return this.Query<RegionJumpEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<SolarSystemEntity> SolarSystems
    {
      get { return this.Query<SolarSystemEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<SolarSystemJumpEntity> SolarSystemJumps
    {
      get { return this.Query<SolarSystemJumpEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<StationEntity> Stations
    {
      get { return this.Query<StationEntity>(); }
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
    public IQueryable<UnitEntity> Units
    {
      get { return this.Query<UnitEntity>(); }
    }

    /// <inheritdoc />
    public IQueryable<UniverseEntity> Universes
    {
      get { return this.Query<UniverseEntity>(); }
    }

    /// <summary>
    /// Gets the inner context.
    /// </summary>
    /// <value>
    /// The <see cref="EveDbContext" /> wrapped by the current instance.
    /// </value>
    private EveDbContext InnerContext
    {
      get
      {
        Contract.Ensures(Contract.Result<EveDbContext>() != null);
        return this.innerContext;
      }
    }

    /* Methods */

    /// <inheritdoc />
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <inheritdoc />
    public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
    {
      var result = this.InnerContext.Set<TEntity>().AsNoTracking();

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
        this.innerContext.Dispose();
      }
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.innerContext != null);
    }
  }
}