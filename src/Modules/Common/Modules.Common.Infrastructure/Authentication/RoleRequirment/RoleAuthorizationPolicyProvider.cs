using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Infrastructure.Authentication.RoleRequirment
{
    public class RoleAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _authorizationOptions;
        public RoleAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _authorizationOptions = options.Value;
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);
            if (policy is not null)
            {
                return policy;
            }
            AuthorizationPolicy rolePolicy = new AuthorizationPolicyBuilder()
                .AddRequirements(new RoleRequirement(policyName))
                .Build();

            _authorizationOptions.AddPolicy(policyName, rolePolicy);
            return rolePolicy;
        }
    }

}
