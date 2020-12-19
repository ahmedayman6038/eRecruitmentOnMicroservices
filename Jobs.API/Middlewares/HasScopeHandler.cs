using Jobs.API.Application.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Middlewares
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope"))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User.FindAll(c => c.Type == "scope").Select(s => s.Value);

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => s == requirement.Scope || s == "manage"))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
