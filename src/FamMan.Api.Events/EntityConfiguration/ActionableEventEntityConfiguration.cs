using FamMan.Api.Events.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamMan.Api.Events.EntityConfiguration
{
    public class ActionableEventEntityConfiguration : IEntityTypeConfiguration<ActionableEvent>
    {
        public void Configure(EntityTypeBuilder<ActionableEvent> builder)
        {
            builder.HasKey(ae => ae.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            
            // Required string fields
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(p => p.EventType)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);
            
            // JSON fields - stored as text/jsonb
            builder.Property(p => p.RecurrenceRules)
                .IsRequired()
                .HasColumnType("jsonb");
            
            // Status and tracking
            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            
            // Audit fields
            builder.Property(p => p.CreatedAt)
                .IsRequired();
            
            builder.Property(p => p.UpdatedAt)
                .IsRequired();
            
            // Indexes for common queries
            builder.HasIndex(p => p.EventType);
            builder.HasIndex(p => p.IsActive);
        }
    }
}
