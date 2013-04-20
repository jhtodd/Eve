//-----------------------------------------------------------------------
// <copyright file="ReadOnlyStationServiceCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Universe
{
  using System.Collections.Generic;
  using System.Linq;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of station services.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlyStationServiceCollection : ReadOnlyKeyedCollection<StationServiceId, StationService>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyStationServiceCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyStationServiceCollection(IEnumerable<StationService> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      if (contents != null)
      {
        foreach (StationService service in contents)
        {
          Items.AddWithoutCallback(service);
        }
      }
    }
  }
}