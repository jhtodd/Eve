//-----------------------------------------------------------------------
// <copyright file="EveDataObjectContracts.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Diagnostics.Contracts;

  using Eve.Data;

  /// <summary>
  /// Contract class for the <see cref="IAttribute" /> interface.
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
  [ContractClassFor(typeof(EveDataObject<>))]
  internal abstract class EveDataObjectContracts<TDerived> : EveDataObject<TDerived>
    where TDerived : class
  {
    /* Properties */

    /// <inheritdoc />
    protected internal override IConvertible CacheKey
    {
      get 
      {
        Contract.Ensures(Contract.Result<IConvertible>() != null);
        throw new NotImplementedException();
      }
    }

    /* Methods */

    /// <inheritdoc />
    public override int CompareTo(TDerived other)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override bool Equals(TDerived other)
    {
      throw new NotImplementedException();
    }
  }
}
