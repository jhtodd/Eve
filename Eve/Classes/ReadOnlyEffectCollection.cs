//-----------------------------------------------------------------------
// <copyright file="ReadOnlyEffectCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data.Entity;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data.Entities;

  using FreeNet;
  using FreeNet.Collections;
  using FreeNet.Collections.ObjectModel;
  using FreeNet.Data.Entity;
  using FreeNet.Utilities;

  /// <summary>
  /// A read-only collection of effects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public partial class ReadOnlyEffectCollection
    : ReadOnlyKeyedCollection<EffectId, Effect>,
      IEffectCollection
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyEffectCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyEffectCollection(IEnumerable<Effect> contents) : base()
    {
      if (contents != null)
      {
        foreach (Effect effect in contents)
        {
          Items.AddWithoutCallback(effect);
        }
      }
    }
  }

  #region IEffectCollection Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEffectCollection" /> interface.
  /// </content>
  public partial class ReadOnlyEffectCollection : IEffectCollection
  {
    IEffect IEffectCollection.this[EffectId effectId]
    {
      get
      {
        var result = this[effectId];

        Contract.Assume(result != null);
        return result;
      }
    }

    bool IEffectCollection.TryGetValue(EffectId effectId, out IEffect value)
    {
      Effect containedValue;

      bool success = TryGetValue(effectId, out containedValue);
      value = containedValue;

      Contract.Assume(!success || value != null);
      return success;
    }
  }
  #endregion

  #region IEnumerable<IEffect> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEnumerable{T}" /> interface.
  /// </content>
  public partial class ReadOnlyEffectCollection : IEnumerable<IEffect>
  {
    IEnumerator<IEffect> IEnumerable<IEffect>.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
  #endregion

  #region IReadOnlyList<IEffect> Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IReadOnlyList{T}" /> interface.
  /// </content>
  public partial class ReadOnlyEffectCollection : IReadOnlyList<IEffect>
  {
    IEffect IReadOnlyList<IEffect>.this[int index]
    {
      get { return this[index]; }
    }
  }
  #endregion
}