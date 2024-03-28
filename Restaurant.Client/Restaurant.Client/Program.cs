using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Restaurant.Client.Services;
using Restaurant.Core;
using Restaurant.Core.Repositories;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Services;
using Restaurant.Core.Services.Interfaces;
using System.Globalization;

namespace Restaurant.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureLocalization(builder.Services);
            builder.Services.AddControllers();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<CurrentCustomer>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
                sp.GetRequiredService<CurrentCustomer>());

            builder.Services.AddScoped<IBillsRepository, BillsRepository>();
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ICuisinesRepository, CuisinesRepository>();
            builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
            builder.Services.AddScoped<IDiscountsRepository, DiscountsRepository>();
           // builder.Services.AddScoped<IDishBillsRepository, DishBillsRepository>();
            builder.Services.AddScoped<IDishesRepository, DishesRepository>();
            builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
            builder.Services.AddScoped<ITablesRepository, TablesRepository>();
            builder.Services.AddScoped<IBlogsRepository, BlogsRepository>();

            builder.Services.AddScoped<IDishesService, DishesService>();
            builder.Services.AddScoped<ITablesService, TablesService>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddScoped<IBlogsService, BlogsService>();

            builder.Services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            app.UseRequestLocalization();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapControllers();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        public static void ConfigureLocalization(IServiceCollection services)
        {
            var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("uk-UA"), new CultureInfo("en-US")
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0].Name, uiCulture: supportedCultures[0].Name);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddLocalization();
        }
    }
}
