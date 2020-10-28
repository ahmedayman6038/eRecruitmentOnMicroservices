using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Interfaces
{
    public interface IIdentityService
    {
        string UserId { get; }
    }
}
