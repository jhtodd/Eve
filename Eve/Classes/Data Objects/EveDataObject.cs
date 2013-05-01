//-----------------------------------------------------------------------
// <copyright file="EveDataObject.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;

  /// <summary>
  /// The base class for all objects read from the EVE database.
  /// </summary>
  /// <typeparam name="TDerived">
  /// The type of the concrete derived class; i.e. to declare a "Ship"
  /// base value, you would write <c>public class Ship : EveDataObject&lt;Ship&gt;</c>.
  /// I know this sort of pseudo-circular "Curiously Recurring Template Pattern"
  /// has its pitfalls, but it allows me to write many useful methods
  /// (Equals, CompareTo, etc.) at this base class level and save myself an
  /// awful lot of hard-to-maintain boilerplate in derived classes.  Since I
  /// have a lot(!) of derived classes to write, and not many people will ever
  /// see or care about these inner workings, I'm going to do it anyway in the
  /// interests of pragmatism :).
  /// </typeparam>
  /// <remarks>
  /// <para>
  /// This class contains boilerplate code common to all (or almost all)
  /// objects loaded from the EVE database.  Classes derived from this type
  /// should be declared either <c>abstract</c> or <c>sealed</c>.
  /// </para>
  /// </remarks>
  [ContractClass(typeof(EveDataObjectContracts<>))]
  public abstract partial class EveDataObject<TDerived>
    : IComparable,
      IComparable<TDerived>,
      IEquatable<TDerived>,
      IEveCacheable
    where TDerived : class
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the <see cref="EveDataObject{TDerived}" /> class.
    /// </summary>
    protected EveDataObject()
    {
    }

    /* Properties */

    /// <summary>
    /// Gets the ID value used to store the object in the cache.
    /// </summary>
    /// <value>
    /// A value which uniquely identifies the entity.
    /// </value>
    protected internal abstract IConvertible CacheKey { get; }

    /* Methods */

    /// <inheritdoc />
    public abstract int CompareTo(TDerived other);

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
      return this.Equals(obj as TDerived);
    }

    /// <inheritdoc />
    public abstract bool Equals(TDerived other);

    /// <inheritdoc />
    public override int GetHashCode()
    {
      // Necessary to suppress warning about not overriding GetHashCode().
      return base.GetHashCode();
    }
  }

  #region IComparable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IComparable" /> interface.
  /// </content>
  public partial class EveDataObject<TDerived> : IComparable
  {
    int IComparable.CompareTo(object obj)
    {
      return this.CompareTo(obj as TDerived);
    }
  }
  #endregion

  #region IEveCacheable Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEveCacheable" /> interface.
  /// </content>
  public abstract partial class EveDataObject<TDerived> : IEveCacheable
  {
    IConvertible IEveCacheable.CacheKey
    {
      get { return this.CacheKey; }
    }
  }
  #endregion
}
