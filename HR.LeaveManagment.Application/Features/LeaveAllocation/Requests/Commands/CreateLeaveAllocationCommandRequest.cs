using HR.LeaveManagment.Application.DTOs.LeaveAllocation;
using HR.LeaveManagment.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Features.LeaveAllocation.Requests.Commands
{
    public class CreateLeaveAllocationCommandRequest:IRequest<BaseCommandResponse>
    {
        public CreateLeveAllocationDto LeaveAllocationDto { get; set; }
    }
}
