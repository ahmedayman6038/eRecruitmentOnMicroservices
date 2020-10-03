using Applying.API.Application.Models;
using Applying.API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Infrastructure.Contexts
{
    public class ApplyContext : DbContext
    {
        public ApplyContext(DbContextOptions<ApplyContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Apply> Applies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplyEntityTypeConfiguration());
        }
    }
}
