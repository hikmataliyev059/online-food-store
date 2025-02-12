using DotNetEnv;
using FoodStore.API.Utils;
using FoodStore.BL;
using FoodStore.DAL;

namespace FoodStore.API;

public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerDocumentation();

        builder.Services.AddConfiguration();
        builder.Services.AddBusinessServices();
        builder.Services.ConfigureMailServices();
        builder.Services.AddRepositories();

        builder.Services.AddIdentityServices();

        builder.Services.GenerateJwtToken();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.ConfigureExceptionHandler();

        app.UseHttpsRedirection();

        app.UseSeedData();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}