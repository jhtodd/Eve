//-----------------------------------------------------------------------
// <copyright file="ReadOnlySkillTypeCollection.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using Eve.Data;
  using Eve.Data.Entities;

  using FreeNet.Collections.ObjectModel;

  /// <summary>
  /// A read-only collection of skill types.
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Base class implements ISerializable but the contents of the collection cannot be serialized.")]
  public sealed class ReadOnlySkillTypeCollection : ReadOnlyKeyedCollection<SkillId, SkillType>
  {
    /* Constructors */

    /// <summary>
    /// Initializes a new instance of the ReadOnlySkillTypeCollection class.
    /// </summary>
    /// <param name="contents">
    /// The contents of the collection.
    /// </param>
    public ReadOnlySkillTypeCollection(IEnumerable<SkillType> contents) : base(contents == null ? 0 : contents.Count())
    {
      if (contents != null)
      {
        foreach (SkillType item in contents)
        {
          Items.AddWithoutCallback(item);
        }
      }
    }

    /// <summary>
    /// Initializes a new instance of the ReadOnlySkillTypeCollection class.
    /// </summary>
    /// <param name="repository">
    /// The <see cref="IEveRepository" /> to which the contents will be added.
    /// </param>
    /// <param name="contents">
    /// A sequence of <see cref="SkillTypeEntity" /> objects from which to create
    /// the <see cref="SkillType" /> objects which will be contained in the
    /// collection.
    /// </param>
    public ReadOnlySkillTypeCollection(IEveRepository repository, IEnumerable<EveTypeEntity> contents)
      : base(contents == null ? 0 : contents.Count())
    {
      Contract.Requires(contents == null || repository != null, "The provided repository cannot be null.");

      if (contents != null)
      {
        var realContents = contents.Select(x => repository.GetOrAddStoredValue(x.Id, () => x.ToAdapter(repository)));

        foreach (EveType item in realContents)
        {
          SkillType skillType = item as SkillType;

          if (skillType == null)
          {
            throw new InvalidCastException("The types being added must all be skill types.");
          }

          Items.AddWithoutCallback(skillType);
        }
      }
    }
  }
}