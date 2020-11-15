using Jobs.API.Application.Entities;
using Jobs.API.Application.Interfaces;
using Jobs.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.Contexts
{
    public class JobContext : DbContext
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IIdentityService _identityService;

        public JobContext(DbContextOptions<JobContext> options, IDateTimeService dateTimeService, IIdentityService identityService) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTimeService = dateTimeService;
            _identityService = identityService;
        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _identityService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _identityService.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CityEntityTypeConfiguration());
            builder.ApplyConfiguration(new CountryEntityTypeConfiguration());
            builder.ApplyConfiguration(new JobEntityTypeConfiguration());
        }
    }
}
