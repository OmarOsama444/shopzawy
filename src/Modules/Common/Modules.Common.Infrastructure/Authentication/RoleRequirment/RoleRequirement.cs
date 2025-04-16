using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Infrastructure.Authentication.RoleRequirment
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; init; }
        public RoleRequirement(string Roles)
        {
            this.Roles = Roles.Split(",").ToArray();
        }
    }
}
