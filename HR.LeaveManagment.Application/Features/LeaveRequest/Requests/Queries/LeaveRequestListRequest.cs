using HR.LeaveManagment.Application.DTOs.LeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Queries
{
    public class LeaveRequestListRequest:IRequest<List<LeaveRequestDto>>
    {
    }
}
