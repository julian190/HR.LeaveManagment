using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Identity;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveAllocation.Validations;
using HR.LeaveManagment.Application.Exceptions;
using HR.LeaveManagment.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagment.Application.Models.Identity;
using HR.LeaveManagment.Application.Responses;
using HR.LeaveManagment.Domain;
using MediatR;

namespace HR.LeaveManagment.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class CreateLeaveAllocationCommandRequestHandler : IRequestHandler<CreateLeaveAllocationCommandRequest, BaseCommandResponse>
    {
      //  private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        private IUserService _userService;
        private IUnitOfWork _unitOfWork;
        public CreateLeaveAllocationCommandRequestHandler(IMapper mapper, IUserService userService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommandRequest request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            var createvalitor = new CreateLeaveAllocationDtoValidation(_unitOfWork.LeaveTypeRepository);
            var validation = await createvalitor.ValidateAsync(request.LeaveAllocationDto);
            if(validation.IsValid == false)
            {
                response.Success = false;
                response.Message = "Validation failed";
                response.Errors = validation.Errors.Select(x => x.ErrorMessage).ToList();
            }
            LeaveType leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.LeaveAllocationDto.LeaveTypeId);
            List<Employee> employees = await _userService.GetEmployees();
            int period = DateTime.Now.Year;
            List<HR.LeaveManagment.Domain.LeaveAllocation> leaveAllocations = new List<Domain.LeaveAllocation>();
            foreach (var employee in employees)
            {
                if (await _unitOfWork.LeaveAllocationRepository.AllocationExists(employee.Id, leaveType.Id, period))
                    continue;
                leaveAllocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveType = leaveType,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period,
                });
            }
            await _unitOfWork.LeaveAllocationRepository.AddAllocation(leaveAllocations);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Created sucessfully";
            return response;

        }
    }
}
