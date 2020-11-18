using Jobs.API.Application.Entities;
using Jobs.API.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.Seeds
{
    public static class DefaultCountries
    {
        public static async Task SeedAsync(JobContext dbContext)
        {
            var countries = new List<Country>()
            {               
                new Country() 
                { 
                    Name = "Egypt",
                    Cities = new List<City>() 
                    { 
                        new City() { Name = "Cairo" },
                        new City() { Name = "Giza" },
                        new City() { Name = "Alex" }
                    }
                },
                new Country()
                {
                    Name = "USA",
                    Cities = new List<City>()
                    {
                        new City() { Name = "NY" },
                        new City() { Name = "CA" },
                        new City() { Name = "WA" }
                    }
                }
            };
            if (!dbContext.Countries.Any())
            {
                await dbContext.Countries.AddRangeAsync(countries);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
