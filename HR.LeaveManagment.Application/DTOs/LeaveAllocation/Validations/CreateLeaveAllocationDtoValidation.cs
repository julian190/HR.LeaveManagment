using FluentValidation;
using HR.LeaveManagment.Application.Contracts.Presistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.DTOs.LeaveAllocation.Validations
{
    public class CreateLeaveAllocationDtoValidation:AbstractValidator<CreateLeveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationDtoValidation(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            RuleFor(p => p.LeaveTypeId)
                          .GreaterThan(0)
                          .MustAsync(async (id, token) =>
                          {
                              var leaveTypeExists = await _leaveTypeRepository.Exists(id);
                              return leaveTypeExists;
                          })
                          .WithMessage("{PropertyName} does not exist.");
        }
    }
}
