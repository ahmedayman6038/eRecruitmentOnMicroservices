using Applying.API.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Queries
{
    public class GetAllAppliesParameter : RequestParameter
    {
        public int StatusId { get; set; }
    }
}
