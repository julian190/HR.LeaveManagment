using AutoMapper;
using HR.LeaveManagment.Application.Constants;
using HR.LeaveManagment.Application.Contracts.Identity;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveRequest;
using HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Queries;
using HR.LeaveManagment.Application.Models.Identity;
using HR.LeaveManagment.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR.LeaveManagment.Application.Features.LeaveRequest.Handlers.Queries
{
    internal class LeaveRequestListRequestHandler : IRequestHandler<LeaveRequestListRequest, List<LeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public LeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public async Task<List<LeaveRequestDto>> Handle(LeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaverequests = new List<Domain.LeaveRequest>();
            var requests = new List<LeaveRequestDto>();
            if(request.IsLoggedInUser)
            {
                var userid = _httpContextAccessor.HttpContext.User.FindFirst(u => u.Type == CustomClaimTypes.Uid)?.Value;
                leaverequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userid);
                var employee = await _userService.GetEmployeeById(userid);
                requests = _mapper.Map<List<LeaveRequestDto>>(leaverequests);
                foreach(var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaverequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestDto>>(leaverequests);
                foreach (var req in requests)
                {
                    req.Employee = await _userService.GetEmployeeById(req.RequestingEmployeeId);
                }
              
            }
            return requests;
        }
    }
}
