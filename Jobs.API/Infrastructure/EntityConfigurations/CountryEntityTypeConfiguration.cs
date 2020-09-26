using Jobs.API.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.EntityConfigurations
{
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
