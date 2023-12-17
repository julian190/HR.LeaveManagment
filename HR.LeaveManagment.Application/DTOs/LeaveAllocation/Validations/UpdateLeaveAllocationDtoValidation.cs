using FluentValidation;
using HR.LeaveManagment.Application.Contracts.Presistance;

namespace HR.LeaveManagment.Application.DTOs.LeaveAllocation.Validations
{
    public class UpdateLeaveAllocationDtoValidation:AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveAllocationDtoValidation(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveAllocationDtoValidation(_leaveTypeRepository));
            RuleFor(p=>p.Id).NotNull().WithMessage("{PropertyName} must be present"); ;
        }
    }
}
