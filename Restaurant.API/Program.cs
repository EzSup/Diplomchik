using System.Text.Json.Serialization;
using Restaurant.Core;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Repositories;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Services;
using Restaurant.Core.Services.Interfaces;
using Mapster;

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
            
            

            //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ICuisinesRepository, CuisinesRepository>();
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
            builder.Services.AddScoped<IDiscountsRepository, DiscountsRepository>();
            builder.Services.AddScoped<IDishBillsRepository, DishBillsRepository>();
            builder.Services.AddScoped<IDishesRepository, DishesRepository>();
            builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
            builder.Services.AddScoped<ITablesRepository, TablesRepository>();

            builder.Services.AddScoped<IDishesService, DishesService>();
                
                
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
