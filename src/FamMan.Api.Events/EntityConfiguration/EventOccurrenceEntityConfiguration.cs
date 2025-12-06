using FamMan.Api.Events.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Events.EntityConfiguration;

public class EventOccurrenceEntityConfiguration : IEntityTypeConfiguration<EventOccurrence>
{
	public void Configure(EntityTypeBuilder<EventOccurrence> builder)
	{
		builder.HasKey(eo => eo.Id);
		builder.Property(p => p.Id).ValueGeneratedNever();
		
		// Foreign key relationship
		builder.HasOne(eo => eo.ActionableEvent)
			.WithMany()
			.HasForeignKey(eo => eo.ActionableEventId)
			.OnDelete(DeleteBehavior.Cascade);
		
		// Required fields
		builder.Property(p => p.ScheduledTime)
			.IsRequired();
		
		builder.Property(p => p.Status)
			.IsRequired()
			.HasMaxLength(50);
		
		builder.Property(p => p.AttemptCount)
			.IsRequired()
			.HasDefaultValue(0);
		
		builder.Property(p => p.LastError)
			.HasMaxLength(2000);
		
		builder.Property(p => p.CreatedAt)
			.IsRequired();
		
		// Indexes for common queries
		builder.HasIndex(p => p.ActionableEventId);
		builder.HasIndex(p => p.ScheduledTime);
		builder.HasIndex(p => p.Status);
		builder.HasIndex(p => new { p.Status, p.ScheduledTime });
		builder.HasIndex(p => new { p.ActionableEventId, p.ScheduledTime });
	}
}
