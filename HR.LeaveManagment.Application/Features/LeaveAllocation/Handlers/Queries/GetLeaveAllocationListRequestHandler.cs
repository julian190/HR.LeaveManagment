using AutoMapper;
using HR.LeaveManagment.Application.Constants;
using HR.LeaveManagment.Application.Contracts.Identity;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveAllocation;
using HR.LeaveManagment.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HR.LeaveManagment.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository _LeaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _LeaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = new List<Domain.LeaveAllocation>();
            var allocations = new List<LeaveAllocationDto>();
            if(request.isLoggedIn)
            {
                var userid = _httpContextAccessor.HttpContext.User.FindFirst(q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveAllocations = await _LeaveAllocationRepository.GetLeaveAllocationsWithDetails(userid);
                var employee = await _userService.GetEmployeeById(userid);
                allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach(LeaveAllocationDto allocation in allocations)
                {
                    allocation.Employee = employee;
                }
            }
            else
            {
                leaveAllocations = await _LeaveAllocationRepository.GetLeaveAllocationWithDetails();
                allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach(LeaveAllocationDto allocation in allocations)
                {
                    allocation.Employee = await _userService.GetEmployeeById(allocation.EmployeeId);
                }
            }
            // var leaveallocationlist = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            return allocations;
        }
    }
}
