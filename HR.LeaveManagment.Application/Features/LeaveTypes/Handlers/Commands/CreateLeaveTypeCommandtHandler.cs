using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagment.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagment.Domain;
using MediatR;
using ValidationException = HR.LeaveManagment.Application.Exceptions.ValidationException;

namespace HR.LeaveManagment.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandtHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandtHandler(ILeaveTypeRepository  leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validtors = new ILeaveTypeDtoValidators();
            var validationresult = await validtors.ValidateAsync((DTOs.LeaveType.ILeaveTypeDto)request.LeaveTypeDto);
            if (validationresult.IsValid == false)
            {
                throw new ValidationException(validationresult);
            }
            var leavetype = _mapper.Map<LeaveType>(request.LeaveTypeDto);
            leavetype = await _leaveTypeRepository.Add(leavetype);
            return leavetype.Id;
        }
    }
}
