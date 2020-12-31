using Jobs.API.Application.Entities;
using Jobs.API.Application.Interfaces;
using Jobs.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DbSet<City> _cities;

        public CityRepository(JobContext dbContext) : base(dbContext)
        {
            _cities = dbContext.Set<City>();
        }

        public async Task<IReadOnlyList<City>> GetPagedReponseAsync(int pageNumber, int pageSize, int countryId)
        {
            return await _cities
                .Where(j => j.CountryId == countryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
