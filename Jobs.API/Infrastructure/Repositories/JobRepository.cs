using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Entities;
using Jobs.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.Repositories
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        private readonly DbSet<Job> _jobs;

        public JobRepository(JobContext dbContext) : base(dbContext)
        {
            _jobs = dbContext.Set<Job>();
        }

        public async Task<IReadOnlyList<Job>> SearchJobByNameAsync(string name)
        {
            return await _jobs
                .Where(j => j.Name == name)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Job>> GetPagedReponseWithEagerLoadAsync(int pageNumber, int pageSize, int cityId)
        {
            var jobs = _jobs
                .Include(j => j.City)
                .ThenInclude(j => j.Country)
                .AsQueryable();

            if (cityId != 0)
            {
                jobs = jobs.Where(j => j.CityId == cityId);
            }

            return await jobs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
