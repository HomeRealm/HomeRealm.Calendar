using FamMan.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Calendars.EntityConfigurations;

public class RecurrenceRuleEntityConfiguration : IEntityTypeConfiguration<RecurrenceRuleEntity>
{
  public void Configure(EntityTypeBuilder<RecurrenceRuleEntity> builder)
  {
    builder.HasKey(rr => rr.Id);
    builder.Property(p => p.Id).ValueGeneratedNever();

    // Foreign key
    builder.Property(p => p.EventId)
      .IsRequired();

    // Required string fields
    builder.Property(p => p.Rule)
      .IsRequired()
      .HasMaxLength(200);

    // Required date field
    builder.Property(p => p.EndDate)
      .IsRequired();

    // Relationship configurations
    builder.HasOne(rr => rr.CalendarEvent)
      .WithOne(ce => ce.RecurrenceRule)
      .HasForeignKey<RecurrenceRuleEntity>(rr => rr.EventId)
      .OnDelete(DeleteBehavior.Cascade); // Delete rule when event deleted

    builder.HasMany(rr => rr.OccurrenceOverrideEntities)
      .WithOne(oo => oo.RecurrenceRule)
      .HasForeignKey(oo => oo.RecurrenceId)
      .OnDelete(DeleteBehavior.Cascade); // Delete overrides when recurrence rule deleted
  }
}
