using HR.LeaveManagment.Application.DTOs.LeaveRequest;
using HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestDto>>> Get()
        {
            var result = await _mediator.Send(new LeaveRequestListRequest());
            return Ok(result);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var result = await _mediator.Send(new LeaveRequestDetailsRequest { Id = id });
            return Ok(result);
            //return "value";
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveRequestDto LeaveRequest)
        {
            var result = await _mediator.Send(new CreateLeaveRequestCommand { LeaveRequestDto = LeaveRequest });
            return Ok(result);
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto LeaveRequest)
        {
            var result = await _mediator.Send(new UpdateLeaveRequestCommand {Id = id, LeaveRequestDto= LeaveRequest });
            return NoContent();
        }
        [HttpPut("{ChangeApproval}/{id}")]
        public async Task<ActionResult> ChangeApproval(int id, [FromBody] ChangeLeaveRequestApprovalDto LeaveRequest)
        {
            var result = await _mediator.Send(new UpdateLeaveRequestCommand { Id = id, ChangeLeaveRequestApprovalDto = LeaveRequest });
            return NoContent();
        }
        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
