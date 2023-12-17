using AutoMapper;
using HR.LeaveManagment.Application.Features.LeaveRequest.Requests.Commands;
using MediatR;
using HR.LeaveManagment.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagment.Application.Exceptions;
using HR.LeaveManagment.Application.Contracts.Presistance;
using HR.LeaveManagment.Application.Models;
using HR.LeaveManagment.Application.Contracts.Infrastructure;

namespace HR.LeaveManagment.Application.Features.LeaveRequest.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository , IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
        }
        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var LeaveRequestValidators = new CreateLeaveRequestDtoValidators(_leaveTypeRepository);
            var validaterequest = LeaveRequestValidators.Validate(request.LeaveRequestDto);
            if (validaterequest.IsValid == false)
            {
                throw new ValidationException(validaterequest);
            }

            var leaverequest = _mapper.Map<HR.LeaveManagment.Domain.LeaveRequest>(request.LeaveRequestDto);
            leaverequest = await _leaveRequestRepository.Add(leaverequest);

            //Send email logic
            Email emailobj = new Email();
            emailobj.To = "employe@company.org";
            emailobj.Subject = "Leave request submitted";
            emailobj.Body = $"Your leave request for {request.LeaveRequestDto.StartDate} to {request.LeaveRequestDto.EndDate} has been submitted sucessfully";
            try
            {
                await _emailSender.Sendemail(emailobj);
            }
            catch (Exception ex)
            {
                //log the error
            }
            return leaverequest.Id;
        }
    }
}
