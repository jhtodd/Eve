//-----------------------------------------------------------------------
// <copyright file="ReadOnlyEffectCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve
{
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Collections;
  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of <see cref="Effect" /> objects.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed partial class ReadOnlyEffectCollection
    : ReadOnlyKeyedEntityAdapterCollection<EffectId, EffectEntity, Effect>,
      IEffectCollection
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlyEffectCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlyEffectCollection(IEveRepository repository, IEnumerable<Effect> contents) : base(repository, contents)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlyEffectCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> associated with the items in the 
    /// collection.
    /// </param>
    /// <param name="entities">
    /// A sequence of entities from which to create the contents of the collection.
    /// </param>
    public ReadOnlyEffectCollection(IEveRepository repository, IEnumerable<EffectEntity> entities) : base(repository, entities)
    {
      Contract.Requires(repository != null, "The provided repository cannot be null.");
    }
  }

  #region IEffectCollection Implementation
  /// <content>
  /// Explicit implementation of the <see cref="IEffectCollection" /> interface.
  /// </content>
  public sealed partial class ReadOnlyEffectCollection : IEffectCollection
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
  public sealed partial class ReadOnlyEffectCollection : IEnumerable<IEffect>
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
  public sealed partial class ReadOnlyEffectCollection : IReadOnlyList<IEffect>
  {
    IEffect IReadOnlyList<IEffect>.this[int index]
    {
      get { return this[index]; }
    }
  }
  #endregion
}