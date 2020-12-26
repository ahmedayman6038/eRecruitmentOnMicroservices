using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Applying.API.Application.Commands;
using Applying.API.Application.Entities;
using Applying.API.Application.Queries;
using Applying.API.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Applying.API.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ApplyController : BaseApiController
    {
        [HttpGet]
        [Authorize("applying.read")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ApplyViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetAllAppliesParameter filter)
        {
            return Ok(await Mediator.Send(
                new GetAllAppliesQuery() 
                { 
                    PageSize = filter.PageSize, 
                    PageNumber = filter.PageNumber, 
                    StatusId = filter.StatusId 
                }));
        }

        [HttpGet("{id}")]
        [Authorize("applying.read")]
        [ProducesResponseType(typeof(Response<ApplyViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetApplyByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize("applying.write")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(CreateApplyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize("applying.write")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, UpdateApplyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize("applying.write")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteApplyByIdCommand { Id = id }));
        }
    }
}
