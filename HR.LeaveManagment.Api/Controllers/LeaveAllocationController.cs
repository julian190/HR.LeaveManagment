using HR.LeaveManagment.Application.DTOs.LeaveAllocation;
using HR.LeaveManagment.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagment.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator )
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<ActionResult <List<LeaveAllocationDto>>> Get()
        {
            var result = await _mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(result);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int id)
        {
            var result = await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
            return Ok(result);
            //return "value";
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeveAllocationDto LeaveAllocation)
        {
            var Result = await _mediator.Send(new CreateLeaveAllocationCommandRequest { LeaveAllocationDto = LeaveAllocation });
            return NoContent();
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveAllocationDto LeaveAllocation)
        {
            var result = await _mediator.Send(new UpdateLeaveAllocationCommandRequest { LeaveAllocationDto= LeaveAllocation });
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
