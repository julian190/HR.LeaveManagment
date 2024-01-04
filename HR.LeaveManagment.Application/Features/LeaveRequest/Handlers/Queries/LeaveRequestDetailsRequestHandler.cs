using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Identity;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveRequest;
using HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;

namespace HR.LeaveManagment.Application.Features.LeaveRequest.Handlers.Queries
{
    public class LeaveRequestDetailsRequestHandler : IRequestHandler<LeaveRequestDetailsRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public LeaveRequestDetailsRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<LeaveRequestDto> Handle(LeaveRequestDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaverequest = _mapper.Map<LeaveRequestDto>(await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
            leaverequest.Employee = await _userService.GetEmployeeById(leaverequest.RequestingEmployeeId);
            return leaverequest;
        }
    }
}
