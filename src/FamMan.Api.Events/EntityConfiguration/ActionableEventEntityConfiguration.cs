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
            builder.Property(p=>p.Id).ValueGeneratedNever();
        }
    }
}
