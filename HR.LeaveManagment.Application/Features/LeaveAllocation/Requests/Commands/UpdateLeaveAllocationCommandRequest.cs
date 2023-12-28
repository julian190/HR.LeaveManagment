using HR.LeaveManagment.Application.DTOs.LeaveAllocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Features.LeaveAllocation.Requests.Commands
{
    public class UpdateLeaveAllocationCommandRequest:IRequest<int>
    {
        public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
