using Applying.API.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Infrastructure.EntityConfigurations
{
    public class ApplyEntityTypeConfiguration : IEntityTypeConfiguration<Apply>
    {
        public void Configure(EntityTypeBuilder<Apply> builder)
        {
            builder.ToTable("Apply");

            builder.HasKey(ci => new { ci.Id, ci.JobId, ci.UserId });

            builder.Property(ci => ci.Id).ValueGeneratedOnAdd();
        }
    }
}
