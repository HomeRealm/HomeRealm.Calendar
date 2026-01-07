using FamMan.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Calendars.EntityConfigurations;

public class ReminderEntityConfiguration : IEntityTypeConfiguration<ReminderEntity>
{
  public void Configure(EntityTypeBuilder<ReminderEntity> builder)
  {
    builder.HasKey(r => r.Id);
    builder.Property(p => p.Id).ValueGeneratedNever();

    // Foreign key
    builder.Property(p => p.EventId)
      .IsRequired();

    // Required string field
    builder.Property(p => p.Method)
      .IsRequired()
      .HasMaxLength(200);

    // Required int field
    builder.Property(p => p.TimeBefore)
      .IsRequired();

    // Relationship configuration
    builder.HasOne(r => r.CalendarEvent)
      .WithMany(ce => ce.Reminders)
      .HasForeignKey(r => r.EventId)
      .OnDelete(DeleteBehavior.Cascade); // Delete reminder when event deleted
  }
}
