//-----------------------------------------------------------------------
// <copyright file="EveQueryMethodAttribute.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Reflection;
  using System.Text;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;

  /// <summary>
  /// Indicates that the target method can be used to retrieve data of a specified type.
  /// </summary>
  /// <remarks>
  /// <para>
  /// Methods decorated with this attribute must allow no parameters and return an
  /// <see cref="IEnumerable" /> containing the results of the query.
  /// </para>
  /// </remarks>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  public class EveQueryMethodAttribute : Attribute
  {
    private Type queryType;

    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the EveQueryMethodAttribute class.
    /// </summary>
    /// <param name="queryType">
    /// The <see cref="Type" /> returned by the query performed by the target
    /// method.
    /// </param>
    public EveQueryMethodAttribute(Type queryType)
    {
      Contract.Requires(queryType != null, "The type returned by the query.");

      this.queryType = queryType;
    }

    /* Properties */

    /// <summary>
    /// Gets the type returned by the query performed by the target method.
    /// </summary>
    /// <value>
    /// The <see cref="Type" /> of the returned type.
    /// </value>
    public Type QueryType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);
        return this.queryType;
      }
    }

    /* Methods */

    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.queryType != null);
    }
  }
}