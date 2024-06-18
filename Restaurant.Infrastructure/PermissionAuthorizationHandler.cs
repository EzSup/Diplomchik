using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory factory)
        {
            _serviceScopeFactory = factory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if(userId == null || !Guid.TryParse(userId.Value, out var id)) 
            {
                return;
            }

            using var scope = _serviceScopeFactory.CreateScope();

            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionsService>();

            var permissions = await permissionService.GetPermissionsAsync(id);

            //ТУТ ПОМІНЯТИ ЩОБ ПЕРЕВІРЯЛОСЬ ЧИ ВСІ ОБ'ЄКТИ requirement МІСТЯТЬСЯ В permission
            if(requirement.Permissions.Intersect(permissions).Count() == requirement.Permissions.Count()) 
            {
                context.Succeed(requirement);
            }
        }
    }
}
