﻿using Jobs.API.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Queries
{
    public class GetAllCitiesParameter : RequestParameter
    {
        public int CountryId { get; set; }
    }
}
