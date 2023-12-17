using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator: AbstractValidator<LeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidator()
        {
            Include(new ILeaveTypeDtoValidators());
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} Must be present");
        }
    }
}
