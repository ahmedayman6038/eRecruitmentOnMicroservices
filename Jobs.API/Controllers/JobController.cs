using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Jobs.API.Application.Commands;
using Jobs.API.Application.Entities;
using Jobs.API.Application.Queries;
using Jobs.API.Application.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobs.API.Controllers
{

    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class JobController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        //[Authorize("jobs.read")]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<JobViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetAllJobsParameter filter)
        {
            return Ok(await Mediator.Send(
                new GetAllJobsQuery() 
                { 
                    PageSize = filter.PageSize, 
                    PageNumber = filter.PageNumber, 
                    CityId = filter.CityId 
                }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize("jobs.read")]
        [ProducesResponseType(typeof(Response<JobViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetJobByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize("jobs.write")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(CreateJobCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize("jobs.write")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(int id, UpdateJobCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize("jobs.write")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteJobByIdCommand { Id = id }));
        }

        [HttpPost("Apply")]
        [Authorize("jobs.post")]
        [ProducesResponseType(typeof(Response<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Apply(ApplyJobCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
