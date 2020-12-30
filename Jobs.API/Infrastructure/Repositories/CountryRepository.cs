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
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly DbSet<Country> _countries;

        public CountryRepository(JobContext dbContext) : base(dbContext)
        {
            _countries = dbContext.Set<Country>();
        }
    }
}
