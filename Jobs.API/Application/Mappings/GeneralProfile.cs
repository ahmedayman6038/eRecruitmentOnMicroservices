using AutoMapper;
using Jobs.API.Application.Commands;
using Jobs.API.Application.Entities;
using Jobs.API.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Job, JobViewModel>()
                .ForMember(dest => dest.City, source => source.MapFrom(source => source.City.Name))
                .ForMember(dest => dest.Country, source => source.MapFrom(source => source.City.Country.Name))
                .ForMember(dest => dest.CityId, source => source.MapFrom(source => source.City.Id))
                .ForMember(dest => dest.CountryId, source => source.MapFrom(source => source.City.Country.Id));

            CreateMap<CreateJobCommand, Job>();

            CreateMap<GetAllJobsQuery, GetAllJobsParameter>();

            CreateMap<GetAllCountriesQuery, GetAllCountriesParameter>();

            CreateMap<GetAllCitiesQuery, GetAllCitiesParameter>();
        }
    }
}
