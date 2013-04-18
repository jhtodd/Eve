//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAssemblyLineStationCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System.Collections.Generic;

  using Eve.Industry;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of agents.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyAssemblyLineStationCollection : ReadOnlyCollection<AssemblyLineStation>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAssemblyLineStationCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAssemblyLineStationCollection(IEnumerable<AssemblyLineStation> contents) : base()
    {
      if (contents != null)
      {
        foreach (AssemblyLineStation agent in contents)
        {
          Items.AddWithoutCallback(agent);
        }
      }
    }
  }
}