using Jobs.API.Application.Entities;
using Jobs.API.Application.Queries;
using Jobs.API.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Jobs.API.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CityController : BaseApiController
    {
        [HttpGet]
        [Authorize("jobs.read")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<City>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetAllCitiesParameter filter)
        {
            return Ok(await Mediator.Send(
                new GetAllCitiesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    CountryId = filter.CountryId
                }));
        }
    }
}
