using HR.LeaveManagment.Application.DTOs.LeaveRequest;
using HR.LeaveManagment.Application.DTOs.LeaveType;
using HR.LeaveManagment.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagment.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagment.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagment.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]

    public class LeaveTypesController : Controller
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator) {
            _mediator = mediator;
        }

        // GET: LeaveTypesController
        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
           // return View();
        }

        // GET: LeaveTypesController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult <LeaveTypeDto>> Get(int id)
        {
            var LeaveType = await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
            return Ok(LeaveType);
        }

     

        // POST: LeaveTypesController/Create
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveTypeDto leaveType)
        {
            var command = new CreateLeaveTypeCommand { LeaveTypeDto = leaveType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

       

        // POST: LeaveTypesController/Edit/5
        [HttpPut]
        
        public async Task<ActionResult> Edit(int id, [FromBody]LeaveTypeDto leaveType)
        {
            var command = new UpdateLeaveTypeCommand { leaveTypeDto = leaveType };
            var response = await _mediator.Send(command);
            return Ok(response);

            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: LeaveTypesController/Delete/5
       
        [HttpDelete("{Id}")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
           throw new NotImplementedException();
        }
    }
}
