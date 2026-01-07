using FamMan.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Calendars.EntityConfigurations;

public class AttendeeEntityConfiguration : IEntityTypeConfiguration<Attendee>
{
  public void Configure(EntityTypeBuilder<Attendee> builder)
  {
    builder.HasKey(a => a.Id);
    builder.Property(p => p.Id).ValueGeneratedNever();

    // Foreign key
    builder.Property(p => p.EventId)
      .IsRequired();

    builder.Property(p => p.UserId)
      .IsRequired();

    // Required string fields
    builder.Property(p => p.Status)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(p => p.Role)
      .IsRequired()
      .HasMaxLength(200);

    // Relationship configuration
    builder.HasOne(a => a.CalendarEvent)
      .WithMany(ce => ce.Attendees)
      .HasForeignKey(a => a.EventId)
      .OnDelete(DeleteBehavior.Cascade); // Delete attendee when event deleted
  }
}
