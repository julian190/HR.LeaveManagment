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
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository , IMapper mapper,ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<int> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var LeaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

            var validator = new UpdateLeaveRequestDtoValidators(_leaveTypeRepository);
            var validing = validator.Validate(request.LeaveRequestDto);
            if (validing.IsValid == false)
            {
                throw new ValidationException(validing);
            }
            if (request.LeaveRequestDto != null)
            {
                _mapper.Map(request.LeaveRequestDto, LeaveRequest);
                await _leaveRequestRepository.Update(LeaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await _leaveRequestRepository.ChangeApprovalStatus(LeaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
            }

            return 1;  
        }
    }
}
