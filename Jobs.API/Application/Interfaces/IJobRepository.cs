using Jobs.API.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IReadOnlyList<Job>> GetPagedReponseAsync(int pageNumber, int pageSize, int cityId);
    }
}
