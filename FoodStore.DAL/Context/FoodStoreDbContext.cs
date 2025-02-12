using System.Reflection;
using FoodStore.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Context;

public class FoodStoreDbContext : IdentityDbContext<AppUser>
{
    public FoodStoreDbContext(DbContextOptions<FoodStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<TagProduct> TagProducts { get; set; }

    public DbSet<SubCategory> SubCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}