using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveAllocation.Validations;
using HR.LeaveManagment.Application.Exceptions;
using HR.LeaveManagment.Application.Features.LeaveAllocation.Requests.Commands;
using MediatR;

namespace HR.LeaveManagment.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class CreateLeaveAllocationCommandRequestHandler : IRequestHandler<CreateLeaveAllocationCommandRequest, int>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandRequestHandler(ILeaveAllocationRepository leaveAllocationRepository , IMapper mapper,ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<int> Handle(CreateLeaveAllocationCommandRequest request, CancellationToken cancellationToken)
        {
            var createvalitor = new CreateLeaveAllocationDtoValidation(_leaveTypeRepository);
            var validation = createvalitor.Validate(request.LeaveAllocationDto);
            if(validation.IsValid == false)
            {
                throw new ValidationException(validation);
            }
            var leaveallocation = _mapper.Map<HR.LeaveManagment.Domain.LeaveAllocation>(request.LeaveAllocationDto);
            leaveallocation = await _leaveAllocationRepository.Add(leaveallocation);
            return leaveallocation.Id;
        }
    }
}
