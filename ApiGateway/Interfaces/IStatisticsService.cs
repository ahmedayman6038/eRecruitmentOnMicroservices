using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Interfaces
{
    public interface IStatisticsService
    {
        Task<Response<JobsStatisticsViewModel>> GetTopJobApplied();
    }
}
