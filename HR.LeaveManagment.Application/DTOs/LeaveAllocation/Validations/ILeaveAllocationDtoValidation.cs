using FluentValidation;
using HR.LeaveManagment.Application.Contracts.Presistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.DTOs.LeaveAllocation.Validations
{
    public class ILeaveAllocationDtoValidation:AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveAllocationDtoValidation(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName should be greater than 0}");
            RuleFor(p => p.Period).GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}");
            RuleFor(p => p.LeaveTypeId).GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var LeavetypeExists = await _leaveTypeRepository.Exists(id);
                    return LeavetypeExists;
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
