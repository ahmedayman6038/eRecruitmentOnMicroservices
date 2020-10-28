using ApiGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Interfaces
{
    public interface IJobsService
    {
        Task<IEnumerable<JobData>> GetAll();

        Task<JobData> GetById(int id);
    }
}
