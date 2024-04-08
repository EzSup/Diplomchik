using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Application.Services;
using Restaurant.Core.Enums;
using Restaurant.Infrastructure;
using System.Text;

namespace Restaurant.API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IOptions<JwtOptions> jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.HttpContext.Request.Cookies["tasty-cookies"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddScoped<IPermissionsService, PermissionService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy("UserPolicy", policy =>
                    {
                        policy.AddRequirements(new PermissionRequirement([Permission.Read]));
                    });
                    options.AddPolicy("AdminPolicy", policy =>
                    {
                        policy.AddRequirements(new PermissionRequirement(
                            [Permission.Read,
                            Permission.Create,
                            Permission.Update,
                            Permission.Delete]));
                    });
                }
            );
        }

        public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
            this TBuilder builder, params Permission[] permissions) where TBuilder : IEndpointConventionBuilder
        {
            return builder.RequireAuthorization(policy =>
            policy.AddRequirements(new PermissionRequirement(permissions)));
        }
    }
}
