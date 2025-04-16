using Microsoft.AspNetCore.Authorization;
using Modules.Common.Infrastructure.Authentication.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Infrastructure.Authentication.RoleRequirment
{
    internal class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            string role = context.User.GetRole();
            if (requirement.Roles.Contains(role))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
