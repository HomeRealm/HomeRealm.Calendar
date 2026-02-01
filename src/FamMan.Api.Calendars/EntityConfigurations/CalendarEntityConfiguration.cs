using FamMan.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Calendars.EntityConfigurations;

public class CalendarEntityConfiguration : IEntityTypeConfiguration<CalendarEntity>
{
  public void Configure(EntityTypeBuilder<CalendarEntity> builder)
  {
    builder.HasKey(c => c.Id);
    builder.Property(p => p.Id).ValueGeneratedNever();

    // Required string fields
    builder.Property(p => p.Name)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(p => p.Description)
      .IsRequired()
      .HasMaxLength(1000);

    builder.Property(p => p.Color)
      .IsRequired()
      .HasMaxLength(50);

    builder.Property(p => p.Owner)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(p => p.Visibility)
      .IsRequired()
      .HasMaxLength(10);

    // Relationship configurations
    builder.HasMany(c => c.CalendarEvents)
      .WithOne(ce => ce.Calendar)
      .HasForeignKey(ce => ce.CalendarId)
      .OnDelete(DeleteBehavior.Cascade); // Delete event when calendar deleted
  }
}
