//-----------------------------------------------------------------------
// <copyright file="DirectEveDbContext.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Data.Entity;
  using System.Data.Entity.ModelConfiguration;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Reflection;

  using Eve.Data.Entities;
  using Eve.Data.Entities.Configuration;
  using Eve.Universe;

  using FreeNet.Data.Entity.Utilities;
  using FreeNet.Utilities;

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
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Load all entity configuration classes in the current assembly, and add them
      // to the model builder.
      foreach (var configurationType in this.GetEntityConfigurationTypes())
      {
        object configuration = Activator.CreateInstance(configurationType);
        modelBuilder.Configurations.Add((dynamic)configuration);
      }
    }
  }
}