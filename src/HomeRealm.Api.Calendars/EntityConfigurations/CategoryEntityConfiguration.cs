using HomeRealm.Api.Calendars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeRealm.Api.Calendars.EntityConfigurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
  public void Configure(EntityTypeBuilder<CategoryEntity> builder)
  {
    builder.HasKey(c => c.Id);
    builder.Property(p => p.Id).ValueGeneratedNever();

    // Required string fields
    builder.Property(p => p.Name)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(p => p.Color)
      .IsRequired()
      .HasMaxLength(200);

    builder.Property(p => p.Icon)
      .IsRequired()
      .HasMaxLength(200);
  }
}

