using AutoMapper;
using FluentValidation;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagment.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagment.Application.Responses;
using HR.LeaveManagment.Domain;
using MediatR;
using ValidationException = HR.LeaveManagment.Application.Exceptions.ValidationException;

namespace HR.LeaveManagment.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandtHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandtHandler(ILeaveTypeRepository  leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            var validtors = new ILeaveTypeDtoValidators();
            var validationresult = await validtors.ValidateAsync(request.LeaveTypeDto);
            if (validationresult.IsValid == false)
            {
                response.Success = false;
                response.Message = "CreationFailed";
                response.Errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {
                var leavetype = _mapper.Map<LeaveType>(request.LeaveTypeDto);
                leavetype = await _leaveTypeRepository.Add(leavetype);
                response.Success = true;
                response.Message = "Create Sucessful";
                response.Id = leavetype.Id;
            }
           return response;
        }
    }
}
