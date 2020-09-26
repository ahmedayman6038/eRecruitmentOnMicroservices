﻿using Jobs.API.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Infrastructure.EntityConfigurations
{
    public class CityEntityTypeConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(ci => ci.Country)
                .WithMany()
                .HasForeignKey(ci => ci.CountryId);
        }
    }
}
