using FamMan.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Calendars.EntityConfigurations;

public class CalendarEventEntityConfiguration : IEntityTypeConfiguration<CalendarEventEntity>
{
  public void Configure(EntityTypeBuilder<CalendarEventEntity> builder)
  {
    builder.HasKey(ce => ce.Id);
    builder.Property(p => p.Id).ValueGeneratedNever();

    // Foreign keys
    builder.Property(p => p.CalendarId)
      .IsRequired();

    builder.Property(p => p.RecurrenceId)
      .IsRequired();

    builder.Property(p => p.CategoryId)
      .IsRequired(false); // CategoryId is nullable

    // Required string fields
    builder.Property(p => p.Title)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(p => p.Description)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(p => p.Location)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(p => p.LinkedResource)
      .HasMaxLength(200);

    // Required date fields
    builder.Property(p => p.Start)
      .IsRequired();

    builder.Property(p => p.End)
      .IsRequired();

    // Boolean field
    builder.Property(p => p.AllDay)
      .IsRequired();

    // Relationship configurations
    builder.HasOne(ce => ce.Calendar)
      .WithMany(c => c.CalendarEvents)
      .HasForeignKey(ce => ce.CalendarId)
      .OnDelete(DeleteBehavior.Cascade); // Delete event when calendar deleted

    builder.HasOne(ce => ce.RecurrenceRule)
      .WithOne(rr => rr.CalendarEvent)
      .HasForeignKey<RecurrenceRuleEntity>(rr => rr.EventId)
      .OnDelete(DeleteBehavior.Cascade); // Delete recurrence rule when event deleted

    builder.HasMany(ce => ce.Attendees)
      .WithOne(a => a.CalendarEvent)
      .HasForeignKey(a => a.EventId)
      .OnDelete(DeleteBehavior.Cascade); // Delete attendees when event deleted

    builder.HasMany(ce => ce.Reminders)
      .WithOne(r => r.CalendarEvent)
      .HasForeignKey(r => r.EventId)
      .OnDelete(DeleteBehavior.Cascade); // Delete reminders when event deleted

    builder.HasOne(ce => ce.Category)
      .WithMany(cat => cat.CalendarEvents)
      .HasForeignKey(ce => ce.CategoryId)
      .IsRequired(false) // CategoryId is nullable, events can exist without category
      .OnDelete(DeleteBehavior.Restrict); // Prevent category deletion if events reference it
  }
}
