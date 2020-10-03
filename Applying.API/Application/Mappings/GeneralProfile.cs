using Applying.API.Application.Commands;
using Applying.API.Application.Enums;
using Applying.API.Application.Models;
using Applying.API.Application.Queries;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateApplyCommand, Apply>();

            CreateMap<Apply, GetAllAppliesViewModel>()
               .ForMember(dest => dest.Status, source => source.MapFrom(source => ((ApplyStatus)source.Status).ToString()));

            CreateMap<GetAllAppliesQuery, GetAllAppliesParameter>();
        }
    }
}
