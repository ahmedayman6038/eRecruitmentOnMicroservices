using Jobs.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IReadOnlyList<Job>> SearchJobByNameAsync(string name);

        Task<IReadOnlyList<Job>> GetPagedReponseWithEagerLoadAsync(int pageNumber, int pageSize, int cityId);
    }
}
