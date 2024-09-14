using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Localization;
using Restaurant.Client.Services;
using Restaurant.Client.Services.Interfaces;
using Restaurant.Client.Auth;
using System.Globalization;
using Restaurant.Client.Extensions;
using Blazored.LocalStorage;
using System.Text.Json;
using System.Text.Json.Serialization;
using Restaurant.Client.Contracts;
using System.Configuration;
using MudBlazor.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Diagnostics;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Blazored.Toast.Services;
using System.Net;

namespace Restaurant.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            ConfigureLocalization(builder.Services);
            builder.Services.AddControllers();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
            {
                options.DetailedErrors = true;
            });
            builder.Services.AddAuthorizationCore();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMudServices();
            builder.Services.AddBlazoredToast();

            builder.Services.Configure<CloudinarySettings>(
                configuration.GetSection("CloudinarySettings"));

            builder.Services.AddTransient<JwtTokenHandler>();

            builder.Services.AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            });
            builder.Services.AddTransient<Services.Interfaces.ILocalStorageService, LocalStorageService>();
            builder.Services.AddTransient<ICookiesService, CookiesService>();
            builder.Services.AddTransient<IPhotoService, PhotoService>();

            builder.Services.AddScoped<IReviewsService, ReviewsService>();
            builder.Services.AddScoped<ITablesService, TablesService>();
            builder.Services.AddScoped<IDishesService, DishesService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<ICuisinesService, CuisinesService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IBlogsService, BlogsService>();
            builder.Services.AddScoped<ICartsService, CartsService>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddScoped<IDeliveriesService, DeliveriesService>();
            builder.Services.AddScoped<IBillsService, BillsService>();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            var handler = new HttpClientHandler
            {
                UseCookies = true, 
                CookieContainer = new CookieContainer(), 
                AllowAutoRedirect = false, 
                UseDefaultCredentials = true 
            };

            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri(configuration.GetConnectionString("API"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => handler); 


            builder.Services.AddControllers();
           
            var app = builder.Build();

            app.UseRequestLocalization();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
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
                new CultureInfo("uk-UA")
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
