using FluentValidation;
using HR.LeaveManagment.Application.Contracts.Presistance;

namespace HR.LeaveManagment.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidators:AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveRequestDtoValidators(ILeaveTypeRepository leaveTypeRepository) {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDtoValidators(_leaveTypeRepository));
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
