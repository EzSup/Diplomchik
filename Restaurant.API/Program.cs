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

namespace Restaurant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddScoped<IBlogsRepository, BlogsRepository>();
            builder.Services.AddScoped<ITablesRepository, TablesRepository>();

            builder.Services.AddScoped<IBlogsService, BlogsService>();
            builder.Services.AddScoped<ITablesService, TablesService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!);
            });

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

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
