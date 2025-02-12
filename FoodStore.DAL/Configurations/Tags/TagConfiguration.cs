using FoodStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStore.DAL.Configurations.Tags;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasKey(t => t.Id);

        builder.HasMany(t => t.TagProducts)
            .WithOne(tp => tp.Tag)
            .HasForeignKey(tp => tp.TagId);
    }
}