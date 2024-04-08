using System.Text.Json.Serialization;
using Restaurant.Core;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Restaurant.Persistense;
using Restaurant.Core.Interfaces;
using Restaurant.Persistense.Repositories;
using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Application.Interfaces.Auth;
using Restaurant.Infrastructure;
using Restaurant.API.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Restaurant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);  

            builder.Services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!);
            });

            builder.Services.Configure<JwtOptions>(
                configuration.GetSection("JwtOptions"));
            builder.Services.Configure<AuthorizationOptions>(
                configuration.GetSection("AuthorizationOptions"));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IBlogsRepository, BlogsRepository>();
            builder.Services.AddScoped<ITablesRepository, TablesRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();

            builder.Services.AddScoped<IBlogsService, BlogsService>();
            builder.Services.AddScoped<ITablesService, TablesService>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
            
            var serviceProvider = builder.Services.BuildServiceProvider();

            builder.Services.AddApiAuthentication(configuration,
                serviceProvider.GetRequiredService<IOptions<JwtOptions>>()
                );

            var app = builder.Build();
            
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
                context.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
