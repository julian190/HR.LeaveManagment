using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveType.Validators;
using HR.LeaveManagment.Application.Exceptions;
using HR.LeaveManagment.Application.Features.LeaveTypes.Requests.Commands;
using MediatR;

namespace HR.LeaveManagment.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, int>
    {
        //private readonly ILeaveTypeRepository _leaveTypeRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            //_leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var updateleavetypevalidator = new UpdateLeaveTypeDtoValidator();
            var validateRquest = updateleavetypevalidator.Validate(request.leaveTypeDto);
            if(validateRquest.IsValid == false)
            {
                throw new ValidationException(validateRquest);
            }
            var leavetype = await _unitOfWork.LeaveTypeRepository.Get(request.leaveTypeDto.Id);
            _mapper.Map(request.leaveTypeDto, leavetype);
            await _unitOfWork.LeaveTypeRepository.Update(leavetype);
            await _unitOfWork.Save();
            return 1;

        }
    }
}
