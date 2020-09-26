using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Models;
using Jobs.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.Repositories
{
    public class JobRepositoryAsync : GenericRepositoryAsync<Job>, IJobRepositoryAsync
    {
        private readonly DbSet<Job> _jobs;

        public JobRepositoryAsync(JobContext dbContext) : base(dbContext)
        {
            _jobs = dbContext.Set<Job>();
        }

        public async Task<IReadOnlyList<Job>> GetByNameAsync(string name)
        {
            return await _jobs
                .Where(j => j.Name == name)
                .ToListAsync();
        }
    }
}
