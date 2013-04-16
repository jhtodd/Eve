//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAssemblyLineTypeGroupDetailCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System.Collections.Generic;

  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of effects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyAssemblyLineTypeGroupDetailCollection : ReadOnlyKeyedCollection<GroupId, AssemblyLineTypeGroupDetail>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAssemblyLineTypeGroupDetailCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAssemblyLineTypeGroupDetailCollection(IEnumerable<AssemblyLineTypeGroupDetail> contents)
      : base(new KeyGenerator<GroupId, AssemblyLineTypeGroupDetail>(x => x.GroupId), null)
    {
      if (contents != null)
      {
        foreach (AssemblyLineTypeGroupDetail details in contents)
        {
          Items.AddWithoutCallback(details);
        }
      }
    }
  }
}