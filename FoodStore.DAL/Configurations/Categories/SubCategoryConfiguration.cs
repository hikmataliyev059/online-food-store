using FoodStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodStore.DAL.Configurations.Categories;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable("SubCategories");
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.CategoryId);
    }
}