using HR.LeaveManagment.Application.DTOs.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Commands
{
    public class UpdateLeaveRequestCommand: IRequest<int>
    {
        public int Id { get; set; }
        public UpdateLeaveRequestDto LeaveRequestDto { get; set; }
        public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; }
    }
}
