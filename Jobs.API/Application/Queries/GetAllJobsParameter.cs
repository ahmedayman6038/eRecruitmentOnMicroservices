using Jobs.API.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Queries
{
    public class GetAllJobsParameter : RequestParameter
    {
        public int CityId { get; set; }
    }
}
