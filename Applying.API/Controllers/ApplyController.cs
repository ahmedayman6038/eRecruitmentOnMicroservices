using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applying.API.Application.Commands;
using Applying.API.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Applying.API.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ApplyController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [Authorize("applying.read")]
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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize("applying.read")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetApplyByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize("applying.write")]
        public async Task<IActionResult> Post(CreateApplyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize("applying.write")]
        public async Task<IActionResult> Put(int id, UpdateApplyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize("applying.write")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteApplyByIdCommand { Id = id }));
        }
    }
}
