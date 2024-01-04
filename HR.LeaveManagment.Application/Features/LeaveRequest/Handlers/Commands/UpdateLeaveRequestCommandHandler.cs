using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Commands;
using MediatR;
using ValidationException = HR.LeaveManagment.Application.Exceptions.ValidationException;

namespace HR.LeaveManagment.Application.Features.LeaveRequest.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, int>
    {
       // private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        //private readonly ILeaveTypeRepository _leaveTypeRepository;
        //  private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private IUnitOfWork _unitOfWork;
        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            //_leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            //_leaveTypeRepository = leaveTypeRepository;
            //_leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<int> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var LeaveRequest = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

            if (request.LeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidators(_unitOfWork.LeaveTypeRepository);
                var validing = validator.Validate(request.LeaveRequestDto);
                if (validing.IsValid == false)
                {
                    throw new ValidationException(validing);
                }
                _mapper.Map(request.LeaveRequestDto, LeaveRequest);
                await _unitOfWork.LeaveRequestRepository.Update(LeaveRequest);
                await _unitOfWork.Save();
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(LeaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation = await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationByUserID(LeaveRequest.RequestingEmployeeId, LeaveRequest.LeaveTypeId);
                    int dayRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;
                    allocation.NumberOfDays -= dayRequested;
                    await _unitOfWork.LeaveAllocationRepository.Update(allocation);
                }
                await _unitOfWork.Save();
            }

            return 1;  
        }
    }
}
