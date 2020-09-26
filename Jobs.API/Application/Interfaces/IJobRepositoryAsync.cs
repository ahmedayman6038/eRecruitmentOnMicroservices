using Jobs.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Interfaces
{
    public interface IJobRepositoryAsync : IGenericRepositoryAsync<Job>
    {
        Task<IReadOnlyList<Job>> GetByNameAsync(string name);
    }
}
