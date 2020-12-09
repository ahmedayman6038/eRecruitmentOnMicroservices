using Applying.API.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Applying.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
        }
        public string UserId { get; }
    }
}
