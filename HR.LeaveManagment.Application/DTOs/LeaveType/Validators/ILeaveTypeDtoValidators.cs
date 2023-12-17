using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagment.Application.DTOs.LeaveType.Validators
{
    public class ILeaveTypeDtoValidators: AbstractValidator<ILeaveTypeDto>
    {
        public ILeaveTypeDtoValidators()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("{PropertyName} is rquired.}")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 charcters");

            RuleFor(p => p.DefaultDays)
                .NotEmpty().WithMessage("{PropertyName} is rquired.}")
                .GreaterThan(0)
                .LessThan(100); 
        }
    }
}
