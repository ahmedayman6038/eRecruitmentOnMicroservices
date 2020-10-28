using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class JobsStatisticsViewModel
    {
        public List<string> JobsName { get; set; }

        public List<int> ApplierCount { get; set; }
    }
}
