//-----------------------------------------------------------------------
// <copyright file="TypeReactionEntityConfiguration.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Data.Entities.Configuration
{
  using System.Data.Entity.ModelConfiguration;
  using System.Diagnostics.Contracts;

  using FreeNet.Data.Entity.Configuration;

  /// <summary>
  /// Contains Entity Framework configuration options for the
  /// <see cref="TypeReactionEntity" /> class.
  /// </summary>
  [EntityConfigurationForContext(typeof(DirectEveDbContext))]
  public class TypeReactionEntityConfiguration : EntityTypeConfiguration<TypeReactionEntity>
  {
    /// <summary>
    /// Initializes a new instance of the TypeReactionEntityConfiguration class.
    /// </summary>
    [ContractVerification(false)] // Saves a lot of basic warnings until EF adds contracts to the base class
    public TypeReactionEntityConfiguration() : base()
    {
      // Table level mappings
      this.ToTable("invTypeReactions");
      this.HasKey(tr => new { tr.ReactionTypeId, tr.Input, tr.TypeId });
      
      // Column level mappings
      this.Property(tr => tr.Input).HasColumnName("input");
      this.Property(tr => tr.Quantity).HasColumnName("quantity");
      this.Property(tr => tr.ReactionTypeId).HasColumnName("reactionTypeID");
      this.Property(tr => tr.TypeId).HasColumnName("typeID");

      // Relationship mappings
      this.HasRequired(tr => tr.ReactionType).WithMany().HasForeignKey(tr => tr.ReactionTypeId);
      this.HasRequired(tr => tr.Type).WithMany().HasForeignKey(tr => tr.TypeId);
    }
  }
}