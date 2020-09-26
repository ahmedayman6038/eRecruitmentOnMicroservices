using Jobs.API.Application.Models;
using Jobs.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.Contexts
{
    public class JobContext : DbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CityEntityTypeConfiguration());
            builder.ApplyConfiguration(new CountryEntityTypeConfiguration());
            builder.ApplyConfiguration(new JobEntityTypeConfiguration());
        }
    }
}
