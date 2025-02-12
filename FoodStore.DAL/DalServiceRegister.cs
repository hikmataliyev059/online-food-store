using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;
using FoodStore.DAL.Repositories.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStore.DAL;

public static class DalServiceRegister
{
    public static void AddConfiguration(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Database connection string is not provided");
        }

        services.AddDbContext<FoodStoreDbContext>(opt => { opt.UseNpgsql(connectionString); });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}