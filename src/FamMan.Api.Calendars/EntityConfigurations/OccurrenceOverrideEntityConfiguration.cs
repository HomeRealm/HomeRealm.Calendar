using FamMan.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Calendars.EntityConfigurations;

public class OccurrenceOverrideEntityConfiguration : IEntityTypeConfiguration<OccurrenceOverrideEntity>
{
  public void Configure(EntityTypeBuilder<OccurrenceOverrideEntity> builder)
  {
    builder.HasKey(oo => oo.Id);
    builder.Property(p => p.Id).ValueGeneratedNever();

    // Foreign key
    builder.Property(p => p.RecurrenceId)
      .IsRequired();

    // Required date field
    builder.Property(p => p.Date)
      .IsRequired();

    // Relationship configuration
    builder.HasOne(oo => oo.RecurrenceRule)
      .WithMany(rr => rr.OccurrenceOverrideEntities)
      .HasForeignKey(oo => oo.RecurrenceId)
      .OnDelete(DeleteBehavior.Cascade); // Delete override when recurrence rule deleted
  }
}
