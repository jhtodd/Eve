//-----------------------------------------------------------------------
// <copyright file="ReadOnlyAssemblyLineTypeGroupDetailCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Industry
{
  using System.Collections.Generic;
  using System.Linq;

  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of effects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlyAssemblyLineTypeGroupDetailCollection : ReadOnlyKeyedCollection<GroupId, AssemblyLineTypeGroupDetail>
  {
    private static readonly KeyGenerator<GroupId, AssemblyLineTypeGroupDetail> AssemblyLineKeyGenerator = new KeyGenerator<GroupId, AssemblyLineTypeGroupDetail>(x => x.GroupId);
    
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyAssemblyLineTypeGroupDetailCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyAssemblyLineTypeGroupDetailCollection(IEnumerable<AssemblyLineTypeGroupDetail> contents)
      : base(contents == null ? 0 : contents.Count(), AssemblyLineKeyGenerator, null)
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