using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using FoodStore.BL.Helpers.Email;
using FoodStore.BL.Services.Implements;
using FoodStore.BL.Services.Interfaces;
using FoodStore.Core.Entities;
using FoodStore.Core.Enums;
using FoodStore.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FoodStore.BL;

public static class BusinessRegister
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessRegister));
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddControllers().AddFluentValidation(cfg =>
            cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
    }

    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = false;
            opt.Lockout.MaxFailedAccessAttempts = 5;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.AllowedForNewUsers = true;
            opt.Password.RequiredLength = 4;
        }).AddEntityFrameworkStores<FoodStoreDbContext>().AddDefaultTokenProviders();
    }

    public static void ConfigureMailServices(this IServiceCollection services)
    {
        services.Configure<MailSettings>(options =>
        {
            options.Mail = Environment.GetEnvironmentVariable("MAIL_SETTINGS_MAIL")!;
            options.DisplayName = Environment.GetEnvironmentVariable("MAIL_SETTINGS_DISPLAY_NAME")!;
            options.Password = Environment.GetEnvironmentVariable("MAIL_SETTINGS_PASSWORD")!;
            options.Host = Environment.GetEnvironmentVariable("MAIL_SETTINGS_HOST")!;
            options.Port = int.TryParse(Environment.GetEnvironmentVariable("MAIL_SETTINGS_PORT"), out var port)
                ? port
                : 587;
        });
        services.AddTransient<IMailService, MailService>();
    }

    public static void GenerateJwtToken(this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECURITY_KEY")!))
                };
            });
    }

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT"
                        },
                        Scheme = "bearer",
                        Name = "JWT",
                        In = ParameterLocation.Header,
                    },
                    new string[] { }
                }
            });
        });
    }

    public static void UseSeedData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            CreateRoles(roleManager).Wait();
            CreateAdmin(userManager).Wait();
        }
    }

    private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
    {
        int res = await roleManager.Roles.CountAsync();

        if (res == 0)
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                await roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }
    }

    private static async Task CreateAdmin(UserManager<AppUser> userManager)
    {
        if (!await userManager.Users.AnyAsync(x => x.UserName == Environment.GetEnvironmentVariable("ADMIN_USERNAME")))
        {
            AppUser user = new AppUser
            {
                UserName = Environment.GetEnvironmentVariable("ADMIN_USERNAME"),
                FirstName = Environment.GetEnvironmentVariable("ADMIN_FIRSTNAME")!,
                LastName = Environment.GetEnvironmentVariable("ADMIN_LASTNAME")!,
                Email = Environment.GetEnvironmentVariable("ADMIN_EMAIL")
            };

            await userManager.CreateAsync(user, Environment.GetEnvironmentVariable("ADMIN_PASSWORD")!);
            await userManager.AddToRoleAsync(user, nameof(UserRoles.Admin));
        }
    }
}